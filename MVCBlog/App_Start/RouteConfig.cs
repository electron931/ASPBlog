using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Admin",
                url: "admin",
                defaults: new { controller = "Admin", action = "Admin" }
            );

            routes.MapRoute(
                name: "addComment",
                url: "addComment",
                defaults: new { controller = "Post", action = "addComment" }
            );

            routes.MapRoute(
                name: "Register",
                url: "register",
                defaults: new { controller = "Admin", action = "Register" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "logout",
                defaults: new { controller = "Admin", action = "Logout" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Admin", action = "Login" }
            );

            routes.MapRoute(
                name: "BlogPost",
                url: "posts/{categorySlug}/{postSlug}",
                defaults: new { controller = "Post", action = "GetPost"}
            );

            routes.MapRoute(
                name: "CategoryPosts",
                url: "posts/{categorySlug}",
                defaults: new { controller = "Category", action = "Posts" }
            );

            routes.MapRoute(
                name: "Tag",
                url: "tag/{tagSlug}",
                defaults: new { controller = "Tags", action = "Tag" }
            );

            routes.MapRoute(
                name: "Contacts",
                url: "contacts",
                defaults: new { controller = "Info", action = "Contacts" }
            );

            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new { controller = "Info", action = "About" }
            );

            routes.MapRoute(
                name: "Search",
                url: "search",
                defaults: new { controller = "Info", action = "Search" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}