<%@ Page Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="ParaCreditProducts.aspx.vb" Inherits="ParaCreditProducts" Title="Credit Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .btn-default.active, .btn-default:active, .open > .dropdown-toggle.btn-default {
            /*color: blue;*/
            background-color: #adadad;
            border-color: #adadad;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a>Credit Products
                </a>
            </h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label5" runat="server"
                        Text="Product Type"></asp:Label>
                </div>
                <div class="col-xs-3">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbProductType" runat="server">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="Personal Loan" Value="Personal"></asp:ListItem>
                        <asp:ListItem Text="Mortgage" Value="Mortgage"></asp:ListItem>
                        <asp:ListItem Text="Invoice Discount" Value="Invoice"></asp:ListItem>
                        <asp:ListItem Text="Working Capital" Value="Working Capital"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-1">
                    <button type="button" class="btn btn-info btn-sm" data-toggle="modal" data-target="#ProductTypeModal">Add</button>
                </div>
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label18" runat="server"
                        Text="Display Name"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtDisplayName" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label7" runat="server"
                        Text="Minimum Amount"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm numeric" ID="txtMinAmt" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label8" runat="server"
                        Text="Maximum Amount"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm numeric" ID="txtMaxAmt" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row label-info">
                <div class="col-xs-12 control-label">
                    Interest Calculation Parameters
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label1" runat="server"
                        Text="Minimum Interest Rate"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm numeric" ID="txtMinimumIntRate" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label2" runat="server"
                        Text="Maximum Interest Rate"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm numeric" ID="txtMaximumIntRate" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    Default Interest Rate
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm numeric" ID="txtDefaultInterestRate" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 control-label">
                    Default Interest Calculation Interval
                </div>
                <div class="col-xs-8">
                    <asp:RadioButtonList ID="rdbInterestInterval" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="Daily" Value="Daily"></asp:ListItem>
                        <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
                        <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                        <asp:ListItem Text="Annual" Value="Annual"></asp:ListItem>
                        <asp:ListItem Text="Loan Duration" Value="Duration"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3 control-label">
                    Interest Calculation Method
                </div>
                <div class="col-xs-9">
                    <asp:RadioButtonList ID="rdbInterestCalcMethod" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="Fixed/Flat" Value="Fixed"></asp:ListItem>
                        <asp:ListItem Text="Declining Balance" Value="Declining"></asp:ListItem>
                        <asp:ListItem Text="Declining Balance with equal instalments" Value="Balance"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3 control-label">
                    Interest Applied in Client Account
                </div>
                <div class="col-xs-9">
                    <asp:RadioButtonList ID="rdbInterestTrigger" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="On disbursement" Value="Disbursement"></asp:ListItem>
                        <asp:ListItem Text="Disbursement Date Anniversary" Value="Anniversary"></asp:ListItem>
                        <asp:ListItem Text="Month-end" Value="Month-end"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3 control-label">
                    Number of days in year
                </div>
                <div class="col-xs-4">
                    <asp:RadioButtonList ID="rdbYearDays" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="360 days" Value="360"></asp:ListItem>
                        <asp:ListItem Text="365 days" Value="365"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row label-info">
                <div class="col-xs-12 control-label">
                    Repayment Scheduling
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label3" runat="server"
                        Text="Minimum Tenure"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm numeric" ID="txtMinimumTenure" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-2 control-label">
                    <asp:Label ID="Label4" runat="server"
                        Text="Maximum Tenure"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm numeric" ID="txtMaximumTenure" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    Default Tenure
                </div>
                <div class="col-xs-4">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm numeric" ID="txtDefaultTenure" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    Repayment Intervals
                </div>
                <div class="col-xs-2">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm numeric" ID="txtRepaymentInterval" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbRepaymentInterval" runat="server">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="Days" Value="Days"></asp:ListItem>
                        <asp:ListItem Text="Weeks" Value="Weeks"></asp:ListItem>
                        <asp:ListItem Text="Months" Value="Months"></asp:ListItem>
                        <asp:ListItem Text="Years" Value="Years"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2 control-label">
                    Has Grace Period
                </div>
                <div class="col-xs-2">
                    <asp:RadioButtonList ID="rdbGracePeriod" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row" id="divGracePeriod">
                <div class="col-xs-2 control-label">
                    Grace Period Type
                </div>
                <div class="col-xs-4">
                    <asp:RadioButtonList ID="rdbGracePeriodType" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="Principal Grace Period" Value="Principal"></asp:ListItem>
                        <asp:ListItem Text="Pure Grace Period" Value="Pure"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="col-xs-2 control-label">
                    Grace Period Length
                </div>
                <div class="col-xs-2">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm numeric" ID="txtGracePerLength" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbGracePerLength" runat="server">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="Days" Value="days"></asp:ListItem>
                        <asp:ListItem Text="Weeks" Value="weeks"></asp:ListItem>
                        <asp:ListItem Text="Months" Value="months"></asp:ListItem>
                        <asp:ListItem Text="Years" Value="years"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3 control-label">
                    Allow Repayment on weekend
                </div>
                <div class="col-xs-2">
                    <asp:RadioButtonList ID="rdbRepayWknd" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row" id="divRepayWknd">
                <div class="col-xs-4 control-label">
                    What to do if repayment falls on weekend/holiday
                </div>
                <div class="col-xs-8">
                    <asp:RadioButtonList ID="rdbRepaymentWknd" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="Move repayment to next working day" Value="Next"></asp:ListItem>
                        <asp:ListItem Text="Move repayment to previous working day" Value="Previous"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3 control-label">
                    Allow editing of payment schedule
                </div>
                <div class="col-xs-2">
                    <asp:RadioButtonList ID="rdbEditPaySchedule" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3 control-label">
                    Repayment Allocation Order
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbRepaymentAllocationOrder1" runat="server">
                        <asp:ListItem Text="Principal" Value="Principal"></asp:ListItem>
                        <asp:ListItem Text="Interest" Value="Interest"></asp:ListItem>
                        <asp:ListItem Text="Fees" Value="Fees"></asp:ListItem>
                        <asp:ListItem Text="Penalties" Value="Penalties"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbRepaymentAllocationOrder2" runat="server">
                        <asp:ListItem Text="Principal" Value="Principal"></asp:ListItem>
                        <asp:ListItem Text="Interest" Value="Interest"></asp:ListItem>
                        <asp:ListItem Text="Fees" Value="Fees"></asp:ListItem>
                        <asp:ListItem Text="Penalties" Value="Penalties"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbRepaymentAllocationOrder3" runat="server">
                        <asp:ListItem Text="Principal" Value="Principal"></asp:ListItem>
                        <asp:ListItem Text="Interest" Value="Interest"></asp:ListItem>
                        <asp:ListItem Text="Fees" Value="Fees"></asp:ListItem>
                        <asp:ListItem Text="Penalties" Value="Penalties"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbRepaymentAllocationOrder4" runat="server">
                        <asp:ListItem Text="Principal" Value="Principal"></asp:ListItem>
                        <asp:ListItem Text="Interest" Value="Interest"></asp:ListItem>
                        <asp:ListItem Text="Fees" Value="Fees"></asp:ListItem>
                        <asp:ListItem Text="Penalties" Value="Penalties"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    Tolerance Period
                </div>
                <div class="col-xs-2">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtTolerancePeriod" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbTolerancePeriod" runat="server">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="Days" Value="days"></asp:ListItem>
                        <asp:ListItem Text="Weeks" Value="weeks"></asp:ListItem>
                        <asp:ListItem Text="Months" Value="months"></asp:ListItem>
                        <asp:ListItem Text="Years" Value="years"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-3 control-label">
                    Days in arrears include non-working days
                </div>
                <div class="col-xs-2">
                    <asp:RadioButtonList ID="rdbArrearNonWorking" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    Penalties to be charged
                </div>
                <div class="col-xs-2">
                    <asp:RadioButtonList ID="rdbPenaltyCharged" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row" id="divPenaltyCharged">
                <div class="col-xs-2 control-label">
                    Penalty Option
                </div>
                <div class="col-xs-5">
                    <asp:RadioButtonList ID="rdbPenaltyOption" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="No penalty" Value="No"></asp:ListItem>
                        <asp:ListItem Text="Once-off Percentage" Value="Once"></asp:ListItem>
                        <asp:ListItem Text="Penalty Interest Rate" Value="Rate"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="col-xs-1 control-label">
                    Amount to Penalise
                </div>
                <div class="col-xs-4">
                    <asp:RadioButtonList ID="rdbAmtToPenalise" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="Principal Only" Value="Principal"></asp:ListItem>
                        <asp:ListItem Text="Principal + Interest" Value="All"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    Penalty Rate (%)
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtPenaltyRate" runat="server" CssClass="col-xs-12 input-sm form-control numeric"></asp:TextBox>
                </div>
                <div class="col-xs-2 control-label">
                    Penalty Interval
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList ID="cmbPenaltyInterval" CssClass="col-xs-12 input-sm form-control" runat="server">
                        <asp:ListItem Text="Daily" Value="Daily"></asp:ListItem>
                        <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
                        <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                        <asp:ListItem Text="Annual" Value="Annual"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    Product Fees
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList CssClass="col-xs-12 input-sm form-control" ID="cmbProductFees" runat="server">
                        <asp:ListItem Text="None" Value="None"></asp:ListItem>
                        <asp:ListItem Text="Manual" Value="Manual"></asp:ListItem>
                        <asp:ListItem Text="Deducted during Disbursement" Value="Deducted"></asp:ListItem>
                        <asp:ListItem Text="Capitalized on Disbursement" Value="Capitalized"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <%--<div class="col-xs-8">
                    <asp:RadioButtonList ID="rdbProductFees" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="None" Value="None"></asp:ListItem>
                        <asp:ListItem Text="Manual" Value="Manual"></asp:ListItem>
                        <asp:ListItem Text="Deducted during Disbursement" Value="Deducted"></asp:ListItem>
                        <asp:ListItem Text="Capitalized on Disbursement" Value="Capitalized"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>--%>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    Product Fee Calculation
                </div>
                <div class="col-xs-5">
                    <asp:RadioButtonList ID="rdbProductFeeCalc" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="Fixed Amount" Value="Fixed"></asp:ListItem>
                        <asp:ListItem Text="Disbursement Amount Percentage" Value="Percentage"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="col-xs-2 control-label">
                    Percentage/Amount
                </div>
                <div class="col-xs-3">
                    <asp:TextBox CssClass="col-xs-12 form-control input-sm numeric" ID="txtProductFee" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-2 control-label">
                    Date Created
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtCreatedDate" runat="server" CssClass="form-control input-sm nofuturedate"></asp:TextBox>
                    <span class="glyphicon glyphicon-calendar form-control-feedback" style="margin-right: 13px; background-color: #eeeeff; border-radius: 0 3px 3px 0; border: 1px solid rgb(149,188,219); z-index: 0;"></span>
                </div>
                <div class="col-xs-2 control-label">
                    <asp:CheckBox runat="server" Text="Active" ID="chkActive" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 text-center">
                    <asp:Button CssClass="btn btn-primary btn-sm save-btn" ID="btnAddProductType" runat="server" Text="Save" UseSubmitBehavior="false" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 center-block">
                    <asp:GridView ID="grdProductTypes" runat="server" DataKeyNames="ID"
                        EmptyDataText="No product type added yet" HorizontalAlign="Center" Width="90%">
                        <SelectedRowStyle BackColor="Azure" Font-Bold="true" />
                        <AlternatingRowStyle CssClass="altrowstyle" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                        <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" />
                        <RowStyle CssClass="rowstyle" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div id="ProductTypeModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Add Product Type</h4>
                </div>
                <div class="modal-body panel-body small">
                    <div class="row">
                        <div class="col-xs-2 control-label">
                            <asp:Label ID="Label106" runat="server" Text="Product"></asp:Label>
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtProduct" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="reqProduct" runat="server" ErrorMessage="Product is required" Font-Bold="true" ForeColor="Red" ControlToValidate="txtProduct" ValidationGroup="valProduct"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-xs-1">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btnAddProduct" runat="server" Text="Add" ValidationGroup="valProduct" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('[id*=rdbGracePeriod] input').click(function () {
                var value = $('[id*=rdbGracePeriod] input:checked').val();
                if (value == 'Y') {
                    $("#divGracePeriod").show();
                }
                else if (value == 'N') {
                    $("#divGracePeriod").hide();
                }
                else {
                    $("#divGracePeriod").hide();
                }
            });
        });

        $(document).ready(function () {
            var value = $('[id*=rdbGracePeriod] input:checked').val();
            if (value == 'Y') {
                $("#divGracePeriod").show();
            }
            else if (value == 'N') {
                $("#divGracePeriod").hide();
            }
            else {
                $("#divGracePeriod").hide();
            }
        });
        $(document).ready(function () {
            $('[id*=rdbRepayWknd] input').click(function () {
                var value = $('[id*=rdbRepayWknd] input:checked').val();
                if (value == 'N') {
                    $("#divRepayWknd").show();
                }
                else if (value == 'Y') {
                    $("#divRepayWknd").hide();
                }
                else {
                    $("#divRepayWknd").hide();
                }
            });
        });

        $(document).ready(function () {
            var value = $('[id*=rdbRepayWknd] input:checked').val();
            if (value == 'N') {
                $("#divRepayWknd").show();
            }
            else if (value == 'Y') {
                $("#divRepayWknd").hide();
            }
            else {
                $("#divRepayWknd").hide();
            }
        });
        //$(function () {
        //    $("[id*=btnAddProductType]").bind("click", function () {
        //        $("[id*=btnAddProductType]").val("Saving...");
        //        $("[id*=btnAddProductType]").attr("disabled", true);
        //    });
        //});
    </script>
</asp:Content>