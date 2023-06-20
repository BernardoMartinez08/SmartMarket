using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Core.Rules
{
    public abstract class RuleBase
    {
        public abstract bool IsMatch(DateOnly today);

        public virtual decimal Apply(StockItem product,int quantity, decimal total, DateOnly today)
        {
            if (product.MembershipDeal is not null)
            {
                var numberOfDeals = quantity / product.MembershipDeal.Quantity;
                var remainder = quantity % product.MembershipDeal.Quantity;
                total = numberOfDeals * product.MembershipDeal.Price + remainder * product.Price;
            }

            return total;
        }
    }
}
