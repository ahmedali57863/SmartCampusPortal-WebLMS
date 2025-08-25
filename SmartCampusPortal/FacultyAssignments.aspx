<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FacultyAssignments.aspx.cs" Inherits="SmartCampusPortal.FacultyAssignments" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Faculty Assignments</title>
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
            <h2 class="section-title">Upload Assignment</h2>
            <div class="mb-3">
                <asp:DropDownList ID="ddlCourses" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Placeholder="Assignment Title"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" Placeholder="Description"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control" Placeholder="Due Date (yyyy-mm-dd)"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:FileUpload ID="fuAssignment" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <asp:Button ID="btnUpload" runat="server" Text="Upload Assignment" CssClass="btn btn-primary" OnClick="btnUpload_Click" />
            </div>
            <asp:Label ID="lblMsg" runat="server" ForeColor="Green"></asp:Label>
            <hr />
            <h3>Your Uploaded Assignments</h3>
            <asp:GridView ID="gvAssignments" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                <Columns>
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:HyperLinkField DataTextField="FilePath" HeaderText="File" DataNavigateUrlFields="FilePath" Target="_blank" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>