using FluentAssertions;
using OrderManagement.Domain.Aggregates;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Tests.Domain.Aggregates;

public class OrderTests
{
    private Address CreateTestAddress() =>
        new("Street", "Wroclaw", "00-001");

    private Customer CreateTestCustomer() =>
        new("John", "Doe");

    private Product CreateTestProduct(decimal price = 100) =>
        new(new ProductId(Guid.NewGuid()), "Test Product", new Price(price), "Test Description");

    private Quantity CreateTestQuantity(int value = 2) =>
        new(value);

    private Order CreateTestOrderItem(Product? product = null, Quantity? quantity = null)
    {
        product ??= CreateTestProduct();
        quantity ??= CreateTestQuantity();
        var item = new OrderItem(product, quantity);

        return new Order(
            shippingAddress: CreateTestAddress(),
            customer: CreateTestCustomer(),
            item: item
        );
    }

    [Fact]
    public void Constructor_WithValidParams_InitializesCorrectly()
    {
        // Arrange
        var address = CreateTestAddress();
        var customer = CreateTestCustomer();
        var product = CreateTestProduct();
        var quantity = CreateTestQuantity();
        var item = new OrderItem(product, quantity);

        // Act
        var order = new Order(address, customer, item);

        // Assert
        order.Id.Should().NotBeNull();
        order.ShippingAddress.Should().Be(address);
        order.Customer.Should().Be(customer);
        order.Status.Should().Be(OrderStatus.Draft);
        order.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        order.LastModifiedDate.Should().BeNull();
        order.Items.Should().ContainSingle().Which.Should().BeEquivalentTo(item);
        order.TotalPrice.Should().Be(new Price(product.Price.Value * quantity.Value));
    }

    [Fact]
    public void AddItem_ToDraftOrder_AddsNewItem()
    {
        // Arrange
        var order = CreateTestOrderItem();
        var newProduct = CreateTestProduct(50);
        var newQuantity = CreateTestQuantity(3);
        var initialItemCount = order.Items.Count;

        // Act
        order.AddItem(newProduct, newQuantity);

        // Assert
        order.Items.Should().HaveCount(initialItemCount + 1);
        order.LastModifiedDate.Should().NotBeNull();
        order.Items.Should().Contain(i =>
            i.ProductId == newProduct.Id &&
            i.Quantity == newQuantity
        );
    }

    [Fact]
    public void AddItem_ToDraftOrder_ExistingProduct_UpdatesQuantity()
    {
        // Arrange
        var product = CreateTestProduct();
        var initialQuantity = CreateTestQuantity(2);
        var order = CreateTestOrderItem(product, initialQuantity);

        var additionalQuantity = CreateTestQuantity(3);

        // Act
        order.AddItem(product, additionalQuantity);

        // Assert
        order.Items.Should().ContainSingle();
        var item = order.Items.Single();
        item.Quantity.Should().Be(new Quantity(initialQuantity.Value + additionalQuantity.Value));
        order.TotalPrice.Should().Be(new Price(product.Price.Value * (initialQuantity.Value + additionalQuantity.Value)));
    }

    [Theory]
    [InlineData(OrderStatus.Submitted)]
    [InlineData(OrderStatus.Paid)]
    [InlineData(OrderStatus.Cancelled)]
    public void AddItem_ToNonDraftOrder_ThrowsDomainException(OrderStatus status)
    {
        // Arrange
        var order = CreateTestOrderItem();
        order.ChangeStatus(status);

        var newProduct = CreateTestProduct();
        var newQuantity = CreateTestQuantity();

        // Act & Assert
        order.Invoking(o => o.AddItem(newProduct, newQuantity))
            .Should().Throw<DomainException>()
            .WithMessage("Only draft orders can be modified");
    }

    [Fact]
    public void ChangeStatus_ValidTransition_UpdatesStatus()
    {
        // Arrange
        var order = CreateTestOrderItem();
        var newStatus = OrderStatus.Submitted;

        // Act
        order.ChangeStatus(newStatus);

        // Assert
        order.Status.Should().Be(newStatus);
        order.LastModifiedDate.Should().NotBeNull();
    }

    [Fact]
    public void ChangeStatus_FromPaid_ThrowsDomainException()
    {
        // Arrange
        var order = CreateTestOrderItem();
        order.ChangeStatus(OrderStatus.Paid);

        // Act & Assert
        order.Invoking(o => o.ChangeStatus(OrderStatus.Submitted))
            .Should().Throw<DomainException>()
            .WithMessage("Cannot change paid or cancelled order");
    }

    [Fact]
    public void ChangeStatus_FromCancelled_ThrowsDomainException()
    {
        // Arrange
        var order = CreateTestOrderItem();
        order.ChangeStatus(OrderStatus.Cancelled);

        // Act & Assert
        order.Invoking(o => o.ChangeStatus(OrderStatus.Submitted))
            .Should().Throw<DomainException>()
            .WithMessage("Cannot change paid or cancelled order");
    }

    [Fact]
    public void ChangeStatus_SameStatus_UpdatesLastModifiedDate()
    {
        // Arrange
        var order = CreateTestOrderItem();
        var initialModifiedDate = order.LastModifiedDate;
        order.ChangeStatus(OrderStatus.Submitted);
        var firstChangeDate = order.LastModifiedDate;

        // Act
        Thread.Sleep(100); // Ensure time difference
        order.ChangeStatus(OrderStatus.Submitted);

        // Assert
        order.LastModifiedDate.Should().NotBe(initialModifiedDate);
        order.LastModifiedDate.Should().NotBe(firstChangeDate);
        order.LastModifiedDate.Should().BeAfter(firstChangeDate!.Value);
    }

    [Fact]
    public void TotalPrice_WithMultipleItems_CalculatesCorrectly()
    {
        // Arrange
        var order = CreateTestOrderItem();
        var product1 = CreateTestProduct(100);
        var product2 = CreateTestProduct(50);

        // Act
        order.AddItem(product1, CreateTestQuantity(3));
        order.AddItem(product2, CreateTestQuantity(2));

        // Assert
        var expectedTotal =
            (order.Items.ElementAt(0).TotalPrice.Value) +
            (100 * 3) +
            (50 * 2);

        order.TotalPrice.Should().Be(new Price(expectedTotal));
    }

    [Fact]
    public void AddItem_NullProduct_ThrowsException()
    {
        // Arrange
        var order = CreateTestOrderItem();

        // Act & Assert
        order.Invoking(o => o.AddItem(null!, CreateTestQuantity()))
            .Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void AddItem_ZeroQuantity_ThrowsException()
    {
        // Arrange
        var order = CreateTestOrderItem();
        var product = CreateTestProduct();

        // Act & Assert
        order.Invoking(o => o.AddItem(product, 0))
            .Should().Throw<DomainException>();
    }
}
