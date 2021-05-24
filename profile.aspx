<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="profile.aspx.vb" Inherits="profile" %>

<asp:Content ID="content2" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="assets/jquery.min.js">
    </script>

    <%--   <script type="text/javascript" src="js/plugins/jquery/jquery.min.js"></script>--%>

    <script type="text/javascript" src="assets/jquery.validationEngine-en.js"></script>

    <script type="text/javascript" src="assets/jquery.validationEngine.js"></script>

    <link href="assets/validationEngine.jquery.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var jq = $.noConflict();
        function pageLoad(sender, args) {
            jq(document).ready(function() {

                jq("#aspnetForm").validationEngine('attach', { promptPosition: "topRight" });
            });

            jq("#<%=CmdSave.ClientID %>").click(function() {


                var valid = jq("#aspnetForm").validationEngine('validate');
                var vars = jq("#aspnetForm").serialize();
                if (valid == true) {
                    return true;

                } else {
                    return false;
                }
            });
        }     
   

    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="section-header">
        <div class="row">
            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12">
                <div class="section-header-breadcrumb-content">
                    <h1>
                        Profile</h1>
                    <div class="section-header-breadcrumb">
                        <div class="breadcrumb-item active">
                            <a href="#"><i class="fas fa-home"></i></a>
                        </div>
                        <div class="breadcrumb-item">
                            <a href="#">Member </a>
                        </div>
                        <div class="breadcrumb-item">
                            <a href="#">Profile</a></div>
                    </div>
                </div>
            </div>
            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12">
            </div>
        </div>
    </div>
    <div class="section-body">
        <div class="row">
            
            <div class="col-2"> </div>
            <div class="col-xl-8 col-lg-12 col-md-12 col-sm-12">
                
                <div class="card">
                    
                    <div class="card-body">
                        <div class="table-responsive">
                            <div class="centered">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                </div>
                                <div class="col-2">
                                </div>
                                <div class="col-md-6">
                                    <h6>
                                        Your Placement Detail
                                    </h6>
                                    <div class="form-group">
                                        <label>
                                            Sponsor ID<span class="red">*</span></label>
                                        <asp:TextBox ID="txtReferalId" class="form-control" TabIndex="1" runat="server" AutoPostBack="True"
                                            Enabled="False"></asp:TextBox>
                                    </div>
                                    <div class="form-group" id="DivSponsorName" runat="server" visible="false">
                                        <label>
                                            Sponsor Name<span class="red">*</span></label>
                                        <asp:TextBox ID="TxtReferalNm" class="form-control" runat="server" Enabled="False"></asp:TextBox>
                                    </div>
                                    <div class="form-group" id="DivUplinerId" runat="server" visible="false">
                                        <label>
                                            Placement ID<span class="red">*</span></label>
                                        <asp:TextBox ID="TxtUplinerid" class="form-control" TabIndex="1" runat="server" AutoPostBack="True"
                                            Enabled="False"></asp:TextBox>
                                    </div>
                                    <div class="form-group " id="DivUplinerName" runat="server" visible="false">
                                        <label>
                                            Placement Name<span class="red">*</span></label>
                                        <asp:TextBox ID="TxtUplinerName" class="form-control" runat="server" Enabled="False"></asp:TextBox>
                                    </div>
                                    <div class="form-group greybt" style="display: none">
                                        <label>
                                            Position<span class="red">*</span></label>
                                        <asp:TextBox ID="lblPosition" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                    <h6>
                                        Personal Detail
                                    </h6>
                                    <div class="form-group ">
                                        <label>
                                            Your Name<span class="red">*</span></label>
                                        <asp:HiddenField ID="hdnidno" runat="server"></asp:HiddenField>
                                        <asp:TextBox ID="txtFrstNm" CssClass="form-control" runat="server" TabIndex="3" ValidationGroup="eInformation"
                                            required=""></asp:TextBox>
                                    </div>
                                    <div class="form-group " style="display: none;">
                                        <label>
                                            Date Of Joining<span class="red">*</span></label>
                                        <asp:TextBox ID="TxtDoj" CssClass="form-control" runat="server" TabIndex="3" ValidationGroup="eInformation"
                                            ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group " style="display: none;">
                                        <label>
                                            Date Of Activation</label>
                                        <asp:TextBox ID="TxtDoa" CssClass="form-control" runat="server" TabIndex="3" ValidationGroup="eInformation"
                                            ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group " style="display: none;">
                                        <label>
                                            Father's Name</label>
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <asp:DropDownList CssClass="form-control" ID="CmbType" runat="server" TabIndex="7">
                                                    <asp:ListItem Value="S/O" Text="S/O"></asp:ListItem>
                                                    <asp:ListItem Value="W/O" Text="W/O"></asp:ListItem>
                                                    <asp:ListItem Value="C/O" Text="C/O"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-10" style="padding-left: 0px;">
                                                <asp:TextBox ID="txtFNm" runat="server" TabIndex="8" CssClass="form-control" ValidationGroup="eInformation"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group  greybt">
                                        <label>
                                            Date of Birth</label>
                                        <asp:TextBox ID="TxtDobDate" class="form-control" runat="server" TabIndex="9" required=""></asp:TextBox>
                                        <AjaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtDobDate"
                                            Format="dd-MM-yyyy">
                                        </AjaxToolkit:CalendarExtender>
                                    </div>
                                    <div class="form-group ">
                                        <label>
                                            Mobile No.<span class="red">*</span></label>
                                        <asp:TextBox ID="txtMobileNo" onkeypress="return isNumberKey(event);" CssClass="form-control "
                                            required="" TabIndex="15" runat="server" MaxLength="10" ValidationGroup="eInformation"></asp:TextBox>
                                    </div>
                                    <div class="form-group " style="display: none;">
                                        <label>
                                            Phone No.</label>
                                        <asp:TextBox ID="txtPhNo" onkeypress="return isNumberKey(event);" CssClass="form-control"
                                            TabIndex="16" runat="server" MaxLength="10" Text="0"></asp:TextBox>
                                    </div>
                                    <div class="form-group greybt ">
                                        <label>
                                            E-Mail ID</label>
                                        <asp:TextBox ID="txtEMailId" CssClass="form-control " TabIndex="17" runat="server"
                                            required=""></asp:TextBox>
                                    </div>
                                    <div class="form-group ">
                                        <label>
                                            Pin code</label>
                                        <asp:TextBox ID="txtPinCode" CssClass="form-control" onkeypress="return isNumberKey(event);"
                                            TabIndex="19" runat="server" MaxLength="6" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="form-group ">
                                        <label>
                                            State</label>
                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="StateCode" runat="server" />
                                    </div>
                                    <div class="form-group ">
                                        <label>
                                            City</label>
                                        <asp:TextBox ID="ddlTehsil" CssClass="form-control" TabIndex="18" runat="server"
                                            ValidationGroup="eInformation" autocomplete="off"></asp:TextBox>
                                        <asp:HiddenField ID="HCityCode" runat="server" />
                                    </div>
                                    <h4 style="display: none;">
                                        <span>Nominee Detail</span>
                                    </h4>
                                    <div class="form-group " style="display: none;">
                                        <label>
                                            Nominee Name</label>
                                        <asp:TextBox ID="txtNominee" CssClass="form-control" TabIndex="18" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group greybt " style="display: none;">
                                        <label>
                                            Relation</label>
                                        <asp:TextBox ID="txtRelation" CssClass="form-control" TabIndex="19" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group ">
                                        <asp:Button ID="CmdSave" runat="server" Text="Update" CssClass="btn btn-primary"
                                            TabIndex="27" ValidationGroup="eInformation" />
                                        &nbsp;<asp:Button ID="CmdCancel" runat="server" Text="Cancel" CssClass="btn btn-primary"
                                            TabIndex="28" ValidationGroup="Form-Reset" />
                                    </div>
                                </div>
                                <div class="col-2">
                                </div>
                                <div class="form-group ">
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="eInformation" />
                                </div>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="None"
                                    ControlToValidate="TxtMobileNo" ErrorMessage="Minimum 10 Digits" ValidationExpression="^[0-9]{10,10}$"
                                    SetFocusOnError="true" ValidationGroup="eInformation"></asp:RegularExpressionValidator>&nbsp;
                                <asp:RegularExpressionValidator ID="EmailExpressionValidator" runat="server" ControlToValidate="txtEMailId"
                                    ErrorMessage="Enter Valid Email ID!" Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    SetFocusOnError="true" ValidationGroup="eInformation"></asp:RegularExpressionValidator>&nbsp;
                            </div>
                        </div>
                    </div>
                </div>
                                
            </div>
            
        </div>
    </div>
</asp:Content>
