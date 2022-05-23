using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using dz3FlightsAPI.DB;

namespace dz3FlightsAPI.Models
{
    public class EditFlightModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Required field")]
        [DisplayName("Name of flight")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DisplayName("Cost, RUB")]
        public int Cost { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DisplayName("Time of departure")]
        public DateTime Time_Departure { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DisplayName("Time of start registration")]
        public DateTime Time_Registration { get; set; }
    }
}
