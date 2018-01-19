Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web

Partial Class ParaCreditLimit
    Inherits System.Web.UI.Page
    Dim cmd As SqlCommand
    Dim con As New SqlConnection
    Dim adp As New SqlDataAdapter
    Dim connection As String
    Dim urlPermission As String = "PermissionDenied.aspx"
    Public Shared limitEditID As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.MaintainScrollPositionOnPostBack = True
            'Page.Header.TiSStle = "Credit Management : Client Rating"
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            If Not IsPostBack Then
                loadClientTypes()
                loadCreditTypes()
                getLimits()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Protected Sub loadCreditTypes()
        Try
            cmd = New SqlCommand("select * from Z_LOAN_TYPE", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "LOANS")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbCreditType.DataSource = ds.Tables(0)
                cmbCreditType.DataValueField = "LOAN_SHORT_DESC"
                cmbCreditType.DataTextField = "LOAN_LONG_DESC"
            Else
                cmbCreditType.DataSource = Nothing
            End If
            cmbCreditType.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub loadClientTypes()
        Try
            cmd = New SqlCommand("select * from PARA_CLIENT_TYPES", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "Clients")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbClientType.DataSource = ds.Tables(0)
                cmbClientType.DataValueField = "CLIENT_TYPE"
                cmbClientType.DataTextField = "CLIENT_TYPE"
            Else
                cmbClientType.DataSource = Nothing
            End If
            cmbClientType.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub getLimits()
        Try
            cmd = New SqlCommand("select PARA_CREDIT_LIMIT.*,Z_LOAN_TYPE.LOAN_LONG_DESC from PARA_CREDIT_LIMIT,Z_LOAN_TYPE where CLIENT_TYPE='" & cmbClientType.SelectedValue & "' and Z_LOAN_TYPE.LOAN_SHORT_DESC=PARA_CREDIT_LIMIT.CREDIT_TYPE", con)
            'msgbox(cmd.CommandText)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "Limits")
            If ds.Tables(0).Rows.Count > 0 Then
                grdLimit.DataSource = ds.Tables(0)
            Else
                grdLimit.DataSource = Nothing
            End If
            grdLimit.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
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

    Protected Sub btnAddLimit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddLimit.Click
        Try
            cmd = New SqlCommand("select * from PARA_CREDIT_LIMIT where CLIENT_TYPE='" & cmbClientType.SelectedValue & "' and CREDIT_TYPE='" & cmbCreditType.SelectedValue & "'", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "LIMITS")
            If ds.Tables(0).Rows.Count > 0 Then
                cmd = New SqlCommand("update PARA_CREDIT_LIMIT set LIMIT='" & Trim(txtLimit.Text) & "' where CLIENT_TYPE='" & cmbClientType.SelectedValue & "' and CREDIT_TYPE='" & cmbCreditType.SelectedValue & "'", con)
            Else
                cmd = New SqlCommand("insert into PARA_CREDIT_LIMIT (CLIENT_TYPE,CREDIT_TYPE,LIMIT) values ('" & BankString.removeSpecialCharacter(cmbClientType.SelectedValue) & "','" & BankString.removeSpecialCharacter(cmbCreditType.SelectedValue) & "','" & txtLimit.Text & "')", con)
            End If
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            msgbox("New limit entered")
            clearAll()
            getLimits()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub clearAll()
        Try
            txtLimit.Text = ""
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub cmbClientType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbClientType.SelectedIndexChanged
        getLimits()
    End Sub

    Protected Sub grdLimit_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdLimit.RowCancelingEdit
        grdLimit.EditIndex = -1
        getLimits()
    End Sub

    Protected Sub grdLimit_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdLimit.RowDeleting
        limitEditID = DirectCast(grdLimit.Rows(e.RowIndex).FindControl("txtGrdLimitID"), TextBox).Text
        cmd = New SqlCommand("delete from PARA_CREDIT_LIMIT where ID='" & limitEditID & "'", con)
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        If cmd.ExecuteNonQuery Then
            msgbox("Successfully deleted")
        Else
            msgbox("Error deleting")
        End If
        con.Close()
        getLimits()
    End Sub

    Protected Sub grdLimit_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdLimit.RowEditing
        limitEditID = DirectCast(grdLimit.Rows(e.NewEditIndex).FindControl("txtGrdLimitID"), TextBox).Text
        grdLimit.EditIndex = e.NewEditIndex
        getLimits()
    End Sub

    Protected Sub grdLimit_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdLimit.RowUpdating
        If Trim(limitEditID) = "" Or IsDBNull(limitEditID) Then
            msgbox("No record selected for update")
            Exit Sub
        End If

        Dim limit As String = DirectCast(grdLimit.Rows(e.RowIndex).FindControl("txtLimitEdit"), TextBox).Text
        cmd = New SqlCommand("update PARA_CREDIT_LIMIT set LIMIT='" & limit & "' where ID='" & limitEditID & "'", con)

        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        If cmd.ExecuteNonQuery Then
            msgbox("Successfully updated")
        Else
            msgbox("Error updating value")
        End If
        con.Close()
        grdLimit.EditIndex = -1
        getLimits()
    End Sub
End Class
