using System;

namespace YourPlace.Models
{
    public class Rate
    {
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public float Rating { get; set; }
    }
}