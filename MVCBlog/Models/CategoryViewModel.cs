using System;
using System.Collections.Generic;
using Domain.Entities;
using BusinessLogic.Handlers;


namespace MVCBlog.Models
{
    public class CategoryViewModel : BaseViewModel
    {
        public CategoryViewModel(CategoryHandler _category)
        {
            Categories = _category.Categories();
        }

        public CategoryViewModel(CategoryHandler _category, string categorySlug, PostHandler _post, int page, int pageLimit) :
            base(_post, page, pageLimit)
        {
            Posts = _post.PostsForCategory(categorySlug, page - 1, pageLimit);
            TotalPosts = _post.TotalPostsForCategory(categorySlug);
            Category = _category.Category(categorySlug);
            TotalPostsForPage = Posts.Count;
        }

        public Category Category { get; private set; }
        public IList<Category> Categories { get; private set; }
    }
}