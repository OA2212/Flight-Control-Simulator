namespace CommonModels
{
    public class NewFlightAndPlaneRequest
    {
        public virtual Flight Flight { get; set; }
        public virtual Plane Plane { get; set; }
    }
}
