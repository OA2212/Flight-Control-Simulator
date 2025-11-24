using CommonModels;

namespace FlightControlAPI.Services
{
    public interface IManagmentATC
    {
        Request? LandingManagment(Request req);
        Request? DepatureManagment(Request req);
        Task<IEnumerable<Plane>> GetAllPlanes();
        NewFlightAndPlaneRequest CreateNewPlaneAndFlight(NewFlightAndPlaneRequest ng);
    }
}
