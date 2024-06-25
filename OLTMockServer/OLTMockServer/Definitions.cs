using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer
{
    public class Definitions
    {
        public enum APINames
        {
            NewOrder,
        }

        public enum KnownOnlineShops
        {
            None,
            Snap,
            Digi,
        }

        public enum TestPlayStatuses
        {
            Stoped = 0,
            Playing = 1,
            Paused = 2,
        }

        public enum PropertyValueGeneratorTypes
        {
            FixedValue,
            IntegerIncremental,
            DecimalIncremental,
            DateIncrementalMinute,
            DateIncrementalHour,
            DateIncrementalDay,
            RandomDate,
            NowDate,
            TodayDate,
            RandomStringName,
            RandomStringFName,
            RandomStringLName,
            RandomIntegerValue,
            RandomStringCode,
            RandomStringCode8,
            RandomStringCode6,
            RandomFixedList,
            RandomDecimal,
        }

    }
}
