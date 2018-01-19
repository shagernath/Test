Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports CreditManager
Imports ErrorLogging

Partial Class Banks
    Inherits System.Web.UI.Page
    Public Shared branchEditID As String
    Dim adp As New SqlDataAdapter
    Dim cmd As SqlCommand
    Dim con As New SqlConnection
    Dim connection As String
    Dim urlPermission As String = "PermissionDenied.aspx"
    Protected Sub btnAddBranch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddBranch.Click
        Try
            Dim cmdIns = New SqlCommand("insert into para_bank (BANK,bank_name) values ('" & txtBranchCode.Text & "','" & BankString.removeSpecialCharacter(txtBranchName.Text) & "')", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            If isUniqueCode(txtBranchCode.Text) Then
                If cmdIns.ExecuteNonQuery Then
                    'cmdAudit.ExecuteNonQuery()
                    msgbox("Bank successfully saved")
                    clearAll()
                    getBanks()
                Else
                    msgbox("Error saving bank")
                End If
            Else
                msgbox("A bank with this code already exists")
                txtBranchCode.Focus()
            End If
            con.Close()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub clearAll()
        Try
            txtBranchCode.Text = ""
            txtBranchName.Text = ""
        Catch ex As Exception
            WriteLogFile(Session("UserId"), "Banks.aspx - clearAll()", ex.Message)
        End Try
    End Sub

    Protected Sub getBanks()
        Try
            Using cmd = New SqlCommand("select * from para_bank", con)
                Dim ds As New DataSet
                adp = New SqlDataAdapter(cmd)
                adp.Fill(ds, "BANK")
                If ds.Tables(0).Rows.Count > 0 Then
                    grdBranches.DataSource = ds.Tables(0)
                Else
                    grdBranches.DataSource = Nothing
                End If
                grdBranches.DataBind()
            End Using
        Catch ex As Exception
            WriteLogFile(Session("UserId"), "Banks.aspx - getBanks()", ex.Message)
        End Try
    End Sub

    Protected Sub grdBranches_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdBranches.RowCancelingEdit
        grdBranches.EditIndex = -1
        getBanks()
    End Sub

    Protected Sub grdBranches_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdBranches.RowDeleting
        Try
            branchEditID = DirectCast(grdBranches.Rows(e.RowIndex).FindControl("txtOldBranchCode"), TextBox).Text
            'cmd = New SqlCommand("update BNCH_DETAILS set active=0 where BNCH_CODE='" & branchEditID & "'", con)
            'Dim cmdSelect = New SqlCommand("select * from BANK_DETAILS where BANK_CODE='" & branchEditID & "'", con)
            'Dim dsSelect As New DataSet
            'Dim adpSel = New SqlDataAdapter(cmd)
            'adpSel.Fill(dsSelect, "BRANCH")

            'Dim cmdAudit As SqlCommand
            'cmdAudit = New SqlCommand("insert into TEMP_BNCH_DETAILS ([ACTION],[PERFORMED_BY],[PERFORMED_DATE],[BNCH_CODE],[BNCH_NAME],[BNCH_ADDRESS],[BNCH_PHONENO],[BNCH_FAXNO]) values ('DELETE','" & Session("UserID") & "',getDate(),'" & dsSelect.Tables(0).Rows(0).Item("BNCH_CODE") & "','" & BankString.removeSpecialCharacter(dsSelect.Tables(0).Rows(0).Item("BNCH_NAME")) & "','" & BankString.removeSpecialCharacter(dsSelect.Tables(0).Rows(0).Item("BNCH_ADDRESS")) & "','" & dsSelect.Tables(0).Rows(0).Item("BNCH_PHONENO") & "','')", con)
            cmd = New SqlCommand("delete from para_bank where BANK='" & branchEditID & "'", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            If cmd.ExecuteNonQuery Then
                'cmdAudit.ExecuteNonQuery()
                notify("Successfully deleted", "success")
            Else
                notify("Error deleting", "error")
            End If
            con.Close()
            getBanks()
        Catch ex As Exception
            WriteLogFile(Session("UserId"), "Banks.aspx - grdBranches_RowDeleting()", ex.Message)
        End Try
    End Sub

    Protected Sub grdBranches_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdBranches.RowEditing
        Try
            branchEditID = DirectCast(grdBranches.Rows(e.NewEditIndex).FindControl("txtOldBranchCode"), TextBox).Text
            grdBranches.EditIndex = e.NewEditIndex
            getBanks()
        Catch ex As Exception
            WriteLogFile(Session("UserId"), "Banks.aspx - grdBranches_RowEditing()", ex.Message)
        End Try
    End Sub

    Protected Sub grdBranches_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdBranches.RowUpdating
        Try
            If Trim(branchEditID) = "" Or IsDBNull(branchEditID) Then
                notify("No record selected for update", "error")
                Exit Sub
            End If
            Dim bnchCode As String = DirectCast(grdBranches.Rows(e.RowIndex).FindControl("txtGrdBranchCode"), TextBox).Text

            Dim bnchName As String = DirectCast(grdBranches.Rows(e.RowIndex).FindControl("txtBranchNameEdit"), TextBox).Text
            'Dim bnchAddress As String = DirectCast(grdBranches.Rows(e.RowIndex).FindControl("txtGrdBranchAddressEdit"), TextBox).Text

            'Dim bnchPhone As String = DirectCast(grdBranches.Rows(e.RowIndex).FindControl("txtBranchPhoneEdit"), TextBox).Text

            Dim oldBnchCode, oldBnchName, oldBnchAddress, oldBnchPhone, oldBnchFax As String
            oldBnchCode = ""
            oldBnchName = ""
            oldBnchAddress = ""
            oldBnchPhone = ""
            oldBnchFax = ""

            cmd = New SqlCommand("select * from para_bank where BANK='" & branchEditID & "'", con)
            Dim ds1 As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds1, "DETAILS")
            If ds1.Tables(0).Rows.Count > 0 Then
                oldBnchCode = ds1.Tables(0).Rows(0).Item("BANK").ToString
                oldBnchName = ds1.Tables(0).Rows(0).Item("BANK_NAME").ToString
                'oldBnchAddress = ds1.Tables(0).Rows(0).Item("BNCH_ADDRESS").ToString
                'oldBnchPhone = ds1.Tables(0).Rows(0).Item("BNCH_PHONENO").ToString
                'oldBnchFax = ds1.Tables(0).Rows(0).Item("BNCH_FAXNO").ToString
            End If

            cmd = New SqlCommand("update para_bank set BANK='" & bnchCode & "', BANK_NAME='" & BankString.removeSpecialCharacter(bnchName) & "' where BANK='" & branchEditID & "'", con)
            'Dim cmdAudit As SqlCommand
            'cmdAudit = New SqlCommand("insert into TEMP_BNCH_DETAILS ([ACTION],[PERFORMED_BY],[PERFORMED_DATE],[OLD_BNCH_CODE],[BNCH_CODE],[OLD_BNCH_NAME],[BNCH_NAME],[OLD_BNCH_ADDRESS],[BNCH_ADDRESS],[OLD_BNCH_PHONENO],[BNCH_PHONENO],[OLD_BNCH_FAXNO],[BNCH_FAXNO]) values ('UPDATE','" & Session("UserID") & "',getDate(),'" & oldBnchCode & "','" & bnchCode & "','" & oldBnchName & "','" & bnchName & "','" & oldBnchAddress & "','" & bnchAddress & "','" & oldBnchPhone & "','" & bnchPhone & "','" & oldBnchFax & "','')", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            If cmd.ExecuteNonQuery Then
                'cmdAudit.ExecuteNonQuery()
                notify("Successfully updated", "success")
            Else
                notify("Error updating value", "error")
            End If
            con.Close()
            grdBranches.EditIndex = -1
            getBanks()

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub grdBranches_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdBranches.SelectedIndexChanged

    End Sub

    Protected Function isUniqueCode(ByVal bnchCode As String) As Boolean
        Try
            cmd = New SqlCommand("select * from para_bank where BANK='" & bnchCode & "'", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "BANKS")
            If ds.Tables(0).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            WriteLogFile(Session("UserId"), "Banks.aspx - isUniqueCode()", ex.Message)
        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Page.MaintainScrollPositionOnPostBack = True
            If Not IsPostBack Then
                getBanks()
            End If
        Catch ex As Exception
            WriteLogFile(Session("UserId"), "Banks.aspx - Page_Load()", ex.Message)
        End Try
    End Sub
End Class