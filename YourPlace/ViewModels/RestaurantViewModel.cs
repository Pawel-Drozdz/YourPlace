using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YourPlace.Models;

namespace YourPlace.ViewModels
{
    public class RestaurantViewModel
    {
        public Restaurant Restaurant { get; set; }

        public List<Comment> Comments { get; set; }
        public Comment NewComment { get; set; }
        public Reply NewParentReply { get; set; }

        public float Rating { get; set; }
        public float OldRate { get; set; }
        [Display(Name = "Rate the restaurant")]
        public int NewRate { get; set; }
        public List<byte> RatesForDropDownList { get; set; } = new List<byte>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    }
}