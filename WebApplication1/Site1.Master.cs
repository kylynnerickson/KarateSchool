using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{

    public partial class Site1 : System.Web.UI.MasterPage
    {
        AdminDataContext dbcon;
        protected void Page_Load(object sender, EventArgs e)
        {
            string connstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ekyly\\OneDrive\\Desktop\\karate\\WebApplication1\\App_Data\\KarateData.mdf;Integrated Security=True;Connect Timeout=30";
            dbcon = new AdminDataContext(connstring);

            //select all members
            var result = from item in dbcon.Members
                         orderby item.LName, item.FName
                         select item;

            //gridview

            ctl00.DataSource = result;
            ctl00.DataBind();
           
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
        }
    }
}