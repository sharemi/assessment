using System;
namespace SpeedyAirLy
{
	public class Order
	{
        public string OrderId { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public int? FlightNumber { get; set; }
    }
}

