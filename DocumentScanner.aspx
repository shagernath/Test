<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="DocumentScanner.aspx.vb" Inherits="DocumentScanner" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/scanner.css" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="container" class="body_Broad_width" style="margin: 0 auto;">
        <div id="headcontainer" class="body_Broad_width" style="background-color: #3a3a3a;border: 0; padding: 0;">
            <!-- DWT_PageHead.js is used to initiate the head of the sample page. Not necessary!
            <br /><script type="text/javascript" src="Scripts/DWT_PageHead.js"></script>-->
        </div>

        <div id="DWTcontainer" class="body_Broad_width" style="background-color: #ffffff;
            height: 810px; border: 0;">
            <!--This is where Dynamic Web TWAIN control will be rendered.-->
            <div id="dwtcontrolContainer">
                <div id="DWTContainerID" style="position: relative;" class='divcontrol'>
                </div>
                <div id="DWTNonInstallContainerID" style="width: 580px">
                </div>
            </div>

            <!--This is where you add the actual buttons to control the component.-->
            <div id="ScanWrapper">
                <input id="btnScan" class="bigbutton" style="color: #FE8E14;" type="button" value="Scan" onclick="DW_AcquireImage();" />
                <input id="btnUpload" class="bigbutton" style="color: #FE8E14;" type="button" value="Upload" onclick="btnUpload_onclick();" />
                <!--<div id="divInfo">-->
                </div>
            </div>
        </div>
    <script type="text/javascript" language="javascript" src="Scripts/DWT_BasicPageInitiate.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/DWTSample_BasicScanUpload.js"></script>
</asp:Content>

