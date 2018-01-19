<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="ParaExtValue.aspx.vb" Inherits="ParaExtValue" Title="Extendible Value Calculation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a data-parent="#collapse" data-toggle="collapse" href="#collapse-one">Extendible Value Calculation
                </a>
            </h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label18" runat="server"
                        Text="Security Type"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbSecType" runat="server">
                        <asp:ListItem Text="Vehicle" Value="Vehicle"></asp:ListItem>
                        <asp:ListItem Text="Property" Value="Property"></asp:ListItem>
                        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label5" runat="server"
                        Text="Ext. Value(%)"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtExtValue" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:Button CssClass="btn btn-primary btn-sm" ID="btnAddValue" runat="server" Text="Add" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 center">
                    <asp:GridView ID="grdValues" runat="server" HorizontalAlign="Center"
                        AutoGenerateColumns="False">
                        <AlternatingRowStyle CssClass="altrowstyle" />
                        <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" />
                        <RowStyle CssClass="rowstyle" />
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"
                                        CommandName="Delete" Text="Delete" OnClientClick="return isDelete();"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="True"
                                        CommandName="Update" Text="Update"></asp:LinkButton>
                                    &nbsp;<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False"
                                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False"
                                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Security Type">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtGrdSecType" runat="server" Text='<%#Bind("SECURITY_TYPE")%>'></asp:TextBox>
                                    <asp:TextBox ID="txtOldSecType11" runat="server" Text='<%#Bind("SECURITY_TYPE")%>' Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSecType" runat="server"><%#Eval("SECURITY_TYPE")%></asp:Label>
                                    <asp:TextBox ID="txtOldSecType" runat="server" Text='<%#Bind("SECURITY_TYPE")%>' Visible="False"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value(%)">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtExtValueEdit" runat="server" Text='<%#Bind("EXT_VALUE_PERC")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblExtValue" runat="server"><%#Eval("EXT_VALUE_PERC")%></asp:Label>
                                    <asp:TextBox ID="txtGrdValue" runat="server" Text='<%#Bind("EXT_VALUE_PERC")%>' Visible="False"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>