using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dz3FlightsAPI.DB;

namespace dz3FlightsAPI.DB
{
    public class Route
    {
        public int Id { get; set; }
        public virtual List<Airport>  Airport_dep { get; set; }
        public virtual List<Airport> Airport_arr { get; set; }
        public int distance { get; set; }

    }
}
