using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogic.Handlers;
using Domain.Entities;
using System.Web.Mvc;

namespace MVCBlog.Models
{
    public class PostModelBinder : DefaultModelBinder
    {
        private readonly CategoryHandler _categoryHandler;
        private readonly TagHandler _tagHandler;
        private readonly UserHandler _userHandler;

        public PostModelBinder()
        {
            _categoryHandler = new CategoryHandler();
            _tagHandler = new TagHandler();
            _userHandler = new UserHandler();
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var post = (Post)base.BindModel(controllerContext, bindingContext);
            
            int categoryId = Int32.Parse(bindingContext.ValueProvider.GetValue("Category").AttemptedValue);
            int authorId = Int32.Parse(bindingContext.ValueProvider.GetValue("Author").AttemptedValue);

            var category = _categoryHandler.Category(categoryId);
            var author = _userHandler.User(authorId);

            if (post.Category == null)
                post.Category = category;

            if (post.Author == null)
                post.Author = author;

            var tags = bindingContext.ValueProvider.GetValue("Tags").AttemptedValue.Split(',');

            if (tags.Length > 0)
            {
                post.Tags = new List<Tag>();

                foreach (var tag in tags)
                {
                    post.Tags.Add(_tagHandler.Tag(int.Parse(tag.Trim())));
                }
            }

            if (bindingContext.ValueProvider.GetValue("oper").AttemptedValue.Equals("edit"))
                post.Modified = DateTime.UtcNow;
            else
                post.PostedOn = DateTime.UtcNow;

            return post;
        }
    }
}