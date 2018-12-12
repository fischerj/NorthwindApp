using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

namespace NorthwindApp.Acc
{
    public partial class RegisterPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new BusinessObjects.User();
                user = user.Register(txtEmail.Text);
                if (user == null)
                {
                    lblStatus.Text = "Email was not sent.";
                }
            }
            catch(Exception ex)
            {
                lblStatus.Text = ex.Message;
            }

        }
    }
}