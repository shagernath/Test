Imports System.Data
Imports System.Data.SqlClient

Partial Class Modules
    Inherits System.Web.UI.Page
    'for editing roles grid and users grid
    Public Shared rolesEditID, usersEditID, moduleEditID As String

    Dim adp As New SqlDataAdapter
    Dim cmd As SqlCommand
    Dim con As New SqlConnection
    Dim connection As String
    Dim ds As New DataSet()
    Dim Objclsdb As New CreditManager
    Dim urlPermission As String = "PermissionDenied.aspx"
    Public Sub FillListView(ByRef lvList As ListBox, ByRef myData As SqlDataReader)
        Dim itmListItem As ListBox
        Dim strValue As String
        Do While myData.Read
            itmListItem = New ListBox()
            strValue = IIf(myData.IsDBNull(0), "", myData.GetValue(0))
            itmListItem.Text = strValue
            For shtCntr = 1 To myData.FieldCount() - 1
                If myData.IsDBNull(shtCntr) Then
                    itmListItem.Items.Add("")
                Else
                    itmListItem.Items.Add(myData.GetValue(shtCntr))
                End If
            Next shtCntr
        Loop
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

    Protected Sub btnSaveModule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveModule.Click
        Try
            Dim moduleName = txtModuleName.Text
            Dim url = txtURL.Text
            If isEmptyString(moduleName) Then
                msgbox("Enter the module name")
                txtModuleName.Focus()
                Exit Sub
            End If
            If isEmptyString(url) Then
                msgbox("Enter module URL")
                txtURL.Focus()
            End If
            If isUniqueModuleName(moduleName) Then
                ' If isUniqueModuleURL(url) Then
                cmd = New SqlCommand("insert into MASTER_MODULES ([ModuleName],[USER_CREATED_DATE],[USER_CREATED_BY],[USER_MODIFIED_BY],[USER_MODIFIED_DATE],[URL_NAME],[MODULE_CATEGORY]) values('" & BankString.removeSpecialCharacter(moduleName) & "',GetDate(),'" & Session("UserID") & "','','','" & url & "','" & BankString.removeSpecialCharacter(cmbModuleCategory.SelectedValue) & "')", con)
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery Then
                    msgbox("Module successfully created")
                    clearAll()
                    'at the end reload grid
                    loadModules()
                Else
                    msgbox("Error adding new module")
                End If
                'Else
                '    msgbox("The URL you entered already exists.")
                '    txtURL.Focus()
                'End If
            Else
                msgbox("The name you entered already exists.")
                txtModuleName.Focus()
            End If

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub clearAll()
        Try
            txtModuleName.Text = ""
            txtURL.Text = ""
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub grdModules_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdModules.PageIndexChanging
        grdModules.PageIndex = e.NewPageIndex
        loadModules()
    End Sub

    Protected Sub grdModules_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdModules.RowCancelingEdit
        grdModules.EditIndex = -1
        loadModules()
    End Sub

    Protected Sub grdModules_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdModules.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow And grdModules.EditIndex = e.Row.RowIndex) Then
            'msgbox(DirectCast(e.Row.FindControl("grdUsers_txtUserType"), TextBox).Text)
            Dim cmbCategory = DirectCast(e.Row.FindControl("cmbModuleCategoryEdit"), DropDownList)
            'cmd = New SqlCommand("select * from MASTER_ROLES", con)
            'Dim ds1 As New DataSet
            'adp = New SqlDataAdapter(cmd)
            'adp.Fill(ds1, "MASTER_ROLES")
            'If ds1.Tables(0).Rows.Count > 0 Then
            '    cmbRole.DataSource = ds1.Tables(0)
            '    cmbRole.DataTextField = "RoleName"
            '    cmbRole.DataValueField = "RoleID"
            'Else
            '    cmbRole.DataSource = Nothing
            'End If
            'cmbRole.DataBind()
            loadModuleCategories(cmbCategory)
            Try
                cmbCategory.SelectedValue = DirectCast(e.Row.FindControl("txtModuleCategory0Edit"), TextBox).Text
            Catch ex As Exception
                cmbCategory.SelectedItem.Text = ""
            End Try
        End If
    End Sub

    Protected Sub grdModules_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdModules.RowDeleting
        moduleEditID = DirectCast(grdModules.Rows(e.RowIndex).FindControl("txtModuleId0"), TextBox).Text
        cmd = New SqlCommand("delete from MASTER_MODULES where ModuleID='" & moduleEditID & "'", con)
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        If cmd.ExecuteNonQuery Then
            msgbox("Module successfully deleted")
        Else
            msgbox("Error deleting module")
        End If
        con.Close()
        loadModules()
    End Sub

    Protected Sub grdModules_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdModules.RowEditing
        moduleEditID = DirectCast(grdModules.Rows(e.NewEditIndex).FindControl("txtModuleId0"), TextBox).Text
        grdModules.EditIndex = e.NewEditIndex
        loadModules()
    End Sub

    Protected Sub grdModules_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdModules.RowUpdating
        If Trim(moduleEditID) = "" Or IsDBNull(moduleEditID) Then
            msgbox("No module selected for update")
            Exit Sub
        End If
        Dim modName As String = DirectCast(grdModules.Rows(e.RowIndex).FindControl("txtModuleName0Edit"), TextBox).Text
        Dim modURL As String = DirectCast(grdModules.Rows(e.RowIndex).FindControl("txtURLName0Edit"), TextBox).Text
        Dim modCat As String = DirectCast(grdModules.Rows(e.RowIndex).FindControl("cmbModuleCategoryEdit"), DropDownList).SelectedValue
        cmd = New SqlCommand("update MASTER_MODULES set ModuleName='" & modName & "',URL_NAME='" & modURL & "',[MODULE_CATEGORY]='" & BankString.removeSpecialCharacter(modCat) & "' where ModuleID='" & moduleEditID & "'", con)
        Dim cmdPerm = New SqlCommand("update MASTER_PERMISSIONS set ModuleName='" & modName & "',URL_NAME='" & modURL & "' where ModuleID='" & moduleEditID & "'", con)
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        If cmd.ExecuteNonQuery Then
            cmdPerm.ExecuteNonQuery()
            msgbox("Module successfully updated")
        Else
            msgbox("Error updating module")
        End If
        con.Close()
        grdModules.EditIndex = -1
        loadModules()
    End Sub

    Protected Sub grdModules_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdModules.SelectedIndexChanged

    End Sub

    Protected Function isEmptyString(ByVal str As String) As Boolean
        If IsDBNull(str) Or Trim(str) = "" Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Function isUniqueModuleName(ByVal moduleName As String) As Boolean
        cmd = New SqlCommand("select * from MASTER_MODULES where ModuleName='" & moduleName & "'", con)
        ds.Clear()
        adp = New SqlDataAdapter(cmd)
        adp.Fill(ds, "MASTER_MODULES")
        If ds.Tables(0).Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Protected Function isUniqueModuleURL(ByVal moduleURL As String) As Boolean
        cmd = New SqlCommand("select * from MASTER_MODULES where URL_NAME='" & moduleURL & "'", con)
        ds.Clear()
        adp = New SqlDataAdapter(cmd)
        adp.Fill(ds, "MASTER_MODULES")
        If ds.Tables(0).Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub loadModuleCategories(ByVal cmbCategory As DropDownList)
        Try
            Dim cmdSel As SqlCommand
            cmdSel = New SqlCommand("select * from MASTER_MODULE_CATEGORIES", con)
            Dim dsSel As New DataSet
            adp = New SqlDataAdapter(cmdSel)
            adp.Fill(dsSel, "CATEGORY")
            If dsSel.Tables(0).Rows.Count > 0 Then
                cmbCategory.DataSource = dsSel.Tables(0)
                cmbCategory.DataTextField = "CATEGORY"
                cmbCategory.DataValueField = "ID"
                'cmbCategory.DataValueField = "CATEGORY"
            Else
                cmbCategory.DataSource = Nothing
            End If
            cmbCategory.Items.Add("")
            cmbCategory.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.MaintainScrollPositionOnPostBack = True
            Page.Header.Title = "360 Credit Management: User Management"
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Dim surl As String = HttpContext.Current.Request.Url.AbsoluteUri
            surl = Mid(surl, surl.LastIndexOf("/") + 2)
            If Not IsPostBack Then
                loadModuleCategories(cmbModuleCategory)
                Dim dd_Module As DataTable
                dd_Module = Objclsdb.UserHasPermissionForModule(Session("Role").ToString().Trim(), surl)
                If (dd_Module Is Nothing) Or (dd_Module.Rows.Count <= 0) Then
                    Response.Redirect(urlPermission)
                    'ClientScript.RegisterStartupScript(GetType(Page), "anil", "<script>alert('Permission denied')</script>")
                Else
                    loadModules()
                End If

            End If
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Private Sub loadModules()
        Try
            cmd = New SqlCommand("select * from MASTER_MODULES mm join MASTER_MODULE_CATEGORIES mmc on mm.MODULE_CATEGORY=mmc.ID order by ModuleId", con)
            adp = New SqlDataAdapter(cmd)
            Dim ds1 As New DataSet
            ds1.Clear()
            adp.Fill(ds1, "MASTER_MODULES")
            If ds1.Tables(0).Rows.Count > 0 Then
                grdModules.DataSource = ds1.Tables(0)
            Else
                grdModules.DataSource = Nothing
            End If
            grdModules.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
End Class