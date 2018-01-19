<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="Modules.aspx.vb" Inherits="Modules" Title="Modules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a data-parent="#collapse" data-toggle="collapse" href="#collapse-one">Add/Edit Modules
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
                    <asp:Label ID="Label30kilo" runat="server"
                        Text="Module Category"></asp:Label>
                </div>
                <div class="col-xs-6">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbModuleCategory" runat="server"
                        AppendDataBoundItems="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label30" runat="server"
                        Text="Module Name"></asp:Label>
                </div>
                <div class="col-xs-6">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtModuleName" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label31" runat="server" Text="URL"></asp:Label>
                </div>
                <div class="col-xs-6">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtURL" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:Button CssClass="btn btn-primary btn-sm" ID="btnSaveModule" runat="server" Text="Save" UseSubmitBehavior="false" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 center-block label-info">
                    <asp:Label ID="Label36" runat="server" Font-Bold="True"
                        Font-Size="Large" ForeColor="#FFFFFF" Text="Modules"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 center-block">
                    <asp:GridView ID="grdModules" runat="server" AutoGenerateColumns="False"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        HorizontalAlign="Center" s="">
                        <PagerSettings FirstPageText="First" NextPageText="Next"
                            PreviousPageText="Previous" />
                        <AlternatingRowStyle CssClass="altrowstyle" />
                        <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" />
                        <RowStyle CssClass="rowstyle" />
                        <PagerStyle CssClass="pagination-ys" />
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkBtnDel" runat="server" CausesValidation="False"
                                        CommandName="Delete" OnClientClick="return isDelete();" Text="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="lnkBtnUpd" runat="server" CausesValidation="False"
                                        CommandName="Update" Text="Update"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnCan" runat="server" CausesValidation="False"
                                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkBtnEdt" runat="server" CausesValidation="False"
                                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Module ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblModuleId0" runat="server" Text='<%#Eval("ModuleId")%>'></asp:Label>
                                    <asp:TextBox ID="txtModuleId0" runat="server" Text='<%#Eval("ModuleId")%>'
                                        Visible="False"></asp:TextBox>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblModuleId0Edit" runat="server" Text='<%#Eval("ModuleId")%>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Module Category">
                                <ItemTemplate>
                                    <asp:Label ID="lblModuleCategory0" runat="server" Text='<%#Eval("CATEGORY")%>'></asp:Label>
                                    <asp:TextBox ID="txtModuleCategory0" runat="server" Text='<%#Eval("MODULE_CATEGORY")%>'
                                        Visible="False"></asp:TextBox>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtModuleCategory0Edit" runat="server"
                                        Text='<%#Eval("MODULE_CATEGORY")%>' Visible="False"></asp:TextBox>
                                    <asp:DropDownList ID="cmbModuleCategoryEdit" runat="server"
                                        AppendDataBoundItems="True">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Module Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblModuleName0" runat="server" Text='<%#Eval("ModuleName")%>'></asp:Label>
                                    <asp:TextBox ID="txtModuleName0" runat="server" Text='<%#Eval("ModuleName")%>'
                                        Visible="False"></asp:TextBox>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtModuleName0Edit" runat="server"
                                        Text='<%#Eval("ModuleName")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="URL Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblURLName0" runat="server" Text='<%#Eval("URL_NAME")%>'></asp:Label>
                                    <asp:TextBox ID="txtURLName0" runat="server" Text='<%#Eval("URL_NAME")%>'
                                        Visible="False"></asp:TextBox>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtURLName0Edit" runat="server" Text='<%#Eval("URL_NAME")%>'></asp:TextBox>
                                </EditItemTemplate>
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
            $("[id*=btnSaveModule]").bind("click", function () {
                $("[id*=btnSaveModule]").val("Saving...");
                $("[id*=btnSaveModule]").attr("disabled", true);
            });
        });

        $('.nofuturedate').datepicker({
            format: 'dd MM yyyy',
            todayHighlight: true,
            endDate: '+0d'
        });
    </script>
</asp:Content>