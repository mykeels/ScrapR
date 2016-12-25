using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models.TrvPaddy.Local
{
    public class Passenger
    {
        public string age_grade { get; set; }
        public Fare departure_trip_data { get; set; }
        public Fare return_trip_data { get; set; }
        public decimal fare { get; set; }
        public decimal tax { get; set; }
        public class Fare
        {
            public string fare { get; set; }
            public string tax { get; set; }
        }
    }
}
