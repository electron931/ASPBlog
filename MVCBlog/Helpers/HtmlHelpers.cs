using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Domain.Entities;


namespace MVCBlog.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString EmbedCss(this HtmlHelper htmlHelper, string path)
        {
            // take a path that starts with "~" and map it to the filesystem.
            var cssFilePath = HttpContext.Current.Server.MapPath(path);
            // load the contents of that file
            string cssText;
            try
            {
                cssText = System.IO.File.ReadAllText(cssFilePath);
            }
            catch (Exception)
            {
                // blank string if we can't read the file for any reason
                cssText = "";
            }
            return MvcHtmlString.Create(cssText);
        }

        public static MvcHtmlString PostLink(this HtmlHelper helper, Post post)
        {
            return helper.ActionLink(post.Title, "GetPost", "Post",
                new 
                { 
                    year = post.PostedOn.Year,
                    month = post.PostedOn.Month,
                    title = post.UrlSlug
                },
                new { title = post.Title });
        }

        public static MvcHtmlString CategoryLink(this HtmlHelper helper, Category category)
        {
            return helper.ActionLink(category.Name, "Category", "Post", 
                new { category = category.UrlSlug }, 
                new { title = String.Format("See all posts in {0}", category.Name) });
        }

        public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag)
        {
            return helper.ActionLink(tag.Name, "Tag", "Post", new { tagSlug = tag.UrlSlug },
                new { title = String.Format("See all posts in {0}", tag.Name) });
        }
    }
}