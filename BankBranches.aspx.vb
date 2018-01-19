Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web
Imports System.IO

Partial Class Bank_Branches
    Inherits System.Web.UI.Page
    Public Shared branchEditID, branchID As String
    Dim adp As New SqlDataAdapter
    Dim cmd As SqlCommand
    Dim con As New SqlConnection
    Dim connection As String
    Dim urlPermission As String = "PermissionDenied.aspx"
    Public Sub msgbox(ByVal strMessage As String)

        'finishes server processing, returns to client.
        Dim strScript As String = "<script language=JavaScript>"
        strScript += "window.alert(""" & strMessage & """);"
        strScript += "</script>"
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = strScript
        Page.Controls.Add(lbl)
    End Sub
    Protected Sub btnAddBranch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddBranch.Click
        Try
            Dim cmdIMPIns = New SqlCommand("insert into para_branch ([bank],[branch],[branch_name]) values ('" & lblBankCode.Text & "','" & txtBranchCode.Text & "','" & BankString.removeSpecialCharacter(txtBranchName.Text) & "')", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            If isUniqueCode(txtBranchCode.Text) Then
                cmdIMPIns.ExecuteNonQuery()
                msgbox("Branch successfully saved")
                clearAll()
                getBranches()
            Else
                msgbox("A branch with this code already exists")
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
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Sub cmbBrBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBrBank.SelectedIndexChanged
        Try
            lblBankCode.Text = cmbBrBank.SelectedValue
            getBranches()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub getBranches()
        Try
            cmd = New SqlCommand("select [bank],[branch],[branch_name] from para_branch where bank='" & lblBankCode.Text & "'", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "Category")
            If ds.Tables(0).Rows.Count > 0 Then
                grdBranches.DataSource = ds.Tables(0)
            Else
                grdBranches.DataSource = Nothing
            End If
            grdBranches.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Sub grdBranches_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdBranches.RowCancelingEdit
        grdBranches.EditIndex = -1
        getBranches()
    End Sub
    Protected Sub grdBranches_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdBranches.RowDeleting
        Try
            branchID = DirectCast(grdBranches.Rows(e.RowIndex).FindControl("lblBranchCode"), Label).Text
            cmd = New SqlCommand(String.Format("delete from para_branch where branch='" & branchID & "' and bank='" & lblBankCode.Text & "'"), con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            If cmd.ExecuteNonQuery Then
                msgbox("Successfully deleted")
            End If
            con.Close()
            getBranches()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Sub grdBranches_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdBranches.RowEditing
        Try
            branchID = DirectCast(grdBranches.Rows(e.NewEditIndex).FindControl("lblBranchCode"), Label).Text
            grdBranches.EditIndex = e.NewEditIndex
            getBranches()
            'msgbox(branchID)
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Sub grdBranches_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdBranches.RowUpdating
        Try
            'If Trim(branchID) = "" Or IsDBNull(branchID) Then
            '    msgbox("No record selected for update")
            '    Exit Sub
            'End If
            msgbox(branchID)
            Dim bnchCode As String = DirectCast(grdBranches.Rows(e.RowIndex).FindControl("txtBranchCodeEdit"), TextBox).Text
            Dim bnchName As String = DirectCast(grdBranches.Rows(e.RowIndex).FindControl("txtBranchNameEdit"), TextBox).Text
            cmd = New SqlCommand("update para_branch set branch='" & bnchCode & "', branch_name='" & BankString.removeSpecialCharacter(bnchName) & "' where branch='" & branchID & "' and bank='" & lblBankCode.Text & "'", con)
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
            grdBranches.EditIndex = -1
            getBranches()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Function isUniqueCode(ByVal bnchCode As String) As Boolean
        Try
            'cmd = New SqlCommand("select * from BNCH_DETAILS where BNCH_CODE='" & bnchCode & "' and BNK_NAME='" & cmbBrBank.SelectedItem.Text & "'", con)
            cmd = New SqlCommand("select * from para_branch where branch='" & bnchCode & "'", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "BRANCHES")
            If ds.Tables(0).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Function
    Protected Sub loadBanks(cmbBank As DropDownList)
        Try
            cmd = New SqlCommand("select * from para_bank", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "BANK")
            CreditManager.loadCombo(ds.Tables(0), cmbBank, "BANK_NAME", "BANK")
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Page.MaintainScrollPositionOnPostBack = True
            If Not IsPostBack Then
                loadBanks(cmbBrBank)
                getBranches()
            End If
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
End Class