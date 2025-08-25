using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartCampusPortal
{
    public partial class TestUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
 
protected void btnAddUser_Click(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
        CampusDBDataContext db = new CampusDBDataContext(connStr);

        User u = new User();
        u.Username = txtUsername.Text;
        u.Password = txtPassword.Text;
        u.Role = txtRole.Text;
        db.Users.InsertOnSubmit(u);
        db.SubmitChanges();
        Response.Write("User added!");
    }
}
}