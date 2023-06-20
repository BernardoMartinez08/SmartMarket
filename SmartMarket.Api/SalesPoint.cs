using SmartMarket.Core.RulesEngines;
using SmartMarket.Core;
using SmartMarket.Core.Interfaces;
using System.Net;

namespace SmartMarket.App;

public class SalesPoint
{
    private readonly Dictionary<string, int> _productsInCart;
    private readonly IEnumerable<StockItem> _stock;
    private readonly IDateProvider _dateProvider;

    public SalesPoint(IEnumerable<StockItem> stock, IDateProvider dateProvider)
    {
        _stock = stock;
        _productsInCart = new Dictionary<string, int>();
        _dateProvider = dateProvider;
    }
    
    public void ScanItem(string productName)
    {
        var stockItem = _stock.FirstOrDefault(x => x.ProductName == productName);
        if (stockItem is null)
        {
            throw new ArgumentException($"Product {productName} not found in stock");
        }

        if (_productsInCart.TryGetValue(productName, out var quantity))
        {
            _productsInCart[productName] = quantity + 1;
        }
        else
        {
            _productsInCart.Add(productName, 1);
        }
    }

    public Dictionary<string, decimal> GetTotals()
    {
        var totals = new Dictionary<string, decimal>();
        foreach (var (product, quantity) in _productsInCart)
        {
            var stockItem = _stock.First(x => x.ProductName == product);
            var total = stockItem.Price * quantity;
            var today = _dateProvider.getDate();
            total = GetTotal(stockItem, quantity, total, today);
            totals.Add(product, total);
        }

        return totals;
    }

    private static decimal GetTotal(StockItem product, int quantity, decimal total, DateOnly today)
    {
        var rulesEngine = new SaleTotalRuleEngine.Builder()
                .WithWeekendsRule()
                .WithWeekDaysRule()
                .Build();

        return rulesEngine.ApplyRules(product, quantity, total, today);
    }
}