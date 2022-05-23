﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz3FlightsAPI.DB
{
    public enum City_name
    {
        Ekaterinburg = 0,
        Moscow = 1,
        SaintPetersburg = 2
    }
    public class Airport
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string ShortName { get; set; }

    }
}
