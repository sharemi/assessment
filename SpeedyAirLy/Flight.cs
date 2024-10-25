using System;
namespace SpeedyAirLy
{
	public class Flight
	{
        public int FlightNumber { get; set; }
        public string Departure { get; set; } = string.Empty;
        public string Arrival { get; set; } = string.Empty;
        public int Day { get; set; }
        public int Capacity { get; set; } = 20;
    }
}

