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
    public class CategoryController : Controller
    {
        private readonly CategoryHandler _category;
        private readonly PostHandler _post;
        private readonly int pageLimit = 9;


        public CategoryController()
        {
            _category = new CategoryHandler();
            _post = new PostHandler();
        }

        public ViewResult Posts(string categorySlug, int page = 1)
        {
            var viewModel = new CategoryViewModel(_category, categorySlug, _post, page, pageLimit);

            if (viewModel.Category == null)
                throw new HttpException(404, "Category not found");

            ViewBag.Title = String.Format(@"Latest posts on category ""{0}""",
                                viewModel.Category.Name);

            ViewBag.Page = page;

            return View(viewModel);
        }

        
        public PartialViewResult Categories()
        {
            var viewModel = new CategoryViewModel(_category);

            return PartialView("_Categories", viewModel.Categories);
        }

    }
}
