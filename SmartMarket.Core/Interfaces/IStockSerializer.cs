
namespace SmartMarket.Core.Interfaces
{
    public interface IStockSerializer
    {
        StockItem? Deserialize(string source);
    }
}
