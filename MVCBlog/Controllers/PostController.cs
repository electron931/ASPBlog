using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Handlers;
using Domain.Entities;
using MVCBlog.Models;

namespace MVCBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly PostHandler _post;
        private readonly int pageLimit = 9;
         
        public PostController()
        {
            _post = new PostHandler();
            
        }

        public ViewResult Index(int page = 1)
        {
            var postModel = new PostViewModel(_post, page, pageLimit);
            ViewBag.Page = page;
    
            return View(postModel);
        }

        public ViewResult GetPost(string categorySlug, string postSlug)
        {
            Post post = _post.Post(categorySlug, postSlug);

            if (post == null)
                throw new HttpException(404, "Post not found");

            if (post.Published == false)
                throw new HttpException(401, "The post is not published");

            ViewBag.Title = post.Title;

            return View(post);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public RedirectResult addComment(string commentBody, int postId)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                UserHandler userHandler = new UserHandler();
                User user = userHandler.User(HttpContext.User.Identity.Name);

                Post post = _post.Post(postId);

                Comment newComment = new Comment { Text = commentBody, Created = DateTime.Now, Post = post, User = user };

                CommentHandler commentHandler = new CommentHandler();
                commentHandler.Add(newComment);

            }

            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}
