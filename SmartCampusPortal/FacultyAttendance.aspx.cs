using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace SmartCampusPortal
{
    public partial class FacultyAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || Session["Role"] == null || Session["Role"].ToString() != "Faculty")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadCourses();
            }
        }
        protected void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("FacultyDashboard.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
        private void LoadCourses()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            ddlCourses.DataSource = db.Courses.ToList();
            ddlCourses.DataTextField = "CourseName";
            ddlCourses.DataValueField = "Id";
            ddlCourses.DataBind();
        }

        protected void btnLoadStudents_Click(object sender, EventArgs e)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            int courseId = int.Parse(ddlCourses.SelectedValue);
            var students = from r in db.Registrations
                           join u in db.Users on r.UserId equals u.Id
                           where r.CourseId == courseId && u.Role == "Student"
                           select new { u.Id, u.Username };

            gvStudents.DataSource = students.ToList();
            gvStudents.DataBind();
        }

        protected void btnSaveAttendance_Click(object sender, EventArgs e)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            int courseId = int.Parse(ddlCourses.SelectedValue);
            DateTime date = DateTime.Parse(txtDate.Text);

            for (int i = 0; i < gvStudents.Rows.Count; i++)
            {
                string username = gvStudents.Rows[i].Cells[0].Text;
                DropDownList ddlStatus = (DropDownList)gvStudents.Rows[i].FindControl("ddlStatus");
                string status = ddlStatus.SelectedValue;

                var student = db.Users.FirstOrDefault(u => u.Username == username);
                if (student != null)
                {
                    // Prevent duplicate attendance for the same date
                    bool alreadyMarked = db.Attendances.Any(a => a.CourseId == courseId && a.StudentId == student.Id && a.Date == date);
                    if (!alreadyMarked)
                    {
                        Attendance att = new Attendance();
                        att.CourseId = courseId;
                        att.StudentId = student.Id;
                        att.Date = date;
                        att.Status = status;
                        db.Attendances.InsertOnSubmit(att);
                    }
                }
            }
            db.SubmitChanges();
            lblMsg.Text = "Attendance saved!";
        }
    }
}