<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="UserManager.aspx.cs" Inherits="UserManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<section class="content-header">
   <div>
         <div>
                <h1>
                    User
                </h1>
            </div>
        </div>
         <div  runat="server" id="dvError" visible="false">
            <div style="width: 100%; text-align: center;">
                <div>
                    <p>
                        <asp:Literal Text="" ID="ltrError" runat="server" />
                    </p>
                </div>
            </div>
            </div>
  </section>
 
 <section class="content">  
<div class="box box-primary" id="dvAddEdit" runat="server" visible="false">
                <div class="box-header with-border">
                  <h4 class="box-title">Add/Edit User</h4>
                </div><!-- /.box-header -->
                <!-- form start -->
                <form role="form">
                  <div class="box-body">
                    <div class="form-group">
                      <label class="col-sm-2 control-label">Role :</label>
                      <div id="divRole" class="col-sm-10">
                     <asp:DropDownList ID="ddlRole" runat="server"  class="form-control" style="width: 150px;">
                    <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Admin" Value="2"></asp:ListItem>
                    <asp:ListItem Text="User" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="rfvRole" runat="server" InitialValue="-1" ControlToValidate="ddlRole"  ErrorMessage="Please select the Role" ValidationGroup="Save">* </asp:RequiredFieldValidator>
                        <asp:HiddenField ID="hdnuserid" runat="server" Value="0" />
                    </div>
                    </div>
                    <div class="form-group">
                     <label class="col-sm-2 control-label"> Email :</label>
                     <div class="col-sm-10">
                          <asp:TextBox ID="txtEmail" runat="server"  class="form-control" placeholder="Enter email"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email is Required" ControlToValidate="txtEmail" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="ReqExpEmail" runat="server" ErrorMessage="Enter the valid Email_Id"
                      ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                      ValidationGroup="Save">*</asp:RegularExpressionValidator> 
                    </div>
                    </div>
                    <div class="form-group">
                      <label class="col-sm-2 control-label"> Password :</label>
                    <div class="col-sm-10">
                   <asp:TextBox ID="txtPassword" runat="server" ValidationGroup="Save" class="form-control" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                   <br/>
                  <%-- <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Password is Required" ControlToValidate="txtPassword" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </div>
                    </div>
                     <div class="form-group">
                      <label class="col-sm-2 control-label" >Confirm Password :</label>
                      <div class="col-sm-10">
                        <asp:TextBox ID="txtConfirmPassword" runat="server"  TextMode="Password" placeholder="Enter Confirm Password" class="form-control"></asp:TextBox>
                       <%--   <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="Confirm Password is Required" ControlToValidate="txtConfirmPassword" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                       <asp:CompareValidator ID="NewPasswordCompare" runat="server" ErrorMessage="The Confirm Password must match the New Password entry."
                       ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" ValidationGroup="Save">*</asp:CompareValidator>
                    </div>
                    </div>
              
                  </div><!-- /.box-body -->

                  <div class="box-footer">
                  <asp:Button ID="btnSave" Text="Save"  ValidationGroup="Save" runat="server" OnClick="btnSave_Click" class="btn btn-primary" TabIndex="11" /> 
                  <asp:Button ID="btnCancel" Text="Cancel"  ValidationGroup="Save" runat="server" OnClick="btnCancel_Click" class="btn btn-primary" TabIndex="11" CausesValidation="False" /> 
                   <asp:ValidationSummary ID="val" runat="server" ShowMessageBox="true"  ValidationGroup="Save" ShowSummary="false"/>
                  </div>
                </form>
              </div>

                    <div  id="dvUserList" runat="server">
                     <div id="tab_2">
                        <div ><asp:LinkButton ID="lnkAddUser" runat="server" OnClick="lnkAdd_Click">Add User</asp:LinkButton>
                        <br /><br />
                          <div >
                          <div>
                                    <h2>
                                        <i ></i><span >User List</span>&nbsp;
                                    </h2>
                                </div>
                             <div>
                                    <!--/row-->
                           <br>
                           <div >
                          <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="false"  Width="96%"  PageSize="10" AllowPaging="true" 
                          OnPageIndexChanging="gvUser_OnPageindexchanging"
                                  onrowcommand="gvUser_RowCommand">
                                  <RowStyle HorizontalAlign="Center" />
                          <Columns>
                         
                          <asp:TemplateField HeaderText="Email ID">
                          <ItemTemplate>
                          <%#Eval("Email")%>
                          </ItemTemplate>
                          </asp:TemplateField>
                           <asp:TemplateField HeaderText="User Password">
                          <ItemTemplate>    
                          <%#Eval("UserPassword")%>
                          </ItemTemplate>
                          </asp:TemplateField>
                           <asp:TemplateField HeaderText="User Role">
                          <ItemTemplate>
                          <%#Eval("RoleName")%>
                          </ItemTemplate>
                          </asp:TemplateField>
                           <asp:TemplateField HeaderText="Created Date">
                          <ItemTemplate>
                            <%#string.Format("{0:MM/dd/yy}", Eval("CreatedDate"))%>
                          </ItemTemplate>
                          </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                          <ItemTemplate>
                             <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("Id") %>' 
                            CommandName="EditItem"  Text="Edit"></asp:LinkButton>/
                          <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id") %>' 
                            CommandName="DeleteItem" OnClientClick="return confirm('Are you sure you want to delete this User?');" Text="Delete"></asp:LinkButton>
                          </ItemTemplate>
                          </asp:TemplateField>
                          </Columns>
                          </asp:GridView>
                          </div>
                          </div>
                          </div>
                        </div>
                        </div>
                </div>
</section>
</asp:Content>

