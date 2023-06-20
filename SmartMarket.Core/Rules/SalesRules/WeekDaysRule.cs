using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Core.Rules.SalesRules
{
    public class WeekDaysRule : SalesRuleBase
    {
        public override decimal Apply(StockItem product, int quantity, decimal total, DateOnly today)
        {
            if (product.MembershipDeal is not null)
            {
                var numberOfDeals = quantity / product.MembershipDeal.Quantity;
                var remainder = quantity % product.MembershipDeal.Quantity;
                total = numberOfDeals * product.MembershipDeal.Price + remainder * product.Price;
            }

            if (today.DayOfWeek is DayOfWeek.Monday or DayOfWeek.Tuesday)
            {
                total -= total * 0.05m;
            }
            return total;
        }

        public override bool IsMatch(DateOnly today) => today.DayOfWeek == DayOfWeek.Monday || today.DayOfWeek == DayOfWeek.Tuesday;
    }
}
