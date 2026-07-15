<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignIn.aspx.cs" Inherits="SignIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
  <center>
    <div style="padding-top: 200px; ">
      <asp:Literal ID="ltrMsg" runat="server"></asp:Literal>
      <br>
        <asp:Label ID="lblPassword" runat="server" Text="Please enter Password:"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
        
        <div style="padding-top: 30px; padding-left: 75px;">
        <asp:Button ID="btnLogin" runat="server" Text="Go To Roadspoke Website" OnClick="Login_Click" />
        </div>

    </div>
    </center>
    </form>
</body>
</html>
