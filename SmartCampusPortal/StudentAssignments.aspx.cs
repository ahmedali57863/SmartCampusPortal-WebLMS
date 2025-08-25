using System;
using System.Linq;

namespace SmartCampusPortal
{
    public partial class StudentAssignments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || Session["Role"] == null || Session["Role"].ToString() != "Student")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadAssignments();
                LoadSubmissions();
                LoadAssignmentDropdown();
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
        private void LoadAssignments()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var user = db.Users.FirstOrDefault(u => u.Username == Session["Username"].ToString());
            if (user != null)
            {
                var courseIds = db.Registrations.Where(r => r.UserId == user.Id).Select(r => r.CourseId).ToList();
                var assignments = db.Assignments.Where(a => courseIds.Contains(a.CourseId)).ToList();
                gvAssignments.DataSource = assignments;
                gvAssignments.DataBind();
            }
        }

        private void LoadAssignmentDropdown()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var user = db.Users.FirstOrDefault(u => u.Username == Session["Username"].ToString());
            if (user != null)
            {
                var courseIds = db.Registrations.Where(r => r.UserId == user.Id).Select(r => r.CourseId).ToList();
                var assignments = db.Assignments.Where(a => courseIds.Contains(a.CourseId)).ToList();
                ddlAssignments.DataSource = assignments;
                ddlAssignments.DataTextField = "Title";
                ddlAssignments.DataValueField = "Id";
                ddlAssignments.DataBind();
            }
        }

        private void LoadSubmissions()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var user = db.Users.FirstOrDefault(u => u.Username == Session["Username"].ToString());
            if (user != null)
            {
                var submissions = from s in db.Submissions
                                  join a in db.Assignments on s.AssignmentId equals a.Id
                                  where s.UserId == user.Id
                                  select new { a.Title, s.SubmissionText, s.Grade, s.SubmissionFilePath };
                gvSubmissions.DataSource = submissions.ToList();
                gvSubmissions.DataBind();
            }
        }

        protected void gvAssignments_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            // You can remove this if you use the file upload section below for submissions.
        }

        protected void btnSubmitFile_Click(object sender, EventArgs e)
        {
            if (!fuSubmission.HasFile)
            {
                lblSubmitMsg.Text = "Please select a file to upload.";
                lblSubmitMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string ext = System.IO.Path.GetExtension(fuSubmission.FileName).ToLower();
            if (ext != ".pdf" && ext != ".doc" && ext != ".docx")
            {
                lblSubmitMsg.Text = "Only PDF or Word files are allowed.";
                lblSubmitMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string fileName = Guid.NewGuid().ToString() + ext;
            string savePath = Server.MapPath("~/Submissions/") + fileName;
            fuSubmission.SaveAs(savePath);

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var user = db.Users.FirstOrDefault(u => u.Username == Session["Username"].ToString());
            int assignmentId = int.Parse(ddlAssignments.SelectedValue);

            if (user != null)
            {
                // Check if already submitted
                bool alreadySubmitted = db.Submissions.Any(s => s.UserId == user.Id && s.AssignmentId == assignmentId);
                if (alreadySubmitted)
                {
                    lblSubmitMsg.Text = "You have already submitted this assignment.";
                    lblSubmitMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                Submission sub = new Submission();
                sub.AssignmentId = assignmentId;
                sub.UserId = user.Id;
                sub.SubmissionFilePath = "~/Submissions/" + fileName;
                sub.SubmissionText = "File submitted by " + user.Username + " at " + DateTime.Now;
                db.Submissions.InsertOnSubmit(sub);
                db.SubmitChanges();
                lblSubmitMsg.Text = "File submitted!";
                lblSubmitMsg.ForeColor = System.Drawing.Color.Green;
                LoadSubmissions();
            }
        }
    }
}