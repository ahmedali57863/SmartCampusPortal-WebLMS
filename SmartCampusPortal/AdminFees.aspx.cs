using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartCampusPortal
{
    public partial class AdminFees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadStudents();
                LoadAllFees();
            }
        }

        private void LoadStudents()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var students = db.Users.Where(u => u.Role == "Student").ToList();
            ddlStudents.DataSource = students;
            ddlStudents.DataTextField = "Username";
            ddlStudents.DataValueField = "Id";
            ddlStudents.DataBind();
        }

        protected void btnAddFee_Click(object sender, EventArgs e)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            Fee fee = new Fee();
            fee.StudentId = int.Parse(ddlStudents.SelectedValue);
            fee.Amount = decimal.Parse(txtAmount.Text);
            fee.DueDate = DateTime.Parse(txtDueDate.Text);
            fee.Status = ddlStatus.SelectedValue;

            db.Fees.InsertOnSubmit(fee);
            db.SubmitChanges();
            lblMsg.Text = "Fee added!";
            LoadAllFees();
        }

        private void LoadAllFees()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
            CampusDBDataContext db = new CampusDBDataContext(connStr);

            var allFees = from f in db.Fees
                          join s in db.Users on f.StudentId equals s.Id
                          select new { FeeId = f.Id, StudentName = s.Username, f.Amount, f.DueDate, f.Status };
            gvAllFees.DataSource = allFees.ToList();
            gvAllFees.DataBind();
        }

        protected void gvAllFees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateFee")
            {
                int feeId = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
                string newStatus = ddlStatus.SelectedValue;

                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDBConnectionString"].ConnectionString;
                CampusDBDataContext db = new CampusDBDataContext(connStr);

                var fee = db.Fees.FirstOrDefault(f => f.Id == feeId);
                if (fee != null)
                {
                    fee.Status = newStatus;
                    db.SubmitChanges();
                    lblMsg.Text = "Fee status updated!";
                }
                LoadAllFees();
            }
        }

        protected void btnBackToDashboard_Click(object sender, EventArgs e)
        {
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
        protected void gvAllFees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                if (ddlStatus != null)
                {
                    string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                    ddlStatus.SelectedValue = status;
                }
            }
        }
    }
}