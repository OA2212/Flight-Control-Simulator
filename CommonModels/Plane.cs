using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonModels
{
    public class Plane
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? AirportLocationId { get; set; }
        public virtual AirportLocation? AirportLocation { get; set; }
        public int? FlightId { get; set; }
        public virtual Flight? Flight { get; set; }
    }
}
