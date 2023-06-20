using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Core.Rules.SalesRules
{
    public class NormalDaysRule : SalesRuleBase
    {
        public override bool IsMatch(DateOnly today) => true;
    }
}
