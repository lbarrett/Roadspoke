<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="PartnerManager.aspx.cs" Inherits="PartnerManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<section class="content-header">
   <div>
         <div>
                <h1>
                    Partner
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
                  <h4 class="box-title">Add/Edit Partner</h4>
                </div><!-- /.box-header -->
                <!-- form start -->
                <form role="form">
                  <div class="box-body">
                    
                    <div class="form-group">
                     <label class="col-sm-2 control-label"> Partner Name :</label>
                     <div class="col-sm-10">
                          <asp:TextBox ID="txtName" runat="server"  class="form-control" placeholder="Enter Name"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Name is Required" ControlToValidate="txtName" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                      <asp:HiddenField id="hdnuserid" runat="server" Value="0" />
                    </div>
                    </div>
                      <div class="form-group">
                     <label class="col-sm-2 control-label"> Partner Image :</label>
                     <div class="col-sm-10">
                        <asp:FileUpload ID="fl" runat="server" />
                    </div>
                    </div>
                      <div class="form-group">
                     <label class="col-sm-2 control-label"> Partner Name :</label>
                     <div class="col-sm-10">
                          <asp:TextBox ID="txtURL" runat="server"  class="form-control" placeholder="Enter URL"></asp:TextBox>
                        
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
                        <div ><asp:LinkButton ID="lnkAddUser" runat="server" OnClick="lnkAdd_Click">Add Partner</asp:LinkButton>
                        <br /><br />
                          <div >
                          <div>
                                    <h2>
                                        <i ></i><span >Partner</span>&nbsp;
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
                         
                      
                           <asp:TemplateField HeaderText="Partner Name">
                          <ItemTemplate>    
                          <%#Eval("PartnerName")%>
                          </ItemTemplate>
                          </asp:TemplateField>
                           <asp:TemplateField HeaderText="Partner Image">
                          <ItemTemplate> 
                           <ItemTemplate>
                                                <a href="<%#"FileUpload/"+ Eval("PartnerImage") %>" target="_blank">
                                                    <asp:Image ID="newsImage" runat="server" Height="100" Width="100" ImageUrl='<%#"FileUpload/"+ Eval("PartnerImage") %>' /></a>
                                            </ItemTemplate>   
                        
                          </ItemTemplate>
                          </asp:TemplateField>
                           <asp:TemplateField HeaderText="URL">
                          <ItemTemplate>    
                          <%#Eval("PartnerURL")%>
                          </ItemTemplate>
                          </asp:TemplateField>
                          
                           <asp:TemplateField HeaderText="Created Date">
                          <ItemTemplate>
                            <%#string.Format("{0:MM/dd/yy}", Eval("CreateDate"))%>
                          </ItemTemplate>
                          </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                          <ItemTemplate>
                             <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("Id") %>' 
                            CommandName="EditItem"  Text="Edit"></asp:LinkButton>/
                          <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id") %>' 
                            CommandName="DeleteItem" OnClientClick="return confirm('Are you sure you want to delete this partner?');" Text="Delete"></asp:LinkButton>
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

