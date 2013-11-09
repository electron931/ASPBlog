using System;
using System.Collections.Generic;
using Domain.Entities;
using BusinessLogic.Handlers;

namespace MVCBlog.Models
{
    public abstract class BaseViewModel
    {
        public BaseViewModel() { }

        public BaseViewModel(PostHandler _post, int page, int pageLimit)
        {
            PageLimit = pageLimit;
        }

        public IList<Post> Posts { get; protected set; }
        public int TotalPostsForPage { get; protected set; }
        public int TotalPosts { get; protected set; }
        public int PageLimit { get; protected set; }
    }
}