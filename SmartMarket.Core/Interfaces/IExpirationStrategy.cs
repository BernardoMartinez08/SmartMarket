﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Core.Interfaces
{
    public interface IExpirationStrategy
    {
        bool IsCloseToExpiration(StockItem stockItem);
    }

}
