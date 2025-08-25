using System;
using System.Linq;

namespace SmartCampusPortal
{
    public partial class StudentAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || Session["Role"] == null || Session["Role"].ToString() != "Student")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadAttendance();
            }
        }
        protected void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentDashboard.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
        private void LoadAttendance()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var user = db.Users.FirstOrDefault(u => u.Username == Session["Username"].ToString());
            if (user != null)
            {
                var attendance = from a in db.Attendances
                                 join c in db.Courses on a.CourseId equals c.Id
                                 where a.StudentId == user.Id
                                 select new { c.CourseName, a.Date, a.Status };
                gvAttendance.DataSource = attendance.ToList();
                gvAttendance.DataBind();
            }
        }
    }
}