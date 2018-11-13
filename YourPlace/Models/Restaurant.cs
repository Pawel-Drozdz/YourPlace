using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourPlace.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RestaurantType { get; set; }
        public string Localisation { get; set; }
    }
}