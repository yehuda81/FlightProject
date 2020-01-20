using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class Flight : IPoco
    {
        public long Id { get; set; }
        public long AirlineCompanyId { get; set; }
        public long OriginCountryCode { get; set; }
        public long DestinationCountryCode { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public int RemainingTickets { get; set; }

        public Flight()
        {
        }
        
        public Flight(long airlineCompanyId, long originCountryCode, long destinationCountryCode, DateTime departureTime, DateTime landingTime, int remainingTickets)
        {            
            AirlineCompanyId = airlineCompanyId;
            OriginCountryCode = originCountryCode;
            DestinationCountryCode = destinationCountryCode;
            DepartureTime = departureTime;
            LandingTime = landingTime;
            RemainingTickets = remainingTickets;
        }
        public static bool operator ==(Flight a, Flight b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }
            return a.Id == b.Id;
        }
        public static bool operator !=(Flight a, Flight b)
        {
            return !(a == b);
        }
        public override string ToString()
        {
            return $"Id: {Id} AirlineCompanyId: {AirlineCompanyId} OriginCountryCode: {OriginCountryCode}" +
                $" DestinationCountryCode: {DestinationCountryCode} DepartureTime: {DepartureTime}" +
                $" LandingTime: {LandingTime} RemainingTickets: {RemainingTickets}";
        }
        public override bool Equals(object obj)
        {
            Flight other = obj as Flight;
            return this == other;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }        
    }
}
