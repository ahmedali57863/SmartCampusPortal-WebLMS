using System;
using System.Linq;

namespace SmartCampusPortal
{
    public partial class FacultyAssignments : System.Web.UI.Page
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
                LoadAssignments();
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

            // For simplicity, show all courses. 
            // If you want to show only courses taught by this faculty, you need a FacultyCourses table.
            ddlCourses.DataSource = db.Courses.ToList();
            ddlCourses.DataTextField = "CourseName";
            ddlCourses.DataValueField = "Id";
            ddlCourses.DataBind();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!fuAssignment.HasFile)
            {
                lblMsg.Text = "Please select a file to upload.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Only allow PDF or Word files
            string ext = System.IO.Path.GetExtension(fuAssignment.FileName).ToLower();
            if (ext != ".pdf" && ext != ".doc" && ext != ".docx")
            {
                lblMsg.Text = "Only PDF or Word files are allowed.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Save file to server
            string fileName = Guid.NewGuid().ToString() + ext;
            string savePath = Server.MapPath("~/Assignments/") + fileName;
            fuAssignment.SaveAs(savePath);

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            Assignment a = new Assignment();
            a.CourseId = int.Parse(ddlCourses.SelectedValue);
            a.Title = txtTitle.Text;
            a.Description = txtDescription.Text;
            a.DueDate = DateTime.Parse(txtDueDate.Text);
            a.FilePath = "~/Assignments/" + fileName;

            db.Assignments.InsertOnSubmit(a);
            db.SubmitChanges();
            lblMsg.Text = "Assignment uploaded!";
            lblMsg.ForeColor = System.Drawing.Color.Green;
            LoadAssignments();
        }

        private void LoadAssignments()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            // Show all assignments uploaded for all courses (or filter by faculty if you have that info)
            gvAssignments.DataSource = db.Assignments.ToList();
            gvAssignments.DataBind();
        }
    }
}