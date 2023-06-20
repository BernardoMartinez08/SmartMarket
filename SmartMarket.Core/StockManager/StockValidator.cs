
using SmartMarket.Core.Rules.StockRules;

namespace SmartMarket.Core.StockManager
{
    public class StockValidator
    {
        private readonly List<IStockRuleBase> _rules;

        public StockValidator()
        {
            _rules = new List<IStockRuleBase>
            {
                new StockAgeRule(30, false),
                new StockAgeRule(15, false),
                new StockAgeRule(7, true)
            };
        }

        public bool Validate(StockItem stockItem)
        {
            if (string.IsNullOrEmpty(stockItem.ProductName) || stockItem.Price <= 0)
            {
                return false;
            }

            var now = DateOnly.FromDateTime(DateTime.Now);
            var currentAge = now.DayNumber - stockItem.ProducedOn.DayNumber;

            foreach (var rule in _rules)
            {
                var isCloseToExpiration = rule.IsCloseToExpirationDate(stockItem, currentAge);
                if (isCloseToExpiration)
                {
                    stockItem.IsCloseToExpirationDate = isCloseToExpiration;
                    break;
                }
            }

            return true;
        }
    }
}
