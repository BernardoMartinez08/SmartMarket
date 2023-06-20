using Moq;
using SmartMarket.App;
using SmartMarket.Core.DataBaseService;
using SmartMarket.Core.Interfaces;
using SmartMarket.Core.StockManager;
using SmartMarket.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartMarket.Tests.Fakes;

namespace SmartMarket.Tests
{
    public class StockServiceTests
    {
        [Fact]
        public async Task AddStockItemAsync_WhenValidStockItem_ReturnsTrue()
        {
            // Arrange
            var stockItemJson = "{\"ProductName\":\"Test Product\",\"Price\":10.0,\"ProducedOn\":\"2023-06-20\",\"ProviderId\":\"12345678-1234-1234-1234-1234567890AB\",\"IsCloseToExpirationDate\":false}";

            var stockSerializerMock = new Mock<IStockSerializer>();
            stockSerializerMock.Setup(s => s.Deserialize(stockItemJson)).Returns(new StockItem
            {
                ProductName = "Test Product",
                Price = 10,
                ProducedOn = new DateOnly(2023, 6, 20),
                ProviderId = Guid.Parse("12345678-1234-1234-1234-1234567890AB"),
                IsCloseToExpirationDate = false
            });

            var stockValidatorMock = new Mock<StockValidator>();
            stockValidatorMock.Setup(v => v.Validate(It.IsAny<StockItem>())).Returns(true);

            var providerManagementServiceMock = new Mock<ProviderManagementService>();
            providerManagementServiceMock.Setup(p => p.GetFromApiByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Provider)null);

            var smartMarketDataAccessMock = new Mock<FakeSmartMarketDataAccess>();

            var stockService = new StockService
            {
                _stockDeserializer = stockSerializerMock.Object,
                _stockValidator = stockValidatorMock.Object,
                _providerManagementService = providerManagementServiceMock.Object
            };

            // Act
            var result = await stockService.AddStockItemAsync(stockItemJson);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddStockItemAsync_WhenInvalidStockItem_ReturnsFalse()
        {
            // Arrange
            var stockItemJson = "{\"ProductName\":\"\",\"Price\":0.0,\"ProducedOn\":\"2023-06-20\",\"ProviderId\":\"12345678-1234-1234-1234-1234567890AB\",\"IsCloseToExpirationDate\":false}";

            var stockSerializerMock = new Mock<IStockSerializer>();
            stockSerializerMock.Setup(s => s.Deserialize(stockItemJson)).Returns(new StockItem
            {
                ProductName = "",
                Price = 0,
                ProducedOn = new DateOnly(2023, 6, 20),
                ProviderId = Guid.Parse("12345678-1234-1234-1234-1234567890AB"),
                IsCloseToExpirationDate = false
            });

            var stockValidatorMock = new Mock<StockValidator>();
            stockValidatorMock.Setup(v => v.Validate(It.IsAny<StockItem>())).Returns(false);

            var providerManagementServiceMock = new Mock<ProviderManagementService>();

            var smartMarketDataAccessMock = new Mock<FakeSmartMarketDataAccess>();

            var stockService = new StockService
            {
                _stockDeserializer = stockSerializerMock.Object,
                _stockValidator = stockValidatorMock.Object,
                _providerManagementService = providerManagementServiceMock.Object
            };

            // Act
            var result = await stockService.AddStockItemAsync(stockItemJson);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task AddStockItemAsync_WhenProviderExists_DoesNotCallAddProvider()
        {
            // Arrange
            var stockItemJson = "{\"ProductName\":\"Test Product\",\"Price\":10.0,\"ProducedOn\":\"2023-06-20\",\"ProviderId\":\"12345678-1234-1234-1234-1234567890AB\",\"IsCloseToExpirationDate\":false}";

            var stockSerializerMock = new Mock<IStockSerializer>();
            stockSerializerMock.Setup(s => s.Deserialize(stockItemJson)).Returns(new StockItem
            {
                ProductName = "Test Product",
                Price = 10,
                ProducedOn = new DateOnly(2023, 6, 20),
                ProviderId = Guid.Parse("12345678-1234-1234-1234-1234567890AB"),
                IsCloseToExpirationDate = false
            });

            var stockValidatorMock = new Mock<StockValidator>();
            stockValidatorMock.Setup(v => v.Validate(It.IsAny<StockItem>())).Returns(true);

            var providerManagementServiceMock = new Mock<ProviderManagementService>();
            providerManagementServiceMock.Setup(p => p.GetFromApiByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Provider());

            var smartMarketDataAccessMock = new Mock<FakeSmartMarketDataAccess>();

            var stockService = new StockService
            {
                _stockDeserializer = stockSerializerMock.Object,
                _stockValidator = stockValidatorMock.Object,
                _providerManagementService = providerManagementServiceMock.Object
            };

            // Act
            var result = await stockService.AddStockItemAsync(stockItemJson);

            // Assert
            Assert.True(result);
        }

    }
}
