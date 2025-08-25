using System;
using System.Linq;

namespace SmartCampusPortal
{
    public partial class StudentFees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || Session["Role"] == null || Session["Role"].ToString() != "Student")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadFees();
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
        private void LoadFees()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var user = db.Users.FirstOrDefault(u => u.Username == Session["Username"].ToString());
            if (user != null)
            {
                var fees = db.Fees.Where(f => f.StudentId == user.Id).ToList();
                gvFees.DataSource = fees;
                gvFees.DataBind();
            }
        }
    }
}