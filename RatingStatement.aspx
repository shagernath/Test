<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="RatingStatement.aspx.vb" Inherits="RatingStatement" title="Rating Statement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table bgcolor="#EEEEEE" style="width: 100%;">
        <tr>
            <td bgcolor="#023E5A" colspan="4">&nbsp;&nbsp;
                                            <br />
                <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Size="Large"
                    ForeColor="#FFFFFF" Text="Rating Statement"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td style="height: 25px"></td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label2" runat="server" Font-Names="Calibri" ForeColor="#555555"
                    Text="Loan Application Number"></asp:Label>
            </td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtLoanAppNo" runat="server"></asp:TextBox>
                <asp:Button ID="btnViewRating" runat="server" Text="View" />
            </td>
        </tr>
        <tr>
            <td style="height: 25px"></td>
        </tr>
    </table>
</asp:Content>

