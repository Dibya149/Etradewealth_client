<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="NewTask.aspx.vb" Inherits="NewTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function onlyNumbers(evt) {
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="section">
        <div class="section-header">
            <div class="row">
                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12">
                    <div class="section-header-breadcrumb-content">
                        <h1>
                            Attempt Task</h1>
                        <div class="section-header-breadcrumb">
                            <div class="breadcrumb-item active">
                                <a href="#"><i class="fas fa-home"></i></a>
                            </div>
                            <div class="breadcrumb-item">
                                <a href="Assignment.aspx">Assignment</a></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="section-body">
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12">
                    <div class="card">
                        <div class="card-header">
                            <h4>
                                Attempt Task</h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="RptDirects" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                                            AllowPaging="true" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <%#Eval("Date")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <%#Eval("Assignmentname")%>
                                                        <asp:HiddenField ID="hdntypeName" runat="server" Value='<%# Eval("TypeName") %>' />
                                                              <asp:HiddenField ID="HdnTypeId" runat="server" Value='<%# Eval("Typeid") %>' />
                                                  
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Upload Image/Code/Mobile No">
                                                    <ItemTemplate>
                                                        <div runat="server" id="divAccountNo"  class="form-group">
                                                       <asp:Label ID="lblAccount" runat="server" Text="Account No" Visible ="false"></asp:Label>
                                                            <asp:TextBox ID="txtAccount" runat="server" placeholder="Enter Account NO." onkeypress="return onlyNumbers(this);"
                                                                Text='<%# Eval("AccountNo") %>' Visible ="false" CssClass ="form-control">
                                                            </asp:TextBox>
                                                            </div>
                                                            <div id="divMobile" runat="server" class="form-group">
                                                             <asp:Label ID="lblMobile" runat="server" Text="Mobile No" Visible="false"  ></asp:Label>
                                                            <asp:TextBox ID="txtMobile" runat="server" placeholder="Enter Mobile No." onkeypress="return onlyNumbers(this);"
                                                                Text='<%# Eval("MobileNo") %>'  Visible ="false" CssClass ="form-control">
                                                            </asp:TextBox>
                                                            </div>
                                                            <div id="DivEmail" runat="server" class="form-group">
                                                             <asp:Label ID="LblEmail" runat="server" Text="Email ID" Visible="false" ></asp:Label>
                                                            <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Email Id." Text='<%# Eval("EmailID") %>'
                                                              Visible ="false" CssClass ="form-control">
                                                            </asp:TextBox>
                                                        </div>
                                                        <div runat="server" id="divCCode" class="form-group" >
                                                         <asp:Label ID="lblccode" runat="server" Text="Client Code" Visible="false" ></asp:Label>
                                                            <asp:TextBox ID="txtCCode" runat="server"  CssClass ="form-control" placeholder="Enter Client Code." Text='<%# Eval("CCode") %>'  Visible ="false">
                                                            </asp:TextBox>
                                                        </div>
                                                        <div runat="server" id="divAll" class="form-group" >
                                                         <asp:Label ID="Lblcode" runat="server" Text="All" Visible="false" ></asp:Label>
                                                       
                                                            <asp:TextBox ID="txtCode" runat="server" Text='<%# Eval("Code") %>' CssClass ="form-control"  Visible ="false">
                                                            </asp:TextBox>
                                                        </div>
                                                         <div runat="server" id="divScore"  class="form-group">
                                                           <asp:Label ID="LblScore" runat="server" Text="Score" Visible="false" ></asp:Label>
                                                       
                                                        <asp:TextBox ID="TxtScore" runat="server" Text='<%# Eval("Score") %>' placeholder="Enter Score" CssClass ="form-control"  Visible ="false"></asp:TextBox></div>
                                                     
                                                     <div runat="server" id="divother" class="form-group" >
                                                          <asp:Label ID="Lblother" runat="server" Text="Enter Link" Visible="false" ></asp:Label>
                                                       
                                                        <asp:TextBox ID="txtOther" runat="server" Text='<%# Eval("Other") %>' placeholder="Enter Link"  Visible ="false" CssClass ="form-control"></asp:TextBox></div>
                                                        
                                                        <div runat="server" id="divOther2" class="form-group" >
                                                          <asp:Label ID="lblother2" runat="server" Text="Reason for not completed assignment" Visible="false" ></asp:Label>
                                                       
                                                        <asp:TextBox ID="TxtOther2" TextMode="MultiLine"  runat="server" Text='<%# Eval("Other2") %>' PlaceHolder="Reason for not completed assignment"  Visible ="false" CssClass ="form-control"></asp:TextBox></div>
                                                       <%--<div runat="server" id="divlink" class="form-group" >
                                                          <asp:Label ID="Lbllink" runat="server" Text="Enter Link" Visible="false" ></asp:Label>
                                                       
                                                        <asp:TextBox ID="txtlink" runat="server" Text='<%# Eval("Other") %>' placeholder="Enter Link"  Visible ="false" CssClass ="form-control"></asp:TextBox></div>
                                                        
                                                        <div runat="server" id="divreason" class="form-group" >
                                                          <asp:Label ID="lblreason" runat="server" Text="Reason for not completed assignment" Visible="false" ></asp:Label>
                                                       
                                                        <asp:TextBox ID="Txtreason" runat="server" TextMode="MultiLine" Text='<%# Eval("Other2") %>' PlaceHolder="Reason for not completed assignment"  Visible ="false" CssClass ="form-control"></asp:TextBox></div>--%>
                                                       <br />
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="" Visible="false" Height="50px" />
                                                        <asp:FileUpload ID="Fup1" runat="server" Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnComplete" runat="server" Text="Submit Task" CssClass="btn btn-primary"
                                                            CommandArgument='<%# Eval("id") %>' CommandName="UploadCOLFile" Visible='<%# Eval("btnComplete") %>' />
                                                        <asp:Button ID="btnNotComplete" runat="server" Text="Task Completed" CssClass="btn btn-dark"
                                                            Visible='<%# Eval("btnNotComplete") %>' Enabled="false" />
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
            </div>
        </div>
    </div>
</asp:Content>
