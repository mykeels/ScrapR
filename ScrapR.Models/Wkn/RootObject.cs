using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models.Wkn
{
    public class Roots:List<Root> {}

    public class Root
    {
        public Request sd { get; set; }
        public List<Airline> ual { get; set; }
        public List<Airline> al { get; set; }
        public List<Airport> ap { get; set; }
        public List<Mt> mt { get; set; }
        public List<Fare> fares { get; set; }
    }

    public class Request
    {
        public string currency { get; set; }
        public string deptCode { get; set; }
        public string arrvCode { get; set; }
        public string trip { get; set; }
        public string searchType { get; set; }
        public string adults { get; set; }
        public string children { get; set; }
        public string infants { get; set; }
        public string deptDate { get; set; }
        public string arrvDate { get; set; }
        public string searchId { get; set; }
        public string deptTime { get; set; }
        public string arrvTime { get; set; }
        public string airlinePref { get; set; }
        public string deptName { get; set; }
        public string arrvName { get; set; }
        public string cabin { get; set; }
        public string deptCode1 { get; set; }
        public string arrvCode1 { get; set; }
        public string deptDate1 { get; set; }
        public string deptCode2 { get; set; }
        public string arrvCode2 { get; set; }
        public string deptDate2 { get; set; }
        public string deptCode3 { get; set; }
        public string arrvCode3 { get; set; }
        public string deptDate3 { get; set; }
        public string deptCode4 { get; set; }
        public string arrvCode4 { get; set; }
        public string deptDate4 { get; set; }
        public string deptCode5 { get; set; }
        public string arrvCode5 { get; set; }
        public string deptDate5 { get; set; }
        public string deptName1 { get; set; }
        public string arrvName1 { get; set; }
        public string deptName2 { get; set; }
        public string arrvName2 { get; set; }
        public string deptName3 { get; set; }
        public string arrvName3 { get; set; }
        public string deptName4 { get; set; }
        public string arrvName4 { get; set; }
        public string deptName5 { get; set; }
        public string arrvName5 { get; set; }
        public string NoSectors { get; set; }
        public string TravelType { get; set; }
    }

    public class Airline
    {
        public string c { get; set; }
        public string n { get; set; }
        public string d { get; set; }
        public string cnt { get; set; }
        public string amt { get; set; }
    }

    public class Airport
    {
        public string c { get; set; }
        public string n { get; set; }
        public string s { get; set; }
        public string ct { get; set; }
        public string cc { get; set; }
        public string cn { get; set; }
    }

    public class Mt
    {
        public string fid { get; set; }
        public string pa { get; set; }
        public string St { get; set; }
        public string Cst { get; set; }
        public int amt { get; set; }
        public int sp { get; set; }
    }

    public class Fare
    {
        public string id { get; set; }
        public string a { get; set; }
        public string t { get; set; }
        public string c { get; set; }
        public string f { get; set; }
        public List<Ob> ob { get; set; }
        public List<object> ib { get; set; }
        public string odu { get; set; }
        public string idu { get; set; }
        public B b { get; set; }
        public string tf { get; set; }
        public string fb { get; set; }
        public string s { get; set; }
        public string d { get; set; }
        public string pa { get; set; }
        public string pd { get; set; }
        public string advs { get; set; }
        public string fixp { get; set; }
        public string isp { get; set; }
        public string arc { get; set; }
        public string multi { get; set; }
        public string multiarc { get; set; }
        public string pt { get; set; }
        public string sm { get; set; }
        public string mk { get; set; }
        public string fst { get; set; }
        public string bestfr { get; set; }
        public string rec { get; set; }
        public List<Sd2> sd { get; set; }

        public class B
        {
            public string ba { get; set; }
            public string bc { get; set; }
            public string bi { get; set; }
            public string ta { get; set; }
            public string tc { get; set; }
            public string ti { get; set; }
            public string dta { get; set; }
            public string dtc { get; set; }
            public string dti { get; set; }
        }

        public class Ob
        {
            public string fc { get; set; }
            public string fcn { get; set; }
            public string fn { get; set; }
            public string o { get; set; }
            public string d { get; set; }
            public string dd { get; set; }
            public string dt { get; set; }
            public string ad { get; set; }
            public string at { get; set; }
            public string du { get; set; }
            public string s { get; set; }
            public string bc { get; set; }
            public string fb { get; set; }
            public string rf { get; set; }
            public string oter { get; set; }
            public string dter { get; set; }
            public string lo { get; set; }
            public string an { get; set; }
            public string lg { get; set; }
        }

        public class Sd2
        {
            public string fc { get; set; }
            public string fcn { get; set; }
            public string fn { get; set; }
            public string o { get; set; }
            public string d { get; set; }
            public string dd { get; set; }
            public string dt { get; set; }
            public string ad { get; set; }
            public string at { get; set; }
            public string du { get; set; }
            public string s { get; set; }
            public string bc { get; set; }
            public string fb { get; set; }
            public string rf { get; set; }
            public string oter { get; set; }
            public string dter { get; set; }
            public string lo { get; set; }
            public string an { get; set; }
            public List<Lg> lg { get; set; }

            public class Lg
            {
                public string fc { get; set; }
                public string fcn { get; set; }
                public string fn { get; set; }
                public string o { get; set; }
                public string d { get; set; }
                public string dd { get; set; }
                public string dt { get; set; }
                public string ad { get; set; }
                public string at { get; set; }
                public string du { get; set; }
                public string s { get; set; }
                public string bc { get; set; }
                public string fb { get; set; }
                public string rf { get; set; }
                public string oter { get; set; }
                public string dter { get; set; }
                public string lo { get; set; }
                public string an { get; set; }
                public string lg { get; set; }
            }
        }
    }

}
