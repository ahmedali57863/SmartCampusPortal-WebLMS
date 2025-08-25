using System;
using System.Linq;
using System.Web.UI;

namespace SmartCampusPortal
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }
            lblUsername.Text = Session["Username"].ToString();
            lblRole.Text = Session["Role"].ToString();

            if (!IsPostBack)
            {
                LoadUsers();
                LoadCourses();
            }
        }
        protected void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            // Redirect to the correct dashboard based on role
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();
                if (role == "Admin")
                    Response.Redirect("AdminDashboard.aspx");
                else if (role == "Faculty")
                    Response.Redirect("FacultyDashboard.aspx");
                else if (role == "Student")
                    Response.Redirect("StudentDashboard.aspx");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
        // User Management
        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            if (db.Users.Any(user => user.Username == txtNewUsername.Text))
            {
                lblAddUserMsg.Text = "Username already exists!";
                lblAddUserMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            User newUser = new User();
            newUser.Username = txtNewUsername.Text;
            newUser.Password = txtNewPassword.Text;
            newUser.Role = ddlNewRole.SelectedValue;
            db.Users.InsertOnSubmit(newUser);
            db.SubmitChanges();
            lblAddUserMsg.Text = "User added!";
            lblAddUserMsg.ForeColor = System.Drawing.Color.Green;
            LoadUsers();
        }

        private void LoadUsers()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);
            gvUsers.DataSource = db.Users.ToList();
            gvUsers.DataBind();
        }

        protected void gvUsers_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);
            var userToDelete = db.Users.SingleOrDefault(user => user.Id == userId);
            if (userToDelete != null)
            {
                db.Users.DeleteOnSubmit(userToDelete);
                db.SubmitChanges();
                LoadUsers();
            }
        }

        // Course Management
        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            if (db.Courses.Any(c => c.CourseName == txtCourseName.Text))
            {
                lblAddCourseMsg.Text = "Course already exists!";
                lblAddCourseMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            Course newCourse = new Course();
            newCourse.CourseName = txtCourseName.Text;
            newCourse.Credits = int.Parse(txtCredits.Text);
            db.Courses.InsertOnSubmit(newCourse);
            db.SubmitChanges();
            lblAddCourseMsg.Text = "Course added!";
            lblAddCourseMsg.ForeColor = System.Drawing.Color.Green;
            LoadCourses();
        }

        private void LoadCourses()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);
            gvCourses.DataSource = db.Courses.ToList();
            gvCourses.DataBind();
        }

        protected void gvCourses_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int courseId = Convert.ToInt32(gvCourses.DataKeys[e.RowIndex].Value);
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);
            var courseToDelete = db.Courses.SingleOrDefault(c => c.Id == courseId);
            if (courseToDelete != null)
            {
                db.Courses.DeleteOnSubmit(courseToDelete);
                db.SubmitChanges();
                LoadCourses();
            }
        }
    }
}