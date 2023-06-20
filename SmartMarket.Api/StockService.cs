
using SmartMarket.Core;
using SmartMarket.Core.DataBaseService;
using SmartMarket.Core.Interfaces;
using SmartMarket.Core.StockManager;
using SmartMarket.Infrastructure;

namespace SmartMarket.App
{
    public class StockService
    {
        public IStockSerializer _stockDeserializer = new StockSerializer();
        public StockValidator _stockValidator = new StockValidator();
        public ProviderManagementService _providerManagementService = new ProviderManagementService();

        public async Task<bool> AddStockItemAsync(string stockItem)
        {
            var stockItemObject = _stockDeserializer.Deserialize(stockItem);
            if (!_stockValidator.Validate(stockItemObject))
            {
                return false;
            }

            var provider = await _providerManagementService.GetFromApiByIdAsync(stockItemObject.ProviderId);
            if (provider is null)
            {
                _providerManagementService.AddProvider(stockItemObject.ProviderId, stockItemObject.ProviderName);
            }

            SmartMarketDataAccess.AddStockItem(stockItemObject);
            return true;
        }
    }
}
