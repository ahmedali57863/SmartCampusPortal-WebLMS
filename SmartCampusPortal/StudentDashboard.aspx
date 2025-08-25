<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="SmartCampusPortal.StudentDashboard" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Dashboard</title>
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
            <h2 class="section-title">Student Dashboard</h2>
            <h5>
                Welcome, <asp:Label ID="lblUsername" runat="server" />
                <br />
                Your role: <asp:Label ID="lblRole" runat="server" />
            </h5>
            <div class="dashboard-btns mb-4">
                <asp:HyperLink ID="lnkRegisterCourses" runat="server" NavigateUrl="StudentCourseRegistration.aspx" CssClass="btn btn-primary">Register for Courses</asp:HyperLink>
                <asp:HyperLink ID="lnkAssignments" runat="server" NavigateUrl="StudentAssignments.aspx" CssClass="btn btn-secondary">Assignments & Submission</asp:HyperLink>
                <asp:HyperLink ID="lnkStudentAnnouncements" runat="server" NavigateUrl="StudentAnnouncements.aspx" CssClass="btn btn-info">Announcements</asp:HyperLink>
                <asp:HyperLink ID="lnkStudentAttendance" runat="server" NavigateUrl="StudentAttendance.aspx" CssClass="btn btn-success">My Attendance</asp:HyperLink>
                <asp:HyperLink ID="lnkStudentFees" runat="server" NavigateUrl="StudentFees.aspx" CssClass="btn btn-warning">My Fees</asp:HyperLink>
                <asp:HyperLink ID="lnkEditProfile" runat="server" NavigateUrl="EditProfile.aspx" CssClass="btn btn-dark">Edit Profile</asp:HyperLink>
            </div>
        </div>
    </form>
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>