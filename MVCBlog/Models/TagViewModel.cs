using System;
using System.Collections.Generic;
using Domain.Entities;
using BusinessLogic.Handlers;


namespace MVCBlog.Models
{
    public class TagViewModel : BaseViewModel
    {
        public TagViewModel(TagHandler _tag)
        {
            Tags = _tag.Tags();
        }

        public TagViewModel(TagHandler _tag, string tagSlug, PostHandler _post, int page, int pageLimit) :
            base(_post, page, pageLimit)
        {
            Tag = _tag.Tag(tagSlug);
            Posts = _post.PostsForTag(tagSlug, page - 1, pageLimit);
            TotalPosts = _post.TotalPostsForTag(tagSlug);
            TotalPostsForPage = Posts.Count;
        }

        public Tag Tag { get; private set; }
        public IList<Tag> Tags { get; private set; }
    }
}