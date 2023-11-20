using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class spx : System.Web.UI.Page
    {
        AdminDataContext dbcon;
        protected void Page_Load(object sender, EventArgs e)
        {
            string connstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ekyly\\OneDrive\\Desktop\\karate\\WebApplication1\\App_Data\\KarateData.mdf;Integrated Security=True;Connect Timeout=30";
            dbcon= new AdminDataContext(connstring);

            //select all members
            var result = from item in dbcon.Members
                         orderby item.LName, item.FName
                         select item;

            //gridview

            MembersGridView.DataSource = result;
            MembersGridView.databind;
        }
    }
}