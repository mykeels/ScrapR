using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models.TrvFix
{
    public class Result
    {
        public ItineraryWSResponse airItineraryWSResponse { get; set; }
        public string airline { get; set; }
        public string airlineCode { get; set; }
        public dynamic cabin { get; set; }
        public dynamic cacheIndex { get; set; }
        public string gds { get; set; }
        public bool hotelCombo { get; set; }
        public InfoWSResponse pricingInfoWSResponse { get; set; }
        public string salesCategory { get; set; }
        public SearchRequest searchRequest { get; set; }
        public string ticketLocale { get; set; }
        public string ticketPolicy { get; set; }
 
        public class ItineraryWSResponse
        {
            public string directionIndicator { get; set; }
            public List<OriginDestination> originDestinationWSResponses { get; set; }
            public class OriginDestination
            {
                public string arrivalDateTime { get; set; }
                public dynamic cabin { get; set; }
                public string departureDateTime { get; set; }
                public string destinationAirport { get; set; }
                public string destinationAirportCode { get; set; }
                public List<FlightSegment> flightSegmentWSResponses { get; set; }
                public string duration { get; set; }
                public string marketingAirline { get; set; }
                public string marketingAirlineCode { get; set; }
                public bool multiAirline { get; set; }
                public int numberOfStops { get; set; }
                public dynamic operatingAirline { get; set; }
                public dynamic operatingAirlineCode { get; set; }
                public string originAirport { get; set; }
                public string originAirportCode { get; set; }
                public class FlightSegment
                {
                    public string airEquipType { get; set; }
                    public string airportCodeContext { get; set; }
                    public string arrivalAirport { get; set; }
                    public string arrivalAirportCode { get; set; }
                    public string arrivalDateTime { get; set; }
                    public dynamic arrivalTimeZone { get; set; }
                    public dynamic cabin { get; set; }
                    public string departureAirport { get; set; }
                    public string departureAirportCode { get; set; }
                    public string departureDateTime { get; set; }
                    public dynamic departureTimeZone { get; set; }
                    public string duration { get; set; }
                    public bool eticketEligible { get; set; }
                    public string flightNumber { get; set; }
                    public string marketingAirline { get; set; }
                    public string marketingAirlineCode { get; set; }
                    public string marriageGrp { get; set; }
                    public dynamic numberInParty { get; set; }
                    public dynamic operatingAirline { get; set; }
                    public dynamic operatingAirlineCode { get; set; }
                    public string resBookDesignCode { get; set; }
                    public dynamic rph { get; set; }
                    public int stopQuantity { get; set; }
                }
            }

        }
        public class InfoWSResponse
        {
            public decimal baseFare { get; set; }
            public decimal commissionedBaseFare { get; set; }
            public List<FareBreakdown> commissionedFareBreakDowns { get; set; }
            public decimal commissionedTotalFare { get; set; }
            public string currencyCode { get; set; }
            public dynamic decimalPlaces { get; set; }
            public List<FareBreakdown> fareBreakDowns { get; set; }
            public string pricingSource { get; set; }
            public decimal totalFare { get; set; }
            public decimal totalTax { get; set; }

            public class FareBreakdown
            {
                public PassengerFare passengerFare { get; set; }
                public PassengerType passengerType { get; set; }
                public class PassengerFare
                {
                    public decimal? baseFare { get; set; }
                    public decimal? totalFare { get; set; }
                    public decimal? totalTax { get; set; }
                }
                public class PassengerType
                {
                    public string code { get; set; }
                    public int quantity { get; set; }
                }
            }
        }

        public class SearchRequest
        {
            public string cabinPrefLevel { get; set; }
            public bool directFlight { get; set; }
            public bool flexibleDate { get; set; }
            public dynamic flightType { get; set; }
            public bool hotelCombo { get; set; }
            public List<OriginDestination> originDestinationRequests { get; set; }
            public List<PassengerType> passengerTypes { get; set; }
            public string preferredAirlineCode { get; set; }
            public string preferredCabin { get; set; }
            public string salesCategory { get; set; }
            public string ticketLocale { get; set; }
            public string ticketPolicy { get; set; }
            public string tripType { get; set; }

            public class OriginDestination
            {
                public string departureDateTime { get; set; }
                public string destination { get; set; }
                public string origin { get; set; }
                public int rph { get; set; }
            }

            public class PassengerType
            {
                public string code { get; set; }
                public int quantity { get; set; }
            }
        }

        public static Result GetSampleData()
        {
            Result ret = new Result()
            {
                airItineraryWSResponse = new ItineraryWSResponse()
                {
                    originDestinationWSResponses = (new ItineraryWSResponse.OriginDestination[]
                    {
                        new ItineraryWSResponse.OriginDestination()
                        {
                            flightSegmentWSResponses = (new ItineraryWSResponse.OriginDestination.FlightSegment[]
                            {
                                new ItineraryWSResponse.OriginDestination.FlightSegment()
                                {

                                }
                            }).ToList()
                        }
                    }).ToList()
                },
                pricingInfoWSResponse = new InfoWSResponse()
                {
                    fareBreakDowns = (new InfoWSResponse.FareBreakdown[] {
                        new InfoWSResponse.FareBreakdown()
                        {
                            passengerFare = new InfoWSResponse.FareBreakdown.PassengerFare(),
                            passengerType = new InfoWSResponse.FareBreakdown.PassengerType()
                        }
                    }).ToList(),
                    commissionedFareBreakDowns = (new InfoWSResponse.FareBreakdown[] {
                        new InfoWSResponse.FareBreakdown()
                        {
                            passengerFare = new InfoWSResponse.FareBreakdown.PassengerFare(),
                            passengerType = new InfoWSResponse.FareBreakdown.PassengerType()
                        }
                    }).ToList()
                },
                searchRequest = new SearchRequest()
                {
                    originDestinationRequests = (new SearchRequest.OriginDestination[]
                    {
                        new SearchRequest.OriginDestination()
                        {

                        }
                    }).ToList(),
                    passengerTypes = (new SearchRequest.PassengerType[]
                    {
                        new SearchRequest.PassengerType()
                        {

                        }
                    }).ToList()
                }
            };
            return ret;
        }
    }
}
