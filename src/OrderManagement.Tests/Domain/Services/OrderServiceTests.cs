using FluentAssertions;
using Moq;
using OrderManagement.Domain.Aggregates;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.Services;
using OrderManagement.Domain.Services.Models;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Tests.Domain.Services;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepoMock = new();
    private readonly Mock<IProductRepository> _productRepoMock = new();
    private readonly OrderService _orderService;

    // Test data
    private readonly Address _testAddress = new("Main St", "Warsaw", "00-001");
    private readonly Customer _testCustomer = new("John", "Doe");
    private readonly Product _testProduct = new Product(
        new ProductId(Guid.NewGuid()),
        "Test Product",
        new Price(100),
        "Desc"
    );
    private readonly Quantity _testQuantity = new(2);

    public OrderServiceTests()
    {
        _orderService = new OrderService(
            _orderRepoMock.Object,
            _productRepoMock.Object
        );
    }

    [Fact]
    public async Task CreateOrderAsync_ProductNotFound_ThrowsException()
    {
        // Arrange
        var model = new CreateOrderModel(
            Street: "Main St",
            City: "Warsaw",
            PostalCode: "00-001",
            CustomerName: "John",
            CustomerSurname: "Doe",
            ProductId: Guid.NewGuid(),
            Quantity: 2
        );

        _productRepoMock.Setup(r => r.GetByIdAsync(model.ProductId))
            .Throws<KeyNotFoundException>();

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _orderService.CreateOrderAsync(model, CancellationToken.None)
        );
    }

    [Fact]
    public async Task AddItemToOrderAsync_ValidData_AddsItemAndUpdatesOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new Order(_testAddress, _testCustomer, new OrderItem(_testProduct, _testQuantity));
        var newProduct = new Product(new ProductId(Guid.NewGuid()), "New Product", new Price(50), "Desc");

        var model = new AddItemToOrderModel(
            OrderId: orderId,
            ProductId: newProduct.Id,
            Quantity: 3
        );

        _orderRepoMock.Setup(r => r.GetByIdAsync(model.OrderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        _productRepoMock.Setup(r => r.GetByIdAsync(model.ProductId))
            .ReturnsAsync(newProduct);

        // Act
        await _orderService.AddItemToOrderAsync(model, CancellationToken.None);

        // Assert
        order.Items.Should().HaveCount(2);
        order.Items.Should().Contain(i => i.ProductId == newProduct.Id);

        _orderRepoMock.Verify(r =>
            r.UpdateAsync(order, It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task AddItemToOrderAsync_InvalidStatus_ThrowsDomainException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new Order(_testAddress, _testCustomer, new OrderItem(_testProduct, _testQuantity));
        order.ChangeStatus(OrderStatus.Submitted); // Zmiana statusu - nie można modyfikować

        var model = new AddItemToOrderModel(
            OrderId: orderId,
            ProductId: _testProduct.Id,
            Quantity: 1
        );

        _orderRepoMock.Setup(r => r.GetByIdAsync(model.OrderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() =>
            _orderService.AddItemToOrderAsync(model, CancellationToken.None)
        );
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_ValidData_UpdatesStatus()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new Order(_testAddress, _testCustomer, new OrderItem(_testProduct, _testQuantity));
        var newStatus = OrderStatus.Submitted;

        var model = new UpdateOrderStatusModel(
            orderId: orderId,
            status: "Submitted" // Poprawna nazwa enum
        );

        _orderRepoMock.Setup(r => r.GetByIdAsync(model.orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        // Act
        await _orderService.UpdateOrderStatusAsync(model, CancellationToken.None);

        // Assert
        order.Status.Should().Be(newStatus);
        _orderRepoMock.Verify(r =>
            r.UpdateAsync(order, It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_InvalidStatusString_ThrowsException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new Order(_testAddress, _testCustomer, new OrderItem(_testProduct, _testQuantity));

        var model = new UpdateOrderStatusModel(
            orderId: orderId,
            status: "InvalidStatus" // Niepoprawna nazwa
        );

        _orderRepoMock.Setup(r => r.GetByIdAsync(model.orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _orderService.UpdateOrderStatusAsync(model, CancellationToken.None)
        );
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_InvalidStatusTransition_ThrowsDomainException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new Order(_testAddress, _testCustomer, new OrderItem(_testProduct, _testQuantity));
        order.ChangeStatus(OrderStatus.Paid); // Status, który nie pozwala na zmianę

        var model = new UpdateOrderStatusModel(
            orderId: orderId,
            status: "Submitted"
        );

        _orderRepoMock.Setup(r => r.GetByIdAsync(model.orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() =>
            _orderService.UpdateOrderStatusAsync(model, CancellationToken.None)
        );
    }

    [Fact]
    public async Task GetAsync_ExistingOrder_ReturnsOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var expectedOrder = new Order(_testAddress, _testCustomer, new OrderItem(_testProduct, _testQuantity));

        _orderRepoMock.Setup(r => r.GetByIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedOrder);

        // Act
        var result = await _orderService.GetAsync(orderId, CancellationToken.None);

        // Assert
        result.Should().Be(expectedOrder);
    }
}
