using BusinessLogic.UnitTests.OrderManagement.TestUtilities;
using Moq;
using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Services;
using PointOfSale.BusinessLogic.PaymentProcessing.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.PaymentProcessing.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.PaymentProcessing.Enums;

namespace BusinessLogic.UnitTests.OrderManagement.Services;

public class OrderServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IProductRepository> _productRepository;
    private readonly Mock<IModifierRepository> _modifierRepository;
    private readonly Mock<IOrderRepository> _orderRepository;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        var orderMappingService = new OrderMappingService();
        _unitOfWork = new Mock<IUnitOfWork>();
        _productRepository = new Mock<IProductRepository>();
        _modifierRepository = new Mock<IModifierRepository>();
        _orderRepository = new Mock<IOrderRepository>();
        var serviceChargeRepository = new Mock<IServiceChargeRepository>();
        var paymentRepository = new Mock<IPaymentRepository>();
        var paymentHandlerFactory = new Mock<Func<PaymentMethod, IPaymentHandler>>();

        _orderService = new OrderService(
            _unitOfWork.Object,
            _productRepository.Object,
            _modifierRepository.Object,
            _orderRepository.Object,
            orderMappingService,
            serviceChargeRepository.Object,
            paymentRepository.Object,
            paymentHandlerFactory.Object
        );
    }

    [Fact]
    public async Task CreateOrder_SingleItem_CreatesOrder()
    {
        // Arrange
        var foodTax = new TaxBuilder().WithId(11).WithName("Food Tax").WithRate(0.02m).Build();

        var coffee = new ProductBuilder()
            .WithId(1)
            .WithName("Coffee")
            .WithPrice(2.99m)
            .WithStock(10)
            .WithTaxes([foodTax])
            .WithModifiers([])
            .Build();

        _productRepository
            .Setup(self => self.GetManyWithRelatedData(It.IsAny<IEnumerable<int>>()))
            .ReturnsAsync([coffee]);

        var createOrderDTO = new CreateOrderDTO
        {
            OrderItems =
            [
                new CreateOrUpdateOrderItemDTO
                {
                    ProductId = coffee.Id,
                    ModifierIds = [],
                    Quantity = 2,
                },
            ],
            ServiceChargeIds = [],
        };

        // Act
        var orderDTO = await _orderService.CreateOrder(createOrderDTO);

        // Assert
        Assert.Equal(OrderStatus.Open, orderDTO.Status);
        Assert.Equal(createOrderDTO.OrderItems.Count, orderDTO.OrderItems.Count);
        Assert.Equal(coffee.Name, orderDTO.OrderItems[0].Name);
        Assert.Equal(createOrderDTO.OrderItems[0].Quantity, orderDTO.OrderItems[0].Quantity);
        Assert.Equal(6.10m, orderDTO.OrderItems[0].TotalPrice);
        Assert.Equal(6.10m, orderDTO.TotalPrice);

        Assert.Equal(8, coffee.Stock);

        _unitOfWork.Verify(self => self.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task CreateOrder_MultipleDifferentItems_CreatesOrder()
    {
        // Arrange
        var foodTax = new TaxBuilder().WithId(11).WithName("Food Tax").WithRate(0.02m).Build();

        var environmentalTax = new TaxBuilder().WithId(12).WithName("Environmental Tax").WithRate(0.05m).Build();

        var almondMilkModifier = new ModifierBuilder()
            .WithId(111)
            .WithName("Almond milk")
            .WithPrice(0.49m)
            .WithStock(10)
            .Build();

        var additionalSugarModifier = new ModifierBuilder()
            .WithId(112)
            .WithName("Additional sugar")
            .WithPrice(0.29m)
            .WithStock(10)
            .Build();

        var coffee = new ProductBuilder()
            .WithId(1)
            .WithName("Coffee")
            .WithPrice(2.99m)
            .WithStock(10)
            .WithTaxes([foodTax])
            .WithModifiers([almondMilkModifier, additionalSugarModifier])
            .Build();

        var tea = new ProductBuilder()
            .WithId(2)
            .WithName("Tea")
            .WithPrice(2.39m)
            .WithStock(10)
            .WithTaxes([foodTax, environmentalTax])
            .WithModifiers([additionalSugarModifier])
            .Build();

        _productRepository
            .Setup(self => self.GetManyWithRelatedData(It.IsAny<IEnumerable<int>>()))
            .ReturnsAsync([coffee, tea]);

        var createOrderDTO = new CreateOrderDTO
        {
            OrderItems =
            [
                new CreateOrUpdateOrderItemDTO
                {
                    ProductId = coffee.Id,
                    ModifierIds = [almondMilkModifier.Id, additionalSugarModifier.Id],
                    Quantity = 2,
                },
                new CreateOrUpdateOrderItemDTO
                {
                    ProductId = tea.Id,
                    ModifierIds = [additionalSugarModifier.Id],
                    Quantity = 3,
                },
            ],
            ServiceChargeIds = [],
        };

        // Act
        var orderDTO = await _orderService.CreateOrder(createOrderDTO);

        // Assert
        Assert.Equal(OrderStatus.Open, orderDTO.Status);
        Assert.Equal(createOrderDTO.OrderItems.Count, orderDTO.OrderItems.Count);
        Assert.Equal(coffee.Name, orderDTO.OrderItems[0].Name);
        Assert.Equal(createOrderDTO.OrderItems[0].Quantity, orderDTO.OrderItems[0].Quantity);
        Assert.Equal(7.69m, orderDTO.OrderItems[0].TotalPrice);
        Assert.Equal(tea.Name, orderDTO.OrderItems[1].Name);
        Assert.Equal(createOrderDTO.OrderItems[1].Quantity, orderDTO.OrderItems[1].Quantity);
        Assert.Equal(8.60m, orderDTO.OrderItems[1].TotalPrice);
        Assert.Equal(16.29m, orderDTO.TotalPrice);

        Assert.Equal(8, coffee.Stock);
        Assert.Equal(7, tea.Stock);
        Assert.Equal(8, almondMilkModifier.Stock);
        Assert.Equal(5, additionalSugarModifier.Stock);

        _unitOfWork.Verify(self => self.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task CreateOrder_ProductNotFound_Throws()
    {
        // Arrange
        _productRepository.Setup(self => self.GetManyWithRelatedData(It.IsAny<IEnumerable<int>>())).ReturnsAsync([]);

        var createOrderDTO = new CreateOrderDTO
        {
            OrderItems =
            [
                new CreateOrUpdateOrderItemDTO
                {
                    ProductId = 1,
                    ModifierIds = [],
                    Quantity = 2,
                },
            ],
            ServiceChargeIds = [],
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            async () => await _orderService.CreateOrder(createOrderDTO)
        );
        Assert.IsType<ProductNotFoundErrorMessage>(exception.ErrorMessage);
    }

    [Fact]
    public async Task CreateOrder_IncompatibleProductModifier_Throws()
    {
        // Arrange
        var coffee = new ProductBuilder()
            .WithId(1)
            .WithName("Coffee")
            .WithPrice(2.99m)
            .WithStock(10)
            .WithTaxes([])
            .WithModifiers([])
            .Build();

        _productRepository
            .Setup(self => self.GetManyWithRelatedData(It.IsAny<IEnumerable<int>>()))
            .ReturnsAsync([coffee]);

        var createOrderDTO = new CreateOrderDTO
        {
            OrderItems =
            [
                new CreateOrUpdateOrderItemDTO
                {
                    ProductId = 1,
                    ModifierIds = [11],
                    Quantity = 2,
                },
            ],
            ServiceChargeIds = [],
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            async () => await _orderService.CreateOrder(createOrderDTO)
        );
        Assert.IsType<IncompatibleProductModifierErrorMessage>(exception.ErrorMessage);
    }

    [Fact]
    public async Task CreateOrder_ProductOutOfStock_Throws()
    {
        // Arrange
        var coffee = new ProductBuilder()
            .WithId(1)
            .WithName("Coffee")
            .WithPrice(2.99m)
            .WithStock(1)
            .WithTaxes([])
            .WithModifiers([])
            .Build();

        _productRepository
            .Setup(self => self.GetManyWithRelatedData(It.IsAny<IEnumerable<int>>()))
            .ReturnsAsync([coffee]);

        var createOrderDTO = new CreateOrderDTO
        {
            OrderItems =
            [
                new CreateOrUpdateOrderItemDTO
                {
                    ProductId = 1,
                    ModifierIds = [],
                    Quantity = 2,
                },
            ],
            ServiceChargeIds = [],
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            async () => await _orderService.CreateOrder(createOrderDTO)
        );
        Assert.IsType<ProductOutOfStockErrorMessage>(exception.ErrorMessage);
    }

    [Fact]
    public async Task CreateOrder_ModifierOutOfStock_Throws()
    {
        // Arrange
        var almondMilkModifier = new ModifierBuilder()
            .WithId(111)
            .WithName("Almond milk")
            .WithPrice(0.49m)
            .WithStock(1)
            .Build();

        var coffee = new ProductBuilder()
            .WithId(1)
            .WithName("Coffee")
            .WithPrice(2.99m)
            .WithStock(2)
            .WithTaxes([])
            .WithModifiers([almondMilkModifier])
            .Build();

        _productRepository
            .Setup(self => self.GetManyWithRelatedData(It.IsAny<IEnumerable<int>>()))
            .ReturnsAsync([coffee]);

        var createOrderDTO = new CreateOrderDTO
        {
            OrderItems =
            [
                new CreateOrUpdateOrderItemDTO
                {
                    ProductId = 1,
                    ModifierIds = [almondMilkModifier.Id],
                    Quantity = 2,
                },
            ],
            ServiceChargeIds = []
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            async () => await _orderService.CreateOrder(createOrderDTO)
        );
        Assert.IsType<ModifierOutOfStockErrorMessage>(exception.ErrorMessage);
    }

    [Theory]
    [InlineData(OrderStatus.Closed)]
    [InlineData(OrderStatus.Canceled)]
    [InlineData(OrderStatus.Refunded)]
    public async Task CancelOrder_OrderIsNotOpen_Throws(OrderStatus orderStatus)
    {
        // Arrange
        var order = new OrderBuilder().WithId(1).WithStatus(orderStatus).Build();

        _orderRepository.Setup(self => self.GetWithOrderItems(order.Id)).ReturnsAsync(order);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            async () => await _orderService.CancelOrder(order.Id)
        );
        Assert.IsType<CannotModifyNonOpenOrderErrorMessage>(exception.ErrorMessage);
    }
}
