using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YourPlace.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Restaurant type")]
        public string RestaurantType { get; set; }

        [Required]
        public string Localisation { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Rate> Rating { get; set; }
        public List<RestaurantTypeTags> RestaurantTypeTags { get; set; }
    }
}