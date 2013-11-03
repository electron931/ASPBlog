using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogic.Handlers;
using Domain.Entities;


namespace MVCBlog.Models
{
    public class PostViewModel
    {
        public PostViewModel(PostHandler _post, int page, int pageLimit)
        {
            Posts = _post.PostsForPage(page - 1, pageLimit);
            TotalPostsForPage = Posts.Count;
            TotalPosts = _post.TotalPosts();
            PageLimit = pageLimit;
        }
 
        public IList<Post> Posts { get; private set; }
        public int TotalPostsForPage { get; private set; }
        public int TotalPosts { get; private set; }
        public int PageLimit { get; private set; }
    }
}