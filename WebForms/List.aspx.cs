using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Handlers;

namespace WebForms
{
    public partial class List : System.Web.UI.Page
    {
        PostHandler postHandler;

        protected void Page_Load(object sender, EventArgs e)
        {
            postHandler = new PostHandler();
        }

        public IList<Domain.Entities.Post> PostsByTag([QueryString("tag")]string tag)
        {
            return postHandler.PostsForTag(tag, 0, 100);
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