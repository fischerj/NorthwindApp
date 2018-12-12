using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

namespace NorthwindApp.Acc
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            User user = new BusinessObjects.User();
            user = user.ForgotPassword(txtEmail.Text);
            if (user == null)
            {
                lblStatus.Text = "No email found";
            }
            else
            {
                lblStatus.Text = "Check your email for your password";
            }
        }
    }
}