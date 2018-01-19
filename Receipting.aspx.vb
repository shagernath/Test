Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class Accounting_Receipting
    Inherits System.Web.UI.Page
    Dim cmd As SqlCommand
    Dim con As New SqlConnection
    Dim adp As New SqlDataAdapter
    Public Sub msgbox(ByVal strMessage As String)

        'finishes server processing, returns to client.
        Dim strScript As String = "<script language=JavaScript>"
        strScript += "window.alert(""" & strMessage & """);"
        strScript += "</script>"
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = strScript
        Page.Controls.Add(lbl)

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
        If Not IsPostBack Then
            loadBathces()
            loadFinAccs()
            loadIndvAccs()
            getCutOffDate()
            getCashCutOffDate()
        End If
    End Sub

    Protected Sub loadFinAccs()
        Try
            cmd = New SqlCommand("select AccountName,  convert(varchar,MainAccount) +'/'+ convert(varchar,SubAccount) as 'Accno' from tbl_FinancialAccountsCreation", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "AccountsTypes")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbAccount.DataSource = ds
                cmbAccount.DataTextField = "AccountName"
                cmbAccount.DataValueField = "Accno"
                cmbAccount.DataBind()
                cmbAccount.Items.Add("-Select-")
                cmbAccount.SelectedIndex = cmbAccount.Items.Count - 1
            Else
                cmbAccount.DataSource = Nothing
                cmbAccount.DataBind()
            End If

        Catch ex As Exception
            msgbox(ex.Message)
        End Try

    End Sub
    Protected Sub loadBathces()
        '  Try
        cmd = New SqlCommand("select * from [tbl_BatchRec] where BatchType= 'Receipting' and [status]=0", con)
        Dim ds As New DataSet
        adp = New SqlDataAdapter(cmd)
        adp.Fill(ds, "BatchRec")
        If ds.Tables(0).Rows.Count > 0 Then
            cmbBatchNo.DataSource = ds
            cmbBatchNo.DataTextField = "BatchNo"
            cmbBatchNo.DataValueField = "id"
            cmbBatchNo.DataBind()
            cmbBatchNo.Items.Add(" ")
            cmbBatchNo.SelectedIndex = cmbBatchNo.Items.Count - 1
        Else
            cmbBatchNo.DataSource = Nothing
            cmbBatchNo.DataBind()
        End If

        'Catch ex As Exception
        'msgbox(ex.Message)
        'End Try
    End Sub
    Protected Sub loadIndvAccs()
        Try
            cmd = New SqlCommand("select [SURNAME]+' '+ [FORENAMES] + ' | ' + CUSTOMER_NUMBER as Name, CUSTOMER_NUMBER from QUEST_APPLICATION order by 'Name'", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "QUEST_APPLICATION")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbAccount0.DataSource = ds
                cmbAccount0.DataTextField = "Name"
                cmbAccount0.DataValueField = "CUSTOMER_NUMBER"
                cmbAccount0.DataBind()
                cmbAccount0.Items.Add("-Select-")
                cmbAccount0.SelectedIndex = cmbAccount0.Items.Count - 1
            Else
                cmbAccount0.DataSource = Nothing
                cmbAccount0.DataBind()
            End If

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub loadGrid()
        Try
            cmd = New SqlCommand("select * from Accounts_Transactions where BatchRef= '" & cmbBatchNo.SelectedItem.Text & "'", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "Accounts")
            If ds.Tables(0).Rows.Count > 0 Then
                grdDetails.DataSource = ds.Tables(0)
                grdDetails.DataBind()
            Else
                grdDetails.DataSource = Nothing
                grdDetails.DataBind()
            End If
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub loadGrid2()
        Try
            cmd = New SqlCommand("select [BatchNo],[BatchType] ,[Amount] ,[NumberOfTrxns],[CreatedBy],convert(varchar,[DateCreated],106) as BatchDate from tbl_BatchRec where [BatchNo]= '" & cmbBatchNo.SelectedItem.Text & "'", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "Accounts")
            If ds.Tables(0).Rows.Count > 0 Then
                grdDetails0.DataSource = ds.Tables(0)
                grdDetails0.DataBind()
            Else
                grdDetails0.DataSource = Nothing
                grdDetails0.DataBind()
            End If
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Public Function Checkfeilds() As Boolean
        Checkfeilds = False
        Try
            If Not IsNumeric(txtAmount.Text) Then
                msgbox("Amount Must Be Numeric")
                txtAmount.Focus()
                Exit Function
            End If
            If txtAmount.Text = "" Then
                msgbox("Amount Is Mandatory")
                txtAmount.Focus()
                Exit Function
            End If
            If cmbAccount.SelectedValue = "-Select-" Then
                msgbox("Account Is Mandatory")
                cmbAccount.Focus()
                Exit Function
            End If
            If txtRef.Text = "" Then
                msgbox("Ref Is Mandatory")
                txtRef.Focus()
                Exit Function
            End If
            If cmbBatchNo.SelectedItem.Text = "-Select-" Then
                msgbox("Select a valid Batch")
                cmbBatchNo.Focus()
                Exit Function
            End If
            Checkfeilds = True
        Catch ex As Exception

        End Try
    End Function
    Protected Sub btnSaveTrxn_Click(sender As Object, e As EventArgs) Handles btnSaveTrxn.Click
        ' Try
        If cmbBatchNo.Text.Trim <> "" Then
            cmd = New SqlCommand("select (amount) as batch_amount, sum(debit) as debits, sum(credit) as credits, count(b.id) as trxns from tbl_BatchRec a, Accounts_Transactions b where a.batchno=b.batchref and  a.BatchNo= '" & cmbBatchNo.SelectedItem.Text & "' group by amount", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "batch")
            'msgbox(cmd.CommandText)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("batch_amount") <> ds.Tables(0).Rows(0).Item("debits") Or ds.Tables(0).Rows(0).Item("debits") <> ds.Tables(0).Rows(0).Item("credits") Then
                    msgbox("Batch Not Balanced, Not Committed!")
                    Exit Sub
                Else
                    cmd = New SqlCommand("Update tbl_BatchRec set status = 1 where BatchNo='" & cmbBatchNo.SelectedItem.Text & "'", con)
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                    loadBathces()
                    ClearFeilds()

                    'msgbox("Batch Balanced, Committed")
                    'Response.Redirect("~/Accounting/Cashbook.aspx")
                    Response.Write("<script>alert('Batch Balanced, Committed') ; location.href='Receipting.aspx'</script>")

                End If
            End If
        Else

        End If

        'Catch ex As Exception
        '    msgbox(ex.Message)
        'End Try
    End Sub

    Protected Sub BalanceBatch()
        Try

        Catch ex As Exception
            msgbox(ex.ToString)
        End Try
    End Sub

    Protected Sub btnSaveTrxn3_Click(sender As Object, e As EventArgs) Handles btnSaveTrxn3.Click
        Try
            If cmbAccount.SelectedValue.Substring(0, 4) = "211/" Then
                If CDate(dtpTrxnDate.Text) <= CDate(lblCashCutOffDate.Text) Then
                    msgbox("You cannot post a cash transaction before the cut off date")
                    dtpTrxnDate.Focus()
                    Exit Sub
                End If
            Else
                If CDate(dtpTrxnDate.Text) <= CDate(lblCutOffDate.Text) Then
                    msgbox("You cannot post a transaction before the cut off date")
                    dtpTrxnDate.Focus()
                    Exit Sub
                End If
            End If
            If Checkfeilds() = True Then
                Dim PayType As String
                If rdbPayType.SelectedIndex = 0 Then
                    PayType = "211/1"
                Else
                    PayType = "212/1"
                End If
                Dim cramount, dramount As Double
                dramount = 0.0
                cramount = CDbl(txtAmount.Text)

                cmd = New SqlCommand("SaveAccountsTrxnsTempWithContra", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Type", "Receipt")
                cmd.Parameters.AddWithValue("@Category", "")
                cmd.Parameters.AddWithValue("@Ref", txtRef.Text)
                cmd.Parameters.AddWithValue("@Desc", txtdesc.Text)
                cmd.Parameters.AddWithValue("@Debit", dramount)
                cmd.Parameters.AddWithValue("@Credit", cramount)
                cmd.Parameters.AddWithValue("@Account", cmbAccount.SelectedValue)
                cmd.Parameters.AddWithValue("@ContraAccount", PayType)
                cmd.Parameters.AddWithValue("@Status", IIf(rdbType0.SelectedIndex = 0, 1, 0))
                cmd.Parameters.AddWithValue("@Other", IIf(cmbAccount0.SelectedValue <> "-Select-", cmbAccount0.SelectedValue, 0))
                cmd.Parameters.AddWithValue("@BankAccID", "")
                cmd.Parameters.AddWithValue("@BankAccName", "")
                cmd.Parameters.AddWithValue("@BatchRef", cmbBatchNo.SelectedItem.Text)
                cmd.Parameters.AddWithValue("@TrxnDate", dtpTrxnDate.Text)
                cmd.Parameters.AddWithValue("@CaptureBy", Session("UserId"))

                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery() Then
                    If SaveContra() = True Then
                        msgbox("Receipt Saved Successfully")
                        loadGrid()
                        ClearFeilds()
                    Else
                        msgbox("Error Saving Details")
                    End If

                Else
                    msgbox("Error Saving Details")
                End If
                con.Close()
            End If
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Public Function SaveContra() As Boolean
        SaveContra = False
        Try
            SaveContra = False
            Dim bankacc As String
            Dim acc As Integer
            If Checkfeilds() = True Then
                Dim PayType As String
                If rdbPayType.SelectedIndex = 0 Then
                    PayType = "211/1"
                    bankacc = ""
                    acc = 0
                Else
                    PayType = "212/1"
                    If cmbAccount1.Text = "" Then
                        bankacc = ""
                        acc = 0
                    Else
                        bankacc = cmbAccount1.SelectedItem.Text
                        acc = cmbAccount1.SelectedValue
                    End If
                End If
                Dim cramount, dramount As Double
                dramount = CDbl(txtAmount.Text)
                cramount = 0.0

                cmd = New SqlCommand("SaveAccountsTrxns", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Type", "Receipt")
                cmd.Parameters.AddWithValue("@Category", "")
                cmd.Parameters.AddWithValue("@Ref", txtRef.Text)
                cmd.Parameters.AddWithValue("@Desc", txtdesc.Text)
                cmd.Parameters.AddWithValue("@Debit", dramount)
                cmd.Parameters.AddWithValue("@Credit", cramount)
                cmd.Parameters.AddWithValue("@Account", PayType)
                cmd.Parameters.AddWithValue("@ContraAccount", cmbAccount.SelectedValue)
                cmd.Parameters.AddWithValue("@Status", IIf(rdbType0.SelectedIndex = 0, 1, 0))
                cmd.Parameters.AddWithValue("@Other", IIf(cmbAccount0.SelectedValue <> "-Select-", cmbAccount0.SelectedValue, 0))
                cmd.Parameters.AddWithValue("@BankAccID", acc)
                cmd.Parameters.AddWithValue("@BankAccName", bankacc)
                cmd.Parameters.AddWithValue("@BatchRef", cmbBatchNo.SelectedItem.Text)
                cmd.Parameters.AddWithValue("@TrxnDate", dtpTrxnDate.Text)

                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery() Then
                    SaveContra = True
                Else
                    msgbox("Error Saving Details")
                End If
                con.Close()
            End If
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Function
    Public Sub ClearFeilds()
        txtAmount.Text = ""
        txtdesc.Text = ""
        txtRef.Text = ""
        cmbAccount.SelectedIndex = cmbAccount.Items.Count - 1
        cmbAccount0.SelectedIndex = cmbAccount0.Items.Count - 1
        rdbPayType.SelectedIndex = -1
        rdbType0.SelectedIndex = -1
    End Sub

    Protected Sub rdbPayType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdbPayType.SelectedIndexChanged
        If rdbPayType.SelectedIndex = 1 Then
            cmbAccount1.Visible = True
            loadBanks()
        Else
            cmbAccount1.Visible = False
            cmbAccount1.DataSource = Nothing
            cmbAccount1.DataBind()
        End If
    End Sub

    Protected Sub loadBanks()
        Try
            cmd = New SqlCommand("select * from tbl_BankAccounts", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "banks")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbAccount1.DataSource = ds
                cmbAccount1.DataTextField = "Code"
                cmbAccount1.DataValueField = "id"
                cmbAccount1.DataBind()
                cmbAccount1.Items.Add("-Select-")
                cmbAccount1.SelectedIndex = cmbAccount1.Items.Count - 1
            Else
                cmbAccount1.DataSource = Nothing
                cmbAccount1.DataBind()
            End If

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub cmbBatchNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBatchNo.SelectedIndexChanged
        loadGrid2()
        loadGrid()
    End Sub

    Protected Sub cmbAccount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAccount.SelectedIndexChanged
        If cmbAccount.SelectedItem.Text = "Loan Debtors" Then
            lblLoanDebtor.Visible = True
            cmbAccount0.Visible = True
            getDefault()
        Else
            lblLoanDebtor.Visible = False
            cmbAccount0.Visible = False
            getDefault()
        End If
    End Sub

    Public Sub getDefault()
        '  Try

        cmd = New SqlCommand("select [Default]  from tbl_FinancialAccountsCreation where [AccountName]= '" & cmbAccount.SelectedItem.Text & "'", con)
        Dim ds As New DataSet
        adp = New SqlDataAdapter(cmd)
        adp.Fill(ds, "Accounts")
        If ds.Tables(0).Rows.Count > 0 Then
            lblDefault.Text = ds.Tables(0).Rows(0).Item("Default").ToString
        End If

        'Catch ex As Exception
        '    msgbox(ex.ToString)
        'End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Try

        Dim i As Integer
        For i = 0 To grdDetails.Rows.Count - 1
            Dim ch As CheckBox
            ch = CType(grdDetails.Rows(i).Cells(0).FindControl("CheckBox2"), CheckBox)
            If ch.Checked Then

                cmd = New SqlCommand("delete from  Accounts_Transactions where ID=" & grdDetails.Rows(i).Cells(1).Text & "", con)
                cmd.CommandType = CommandType.Text
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                cmd.ExecuteNonQuery()
                'If cmd.ExecuteNonQuery() Then
                '    loadGrid()
                'Else
                '    msgbox("Trxns Not Found")
                '    loadGrid()
                'End If
            End If
        Next
        loadGrid()
        'Catch ex As Exception

        'End Try

    End Sub

    Protected Sub getCutOffDate()
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd As New SqlCommand("select id,convert(varchar,CutOff,106) as [Date],CapturedBy as [Captured By],convert(varchar,CaptureDate,113) as [Capture Date] from AccCutOffDates where Authorised=1 order by id desc", con)
                Dim ds As New DataSet
                Dim adp = New SqlDataAdapter(cmd)
                adp.Fill(ds, "APP")
                If ds.Tables(0).Rows.Count > 0 Then
                    lblCutOffDate.Text = ds.Tables(0).Rows(0).Item("Date")
                Else
                    lblCutOffDate.Text = ""
                End If
            End Using
        End Using
    End Sub

    Protected Sub getCashCutOffDate()
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd As New SqlCommand("select id,convert(varchar,CutOff,106) as [Date],CapturedBy as [Captured By],convert(varchar,CaptureDate,113) as [Capture Date] from AccCashCutOffDates where Authorised=1 order by id desc", con)
                Dim ds As New DataSet
                Dim adp = New SqlDataAdapter(cmd)
                adp.Fill(ds, "APP")
                If ds.Tables(0).Rows.Count > 0 Then
                    lblCashCutOffDate.Text = ds.Tables(0).Rows(0).Item("Date")
                Else
                    lblCashCutOffDate.Text = ""
                End If
            End Using
        End Using
    End Sub
End Class