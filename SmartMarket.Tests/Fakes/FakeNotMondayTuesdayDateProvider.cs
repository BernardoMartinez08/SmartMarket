using SmartMarket.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Tests.Fakes
{
    public class FakeNotMondayTuesdayDateProvider : IDateProvider
    {
        public DateOnly getDate()
        {
            return new DateOnly(2023, 06, 21);
        }
    }
}
