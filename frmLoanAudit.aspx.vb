Imports System.Data
Imports System.Data.SqlClient

Partial Class frmLoanAudit
    Inherits System.Web.UI.Page
    Dim cmd As SqlCommand
    Dim adp As SqlDataAdapter
    Dim con As New SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        'Page.Header.Title = "Credit Management: Print Loan Reports"
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
        If Not IsPostBack Then
            loadBranches()
            loadUsers()
            loadProdType()
        End If
    End Sub

    Public Sub msgbox(ByVal strMessage As String)

        'finishes server processing, returns to client.
        Dim strScript As String = "<script language=JavaScript>"
        strScript += "window.alert(""" & strMessage & """);"
        strScript += "</script>"
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = strScript
        Page.Controls.Add(lbl)

    End Sub

    Protected Sub loadBranches()
        Try
            cmd = New SqlCommand("select * from BNCH_DETAILS", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "BRANCH")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbBranch.DataSource = ds.Tables(0)
                cmbBranch.DataValueField = "BNCH_CODE"
                cmbBranch.DataTextField = "BNCH_NAME"
            Else
                cmbBranch.DataSource = Nothing
            End If
            cmbBranch.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub loadUsers()
        Try
            cmd = New SqlCommand("select distinct CRBY from Z_LOAN_SUBMISSION_AUDIT", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "AUDIT")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbUser.DataSource = ds.Tables(0)
                cmbUser.DataValueField = "CRBY"
                cmbUser.DataTextField = "CRBY"
            Else
                cmbUser.DataSource = Nothing
            End If
            cmbUser.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub loadProdType()
        Try
            cmd = New SqlCommand("select * from Z_LOAN_TYPE", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "TYPE")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbProdType.DataSource = ds.Tables(0)
                cmbProdType.DataValueField = "LOAN_SHORT_DESC"
                cmbProdType.DataTextField = "LOAN_LONG_DESC"
            Else
                cmbProdType.DataSource = Nothing
            End If
            cmbProdType.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub branchesVisible()
        Try
            lblBranch.Visible = True
            cmbBranch.Visible = True
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub branchesInvisible()
        Try
            lblBranch.Visible = False
            cmbBranch.Visible = False
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub usersVisible()
        Try
            lblUser.Visible = True
            cmbUser.Visible = True
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub usersInvisible()
        Try
            lblUser.Visible = False
            cmbUser.Visible = False
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub prodTypeVisible()
        Try
            lblProdType.Visible = True
            cmbProdType.Visible = True
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub prodTypeInvisible()
        Try
            lblProdType.Visible = False
            cmbProdType.Visible = False
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub datesVisible()
        Try
            lblDateFrom.Visible = True
            bdpFromDate.Visible = True
            lblDateTo.Visible = True
            bdptoDate.Visible = True
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub datesInvisible()
        Try
            lblDateFrom.Visible = False
            bdpFromDate.Visible = False
            lblDateTo.Visible = False
            bdptoDate.Visible = False
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub chkBranch_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBranch.CheckedChanged
        Try
            If chkBranch.Checked Then
                branchesVisible()
            Else
                branchesInvisible()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub chkProdType_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkProdType.CheckedChanged
        Try
            If chkProdType.Checked Then
                prodTypeVisible()
            Else
                prodTypeInvisible()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub chkDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDate.CheckedChanged
        Try
            If chkDate.Checked Then
                datesVisible()
            Else
                datesInvisible()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
        Try
            Dim strscript As String
            Dim EncQuery As New BankEncryption64
            strscript = "<script langauage=JavaScript>"
            strscript += "window.open('rptLoanAuditRpt.aspx?qry=" & EncQuery.Encrypt(buildQuery(), "lovely12345") & "');"
            strscript += "</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "newwin", strscript)
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Function buildQuery() As String
        Try
            Dim qryString As String = ""
            Dim query As New ArrayList
            If chkBranch.Checked Then
                query.Add("TLR_BRANCH_CODE = '" & cmbBranch.SelectedValue & "'")
            End If
            If chkUser.Checked Then
                query.Add("CRBY = '" & cmbUser.SelectedValue & "'")
            End If
            If chkDate.Checked Then
                query.Add("DATE_PERFORMED BETWEEN '" & DateFormat.getSaveDate(bdpFromDate.SelectedDate) & "' AND '" & DateFormat.getSaveDate(bdptoDate.SelectedDate) & "'")
            End If
            If chkProdType.Checked Then
                query.Add("LOAN_TY = '" & cmbProdType.SelectedValue & "'")
            End If
            Try
                For Each StrItem As String In query
                    If StrItem = query(0) Then
                        qryString = "and " & StrItem
                    Else
                        qryString = qryString & " AND " & StrItem
                    End If
                Next
            Catch ex As Exception

            End Try
            Return qryString
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub chkUser_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUser.CheckedChanged
        Try
            If chkUser.Checked Then
                usersVisible()
            Else
                usersInvisible()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class