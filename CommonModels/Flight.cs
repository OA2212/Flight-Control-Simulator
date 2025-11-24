using System.Numerics;

namespace CommonModels
{
    public class Flight
    {
        public int Id { get; set; }
        public string? FlightNumber { get; set; }
        public int? PlaneId { get; set; }
        public virtual Plane? Plane { get; set; }
        public bool isLanding { get; set; }
    }
}