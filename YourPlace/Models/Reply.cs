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
    }
}