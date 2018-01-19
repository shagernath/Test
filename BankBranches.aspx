<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="BankBranches.aspx.vb" Inherits="Bank_Branches" Title="Branches" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a data-parent="#collapse" data-toggle="collapse" href="#collapse-one">Add/Edit Branches
                </a>
            </h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label218" runat="server" Text="Bank Name"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbBrBank" runat="server" AppendDataBoundItems="True"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label2118" runat="server"
                        Text="Bank Code" Visible="False"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:Label ID="lblBankCode" runat="server" Visible="False"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label18" runat="server"
                        Text="Branch Name"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtBranchName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvBranchName" runat="server" ErrorMessage="Branch Name is required" Display="Dynamic" ControlToValidate="txtBranchName"></asp:RequiredFieldValidator>
                </div>
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label5" runat="server"
                        Text="Branch Code"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtBranchCode" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvBranchCode" runat="server" ErrorMessage="Branch Code is required" Display="Dynamic" ControlToValidate="txtBranchCode"></asp:RequiredFieldValidator>
                </div>
            </div>
           
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:Button CssClass="btn btn-primary btn-sm" ID="btnAddBranch" runat="server" Text="Add Branch" UseSubmitBehavior="false" />
                </div>
            </div>
           
            <div class="row">
                <div class="col-xs-12 center-block">
                    <asp:GridView ID="grdBranches" runat="server" HorizontalAlign="Center"
                        AutoGenerateColumns="False" Width="90%">
                        <AlternatingRowStyle CssClass="altrowstyle" />
                        <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" />
                        <RowStyle CssClass="rowstyle" />
                        <PagerStyle CssClass="pagination-ys" />
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"
                                        CommandName="Delete" Text="Delete" OnClientClick="return isDelete();"></asp:LinkButton>
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
                            <asp:TemplateField HeaderText="Bank Code">
                               <ItemTemplate>
                                    <asp:Label ID="lblBank" runat="server" Text ='<%#Bind("Bank")%>'></asp:Label>
                               </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBranchCodeEdit" runat="server" Text='<%#Bind("Branch")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchCode" runat="server" Text ='<%#Bind("Branch")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBranchNameEdit" runat="server" Text='<%#Bind("Branch_NAME")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" runat="server" Text ='<%#Bind("Branch_NAME")%>'></asp:Label>
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
            $("[id*=btnAddBranch]").bind("click", function () {
                $("[id*=btnAddBranch]").val("Adding Branch...");
                $("[id*=btnAddBranch]").attr("disabled", true);
            });
        });

        $('.nofuturedate').datepicker({
            format: 'dd MM yyyy',
            todayHighlight: true,
            endDate: '+0d'
        });
    </script>
</asp:Content>