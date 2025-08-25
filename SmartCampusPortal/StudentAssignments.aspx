<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentAssignments.aspx.cs" Inherits="SmartCampusPortal.StudentAssignments" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assignments</title>
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
            <h2 class="section-title">Assignments</h2>
            <asp:GridView ID="gvAssignments" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowCommand="gvAssignments_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:HyperLinkField DataTextField="FilePath" HeaderText="File" DataNavigateUrlFields="FilePath" Target="_blank" Text="Download" />
                    <asp:ButtonField ButtonType="Button" Text="Submit" CommandName="Submit" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblMsg" runat="server" ForeColor="Green"></asp:Label>
            <hr />
            <h3>Your Submissions</h3>
            <asp:GridView ID="gvSubmissions" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                <Columns>
                    <asp:BoundField DataField="Title" HeaderText="Assignment" />
                    <asp:BoundField DataField="SubmissionText" HeaderText="Your Submission" />
                    <asp:BoundField DataField="Grade" HeaderText="Grade" />
                    <asp:HyperLinkField DataTextField="SubmissionFilePath" HeaderText="Your File" DataNavigateUrlFields="SubmissionFilePath" Target="_blank" Text="Download" />
                </Columns>
            </asp:GridView>
            <hr />
            <h3>Submit Assignment File</h3>
            <div class="row mb-3">
                <div class="col-md-4 mb-2">
                    <asp:DropDownList ID="ddlAssignments" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="col-md-4 mb-2">
                    <asp:FileUpload ID="fuSubmission" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-4 mb-2">
                    <asp:Button ID="btnSubmitFile" runat="server" Text="Submit File" CssClass="btn btn-primary w-100" OnClick="btnSubmitFile_Click" />
                </div>
            </div>
            <asp:Label ID="lblSubmitMsg" runat="server" ForeColor="Green"></asp:Label>
        </div>
    </form>
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>