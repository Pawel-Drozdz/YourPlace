﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourPlace.Models;

namespace YourPlace.ViewModels
{
    public class CommentViewModel
    {
        public List<Comment> Comments { get; set; }
        public Comment NewComment { get; set; }
    }
}