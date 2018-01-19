<%@ Page Title="Loan Disbursement" Language="VB" MasterPageFile="~/Site2.master" AutoEventWireup="false" CodeFile="LoanDisbursement.aspx.vb" Inherits="Credit_LoanDisbursement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            margin-left: 22;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">Applications ready for Disbursement
                <asp:Label ID="lblAppCount" runat="server" Text="0" CssClass="badge"></asp:Label>
            </h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <asp:GridView ID="grdDisbursements" runat="server" HorizontalAlign="center" EmptyDataText="No applications ready for disbursement" AllowPaging="true">
                    <AlternatingRowStyle CssClass="altrowstyle" />
                    <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" />
                    <RowStyle CssClass="rowstyle" />
                    <PagerStyle CssClass="pagination-ys" />
                    <SelectedRowStyle Font-Bold="true" BackColor="SeaShell" />
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"
                                    CommandName="Select" Text="Select" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="panel panel-primary small">
        <div class="panel-heading">
            <h4 class="panel-title">Loan Disbursement
            </h4>
        </div>
        <div id="collapse-one" class="panel-collapse collapse in">
            <div class="panel-body">
                <div class="row">
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="Label2" runat="server" Text="Customer Number"></asp:Label>
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtCustNo" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-1">
                    </div>
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="Label1" runat="server" Text="Client Type"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:DropDownList Enabled="false" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="rdbClientType" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2 control-label" style="height: 17px">
                        <asp:Label ID="Label21" runat="server" Text="Branch"></asp:Label>
                    </div>
                    <div class="col-xs-4 left control-label">
                        <asp:Label ID="lblBranchCode" runat="server" Text=""></asp:Label>
                        <asp:Label ID="Label53" runat="server" Text="   "></asp:Label>
                        <asp:Label ID="lblBranchName" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="control-label col-xs-2">
                        Name
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" ID="txtName" runat="server" CssClass="form-control input-sm col-xs-12"></asp:TextBox>
                    </div>
                    <strong>CASH BOX</strong>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="auto-style1" Font-Size="Large" Font-Bold="true" style="text-align:center"  Enabled="False" Height="45px" Width="436px"></asp:TextBox>
                </div>
                <div class="row">
                    <div class="col-xs-2 control-label">
                        Applicant Category
                    </div>
                    <div class="col-xs-6 text-center control-label">
                        <asp:RadioButtonList Enabled="false" ForeColor="Black" Font-Bold="true" ID="rdbSubIndividual" runat="server"
                            RepeatDirection="Horizontal" AutoPostBack="True" CssClass="col-xs-12">
                            <asp:ListItem Text="PMEC" Value="PMEC"></asp:ListItem>
                            <asp:ListItem Text="Bankers" Value="Bankers"></asp:ListItem>
                            <asp:ListItem Text="PDAs" Value="PDAs"></asp:ListItem>
                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="lblMinDept" runat="server" Text="Ministry/Department" Visible="False"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtMinDept" runat="server" Visible="False"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="lblMinDeptNo" runat="server" Text="Min/Dept No." Visible="False"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtMinDeptNo" runat="server" Visible="False"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="lblEmpCode" runat="server" Text="Employee Code Number" Visible="False"></asp:Label>
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtECNo" runat="server" Visible="False"
                            onblur="return isEmployeeCode()"></asp:TextBox>
                    </div>
                    <div class="col-xs-1">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtECNoCD" runat="server" Visible="False"></asp:TextBox>
                    </div>
                    <div class="col-xs-3">
                        <asp:Label ID="frmLoanLabel" Font-Bold="true" runat="server" Text="Rescheduled From Loan"></asp:Label>
                        <asp:Label ID="frmLoan" runat="server"  Text="Label"></asp:Label>
                            </div>
                </div>
                <div class="row label-info">
                    <div class="col-xs-12">
                        Financial Requirements
                    </div>
                </div>
                <div class="row hidden">
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="Label55" runat="server" Text="Financing Type"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:RadioButtonList Enabled="false" ForeColor="Black" Font-Bold="true" ID="rdbType" runat="server" AutoPostBack="true"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                            <asp:ListItem Text="Asset Financing" Value="Asset Financing"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="lblAsset" runat="server" Visible="false" Text="Asset"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:DropDownList Enabled="false" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="ddlAssets" runat="server" AutoPostBack="true" Visible="false">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="Label3" runat="server" Text="Product Type"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:DropDownList Enabled="false" ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="cmbProductType" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="lblSector" runat="server" Text="Sector"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:DropDownList Enabled="false" ReadOnly="True" ForeColor="Black" Font-Bold="true" ID="cmbSector" runat="server" CssClass="col-xs-12 form-control input-sm"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="lblDHAsAt" runat="server" Text="Amount Required ZMW"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtFinReqAmt" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="lblDHName" runat="server" Text="No. of Repayments"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtFinReqTenor" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2 control-label">
                        Repayment Intervals
                    </div>
                    <div class="col-xs-1">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm numeric" ID="txtRepayInterval" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-2">
                        <asp:DropDownList Enabled="false" ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="cmbRepayInterval" runat="server">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="Days" Value="Days"></asp:ListItem>
                            <asp:ListItem Text="Weeks" Value="Weeks"></asp:ListItem>
                            <asp:ListItem Text="Months" Value="Months"></asp:ListItem>
                            <asp:ListItem Text="Years" Value="Years"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row hidden">
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="Label4347" runat="server" Text="Interest Rate %"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtInterestRate" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="lblInsurance" runat="server" Text="Insurance Rate %"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtInsuranceRate" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="Label47" runat="server" Text="Interest Rate (%)"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtFinReqIntRate" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="lblAdminRate" runat="server" Text="Admin Fee %"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtAdminRate" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="lblDHDIEI" runat="server" Text="Purpose"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtFinReqPurpose" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="lblDHHoldingPerc" runat="server" Text="Source of Repayment"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtFinReqSource" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row hidden">
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="Label48" runat="server" Text="Security Offered"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtFinReqSecOffer" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row hidden">
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="Label49" runat="server" Text="Bank"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtFinReqBank" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="Label50" runat="server" Text="Branch Name"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtFinReqBranchName" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row hidden">
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="Label51" runat="server" Text="Branch Code"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtFinReqBranchCode" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="Label52" runat="server" Text="A/c Number"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtFinReqAccNo" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2 control-label">
                        Disbursement Channel
                    </div>
                    <div class="col-xs-4">
                        <asp:RadioButtonList ID="rdbFinReqDisburseOption" runat="server" AutoPostBack="true" RepeatDirection="horizontal" CssClass="col-xs-12">
                            <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                            <asp:ListItem Text="PMEC Salary deduction" Value="PMEC"></asp:ListItem>
                            <asp:ListItem Text="Direct Debit" Value="DDebit"></asp:ListItem>
                            <%--<asp:ListItem Text="Asset" Value="Asset">   <asp:ListItem Text="Mobile wallet" Value="Mobile"></asp:ListItem></asp:ListItem>--%>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="lblEcocashNumber" runat="server" Text="Mobile Number" Visible="False"></asp:Label>
                    </div>
                  
                    <div class="col-xs-4">
                        <asp:TextBox ReadOnly="True" ForeColor="Black" Font-Bold="true" CssClass="col-xs-12 form-control input-sm" ID="txtEcocashNumber" runat="server" Visible="False"></asp:TextBox>
                    </div>
                </div>
                  <div class="row" id="divAppTypeBanker" runat="server" >
                                        <div class="col-xs-2 control-label">
                                            <asp:Label ID="Label4" runat="server" Visible="False" Text="Bank"></asp:Label>
                                            
                                        </div>
                                        <div class="col-xs-4">
                                            <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbBank" runat="server" AutoPostBack="true" Visible="False"></asp:DropDownList>
                                        </div>
                                        <div class="col-xs-2 control-label">
                                             <asp:Label ID="Label5" Visible="False" runat="server" Text="Branch"></asp:Label>
                                        </div>
                                        <div class="col-xs-4">
                                            <asp:DropDownList CssClass="col-xs-12 form-control input-sm" ID="cmbBranch" runat="server" Visible="False"></asp:DropDownList>
                                        </div>

                      <br />
                      <div class="col-xs-2 control-label">
                                             <asp:Label ID="Label7" Visible="False" runat="server" Text="Account Number"></asp:Label>
                                        </div>
                                        <div class="col-xs-4">
                                            <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="AccNo" runat="server" Visible="False"></asp:TextBox>
                                        </div>
                                    </div>
                <div class="row">
                    <div class="col-xs-2 control-label">
                        Upfront Fees (ZMW)
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox CssClass="col-xs-12 form-control input-sm numeric" ID="txtUpfrontFees" runat="server" AutoPostBack="true"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-3 control-label">
                        Interest Trigger
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
                    <div class="col-xs-2 control-label">
                        Amount to Disburse
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox CssClass="col-xs-12 form-control input-sm" ID="txtAmtToDisburse" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 control-label hidden">
                        Disbursement Account
                    </div>
                    <div class="col-xs-4 hidden">
                        <asp:DropDownList CssClass="col-xs-12 form-control input-sm chosen" ID="cmbDisbursementAccount" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2 control-label">
                        Interest Account
                    </div>
                    <div class="col-xs-4">
                        <asp:DropDownList CssClass="col-xs-12 form-control input-sm chosen" ID="cmbInterestAccount" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-2 control-label">
                        <asp:Label ID="lblDisburseDate" runat="server" Text="Disbursement Date"></asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox CssClass="col-xs-12 form-control input-sm nofuturedate" ID="txtDisburseDate" runat="server"></asp:TextBox>
                        <span class="glyphicon glyphicon-calendar form-control-feedback" style="margin-right: 13px; background-color: #eeeeff; border-radius: 0 3px 3px 0; border: 1px solid rgb(149,188,219); z-index: 0;"></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 text-center">
                        <asp:Repeater ID="repAgreements" runat="server">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkRepAgreement" Text='<%#Eval("lnkText") %>' NavigateUrl='<%#Eval("navURL") %>' runat="server" Target="_blank"></asp:HyperLink>
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="row hidden">
                    <div class="col-xs-12 text-center">
                        <asp:HyperLink ID="lnkAppRating" runat="server">Application Rating</asp:HyperLink>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 text-center">
                        <asp:HyperLink ID="lnkAmortizationSchedule" runat="server" Target="_blank">View Amortization Schedule</asp:HyperLink>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 text-center">
                        <asp:HyperLink ID="lnkArmotize" runat="server" Visible="false"></asp:HyperLink>
                        <a data-target="#AmortModal" role="button" data-toggle="modal" id="launchAmortization" style="height: 0;">Create/Revise Armotization Schedule</a>
                    </div>
                </div>
                <div style="height: 20px;"></div>
                <div class="row">
                    <div class="col-xs-12 text-center" style="left: 0px; top: 0px">
                        <asp:Button ID="btnDisburse" runat="server" CssClass="btn btn-primary btn-sm" Text="Disburse" UseSubmitBehavior="false" style="height: 33px" />
                        <asp:Button ID="btnReject" runat="server" CssClass="btn btn-danger btn-sm" Text="Reject" UseSubmitBehavior="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div>
        <div id="modal_dialog" style="display: none">
        </div>
    </div>
    <div id="AmortModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Loan Amortization</h4>
                </div>
                <div class="modal-body panel-body small">
                    <div class="row hidden">
                        <div class="col-xs-4 control-label">
                            <asp:Label ID="Label6" runat="server" Text="Repayment Option"></asp:Label>
                        </div>
                        <div class="col-xs-8">
                            <asp:RadioButtonList ID="rdbRepayOption" CssClass="col-xs-12" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem Text="Fixed/Flat" Value="Fixed"></asp:ListItem>
                                <asp:ListItem Text="Declining Balance" Value="Declining"></asp:ListItem>
                                <asp:ListItem Text="Declining Balance with equal instalments" Value="Balance"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4 control-label">
                            <asp:Label ID="lblRepPer" runat="server" Text="No. of Repayments"></asp:Label>
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox Enabled="false" CssClass="col-xs-12 form-control input-sm" ID="txtRepayPeriod" runat="server" onkeypress="return isnumeric(event)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4 control-label">
                            Repayment Intervals
                        </div>
                        <div class="col-xs-3">
                            <asp:TextBox Enabled="false" CssClass="col-xs-12 form-control input-sm numeric" ID="txtRepaymentInterval" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-5">
                            <asp:DropDownList Enabled="false" CssClass="col-xs-12 form-control input-sm" ID="cmbRepaymentInterval" runat="server">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Days" Value="Days"></asp:ListItem>
                                <asp:ListItem Text="Weeks" Value="Weeks"></asp:ListItem>
                                <asp:ListItem Text="Months" Value="Months"></asp:ListItem>
                                <asp:ListItem Text="Years" Value="Years"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4 control-label">
                            <asp:Label ID="Label7436" runat="server" Text="Interest Rate(%)"></asp:Label>
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox Enabled="false" CssClass="col-xs-12 form-control input-sm" ID="txtIntRate" runat="server" onkeypress="return isnumeric(event)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4 control-label">
                            <asp:Label ID="Label746" runat="server" Text="Interest Adjustment($)"></asp:Label>
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox Enabled="false" CssClass="col-xs-12 form-control input-sm" ID="txtAdminCharge" runat="server" onkeypress="return isnumeric(event)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4 control-label">
                            <asp:Label ID="Label7438" runat="server" Text="First Payment Date"></asp:Label>
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox CssClass="col-xs-12 form-control input-sm datepicker" ID="txt1stPayDate" runat="server"></asp:TextBox>
                            <span class="glyphicon glyphicon-calendar form-control-feedback" style="margin-right: 13px; background-color: #eeeeff; border-radius: 0 3px 3px 0; border: 1px solid rgb(149,188,219); z-index: 0;"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 text-center">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btnSaveCreditParameters" runat="server" UseSubmitBehavior="false"
                                Text="Create Amortization" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function showAmortization() {
            $("#launchAmortization").click();
        };
        $(function () {
            $("[id*=btnDisburse]").bind("click", function () {
                $("[id*=btnDisburse]").val("Disbursing...");
                $("[id*=btnDisburse]").attr("disabled", true);
            });
        });
    </script>
</asp:Content>