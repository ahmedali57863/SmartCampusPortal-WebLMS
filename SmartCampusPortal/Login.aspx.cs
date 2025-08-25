using System;
using System.Linq;
using System.Configuration;

namespace SmartCampusPortal
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var user = db.Users.FirstOrDefault(u => u.Username == txtUsername.Text && u.Password == txtPassword.Text);

            if (user != null)
            {
                Session["Username"] = user.Username;
                Session["Role"] = user.Role;

                // Redirect based on role
                if (user.Role == "Student")
                    Response.Redirect("StudentDashboard.aspx");
                else if (user.Role == "Faculty")
                    Response.Redirect("FacultyDashboard.aspx");
                else if (user.Role == "Admin")
                    Response.Redirect("AdminDashboard.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid username or password!";
            }
        }
    }
}