<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="RatingQuestionValues.aspx.vb" Inherits="RatingQuestionValues" Title="Rating Parameters for Questions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function isDelete() {
            return confirm("Are you sure you want to deactivate this record?");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">Categories for Client Rating
            </h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-12 text-center">
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
                        <%--CssClass="tab"--%>

                        <DynamicHoverStyle ForeColor="White" BackColor="#FA9221"></DynamicHoverStyle>
                        <DynamicMenuStyle BackColor="#FA9221" />
                        <DynamicSelectedStyle BackColor="#FA9221" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <StaticHoverStyle BackColor="#FA9266" ForeColor="White" />
                        <Items>
                            <asp:MenuItem Text="Categories" Value="Categories"
                                NavigateUrl="~/RatingCategories.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Questions" Value="Questions"
                                NavigateUrl="~/RatingQuestions.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Rating Values" Value="Rating Values"
                                NavigateUrl="~/RatingQuestionValues.aspx"></asp:MenuItem>
                        </Items>
                    </asp:Menu>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label1" runat="server"
                        Text="Applicant Type"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbApplicantType" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label2" runat="server"
                        Text="Category"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbCategory" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label4" runat="server"
                        Text="Question"></asp:Label>
                </div>
                <div class="col-xs-6">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbQuestion" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label5" runat="server"
                        Text="Actual Value"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtActualValue" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label6" runat="server"
                        Text="Rating (max. 100)"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtRating" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:Button CssClass="btn btn-primary btn-sm" ID="btnAddRating" runat="server"
                        Text="Add" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:GridView ID="grdRateCriteria" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                        EmptyDataText="No ratings entered yet for this question">
                        <AlternatingRowStyle CssClass="altrowstyle" />
                        <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" />
                        <RowStyle CssClass="rowstyle" />
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"
                                        CommandName="Delete" Text="Deactivate" OnClientClick="return isDelete();"></asp:LinkButton>
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
                            <asp:TemplateField HeaderText="Actual Value">
                                <EditItemTemplate>
                                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtActValEdit" runat="server" Text='<%#Bind("COMMENT")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblActVal" runat="server"><%#Eval("COMMENT")%></asp:Label>
                                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtRateID" runat="server" Text='<%#Bind("ID")%>' Visible="False"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rating">
                                <EditItemTemplate>
                                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtRatingEdit" runat="server" Text='<%#Bind("CALC_VALUE")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRating" runat="server"><%#Eval("CALC_VALUE")%></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>