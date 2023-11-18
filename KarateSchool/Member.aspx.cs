using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateSchool
{
    public partial class Member : System.Web.UI.Page
    {
        //Creates Connection to the Database
        string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\w_m_j\\OneDrive\\Desktop\\Testing\\KarateSchool\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        KarateSchoolDataContext dbcon;

        protected void Page_Load(object sender, EventArgs e)
        {

            dbcon = new KarateSchoolDataContext(connString);

            //TEMPORARY LOGGED IN MEMBER TIL PAGE IS COMPLETE
            int loggedInMemberID = 6;

            //Selects the first name of the logged in member
            var firstName = (from x in dbcon.Members where x.Member_UserID == loggedInMemberID select x.MemberFirstName).SingleOrDefault();

            //Sets the Label to the First Name of the Logged in User
            lbl_FirstName.Text = firstName.ToString();


            //Selects the Last Name of the Logged In Member
            var lastName = (from x in dbcon.Members where x.Member_UserID == loggedInMemberID select x.MemberLastName).SingleOrDefault();

            //Sets the Label to the Last Name of the Logged In User
            lbl_LastName.Text = lastName.ToString();

            var result = (from s in dbcon.Sections
                          join i in dbcon.Instructors on s.Instructor_ID equals i.InstructorID
                          join m in dbcon.Members on s.Member_ID equals m.Member_UserID
                          join u in dbcon.NetUsers on m.Member_UserID equals u.UserID
                          where u.UserID == loggedInMemberID
                          select new
                          {
                              s.SectionName,
                              i.InstructorFirstName,
                              i.InstructorLastName,
                              s.SectionFee,
                              s.SectionStartDate
                          }).ToList(); // Retrieve data from the database

            var formattedResult = result.Select(r => new
            {
                Section = r.SectionName,
                Instructor_Name = r.InstructorFirstName,
                Last_Name = r.InstructorLastName,
                Cost = r.SectionFee.ToString("C"), // Format SectionFee as currency
                Date = r.SectionStartDate.ToShortDateString() // Format SectionStartDate as short date string
            }).ToList();

            GridView1.DataSource = formattedResult;
            GridView1.DataBind();

        }
    }
}