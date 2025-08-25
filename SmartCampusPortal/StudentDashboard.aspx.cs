using System;
using System.Web.UI;

namespace SmartCampusPortal
{

    public partial class StudentDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is logged in and is a Student
            if (Session["Username"] == null || Session["Role"] == null || Session["Role"].ToString() != "Student")
            {
                Response.Redirect("Login.aspx");
            }
            // Set the labels to show the username and role
            lblUsername.Text = Session["Username"].ToString();
            lblRole.Text = Session["Role"].ToString();
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
    }

}