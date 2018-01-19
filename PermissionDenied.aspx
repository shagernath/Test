<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="PermissionDenied.aspx.vb" Inherits="PermissionDenied" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="bg-danger">
        <p class="alert-danger">
            <asp:Label ID="Label8" runat="server" Text="ACCESS TO THE PAGE DENIED " Font-Size="Large"></asp:Label>
        </p>
        <p>
            You do not have permission to access this page. Use the menu to navigate or return to your <a href="index.aspx">dashboard</a>
        </p>
    </div>
</asp:Content>