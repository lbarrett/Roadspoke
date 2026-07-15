<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Query.aspx.cs" Inherits="Query" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" Width="400" Height="400">
    
    </asp:TextBox>
    <asp:Button ID="btnquery" runat="server" Text="query"  OnClick="btnquery_Click"/>
    <asp:Button ID="btnnonquery" runat="server" Text="nonquery" OnClick="btnnonquery_Click" />
    <asp:Button ID="Button1" runat="server" Text="newjob" OnClick="btnnonquery2_Click" />
    <asp:GridView ID="grd" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>
