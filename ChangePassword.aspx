<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="ChangePassword.aspx.vb" Inherits="ChangePassword" Title="Change Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a data-parent="#collapse" data-toggle="collapse" href="#collapse-one">Change Password
                </a>
            </h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-12 text-center alert-danger">
                    <span class="fa fa-exclamation-triangle"></span>
                    <asp:Label ID="lblPwdShort" runat="server"
                        Text="Your password is too short. Please change it before you proceed"
                        Visible="False" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3 control-label">
                    <asp:Label ID="Label5" runat="server" Text="Old Password"></asp:Label>
                </div>
                <div class="col-xs-5">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtold" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    <asp:RequiredFieldValidator ID="usernameValidator" runat="server"
                        ErrorMessage="*" ControlToValidate="txtold"
                        Font-Size="Large"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-3 control-label">
                    <asp:Label ID="Label6" runat="server" Text="New Password"></asp:Label>
                </div>
                <div class="col-xs-5">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtnew" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    <asp:RequiredFieldValidator ID="newValidator" runat="server"
                        ErrorMessage="*" ControlToValidate="txtnew" Font-Size="Large"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3 control-label">
                    <asp:Label ID="Label5556" runat="server" Text="Confirm New Password"></asp:Label>
                </div>
                <div class="col-xs-5">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtconfirm" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    <asp:RequiredFieldValidator ID="passwordValidator" runat="server"
                        ErrorMessage="*" ControlToValidate="txtconfirm" Font-Size="Large"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12 center-block">
                    <asp:Label ID="lblLoginError" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:Button ID="btnChangePwd" runat="server" Text="Change" CssClass="btn btn-primary btn-sm" />
                    <asp:Button ID="btn_Quit" runat="server" Text="Quit" CausesValidation="False" CssClass="btn btn-danger btn-sm" Visible="false" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>