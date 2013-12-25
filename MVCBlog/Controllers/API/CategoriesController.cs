using BusinessLogic;
using BusinessLogic.Handlers;
using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MVCBlog.Controllers.API
{
    public class CategoriesController : ApiController
    {
        private readonly CategoryHandler _categoryHandler;
        private JsonSerializer serializer;

        public CategoriesController()
        {
            _categoryHandler = new CategoryHandler();

            serializer = new JsonSerializer
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new NHibernateContractResolver()
            };
        }

        // GET /api/categories
        public HttpResponseMessage GetAllCategories()
        {
            var categories = _categoryHandler.Categories();

            if (categories == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Oops!" });
            else
            {
                /*
                StringWriter stringWriter = new StringWriter();
                JsonWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(stringWriter);
                serializer.Serialize(jsonWriter, categories);

                string serializedObject = stringWriter.ToString();
                */
                return Request.CreateResponse(HttpStatusCode.OK, categories);
            }
        }

        // GET /api/categories/{id}
        public HttpResponseMessage GetCategory(int id)
        {
            Category category = _categoryHandler.Category(id);
            if (category == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Oops!" });
            else
            {
                /*
                StringWriter stringWriter = new StringWriter();
                JsonWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(stringWriter);
                serializer.Serialize(jsonWriter, category);

                string serializedObject = stringWriter.ToString();
                */
                return Request.CreateResponse(HttpStatusCode.OK, category);
            }
        }

        // POST /api/categories
        //response body:
        /*
         * {
            "Name": "Yahho!!",
            "UrlSlug": "yaho",
            "Description": null,
            "Id": 3
        }
         * */
        public HttpResponseMessage PostCategory(Category category)
        {
            try
            {
                _categoryHandler.Edit(category);
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Category was updated!" });
            }
            catch(Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = "Oops!" });
            }
        }

        // PUT /api/categories
        //response body:
        /*
         * {
            "Name": "Yahho!!",
            "UrlSlug": "yaho",
            "Description": null
        }
         * */
        public HttpResponseMessage PutCategory([Bind(Exclude = "Id")]Category category)
        {
            //instead of Admin/AddCategory/
            //test mode
            string json;

            try 
            {
                var id = _categoryHandler.Add(category);
                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Category added successfully."
                });

                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
            catch(Exception)
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the category."
                });

                return Request.CreateResponse(HttpStatusCode.InternalServerError, json);
            }

            /*
            try
            {
                _categoryHandler.Add(category);
                return Request.CreateResponse<Category>(HttpStatusCode.Created, category);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = "Oops!" });
            }
             * */
        }

        // DELETE /api/categories/{id}
        public HttpResponseMessage DeleteCategory(int id)
        {
            try
            {
                _categoryHandler.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Category was deleted!" });
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = "Oops!" });
            }
        }
    }
}
