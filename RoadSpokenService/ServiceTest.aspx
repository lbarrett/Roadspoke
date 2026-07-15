<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceTest.aspx.cs" Inherits="RoadSpokenService.ServiceTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <div>
        <br />
        <br />
        <hr />
        <table>
            <tr>
                <td>
                    &nbsp;Method Name :
                    <asp:DropDownList ID="ddlMethodName" runat="server" Width="374px" Height="23px">
                        <asp:ListItem Text="Login" Value="Login"></asp:ListItem>
                        <asp:ListItem Text="RegisterUser" Value="RegisterUser"></asp:ListItem>
                         <asp:ListItem Text="GetRoutePointList" Value="GetRoutePointList"></asp:ListItem>
                         <asp:ListItem Text="GetRegionByRoutePointList" Value="GetRegionByRoutePointList"></asp:ListItem>
                         <asp:ListItem Text="GetNearByRoutePointList" Value="GetNearByRoutePointList"></asp:ListItem>
                         <asp:ListItem Text="GetAllRegion" Value="GetAllRegion"></asp:ListItem>
                          <asp:ListItem Text="GetAllRoutePointsByHighway" Value="GetAllRoutePointsByHighway"></asp:ListItem>
                           <asp:ListItem Text="GetAllPartners" Value="GetAllPartners"></asp:ListItem>
                            <asp:ListItem Text="GetAllHighways" Value="GetAllHighways"></asp:ListItem>
                            <asp:ListItem Text="ForgotPassword" Value="ForgotPassword"></asp:ListItem>
                            <asp:ListItem Text="GetLastUpdatetimeStamp" Value="GetLastUpdatetimeStamp"></asp:ListItem>


                             <asp:ListItem Text="GetAdList" Value="GetAdList"></asp:ListItem>
                              <asp:ListItem Text="GetNearByAdList" Value="GetNearByAdList"></asp:ListItem>
                               <asp:ListItem Text="GetAllAdByHighway" Value="GetAllAdByHighway"></asp:ListItem>
                                <asp:ListItem Text="GetRegionByAdList" Value="GetRegionByAdList"></asp:ListItem>
                    </asp:DropDownList>
                  
                </td>
                <td class="style1">
                    <asp:RadioButtonList ID="rbtlist" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="JSON" Value="json" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                          <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="DirectInvokeService"
                        UseSubmitBehavior="true" />
                        
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
 
       <asp:TextBox ID="txtpostdata" runat="server" TextMode="MultiLine" width="700px" Height="400px"></asp:TextBox>
    </form>
</body>
</html>
