using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Handlers;

namespace WebForms
{
    public partial class _Default : Page
    {
        private PostHandler handler;

        protected void Page_Load(object sender, EventArgs e)
        {
            handler = new PostHandler();

            if (!string.IsNullOrEmpty((string)Session["StatusMessage"]))
            {
                string message = (string)Session["StatusMessage"];
                Session["StatusMessage"] = null;
                MessageLabel.Visible = true;
                MessageLabel.Text = message;
            }
        }

        public int TotalPosts()
        {
            return handler.TotalPosts();
        }

        public IQueryable<Domain.Entities.Post> Posts()
        {
            return handler.Posts().AsQueryable();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            DropDownList switchThemes = (DropDownList)this.Page.Master.FindControl("switchThemes");

            string theme;
            theme = (string)Session["theme"];

            if (theme != null)
            {
                Page.Theme = theme;
                switchThemes.Text = theme;
            }
            else
            {
                Session["theme"] = switchThemes.Text;
                Page.Theme = "Default";
            }
        }
    }
}