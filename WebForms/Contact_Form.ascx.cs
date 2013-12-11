using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms
{
    public partial class Contact_Form : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void btnSend_Click(object sender, EventArgs e)
        {
            Session["StatusMessage"] = "Thank you, " + txtName.Text + ", for your message!";
            Response.Redirect("Default.aspx");
        }

        public void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtSubject.Text = "";
            txtMessage.Value = "";
        }
    }
}