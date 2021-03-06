﻿using System.Collections.Generic;
using YourPlace.Models;

namespace YourPlace.ViewModels
{
    public class ChildReplyViewModel
    {
        public List<Reply> ChildReplies { get; set; }
        public Reply NewChildReply { get; set; }
        public int RestaurantId { get; set; }
        public int CommentId { get; set; }
    }
}