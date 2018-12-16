using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourPlace.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}