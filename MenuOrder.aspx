<%@ Page Title="Arrange Menu Items Order" Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="MenuOrder.aspx.vb" Inherits="MenuOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .selected {
            background-color: #b3dff8;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a>Menu Items Ordering</a>
            </h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-12 center-block">
                    <asp:Menu ID="menuTabs" runat="server" BackColor="#336699" DynamicHorizontalOffset="2" DynamicHoverStyle-BackColor="#336699" DynamicHoverStyle-ForeColor="White" Font-Names="Verdana" Font-Size="0.8em" ForeColor="White" Height="18px" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedTab" StaticSubMenuIndent="10px" Style="top: 4px; left: 2px" Width="776px">
                        <StaticMenuStyle BackColor="#336699" />
                        <StaticSelectedStyle BackColor="White" BorderColor="#1D72A7" BorderStyle="Dotted" BorderWidth="1px" ForeColor="#FFFFFF" />
                        <StaticMenuItemStyle BackColor="#FA9221" HorizontalPadding="5px" VerticalPadding="2px" />
                        <%--CssClass="tab"--%>
                        <DynamicHoverStyle BackColor="#FA9221" ForeColor="White" />
                        <DynamicMenuStyle BackColor="#FA9221" />
                        <DynamicSelectedStyle BackColor="#FA9221" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <StaticHoverStyle BackColor="#FA9266" ForeColor="White" />
                        <Items>
                            <asp:MenuItem NavigateUrl="~/Users.aspx" Text="Users" Value="Users"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/Roles.aspx" Text="Roles" Value="Roles"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/Modules.aspx" Text="Modules" Value="Modules"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/Permissions.aspx" Text="Permissions" Value="Permissions"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/SpecialPermissions.aspx" Text="Special Permissions" Value="Special Permissions"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/UserMgtAuth.aspx" Text="Authorization" Value="Authorization"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/LoginParameters.aspx" Text="Login Parameters" Value="Login Parameters"></asp:MenuItem>
                        </Items>
                    </asp:Menu>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label1" runat="server" Text="Role"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="ddl_Role" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Labelllll1" runat="server" Text="Module Category"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbModuleCategory" runat="server" AppendDataBoundItems="True" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 label-info control-label">
                    <asp:Label ID="Label29" runat="server" Font-Bold="True" Text="Drag and drop the permissions into the desired order and save"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 center-block">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gv_ModuleDetails" runat="server" AutoGenerateColumns="False" HorizontalAlign="center" EmptyDataText="No permissions for this role in this category">
                                <PagerSettings FirstPageText="First" NextPageText="Next" PreviousPageText="Previous" />
                                <AlternatingRowStyle CssClass="altrowstyle" />
                                <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" />
                                <RowStyle CssClass="rowstyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Module ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblModuleId" runat="server" Text='<%#Eval("ModuleId")%>'></asp:Label>
                                            <input type="hidden" name="lblPermissionId" value='<%# Eval("ID") %>' />
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
                    <asp:Button CssClass="btn btn-primary btn-sm" ID="btn_SaveRole" runat="server" Text="Save" OnClick="UpdatePreference" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("[id*=gv_ModuleDetails]").sortable({
                items: 'tr:not(tr:first-child)',
                cursor: 'hand',
                axis: 'y',
                dropOnEmpty: false,
                start: function (e, ui) {
                    ui.item.addClass("selected");
                },
                stop: function (e, ui) {
                    ui.item.removeClass("selected");
                },
                receive: function (e, ui) {
                    $(this).find("tbody").append(ui.item);
                }
            });
        });
    </script>
</asp:Content>