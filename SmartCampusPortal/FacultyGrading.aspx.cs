using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace SmartCampusPortal
{
    public partial class FacultyGrading : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || Session["Role"] == null || Session["Role"].ToString() != "Faculty")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadSubmissions();
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
        private void LoadSubmissions()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            // For simplicity, show all submissions for all assignments.
            // If you want to filter by faculty's courses, you need a FacultyCourses table.
            var submissions = from s in db.Submissions
                              join a in db.Assignments on s.AssignmentId equals a.Id
                              join u in db.Users on s.UserId equals u.Id
                              select new
                              {
                                  SubmissionId = s.Id,
                                  AssignmentTitle = a.Title,
                                  StudentName = u.Username,
                                  s.SubmissionFilePath,
                                  s.Grade
                              };
            gvSubmissions.DataSource = submissions.ToList();
            gvSubmissions.DataBind();
        }

        protected void gvSubmissions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SaveGrade")
            {
                int submissionId = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                TextBox txtGrade = (TextBox)row.FindControl("txtGrade");
                string grade = txtGrade.Text;

                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
                CampusDBDataContext db = new CampusDBDataContext(connStr);

                var submission = db.Submissions.FirstOrDefault(s => s.Id == submissionId);
                if (submission != null)
                {
                    submission.Grade = grade;
                    db.SubmitChanges();
                    lblMsg.Text = "Grade saved!";
                }
                LoadSubmissions();
            }
        }
    }
}