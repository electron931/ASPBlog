using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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