using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLogic.Handlers;
using MVCBlog.Account;
using System.Web.Script.Serialization;
using MVCBlog.Models;
using Newtonsoft.Json;
using MVCBlog.Helpers;
using System.Text;
using BusinessLogic;
using System.IO;

namespace MVCBlog.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserHandler _userHandler;
        private readonly PostHandler _postHandler;
        private readonly CategoryHandler _categoryHandler;
        private readonly TagHandler _tagHandler;

        public AdminController()
        {
            _userHandler = new UserHandler();
            _postHandler = new PostHandler();
            _categoryHandler = new CategoryHandler();
            _tagHandler = new TagHandler();

            List<String> authorName = new List<string>();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Manage");
            }

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Manage");
            }

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Register(string userName, string password)
        {
            //place for validation
            User user = _userHandler.User(userName);
            if (user != null)
            {
                ViewBag.AlreadyExists = "The user with this username is already exists!";
                return View();
            }

            User newUser = new User { UserName = userName, Password = password, Type = UserType.Subscriber };
            _userHandler.Add(newUser);
            UserInfo userInfo = new UserInfo(newUser);

            string userData = userInfo.ToString();

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     userName,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(45),
                     true,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);

            return RedirectToAction("Index", "Post");
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public RedirectToRouteResult Login(string userName, string password)
        {
            var user = _userHandler.User(userName, password);
            if (user != null)
            {
                UserInfo userInfo = new UserInfo(user);

                string userData = userInfo.ToString();

                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                         1,
                         userName,
                         DateTime.Now,
                         DateTime.Now.AddMinutes(45),
                         true,
                         userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);

                return RedirectToAction("Index", "Post");
            }
            else
            {
                return RedirectToAction("Contacts", "Info");
            }
            
        }

        [Authorize(Roles = "Author,Admin,Subscriber")]
        public RedirectResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(Request.UrlReferrer.ToString());
        }

        [AllowAnonymous]
        public ActionResult Admin()
        {
            if (HttpContext.User.IsInRole("Admin,Author"))
            {
                return RedirectToAction("Manage", "Admin");
            }

            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult AdminValidate(UserModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.IsInRole("Subscriber"))
                {
                    ViewBag.AdminValidationError = "The user name or password provided is incorrect.";
                    return View("Admin");
                }
                else
                {
                    var user = _userHandler.User(model.UserName, model.Password);

                    if (user != null)
                    {
                        if (user.Type == UserType.Subscriber)
                        {
                            ViewBag.AdminValidationError = "The user name or password provided is incorrect.";
                            return View("Admin");
                        }

                        UserInfo userInfo = new UserInfo(user);

                        string userData = userInfo.ToString();

                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                    1,
                                    model.UserName,
                                    DateTime.Now,
                                    DateTime.Now.AddMinutes(45),
                                    true,
                                    userData);

                        string encTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        Response.Cookies.Add(faCookie);

                        return RedirectToAction("Manage", "Admin");
                    }
                    else
                    {
                        ViewBag.AdminValidationError = "The user name or password provided is incorrect.";
                        return View("Admin");
                    }
                }
            }
            
            else
            {
                ViewBag.AdminValidationError = "The user name or password provided is incorrect.";
                return View("Admin");
            }
        }

        [Authorize(Roles="Admin,Author")]
        public ViewResult Manage()
        {
            return View();
        }

        public ContentResult Posts(JqInViewModel jqParams)
        {
            IList<Post> posts;
            int totalPosts;

            if (HttpContext.User.IsInRole("Admin"))
            {
                posts = _postHandler.Posts(jqParams.page - 1, jqParams.rows,
                                                jqParams.sortColumnName, jqParams.sortOrder == "asc");
                totalPosts = _postHandler.TotalPosts(false);
            }
            else
            {
                User user = _userHandler.User(HttpContext.User.Identity.Name);
                posts = _postHandler.UserPosts(user.Id, jqParams.page - 1, jqParams.rows,
                                                jqParams.sortColumnName, jqParams.sortOrder == "asc");
                totalPosts = _postHandler.TotalUserPosts(user.Id, false);
            }  

            var serializer = new JsonSerializer
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new NHibernateContractResolver()
            };

            StringWriter stringWriter = new StringWriter();
            JsonWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(stringWriter);
            serializer.Serialize(jsonWriter, new
                                            {
                                                page = jqParams.page,
                                                records = totalPosts,
                                                rows = posts,
                                                total = Math.Ceiling(Convert.ToDouble(totalPosts) / jqParams.rows)
                                            });

            string serializedObject = stringWriter.ToString();

            return Content(serializedObject, "application/json");
        }

        public ContentResult GetCategoriesHtml()
        {
            var categories = _categoryHandler.Categories().OrderBy(s => s.Name);

            var sb = new StringBuilder();
            sb.AppendLine(@"<select>");

            foreach (var category in categories)
            {
                sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>",
                    category.Id, category.Name));
            }

            sb.AppendLine("<select>");
            return Content(sb.ToString(), "text/html");
        }

        public ContentResult GetTagsHtml()
        {
            var tags = _tagHandler.Tags().OrderBy(s => s.Name);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<select multiple=""multiple"">");

            foreach (var tag in tags)
            {
                sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>",
                    tag.Id, tag.Name));
            }

            sb.AppendLine("<select>");
            return Content(sb.ToString(), "text/html");
        }

        public ContentResult GetAuthorsHtml()
        {
            var users = _userHandler.AuthorUsers().OrderBy(s => s.UserName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<select multiple=""false"">");

            foreach (var user in users)
            {
                sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>",
                    user.Id, user.UserName));
            }

            sb.AppendLine("<select>");
            return Content(sb.ToString(), "text/html");
        }

        [HttpPost, ValidateInput(false)]
        public ContentResult AddPost(Post post)
        {
            string json;

            ModelState.Clear();

            if (TryValidateModel(post))
            {
                var id = _postHandler.Add(post);

                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Post added successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the post."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost, ValidateInput(false)]
        public ContentResult EditPost(Post post)
        {
            string json;

            ModelState.Clear();

            if (TryValidateModel(post))
            {
                _postHandler.Edit(post);
                json = JsonConvert.SerializeObject(new
                {
                    id = post.Id,
                    success = true,
                    message = "Changes saved successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to save the changes."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult DeletePost(int id)
        {
            string json;

            try
            {
                _postHandler.Delete(id);
            }
            catch (Exception)
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Oops! Something terrible has happened..."
                });

                return Content(json, "application/json");
            }

            json = JsonConvert.SerializeObject(new
            {
                id = 0,
                success = true,
                message = "Post deleted successfully."
            });

            return Content(json, "application/json");
        }

        public ContentResult Categories()
        {
            var categories = _categoryHandler.Categories();

            return Content(JsonConvert.SerializeObject(new
            {
                page = 1,
                records = categories.Count,
                rows = categories,
                total = 1
            }), "application/json");
        }

        [HttpPost]
        public ContentResult AddCategory([Bind(Exclude = "Id")]Category category)
        {
            string json;

            if (ModelState.IsValid)
            {
                var id = _categoryHandler.Add(category);
                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Category added successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the category."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult EditCategory(Category category)
        {
            string json;

            if (ModelState.IsValid)
            {
                _categoryHandler.Edit(category);
                json = JsonConvert.SerializeObject(new
                {
                    id = category.Id,
                    success = true,
                    message = "Changes saved successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to save the changes."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult DeleteCategory(int id)
        {
            string json;
            try
            {
                _categoryHandler.Delete(id);
            }
            catch (Exception)
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Category has posts!"
                });

                return Content(json, "application/json");
            }

            json = JsonConvert.SerializeObject(new
            {
                id = 0,
                success = true,
                message = "Category deleted successfully."
            });

            return Content(json, "application/json");
        }

        public ContentResult Tags()
        {
            var tags = _tagHandler.Tags();

            return Content(JsonConvert.SerializeObject(new
            {
                page = 1,
                records = tags.Count,
                rows = tags,
                total = 1
            }), "application/json");
        }

        [HttpPost]
        public ContentResult AddTag([Bind(Exclude = "Id")]Tag tag)
        {
            string json;

            if (ModelState.IsValid)
            {
                var id = _tagHandler.Add(tag);
                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Tag added successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the tag."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult EditTag(Tag tag)
        {
            string json;

            if (ModelState.IsValid)
            {
                _tagHandler.Edit(tag);
                json = JsonConvert.SerializeObject(new
                {
                    id = tag.Id,
                    success = true,
                    message = "Changes saved successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to save the changes."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult DeleteTag(int id)
        {
            string json;

            try
            {
                _tagHandler.Delete(id);
            }
            catch (Exception)
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Tag has assigned posts!"
                });

                return Content(json, "application/json");
            }

            json = JsonConvert.SerializeObject(new
            {
                id = 0,
                success = true,
                message = "Tag deleted successfully."
            });

            return Content(json, "application/json");
        }

        [Authorize(Roles="Admin")]
        public ContentResult Users(JqInViewModel jqParams)
        {
            var users = _userHandler.AllUsers();
            int totalUsers = users.Count;

            var serializer = new JsonSerializer
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new NHibernateContractResolver()
            };

            StringWriter stringWriter = new StringWriter();
            JsonWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(stringWriter);
            serializer.Serialize(jsonWriter, new
            {
                page = jqParams.page,
                records = totalUsers,
                rows = users,
                total = Math.Ceiling(Convert.ToDouble(totalUsers) / jqParams.rows)
            });

            string serializedObject = stringWriter.ToString();

            return Content(serializedObject, "application/json");
        }

        public ContentResult GetRolesHtml()
        {
            var roles = Enum.GetValues(typeof(UserType)).Cast<UserType>().ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<select multiple=""false"">");

            foreach (var role in roles)
            {
                sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>",
                   role, role));
            }

            sb.AppendLine("<select>");
            return Content(sb.ToString(), "text/html");
        }

        [HttpPost]
        public ContentResult AddUser([Bind(Exclude = "Id")]User user)
        {
            string json;

            if (ModelState.IsValid)
            {
                var id = _userHandler.Add(user);
                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Tag added successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the tag."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult EditUser(User user)
        {
            string json;

            if (ModelState.IsValid)
            {
                _userHandler.Edit(user);
                json = JsonConvert.SerializeObject(new
                {
                    id = user.Id,
                    success = true,
                    message = "Changes saved successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to save the changes."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult DeleteUser(int id)
        {
            string json;

            var user = _userHandler.User(id);
            if (user.UserName == HttpContext.User.Identity.Name)
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "You can't delete yourself! It's stupid..."
                });
            }
            else
            {
                try
                {
                    _userHandler.Delete(id);
                }
                catch (Exception)
                {
                    json = JsonConvert.SerializeObject(new
                    {
                        id = 0,
                        success = false,
                        message = "User has assigned posts!"
                    });
                    return Content(json, "application/json");
                }

                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = true,
                    message = "User deleted successfully."
                });
            }

            return Content(json, "application/json");
        }

    }
}
