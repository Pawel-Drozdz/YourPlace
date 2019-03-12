using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YourPlace.Models
{
    public class TypeTag
    {
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        public List<RestaurantTypeTags> RestaurantTypeTags { get; set; }
    }
}