using Newtonsoft.Json;
using SpeedyAirLy;

namespace SpeedyAirProject
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            var flights = LoadFlights();
            PrintFlightSchedule(flights);

            var orders = LoadOrders();
            ScheduleOrders(flights, orders);
            PrintOrdersSchedule(orders, flights);
        }

        public static List<Flight> LoadFlights()
        {
            return new List<Flight>
            {
                new Flight { FlightNumber = 1, Departure = "YUL", Arrival = "YYZ", Day = 1 },
                new Flight { FlightNumber = 2, Departure = "YUL", Arrival = "YYC", Day = 1 },
                new Flight { FlightNumber = 3, Departure = "YUL", Arrival = "YVR", Day = 1 },
                new Flight { FlightNumber = 4, Departure = "YUL", Arrival = "YYZ", Day = 2 },
                new Flight { FlightNumber = 5, Departure = "YUL", Arrival = "YYC", Day = 2 },
                new Flight { FlightNumber = 6, Departure = "YUL", Arrival = "YVR", Day = 2 },
            };
        }
        public static List<Order> LoadOrders()
        {
            string jsonOrders = File.ReadAllText("orders.json");
            var ordersDict = JsonConvert.DeserializeObject<Dictionary<string, Order>>(jsonOrders);
            return ordersDict?.Select(kv =>
            {
                kv.Value.OrderId = kv.Key;
                return kv.Value;
            }).ToList() ?? new List<Order>();
        }
        public static void ScheduleOrders(List<Flight> flights, List<Order> orders)
        {
            foreach (var order in orders)
            {
                var flight = flights.FirstOrDefault(f => f.Arrival == order.Destination && f.Capacity > 0);
                if (flight != null)
                {
                    order.FlightNumber = flight.FlightNumber;
                    flight.Capacity--;
                }
            }
        }

        public static void PrintFlightSchedule(List<Flight> flights)
        {
            foreach (var flight in flights)
            {
                Console.WriteLine($"Flight: {flight.FlightNumber}, departure: {flight.Departure}, arrival: {flight.Arrival}, day: {flight.Day}");
            }
        }

        public static void PrintOrdersSchedule(List<Order> orders, List<Flight> flights)
        {
            foreach (var order in orders)
            {
                if (order.FlightNumber.HasValue)
                {
                    var flight = flights.First(f => f.FlightNumber == order.FlightNumber.Value);
                    Console.WriteLine($"Order: {order.OrderId}, FlightNumber: {order.FlightNumber}, departure: {flight.Departure}, arrival: {order.Destination}, day: {flight.Day}");
                }
                else
                {
                    Console.WriteLine($"Order: {order.OrderId}, FlightNumber: not scheduled");
                }
            }
        }
    }
}