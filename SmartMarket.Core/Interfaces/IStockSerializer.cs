using SmartMarket.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCompanySystem.Core.Interfaces
{
    public interface IStockSerializer
    {
        StockItem? Deserialize(string source);
    }
}
