<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="fonts/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="shortcut icon" href="~/favicon.ico" />
    <title>360 Credit Management System Login</title>
    <style>
        .col-center-block {
            float: none;
            display: block;
            margin-left: auto;
            margin-right: auto;
        }

        html,
        body,
        .container {
            height: 100%;
            width: 100%;
        }

        .container {
            display: table;
            vertical-align: middle;
            padding-top: 5%;
        }

        .vertical-center-row {
            display: table-cell;
            vertical-align: middle;
        }

        .login-screen-bg {
            background-color: #EFEFEF;
        }

        .panel-git {
            border: 1px solid #d8dee2;
        }

            .panel-git h3 {
                color: #fff;
            }

            .panel-git .panel-heading {
                background-color: #829AA8;
            }

        .login-widget {
            padding: 50px;
            border-radius: 10px;
            padding: 30px;
            box-shadow: 0px 0px 1px 1px rgba(161, 159, 159, 0.1);
            background-color: #FFF;
            /*width: 50%;*/
        }
    </style>
</head>
<body class="login-screen-bg">
    <form id="form1" runat="server">
        <div class="container">
            <div class="row vertical-center-row">
                <div class="col-md-4 col-center-block login-widget">
                    <h3 class="text-center" style="color: #336699">360&deg; Credit Management System</h3>
                    <br />
                    <div>
                        <div class="form-group">
                            <div class="input-group">
                                <div class="input-group-addon"><i class="fa fa-user fa-fw"></i></div>
                                <input id="txt_UserId" runat="server" class="form-control input-lg" placeholder="Username" type="text">
                            </div>
                            <div class="input-group">
                                <asp:RequiredFieldValidator Display="Dynamic" ID="usernameValidator" runat="server"
                                    ErrorMessage="Username is required" ControlToValidate="txt_UserId"
                                    Font-Size="Small"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group">
                                <div class="input-group-addon"><i class="fa fa-key fa-fw"></i></div>
                                <input id="txt_Password" runat="server" class="form-control input-lg" placeholder="Password" type="password">
                            </div>
                            <div class="input-group">
                                <asp:RequiredFieldValidator Display="Dynamic" ID="passwordValidator" runat="server"
                                    ErrorMessage="Password is required" ControlToValidate="txt_Password" Font-Size="Small"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12 text-center">
                                <asp:Label ID="lblLoginError" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                        <br />
                        <div class="form-group">
                            <%--<button id="btn_Login" runat="server" class="btn btn-primary btn-block" type="submit" onclick="btn_Login_Click">Login</button>--%>
                            <asp:Button ID="btn_Login" runat="server" Text="Login" CssClass="btn btn-primary btn-block btn-lg" OnClick="btn_Login_Click" UseSubmitBehavior="false" />
                            <hr>
                        </div>
                        <div class="form-group text-right">
                            <%--<div class="label label-default">Powered by</div>--%>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/escrow-small.png" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="Scripts/jquery-2.2.3.min.js"></script>
        <script type="text/javascript">

            $(function () {
                $("[id*=btn_Login]").bind("click", function () {
                    // $("[id*=btn_Login]").val("Saving...");
                    $("[id*=btn_Login]").attr("disabled", true);
                });
            });
        </script>
    </form>
</body>
</html>