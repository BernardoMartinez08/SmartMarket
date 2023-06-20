using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Core.Rules
{
    public class NormalDaysRule : RuleBase
    {
        public override bool IsMatch(DateOnly today) => true;
    }
}
