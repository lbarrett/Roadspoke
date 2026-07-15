<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Roadspoke |  Login Area</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.5 -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
    <!-- AdminLTE Skins. We have chosen the skin-blue for this starter
          page. However, you can choose any other skin. Make sure you
          apply the skin class to the body tag so the changes take effect.
    -->
    <link rel="stylesheet" href="dist/css/skins/skin-blue.min.css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>

<body>
    <form id="form1" runat="server">
     <div class="col-md-6" >
              <div class="box box-info">
                <div class="box-header with-border">
                  <h1 class="box-title">Login to your account</h1>
                </div><!-- /.box-header -->
                <!-- form start -->
                <div>
                 <span> <asp:Literal ID="ltrMsg" Text="" runat="server" /></span>
                </div>
                <form>
                  <div class="box-body">
                    <div class="form-group">
                        <asp:Label ID="lblEmail" runat="server" Text="UserName" class="col-sm-2 control-label"></asp:Label>
                      <div class="col-sm-10">
                          <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Enter UserName"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="rfv" runat="server" ErrorMessage="Email is Required" ControlToValidate="txtEmail" ValidationGroup="Save"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="form-group">
                      <asp:Label ID="lblPassword" runat="server" Text="Password" class="col-sm-2 control-label"></asp:Label>
                      <div class="col-sm-10">
                   <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="rfvaPassword" runat="server" ErrorMessage="Password is Required" ControlToValidate="txtPassword" ValidationGroup="Save"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                  <%--  <div class="form-group">
                      <div class="col-sm-offset-2 col-sm-10">
                        <div class="checkbox">
                          <label>
                            <input type="checkbox"> Remember me
                          </label>
                        </div>
                      </div>
                    </div>--%>
                  </div><!-- /.box-body -->
                  <div class="box-footer">
 
                      <asp:Button ID="btnLogin" runat="server" Text="Sign in" ValidationGroup="Save" OnClick="BtnLogin_Click" class="btn btn-info pull-right"/>
                  </div><!-- /.box-footer -->
                </form>
              </div>
    </form>
</body>
</html>
