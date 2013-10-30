using System;
using System.Collections.Generic;
using BusinessLogic.Handlers;
using Domain.Entities;
using NLog;


namespace TestConsoleApplication
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Debug("Application start");

            CategoryHandler catHandler = new CategoryHandler();

            IList<Category> categories = catHandler.Categories();
            foreach (var category in categories)
                Console.WriteLine(category.Id + ". " + category.Name);

            CommentHandler comHandler = new CommentHandler();

            IList<Comment> comments = comHandler.UserComments(1);
            foreach (var comment in comments)
                Console.WriteLine(comment.Id + ". " + comment.Text);

            PostHandler postHandler = new PostHandler();

            IList<Post> posts = postHandler.Posts();
            foreach (var post in posts)
                Console.WriteLine(post.Id + ". " + post.Author.Login + "  " + post.PostedOn.ToShortDateString());

            TagHandler tagHandler = new TagHandler();

            IList<Tag> tags = tagHandler.Tags();
            foreach (var tag in tags)
                Console.WriteLine(tag.Name);

            UserHandler userHandler = new UserHandler();

            IList<User> users = userHandler.AuthorUsers();
            foreach (var user in users)
                Console.WriteLine(user.Id + ". " + user.Login + " " + user.Password);


            logger.Debug("Application loaded succesfully");

            Console.ReadLine();
        }
    }
}
