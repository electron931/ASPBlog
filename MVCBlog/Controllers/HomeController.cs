using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Handlers;
using MVCBlog.Models;

namespace MVCBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly PostHandler _post;
        private readonly int postsForMainPage = 9;
         
        public HomeController()
        {
            _post = new PostHandler();
        }

        public ViewResult Index(int page = 1)
        {
            var postModel = new PostViewModel(_post, page, postsForMainPage);
            ViewBag.Page = page;
    
            return View(postModel);
        }

    }
}
