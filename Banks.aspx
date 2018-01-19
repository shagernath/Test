<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="Banks.aspx.vb" Inherits="Banks" Title="Banks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a data-parent="#collapse" data-toggle="collapse" href="#collapse-one">Add/Edit Banks
                </a>
            </h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label5" runat="server"
                        Text="Bank Code"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtBranchCode" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label18" runat="server"
                        Text="Bank Name"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtBranchName" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3"></div>
                <div class="col-xs-3">
                    <asp:RequiredFieldValidator ID="valBankCode" ValidationGroup="main" runat="server" ErrorMessage="Bank Code is required" ControlToValidate="txtBranchCode"></asp:RequiredFieldValidator>
                </div>
                <div class="col-xs-3"></div>
                <div class="col-xs-3">
                    <asp:RequiredFieldValidator ID="valBankName" ValidationGroup="main" runat="server" ErrorMessage="Bank Name is required" ControlToValidate="txtBranchName"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:Button CssClass="btn btn-primary btn-sm" ValidationGroup="main" ID="btnAddBranch" runat="server" Text="Add Bank" UseSubmitBehavior="false" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 center-block">
                    <asp:GridView ID="grdBranches" runat="server" HorizontalAlign="Center"
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
                            <asp:TemplateField HeaderText="Bank Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtGrdBranchCode" runat="server" Text='<%#Bind("BANK")%>'></asp:TextBox>
                                    <asp:TextBox ID="txtOldBranchCode11" runat="server" Text='<%#Bind("BANK")%>' Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchCode" runat="server"><%#Eval("BANK")%></asp:Label>
                                    <asp:TextBox ID="txtOldBranchCode" runat="server" Text='<%#Bind("BANK")%>' Visible="False"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBranchNameEdit" runat="server" Text='<%#Bind("BANK_NAME")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" runat="server"><%#Eval("BANK_NAME")%></asp:Label>
                                    <asp:TextBox ID="txtGrdBranch" runat="server" Text='<%#Bind("ID")%>' Visible="False"></asp:TextBox>
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
            $("[id*=btnAddBranch]").bind("click", function () {
                $("[id*=btnAddBranch]").val("Adding Bank...");
                $("[id*=btnAddBranch]").attr("disabled", true);
            });
        });
    </script>
</asp:Content>