using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogic.Handlers;
using Domain.Entities;
using System.IO;
using Newtonsoft.Json;
using BusinessLogic;

namespace MVCBlog.Controllers.API
{
    public class CommentsController : ApiController
    {
        private readonly CommentHandler _commentsHandler;
        private JsonSerializer serializer;

        public CommentsController()
        {
            _commentsHandler = new CommentHandler();
            serializer = new JsonSerializer
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new NHibernateContractResolver()
            };
        }

        // GET /api/comments
        public HttpResponseMessage GetAllComments()
        {
            var comments = _commentsHandler.Comments();

            if (comments == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Oops!" });
            else
            {

                StringWriter stringWriter = new StringWriter();
                JsonWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(stringWriter);
                serializer.Serialize(jsonWriter, comments);


                string serializedObject = stringWriter.ToString();

                return Request.CreateResponse(HttpStatusCode.OK, serializedObject);
            }
        }

        // GET /api/comments/{id}
        public HttpResponseMessage GetComments(int id)
        {
            var comments = _commentsHandler.CommentsForPost(id);

            if (comments == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Oops!" });
            else
            {

                StringWriter stringWriter = new StringWriter();
                JsonWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(stringWriter);
                serializer.Serialize(jsonWriter, comments);

                string serializedObject = stringWriter.ToString();

                return Request.CreateResponse(HttpStatusCode.OK, serializedObject);
            }
        }


        //POST /api/comments
        //response body:
        /*
         * {
            "Text": "just a simple comment",
            "Likes": 5,
            "Dislikes": 2,
            "Created": "2013-08-02T00:00:00",
            "Modified": null,
            "User": {
                "UserName": "Tomas Anderson",
                "Password": "matrix",
                "Type": "Author",
                "FirstName": null,
                "LastName": null,
                "Email": null,
                "Info": null,
                "Id": 1
            },
            "Post": {
                "Title": "My Mouse Is Missing",
                "ShortDescription": "Test. ",
                "Description": "Test",
                "Meta": "",
                "UrlSlug": "my_mouse_is_missing",
                "Image": "pic1.jpg",
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
            },
            "Id": 5
        }
         * */
        public HttpResponseMessage PostComment(Comment comment)
        {
            try
            {
                _commentsHandler.Edit(comment);
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Comment was updated!" });
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = "Oops!" });
            }
        }

        //PUT /api/comments
        //response body:
        /*
         * {
            "Text": "just a simple comment",
            "Likes": 5,
            "Dislikes": 2,
            "Created": "2013-08-02T00:00:00",
            "Modified": null,
            "User": {
                "UserName": "Tomas Anderson",
                "Password": "matrix",
                "Type": "Author",
                "FirstName": null,
                "LastName": null,
                "Email": null,
                "Info": null,
                "Id": 1
            },
            "Post": {
                "Title": "My Mouse Is Missing",
                "ShortDescription": "Test. ",
                "Description": "Test",
                "Meta": "",
                "UrlSlug": "my_mouse_is_missing",
                "Image": "pic1.jpg",
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
        }
         * */
        public HttpResponseMessage PutComment(Comment comment)
        {
            try
            {
                _commentsHandler.Add(comment);
                return Request.CreateResponse<Comment>(HttpStatusCode.Created, comment);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = "Oops!" });
            }
        }

        // DELETE /api/comments/{id}
        public HttpResponseMessage DeleteComment(int id)
        {
            try
            {
                _commentsHandler.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Comment was deleted!" });
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = "Oops!" });
            }
        }

    }
}
