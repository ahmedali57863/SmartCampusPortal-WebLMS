using System;
using System.Linq;

namespace SmartCampusPortal
{
    public partial class StudentAnnouncements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || Session["Role"] == null || Session["Role"].ToString() != "Student")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadAnnouncements();
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
        private void LoadAnnouncements()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var user = db.Users.FirstOrDefault(u => u.Username == Session["Username"].ToString());
            if (user != null)
            {
                var courseIds = db.Registrations.Where(r => r.UserId == user.Id).Select(r => r.CourseId).ToList();
                var announcements = from a in db.Announcements
                                    join c in db.Courses on a.CourseId equals c.Id
                                    where courseIds.Contains(a.CourseId)
                                    select new { c.CourseName, a.Message, a.DatePosted };
                gvAnnouncements.DataSource = announcements.ToList();
                gvAnnouncements.DataBind();
            }
        }
    }
}