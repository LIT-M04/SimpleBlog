using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBlog.Data;

namespace SimpleBlog.Models
{
    public class IndexViewModel
    {
        public IEnumerable<PostWithCommentCount> Posts { get; set; }
        public int CurrentPage { get; set; }
        public bool ShowOlder { get; private set; }

        public void SetShowOlder(int total)
        {
            ShowOlder = CurrentPage * 3 < total;
        }
    }

    public class PostWithCommentCount
    {
        public Post Post { get; set; }
        public int CommentCount { get; set; }
    }
}