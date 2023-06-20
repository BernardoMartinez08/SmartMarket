using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Core.Rules.StockRules
{
    public interface IStockRuleBase
    {
        public bool IsCloseToExpirationDate(StockItem stockItem, int currentAge);
    }
}
