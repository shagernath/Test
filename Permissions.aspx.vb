Imports System.Data
Imports System.Data.SqlClient

Partial Class Permissions
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
    Function GetSignatory() As DataSet
        Try
            'Dim cmd As SqlCommand = New SqlCommand("SELECT TOP 5 firstname,lastname,hiredate FROM EMPLOYEES", New SqlConnection("Server=localhost;Database=Northwind;Trusted_Connection=True;"))
            Dim con As New SqlConnection()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim cmd As New SqlCommand()
            cmd.CommandText = "Select * From MASTER_ROLES where USER_STATUS=1"
            cmd.CommandType = CommandType.Text
            cmd.Connection = con
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter(cmd.CommandText, con)
            cmd.Connection.Open()
            da.Fill(ds, "IDList")
            Return ds
        Catch ex As Exception
            Return Nothing
            'lblStatus.Text = ex.Message
            'MsgBox(ex.Message)
        End Try
    End Function

    Sub LoadRole(ByVal CBox As DropDownList)
        Try
            Dim ds As DataSet = GetSignatory()
            CBox.DataSource = ds.Tables(0)
            CBox.DataTextField = "RoleName"
            CBox.DataValueField = "RoleID"
            CBox.DataBind()
            'objclsDB.FillComboBoxField(ds, CBox, 1)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_SaveRole_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_SaveRole.Click
        updateRolePermissions()
    End Sub

    Protected Sub clearMessages()
        Repeater1.DataSource = Nothing
        Repeater1.DataBind()
    End Sub

    Protected Sub cmbModuleCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbModuleCategory.SelectedIndexChanged
        Try
            getModulesByCategory(Trim(cmbModuleCategory.SelectedValue))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddl_Role_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Role.SelectedIndexChanged
        'GetModuleDetails()
        getModulesByCategory(Trim(cmbModuleCategory.SelectedValue))
    End Sub

    Protected Sub getModulesByCategory(Optional ByVal category As String = "")
        Try
            If category = "" Then
                cmd = New SqlCommand("select * from MASTER_MODULES order by ModuleId", con)
            Else
                cmd = New SqlCommand("select * from MASTER_MODULES where MODULE_CATEGORY='" & category & "' order by ModuleId", con)
            End If

            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "MODULES")
            If ds.Tables(0).Rows.Count > 0 Then
                gv_ModuleDetails.DataSource = ds
            Else
                gv_ModuleDetails.DataSource = Nothing
            End If
            gv_ModuleDetails.DataBind()

            'remove update messages
            clearMessages()
        Catch ex As Exception

        End Try
    End Sub

    Protected Function getRolePermissions(ByVal Role As String, ByVal Permission As String) As Boolean
        Try
            'msgbox(Role & " : " & Permission)
            cmd = New SqlCommand("select ID from MASTER_PERMISSIONS where RoleID='" & Role & "' and ModuleName='" & Permission & "'", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            If cmd.ExecuteScalar Then
                Return True
            Else
                Return False
            End If
            con.Close()
        Catch ex As Exception
            CreditManager.notify(ex.Message, "error")
        End Try
    End Function

    Protected Sub gv_ModuleDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv_ModuleDetails.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim chkView As CheckBox
            chkView = DirectCast(e.Row.FindControl("chk"), CheckBox)
            Dim modName = DirectCast(e.Row.FindControl("lblModuleName"), Label).Text
            Dim roleName = ddl_Role.Text
            chkView.Checked = getRolePermissions(roleName, modName)
        End If
    End Sub

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
            MsgBox(ex.Message)
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
                Dim dd_Module As DataTable
                dd_Module = Objclsdb.UserHasPermissionForModule(Session("Role").ToString().Trim(), surl)
                If (dd_Module Is Nothing) Or (dd_Module.Rows.Count <= 0) Then
                    Response.Redirect(urlPermission)
                    'ClientScript.RegisterStartupScript(GetType(Page), "anil", "<script>alert('Permission denied')</script>")
                Else
                    LoadRole(ddl_Role)
                    GetModuleDetails()
                    loadModuleCategories(cmbModuleCategory)
                    getModulesByCategory(Trim(cmbModuleCategory.SelectedValue))
                End If

            End If
        Catch ex As Exception
            CreditManager.notify(ex.Message, "error")
        End Try
    End Sub
    Protected Sub updateRolePermissions()
        Try
            Dim messages As New ArrayList
            For Each row As GridViewRow In gv_ModuleDetails.Rows
                Dim chkView As CheckBox = DirectCast(row.FindControl("chk"), CheckBox)
                Dim modName = DirectCast(row.FindControl("lblModuleName"), Label).Text
                Dim roleName = ddl_Role.Text
                cmd = New SqlCommand("select * from MASTER_PERMISSIONS where RoleID='" & roleName & "' and ModuleName='" & modName & "'", con)
                Dim ds As New DataSet
                adp = New SqlDataAdapter(cmd)
                adp.Fill(ds, "MASTER_PERMISSIONS")
                If ds.Tables(0).Rows.Count > 0 Then
                    'permission already granted
                    cmd = New SqlCommand("delete from MASTER_PERMISSIONS where RoleID='" & roleName & "' and ModuleName='" & modName & "'", con)
                    If chkView.Checked Then
                        'do nothing, to avoid duplicate
                    Else
                        If con.State = ConnectionState.Open Then
                            con.Close()
                        End If
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                        messages.Add(modName & " permission has been taken from role " & ddl_Role.SelectedItem.Text)
                    End If
                Else
                    'permission not yet granted
                    cmd = New SqlCommand("insert into MASTER_PERMISSIONS()", con)

                    Dim sql As String = ""
                    Dim userId As String
                    Dim moduleId As Integer
                    Dim moduleName As String
                    Dim RoleIdId As Integer
                    Dim x As Integer
                    'Dim URL_NAME As String
                    userId = Session("UserID")
                    RoleIdId = Convert.ToInt32(ddl_Role.SelectedValue.ToString().Trim())
                    roleName = ddl_Role.SelectedItem.Text.Trim()

                    If chkView.Checked = True Then
                        Dim lblModule As Label = CType(row.FindControl("lblModuleId"), Label)
                        Dim lblModuleName As Label = CType(row.FindControl("lblModuleName"), Label)
                        moduleId = Convert.ToInt32(lblModule.Text)
                        moduleName = lblModuleName.Text
                        Dim URL_NAME As Label = CType(row.FindControl("lblURLName"), Label)
                        Dim sUrl As String = URL_NAME.Text
                        cmd = New SqlCommand("InsertPermissionDetails", con)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@RoleID", RoleIdId)
                        cmd.Parameters.AddWithValue("@RoleName", BankString.removeSpecialCharacter(roleName))
                        cmd.Parameters.AddWithValue("@ModuleID", moduleId)
                        cmd.Parameters.AddWithValue("@ModuleName", BankString.removeSpecialCharacter(moduleName))
                        cmd.Parameters.AddWithValue("@ALW_View", "Y")
                        cmd.Parameters.AddWithValue("@CREATED_BY", userId)
                        cmd.Parameters.AddWithValue("@MODIFIED_BY", userId)
                        cmd.Parameters.AddWithValue("@URL_NAME", sUrl)

                        con.Open()
                        x = cmd.ExecuteNonQuery()
                        con.Close()
                        If (x > 0) Then
                            'ClientScript.RegisterStartupScript(GetType(Page), "anil", "<script>alert('Permission Added Successfully.')</script>")
                            'Response.Redirect("SignPermission.aspx")
                            messages.Add(modName & " permission granted to role " & roleName)
                        End If
                    Else
                        'nothing to be done
                    End If

                End If
            Next
            Repeater1.DataSource = messages
            Repeater1.DataBind()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub GetModuleDetails()
        Try
            adp = New SqlDataAdapter("GetModuleDetails", con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.Fill(ds)
            gv_ModuleDetails.DataSource = ds
            gv_ModuleDetails.DataBind()

            'remove update messages
            clearMessages()
        Catch ex As Exception

        End Try
    End Sub
End Class