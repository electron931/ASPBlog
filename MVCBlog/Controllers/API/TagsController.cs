using BusinessLogic;
using BusinessLogic.Handlers;
using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace MVCBlog.Controllers.API
{
    public class TagsController : ApiController
    {
        private readonly TagHandler _tagHandler;
        private JsonSerializer serializer;

        public TagsController()
        {
            _tagHandler = new TagHandler();

            serializer = new JsonSerializer
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new NHibernateContractResolver()
            };
        }

        // GET /api/tags
        public HttpResponseMessage GetAllTags()
        {
            var tags = _tagHandler.Tags();

            if (tags == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Oops!" });
            else
            {
                /*
                StringWriter stringWriter = new StringWriter();
                JsonWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(stringWriter);
                serializer.Serialize(jsonWriter, tags);

                string serializedObject = stringWriter.ToString();
                */
                return Request.CreateResponse(HttpStatusCode.OK, tags);
            }
        }

        // GET /api/tags/{id}
        public HttpResponseMessage GetTag(int id)
        {
            Tag tag = _tagHandler.Tag(id);
            if (tag == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Oops!" });
            else
            {
                /*
                StringWriter stringWriter = new StringWriter();
                JsonWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(stringWriter);
                serializer.Serialize(jsonWriter, tag);

                string serializedObject = stringWriter.ToString();
                */
                return Request.CreateResponse(HttpStatusCode.OK, tag);
            }
        }

        // POST /api/tags
        //response body:
        /*
         * {
            "Name": "NewTag",
            "UrlSlug": "newtag",
            "Description": null,
            "Id": 8
        }
         * */
        public HttpResponseMessage PostTag(Tag tag)
        {
            try
            {
                _tagHandler.Edit(tag);
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Tag was updated!" });
            }
            catch(Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = "Oops!" });
            }
        }

        // PUT /api/tags
        //response body:
        /*
         * {
            "Name": "NewTag",
            "UrlSlug": "newtag",
            "Description": null
        }
         * */
        public HttpResponseMessage PutTag(Tag tag)
        {
            try
            {
                _tagHandler.Add(tag);
                return Request.CreateResponse<Tag>(HttpStatusCode.Created, tag);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = "Oops!" });
            }
        }

        // DELETE /api/tags/{id}
        public HttpResponseMessage DeleteTag(int id)
        {
            try
            {
                _tagHandler.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Tag was deleted!" });
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = "Oops!" });
            }
        }
    }
}
