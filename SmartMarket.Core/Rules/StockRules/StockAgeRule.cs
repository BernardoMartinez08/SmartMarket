using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Core.Rules.StockRules
{
    public class StockAgeRule : IStockRuleBase
    {
        private int _days;
        private bool IsCloseToExpiration;

        public StockAgeRule(int days, bool isCloseToExpiration)
        {
            _days = days;
            IsCloseToExpiration = isCloseToExpiration;
        }


        public bool IsCloseToExpirationDate(StockItem stockItem, int currentAge)
        {
            IsCloseToExpiration = currentAge > _days && (IsCloseToExpiration || stockItem.MembershipDeal != null);
            return IsCloseToExpiration;
        }
    }
}
