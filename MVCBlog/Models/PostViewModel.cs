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
            TotalPosts = Posts.Count;
        }
 
        public IList<Post> Posts { get; private set; }
        public int TotalPosts { get; private set; }
    }
}