<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FacultyGrading.aspx.cs" Inherits="SmartCampusPortal.FacultyGrading" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Grade Submissions</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background: linear-gradient(135deg, rgb(0, 210, 255) 0%, rgb(255, 175, 123) 100%);
            min-height: 100vh;
        }
        .main-card {
            max-width: 900px;
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
            <h2 class="section-title">Grade Student Submissions</h2>
            <asp:GridView ID="gvSubmissions" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowCommand="gvSubmissions_RowCommand">
                <Columns>
                    <asp:BoundField DataField="AssignmentTitle" HeaderText="Assignment" />
                    <asp:BoundField DataField="StudentName" HeaderText="Student" />
                    <asp:HyperLinkField DataTextField="SubmissionFilePath" HeaderText="Submission File" DataNavigateUrlFields="SubmissionFilePath" Text="Download" Target="_blank" />
                    <asp:BoundField DataField="Grade" HeaderText="Grade" />
                    <asp:TemplateField HeaderText="Enter Grade">
                        <ItemTemplate>
                            <asp:TextBox ID="txtGrade" runat="server" Width="50px" Text='<%# Bind("Grade") %>' CssClass="form-control d-inline-block" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-sm ms-2" CommandName="SaveGrade" CommandArgument='<%# Eval("SubmissionId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblMsg" runat="server" ForeColor="Green"></asp:Label>
        </div>
    </form>
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>