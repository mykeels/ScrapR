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
        public string deptCode1 { get; set; }
        public string arrvCode1 { get; set; }
        public string deptCode2 { get; set; }
        public string arrvCode2 { get; set; }
        public string deptCode3 { get; set; }
        public string arrvCode3 { get; set; }
        public string deptCode4 { get; set; }
        public string arrvCode4 { get; set; }
        public string searchType { get; set; }
        public int deptYear { get; set; }
        public int deptMonth { get; set; }
        public int deptDay { get; set; }
        public int deptYear1 { get; set; }
        public int deptMonth1 { get; set; }
        public int deptDay1 { get; set; }
        public int deptYear2 { get; set; }
        public int deptMonth2 { get; set; }
        public int deptDay2 { get; set; }
        public int deptYear3 { get; set; }
        public int deptMonth3 { get; set; }
        public int deptDay3 { get; set; }
        public int deptYear4 { get; set; }
        public int deptMonth4 { get; set; }
        public int deptDay4 { get; set; }
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
            //multi trip from Lagos -> Abuja -> Enugu -> London (international)
            /*http://www.wakanow.com/en-ng/flights/searchprocess/searchprocess?adults=1
            &children=0&infants=0&trip=MS&deptCode=LOS&arrvCode=ABV&deptCode1=ABV&arrvCode1=ENU
            &deptCode2=ENU&arrvCode2=LHR&searchType=I&deptYear=2016&deptMonth=12&deptDay=04
            &retYear=2016&retMonth=12&retDay=04&deptYear1=2016&deptMonth1=12&deptDay1=04
            &deptYear2=2016&deptMonth2=12&deptDay2=06&deptYear3=2016&deptMonth3=12&deptDay3=08
            &cabin=E&currency=NGN&Market=ng&deptTime=&arrvTime=&airlinePref=&showFlexiDate=false*/

            //one-way trip from Lagos -> Abuja (local)
            /*http://www.wakanow.com/en-ng/flights/searchprocess/searchprocess?adults=1
            &children=0&infants=0&trip=OW&deptCode=LOS&arrvCode=ABV&searchType=I&deptYear=2016
            &deptMonth=12&deptDay=04&retYear=2016&retMonth=12&retDay=04&cabin=E&currency=NGN
            &Market=ng&deptTime=&arrvTime=&airlinePref=&showFlexiDate=false*/

            string searchUrl = "http://www.wakanow.com/en-ng/flights/searchprocess/searchprocess?adults=" + adults + "&children=" + children + "&infants=" + infants + 
                "&trip=" + trip + "&deptCode=" + deptCode + "&arrvCode=" + arrvCode + "&searchType=" + searchType + 
                "&deptYear=" + deptYear + "&deptMonth=" + deptMonth + "&deptDay=" + deptDay + "&cabin=" + cabin + "&currency=" + currency + "&Market=" + market + 
                "&deptTime=" + deptTime + "&arrvTime=" + arrvTime + "&airlinePref=" + airlinePref + "&showFlexiDate=" + showFlexiDate;
            if (retDay > 0 && retMonth > 0 && retYear > 0)
            {
                searchUrl += "&retYear=" + retYear + "&retMonth=" + retMonth + "&retDay=" + retDay;
            }
            if (!String.IsNullOrEmpty(deptCode1) && !String.IsNullOrEmpty(arrvCode1) && (deptDay1 > 0) && (deptMonth1 > 0) && (deptYear1 > 0))
            {
                searchUrl += "&deptCode1" + deptCode1 + "&arrvCode1" + arrvCode1 + "&deptYear1=" + deptYear1 + "&deptMonth1=" + deptMonth1 + "&deptDay1=" + deptDay1;
            }
            if (!String.IsNullOrEmpty(deptCode2) && !String.IsNullOrEmpty(arrvCode2) && (deptDay2 > 0) && (deptMonth2 > 0) && (deptYear2 > 0))
            {
                searchUrl += "&deptCode2" + deptCode2 + "&arrvCode2" + arrvCode2 + "&deptYear2=" + deptYear2 + "&deptMonth2=" + deptMonth2 + "&deptDay2=" + deptDay2;
            }
            if (!String.IsNullOrEmpty(deptCode3) && !String.IsNullOrEmpty(arrvCode3) && (deptDay3 > 0) && (deptMonth3 > 0) && (deptYear3 > 0))
            {
                searchUrl += "&deptCode3" + deptCode3 + "&arrvCode3" + arrvCode3 + "&deptYear3=" + deptYear3 + "&deptMonth3=" + deptMonth3 + "&deptDay3=" + deptDay3;
            }
            if (!String.IsNullOrEmpty(deptCode4) && !String.IsNullOrEmpty(arrvCode4) && (deptDay4 > 0) && (deptMonth4 > 0) && (deptYear4 > 0))
            {
                searchUrl += "&deptCode4" + deptCode4 + "&arrvCode4" + arrvCode4 + "&deptYear4=" + deptYear4 + "&deptMonth4=" + deptMonth4 + "&deptDay4=" + deptDay4;
            }
            return searchUrl;
        }

        public class Cabins
        {
            public const string Economy = "E";
            public const string PremiumEconomy = "W";
            public const string Business = "B";
            public const string FirstClass = "F";
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
            public const string Multi = "MS";
        }

        public static Query GetSampleQuery(string tripType = TripType.Multi)
        {
            var startDate = DateTime.Now.AddDays(3);
            var endDate = startDate.AddDays(4);
            Query query = new Query()
            {
                adults = 1,
                children = 0,
                infants = 0,
                trip = tripType,
                deptCode = "LOS",
                arrvCode = "ABV",
                searchType = "I",
                deptDay = startDate.Day,
                deptMonth = startDate.Month,
                deptYear = startDate.Year,
                cabin = Cabins.Economy,
                currency = Currencies.Naira,
                market = Markets.Nigeria,
                deptTime = "",
                arrvTime = "",
                airlinePref = "",
                showFlexiDate = "false"
            };
            if (tripType == TripType.Return)
            {
                query.retDay = endDate.Day;
                query.retMonth = endDate.Month;
                query.retYear = endDate.Year;
            }
            else if (tripType == TripType.Multi)
            {
                var d1 = DateTime.Today.AddDays(6);
                var d2 = DateTime.Today.AddDays(9);
                var d3 = DateTime.Today.AddDays(12);
                var d4 = DateTime.Today.AddDays(15);
                query.deptDay1 = d1.Day;
                query.deptDay2 = d2.Day;
                query.deptMonth1 = d1.Month;
                query.deptMonth2 = d2.Month;
                query.deptYear1 = d1.Year;
                query.deptYear2 = d2.Year;
                query.deptCode1 = query.arrvCode; //ABJ -> LHR
                query.arrvCode1 = "LHR";
                query.deptCode2 = query.arrvCode1; //LHR -> CDG
                query.arrvCode2 = "CDG";
            }
            return query;
        }
    }
}
