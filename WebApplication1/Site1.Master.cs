using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
namespace WebApplication1
{

    public partial class Site1 : System.Web.UI.MasterPage
    {
        private SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ekyly\\OneDrive\\Desktop\\karate\\WebApplication1\\App_Data\\KarateData.mdf;Integrated Security=True;Connect Timeout=30");

        AdminDataContext dbcon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvbind();
            }
            string connstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ekyly\\OneDrive\\Desktop\\karate\\WebApplication1\\App_Data\\KarateData.mdf;Integrated Security=True;Connect Timeout=30";
            dbcon = new AdminDataContext(connstring);

            //select all members
            var result = from item in dbcon.Members
                         orderby item.LName, item.FName
                         select item;

            //gridview

            ctl00.DataSource = result;
            ctl00.DataBind();




            //select all instr
            var result2 = from item in dbcon.Instructors
                         orderby item.LName, item.FName
                         select item;

            //gridview

            GridView1.DataSource = result2;
            GridView1.DataBind();
        }

        protected void gvbind()
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from detail", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridView1.DataSource = ds;
                GridView1.DataBind();
                int columncount = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView1.Rows[0].Cells[0].Text = "No Records Found";
            }
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            Label lbldeleteid = (Label)row.FindControl("lblID");
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete FROM detail where id='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            gvbind();
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            gvbind();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            Label lblID = (Label)row.FindControl("lblID");
            //TextBox txtname=(TextBox)gr.cell[].control[];  
            TextBox textName = (TextBox)row.Cells[0].Controls[0];
            TextBox textadd = (TextBox)row.Cells[1].Controls[0];
            TextBox textc = (TextBox)row.Cells[2].Controls[0];
            //TextBox textadd = (TextBox)row.FindControl("txtadd");  
            //TextBox textc = (TextBox)row.FindControl("txtc");  
            GridView1.EditIndex = -1;
            conn.Open();
            //SqlCommand cmd = new SqlCommand("SELECT * FROM detail", conn);  
            SqlCommand cmd = new SqlCommand("update detail set name='" + textName.Text + "',address='" + textadd.Text + "',country='" + textc.Text + "'where id='" + userid + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            gvbind();
            //GridView1.DataBind();  
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            gvbind();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            gvbind();
        }
        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            String LastName = LastTxt.Text;

            string connstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ekyly\\OneDrive\\Desktop\\karate\\WebApplication1\\App_Data\\KarateData.mdf;Integrated Security=True;Connect Timeout=30";
            dbcon = new AdminDataContext(connstring);
            
            


            //select all
            var result = from item in dbcon.Members
                         where item.LName.Contains(LastName)
                         select item;

            //
            ctl00.DataSource= result;
            ctl00.DataBind();



            String LastName2 = LastTxt.Text;

            

            //select all
            var result2 = from item in dbcon.Instructors
                         where item.LName.Contains(LastName2)
                         select item;

            //
            GridView1.DataSource = result;
            GridView1.DataBind();
        }





        protected void gvbind2()
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from detail", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ctl00.DataSource = ds;
                ctl00.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                ctl00.DataSource = ds;
                ctl00.DataBind();
                int columncount = ctl00.Rows[0].Cells.Count;
                ctl00.Rows[0].Cells.Clear();
                ctl00.Rows[0].Cells.Add(new TableCell());
                ctl00.Rows[0].Cells[0].ColumnSpan = columncount;
                ctl00.Rows[0].Cells[0].Text = "No Records Found";
            }
        }
        protected void ctl00_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)ctl00.Rows[e.RowIndex];
            Label lbldeleteid = (Label)row.FindControl("lblID");
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete FROM detail where id='" + Convert.ToInt32(ctl00.DataKeys[e.RowIndex].Value.ToString()) + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            gvbind2();
        }
        protected void ctl00_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ctl00.EditIndex = e.NewEditIndex;
            gvbind2();
        }
        protected void ctl00_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int userid = Convert.ToInt32(ctl00.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)ctl00.Rows[e.RowIndex];
            Label lblID = (Label)row.FindControl("lblID");
            //TextBox txtname=(TextBox)gr.cell[].control[];  
            TextBox textName = (TextBox)row.Cells[0].Controls[0];
            TextBox textadd = (TextBox)row.Cells[1].Controls[0];
            TextBox textc = (TextBox)row.Cells[2].Controls[0];
            //TextBox textadd = (TextBox)row.FindControl("txtadd");  
            //TextBox textc = (TextBox)row.FindControl("txtc");  
            ctl00.EditIndex = -1;
            conn.Open();
            //SqlCommand cmd = new SqlCommand("SELECT * FROM detail", conn);  
            SqlCommand cmd = new SqlCommand("update detail set name='" + textName.Text + "',address='" + textadd.Text + "',country='" + textc.Text + "'where id='" + userid + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            gvbind2();
            //GridView1.DataBind();  
        }
        protected void ctl00_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ctl00.PageIndex = e.NewPageIndex;
            gvbind2();
        }
        protected void ctl00_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ctl00.EditIndex = -1;
            gvbind2();
        }
    }
}