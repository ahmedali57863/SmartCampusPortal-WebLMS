<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestUser.aspx.cs" Inherits="SmartCampusPortal.TestUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtUsername" runat="server" Placeholder="Username"></asp:TextBox><br />
            <asp:TextBox ID="txtPassword" runat="server" Placeholder="Password"></asp:TextBox><br />
            <asp:TextBox ID="txtRole" runat="server" Placeholder="Role"></asp:TextBox><br />
            <asp:Button ID="btnAddUser" runat="server" Text="Add User" OnClick="btnAddUser_Click" />
        </div>
    </form>
</body>
</html>