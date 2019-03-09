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

        //Refactor!!!
        public Time TimeSinceAdding(DateTime createDate)
        {
            var now = DateTime.Now;
            TimeSpan interval = now - createDate;

            if (interval.TotalSeconds < 1)
            {
                return new Time { Number = 1, TimeUnit = "sec." };
            }
            else if (interval.TotalSeconds < 60)
            {
                return new Time { Number = Math.Round(interval.TotalSeconds), TimeUnit = "sec."};
            }
            else if (interval.TotalMinutes < 60)
            {
                return new Time { Number = Math.Round(interval.TotalMinutes), TimeUnit = "min." };
            }
            else if (interval.TotalHours < 24)
            {
                return new Time { Number = Math.Round(interval.TotalHours), TimeUnit = "h." };
            }
            else
            {
                return new Time { Number = Math.Round(interval.TotalDays), TimeUnit = "d." };
            }
        }

        //Refactor!!!
        public class Time
        {
            public double Number { get; set; }
            public string TimeUnit { get; set; }
        }
    }
}