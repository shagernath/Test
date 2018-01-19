Imports System.Data
Imports System.Data.SqlClient

Partial Class frmUserMgtAudit
    Inherits System.Web.UI.Page
    Dim cmd As SqlCommand
    Dim adp As SqlDataAdapter
    Dim con As New SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        'Page.Header.Title = "Credit Management: Print Loan Reports"
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
        If Not IsPostBack Then
            loadUsers()
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

    Protected Sub loadUsers()
        Try
            cmd = New SqlCommand("select distinct USER_LOGIN from MASTER_USERS", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "AUDIT")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbUser.DataSource = ds.Tables(0)
                cmbUser.DataValueField = "USER_LOGIN"
                cmbUser.DataTextField = "USER_LOGIN"
            Else
                cmbUser.DataSource = Nothing
            End If
            cmbUser.DataBind()
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

    Protected Sub datesVisible()
        Try
            lblDateFrom.Visible = True
            bdpFromDate.Visible = True
            lblDateTo.Visible = True
            bdptoDate.Visible = True
            fromSpan.Visible = True
            toSpan.Visible = True
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
            fromSpan.Visible = False
            toSpan.Visible = False
        Catch ex As Exception
            msgbox(ex.Message)
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
            Dim strURL As String = ""
            If cmbRptType.SelectedValue = "Roles" Then
                If cmbActionType.SelectedValue = "Add" Then
                    strURL = "rptUserMgtRolesAdd.aspx"
                ElseIf cmbActionType.SelectedValue = "Update" Then
                    strURL = "rptUserMgtRolesUpdate.aspx"
                End If
            ElseIf cmbRptType.SelectedValue = "Users" Then
                If cmbActionType.SelectedValue = "Add" Then
                    strURL = "rptUserMgtUsersAdd.aspx"
                ElseIf cmbActionType.SelectedValue = "Update" Then
                    strURL = "rptUserMgtUsersUpdate.aspx"
                End If
            ElseIf cmbRptType.SelectedValue = "Branches" Then
                If cmbActionType.SelectedValue = "Add" Then
                    strURL = "rptUserMgtBranchesAdd.aspx"
                ElseIf cmbActionType.SelectedValue = "Update" Then
                    strURL = "rptUserMgtBranchesUpdate.aspx"
                End If
            End If
            If strURL <> "" Then
                strscript = "<script langauage=JavaScript>"
                strscript += "window.open('" & strURL & "?qry=" & EncQuery.Encrypt(buildQuery(), "lovely12345") & "');"
                strscript += "</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "newwin", strscript)
            Else

            End If

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Function buildQuery() As String
        Try
            Dim qryFieldUser As String = ""
            Dim qryFieldDate As String = ""
            If cmbRptType.SelectedValue = "Roles" Then
                qryFieldUser = "USER_CREATED_BY"
                qryFieldDate = "USER_CREATED_DATE"
            ElseIf cmbRptType.SelectedValue = "Users" Then
                qryFieldUser = "USER_MODIFIED_BY"
                qryFieldDate = "USER_MODIFIED_DATE"
            ElseIf cmbRptType.SelectedValue = "Branches" Then
                qryFieldUser = "PERFORMED_BY"
                qryFieldDate = "PERFORMED_DATE"
            End If

            Dim qryString As String = ""
            Dim query As New ArrayList
            If chkUser.Checked Then
                query.Add("" & qryFieldUser & " = '" & cmbUser.SelectedValue & "'")
            End If
            If chkDate.Checked Then
                query.Add("" & qryFieldDate & " BETWEEN '" & DateFormat.getSaveDate(bdpFromDate.Text) & "' AND '" & DateFormat.getSaveDate(bdptoDate.Text) & "'")
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