<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminFees.aspx.cs" Inherits="SmartCampusPortal.AdminFees" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fee Management</title>
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
            <h2 class="section-title">Fee Management</h2>
            <h4>Add Fee</h4>
            <div class="row mb-3">
                <div class="col-md-3 mb-2">
                    <asp:DropDownList ID="ddlStudents" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="col-md-2 mb-2">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" Placeholder="Amount"></asp:TextBox>
                </div>
                <div class="col-md-3 mb-2">
                    <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control" Placeholder="Due Date (yyyy-mm-dd)"></asp:TextBox>
                </div>
                <div class="col-md-2 mb-2">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Unpaid" Value="Unpaid" />
                        <asp:ListItem Text="Paid" Value="Paid" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 mb-2">
                    <asp:Button ID="btnAddFee" runat="server" Text="Add Fee" CssClass="btn btn-success w-100" OnClick="btnAddFee_Click" />
                </div>
            </div>
            <asp:Label ID="lblMsg" runat="server" ForeColor="Green"></asp:Label>
            <hr />
            <h4>All Fees</h4>
            <asp:GridView ID="gvAllFees" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                OnRowCommand="gvAllFees_RowCommand" OnRowDataBound="gvAllFees_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="FeeId" HeaderText="Fee ID" ReadOnly="True" />
                    <asp:BoundField DataField="StudentName" HeaderText="Student" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Unpaid" Value="Unpaid" />
                                <asp:ListItem Text="Paid" Value="Paid" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary btn-sm" CommandName="UpdateFee" CommandArgument='<%# Eval("FeeId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>