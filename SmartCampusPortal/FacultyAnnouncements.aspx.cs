using System;
using System.Linq;

namespace SmartCampusPortal
{
    public partial class FacultyAnnouncements : System.Web.UI.Page
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
                LoadAnnouncements();
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

            // For simplicity, show all courses. You can filter by faculty if you have that info.
            ddlCourses.DataSource = db.Courses.ToList();
            ddlCourses.DataTextField = "CourseName";
            ddlCourses.DataValueField = "Id";
            ddlCourses.DataBind();
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var faculty = db.Users.FirstOrDefault(u => u.Username == Session["Username"].ToString());
            if (faculty != null)
            {
                Announcement ann = new Announcement();
                ann.CourseId = int.Parse(ddlCourses.SelectedValue);
                ann.FacultyId = faculty.Id;
                ann.Message = txtMessage.Text;
                ann.DatePosted = DateTime.Now;

                db.Announcements.InsertOnSubmit(ann);
                db.SubmitChanges();
                lblMsg.Text = "Announcement posted!";
                LoadAnnouncements();
            }
        }

        private void LoadAnnouncements()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var announcements = from a in db.Announcements
                                join c in db.Courses on a.CourseId equals c.Id
                                select new { c.CourseName, a.Message, a.DatePosted };
            gvAnnouncements.DataSource = announcements.ToList();
            gvAnnouncements.DataBind();
        }
    }
}