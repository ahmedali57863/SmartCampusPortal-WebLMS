using System;
using System.Linq;

namespace SmartCampusPortal
{
    public partial class StudentCourseRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || Session["Role"] == null || Session["Role"].ToString() != "Student")
            {
                Response.Redirect("Login.aspx");
            }
            lblUsername.Text = Session["Username"].ToString();

            if (!IsPostBack)
            {
                LoadCourses();
                LoadRegisteredCourses();
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
        private void LoadCourses()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);
            gvCourses.DataSource = db.Courses.ToList();
            gvCourses.DataBind();
        }

        private void LoadRegisteredCourses()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var user = db.Users.FirstOrDefault(u => u.Username == Session["Username"].ToString());
            if (user != null)
            {
                var registered = from r in db.Registrations
                                 join c in db.Courses on r.CourseId equals c.Id
                                 where r.UserId == user.Id
                                 select new { c.CourseName, c.Credits };
                gvRegistered.DataSource = registered.ToList();
                gvRegistered.DataBind();
            }
        }

        protected void gvCourses_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Register")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int courseId = Convert.ToInt32(gvCourses.Rows[rowIndex].Cells[0].Text);

                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
                CampusDBDataContext db = new CampusDBDataContext(connStr);

                var user = db.Users.FirstOrDefault(u => u.Username == Session["Username"].ToString());
                if (user != null)
                {
                    // Check if already registered
                    bool alreadyRegistered = db.Registrations.Any(r => r.UserId == user.Id && r.CourseId == courseId);
                    if (alreadyRegistered)
                    {
                        lblMsg.Text = "Already registered for this course!";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    Registration reg = new Registration();
                    reg.UserId = user.Id;
                    reg.CourseId = courseId;
                    db.Registrations.InsertOnSubmit(reg);
                    db.SubmitChanges();
                    lblMsg.Text = "Registered successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    LoadRegisteredCourses();
                }
            }
        }
    }
}