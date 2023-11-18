using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateSchool
{
    public partial class Instructor1 : System.Web.UI.Page
    {
        //Creates Connection to the Database
        string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\w_m_j\\OneDrive\\Desktop\\Testing\\KarateSchool\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        KarateSchoolDataContext dbcon;

        protected void Page_Load(object sender, EventArgs e)
        {

            dbcon = new KarateSchoolDataContext(connString);

            //TEMPORARY LOGGED IN Instructor TIL PAGE IS COMPLETE
            int loggedInInstructor = 1;

            //Selects the first name of the logged in Instructor
            var firstName = (from x in dbcon.Instructors where x.InstructorID == loggedInInstructor select x.InstructorFirstName).SingleOrDefault();

            //Sets the Label to the First Name of the Logged in Instructor
            lbl_FirstName.Text = firstName.ToString();


            //Selects the Last Name of the Logged In Instructor
            var lastName = (from x in dbcon.Instructors where x.InstructorID == loggedInInstructor select x.InstructorLastName).SingleOrDefault();

            //Sets the Label to the Last Name of the Logged In Instructor
            lbl_LastName.Text = lastName.ToString();

            //Section Name      - Section table
            //Member First Name - Member Table
            //Member Last Name  - Member Table

            var result = from x in dbcon.Sections
                         join i in dbcon.Members on x.Member_ID equals i.Member_UserID
                         where x.Instructor_ID == loggedInInstructor
                         select new
                         {
                             x.SectionName,
                             i.MemberFirstName,
                             i.MemberLastName
                         };

            GridView1.DataSource = result;
            GridView1.DataBind();

        }
    }
}