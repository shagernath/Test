<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="frmUserMgtAudit.aspx.vb" Inherits="frmUserMgtAudit" Title="User Management Audit Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a>User Management Audit Report</a>
            </h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-12 center-block">
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
                <div class="col-xs-12 text-center control-label label-info">
                    <asp:Label ID="Label1" runat="server" Text="Select the fields below to filter report"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label2" runat="server" Text="Report Type"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbRptType" runat="server">
                        <asp:ListItem Text="Roles" Value="Roles"></asp:ListItem>
                        <asp:ListItem Text="Users" Value="Users"></asp:ListItem>
                        <asp:ListItem Text="Branches" Value="Branches"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label1113" runat="server" Text="Action Type"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbActionType" runat="server">
                        <asp:ListItem Text="Add and Delete" Value="Add"></asp:ListItem>
                        <asp:ListItem Text="Update" Value="Update"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:CheckBox ID="chkUser" runat="server" AutoPostBack="True" Text="User"
                        TextAlign="Left" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="lblUser" runat="server" Text="Select User" Visible="False"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbUser" runat="server" Visible="False">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:CheckBox ID="chkDate" runat="server" AutoPostBack="True" Text="Date"
                        TextAlign="Left" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="lblDateFrom" runat="server" Text="Date from"
                        Visible="False"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="bdpFromDate" runat="server" CssClass="col-xs-12 form-control input-sm datepicker" Visible="false"></asp:TextBox>
                    <span class="glyphicon glyphicon-calendar form-control-feedback" style="margin-right: 13px; background-color: #eeeeff; border-radius: 0 3px 3px 0; border: 1px solid rgb(149,188,219);" runat="server" id="fromSpan" visible="false"></span>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="lblDateTo" runat="server" Text="Date to"
                        Visible="False"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="bdptoDate" runat="server" CssClass="col-xs-12 form-control input-sm datepicker" Visible="false"></asp:TextBox>
                    <span class="glyphicon glyphicon-calendar form-control-feedback" style="margin-right: 13px; background-color: #eeeeff; border-radius: 0 3px 3px 0; border: 1px solid rgb(149,188,219);" runat="server" id="toSpan" visible="false"></span>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:Button CssClass="btn btn-primary btn-sm" ID="btnView" runat="server"
                        Text="View Report" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 center-block">
                    <asp:GridView ID="grdSessions" runat="server">
                        <AlternatingRowStyle CssClass="altrowstyle" />
                        <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" />
                        <RowStyle CssClass="rowstyle" />
                        <PagerStyle CssClass="pagination-ys" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server"
                                        Text="View Details" Target="new"></asp:HyperLink>
                                    <asp:TextBox runat="server" ID="txtSession" Visible="False" Text='<%#Bind("SESSION_ID")%>'>
                                    </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
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