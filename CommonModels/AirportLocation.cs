using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonModels
{
    public class AirportLocation
    {
        public int Id { get; set; }
        public int LocationNumber { get; set; }
        public virtual ICollection<Plane>? Planes { get; set; }
    }
}
