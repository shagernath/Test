Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web

Partial Class ParaClientTypes
    Inherits System.Web.UI.Page
    Dim cmd As SqlCommand
    Dim con As New SqlConnection
    Dim adp As New SqlDataAdapter
    Dim connection As String
    Dim urlPermission As String = "PermissionDenied.aspx"
    Public Shared typeEditID As Double

    Protected Sub btnAddClientType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddClientType.Click
        Try
            cmd = New SqlCommand("select * from PARA_CLIENT_TYPES where CLIENT_TYPE='" & txtClientType.Text & "'", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "TYPES")
            If ds.Tables(0).Rows.Count > 0 Then
                cmd = New SqlCommand("update PARA_CLIENT_TYPES set CLIENT_TYPE_DESC='" & BankString.removeSpecialCharacter(Trim(txtTypeDescription.Text)) & "' where CLIENT_TYPE='" & Trim(txtClientType.Text) & "'", con)
            Else
                cmd = New SqlCommand("insert into PARA_CLIENT_TYPES (CLIENT_TYPE,CLIENT_TYPE_DESC) values ('" & BankString.removeSpecialCharacter(Trim(txtClientType.Text)) & "','" & BankString.removeSpecialCharacter(Trim(txtTypeDescription.Text)) & "')", con)
            End If
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            msgbox("New client type entered")
            clearAll()
            getClienTypes()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub clearAll()
        Try
            txtClientType.Text = ""
            txtTypeDescription.Text = ""
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.MaintainScrollPositionOnPostBack = True
            'Page.Header.TiSStle = "Credit Management : Client Rating"
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            If Not IsPostBack Then
                getClienTypes()
            End If
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub getClienTypes()
        Try
            cmd = New SqlCommand("select * from PARA_CLIENT_TYPES", con)
            'msgbox(cmd.CommandText)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "ClientTypes")
            If ds.Tables(0).Rows.Count > 0 Then
                grdClientType.DataSource = ds.Tables(0)
            Else
                grdClientType.DataSource = Nothing
            End If
            grdClientType.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub grdClientType_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdClientType.RowCancelingEdit
        grdClientType.EditIndex = -1
        getClienTypes()
    End Sub

    Protected Sub grdClientType_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdClientType.RowDeleting
        typeEditID = DirectCast(grdClientType.Rows(e.RowIndex).FindControl("txtGrdTypeID"), TextBox).Text
        cmd = New SqlCommand("delete from PARA_CLIENT_TYPES where ID='" & typeEditID & "'", con)
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
        getClienTypes()
    End Sub

    Protected Sub grdClientType_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdClientType.RowEditing
        typeEditID = DirectCast(grdClientType.Rows(e.NewEditIndex).FindControl("txtGrdTypeID"), TextBox).Text
        grdClientType.EditIndex = e.NewEditIndex
        getClienTypes()
    End Sub

    Protected Sub grdClientType_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdClientType.RowUpdating
        If Trim(typeEditID) = "" Or IsDBNull(typeEditID) Then
            msgbox("No record selected for update")
            Exit Sub
        End If

        Dim newClientType As String = DirectCast(grdClientType.Rows(e.RowIndex).FindControl("txtClientTypeEdit"), TextBox).Text
        Dim newTypeDesc As String = DirectCast(grdClientType.Rows(e.RowIndex).FindControl("txtGrdTypeDescEdit"), TextBox).Text
        cmd = New SqlCommand("update PARA_CLIENT_TYPES set CLIENT_TYPE='" & BankString.removeSpecialCharacter(newClientType) & "',CLIENT_TYPE_DESC='" & BankString.removeSpecialCharacter(newTypeDesc) & "' where ID='" & typeEditID & "'", con)

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
        grdClientType.EditIndex = -1
        getClienTypes()
    End Sub
End Class
