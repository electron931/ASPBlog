using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Handlers;
using System.Web.ModelBinding;

namespace WebForms
{
    public partial class Post : System.Web.UI.Page
    {
        PostHandler postHandler;
        CommentHandler commentHandler;

        protected void Page_Load(object sender, EventArgs e)
        {
            postHandler = new PostHandler();
            commentHandler = new CommentHandler();
        }

        public Domain.Entities.Post GetPost([QueryString("id")]int? id)
        {
            return postHandler.Post(id ?? -1);
        }

        public int CommentsCount(int postId)
        {
            return commentHandler.TotalCommentsForPost(postId);
        }

        public IList<Domain.Entities.Comment> GetComments([QueryString("id")]int? postId)
        {
            return commentHandler.CommentsForPost(postId ?? -1);
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