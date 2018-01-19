<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="Permissions.aspx.vb" Inherits="Permissions" Title="Permission Allocation" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a data-parent="#collapse" data-toggle="collapse" href="#collapse-one">Permission Management
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
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label1" runat="server"
                        Text="Role"></asp:Label>
                </div>
                <div class="col-xs-6">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="ddl_Role" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Labelllll1" runat="server"
                        Text="Module Category"></asp:Label>
                </div>
                <div class="col-xs-6">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbModuleCategory" runat="server" AutoPostBack="True"
                        AppendDataBoundItems="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 center-block label-info control-label">
                    <asp:Label ID="Label29" runat="server" Text="Modules"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 center-block">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gv_ModuleDetails" runat="server" AutoGenerateColumns="False" HorizontalAlign="center"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <PagerSettings FirstPageText="First" NextPageText="Next"
                                    PreviousPageText="Previous" />
                                <AlternatingRowStyle CssClass="altrowstyle" />
                                <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" />
                                <RowStyle CssClass="rowstyle" />
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Chk" runat="server" AutoPostBack="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Module ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblModuleId" runat="server" Text='<%#Eval("ModuleId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Module Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblModuleName" runat="server" Text='<%#Eval("ModuleName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="URL Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblURLName" runat="server" Text='<%#Eval("URL_NAME")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <li style="list-style-type: none" class="alert-info">
                                <asp:Label ID="role_pagelbl" runat="server"
                                    Text="<%# Container.DataItem %>"></asp:Label>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:Button CssClass="btn btn-primary btn-sm" ID="btn_SaveRole" runat="server" Text="Save" UseSubmitBehavior="false" />
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
            $("[id*=btn_SaveRole]").bind("click", function () {
                $("[id*=btn_SaveRole]").val("Saving...");
                $("[id*=btn_SaveRole]").attr("disabled", true);
            });
        });

        $('.nofuturedate').datepicker({
            format: 'dd MM yyyy',
            todayHighlight: true,
            endDate: '+0d'
        });
    </script>
</asp:Content>