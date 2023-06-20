using SmartMarket.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Infrastructure.DateProvider
{
    public class ActualDateProvider : IDateProvider
    {
        public DateOnly getDate()
        {
            return DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
