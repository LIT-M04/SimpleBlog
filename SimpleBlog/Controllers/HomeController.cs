using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleBlog.Data;
using SimpleBlog.Models;

namespace SimpleBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? page)
        {
            var manager = new SimpleBlogManager(Properties.Settings.Default.ConStr);
            var viewModel = new IndexViewModel();
            #region go away
            //IEnumerable<Post> posts = manager.GetPosts();
            //List<PostWithCommentCount> foo = new List<PostWithCommentCount>();
            //foreach (Post post in posts)
            //{
            //    PostWithCommentCount count = new PostWithCommentCount();
            //    count.Post = post;
            //    count.CommentCount = manager.GetCommentCountForPost(post.Id);
            //    foo.Add(count);
            //}

            //viewModel.Posts = foo;
            #endregion
            if (page == null)
            {
                page = 1;
            }
            viewModel.Posts = manager.GetPosts(page.Value).Select(p => new PostWithCommentCount
            {
                Post = p,
                CommentCount = manager.GetCommentCountForPost(p.Id)
            });
            viewModel.CurrentPage = page.Value;
            viewModel.SetShowOlder(manager.GetPostCount());
            return View(viewModel);
        }

        public ActionResult Post(int postId)
        {
            var manager = new SimpleBlogManager(Properties.Settings.Default.ConStr);
            var viewModel = new PostViewModel();
            viewModel.Post = manager.GetPostById(postId);
            viewModel.Comments = manager.GetCommentsForPost(postId);
            return View(viewModel);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(string subject, string content)
        {
            var manager = new SimpleBlogManager(Properties.Settings.Default.ConStr);
            manager.AddPost(subject, content);
            return Redirect("/home/index");
        }

        public ActionResult AddComment(int postId, string name, string content)
        {
            var manager = new SimpleBlogManager(Properties.Settings.Default.ConStr);
            manager.AddComment(postId, name, content);
            return Redirect("/home/post?postId=" + postId);
        }



    }
}
