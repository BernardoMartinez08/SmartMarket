using InsuranceCompanySystem.Core.Interfaces;

namespace SmartMarket.Core;

public class StockService
{
    private readonly IStockSerializer _stockSerializer;
    public StockService(IStockSerializer stockSerializer)
    {
        _stockSerializer = stockSerializer;
    }
    public async Task<bool> AddStockItemAsync(string stockItem)
    {
        var stockItemObject = _stockSerializer.Deserialize(stockItem);
        if (string.IsNullOrEmpty(stockItemObject.ProductName))
        {
            return false;
        }

        if (stockItemObject.Price <= 0)
        {
            return false;
        }

        var now = DateOnly.FromDateTime(DateTime.Now);
        var currentAge = now.DayNumber - stockItemObject.ProducedOn.DayNumber;
        switch (currentAge)
        {
            case > 30:
                return false;
            case > 15:
            case > 7 when stockItemObject.MembershipDeal is not null:
                stockItemObject.IsCloseToExpirationDate = true;
                break;
            default:
                stockItemObject.IsCloseToExpirationDate = false;
                break;
        }

        using (var providerManagementService = new ProviderManagementService())
        {
            var provider = await providerManagementService.GetFromApiByIdAsync(stockItemObject.ProviderId);
            if (provider is null)
            {
                SmartMarketDataAccess.AddProvider(stockItemObject.ProviderId, stockItemObject.ProviderName);
            }
        }
        SmartMarketDataAccess.AddStockItem(stockItemObject);
        return true;
    }
}