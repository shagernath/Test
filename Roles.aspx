<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="Roles.aspx.vb" Inherits="Roles" Title="User Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a data-parent="#collapse" data-toggle="collapse" href="#collapse-one">Add/Edit User Roles
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
                    Role Name
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txt_Role" runat="server" ValidationGroup="roles"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvRoleName" runat="server" ValidationGroup="roles"
                        ControlToValidate="txt_Role" ErrorMessage="Enter Role Name"></asp:RequiredFieldValidator>
                </div>
                <div class="col-xs-2 control-label">
                    Dashboard View
                </div>
                <div class="col-xs-4">
                    <asp:RadioButtonList ID="rdbDashboardView" runat="server" RepeatDirection="Horizontal" CssClass="col-xs-12">
                        <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
                        <asp:ListItem Text="Branch" Value="Branch"></asp:ListItem>
                        <asp:ListItem Text="Overall" Value="Overall"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:Button CssClass="btn btn-primary btn-sm" ID="btn_AddRole" runat="server" Text="Add Role"
                        ValidationGroup="roles" UseSubmitBehavior="false" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 center-block">
                    <asp:GridView ID="grdAddedRoles" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" HorizontalAlign="Center">
                        <AlternatingRowStyle CssClass="altrowstyle" />
                        <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" />
                        <RowStyle CssClass="rowstyle" />
                        <PagerStyle CssClass="pagination-ys" />
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"
                                        CommandName="Delete" OnClientClick="return isDelete();" Text="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False"
                                        CommandName="Update" Text="Update"></asp:LinkButton>
                                    &nbsp;<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False"
                                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False"
                                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role ID">
                                <EditItemTemplate>
                                    <asp:Label ID="lblRoleIDEdit" runat="server"><%#Eval("RoleID")%></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRoleID" runat="server"><%#Eval("RoleID")%></asp:Label>
                                    <asp:TextBox ID="txtRoleIDEdit" runat="server" Text='<%# Bind("RoleID")%>'
                                        Visible="False"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRoleNameEdit" runat="server" Text='<%# Bind("RoleName")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRoleName" runat="server"><%#Eval("RoleName")%></asp:Label>
                                    <asp:TextBox ID="txtRoleName" runat="server" Text='<%# Bind("RoleName")%>'
                                        Visible="False"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dashboard View">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDashboardViewEdit" runat="server" Text='<%# Bind("DASHBOARD")%>'
                                        Visible="False"></asp:TextBox>
                                    <asp:DropDownList ID="cmbDashboardViewEdit" runat="server">
                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                        <asp:ListItem Value="Individual" Text="Individual"></asp:ListItem>
                                        <asp:ListItem Value="Branch" Text="Branch"></asp:ListItem>
                                        <asp:ListItem Value="Overall" Text="Overall"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDashboardView" runat="server"><%#Eval("DASHBOARD")%></asp:Label>
                                    <asp:TextBox ID="txtDashboardView" runat="server" Text='<%# Bind("DASHBOARD")%>'
                                        Visible="False"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Status">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkStatusEdit" runat="server"
                                        Checked='<%# Bind("USER_STATUS")%>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkStatus" runat="server" Checked='<%# Bind("USER_STATUS")%>'
                                        Enabled="False" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("[id*=btn_AddRole]").bind("click", function () {
                $("[id*=btn_AddRole]").val("Saving Role...");
                $("[id*=btn_AddRole]").attr("disabled", true);
            });
        });
    </script>
</asp:Content>