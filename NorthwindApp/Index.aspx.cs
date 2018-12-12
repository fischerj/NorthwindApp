using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using System.IO;

namespace NorthwindApp
{
    public partial class Index : System.Web.UI.Page
    {
        ShipperList shippers = new ShipperList();
        protected void Page_Load(object sender, EventArgs e)
        {
            gvShipper.RowEditing += GvShipper_RowEditing;
            gvShipper.RowCancelingEdit += GvShipper_RowCancelingEdit;
            gvShipper.RowUpdating += GvShipper_RowUpdating;
            gvShipper.RowDataBound += GvShipper_RowDataBound;
            gvShipper.RowDeleting += GvShipper_RowDeleting;

            if (!IsPostBack)
            {
                getAll();
            }
            
        }

        private void GvShipper_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Session["shippers"] != null)
            {
                ShipperList shippers = (ShipperList)Session["shippers"];
                Shipper shipper = shippers.List[e.RowIndex];
                shipper.Deleted = true;
                shipper.Save();
            }
            gvShipper.EditIndex = -1;
            getAll();
        }

        private void GvShipper_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkBtnDelete = e.Row.FindControl("lbDelete") as LinkButton;

                // Use whatever control you want to show in the confirmation message
                Label lblContactName = e.Row.FindControl("lblCompanyName") as Label;

                lnkBtnDelete.Attributes.Add("onclick", string.Format("return confirm('Are you sure you want to delete the contact {0}?');", lblContactName.Text));

            }
        }

        private void GvShipper_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtCompanyName = (TextBox)gvShipper.Rows[e.RowIndex].FindControl("txtCompanyName");
            TextBox txtPhone = (TextBox)gvShipper.Rows[e.RowIndex].FindControl("txtPhone");
            FileUpload fu = (FileUpload)gvShipper.Rows[e.RowIndex].FindControl("fileupload1");
            if(Session["shippers"] != null)
            {
                ShipperList shippers = (ShipperList)Session["shippers"];
                Shipper shipper = shippers.List[e.RowIndex];
                shipper.CompanyName = txtCompanyName.Text;
                shipper.Phone = txtPhone.Text;
                shipper.FullPathfileName =  Path.Combine( Server.MapPath("Files"), fu.FileName);
                shipper.Image = fu.FileBytes;
                shipper.Save();
            }
            gvShipper.EditIndex = -1;
            getAll();
        }

        private void GvShipper_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvShipper.EditIndex = -1;
            getAll();
        }

        private void GvShipper_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvShipper.EditIndex = e.NewEditIndex;
            getAll();
        }

        private void getAll()
        {
            shippers.MapPath = Server.MapPath("Files");
            shippers = shippers.GetAll();

            if(Session["Shippers"] == null)
            {
                Session["Shippers"] = shippers;
            }

            Shipper shipper = new Shipper();

            gvShipper.DataSource = shippers.List;
            shippers.List.Add(shipper);

            gvShipper.DataBind();

            int row = gvShipper.Rows.Count-1;

            ((LinkButton)gvShipper.Rows[row].Cells[0].Controls[1]).Text = "Add";
        }
    }
}