using SmartMarket.Core.Rules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Core.RulesEngines
{
    public class SaleTotalRuleEngine
    {
        private readonly IEnumerable<RuleBase> _rules = new List<RuleBase>();

        private SaleTotalRuleEngine(IEnumerable<RuleBase> rules)
        {
            _rules = rules;
        }

        public decimal ApplyRules(StockItem product, int quantity, decimal total, DateOnly today)
        {
            decimal _total = 0;
            foreach (var rule in _rules)
            {
                if (rule.IsMatch(today))
                {
                    _total = rule.Apply(product, quantity, total, today);
                    break;
                }
            }
            return _total;
        }

        public class Builder
        {
            private readonly List<RuleBase> _rules = new List<RuleBase>();

            public Builder WithWeekDaysRule()
            {
                _rules.Add(new WeekDaysRule());
                return this;
            }

            public Builder WithWeekendsRule()
            {
                _rules.Add(new WeekEndsRule());
                return this;
            }

            public SaleTotalRuleEngine Build()
            {
                _rules.Add(new NormalDaysRule());
                return new SaleTotalRuleEngine(_rules);
            }
        }
    }
}
