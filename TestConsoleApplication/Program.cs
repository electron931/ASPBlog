﻿using System;
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
                Console.WriteLine(post.Id + ". " + post.Comments.Count + "  " + post.PostedOn.ToShortDateString());

            TagHandler tagHandler = new TagHandler();

            IList<Tag> tags = tagHandler.Tags();
            foreach (var tag in tags)
                Console.WriteLine(tag.Name);

            UserHandler userHandler = new UserHandler();

            IList<User> users = userHandler.AuthorUsers();
            foreach (var user in users)
                Console.WriteLine(user.Id + ". " + user.UserName + " " + user.Password);

            Post superPost = new Post { Author = userHandler.User(3), Category = catHandler.Category(2), Title = "Test", ShortDescription = "Test", Description = "Test", Image = "Test", Published = true, PostedOn = DateTime.Now, UrlSlug = "Test", Tags = tagHandler.Tags() };

            postHandler.Add(superPost);

            logger.Debug("Application loaded succesfully");
            
            Console.ReadLine();
        }
    }
}
