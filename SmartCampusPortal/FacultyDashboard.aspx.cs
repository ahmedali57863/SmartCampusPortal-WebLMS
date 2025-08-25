using System;
using System.Web.UI;

namespace SmartCampusPortal
{
    public partial class FacultyDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is logged in and is a Faculty
            if (Session["Username"] == null || Session["Role"] == null || Session["Role"].ToString() != "Faculty")
            {
                Response.Redirect("Login.aspx");
            }
            lblUsername.Text = Session["Username"].ToString();
            lblRole.Text = Session["Role"].ToString();
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
    }
}