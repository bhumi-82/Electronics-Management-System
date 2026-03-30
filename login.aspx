<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Electronicsmgtsystem_071.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
           <h2> Customer Login Form</h2>
        <div>
            <asp:Label ID="lbl_email" runat="server" Text="Email :"></asp:Label>
        &nbsp;<asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
            <br ><br>
            <asp:Label ID="lbl_pass" runat="server" Text="Password :"></asp:Label>
&nbsp;<asp:TextBox ID="txt_pass" runat="server"></asp:TextBox>
            <br >
            <br >
            <asp:Button ID="btn_login" runat="server" Text="Login" OnClick="btn_login_Click" />
            <br>
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
            </center>
    </form>
</body>
</html>
