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
    public class InfoController : Controller
    {
        private readonly PostHandler _post;
        private readonly int pageLimit = 9;

        public InfoController()
        {
            _post = new PostHandler();
        }

        public ViewResult Contacts()
        {
            return View();
        }

        public ViewResult About()
        {
            return View();
        }


        public ViewResult Search(string searchText, int page = 1)
        {
            if (searchText == null)
                throw new HttpException(404, "Page not found");

            ViewBag.Search = searchText;
            ViewBag.Page = page;

            var viewModel = new PostViewModel(_post, searchText, page, pageLimit);

            return View(viewModel);
        }

    }
}
