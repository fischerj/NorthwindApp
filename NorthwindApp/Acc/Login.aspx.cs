using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

namespace NorthwindApp.Acc
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            User user = new BusinessObjects.User();
            user = user.Login(txtEmail.Text, txtPassword.Text);
            if(user == null)
            {
                lblStatus.Text = "Invalid User Name or password";
            }
            else if(user.Version == 0)
            {
                
                Session["User"] = user;
                Response.Redirect("ChangePassword.aspx");
            }
            else
            {
                Session["User"] = user;
                Response.Redirect("../index.aspx");
            }

        }

        protected void lbForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx");
        }
    }
}