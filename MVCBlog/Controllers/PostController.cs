using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBlog.Models;
using BusinessLogic.Handlers;


namespace MVCBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly PostHandler _post;
 
        public PostController()
        {
            _post = new PostHandler();
        }

        public ViewResult Index(int page = 1)
        {
            var postModel = new PostViewModel(_post, page, 12);
            
            ViewBag.Title = "Latest Posts";
            return View(postModel);
        }

    }
}
