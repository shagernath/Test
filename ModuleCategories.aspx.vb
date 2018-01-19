Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web

Partial Class ModuleCategories
    Inherits System.Web.UI.Page
    Dim cmd As SqlCommand
    Dim con As New SqlConnection
    Dim adp As New SqlDataAdapter
    Dim connection As String
    Dim urlPermission As String = "PermissionDenied.aspx"
    Public Shared catEditID As String

    Protected Sub btnAddCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCategory.Click
        Try
            Dim category = Trim(txtCategoryName.Text)
            If category = "" Or IsDBNull(category) Then
                msgbox("Enter a value for category")
                txtCategoryName.Focus()
                Exit Sub
            End If
            If Not isUniqueCategory(category) Then
                msgbox("This category has already been added")
                txtCategoryName.Focus()
                Exit Sub
            End If
            cmd = New SqlCommand("insert into MASTER_MODULE_CATEGORIES (CATEGORY) values ('" & category & "')", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            If cmd.ExecuteNonQuery Then
                SecureBank.recordAction("Insert", "Added module category '" & category & "'")
                msgbox("Category successfully added")
                getCategories()
                txtCategoryName.Text = ""
            End If
            con.Close()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Function isUniqueCategory(ByVal cat As String) As Boolean
        Try
            Dim cmdCat As SqlCommand
            cmdCat = New SqlCommand("select * from MASTER_MODULE_CATEGORIES where CATEGORY='" & cat & "'", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmdCat)
            adp.Fill(ds, "CATEGORIES")
            If ds.Tables(0).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Page.MaintainScrollPositionOnPostBack = True
            If Not IsPostBack Then
                getCategories()
            End If
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub getCategories()
        Try
            cmd = New SqlCommand("select * from MASTER_MODULE_CATEGORIES", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "CATEGORY")
            If ds.Tables(0).Rows.Count > 0 Then
                grdCategories.DataSource = ds.Tables(0)
            Else
                grdCategories.DataSource = Nothing
            End If
            grdCategories.DataBind()
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

    Protected Sub grdCategories_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdCategories.RowCancelingEdit
        grdCategories.EditIndex = -1
        getCategories()
    End Sub

    Protected Sub grdCategories_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdCategories.RowDeleting
        'Try
        catEditID = DirectCast(grdCategories.Rows(e.RowIndex).FindControl("txtCategoryID"), TextBox).Text
        'cmd = New SqlCommand("update BNCH_DETAILS set active=0 where BNCH_CODE='" & branchEditID & "'", con)
        Dim cmdSelect = New SqlCommand("select * from MASTER_MODULE_CATEGORIES where ID='" & catEditID & "'", con)
        Dim dsSelect As New DataSet
        Dim adpSel = New SqlDataAdapter(cmdSelect)
        adpSel.Fill(dsSelect, "CATEGORY")

        'Dim cmdAudit As SqlCommand
        'cmdAudit = New SqlCommand("insert into TEMP_BNCH_DETAILS ([ACTION],[PERFORMED_BY],[PERFORMED_DATE],[BNCH_CODE],[BNCH_NAME],[BNCH_ADDRESS],[BNCH_PHONENO],[BNCH_FAXNO]) values ('DELETE','" & Session("UserID") & "',getDate(),'" & dsSelect.Tables(0).Rows(0).Item("BNCH_CODE") & "','" & BankString.removeSpecialCharacter(dsSelect.Tables(0).Rows(0).Item("BNCH_NAME")) & "','" & BankString.removeSpecialCharacter(dsSelect.Tables(0).Rows(0).Item("BNCH_ADDRESS")) & "','" & dsSelect.Tables(0).Rows(0).Item("BNCH_PHONENO") & "','')", con)
        cmd = New SqlCommand("delete from MASTER_MODULE_CATEGORIES where ID='" & catEditID & "'", con)
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        If cmd.ExecuteNonQuery Then
            'cmdAudit.ExecuteNonQuery()
            SecureBank.recordAction("Delete", "Deleted module category '" & catEditID & ": " & dsSelect.Tables(0).Rows(0).Item("CATEGORY") & "'")
            msgbox("Successfully deleted")
        Else
            msgbox("Error deleting")
        End If
        con.Close()
        getCategories()

        'Catch ex As Exception
        '    msgbox(ex.Message)
        'End Try
    End Sub

    Protected Sub grdCategories_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdCategories.RowEditing
        Try
            catEditID = DirectCast(grdCategories.Rows(e.NewEditIndex).FindControl("txtCategoryID"), TextBox).Text
            grdCategories.EditIndex = e.NewEditIndex
            getCategories()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub grdCategories_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdCategories.RowUpdating
        Try
            If Trim(catEditID) = "" Or IsDBNull(catEditID) Then
                msgbox("No record selected for update")
                Exit Sub
            End If
            Dim categoryName As String = DirectCast(grdCategories.Rows(e.RowIndex).FindControl("txtGrdCategoryName"), TextBox).Text

            Dim oldCatName As String = "" ' DirectCast(grdCategories.Rows(e.RowIndex).FindControl("txtOldCategoryName"), TextBox).Text

            cmd = New SqlCommand("select * from MASTER_MODULE_CATEGORIES where ID='" & catEditID & "'", con)
            Dim ds1 As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds1, "DETAILS")
            If ds1.Tables(0).Rows.Count > 0 Then
                oldCatName = ds1.Tables(0).Rows(0).Item("CATEGORY").ToString
            End If

            cmd = New SqlCommand("update MASTER_MODULE_CATEGORIES set CATEGORY='" & BankString.removeSpecialCharacter(categoryName) & "' where ID='" & catEditID & "'", con)
            Dim cmdCascade = New SqlCommand("update MASTER_MODULES set MODULE_CATEGORY='" & BankString.removeSpecialCharacter(categoryName) & "' where MODULE_CATEGORY='" & oldCatName & "'", con)
            'Dim cmdAudit As SqlCommand
            'cmdAudit = New SqlCommand("insert into MASTER_MODULE_CATEGORIES ([ACTION],[PERFORMED_BY],[PERFORMED_DATE],[OLD_BNCH_CODE],[BNCH_CODE],[OLD_BNCH_NAME],[BNCH_NAME],[OLD_BNCH_ADDRESS],[BNCH_ADDRESS],[OLD_BNCH_PHONENO],[BNCH_PHONENO],[OLD_BNCH_FAXNO],[BNCH_FAXNO]) values ('UPDATE','" & Session("UserID") & "',getDate(),'" & oldBnchCode & "','" & bnchCode & "','" & oldBnchName & "','" & bnchName & "','" & oldBnchAddress & "','" & bnchAddress & "','" & oldBnchPhone & "','" & bnchPhone & "','" & oldBnchFax & "','')", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            If cmd.ExecuteNonQuery Then
                'cmdAudit.ExecuteNonQuery()
                cmdCascade.ExecuteNonQuery()
                SecureBank.recordAction("Update", "Updated module category " & catEditID & " from '" & oldCatName & "' to '" & categoryName & "'")

                msgbox("Successfully updated")
            Else
                msgbox("Error updating value")
            End If
            con.Close()
            grdCategories.EditIndex = -1
            getCategories()

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
End Class
