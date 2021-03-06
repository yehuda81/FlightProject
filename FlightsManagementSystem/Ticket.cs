﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class Ticket : IPoco
    {
        public long Id { get; set; }
        public long FlightId { get; set; }
        public long CustomerId { get; set; }

        public Ticket()
        {
        }

        public Ticket(long flightId, long customerId)
        {            
            FlightId = flightId;
            CustomerId = customerId;
        }
        public static bool operator ==(Ticket a, Ticket b)
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
        public static bool operator !=(Ticket a, Ticket b)
        {
            return !(a == b);
        }
        public override string ToString()
        {
            return $"Id: {Id} FlightId: {FlightId} CustomerId: {CustomerId}";
        }

        public override bool Equals(object obj)
        {
            Ticket other = obj as Ticket;
            return this == other;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}