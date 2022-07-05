using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTask.Models;

namespace TimeTask.Test
{
    public class getResultsDisplayDataTest
    {
        List<(Time, Time)> OneTimePeriodResult = new List<(Time, Time)>
        {
            (new Time{ hour = 8 , minute = 16}, new Time{hour = 9, minute = 20}),
            (new Time{ hour = 8 , minute = 14}, new Time{hour = 9, minute = 22}),
            (new Time{ hour = 8 , minute = 18}, new Time{hour = 9, minute = 24}),

        };
        List<(Time, Time)> TwoTimePeriodResults = new List<(Time, Time)>
        {
            (new Time{ hour = 8 , minute = 16}, new Time{hour = 9, minute = 20}),
            (new Time{ hour = 12 , minute = 16}, new Time{hour = 14, minute = 20}),
        };

        List<(Time, Time)> PeriodBetweenHours = new List<(Time, Time)>
        {
            (new Time{ hour = 8 , minute = 59}, new Time{hour = 9, minute = 0}),
        };

        List<(Time, Time)> InvalidPeriod = new List<(Time, Time)>
        {
            (new Time{ hour = 20 , minute = 10}, new Time{hour = 10, minute = 10}),
        };

        List<(Time, Time)> InvalidTime = new List<(Time, Time)>
        {
            (new Time{ hour = 28 , minute = -10}, new Time{hour = 10, minute = 30}),
        };




        [SetUp]
        public void Setup()
        {

        }

        [Test]

    }
}
