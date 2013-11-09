using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogic.Handlers;
using Domain.Entities;


namespace MVCBlog.Models
{
    public class PostViewModel : BaseViewModel
    {
        public PostViewModel(PostHandler _post, int page, int pageLimit): base(_post, page, pageLimit)
        {
            Posts = _post.PostsForPage(page - 1, pageLimit);
            TotalPostsForPage = Posts.Count;
            TotalPosts = _post.TotalPosts();
        }

    }
}