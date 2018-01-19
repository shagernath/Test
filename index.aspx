<%@ Page Language="VB" AutoEventWireup="false" CodeFile="index.aspx.vb" Inherits="index" %>

<%@ Register Assembly="DevExpress.XtraCharts.v13.2.Web, Version=13.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>

<%@ Register Assembly="DevExpress.XtraCharts.v13.2, Version=13.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Escrow 360 Credit Management System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="fonts/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/layouts/layout/css/themes/light2.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME LAYOUT STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />
</head>
<body class="page-header-fixed page-sidebar-closed-hide-logo page-container-bg-solid page-content-white">
    <form runat="server" id="form1">
        <!-- BEGIN HEADER -->
        <div class="page-header navbar navbar-fixed-top">
            <!-- BEGIN HEADER INNER -->
            <div class="page-header-inner ">
                <!-- BEGIN LOGO -->
                <div class="page-logo">
                    <a href="#">
                        <img src="assets/layouts/layout/img/logo.png" alt="logo" class="logo-default" />
                    </a>
                    <!--<div class="menu-toggler sidebar-toggler"> </div>-->
                </div>
                <!-- END LOGO -->
                <div class="">
                    <ul class="nav navbar-nav pull-left">
                        <asp:Menu ID="Menu1" runat="server" StaticSubMenuIndent="16px" Orientation="Horizontal" CssClass="dropdown"
                            OnMenuItemDataBound="mainMenu_MenuItemDataBound" Style="margin-top: 0; color: #808080;"
                            Font-Size="Small" ForeColor="#0033CC" StaticMenuItemStyle-Font-Bold="true"
                            StaticMenuItemStyle-CssClass="nav navbar-nav"
                            StaticEnableDefaultPopOutImage="false" StaticMenuItemStyle-ForeColor="#0033CC"
                            StaticMenuItemStyle-HorizontalPadding="0px" StaticMenuItemStyle-VerticalPadding="0px"
                            DynamicMenuStyle-CssClass="dropdown-menu dropdown-menu-default" StaticMenuItemStyle-BorderColor="White"
                            StaticMenuItemStyle-ItemSpacing="20px" DynamicMenuStyle-BackColor="#337ab7"
                            DynamicMenuStyle-BorderColor="White" DynamicMenuStyle-HorizontalPadding="2px"
                            DynamicHoverStyle-BackColor="White" DynamicMenuItemStyle-Font-Size="Small"
                            DynamicHoverStyle-Font-Underline="false" DynamicHoverStyle-Font-Bold="true"
                            DynamicHoverStyle-ForeColor="#337ab7"
                            DynamicMenuStyle-Width="200px" DynamicMenuItemStyle-VerticalPadding="2px"
                            DynamicMenuItemStyle-BorderColor="White" DynamicMenuItemStyle-HorizontalPadding="5px"
                            DynamicMenuItemStyle-Width="200px" DynamicMenuItemStyle-ForeColor="White" DynamicSelectedStyle-ForeColor="#007FFF"
                            ItemWrap="true">
                        </asp:Menu>
                    </ul>
                </div>
                <!-- BEGIN RESPONSIVE MENU TOGGLER -->
                <a href="javascript:;" class="menu-toggler responsive-toggler" data-toggle="collapse" data-target=".navbar-collapse"></a>
                <!-- END RESPONSIVE MENU TOGGLER -->
                <!-- BEGIN TOP NAVIGATION MENU -->
                <div class="top-menu">
                    <ul class="nav navbar-nav pull-right">
                        <!-- BEGIN USER LOGIN DROPDOWN -->
                        <!-- DOC: Apply "dropdown-dark" class after below "dropdown-extended" to change the dropdown styte -->
                        <li class="dropdown dropdown-user">
                            <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                                <%--<img alt="" class="img-circle" src="assets/layouts/layout/img/avatar3_small.jpg" />--%>
                                <span class="glyphicon glyphicon-user"></span>
                                <span class="username username-hide-on-mobile">
                                    <asp:Label ID="lblSess" runat="server" Text=""></asp:Label></span>
                                <i class="fa fa-angle-down"></i>
                            </a>
                            <ul class="dropdown-menu dropdown-menu-default">
                                <li>
                                    <a href="ChangePassword.aspx">
                                        <i class="icon-user"></i>Change Password
                                    </a>
                                </li>
                                <li>
                                    <a href="Credit/ApplicationView.aspx">
                                        <i class="icon-envelope-open"></i>My Inbox
                                    <span class="badge badge-danger">
                                        <asp:Label ID="lblInbox" runat="server" Text="0"></asp:Label></span>
                                    </a>
                                </li>
                                <%--<li>
                                    <a href="app_todo.html">
                                        <i class="icon-rocket"></i>My Tasks
                                    <span class="badge badge-success">7 </span>
                                    </a>
                                </li>--%>
                                <li class="divider"></li>
                                <%--<li>
                                    <a href="LockScreen.aspx">
                                        <i class="icon-lock"></i>Lock Screen
                                    </a>
                                </li>--%>
                                <li>
                                    <a href="Logout.aspx">
                                        <i class="icon-key"></i>Log Out
                                    </a>
                                </li>
                            </ul>
                        </li>
                        <!-- END USER LOGIN DROPDOWN -->
                    </ul>
                </div>
                <!-- END TOP NAVIGATION MENU -->
            </div>
            <!-- END HEADER INNER -->
        </div>
        <!-- END HEADER -->
        <!-- BEGIN HEADER & CONTENT DIVIDER -->
        <%--<div class="clearfix"></div>--%>
        <!-- END HEADER & CONTENT DIVIDER -->
        <!-- BEGIN CONTAINER -->
        <div class="page-container">
            <!-- BEGIN CONTENT -->
            <div class="page-content-wrapper">
                <!-- BEGIN CONTENT BODY -->
                <div class="page-content">
                    <!-- BEGIN PAGE HEADER-->
                    <!-- BEGIN PAGE BAR -->
                    <%--<div class="page-bar">
                        <ul class="page-breadcrumb">
                            <li>
                                <a href="index.html">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>Dashboard</span>
                            </li>
                        </ul>
                        <!--<div class="page-toolbar">
                        <div id="dashboard-report-range" class="pull-right tooltips btn btn-sm" data-container="body" data-placement="bottom" data-original-title="Change dashboard date range">
                            <i class="icon-calendar"></i>&nbsp;
                            <span class="thin uppercase hidden-xs"></span>&nbsp;
                            <i class="fa fa-angle-down"></i>
                        </div>
                    </div>-->
                    </div>--%>
                    <!-- END PAGE BAR -->
                    <!-- BEGIN PAGE TITLE-->
                    <h2 class="text-center bold" style="color: #336699">360&deg; Credit Management System</h2>
                    <!-- END PAGE TITLE-->
                    <!-- END PAGE HEADER-->
                    <div class="clearfix row" style="height: 10px;"></div>
                    <div class="row">
                        <div class="col-xs-4">
                            <h3 class="page-title font-blue-steel bold">
                                <asp:Label ID="lblDashboardView" runat="server" Text="Statistics"></asp:Label>
                                <small class="font-blue-steel">
                                    <asp:Label ID="lblSubInfo" runat="server" Text=""></asp:Label>
                                </small>
                            </h3>
                        </div>
                        <div class="col-xs-8" id="divOverallView" runat="server" style="margin: 25px 0;">
                            <div class="row">
                                <div class="col-xs-2"></div>
                                <h4 class="font-blue-steel col-xs-2 control-label text-right">View By
                                </h4>
                                <div class="font-blue-steel col-xs-4">
                                    <asp:RadioButtonList ID="rdbViewBy" runat="server" RepeatDirection="Horizontal" CssClass="col-xs-12" AutoPostBack="true">
                                        <asp:ListItem Text="Loan Officer" Value="Officer"></asp:ListItem>
                                        <asp:ListItem Text="Branch" Value="Branch"></asp:ListItem>
                                        <asp:ListItem Text="Institution" Value="Institution"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-xs-4">
                                    <asp:DropDownList ID="cmbViewDetails" runat="server" CssClass="form-control input-sm col-xs-12" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <a href="Credit/ApplicationView.aspx">
                            <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                                <div class="dashboard-stat2 ">
                                    <div class="display">
                                        <div class="number">
                                            <h4 class="font-purple-soft bold">
                                                <span data-counter="counterup" data-value="<%= lblAppCount.Text %>">0</span>
                                                <asp:Label ID="lblAppCount" runat="server" Text="0" Visible="false"></asp:Label>
                                            </h4>
                                            <small class="font-dark">INCOMING APPLICATIONS</small>
                                        </div>
                                    </div>
                                    <div class="progress-info">
                                        <div class="progress">
                                            <span style="width: 100%;" class="progress-bar progress-bar-success purple-soft"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                        <a href="Credit/frmReports.aspx">
                            <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                                <div class="dashboard-stat2 ">
                                    <div class="display">
                                        <div class="number">
                                            <h4 class="font-blue-steel bold">
                                                <small class="font-blue-steel">ZMW</small>
                                                <span data-counter="counterup" data-value="<%= lblDisbAmt.Text%>">0</span>
                                                <asp:Label ID="lblDisbAmt" runat="server" Text="0" Visible="false"></asp:Label>
                                            </h4>
                                            <small class="font-dark">
                                                <asp:Label ID="lblDisbCount" runat="server" Text="0" Visible="false"></asp:Label>
                                                <span data-counter="counterup" data-value="<%= lblDisbCount.Text%>">0</span>&nbsp;TOTAL DISBURSEMENTS</small>
                                        </div>
                                    </div>
                                    <div class="progress-info">
                                        <div class="progress">
                                            <span style="width: 100%;" class="progress-bar progress-bar-success blue-steel"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                        <a href="Credit/frmReports.aspx">
                            <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                                <div class="dashboard-stat2 ">
                                    <div class="display">
                                        <div class="number">
                                            <h4 class="font-red-haze bold">
                                                <small class="font-red-haze">ZMW</small>
                                                <span data-counter="counterup" data-value="<%= lblRepayAmt.Text%>">0</span>
                                                <asp:Label ID="lblRepayAmt" runat="server" Text="0" Visible="false"></asp:Label>
                                            </h4>
                                            <small class="font-dark">
                                                <asp:Label ID="lblRepayCount" runat="server" Text="0" Visible="false"></asp:Label>
                                                <span data-counter="counterup" data-value="<%= lblRepayCount.Text%>">0</span>&nbsp;TOTAL REPAYMENTS</small>
                                        </div>
                                    </div>
                                    <div class="progress-info">
                                        <div class="progress">
                                            <span style="width: 100%;" class="progress-bar progress-bar-success red-haze"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                        <a href="Credit/frmReports.aspx">
                            <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                                <div class="dashboard-stat2">
                                    <div class="display">
                                        <div class="number">
                                            <h4 class=" font-yellow-lemon  bold">
                                                <small class="font-yellow-lemon">ZMW</small>
                                                <span data-counter="counterup" data-value="<%= lblGLP.Text %>">0</span>
                                                <asp:Label ID="lblGLP" runat="server" Text="0" Visible="false"></asp:Label>
                                            </h4>
                                            <small class="font-dark">GROSS LOAN PORTFOLIO</small>
                                        </div>
                                    </div>
                                    <div class="progress-info">
                                        <div class="progress">
                                            <span style="width: 100%;" class="progress-bar progress-bar-success yellow-lemon"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                        <a href="Credit/frmReports.aspx">
                            <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                                <div class="dashboard-stat2 ">
                                    <div class="display">
                                        <div class="number row">
                                            <div class="text-left col-xs-5">
                                                <h4 class="font-green-sharp bold">
                                                    <span data-counter="counterup" data-value="<%= lblCaseload.Text%>">0</span>
                                                    <asp:Label ID="lblCaseload" runat="server" Text="0" Visible="false"></asp:Label>
                                                </h4>
                                                <small class="font-dark">CASELOAD</small>
                                            </div>
                                            <div class="text-right col-xs-7">
                                                <h4 class="font-green-sharp bold">
                                                    <span data-counter="counterup" data-value="<%= lblPAR30.Text%>">0</span>
                                                    <small class="font-green-sharp">%</small>
                                                    <asp:Label ID="lblPAR30" runat="server" Text="0" Visible="false"></asp:Label>
                                                </h4>
                                                <small class="font-dark">PAR30</small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="progress-info">
                                        <div class="progress">
                                            <span style="width: 100%;" class="progress-bar progress-bar-success green-sharp"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                        <a href="Credit/frmReports.aspx">
                            <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                                <div class="dashboard-stat2 ">
                                    <div class="display">
                                        <div class="number">
                                            <h4 class="font-blue-sharp bold">
                                                <span data-counter="counterup" data-value="<%= lblTotalAppCount.Text%>">0</span>
                                                <asp:Label ID="lblTotalAppCount" runat="server" Text="0" Visible="false"></asp:Label>
                                            </h4>
                                            <small class="font-dark">TOTAL APPLICATIONS</small>
                                        </div>
                                    </div>
                                    <div class="progress-info">
                                        <div class="progress">
                                            <span style="width: 100%;" class="progress-bar progress-bar-success blue-sharp"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-sm-6">
                            <div class="portlet light ">
                                <div class="portlet-title">
                                    <div class="caption font-red">
                                        <span class="caption-subject bold uppercase">Disbursements/Repayments</span>
                                        <span class="caption-helper">week by week analysis (amount)</span>
                                    </div>
                                    <%--<div class="actions">
                                        <a href="#" class="btn btn-circle green btn-outline btn-sm">
                                            <i class="fa fa-pencil"></i>Export
                                        </a>
                                        <a href="#" class="btn btn-circle green btn-outline btn-sm">
                                            <i class="fa fa-print"></i>Print
                                        </a>
                                    </div>--%>
                                </div>
                                <div class="portlet-body">
                                    <div id="dataChart" class="CSSAnimationChart"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-6">
                            <div class="portlet light ">
                                <div class="portlet-title">
                                    <div class="caption font-green">
                                        <span class="caption-subject bold uppercase">Disbursements/Repayments</span>
                                        <span class="caption-helper">number of transactions weekly analysis</span>
                                    </div>
                                    <%--<div class="actions">
                                        <a class="btn btn-circle btn-icon-only btn-default" href="#">
                                            <i class="icon-cloud-upload"></i>
                                        </a>
                                        <a class="btn btn-circle btn-icon-only btn-default" href="#">
                                            <i class="icon-wrench"></i>
                                        </a>
                                        <a class="btn btn-circle btn-icon-only btn-default" href="#">
                                            <i class="icon-trash"></i>
                                        </a>
                                        <a class="btn btn-circle btn-icon-only btn-default fullscreen" href="#"></a>
                                    </div>--%>
                                </div>
                                <div class="portlet-body">
                                    <div id="countChart" class="CSSAnimationChart"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END CONTENT BODY -->
            </div>
            <!-- END CONTENT -->
        </div>
        <!-- END CONTAINER -->
        <!-- BEGIN FOOTER -->
        <div class="page-footer">
            <div class="page-footer-inner">
                &copy;2016 <a href="http://www.escrowsystems.net" target="new">Escrow Systems.</a>
                <%--<a href="http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes" title="Purchase Metronic just for 27ZMW and get lifetime updates for free" target="_blank">Purchase Metronic!</a>--%>
            </div>
            <div class="scroll-to-top">
                <i class="icon-arrow-up"></i>
            </div>
        </div>
        <!-- END FOOTER -->
        <div class="modal fade" id="idle-timeout-dialog" data-backdrop="static">
            <div class="modal-dialog modal-small">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Your session is about to expire.</h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            <i class="fa fa-warning text-danger"></i>You session will be logged out in

                            <span id="idle-timeout-counter"></span>&nbsp; seconds.
                        </p>
                        <p>Do you want to continue your session? </p>
                    </div>
                    <div class="modal-footer">
                        <button id="idle-timeout-dialog-logout" type="button" class="btn btn-warning btn-sm text-uppercase">No, Logout</button>
                        <button id="idle-timeout-dialog-keepalive" type="button" class="btn btn-success btn-sm text-uppercase" data-dismiss="modal">Yes, Keep Working</button>
                    </div>
                </div>
            </div>
        </div>
        <!--[if lt IE 9]>
    <![endif]-->
        <!-- BEGIN CORE PLUGINS -->
        <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <!-- END CORE PLUGINS -->
        <!-- BEGIN PAGE LEVEL PLUGINS -->
        <script src="assets/global/plugins/morris/raphael-min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/morris/morris.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/counterup/jquery.waypoints.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/counterup/jquery.counterup.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amcharts/amcharts.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amcharts/serial.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amcharts/pie.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amcharts/radar.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amcharts/themes/light.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amcharts/themes/patterns.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amcharts/themes/chalk.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amstockcharts/amstock.js" type="text/javascript"></script>
        <script type="text/javascript" src="assets/global/plugins/jquery-idle-timeout/jquery.idletimeout.js"></script>
        <script type="text/javascript" src="assets/global/plugins/jquery-idle-timeout/jquery.idletimer.js"></script>
        <script type="text/javascript" src="assets/pages/scripts/ui-idletimeout.min.js"></script>
        <!-- END PAGE LEVEL PLUGINS -->
        <!-- BEGIN THEME GLOBAL SCRIPTS -->
        <script src="assets/global/scripts/app.min.js" type="text/javascript"></script>
        <!-- END THEME GLOBAL SCRIPTS -->
        <!-- BEGIN PAGE LEVEL SCRIPTS -->
        <script src="assets/pages/scripts/dashboard.min.js" type="text/javascript"></script>
        <!-- END PAGE LEVEL SCRIPTS -->
        <!-- BEGIN THEME LAYOUT SCRIPTS -->
        <script src="assets/layouts/layout/scripts/layout.min.js" type="text/javascript"></script>
        <!-- END THEME LAYOUT SCRIPTS -->
        <script type="text/javascript">
            $(document).ready(function () {
                $.ajax({
                    url: "DashboardData.asmx/GetWeeklyData",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (chartData) {
                        //debugger;
                        var chart = AmCharts.makeChart("dataChart", {
                            "type": "serial",
                            "addClassNames": true,
                            "theme": "light",
                            "path": "assets/global/plugins/amcharts/ammap/images/",
                            "autoMargins": true,
                            //"marginLeft": 50,
                            //"marginRight": 8,
                            //"marginTop": 10,
                            //"marginBottom": 26,
                            "balloon": {
                                "adjustBorderColor": false,
                                "horizontalPadding": 10,
                                "verticalPadding": 8,
                                "color": "#ffffff"
                            },
                            "dataProvider": JSON.parse(chartData.d),
                            "valueAxes": [{
                                "axisAlpha": 0,
                                "position": "left",
                                "title": "Amount [USZMW]"//,
                                //"minimum": 0,
                                //"maximum": 8000
                            }],
                            "startDuration": 1,
                            "graphs": [{
                                "alphaField": "alpha",
                                "balloonText": "<span style='font-size:12px;'>[[title]] for week ended [[category]]:<br><span style='font-size:20px;'>[[value]]</span> [[additional]]</span>",
                                "fillAlphas": 1,
                                "title": "Value of Disbursements",
                                "type": "column",
                                "valueField": "DisbAmt"
                            }, {
                                "id": "graph2",
                                "balloonText": "<span style='font-size:12px;'>[[title]] for week ended [[category]]:<br><span style='font-size:20px;'>[[value]]</span> [[additional]]</span>",
                                "bullet": "round",
                                "lineThickness": 3,
                                "bulletSize": 7,
                                "bulletBorderAlpha": 1,
                                "bulletColor": "#FFFFFF",
                                "useLineColorForBulletBorder": true,
                                "bulletBorderThickness": 3,
                                "fillAlphas": 0,
                                "lineAlpha": 1,
                                "title": "Value of Repayments",
                                "valueField": "RepayAmt"
                            }],
                            "categoryField": "WeekEnding",
                            "categoryAxis": {
                                "gridPosition": "start",
                                "axisAlpha": 0,
                                "tickLength": 0,
                                "title": "Week ended",
                                "labelRotation": 45
                            },
                            "export": {
                                "enabled": true
                            }
                        });
                    },
                    error: function (xhr, status, error) {
                        alert(status);
                    }
                });
            });
            $(document).ready(function () {
                $.ajax({
                    url: "DashboardData.asmx/GetWeeklyData",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (chartData) {
                        //debugger;
                        var chart = AmCharts.makeChart("countChart", {
                            "type": "serial",
                            "addClassNames": true,
                            "theme": "light",
                            "path": "assets/global/plugins/amcharts/ammap/images/",
                            "autoMargins": true,
                            //"marginLeft": 30,
                            //"marginRight": 8,
                            //"marginTop": 10,
                            //"marginBottom": 26,
                            "balloon": {
                                "adjustBorderColor": false,
                                "horizontalPadding": 10,
                                "verticalPadding": 8,
                                "color": "#ffffff"
                            },
                            "dataProvider": JSON.parse(chartData.d),
                            "valueAxes": [{
                                "axisAlpha": 0,
                                "position": "left",
                                "title": "Number of transactions"
                                //"minimum": 0,
                                //"maximum": 100
                            }],
                            "startDuration": 1,
                            "graphs": [{
                                "alphaField": "alpha",
                                "balloonText": "<span style='font-size:12px;'>[[title]] for week ended [[category]]:<br><span style='font-size:20px;'>[[value]]</span> [[additional]]</span>",
                                "fillAlphas": 1,
                                "title": "Number of Disbursements",
                                "type": "column",
                                "valueField": "DisbCount"
                            }, {
                                "id": "graph2",
                                "balloonText": "<span style='font-size:12px;'>[[title]] for week ended [[category]]:<br><span style='font-size:20px;'>[[value]]</span> [[additional]]</span>",
                                "bullet": "round",
                                "lineThickness": 3,
                                "bulletSize": 7,
                                "bulletBorderAlpha": 1,
                                "bulletColor": "#FFFFFF",
                                "useLineColorForBulletBorder": true,
                                "bulletBorderThickness": 3,
                                "fillAlphas": 0,
                                "lineAlpha": 1,
                                "title": "Number of Repayments",
                                "valueField": "RepayCount"
                            }],
                            "categoryField": "WeekEnding",
                            "categoryAxis": {
                                "gridPosition": "start",
                                "axisAlpha": 0,
                                "tickLength": 0,
                                "title": "Week ended",
                                "labelRotation": 45
                            },
                            "export": {
                                "enabled": true
                            }
                        });
                    },
                    error: function (xhr, status, error) {
                        alert(status);
                    }
                });
            });
        </script>
    </form>
</body>
</html>