using System;
using System.Collections.Generic;

namespace YourPlace.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime DateTime { get; set; }
        public List<Reply> Replies { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}