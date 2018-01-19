<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="LoginParameters.aspx.vb" Inherits="LoginParameters" Title="Login Parameters - Credit Management System" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a data-parent="#collapse" data-toggle="collapse" href="#collapse-one">Login Parameters
                </a>
            </h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-12 center-block">
                    <asp:Menu ID="menuTabs" DynamicHoverStyle-BackColor="#336699"
                        DynamicHoverStyle-ForeColor="White" StaticMenuItemStyle-CssClass="tab"
                        StaticSelectedStyle-CssClass="selectedTab" Orientation="Horizontal"
                        runat="server" Width="776px"
                        ForeColor="White" Style="top: 4px; left: 2px" BackColor="#336699"
                        DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em"
                        StaticSubMenuIndent="10px" Height="18px">
                        <StaticMenuStyle BackColor="#336699" />
                        <StaticSelectedStyle BackColor="White" BorderColor="#1D72A7"
                            BorderStyle="Dotted" BorderWidth="1px" ForeColor="#FFFFFF"></StaticSelectedStyle>
                        <StaticMenuItemStyle BackColor="#FA9221" HorizontalPadding="5px"
                            VerticalPadding="2px"></StaticMenuItemStyle>
                        <DynamicHoverStyle ForeColor="White" BackColor="#FA9221"></DynamicHoverStyle>
                        <DynamicMenuStyle BackColor="#FA9221" />
                        <DynamicSelectedStyle BackColor="#FA9221" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <StaticHoverStyle BackColor="#FA9266" ForeColor="White" />
                        <Items>
                            <asp:MenuItem Text="Users" Value="Users" NavigateUrl="~/Users.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Roles" Value="Roles" NavigateUrl="~/Roles.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Modules" Value="Modules" NavigateUrl="~/Modules.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Permissions" Value="Permissions"
                                NavigateUrl="~/Permissions.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Special Permissions" Value="Special Permissions"
                                NavigateUrl="~/SpecialPermissions.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Authorization" Value="Authorization"
                                NavigateUrl="~/UserMgtAuth.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Login Parameters" Value="Login Parameters"
                                NavigateUrl="~/LoginParameters.aspx"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/Admin/UnlockUsers.aspx" Text="Unlock Users" Value="Unlock Users"></asp:MenuItem>
                        </Items>
                    </asp:Menu>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3 control-label">
                    <asp:Label ID="Label3" runat="server"
                        Text="System Uptime"></asp:Label>
                </div>
                <div class="col-xs-3">
                    <BDP:TimePicker ID="bdpUptime" runat="server">
                    </BDP:TimePicker>
                    <BDP:IsTimeValidator ID="IsTimeUptime" runat="server"
                        ControlToValidate="bdpUpTime" ErrorMessage="Select Time Please">
                    </BDP:IsTimeValidator>
                </div>
                <div class="col-xs-3 control-label">
                    <asp:Label ID="Label8" runat="server"
                        Text="System Downtime"></asp:Label>
                </div>
                <div class="col-xs-3">
                    <BDP:TimePicker ID="bdpDowntime" runat="server">
                    </BDP:TimePicker>
                    <BDP:IsTimeValidator ID="IsTimeDowntime" runat="server"
                        ControlToValidate="bdpDownTime" ErrorMessage="Select Time Please">
                    </BDP:IsTimeValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3 control-label">
                    <asp:Label ID="Label9" runat="server"
                        Text="Password Expiry"></asp:Label>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtPasswordExpiry" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-3 control-label">
                    <asp:Label ID="Label10" runat="server"
                        Text="Min Password Length"></asp:Label>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtPasswordLength" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3 control-label">
                    <asp:Label ID="Label11" runat="server"
                        Text="Number of Access Users"></asp:Label>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtAccessUsers" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-3">
                    <asp:CheckBox ID="chkSpecialCharacters" runat="server"
                        Text="Allow Special Characters" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3 control-label">
                    <asp:Label ID="Label12" runat="server"
                        Text="Max Login Attempts"></asp:Label>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtLoginAttempts" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-3">
                    <asp:CheckBox ID="chkUserCaseSensitive" runat="server"
                        Text="Username Case Sensitive" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3 control-label">
                    <asp:Label ID="Label13" runat="server"
                        Text="Session Timeout"></asp:Label>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtSessionTimeout" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-3 control-label">
                    <asp:Label ID="Labelfff13" runat="server"
                        Text="Default User Password"></asp:Label>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtDefaultPassword" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3 control-label">
                    <asp:Label ID="lblDomain" runat="server"
                        Text="Active Directory Domain"></asp:Label>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtDomain" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:Button CssClass="btn btn-primary btn-sm" ID="btnSaveLoginParameters" runat="server" Text="Save" UseSubmitBehavior="false" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $('.datepicker').datepicker({
            format: 'dd MM yyyy',
            todayHighlight: true
        });

        $(function () {
            $("[id*=btnSaveLoginParameters]").bind("click", function () {
                $("[id*=btnSaveLoginParameters]").val("Saving...");
                $("[id*=btnSaveLoginParameters]").attr("disabled", true);
            });
        });

        $('.nofuturedate').datepicker({
            format: 'dd MM yyyy',
            todayHighlight: true,
            endDate: '+0d'
        });
    </script>
</asp:Content>