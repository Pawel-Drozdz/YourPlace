using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourPlace.Models
{
    public class Adress
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string NameOfTheStreet { get; set; }
        public int BuildingNumber { get; set; }
    }
}