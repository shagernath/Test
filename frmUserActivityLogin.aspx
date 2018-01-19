<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="frmUserActivityLogin.aspx.vb" Inherits="frmUserActivityLogin" Title="User Login Reports" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a>User Login Reports
                </a>
            </h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:Menu ID="menuTabs" runat="server" BackColor="#336699"
                        DynamicHorizontalOffset="2" DynamicHoverStyle-BackColor="#336699"
                        DynamicHoverStyle-ForeColor="White" Font-Names="Verdana" Font-Size="0.8em"
                        ForeColor="White" Height="18px" Orientation="Horizontal"
                        StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedTab"
                        StaticSubMenuIndent="10px" Style="top: 4px; left: 2px" Width="776px">
                        <StaticMenuStyle BackColor="#336699" />
                        <StaticSelectedStyle BackColor="White" BorderColor="#1D72A7"
                            BorderStyle="Dotted" BorderWidth="1px" ForeColor="#FFFFFF" />
                        <StaticMenuItemStyle BackColor="#FA9221" HorizontalPadding="5px"
                            VerticalPadding="2px" />
                        <%--CssClass="tab"--%>

                        <DynamicHoverStyle BackColor="#FA9221" ForeColor="White" />
                        <DynamicMenuStyle BackColor="#FA9221" />
                        <DynamicSelectedStyle BackColor="#FA9221" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <StaticHoverStyle BackColor="#FA9266" ForeColor="White" />
                        <Items>
                            <asp:MenuItem NavigateUrl="~/frmUserMgtAudit.aspx" Text="User Management Audit"
                                Value="User Management Audit"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/frmUserActivityLogin.aspx" Text="User Logins"
                                Value="User Logins"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/frmUserActivityPages.aspx" Text="Page Views"
                                Value="Page Views"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/frmUserActivityActions.aspx" Text="User Activity"
                                Value="User Activity"></asp:MenuItem>
                        </Items>
                    </asp:Menu>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label1" runat="server"
                        Text="Search User"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbUsernames" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label2" runat="server" Text="Date from"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="bdpDateFrom" runat="server" CssClass="col-xs-12 form-control input-sm datepicker"></asp:TextBox>
                    <span class="glyphicon glyphicon-calendar form-control-feedback" style="margin-right: 13px; background-color: #eeeeff; border-radius: 0 3px 3px 0; border: 1px solid rgb(149,188,219); z-index: 0;"></span>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label4" runat="server" Text="Date to"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="bdpDateTo" runat="server" CssClass="col-xs-12 form-control input-sm datepicker"></asp:TextBox>
                    <span class="glyphicon glyphicon-calendar form-control-feedback" style="margin-right: 13px; background-color: #eeeeff; border-radius: 0 3px 3px 0; border: 1px solid rgb(149,188,219); z-index: 0;"></span>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:Button CssClass="btn btn-primary btn-sm" ID="btnView" runat="server" Text="View Report" UseSubmitBehavior="false" />
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
            $("[id*=btnView]").bind("click", function () {
                $("[id*=btnView]").val("View Report...");
                $("[id*=btnView]").attr("disabled", true);
            });
        });

        $('.nofuturedate').datepicker({
            format: 'dd MM yyyy',
            todayHighlight: true,
            endDate: '+0d'
        });
    </script>
</asp:Content>