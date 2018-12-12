using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

namespace NorthwindApp.Acc
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            User user = null;
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
                if (user.Password == txtCurrentPassword.Text)
                {
                    if (txtNewPassword.Text == txtConfirmPassword.Text)
                    {
                        user = user.ChangePassword(user.Email, txtNewPassword.Text);

                        if(user==null)
                        {
                            lblStatus.Text = "Password was not changed.";
                            EmailHelper.Email.Send(user.Email, "Email was not changed", "Contact Administrator.");
                        }
                        else
                        {
                            lblStatus.Text = "Password was changed sussessfully";
                        }
                    }
                    else
                    {
                        lblStatus.Text = "New Password does match confirm password.";
                    }
                }
                else
                {
                    lblStatus.Text = "Password does not match currrent password.";
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}