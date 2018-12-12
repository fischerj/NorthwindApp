using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BusinessObjects;

namespace NorthwindApp
{
    public partial class UploadImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ImageList images = new ImageList();
            images.MapPath = Server.MapPath("Files");

            images = images.GetAll();
            rptImages.DataSource = images.List;
            rptImages.DataBind();
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string folderPath = Server.MapPath("~/Files/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            //Save the File to the Directory (Folder).
            FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

            //Display the success message.
            lblMessage.Text = Path.GetFileName(FileUpload1.FileName) + " has been uploaded.";

            Images images = new Images();

            User user = (User)Session["User"];
            images.UserId = user.Id;
            images.FullPathfileName = Path.Combine(folderPath, FileUpload1.FileName);
            images.Save();
            
        }
    }
}