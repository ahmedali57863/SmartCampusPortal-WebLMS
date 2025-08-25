<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="SmartCampusPortal.AdminDashboard" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Dashboard</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background: linear-gradient(135deg, rgb(0, 210, 255) 0%, rgb(255, 175, 123) 100%);
            min-height: 100vh;
        }
        .main-card {
            max-width: 1100px;
            margin: 40px auto;
            border-radius: 1rem;
            box-shadow: 0 4px 24px rgba(0,0,0,0.10);
            background: #fff;
            padding: 2rem 2.5rem;
        }
        .section-title {
            color: rgb(0, 210, 255);
            font-weight: bold;
            margin-bottom: 1.5rem;
        }
        .dashboard-btns .btn {
            margin-right: 10px;
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navbar -->
        <nav class="navbar navbar-expand-lg navbar-dark bg-dark mb-4">
            <div class="container-fluid">
                <a class="navbar-brand" href="#">Smart Campus Portal</a>
                <div>
                    <asp:Button ID="btnBackToDashboard" runat="server" Text="Back to Dashboard" CssClass="btn btn-outline-light me-2" OnClick="btnBackToDashboard_Click" />
                    <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-danger" OnClick="btnLogout_Click" />
                </div>
            </div>
        </nav>
        <div class="main-card">
            <h2 class="section-title">Admin Dashboard</h2>
            <h5>
                Welcome, <asp:Label ID="lblUsername" runat="server" />
                <br />
                Your role: <asp:Label ID="lblRole" runat="server" />
            </h5>
            <div class="mb-4">
                <asp:HyperLink ID="lnkAdminFees" runat="server" NavigateUrl="AdminFees.aspx" CssClass="btn btn-primary me-2 mb-2">Fee Management & Reports</asp:HyperLink>
                <asp:HyperLink ID="lnkAdminReports" runat="server" NavigateUrl="AdminReports.aspx" CssClass="btn btn-secondary mb-2">View Reports</asp:HyperLink>
            </div>
            <h3>Add New User</h3>
            <div class="row mb-3">
                <div class="col-md-3 mb-2">
                    <asp:TextBox ID="txtNewUsername" runat="server" CssClass="form-control" Placeholder="Username"></asp:TextBox>
                </div>
                <div class="col-md-3 mb-2">
                    <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" Placeholder="Password"></asp:TextBox>
                </div>
                <div class="col-md-3 mb-2">
                    <asp:DropDownList ID="ddlNewRole" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Student" Value="Student" />
                        <asp:ListItem Text="Faculty" Value="Faculty" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-3 mb-2">
                    <asp:Button ID="btnAddUser" runat="server" Text="Add User" CssClass="btn btn-success w-100" OnClick="btnAddUser_Click" />
                </div>
            </div>
            <asp:Label ID="lblAddUserMsg" runat="server" ForeColor="Green"></asp:Label>
            <hr />
            <h3>All Users</h3>
            <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-striped" OnRowDeleting="gvUsers_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Username" HeaderText="Username" />
                    <asp:BoundField DataField="Role" HeaderText="Role" />
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <hr />
            <h3>Add New Course</h3>
            <div class="row mb-3">
                <div class="col-md-6 mb-2">
                    <asp:TextBox ID="txtCourseName" runat="server" CssClass="form-control" Placeholder="Course Name"></asp:TextBox>
                </div>
                <div class="col-md-3 mb-2">
                    <asp:TextBox ID="txtCredits" runat="server" CssClass="form-control" Placeholder="Credits"></asp:TextBox>
                </div>
                <div class="col-md-3 mb-2">
                    <asp:Button ID="btnAddCourse" runat="server" Text="Add Course" CssClass="btn btn-success w-100" OnClick="btnAddCourse_Click" />
                </div>
            </div>
            <asp:Label ID="lblAddCourseMsg" runat="server" ForeColor="Green"></asp:Label>
            <hr />
            <h3>All Courses</h3>
            <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-striped" OnRowDeleting="gvCourses_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                    <asp:BoundField DataField="Credits" HeaderText="Credits" />
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>