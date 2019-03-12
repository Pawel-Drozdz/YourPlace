using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourPlace.Models
{
    public class RestaurantTypeTags
    {
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public int TypeTagId { get; set; }
        public TypeTag TypeTag { get; set; }
    }
}