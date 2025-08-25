using System;
using System.Linq;

namespace SmartCampusPortal
{
    public partial class EditProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || Session["Role"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadProfile();
            }
        }

        private void LoadProfile()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var user = db.Users.FirstOrDefault(u => u.Username == Session["Username"].ToString());
            if (user != null)
            {
                txtUsername.Text = user.Username;
                txtPassword.Text = user.Password;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var user = db.Users.FirstOrDefault(u => u.Username == Session["Username"].ToString());
            if (user != null)
            {
                // Check if current password matches
                if (txtPassword.Text != user.Password)
                {
                    lblMsg.Text = "Current password is incorrect!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Update password if new password is provided
                if (!string.IsNullOrWhiteSpace(txtNewPassword.Text))
                {
                    user.Password = txtNewPassword.Text;
                }

                db.SubmitChanges();
                lblMsg.Text = "Profile updated!";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
        }
        protected void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            // Redirect based on role
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
    }
}