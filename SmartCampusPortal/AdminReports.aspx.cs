using System;
using System.Linq;

namespace SmartCampusPortal
{
    public partial class AdminReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadSummary();
                LoadCourseStats();
            }
        }

        private void LoadSummary()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            lblTotalStudents.Text = db.Users.Count(u => u.Role == "Student").ToString();
            lblTotalFaculty.Text = db.Users.Count(u => u.Role == "Faculty").ToString();
            lblTotalCourses.Text = db.Courses.Count().ToString();
            lblTotalFees.Text = db.Fees.Where(f => f.Status == "Paid").Sum(f => (decimal?)f.Amount).GetValueOrDefault(0).ToString("C");
        }
        protected void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminDashboard.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
        private void LoadCourseStats()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var stats = from c in db.Courses
                        join r in db.Registrations on c.Id equals r.CourseId into reg
                        select new
                        {
                            CourseName = c.CourseName,
                            StudentCount = reg.Count()
                        };
            gvCourseStats.DataSource = stats.ToList();
            gvCourseStats.DataBind();
        }
    }
}