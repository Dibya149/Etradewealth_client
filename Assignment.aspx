<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="Assignment.aspx.vb" Inherits="Assignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<style>
    .disabled {
  pointer-events: none;
  cursor: default;
  color:Gray;
}
</style>
    <section class="section">
          <div class="section-header">
						<div class="row">
							<div class="col-xl-6 col-lg-6 col-md-6 col-sm-12">
								<div class="section-header-breadcrumb-content">
									<h1>Assignments</h1>
                  <div class="section-header-breadcrumb">
                    <div class="breadcrumb-item active"><a href="#"><i class="fas fa-home"></i></a></div>
                    <div class="breadcrumb-item"><a href="Index.aspx">Home</a></div>
                    
                  </div>
								</div>
							</div>
							
						</div>
					</div>
          <div class="section-body">
            <asp:Repeater ID="Repdesable" runat="server">
          <ItemTemplate>
            <div class="row">
              <div class="col-lg-12 col-md-12 col-sm-12">
                <div class="card">
                  <div class="card-header" ID="href_assign" runat = "server" >
                  
                   <h4><a href=  '<%# "AssignmentDetail.aspx?id=" & Crypto.Encrypt(Eval("ID"))  %>' class ="disabled"> <%#Eval("Assignmentname")%> </a></h4>
                  </div>
                  
                </div>
              </div>
           </div>
           </ItemTemplate>
          </asp:Repeater>
          <asp:Repeater ID="RepAssign" runat="server">
          <ItemTemplate>
            <div class="row">
              <div class="col-lg-12 col-md-12 col-sm-12">
                <div class="card">
                  <div class="card-header" ID="href_assign" runat = "server" >
                 <%-- <asp:HyperLink ID = "assign_link" runat = "server" Text = '<%#Eval("Assignmentname")%>' NavigateUrl ='<%# "AssignmentDetail.aspx?id=" & Crypto.Encrypt(Eval("ID"))  %>'></asp:HyperLink>--%>
                    <h4><a  href=  '<%# "AssignmentDetail.aspx?id=" & Crypto.Encrypt(Eval("ID"))  %>' readonly="readonly"  > <%#Eval("Assignmentname")%> </a></h4>
                  </div>
                  
                </div>
              </div>
           </div>
           </ItemTemplate>
          </asp:Repeater>
           
           
          </div>
        </section>
</asp:Content>
