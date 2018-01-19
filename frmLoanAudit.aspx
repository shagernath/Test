<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="frmLoanAudit.aspx.vb" Inherits="frmLoanAudit" title="Untitled Page" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table bgcolor="#EEEEEE" style="width: 100%;">
        <tr>
            <td bgcolor="#023E5A" colspan="4">
                <br />
                &nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Size="Large"
                    ForeColor="#FFFFFF" Text="Loan Audit Report"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" colspan="2">
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
                        <asp:MenuItem NavigateUrl="~/frmSumLoanReports.aspx" Text="Loan Reports"
                            Value="Loan Reports"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/LoanbyBranch.aspx" Text="Loan Type Report"
                            Value="Loan Type Report"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/Loanbytype.aspx" Text="Loan Branch Report"
                            Value="Loan Branch Report"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/LoanByStatus.aspx" Text="Loan Status Report"
                            Value="Loan Status Report"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/frmDueRepayments.aspx" Text="Due Repayments"
                            Value="Due Repayments"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="frmLoanRepayments.aspx" Text="Repayments Report"
                            Value="Repayments Report"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/frmLoanAudit.aspx" Text="Audit Trail"
                            Value="Audit Trail"></asp:MenuItem>
                    </Items>
                </asp:Menu>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <hr size="1" style="color: #7C8D59" width="95%" />
            </td>
        </tr>
        <tr>
            <td style="height: 10px"></td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Label ID="Label1" runat="server" Text="Select the fields below to filter report"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 90px;" align="right">
                <asp:CheckBox ID="chkBranch" runat="server" AutoPostBack="True" Text="Branch"
                    TextAlign="Left" />
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 240px"></td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblBranch" runat="server" Font-Names="Calibri"
                    ForeColor="#555555" Text="Select Branch" Visible="False"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="cmbBranch" runat="server" Visible="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 90px;" align="right">
                <asp:CheckBox ID="chkUser" runat="server" AutoPostBack="True" Text="User"
                    TextAlign="Left" />
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 240px"></td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblUser" runat="server" Font-Names="Calibri"
                    ForeColor="#555555" Text="Select User" Visible="False"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="cmbUser" runat="server" Visible="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:CheckBox ID="chkProdType" runat="server" AutoPostBack="True"
                    Text="Product Type" TextAlign="Left" />
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblProdType" runat="server" Font-Names="Calibri"
                    ForeColor="#555555" Text="Select Product Type"
                    Visible="False"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="cmbProdType" runat="server" Visible="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:CheckBox ID="chkDate" runat="server" AutoPostBack="True" Text="Date"
                    TextAlign="Left" />
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblDateFrom" runat="server" Text="Date from" Font-Names="Calibri"
                    ForeColor="#555555" Visible="False"></asp:Label>
            </td>
            <td align="left">
                <BDP:BasicDatePicker ID="bdpFromDate" runat="server" DateFormat="yyyy-MM-dd"
                    Visible="False">
                </BDP:BasicDatePicker>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblDateTo" runat="server" Text="Date to" Font-Names="Calibri"
                    ForeColor="#555555" Visible="False"></asp:Label>
            </td>
            <td align="left">
                <BDP:BasicDatePicker ID="bdptoDate" runat="server" DateFormat="yyyy-MM-dd"
                    Visible="False">
                </BDP:BasicDatePicker>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnView" runat="server" Font-Names="Calibri"
                    ForeColor="#555555" Text="View Report" />
            </td>
        </tr>
        <tr>
            <td style="height: 5px"></td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:GridView ID="grdSessions" runat="server">
                    <AlternatingRowStyle CssClass="altrowstyle" />
                    <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" />
                    <RowStyle CssClass="rowstyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server"
                                    NavigateUrl="" Text="View Details" Target="new"></asp:HyperLink>
                                <asp:TextBox runat="server" ID="txtSession" Visible="False" Text='<%#Bind("SESSION_ID")%>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

