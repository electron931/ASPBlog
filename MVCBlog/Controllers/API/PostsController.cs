using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogic.Handlers;
using Domain.Entities;
using Newtonsoft.Json;
using BusinessLogic;
using System.IO;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Web.Script.Serialization;


namespace MVCBlog.Controllers.API
{
    public class PostsController : ApiController
    {
        private readonly PostHandler _postHandler;
        private JsonSerializer serializer;

        public PostsController()
        {
            _postHandler = new PostHandler();

            serializer = new JsonSerializer
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new NHibernateContractResolver()
            };
        }

        // GET /api/posts
        public HttpResponseMessage GetAllPosts()
        {
            var posts = _postHandler.Posts();

            if (posts == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Oops!" });
            else
            {

                StringWriter stringWriter = new StringWriter();
                JsonWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(stringWriter);
                serializer.Serialize(jsonWriter, posts);

                
                string serializedObject = stringWriter.ToString();

                return Request.CreateResponse(HttpStatusCode.OK, serializedObject);
            }
        }

        // GET /api/posts/{id}
        public HttpResponseMessage GetPost(int id)
        {
            Post post = _postHandler.Post(id);
            if (post == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Oops!" });
            else
            {
                StringWriter stringWriter = new StringWriter();
                JsonWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(stringWriter);
                serializer.Serialize(jsonWriter, post);

                string serializedObject = stringWriter.ToString().Replace(@"\", "");

                return Request.CreateResponse(HttpStatusCode.OK, serializedObject);
            }
        }

        // POST /api/posts
        //response body:
        /*
         * {
            "Title": "Test",
            "ShortDescription": "Test",
            "Description": "Test",
            "Meta": "",
            "UrlSlug": "test",
            "Image": "test.jpg",
            "Published": true,
            "PostedOn": "2013-11-13T00:00:00",
            "Modified": null,
            "Category": {
                "Name": "Programming",
                "UrlSlug": "programming",
                "Description": null,
                "Id": 1
            },
            "Tags": [{
                "Name": "CSharp",
                "UrlSlug": "csharp",
                "Description": null,
                "Id": 1
            }, {
                "Name": "ASP.NET",
                "UrlSlug": "asp_net",
                "Description": null,
                "Id": 3
            }],
            "Author": {
                "UserName": "Tomas Anderson",
                "Password": "matrix",
                "Type": "Author",
                "FirstName": null,
                "LastName": null,
                "Email": null,
                "Info": null,
                "Id": 1
            },
            "Id": 1
        }
         * */
        public HttpResponseMessage PostPost(Post post)
        {
            try
            {
                _postHandler.Edit(post);
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Post was updated!" });
            }
            catch(Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = "Oops!" });
            }
        }

        // PUT /api/posts
        //response body:
        /*
         * {
            "Title": "Test",
            "ShortDescription": "Test",
            "Description": "Test",
            "Meta": "",
            "UrlSlug": "test",
            "Image": "test.jpg",
            "Published": true,
            "PostedOn": "2013-11-13T00:00:00",
            "Modified": null,
            "Category": {
                "Name": "Programming",
                "UrlSlug": "programming",
                "Description": null,
                "Id": 1
            },
            "Tags": [{
                "Name": "CSharp",
                "UrlSlug": "csharp",
                "Description": null,
                "Id": 1
            }, {
                "Name": "ASP.NET",
                "UrlSlug": "asp_net",
                "Description": null,
                "Id": 3
            }],
            "Author": {
                "UserName": "Tomas Anderson",
                "Password": "matrix",
                "Type": "Author",
                "FirstName": null,
                "LastName": null,
                "Email": null,
                "Info": null,
                "Id": 1
            }
        }
         * */
        public HttpResponseMessage PutPost(Post post)
        {
            try
            {
                _postHandler.Add(post);
                return Request.CreateResponse<Post>(HttpStatusCode.Created, post);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = "Oops!" });
            }
        }

        // DELETE /api/posts/{id}
        public HttpResponseMessage DeletePost(int id)
        {
            try
            {
                _postHandler.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Post was deleted!" });
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = "Oops!" });
            }
        }
    }
}
