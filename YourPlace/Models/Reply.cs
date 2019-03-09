using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourPlace.Models
{
    public class Reply
    {
        public int Id { get; set; }
        public int ParentReplyId { get; set; }
        public string Body { get; set; }
        public string AuthorName { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Reply> ChildReplies { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }

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
                return new Time { Number = Math.Round(interval.TotalSeconds), TimeUnit = "sec." };
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