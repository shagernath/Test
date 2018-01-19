Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web

Partial Class ParaExtValue
    Inherits System.Web.UI.Page
    Dim cmd As SqlCommand
    Dim con As New SqlConnection
    Dim adp As New SqlDataAdapter
    Dim connection As String
    Dim urlPermission As String = "PermissionDenied.aspx"
    Public Shared valueEditID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Page.MaintainScrollPositionOnPostBack = True
            If Not IsPostBack Then
                getValues()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub getValues()
        Try
            cmd = New SqlCommand("select * from PARA_EXT_VALUE", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "Value")
            If ds.Tables(0).Rows.Count > 0 Then
                grdValues.DataSource = ds.Tables(0)
            Else
                grdValues.DataSource = Nothing
            End If
            grdValues.DataBind()
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

    Protected Sub grdValues_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdValues.RowCancelingEdit
        grdValues.EditIndex = -1
        getValues()
    End Sub

    Protected Sub grdValues_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdValues.RowDeleting
        Try
            valueEditID = DirectCast(grdValues.Rows(e.RowIndex).FindControl("txtOldSecType"), TextBox).Text
            'cmd = New SqlCommand("update BNCH_DETAILS set active=0 where BNCH_CODE='" & branchEditID & "'", con)
            Dim cmdSelect = New SqlCommand("select * from PARA_EXT_VALUE where SECURITY_TYPE='" & valueEditID & "'", con)
            Dim dsSelect As New DataSet
            Dim adpSel = New SqlDataAdapter(cmd)
            adpSel.Fill(dsSelect, "VALUES")

            'Dim cmdAudit As SqlCommand
            'cmdAudit = New SqlCommand("insert into TEMP_BNCH_DETAILS ([ACTION],[PERFORMED_BY],[PERFORMED_DATE],[BNCH_CODE],[BNCH_NAME],[BNCH_ADDRESS],[BNCH_PHONENO],[BNCH_FAXNO]) values ('DELETE','" & Session("UserID") & "',getDate(),'" & dsSelect.Tables(0).Rows(0).Item("BNCH_CODE") & "','" & BankString.removeSpecialCharacter(dsSelect.Tables(0).Rows(0).Item("BNCH_NAME")) & "','" & BankString.removeSpecialCharacter(dsSelect.Tables(0).Rows(0).Item("BNCH_ADDRESS")) & "','" & dsSelect.Tables(0).Rows(0).Item("BNCH_PHONENO") & "','')", con)
            cmd = New SqlCommand("delete from PARA_EXT_VALUE where SECURITY_TYPE='" & valueEditID & "'", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            If cmd.ExecuteNonQuery Then
                'cmdAudit.ExecuteNonQuery()
                msgbox("Successfully deleted")
            Else
                msgbox("Error deleting")
            End If
            con.Close()
            getValues()

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub grdValues_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdValues.RowEditing
        Try
            valueEditID = DirectCast(grdValues.Rows(e.NewEditIndex).FindControl("txtOldSecType"), TextBox).Text
            grdValues.EditIndex = e.NewEditIndex
            getValues()

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub grdValues_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdValues.RowUpdating
        Try
            If Trim(valueEditID) = "" Or IsDBNull(valueEditID) Then
                msgbox("No record selected for update")
                Exit Sub
            End If
            Dim secType As String = DirectCast(grdValues.Rows(e.RowIndex).FindControl("txtGrdSecType"), TextBox).Text

            Dim percVal As String = DirectCast(grdValues.Rows(e.RowIndex).FindControl("txtExtValueEdit"), TextBox).Text
            'Dim bnchAddress As String = DirectCast(grdBranches.Rows(e.RowIndex).FindControl("txtGrdBranchAddressEdit"), TextBox).Text

            'Dim bnchPhone As String = DirectCast(grdBranches.Rows(e.RowIndex).FindControl("txtBranchPhoneEdit"), TextBox).Text

            'Dim oldBnchCode, oldBnchName, oldBnchAddress, oldBnchPhone, oldBnchFax As String
            'oldBnchCode = ""
            'oldBnchName = ""
            'oldBnchAddress = ""
            'oldBnchPhone = ""
            'oldBnchFax = ""

            'cmd = New SqlCommand("select * from PARA_EXT_VALUE where SECURITY_TYPE='" & valueEditID & "'", con)
            'Dim ds1 As New DataSet
            'adp = New SqlDataAdapter(cmd)
            'adp.Fill(ds1, "VALUE")
            'If ds1.Tables(0).Rows.Count > 0 Then
            '    oldBnchCode = ds1.Tables(0).Rows(0).Item("BNCH_CODE").ToString
            '    oldBnchName = ds1.Tables(0).Rows(0).Item("BNCH_NAME").ToString
            '    oldBnchAddress = ds1.Tables(0).Rows(0).Item("BNCH_ADDRESS").ToString
            '    oldBnchPhone = ds1.Tables(0).Rows(0).Item("BNCH_PHONENO").ToString
            '    oldBnchFax = ds1.Tables(0).Rows(0).Item("BNCH_FAXNO").ToString
            'End If

            cmd = New SqlCommand("update PARA_EXT_VALUE set SECURITY_TYPE='" & secType & "', EXT_VALUE_PERC='" & percVal & "' where SECURITY_TYPE='" & valueEditID & "'", con)
            'Dim cmdAudit As SqlCommand
            'cmdAudit = New SqlCommand("insert into TEMP_BNCH_DETAILS ([ACTION],[PERFORMED_BY],[PERFORMED_DATE],[OLD_BNCH_CODE],[BNCH_CODE],[OLD_BNCH_NAME],[BNCH_NAME],[OLD_BNCH_ADDRESS],[BNCH_ADDRESS],[OLD_BNCH_PHONENO],[BNCH_PHONENO],[OLD_BNCH_FAXNO],[BNCH_FAXNO]) values ('UPDATE','" & Session("UserID") & "',getDate(),'" & oldBnchCode & "','" & bnchCode & "','" & oldBnchName & "','" & bnchName & "','" & oldBnchAddress & "','" & bnchAddress & "','" & oldBnchPhone & "','" & bnchPhone & "','" & oldBnchFax & "','')", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            If cmd.ExecuteNonQuery Then
                'cmdAudit.ExecuteNonQuery()
                msgbox("Successfully updated")
            Else
                msgbox("Error updating value")
            End If
            con.Close()
            grdValues.EditIndex = -1
            getValues()

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub btnAddValue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddValue.Click
        Try
            If Trim(txtExtValue.Text) = "" Then
                msgbox("Enter extendible value")
                txtExtValue.Focus()
                Exit Sub
            End If
            Dim cmdValue As New SqlCommand
            cmd = New SqlCommand("Select * from PARA_EXT_VALUE where SECURITY_TYPE='" & cmbSecType.SelectedValue & "'", con)
            Dim ds As New DataSet
            Dim adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "VALUE")
            If ds.Tables(0).Rows.Count > 0 Then
                cmdValue = New SqlCommand("update PARA_EXT_VALUE set EXT_VALUE_PERC='" & Trim(txtExtValue.Text) & "' where SECURITY_TYPE='" & cmbSecType.SelectedValue & "'", con)
            Else
                cmdValue = New SqlCommand("insert into PARA_EXT_VALUE(SECURITY_TYPE,EXT_VALUE_PERC) values ('" & cmbSecType.SelectedValue & "','" & Trim(txtExtValue.Text) & "')", con)
            End If
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            If cmdValue.ExecuteNonQuery Then
                msgbox("Value added successfully")
                clearAll()
                getValues()
            Else
                msgbox("Error adding value")
            End If
            con.Close()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub clearAll()
        Try
            cmbSecType.ClearSelection()
            txtExtValue.Text = ""
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
End Class
