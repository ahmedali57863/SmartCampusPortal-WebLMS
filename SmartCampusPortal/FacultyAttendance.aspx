<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FacultyAttendance.aspx.cs" Inherits="SmartCampusPortal.FacultyAttendance" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mark Attendance</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background: linear-gradient(135deg, rgb(0, 210, 255) 0%, rgb(255, 175, 123) 100%);
            min-height: 100vh;
        }
        .main-card {
            max-width: 800px;
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
            <h2 class="section-title">Mark Attendance</h2>
            <div class="row mb-3">
                <div class="col-md-4 mb-2">
                    <asp:DropDownList ID="ddlCourses" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="col-md-4 mb-2">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" Placeholder="Date (yyyy-mm-dd)"></asp:TextBox>
                </div>
                <div class="col-md-4 mb-2">
                    <asp:Button ID="btnLoadStudents" runat="server" Text="Load Students" CssClass="btn btn-primary w-100" OnClick="btnLoadStudents_Click" />
                </div>
            </div>
            <hr />
            <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                <Columns>
                    <asp:BoundField DataField="Username" HeaderText="Student" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Present" Value="Present" />
                                <asp:ListItem Text="Absent" Value="Absent" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnSaveAttendance" runat="server" Text="Save Attendance" CssClass="btn btn-success mt-2" OnClick="btnSaveAttendance_Click" />
            <asp:Label ID="lblMsg" runat="server" ForeColor="Green" CssClass="ms-3"></asp:Label>
        </div>
    </form>
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>