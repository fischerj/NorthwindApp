using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;  

namespace NorthwindApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvShipper.AutoGenerateColumns = false;
            gvShipper.RowEditing += GvShipper_RowEditing;
            gvShipper.RowCancelingEdit += GvShipper_RowCancelingEdit;
            gvShipper.RowUpdating += GvShipper_RowUpdating;
            gvShipper.SelectedIndexChanging += GvShipper_SelectedIndexChanging;
            gvShipper.RowDataBound += GvShipper_RowDataBound;
            gvShipper.RowCommand += GvShipper_RowCommand;
            if (!IsPostBack)
            {
                GetAll();
            }
        }

        private void GvShipper_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        private void GvShipper_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                LinkButton lb = new LinkButton();
                lb.CommandArgument = e.Row.Cells[0].Text;
                lb.CommandName = "NumClick";
                lb.Text = e.Row.Cells[0].Text;
                e.Row.Cells[0].Controls.Add((Control)lb);
            }
        }

        private void GvShipper_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            // find values for update

            //TextBox txtname = (TextBox)GridView1.FooterRow.FindControl("txt_Name");
            //TextBox txtbranch = (TextBox)GridView1.FooterRow.FindControl("txt_Branch");
            //TextBox txtcity = (TextBox)GridView1.FooterRow.FindControl("txt_City");

            //// insert values into database

            //SqlCommand cmd = new SqlCommand("insert into tbl_student(Name, Branch, City)
            //                  values('" + txtname.Text + "', '" + txtbranch.Text + "', '" + txtcity.Text + "')", con);
    
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
            //BindGridView();
        }

        private void GvShipper_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvShipper.Rows[e.RowIndex];
            TextBox txtCompanyName = (TextBox)row.FindControl("txtCompanyname");
            TextBox txtPhone = (TextBox)row.FindControl("txtPhone");
            Label lblId = (Label)row.FindControl("lblId");

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Northwind;Data Source=219A-INST-2-18";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SHIPPER_UPDATE";
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = lblId.Text;
            cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = txtCompanyName.Text;
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = txtPhone.Text;

            cmd.ExecuteNonQuery();
            cn.Close();

            gvShipper.EditIndex = -1;
            GetAll();

        }

        private void GvShipper_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvShipper.EditIndex = -1;
            GetAll();
        }

        private void GetAll()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Northwind;Data Source=219A-INST-2-18";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SHIPPER_GETALL";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataSet ds = new DataSet();
            da.Fill(ds);
            cn.Close();

            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            
            dt.Rows.Add(dr);


            this.gvShipper.DataSource = dt;
            this.gvShipper.DataBind();
        }
        private void GvShipper_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvShipper.EditIndex = e.NewEditIndex;
            GetAll();
        }
    }
}