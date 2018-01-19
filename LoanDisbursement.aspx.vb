Imports System.Data.SqlClient
Imports System.Data
Imports CreditManager
Imports ErrorLogging

Partial Class Credit_LoanDisbursement
    Inherits System.Web.UI.Page
    Dim cmd2 As SqlCommand
    Public Sub loadAssets()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd As New SqlCommand("Select * from Quest_Assets", con)
                    Using adp As New SqlDataAdapter(cmd)
                        Dim ds As New DataSet
                        adp.Fill(ds, "Assets")
                        loadCombo(ds.Tables(0), ddlAssets, "Name", "Selling_Price")
                    End Using
                End Using
            End Using
        Catch ex As Exception
            msgbox(ex.ToString)
        End Try
    End Sub
     Protected Sub loadBank()


        Try

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd As New SqlCommand("select distinct bank, bank_name from para_bank order by bank", con)
                    Dim dss As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(dss, "para_bank")
                     If dss.Tables(0).Rows.Count > 0 Then


               cmbBank.AppendDataBoundItems=True
                   cmbBank.DataSource = dss
          
               cmbBank.DataValueField = "bank"
                cmbBank.DataTextField="bank_name"
           
               cmbBank.DataBind()
                        Else
                        cmbBank.DataSource = Nothing
               cmbBank.DataBind()
                     End If
               
                   ' loadCombo(dss.Tables(0), cmbBank, "bank_name", "bank")
                   ' cmbBank.Items.Insert(0,new ListItem(dss.Tables(0).Rows(0).Item("brank_name"), dss.Tables(0).Rows(0).Item("bank")))
             
                End Using
            End Using
        Catch ex As Exception
            WriteLogFile(Session("UserId"), Request.Url.ToString & " --- loadBank()", ex.Message)
        End Try
    End Sub

    Protected Sub loadBranch()

         Try
             Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                 Using cmd = New SqlCommand("SELECT bank, branch, branch_name FROM para_branch where bank='" & cmbBank.SelectedValue & "'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "para_branch")
                
                     If ds.Tables(0).Rows.Count > 0 Then


               cmbBranch.AppendDataBoundItems=True
                    cmbBranch.DataSource = ds
          
                cmbBranch.DataValueField = "branch"
                cmbBranch.DataTextField="branch_name"
           
                cmbBranch.DataBind()
                        Else
                         cmbBranch.DataSource = Nothing
                cmbBranch.DataBind()
                     End If
               
                   ' loadCombo(dss.Tables(0), cmbBank, "bank_name", "bank")
                   ' cmbBank.Items.Insert(0,new ListItem(dss.Tables(0).Rows(0).Item("brank_name"), dss.Tables(0).Rows(0).Item("bank")))
                   
             
                End Using
                End Using
            
            Catch ex As Exception
            WriteLogFile(Session("UserId"), Request.Url.ToString & " --- loadBank()", ex.Message)
        End Try
    End Sub
    Protected Sub cmbBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBank.SelectedIndexChanged
        loadBranch()
    End Sub
    Protected Sub btnDisburse_Click(sender As Object, e As EventArgs) Handles btnDisburse.Click
        Try
            Dim bal As double= Convert.ToDouble(Session("CashBox").ToString())
            Dim cmp As double=Convert.ToDouble(txtAmtToDisburse.Text)
            If cmp>=bal Then
                msgbox("The amount to disburse must be less than or equal to Cash Box amount")
                Exit Sub
            End If

            If rdbFinReqDisburseOption.SelectedIndex=2 Then

                If cmbBank.SelectedValue="--Select--" Then
                    msgbox("Select a bank for Direct Debit")
                    cmbBank.Focus()
                    Exit Sub
                End If

                     If cmbBranch.SelectedValue="--Select--" Then
                    msgbox("Select a branch for Direct Debit")
                    cmbBranch.Focus()
                    Exit Sub
                    If AccNo.Text<>"" Then
                        msgbox("Account Number is required for Direct Debit")
                        AccNo.Focus()
                    Exit Sub
                    End If
                End If
              
            End If

            If Trim(txtDisburseDate.Text) = "" Or Not IsDate(txtDisburseDate.Text) Then
                notify("Please enter disbursement date", "error")
                txtDisburseDate.Focus()
                'ElseIf cmbDisbursementAccount.SelectedItem.Text = "Select Account" Or Trim(cmbDisbursementAccount.SelectedValue) = "" Then
                '    notify("Please select the account to disburse from", "error")
                '    cmbDisbursementAccount.Focus()
            ElseIf cmbInterestAccount.SelectedItem.Text = "Select Account" Or Trim(cmbInterestAccount.SelectedValue) = "" Then
                notify("Please select the interest account", "error")
                cmbInterestAccount.Focus()
            ElseIf txtAmtToDisburse.Text = "" Or Not IsNumeric(txtAmtToDisburse.Text) Then
                notify("Please enter the amount to disburse", "error")
                txtAmtToDisburse.Focus()
            Else
                If Convert.ToDouble(txtAmtToDisburse.Text) > Convert.ToDouble(txtFinReqAmt.Text) Then
                    notify("Amount to disburse cannot be greater than amount applied", "error")
                ElseIf CDbl(txtAmtToDisburse.Text) + CDbl(txtUpfrontFees.Text) > CDbl(txtFinReqAmt.Text) Then
                    notify("Amount to disburse and upfront fees cannot be greater than amount applied", "error")
                Else
                    '''''''''''''''''''''''first save amt to disburse********************
                    '''''''''''''''''''''''RUN IN STORED PROCEDURE************************
                    Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        Using cmd As New SqlCommand("sp_disburse", con)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("@loanID", ViewState("loanID"))
                            cmd.Parameters.AddWithValue("@amtToDisburse", txtAmtToDisburse.Text.Replace(",", ""))
                            cmd.Parameters.AddWithValue("@disburseDate", txtDisburseDate.Text)
                            cmd.Parameters.AddWithValue("@disburseOption", rdbFinReqDisburseOption.SelectedValue)
                            cmd.Parameters.AddWithValue("@userID", Session("ID"))
                            'msgbox(Request.QueryString("id") + ";" + txtAmtToDisburse.Text + ";" + txtDisburseDate.Text)
                            If con.State = ConnectionState.Open Then
                                con.Close()
                            End If
                            con.Open()
                            If cmd.ExecuteNonQuery() Then
                                saveDisbursement()
                                 savebank()
                      
                                saveComment()
                                recordDisbursalTrans(txtCustNo.Text, ViewState("loanID"), txtAmtToDisburse.Text)
                                createAmortizationOptions(ViewState("loanID"))

                                'principal amount disbursed
                                'saveTransaction(ViewState("loanID"), "Disbursement", toMoney(txtAmtToDisburse.Text), 0, txtCustNo.Text, cmbDisbursementAccount.SelectedValue, 1, txtCustNo.Text, "", "", "", txtDisburseDate.Text)
                                saveTransaction(ViewState("loanID"), "Disbursement", toMoney(txtAmtToDisburse.Text), 0, txtCustNo.Text, "LO/" & Session("ID").ToString, 1, txtCustNo.Text, "", "", "", txtDisburseDate.Text)
                                If rdbInterestTrigger.SelectedValue = "Disbursement" Then
                                    'save interest to maturity
                                    'new function to insert interest to maturity on disbursement
                                    insertInterestAccountsTemp(ViewState("loanID"))
                                End If
                                If IsNumeric(toMoney(txtUpfrontFees.Text)) And CDbl(toMoney(txtUpfrontFees.Text)) > 0 Then
                                    insertUpfrontTemp(ViewState("loanID"))
                                End If
                                Response.Write("<script>alert('Loan successfully disbursed') ; location.href='LoanDisbursement.aspx'</script>")
                            End If
                        End Using
                    End Using
                End If
            End If

            
        Catch ex As Exception
            msgbox(ex.ToString)
        End Try
    End Sub
 
    Public Sub savebank()
        Dim cs As String = ConfigurationManager.ConnectionStrings("Constring").ConnectionString

        Dim frm As Double=Convert.ToDouble(frmLoan.Text)
        If rdbFinReqDisburseOption.SelectedValue="DDebit" Then
           '     msgbox("in there")                           'Update Query
                
        Using cn As New SqlConnection(cs)
            Dim sql As String = "Update Quest_Application Set  FIN_BANK='"& cmbBank.SelectedValue &"',FIN_BRANCH='"&cmbBranch.SelectedValue &"',FIN_BRANCH_CODE='"& cmbBranch.SelectedValue &"',FIN_ACCNO='"& AccNo.Text &"',Bank='"& cmbBank.SelectedItem.Text &"',BankBranch='"& cmbBranch.SelectedItem.Text &"',BankAccountNo='"& AccNo.Text &"',BranchCode='"& cmbBranch.SelectedValue &"' where ID='"&ViewState("loanID").ToString()&"'"
            Dim cmd As New SqlCommand(sql)
            cmd.CommandType = CommandType.Text
            cmd.Connection = cn
            cn.Open()
               Try
                          cmd.ExecuteNonQuery()
               Catch ex As Exception

               End Try
                cn.Close()
                                            End Using
       
                                     End If      
         Using cn As New SqlConnection(cs)
            Dim sql As String = "insert into Accounts_Transactions(Type,Category,TrxnDate,Account,Refrence,Description,Debit,Credit,ContraAccount,CostCode,Status,Other,Committed,BatchRef,CaptureDate) values('System Entry','Loan Disbursement',GETDATE(), 'LO/'+ CONVERT(VARCHAR,'"&  Session("ID").ToString() &"'),'10','Loan Disbursement','0','"& txtFinReqAmt.Text &"', 'LO/'+ CONVERT(VARCHAR,'"&  Session("ID").ToString() &"'),'','1','','','Rep10001',GETDATE())"
            Dim cmd As New SqlCommand(sql)
            cmd.CommandType = CommandType.Text
            cmd.Connection = cn
            cn.Open()
               Try
                          cmd.ExecuteNonQuery()
               Catch ex As Exception
                msgbox("Failed to debit cashbox")
               End Try
                cn.Close()
                                            End Using


        If frm>0 Then
      Using cn As New SqlConnection(cs)
            Dim sql As String = "update QUEST_APPLICATION set [REPAID]='1',Status='REPAID'  where ID='" & frmLoan.Text & "'"
            Dim cmd As New SqlCommand(sql)
            cmd.CommandType = CommandType.Text
            cmd.Connection = cn
            cn.Open()
               Try
                          cmd.ExecuteNonQuery()
               Catch ex As Exception
                msgbox("Failed to update previously rescheduled loan")
               End Try
                cn.Close()
            End Using
        End If
       
                                       

       
    End Sub
    Protected Sub btnSaveCreditParameters_Click(sender As Object, e As EventArgs) Handles btnSaveCreditParameters.Click
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            'Using cmd = New SqlCommand("update QUEST_APPLICATION set [FIN_REPAY_OPT]='" & rdbRepayOption.SelectedValue & "',[FIN_TENOR]='" & txtRepayPeriod.Text & "',[FIN_INT_RATE]='" & txtIntRate.Text & "',[FIN_ADMIN]='" & txtAdminCharge.Text & "',[FIN_REPAY_DATE]='" & txt1stPayDate.Text & "' where ID='" & ViewState("loanID") & "'", con)
            Using cmd = New SqlCommand("update QUEST_APPLICATION set [FIN_REPAY_DATE]='" & txt1stPayDate.Text & "' where ID='" & ViewState("loanID") & "'", con)
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                Try
                    cmd.ExecuteNonQuery()
                    createAmortizationOptions(ViewState("loanID"))
                    notify("Amortization schedule created", "success")
                Catch ex As Exception
                    ErrorLogging.WriteLogFile(Session("UserId"), Request.Url.ToString & " --- btnSaveCreditParameters_Click()", ex.ToString)
                    CreditManager.notify("Error creating amortization schedule", "error")
                Finally
                    con.Close()
                End Try
            End Using
        End Using
    End Sub

    Protected Sub clearAgreements()
        repAgreements.DataSource = Nothing
        repAgreements.DataBind()
    End Sub

    Protected Sub createAmortizationOptions(ByVal loanID As String)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd As New SqlCommand("select FIN_REPAY_OPT from QUEST_APPLICATION where ID='" & loanID & "'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "LOANS")
                    If ds.Tables(0).Rows.Count > 0 Then
                        'If ds.Tables(0).Rows(0).Item("FIN_REPAY_OPT") = "Interest" Then
                        '    Using cmdSimple = New SqlCommand("sp_amortize_simple_daily", con)
                        '        cmdSimple.Parameters.AddWithValue("@loanID", loanID)
                        '        cmdSimple.CommandType = CommandType.StoredProcedure
                        '        If con.State <> ConnectionState.Closed Then
                        '            con.Close()
                        '        End If
                        '        con.Open()
                        '        cmdSimple.ExecuteNonQuery()
                        '        con.Close()
                        '    End Using
                        'ElseIf ds.Tables(0).Rows(0).Item("FIN_REPAY_OPT") = "Balance" Then
                        '    Using cmdNormal = New SqlCommand("sp_amortize_normal_daily", con)
                        '        cmdNormal.CommandType = CommandType.StoredProcedure
                        '        cmdNormal.Parameters.AddWithValue("@loanID", loanID)
                        '        If con.State <> ConnectionState.Closed Then
                        '            con.Close()
                        '        End If
                        '        con.Open()
                        '        cmdNormal.ExecuteNonQuery()
                        '        con.Close()
                        '    End Using
                        'Else
                        Using cmdNormal = New SqlCommand("sp_amortize_balance_daily", con)
                            cmdNormal.CommandType = CommandType.StoredProcedure
                            cmdNormal.Parameters.AddWithValue("@loanID", loanID)
                            If con.State <> ConnectionState.Closed Then
                                con.Close()
                            End If
                            con.Open()
                            cmdNormal.ExecuteNonQuery()
                            con.Close()
                        End Using
                        'End If
                    End If
                End Using
            End Using
        Catch ex As Exception
            notify("Unable to create amortization schedule. Make sure all parameters are entered", "error")
            'ClientScript.RegisterStartupScript(Me.GetType(), "Gritter", "<script type=""text/javascript"">$.gritter.add({title: 'Amortization Failure!',text: 'An error occurred while creating the amortization schedule. Please make sure all parameters are entered correctly with the right format and try again.',image: 'images/error_button.png'});</script>")
        End Try
    End Sub

    Protected Sub getAppDetails(ByVal loanID As String)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd As New SqlCommand("select *,convert(varchar,DOB,106) as DOB1,convert(varchar,ISSUE_DATE,106) as ISSUE_DATE1,convert(varchar,GUARANTOR_DOB,106) as GUARANTOR_DOB1 from QUEST_APPLICATION where ID='" & loanID & "'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "APP")
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim dr = ds.Tables(0).Rows(0)
                        txtCustNo.Text = dr.Item("CUSTOMER_NUMBER")
                        txtName.Text = Trim(BankString.isNullString(dr.Item("SURNAME"))) + " " + BankString.isNullString(dr.Item("FORENAMES"))
                        lblBranchCode.Text = BankString.isNullString(dr.Item("BRANCH_CODE"))
                        lblBranchName.Text = BankString.isNullString(dr.Item("BRANCH_NAME"))
                        txtFinReqAccNo.Text = BankString.isNullString(dr.Item("FIN_ACCNO"))
                        Try
                            txtFinReqAmt.Text = FormatNumber(BankString.isNullString(dr.Item("AMT_APPLIED")), 2)
                        Catch ex As Exception
                            txtFinReqAmt.Text = ""
                        End Try

                        'txtRecAmt.Text = BankString.isNullString(ds.Tables(0).Rows(0).Item("FIN_AMT"))
                        Try
                            txtAmtToDisburse.Text = FormatNumber(BankString.isNullString(dr.Item("FIN_AMT")), 2)
                        Catch ex As Exception
                            txtAmtToDisburse.Text = ""
                        End Try
                        'txtFinReqBank.Text = BankString.isNullString(dr.Item("FIN_BANK"))
                        'txtFinReqBranchCode.Text = BankString.isNullString(dr.Item("FIN_BRANCH_CODE"))
                        'txtFinReqBranchName.Text = BankString.isNullString(dr.Item("FIN_BRANCH"))
                        txtFinReqIntRate.Text = BankString.isNullString(dr.Item("FIN_INT_RATE"))
                        txtFinReqPurpose.Text = BankString.isNullString(dr.Item("FIN_PURPOSE"))
                        txtFinReqSecOffer.Text = BankString.isNullString(dr.Item("FIN_SEC_OFFER"))
                        txtFinReqSource.Text = BankString.isNullString(dr.Item("FIN_SRC_REPAYMT"))
                        txtFinReqTenor.Text = BankString.isNullString(dr.Item("FIN_TENOR"))
                        txtInterestRate.Text = BankString.isNullString(ds.Tables(0).Rows(0).Item("INT_RATE"))
                        txtInsuranceRate.Text = BankString.isNullString(ds.Tables(0).Rows(0).Item("INSURANCE_RATE"))
                        txtAdminRate.Text = BankString.isNullString(ds.Tables(0).Rows(0).Item("ADMIN_RATE"))
                        Try
                            rdbClientType.SelectedValue = dr.Item("CUSTOMER_TYPE")
                        Catch ex As Exception
                            rdbClientType.ClearSelection()
                        End Try
                        Try
                            rdbFinReqDisburseOption.SelectedValue = dr.Item("DISBURSE_OPTION")
                        Catch ex As Exception
                            rdbFinReqDisburseOption.ClearSelection()
                        End Try
                        Try
                            cmbProductType.SelectedValue = dr.Item("FinProductType")
                        Catch ex As Exception
                            cmbProductType.ClearSelection()
                        End Try
                        Try
                            getProductDefaults(dr.Item("FinProductType"))
                        Catch ex As Exception

                        End Try
                        Try
                            cmbSector.SelectedValue = BankString.isNullString(ds.Tables(0).Rows(0).Item("Sector"))
                        Catch ex As Exception
                            cmbSector.ClearSelection()
                        End Try
                        If rdbClientType.SelectedValue = "Individual" Then
                            Try
                                rdbSubIndividual.Visible = True
                                rdbSubIndividual.SelectedValue = ds.Tables(0).Rows(0).Item("SUB_INDIVIDUAL")
                            Catch ex As Exception

                            End Try
                            If Session("ROLE") = "4042" Then
                                'tickSSB()
                            End If
                        ElseIf rdbClientType.SelectedValue = "Business" Then
                        End If
                        If rdbSubIndividual.SelectedValue = "SSB" Then
                            txtMinDept.Text = BankString.isNullString(dr.Item("MIN_DEPT"))
                            txtMinDeptNo.Text = BankString.isNullString(dr.Item("MIN_DEPT_NO"))
                            txtECNo.Text = BankString.isNullString(dr.Item("ECNO"))
                            txtECNoCD.Text = BankString.isNullString(dr.Item("CD"))

                            lblMinDept.Visible = True
                            lblMinDeptNo.Visible = True
                            lblEmpCode.Visible = True
                            txtMinDept.Visible = True
                            txtMinDeptNo.Visible = True
                            txtECNo.Visible = True
                            txtECNoCD.Visible = True
                        End If

                        If rdbFinReqDisburseOption.SelectedValue = "Ecocash" Then
                            lblEcocashNumber.Visible = True
                            txtEcocashNumber.Visible = True
                            txtEcocashNumber.Text = dr.Item("ECOCASH_NUMBER")
                        End If

    
Dim test2 As String=""
               Dim test As String=txtCustNo.Text
            Try
                cmbBranch.Items.Clear()
                 cmbBranch.DataSource = Nothing
             
  
        cmd2 = New SqlCommand("select * from CUSTOMER_DETAILS where Customer_Number='" + test+ "'", con)
        Dim ds4 As New DataSet
        adp = New SqlDataAdapter(cmd2)
        adp.Fill(ds4, "branch")
        If ds4.Tables(0).Rows.Count > 0 Then
   
       cmbBranch.Items.Insert(0,new ListItem(ds4.Tables(0).Rows(0).Item("BankBranch"),ds4.Tables(0).Rows(0).Item("BranchCode")))
                           cmbBranch.SelectedIndex = 0

               AccNo.Text=ds4.Tables(0).Rows(0).Item("BankAccountNo")
                                test2=ds4.Tables(0).Rows(0).Item("Bank")
            con.close()


                 

                End If
        
   
             
            Catch ex As Exception

            End Try

                        Try
                            cmbBank.Items.Clear()
                            cmbBank.DataSource = Nothing



                            cmd2 = New SqlCommand("select * from para_bank where bank_name='" + test2 + "'", con)
                            Dim ds5 As New DataSet
                            adp = New SqlDataAdapter(cmd2)
                            adp.Fill(ds5, "para_bank")
                            If ds5.Tables(0).Rows.Count > 0 Then

                                cmbBank.Items.Insert(0, new ListItem(ds5.Tables(0).Rows(0).Item("bank_name"), ds5.Tables(0).Rows(0).Item("bank")))
                                cmbBank.SelectedIndex = 0
                                con.close()
                            End If
                        Catch ex As Exception

                        End Try
                  frmLoan.Text=BankString.isNullString(dr.Item("FromLoan"))
               Dim mynum As Double=Convert.ToDouble(frmLoan.Text)

                        If mynum=0 Then
                            frmLoanLabel.Visible=False
                            frmLoan.Visible=false
                        End If
                        loadBank()
                        loadBranch()
                    Else
                    End If
                End Using
            End Using
        Catch ex As Exception
            msgbox(ex.ToString)
        End Try
    End Sub

    Protected Sub getDisbursements(ByVal roleID As String, cliName As String)
        Try
            Dim ds As New DataSet
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd As New SqlCommand("select ID,CUSTOMER_NUMBER as [CUST NO.],CUSTOMER_TYPE as [TYPE],case IS_PARTIAL when 1 then RTRIM(isnull(SURNAME,'')+' '+isnull(FORENAMES,''))+' - PARTIALLY DISBURSED' else RTRIM(isnull(SURNAME,'')+' '+isnull(FORENAMES,'')) end as NAME,CONVERT(DECIMAL(30,2),FIN_AMT) as AMOUNT,convert(varchar,CREATED_DATE,113) as 'APPLICATION DATE' from QUEST_APPLICATION where (SEND_TO='" & roleID & "') and STATUS<>'REJECTED' and RTRIM(isnull(SURNAME,'')+' '+isnull(FORENAMES,'')) like '%" & cliName & "%' And BRANCH_CODE='"& Session("BRANCHCODE").ToString() &"' order by SURNAME asc", con)
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "APP")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdDisbursements.DataSource = ds.Tables(0)
                    Else
                        grdDisbursements.DataSource = Nothing
                    End If
                    grdDisbursements.DataBind()
                End Using
            End Using
            lblAppCount.Text = ds.Tables(0).Rows.Count
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub getProductDefaults(productID As String)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select * from [CreditProducts] where id='" & productID & "'", con)
                    Dim ds As New DataSet
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(ds, "PDA")
                    End Using
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim dr = ds.Tables(0).Rows(0)
                        Try
                            If dr("ProductFees") = "None" Then
                                lblAdminRate.Visible = False
                                txtAdminCharge.Text = "0"
                                txtAdminCharge.Visible = False
                            Else
                                lblAdminRate.Visible = True
                                txtAdminCharge.Visible = True
                                Try
                                    lblAdminRate.Text = IIf(dr("ProductFeeCalc") = "Percentage", "Application Fees (%)", "Application Fees ($)")
                                Catch ex As Exception

                                End Try
                                Try
                                    txtAdminCharge.Text = dr("ProductFeeAmtPerc")
                                Catch ex As Exception
                                    txtAdminCharge.Text = ""
                                End Try
                            End If
                        Catch ex As Exception

                        End Try
                        txtUpfrontFees.Text = 0
                        Try
                            rdbInterestTrigger.SelectedValue = dr("IntTrigger")
                        Catch ex As Exception
                            rdbInterestTrigger.ClearSelection()
                        End Try
                    End If
                End Using
            End Using
        Catch ex As Exception
            WriteLogFile(ex.ToString)
        End Try
    End Sub
        Protected Sub saveDisbursement()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("SaveDisbursement", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@CustNo", txtCustNo.Text)
                    cmd.Parameters.AddWithValue("@LoanID", ViewState("loanID"))
                    cmd.Parameters.AddWithValue("@TrxnDate", txtDisburseDate.Text)
                    cmd.Parameters.AddWithValue("@Name", txtName.Text)
                    cmd.Parameters.AddWithValue("@ProductID", cmbProductType.SelectedValue)
                    cmd.Parameters.AddWithValue("@ProductName", cmbProductType.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@Sector", cmbSector.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@AmountRequired", txtFinReqAmt.Text)
                    cmd.Parameters.AddWithValue("@NoOfRepayments", txtFinReqTenor.Text)
                    cmd.Parameters.AddWithValue("@RepaymentIntervalNum", txtRepayInterval.Text)
                    cmd.Parameters.AddWithValue("@RepaymentIntervalUnit", cmbRepayInterval.SelectedValue)
                    cmd.Parameters.AddWithValue("@InterestRate", txtFinReqIntRate.Text)
                    cmd.Parameters.AddWithValue("@ApplicationFees", txtAdminRate.Text)
                    cmd.Parameters.AddWithValue("@Purpose", txtFinReqPurpose.Text)
                    cmd.Parameters.AddWithValue("@DisbursementChannel", rdbFinReqDisburseOption.SelectedValue)
                    cmd.Parameters.AddWithValue("@UpfrontFees", txtUpfrontFees.Text)
                    cmd.Parameters.AddWithValue("@AmountToDisburse", txtAmtToDisburse.Text)
                    'cmd.Parameters.AddWithValue("@PrincipalAccount", cmbDisbursementAccount.SelectedValue)
                    cmd.Parameters.AddWithValue("@PrincipalAccount", "LO/" & Session("ID").ToString)
                    cmd.Parameters.AddWithValue("@InterestAccount", cmbInterestAccount.SelectedValue)
                    cmd.Parameters.AddWithValue("@User", Session("UserId"))
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdDisbursements_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdDisbursements.PageIndexChanging
        grdDisbursements.PageIndex = e.NewPageIndex
        getDisbursements(Session("ROLE"), "")
    End Sub

    Protected Sub grdDisbursements_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdDisbursements.RowCommand
        Try
            Dim EncQuery As New BankEncryption64
            If e.CommandName = "Select" Then
                Dim loanID = e.CommandArgument
                'Response.Redirect("LoanDisbursement.aspx?id=" & HttpUtility.UrlEncode(EncQuery.Encrypt(loanID, "taDz392018hbdER")))
                Response.Redirect("LoanDisbursement.aspx?id=" & loanID)
            ElseIf e.CommandName = "Details" Then
                Dim loanID = e.CommandArgument
            End If
        Catch ex As Exception
            ErrorLogging.WriteLogFile(Session("UserId"), "QuestCredit/LoanDisbursement---grdDisbursements_RowCommand()", ex.ToString)
        End Try
    End Sub

    Protected Sub grdDisbursements_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdDisbursements.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lnk As LinkButton = CType(e.Row.FindControl("LinkButton1"), LinkButton)
                If lnk.CommandArgument = ViewState("loanID") Then
                    Dim ri = (e.Row.RowIndex)
                    e.Row.RowState = DataControlRowState.Selected
                End If
            End If
        Catch ex As Exception
            ErrorLogging.WriteLogFile(Session("UserId"), "Credit/LoanDisbursement---grdDisbursements_RowDataBound()", ex.ToString)
        End Try
    End Sub

    Protected Sub insertInterestAccountsTemp(loanID As String)
        'get ineterest to maturity
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Dim cmdInt = New SqlCommand("select max(cumulative_interest) from AMORTIZATION_SCHEDULE where LOANID='" & loanID & "'", con)
            Dim intToMaturity As Double = 0
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
            con.Open()
            intToMaturity = cmdInt.ExecuteScalar
            con.Close()

            Using cmd = New SqlCommand("SaveAccountsTrxnsTempWithContra", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Type", "System Entry")
                cmd.Parameters.AddWithValue("@Category", "Interest Payable")
                cmd.Parameters.AddWithValue("@Ref", loanID)
                cmd.Parameters.AddWithValue("@Desc", "Interest to Maturity")
                cmd.Parameters.AddWithValue("@Debit", intToMaturity)
                cmd.Parameters.AddWithValue("@Credit", 0.0)
                cmd.Parameters.AddWithValue("@Account", txtCustNo.Text)
                cmd.Parameters.AddWithValue("@ContraAccount", cmbInterestAccount.SelectedValue)
                cmd.Parameters.AddWithValue("@Status", 1)
                cmd.Parameters.AddWithValue("@Other", txtCustNo.Text)
                cmd.Parameters.AddWithValue("@BankAccID", "")
                cmd.Parameters.AddWithValue("@BankAccName", "")
                cmd.Parameters.AddWithValue("@BatchRef", "")
                cmd.Parameters.AddWithValue("@TrxnDate", txtDisburseDate.Text)
                cmd.Parameters.AddWithValue("@CaptureBy", Session("UserId"))
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
    End Sub

    Protected Sub insertUpfrontTemp(loanID As String)
        'get interest to maturity
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)

            Using cmd = New SqlCommand("SaveAccountsTrxnsTempWithContra", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Type", "System Entry")
                cmd.Parameters.AddWithValue("@Category", "Loan Disbursement")
                cmd.Parameters.AddWithValue("@Ref", loanID)
                cmd.Parameters.AddWithValue("@Desc", "Upfront Fees")
                cmd.Parameters.AddWithValue("@Debit", toMoney(txtUpfrontFees.Text))
                cmd.Parameters.AddWithValue("@Credit", 0.0)
                cmd.Parameters.AddWithValue("@Account", cmbInterestAccount.SelectedValue)
                cmd.Parameters.AddWithValue("@ContraAccount", txtCustNo.Text)
                cmd.Parameters.AddWithValue("@Status", 1)
                cmd.Parameters.AddWithValue("@Other", txtCustNo.Text)
                cmd.Parameters.AddWithValue("@BankAccID", "")
                cmd.Parameters.AddWithValue("@BankAccName", "")
                cmd.Parameters.AddWithValue("@BatchRef", "")
                cmd.Parameters.AddWithValue("@TrxnDate", txtDisburseDate.Text)
                cmd.Parameters.AddWithValue("@CaptureBy", Session("UserId"))
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
    End Sub

    Protected Sub saveTransaction(reference As String, description As String, debit As Double, credit As Double, account As String, contra As String, status As String, other As String, bankAccId As String, bankAccName As String, batchRef As String, trxnDate As Date)
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd As New SqlCommand("SaveAccountsTrxnsTempWithContra", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Type", "System Entry")
                cmd.Parameters.AddWithValue("@Category", "Loan Disbursement")
                cmd.Parameters.AddWithValue("@Ref", reference)
                cmd.Parameters.AddWithValue("@Desc", description)
                cmd.Parameters.AddWithValue("@Debit", debit)
                cmd.Parameters.AddWithValue("@Credit", credit)
                cmd.Parameters.AddWithValue("@Account", account)
                cmd.Parameters.AddWithValue("@ContraAccount", contra)
                cmd.Parameters.AddWithValue("@Status", status)
                cmd.Parameters.AddWithValue("@Other", other)
                cmd.Parameters.AddWithValue("@BankAccID", bankAccId)
                cmd.Parameters.AddWithValue("@BankAccName", bankAccName)
                cmd.Parameters.AddWithValue("@BatchRef", batchRef)
                cmd.Parameters.AddWithValue("@TrxnDate", trxnDate)
                cmd.Parameters.AddWithValue("@CaptureBy", Session("UserId"))

                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
    End Sub
    Protected Function isAmortized(loanID As String) As Integer
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmdApp = New SqlCommand("select * from QUEST_APPLICATION where ID='" & loanID & "'", con)
                Dim dsApp As New DataSet
                Using adp = New SqlDataAdapter(cmdApp)
                    adp.Fill(dsApp, "qa")
                End Using
                If dsApp.Tables(0).Rows.Count > 0 Then
                    Using cmd = New SqlCommand("select * from AMORTIZATION_SCHEDULE where LOANID='" & loanID & "'", con)
                        Dim ds As New DataSet
                        Using adp = New SqlDataAdapter(cmd)
                            adp.Fill(ds, "armo")
                        End Using
                        If ds.Tables(0).Rows.Count > 0 Then
                            Return 2
                        Else
                            Return 1
                        End If
                    End Using
                Else
                    Return 0
                End If
            End Using
        End Using
    End Function

    Protected Sub loadAccounts(mainAcc As String)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select convert(varchar,MainAccount)  + '/' + convert(varchar,SubAccount) as AccountNo, AccountName  + '  ' + convert(varchar,MainAccount)  + '/' + convert(varchar,SubAccount) as AccountName from tbl_FinancialAccountsCreation where MainAccount='" & mainAcc & "' and SubAccount<>1", con)
                    'End if
                    Dim ds As New DataSet
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(ds, "LRS2")
                    End Using
                    cmbDisbursementAccount.Visible = True
                    loadCombo(ds.Tables(0), cmbDisbursementAccount, "AccountName", "AccountNo")
                End Using
            End Using
        Catch ex As Exception
            WriteLogFile(Session("UserId"), Request.Url.ToString & " --- loadAccounts()", ex.ToString)
        End Try
    End Sub

    Protected Sub loadAgreements()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select isnull([SUB_INDIVIDUAL],'') as [SUB_INDIVIDUAL],[FIN_AMT],[FIN_TENOR],[FIN_INT_RATE],[FIN_ADMIN],[FIN_REPAY_DATE],[FIN_REPAY_OPT],[CUSTOMER_TYPE],[CUSTOMER_NUMBER],[MD_ID],ISNULL(ASSET_NAME,'') as [ASSET_NAME] from QUEST_APPLICATION where ID='" & ViewState("loanID") & "' and [STATUS]<>'REJECTED'", con)
                    'msgbox(cmd.CommandText)
                    Dim ds As New DataSet
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(ds, "CREDIT")
                    End Using
                    repAgreements.DataSource = Nothing
                    repAgreements.DataBind()
                    If ds.Tables(0).Rows.Count > 0 Then
                        Try
                            Dim dt As New DataTable
                            dt.Columns.Add("navURL")
                            dt.Columns.Add("lnkText")
                            If Trim(ds.Tables(0).Rows(0).Item("ASSET_NAME")) <> "" Then
                                'dt.Rows.Add("rptAcknowledgement.aspx?id=" & ViewState("loanID") & "&asset=1", "Acknowledgement of Debt")
                                dt.Rows.Add("rptAssetFinancing.aspx?id=" & ViewState("loanID") & "&asset=1", "Acknowledgement of Debt")

                            ElseIf ds.Tables(0).Rows(0).Item("CUSTOMER_TYPE") = "Group" Then
                                dt.Rows.Add("rptFormLetter.aspx?id=" & ViewState("loanID") & "&typ=grp&cust=" & ds.Tables(0).Rows(0).Item("CUSTOMER_NUMBER") & "", "Form Letter")
                                dt.Rows.Add("rptAcknowledgement.aspx?id=" & ViewState("loanID") & "&typ=grp&cust=" & ds.Tables(0).Rows(0).Item("CUSTOMER_NUMBER") & "", "Acknowledgement of Debt")
                            Else
                                If ds.Tables(0).Rows(0).Item("SUB_INDIVIDUAL") = "SSB" Then
                                    'dt.Rows.Add("SSBSalaryDeduction.aspx?id=" & ViewState("loanID") & "", "SSB Deduction Form")
                                    dt.Rows.Add("rptFormLetter.aspx?id=" & ViewState("loanID") & "", "Form Letter")
                                    dt.Rows.Add("rptAcknowledgeSSB.aspx?id=" & ViewState("loanID") & "", "Acknowledgement of Debt")
                                Else
                                    dt.Rows.Add("rptFormLetter.aspx?id=" & ViewState("loanID") & "", "Form Letter")
                                    dt.Rows.Add("rptAcknowledgement.aspx?id=" & ViewState("loanID") & "", "Acknowledgement of Debt")
                                End If
                            End If
                            repAgreements.DataSource = dt
                            repAgreements.DataBind()
                            'End If
                        Catch ex As Exception
                            msgbox(ex.ToString)
                        End Try
                    Else
                    End If
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub loadClientTypes()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd As New SqlCommand("select * from PARA_CLIENT_TYPES", con)
                    Using adp As New SqlDataAdapter(cmd)
                        Dim ds As New DataSet
                        adp.Fill(ds, "Clients")
                        loadCombo(ds.Tables(0), rdbClientType, "CLIENT_TYPE", "CLIENT_TYPE")
                    End Using
                End Using
            End Using
        Catch ex As Exception
            msgbox(ex.ToString)
        End Try
    End Sub

    Protected Sub loadInterestAccounts()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select convert(varchar,MainAccount)  + '/' + convert(varchar,SubAccount) as AccountNo, AccountName  + '  ' + convert(varchar,MainAccount)  + '/' + convert(varchar,SubAccount) as AccountName from tbl_FinancialAccountsCreation where convert(varchar,MainAccount) + '/' + convert(varchar,SubAccount)='302/2' or convert(varchar,MainAccount) + '/' + convert(varchar,SubAccount)='223/2'", con)
                    'End if
                    Dim ds As New DataSet
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(ds, "LRS2")
                    End Using
                    cmbInterestAccount.Visible = True
                    loadCombo(ds.Tables(0), cmbInterestAccount, "AccountName", "AccountNo")
                End Using
            End Using
        Catch ex As Exception
            msgbox(ex.ToString)
        End Try
    End Sub

    Protected Sub loadRepayParameters()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select [FIN_AMT],[FIN_TENOR],[FIN_INT_RATE],qa.ADMIN_RATE, [FIN_ADMIN],convert(varchar(50),fin_repay_date,106) as [FIN_REPAY_DATE],[FIN_REPAY_OPT],[CUSTOMER_TYPE],ISNULL(qa.[DefaultIntInterval],cp.DefaultIntInterval) as [DefaultIntInterval],ISNULL(qa.[IntCalcMethod],cp.[IntCalcMethod]) as [IntCalcMethod],ISNULL(qa.[IntTrigger],cp.[IntTrigger]) as [IntTrigger],ISNULL(qa.[DaysInYear],cp.[DaysInYear]) as [DaysInYear],ISNULL(qa.[RepaymentFreq],cp.[RepaymentFreq]) as [RepaymentFreq],ISNULL(qa.[HasGracePeriod],cp.[HasGracePeriod]) as [HasGracePeriod],ISNULL(qa.[GracePeriodType],cp.[GracePeriodType]) as [GracePeriodType],ISNULL(qa.[GracePeriodLength],cp.[GracePeriodLength]) as [GracePeriodLength],ISNULL(qa.[GracePeriodUnit],cp.[GracePeriodUnit]) as [GracePeriodUnit],ISNULL(qa.[AllowRepaymentOnWknd],cp.[AllowRepaymentOnWknd]) as [AllowRepaymentOnWknd],ISNULL(qa.[IfRepaymentFallsOnWknd],cp.[IfRepaymentFallsOnWknd]) as [IfRepaymentFallsOnWknd],ISNULL(qa.[AllowEditingPaymentSchedule],cp.[AllowEditingPaymentSchedule]) as [AllowEditingPaymentSchedule],ISNULL(qa.[RepayOrder1],cp.[RepayOrder1]) as [RepayOrder1],ISNULL(qa.[RepayOrder2],cp.[RepayOrder2]) as [RepayOrder2],ISNULL(qa.[RepayOrder3],cp.[RepayOrder3]) as [RepayOrder3],ISNULL(qa.[RepayOrder4],cp.[RepayOrder4]) as [RepayOrder4],ISNULL(qa.[TolerancePeriodNum],cp.[TolerancePeriodNum]) as [TolerancePeriodNum],ISNULL(qa.[TolerancePeriodUnit],cp.[TolerancePeriodUnit]) as [TolerancePeriodUnit],ISNULL(qa.[ArrearNonWorkingDays],cp.[ArrearNonWorkingDays]) as [ArrearNonWorkingDays],ISNULL(qa.[PenaltyCharged],cp.[PenaltyCharged]) as [PenaltyCharged],ISNULL(qa.[PenaltyOption],cp.[PenaltyOption]) as [PenaltyOption],ISNULL(qa.[AmtToPenalise],cp.[AmtToPenalise]) as [AmtToPenalise],ISNULL(qa.[ProductFees],cp.[ProductFees]) as [ProductFees],ISNULL(qa.[ProductFeeCalc],cp.[ProductFeeCalc]) as [ProductFeeCalc],ISNULL(qa.[ProductFeeAmtPerc],cp.[ProductFeeAmtPerc]) as [ProductFeeAmtPerc],qa.RepaymentIntervalNum, qa.RepaymentIntervalUnit,FinProductType from QUEST_APPLICATION qa JOIN creditproducts cp ON qa.FinProductType=cp.id where qa.ID='" & ViewState("loanID") & "'", con)
                    Dim ds As New DataSet
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(ds, "CREDIT")
                    End Using
                    If ds.Tables(0).Rows.Count > 0 Then
                        Try
                            Try
                                txtRepayPeriod.Text = FormatNumber(ds.Tables(0).Rows(0).Item("FIN_TENOR"), 0)
                            Catch ex As Exception
                                txtRepayPeriod.Text = ""
                            End Try

                            txtIntRate.Text = ds.Tables(0).Rows(0).Item("FIN_INT_RATE")
                            Try
                                txtAdminCharge.Text = ds.Tables(0).Rows(0).Item("ADMIN_RATE")
                            Catch ex As Exception
                                txtAdminCharge.Text = "0"
                            End Try
                            Try
                                txt1stPayDate.Text = ds.Tables(0).Rows(0).Item("FIN_REPAY_DATE")
                            Catch ex As Exception
                                txt1stPayDate.Text = ""
                            End Try
                            'Try
                            '    txtLoanAmt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("FIN_AMT"), 2)
                            'Catch ex As Exception
                            '    txtLoanAmt.Text = ""
                            'End Try
                            Try
                                txtRepaymentInterval.Text = ds.Tables(0).Rows(0).Item("RepaymentIntervalNum")
                                txtRepayInterval.Text = ds.Tables(0).Rows(0).Item("RepaymentIntervalNum")
                            Catch ex As Exception
                                txtRepaymentInterval.Text = ""
                                txtRepayInterval.Text = ""
                            End Try
                            Try
                                cmbRepaymentInterval.SelectedValue = ds.Tables(0).Rows(0).Item("RepaymentIntervalUnit")
                                cmbRepayInterval.SelectedValue = ds.Tables(0).Rows(0).Item("RepaymentIntervalUnit")
                            Catch ex As Exception
                                cmbRepaymentInterval.ClearSelection()
                                cmbRepayInterval.ClearSelection()
                            End Try
                            'getProductDefaults(ds.Tables(0).Rows(0).Item("FinProductType"))
                            'lblViewSchedule.Text = "<a href='rptAmortizationSchedule.aspx?loanID=" & txtLoanID.Text & "' target='new'>View Schedule</a>"
                        Catch ex As Exception
                            WriteLogFile(Session("UserId"), Request.Url.ToString & " --- loadRepayParameters()", ex.ToString)
                        End Try
                    Else
                        CreditManager.notify("No matches found", "error")
                    End If
                End Using
            End Using
        Catch ex As Exception
            WriteLogFile(Session("UserId"), Request.Url.ToString & " --- loadRepayParameters()", ex.ToString)
        End Try
    End Sub
    'Protected Sub loadRepayParameters()
    '    Try
    '        Dim repayPer, intRate, adminCharge As Double
    '        Dim firstPayDate As String = ""
    '        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
    '            Using cmd = New SqlCommand("select [FIN_AMT],[FIN_TENOR],[FIN_INT_RATE],[FIN_ADMIN],convert(varchar(30),[FIN_REPAY_DATE],113) as [FIN_REPAY_DATE],[FIN_REPAY_OPT],[CUSTOMER_TYPE] from QUEST_APPLICATION where ID='" & ViewState("loanID") & "'", con)
    '                Dim ds As New DataSet
    '                Using adp = New SqlDataAdapter(cmd)
    '                    adp.Fill(ds, "CREDIT")
    '                End Using
    '                If ds.Tables(0).Rows.Count > 0 Then
    '                    Try
    '                        repayPer = ds.Tables(0).Rows(0).Item("FIN_TENOR")
    '                        intRate = ds.Tables(0).Rows(0).Item("FIN_INT_RATE")
    '                        firstPayDate = ds.Tables(0).Rows(0).Item("FIN_REPAY_DATE")
    '                        adminCharge = ds.Tables(0).Rows(0).Item("FIN_ADMIN")
    '                        If ds.Tables(0).Rows(0).Item("CUSTOMER_TYPE") = "Farmer" Then
    '                            ViewState("isFarmer") = "1"
    '                        End If
    '                    Catch ex As Exception
    '                        'msgbox(ex.tostring)
    '                    End Try
    '                    Try
    '                        rdbRepayOption.SelectedValue = ds.Tables(0).Rows(0).Item("FIN_REPAY_OPT")
    '                    Catch ex As Exception
    '                        rdbRepayOption.ClearSelection()
    '                    End Try
    '                Else
    '                    'ClientScript.RegisterStartupScript(Me.GetType(), "Gritter", "<script type=""text/javascript"">$.gritter.add({title: 'Loan ID not found!',text: 'There is no record which matches the entered Loan ID.',image: 'images/error_button.png'});</script>")
    '                End If
    '            End Using
    '        End Using
    '        txtRepayPeriod.Text = repayPer
    '        txtIntRate.Text = intRate
    '        txtAdminCharge.Text = adminCharge
    '        txt1stPayDate.Text = firstPayDate
    '    Catch ex As Exception
    '        msgbox(ex.tostring)
    '    End Try
    'End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        loadCashBox()
  
        If Not IsPostBack Then
         'loadBank()

            loadProductType(cmbProductType)
            loadSectors(cmbSector)
            ViewState("loanID") = Request.QueryString("id")
            If Session("ROLE") = "4045" Then
                'chkPartial.Visible = True
                loadAccounts("212")
                cmbDisbursementAccount.Visible = True
            ElseIf Session("ROLE") = "1024" Then
                'chkPartial.Visible = True
                loadAccounts("211")
                cmbDisbursementAccount.Visible = True
            Else
                'chkPartial.Visible = False
            End If
            loadInterestAccounts()
            getDisbursements(Session("ROLE"), "")
            loadAssets()
            loadClientTypes()
            If ViewState("loanID") <> "" Then
                getAppDetails(ViewState("loanID"))
                loadRepayParameters()
                If isAmortized(ViewState("loanID")) = 0 Then
                    notify("Requested Loan ID not found", "error")
                ElseIf isAmortized(ViewState("loanID")) = 2 Then
                    loadAgreements()
                ElseIf isAmortized(ViewState("loanID")) = 1 Then
                    notify("This loan application has not yet been amortized. Create amortization schedule first.", "warning")
                    ClientScript.RegisterStartupScript(Me.GetType(), "showAmortization", "<script type=""text/javascript"">showAmortization();</script>")
                End If
                lnkArmotize.NavigateUrl = "Amortization.aspx?ID=" & ViewState("loanID") & "&App=1"
                lnkAppRating.NavigateUrl = "ApplicationRating.aspx?loanID=" & ViewState("loanID")
                lnkAmortizationSchedule.NavigateUrl = "rptAmortizationSchedule.aspx?loanID=" & ViewState("loanID")
            End If
        End If
    End Sub
    Public Sub loadCashBox()

        Dim accNo as string=""
        Dim balance As String=""
                 Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Dim ds As New DataSet
         
     
     Try
cmd2 = New SqlCommand("select FNAME + ' '+ LNAME + ' | ' + 'LO/'+ CONVERT(VARCHAR,USERID) as AccountName, 'LO/'+ CONVERT(VARCHAR,USERID) as AccNo from MASTER_USERS where USER_TYPE='4041' and USER_LOGIN='"& Session("UserID").ToString() &"'", con)
        Dim ds4 As New DataSet
        Dim adp2 = New SqlDataAdapter(cmd2)
        adp2.Fill(ds4, "users")
        If ds4.Tables(0).Rows.Count > 0 Then
   
       accNo=ds4.Tables(0).Rows(0).Item("AccNo")
 
            con.close()



                End If
        
     Catch ex As Exception

     End Try
            
             
  
        
   
             
           Try


            con.Open()
          
             
  
        cmd2 = New SqlCommand("select isnull(sum(debit-credit),0) as Bal from Accounts_Transactions where Account='" & accNo & "'", con)
        Dim ds5 As New DataSet
        Dim adp = New SqlDataAdapter(cmd2)
        adp.Fill(ds5, "Accounts_Transactions")
        If ds5.Tables(0).Rows.Count > 0 Then
   
    balance=ds5.Tables(0).Rows(0).Item("Bal")
 
            con.close()


                 

                End If
        
   
           Catch ex As Exception

           End Try

            Session("CashBox")=balance
         
            TextBox1.Text="ZMW"+balance
        
            End Using

    End Sub
    Protected Sub rdbFinReqDisburseOption_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdbFinReqDisburseOption.SelectedIndexChanged
        If rdbFinReqDisburseOption.SelectedValue = "Cash" Then
            loadAccounts("211")
               cmbBank.Visible=False
            cmbBranch.Visible=False
            AccNo.Visible=False
            Label4.Visible=False
              Label5.Visible=False
              Label7.Visible=False
        ElseIf rdbFinReqDisburseOption.SelectedValue = "RTGS" Then
            loadAccounts("212")
               cmbBank.Visible=False
            cmbBranch.Visible=False
            AccNo.Visible=False
            Label4.Visible=False
              Label5.Visible=False
              Label7.Visible=False
        ElseIf rdbFinReqDisburseOption.SelectedValue = "Mobile" Then
            loadAccounts("211")
               cmbBank.Visible=False
            cmbBranch.Visible=False
            AccNo.Visible=False
            Label4.Visible=False
              Label5.Visible=False
              Label7.Visible=False
        ElseIf rdbFinReqDisburseOption.SelectedValue = "Asset" Then
            loadAccounts("216")
               cmbBank.Visible=False
            cmbBranch.Visible=False
            AccNo.Visible=False
            Label4.Visible=False
              Label5.Visible=False
              Label7.Visible=False
            ElseIf rdbFinReqDisburseOption.SelectedValue = "DDebit"  Then
            cmbBank.Visible=True
            cmbBranch.Visible=True
            AccNo.Visible=True
            Label4.Visible=true
              Label5.Visible=true
              Label7.Visible=true
            Else
           cmbBank.Visible=False
            cmbBranch.Visible=False
            AccNo.Visible=False
            Label4.Visible=False
              Label5.Visible=False
              Label7.Visible=False
        End If
    End Sub

    Protected Sub recordDisbursalTrans(custNo As String, loanID As String, amt As Double)
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("insert into QUEST_TRANSACTIONS (CUST_NO,LOANID,TRANS_DATE,TRANS_DESC,DEBIT,CREDIT,BAL_BFWD,BAL_CFWD) VALUES ('" & custNo & "','" & loanID & "','" & txtDisburseDate.Text & "','Loan Disbursement','" & amt & "','0','0','" & amt & "')", con)
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
    End Sub

    Protected Sub saveComment()
        'Try
        'Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
        'Using cmd As New SqlCommand("insert into REQUEST_HISTORY (LOANID,COMMENT_DATE,USERID,COMMENT,RECOMMENDED_AMT) values('" & Request.QueryString("id") & "',GETDATE(),'" & Session("UserID") & "','" & BankString.removeSpecialCharacter(txtComment.Text) & "','" & txtRecAmt.Text & "')", con)
        'If con.State = ConnectionState.Open Then
        'con.Close()
        'End If
        'con.Open()
        'cmd.ExecuteNonQuery()
        'con.Close()
        'End Using
        'End Using
        'Catch ex As Exception
        '
        'End Try
    End Sub

    Protected Sub txtUpfrontFees_TextChanged(sender As Object, e As EventArgs) Handles txtUpfrontFees.TextChanged
        Try
            If IsNumeric(toMoney(txtFinReqAmt.Text)) And IsNumeric(toMoney(txtUpfrontFees.Text)) Then
                If CDbl(txtFinReqAmt.Text) <= CDbl(txtUpfrontFees.Text) Then
                    notify("Upfront fees cannot be greater than or equal to amount required", "error")
                Else
                    txtAmtToDisburse.Text = FormatNumber(txtFinReqAmt.Text - txtUpfrontFees.Text, 2)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


    
End Class