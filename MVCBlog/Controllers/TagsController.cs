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
    public class TagsController : Controller
    {
        private readonly TagHandler _tag;
        private readonly PostHandler _post;
        private readonly int pageLimit = 9;


        public TagsController()
        {
            _tag = new TagHandler();
            _post = new PostHandler();
        }

        public ViewResult Tag(string tagSlug, int page = 1)
        {
            var viewModel = new TagViewModel(_tag, tagSlug, _post, page, pageLimit);

            if (viewModel.Tag == null)
                throw new HttpException(404, "Tag not found");

            ViewBag.Title = String.Format(@"Latest posts tagged on ""{0}""",
                viewModel.Tag.Name);

            ViewBag.Page = page;

            return View(viewModel);
        }

        public ViewResult List()
        {
            var viewModel = new TagViewModel(_tag);

            return View(viewModel.Tags);
        }
    }
}
