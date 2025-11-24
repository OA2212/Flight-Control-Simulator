using CommonModels;
using FlightControlAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightControlAPI.Services
{
    public class ATC_Mangement : IManagmentATC
    {
        private readonly DataContext _dataContext;
        Random rnd = new Random();
        public ATC_Mangement(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Request? DepatureManagment(Request req)
        {
            if (req == null)
            {
                throw new ArgumentNullException();
            }
            var flightIndex = _dataContext.Flights.Where(x => x.Id == req.FlightId).FirstOrDefault();
            var lane8 = _dataContext.Locations.Where(x => x.LocationNumber == 8).FirstOrDefault();
            if (!req.isLanding && (flightIndex!.Plane!.AirportLocation!.LocationNumber == 6 || flightIndex.Plane.AirportLocation.LocationNumber == 7))
            {
                if (lane8?.Planes?.Count == 0)
                {
                    _dataContext.Locations.Where(x => x.LocationNumber == flightIndex!.Plane!.AirportLocation!.LocationNumber).FirstOrDefault()!.Planes?.Remove(flightIndex.Plane);
                    req.Flight = flightIndex;
                    lane8.Planes.Add(req.Flight.Plane);
                    _dataContext.SaveChanges();
                    return req;
                }
            }

            var lane4 = _dataContext.Locations.Where(x => x.LocationNumber == 4).FirstOrDefault();
            if (flightIndex?.Plane?.AirportLocation?.LocationNumber == 8)
            {
                if (lane4?.Planes?.Count == 0)
                {
                    _dataContext.Locations.Where(x => x.LocationNumber == flightIndex!.Plane!.AirportLocation!.LocationNumber).FirstOrDefault()!.Planes?.Remove(flightIndex.Plane);
                    req.Flight = flightIndex;
                    lane4.Planes.Add(req.Flight.Plane);
                    _dataContext.SaveChanges();
                    return req;
                }
            }

            var lane9 = _dataContext.Locations.Where(x => x.LocationNumber == 9).FirstOrDefault();
            if (flightIndex?.Plane?.AirportLocation?.LocationNumber == 4)
            {
                if (lane9?.Planes?.Count == 0)
                {
                    _dataContext.Locations.Where(x => x.LocationNumber == flightIndex!.Plane!.AirportLocation!.LocationNumber).FirstOrDefault()!.Planes?.Remove(flightIndex.Plane);
                    req.Flight = flightIndex;
                    lane9.Planes.Add(req.Flight.Plane);
                    _dataContext.SaveChanges();
                    return req;
                }
            }

            if(flightIndex?.Plane?.AirportLocation?.LocationNumber == 9)
            {
                lane9?.Planes?.Clear();
                _dataContext.Planes.Remove(flightIndex.Plane);
                _dataContext.SaveChanges();
            }

            return null;

        }

        public Request? LandingManagment(Request req)
        {
            if (req == null)
            {
                throw new ArgumentNullException();
            }

            if (req.isLanding)
            {
                var flightIndex = _dataContext.Flights.Where(x => x.Id == req.FlightId).FirstOrDefault();
                var location = _dataContext.Locations.Where(x => x.LocationNumber == flightIndex!.Plane!.AirportLocation!.LocationNumber + 1).FirstOrDefault();
               
                if ((location?.LocationNumber < 5 && location?.Planes?.Count == 0) || (location?.LocationNumber == 5 && location?.Planes?.Count <= 8))
                {
                    _dataContext.Locations.Where(x => x.LocationNumber == flightIndex!.Plane!.AirportLocation!.LocationNumber).FirstOrDefault()!.Planes?.Remove(flightIndex!.Plane!);
                    req.Flight = flightIndex;
                    location?.Planes?.Add(flightIndex!.Plane!);
                    _dataContext.SaveChanges();
                    return req;
                }

                var lane6 = _dataContext.Locations.Where(x => x.LocationNumber == 6).FirstOrDefault();
                if (flightIndex?.Plane?.AirportLocation?.LocationNumber==5 && lane6?.Planes?.Count == 0)
                {
                    _dataContext.Locations.Where(x => x.LocationNumber == flightIndex!.Plane!.AirportLocation!.LocationNumber).FirstOrDefault()!.Planes?.Remove(flightIndex.Plane);
                    lane6?.Planes?.Add(flightIndex!.Plane!);
                    req.Flight = flightIndex;
                    _dataContext.SaveChanges();
                    var plane = flightIndex.Plane;
                    Thread.Sleep(3500);
                    var newFlight = new Flight() { FlightNumber = $"LY{rnd.Next(100, 1000)}", isLanding = false, Plane = plane, PlaneId = plane.Id };
                    plane.Flight = newFlight;
                    req.Flight = newFlight;
                    _dataContext.SaveChanges();
                    return req;
                }

                var lane7 = _dataContext.Locations.Where(x => x.LocationNumber == 7).FirstOrDefault();
                if (flightIndex?.Plane?.AirportLocation?.LocationNumber == 5 && lane7?.Planes?.Count == 0)
                {
                    _dataContext.Locations.Where(x => x.LocationNumber == flightIndex!.Plane!.AirportLocation!.LocationNumber).FirstOrDefault()!.Planes?.Remove(flightIndex.Plane);
                    lane7?.Planes?.Add(flightIndex!.Plane!);
                    req.Flight = flightIndex;
                    _dataContext.SaveChanges();
                    var plane = flightIndex.Plane;
                    Thread.Sleep(3500);
                    var newFlight = new Flight() { FlightNumber = $"LY{rnd.Next(100, 1000)}", isLanding = false , Plane= plane, PlaneId = plane.Id };
                    plane.Flight = newFlight;
                    req.Flight = newFlight;
                    _dataContext.SaveChanges();
                    return req;
                }
            }
            return null;
        }

        public async Task<IEnumerable<Plane>> GetAllPlanes()
        {
            var planes = await _dataContext.Planes.ToListAsync();
            return planes;
        }


        public NewFlightAndPlaneRequest CreateNewPlaneAndFlight(NewFlightAndPlaneRequest ng)
        {
            _dataContext.Planes.Add(ng.Plane);
            _dataContext.SaveChanges();
            ng.Flight.PlaneId = _dataContext.Planes.Where(x => x.Name == ng.Plane.Name).FirstOrDefault()!.Id;
            _dataContext.Flights.Add(ng.Flight);
            _dataContext.SaveChanges();
            _dataContext.Planes.Where(x => x.Name == ng.Plane.Name).FirstOrDefault()!.FlightId = _dataContext.Flights.Where(x => x.FlightNumber == ng.Flight.FlightNumber).FirstOrDefault()!.Id;
            _dataContext.SaveChanges();

            ng.Plane = _dataContext.Planes.Where(x => x.Name == ng.Plane.Name).FirstOrDefault()!;
            ng.Flight = _dataContext.Flights.Where(x => x.FlightNumber == ng.Flight.FlightNumber).FirstOrDefault()!;

            return ng;

        }
    }
}
