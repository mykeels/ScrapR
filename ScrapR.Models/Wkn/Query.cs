using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models.Wkn
{
    public class Query
    {
        public int adults { get; set; }
        public int children { get; set; }
        public int infants { get; set; }
        public string trip { get; set; }
        public string deptCode { get; set; }
        public string arrvCode { get; set; }
        public string searchType { get; set; }
        public int deptYear { get; set; }
        public int deptMonth { get; set; }
        public int deptDay { get; set; }
        public int retYear { get; set; }
        public int retMonth { get; set; }
        public int retDay { get; set; }
        public string cabin { get; set; }
        public string currency { get; set; }
        public string market { get; set; }
        public string deptTime { get; set; }
        public string arrvTime { get; set; }
        public string airlinePref { get; set; }
        public string showFlexiDate { get; set; }

        public new string ToString()
        {
            return "http://www.wakanow.com/en-ng/flights/searchprocess/searchprocess?adults=" + adults + "&children=" + children + "&infants=" + infants + "&trip=" + trip +
                "&deptCode=" + deptCode + "&arrvCode=" + arrvCode + "&searchType=" + searchType + "&deptYear=" + deptYear + "&deptMonth=" + deptMonth + "&deptDay=" + deptDay + 
                "&retYear=" + retYear + "&retMonth=" + retMonth + "&retDay=" + retDay + "&cabin=" + cabin +
                "&currency=" + currency + "&Market=" + market + "&deptTime=" + deptTime + "&arrvTime=" + arrvTime + "&airlinePref=" + airlinePref + "&showFlexiDate=" + showFlexiDate;
        }

        public class Cabins
        {
            public const string Economy = "E";
        }

        public class Currencies 
        {
            public const string Naira = "NGN";
            public const string Dollar = "USD";
            public const string Euro = "EUR";
            public const string Pound = "GBP";
            public const string Cedis = "GHS";
        }

        public class Markets
        {
            public const string Nigeria = "ng";
            public const string Uk = "uk";
            public const string Ghana = "gh";
            public const string Usa = "us";
        }

        public class TripType
        {
            public const string OneWay = "OW";
            public const string Return = "RT";
        }

        public static Query GetSampleQuery()
        {
            var startDate = DateTime.Now.AddDays(3);
            var endDate = startDate.AddDays(4);
            Query query = new Query()
            {
                adults = 1,
                children = 0,
                infants = 0,
                trip = TripType.Return,
                deptCode = "LOS",
                arrvCode = "ABV",
                searchType = "I",
                deptDay = startDate.Day,
                deptMonth = startDate.Month,
                deptYear = startDate.Year,
                retDay = endDate.Day,
                retMonth = endDate.Month,
                retYear = endDate.Year,
                cabin = Cabins.Economy,
                currency = Currencies.Naira,
                market = Markets.Nigeria,
                deptTime = "",
                arrvTime = "",
                airlinePref = "",
                showFlexiDate = "false"
            };
            return query;
        }
    }
}
