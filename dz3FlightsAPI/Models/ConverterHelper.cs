using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dz3FlightsAPI.DB;
using dz3FlightsAPI.Models;

namespace dz3FlightsAPI.Helpers
{
    public static class ConverterHelper
    {
        public static EditFlightModel ToEditFlightModel(this Flight flight)
        {
            return new EditFlightModel()
            {
                Id = flight.Id,
                Name= flight.Name,
                Cost = flight.Cost
            };
        }
    }
}
