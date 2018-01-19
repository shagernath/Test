Imports System.Data
Imports System.Data.SqlClient
Imports DateFormat
Imports CreditManager
Imports ErrorLogging

Partial Class Roles
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

    Public Sub msgbox(ByVal strMessage As String)

        'finishes server processing, returns to client.
        Dim strScript As String = "<script language=JavaScript>"
        strScript += "window.alert(""" & strMessage & """);"
        strScript += "</script>"
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = strScript
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub btn_AddRole_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_AddRole.Click
        Try
            'reject blank role name
            If Trim(txt_Role.Text) = "" Or IsDBNull(txt_Role.Text) Then
                notify("Enter name for new role", "error")
                txt_Role.Focus()
                Exit Sub
            End If
            'check if new role is unique(no other role with same name)
            If isUniqueRole(txt_Role.Text) Then
                Dim sql As String = ""
                Dim userId As String
                userId = Session("UserID")

                Dim addCommand = "insert into MASTER_ROLES(RoleName,USER_STATUS, USER_CREATED_DATE, USER_CREATED_BY, USER_MODIFIED_BY, USER_MODIFIED_DATE,DASHBOARD) values(''" & BankString.removeSpecialCharacter(txt_Role.Text.Trim()) & "'',''" & 1 & "'',GETDATE(),''" & userId & "'',''" & userId & "'',''" & getSaveDateTime(Date.Now) & "'',''" & rdbDashboardView.SelectedValue & "'')"

                cmd = New SqlCommand("insert into TEMP_ROLES(ACTION, RoleName, USER_STATUS,USER_CREATED_DATE,USER_CREATED_BY,USER_MODIFIED_BY,COMMAND,UPDATED,DASHBOARD) values ('INSERT','" & BankString.removeSpecialCharacter(txt_Role.Text.Trim) & "','1',GETDATE(),'" & Session("UserID") & "','" & Session("UserID") & "','" & addCommand & "',0,'" & rdbDashboardView.SelectedValue & "')", con)
                'msgbox(cmd.CommandText)
                con.Open()
                Dim x As Integer = cmd.ExecuteNonQuery()
                If (x > 0) Then
                    notify("Role successfully added. Authorization pending", "success")
                    txt_Role.Text = ""
                    getAddedRoles()
                End If
            Else
                notify("The role you entered has already been created", "error")
                txt_Role.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            WriteLogFile(Session("UserId"), Request.Url.ToString & " --- btn_AddRole_Click()", ex.ToString)
        Finally
            con.Close()
        End Try
    End Sub

    Protected Sub getAddedRoles()
        Try
            cmd = New SqlCommand("select * from MASTER_ROLES order by roleid", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "MASTER_ROLES")
            If ds.Tables(0).Rows.Count > 0 Then
                grdAddedRoles.DataSource = ds.Tables(0)
            Else
                grdAddedRoles.DataSource = Nothing
            End If
            grdAddedRoles.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub grdAddedRoles_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdAddedRoles.PageIndexChanging
        grdAddedRoles.PageIndex = e.NewPageIndex
        getAddedRoles()
    End Sub

    Protected Sub grdAddedRoles_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdAddedRoles.RowCancelingEdit
        grdAddedRoles.EditIndex = -1
        getAddedRoles()
    End Sub

    Protected Sub grdAddedRoles_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdAddedRoles.RowDeleting
        'usersEditID = DirectCast(grdAddedUsers.Rows(e.RowIndex).FindControl("grdUsers_txtUsername"), TextBox).Text
        Dim userStatus = DirectCast(grdAddedRoles.Rows(e.RowIndex).FindControl("chkStatus"), CheckBox).Checked
        Dim roleName = DirectCast(grdAddedRoles.Rows(e.RowIndex).FindControl("txtRoleName"), TextBox).Text
        Dim dashboard = DirectCast(grdAddedRoles.Rows(e.RowIndex).FindControl("txtDashboardView"), TextBox).Text
        'Dim branch = DirectCast(grdAddedUsers.Rows(e.RowIndex).FindControl("grdUsers_txtBranch"), TextBox).Text

        'Dim delCommand = "DELETE from MASTER_USERS where USER_LOGIN=''" & usersEditID & "''"

        rolesEditID = DirectCast(grdAddedRoles.Rows(e.RowIndex).FindControl("txtRoleIDEdit"), TextBox).Text
        'cmd = New SqlCommand("delete from MASTER_ROLES where RoleID='" & rolesEditID & "'", con)
        Dim delCommand = "delete from MASTER_ROLES where RoleID=''" & rolesEditID & "''"
        'send to TEMP_ROLES table for authorization
        cmd = New SqlCommand("insert into TEMP_ROLES (ACTION,OLD_RoleID,RoleID,OLD_RoleName,RoleName,USER_MODIFIED_BY,USER_MODIFIED_DATE,COMMAND,UPDATED,OLD_USER_STATUS,USER_STATUS,OLD_DASHBOARD,DASHBOARD) values('DELETE','" & rolesEditID & "','" & rolesEditID & "','" & BankString.removeSpecialCharacter(roleName) & "','" & BankString.removeSpecialCharacter(roleName) & "','" & Session("UserID") & "',GETDATE(),'" & delCommand & "'," & 0 & ",'" & userStatus & "','" & userStatus & "','" & dashboard & "','" & dashboard & "')", con)
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        If cmd.ExecuteNonQuery Then
            notify("Role successfully flagged for deletion. Authorization pending", "success")
        Else
            notify("Error deleting role", "error")
        End If
        con.Close()
        getAddedRoles()
    End Sub

    Protected Sub grdAddedRoles_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdAddedRoles.RowEditing
        rolesEditID = DirectCast(grdAddedRoles.Rows(e.NewEditIndex).FindControl("txtRoleIDEdit"), TextBox).Text
        Dim dView = DirectCast(grdAddedRoles.Rows(e.NewEditIndex).FindControl("txtDashboardView"), TextBox).Text
        grdAddedRoles.EditIndex = e.NewEditIndex
        'Dim cmbDashboard = DirectCast(grdAddedRoles.Rows(e.NewEditIndex).FindControl("cmbDashboardViewEdit"), DropDownList)
        'cmbDashboard.SelectedValue = dView
        getAddedRoles()
    End Sub

    Protected Sub grdAddedRoles_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdAddedRoles.RowUpdating
        If Trim(rolesEditID) = "" Or IsDBNull(rolesEditID) Then
            notify("No role selected for update", "error")
            Exit Sub
        End If
        Dim RoleName As String = DirectCast(grdAddedRoles.Rows(e.RowIndex).FindControl("txtRoleNameEdit"), TextBox).Text
        Dim status = DirectCast(grdAddedRoles.Rows(e.RowIndex).FindControl("chkStatusEdit"), CheckBox).Checked
        Dim dashboard = DirectCast(grdAddedRoles.Rows(e.RowIndex).FindControl("cmbDashboardViewEdit"), DropDownList).SelectedValue

        Dim oldUserStatus, oldRoleName, oldDashboard As String
        oldUserStatus = ""
        oldRoleName = ""
        oldDashboard = ""

        cmd = New SqlCommand("select * from MASTER_ROLES where RoleID='" & rolesEditID & "'", con)
        Dim ds1 As New DataSet
        adp = New SqlDataAdapter(cmd)
        adp.Fill(ds1, "MASTER_ROLES")
        If ds1.Tables(0).Rows.Count > 0 Then
            oldRoleName = ds1.Tables(0).Rows(0).Item("RoleName").ToString
            oldUserStatus = ds1.Tables(0).Rows(0).Item("USER_STATUS").ToString
            oldDashboard = ds1.Tables(0).Rows(0).Item("DASHBOARD").ToString
        End If

        'cmd = New SqlCommand("update MASTER_ROLES set RoleName='" & RoleName & "', USER_STATUS='" & status & "' where RoleID='" & rolesEditID & "'", con)

        'Dim updateCmd = "update MASTER_ROLES set RoleName=''" & RoleName & "'', USER_STATUS=''" & status & "'' where RoleID=''" & rolesEditID & "''"
        Dim updateCmd = "update MASTER_ROLES set RoleName='" & BankString.removeSpecialCharacter(RoleName) & "', USER_STATUS='" & status & "', DASHBOARD='" & dashboard & "' where RoleID='" & rolesEditID & "'"
        'cmd = New SqlCommand("insert into TEMP_ROLES (ACTION,OLD_RoleID,RoleID,OLD_RoleName,RoleName,OLD_USER_STATUS,USER_STATUS,USER_MODIFIED_BY,USER_MODIFIED_DATE,COMMAND,UPDATED) values ('UPDATE','" & rolesEditID & "','" & rolesEditID & "','" & oldRoleName & "','" & RoleName & "','" & oldUserStatus & "','" & status & "','" & Session("UserID") & "','" & Date.Now & "','" & updateCmd & "',0)", con)
        'msgbox(cmd.CommandText)

        cmd = New SqlCommand("InsertTempRoleDetails", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@ACTION", "UPDATE")
        cmd.Parameters.AddWithValue("@OLDRoleID", rolesEditID)
        cmd.Parameters.AddWithValue("@RoleID", rolesEditID)
        cmd.Parameters.AddWithValue("@OLD_RoleName", oldRoleName)
        cmd.Parameters.AddWithValue("@RoleName", RoleName)
        cmd.Parameters.AddWithValue("@OLD_DASHBOARD", oldDashboard)
        cmd.Parameters.AddWithValue("@DASHBOARD", dashboard)
        cmd.Parameters.AddWithValue("@OLD_USER_STATUS", oldUserStatus)
        cmd.Parameters.AddWithValue("@USER_STATUS", status)
        cmd.Parameters.AddWithValue("@USER_MODIFIED_BY", Session("userId"))
        'cmd.Parameters.AddWithValue("@USER_MODIFIED_DATE", Now.Date)
        cmd.Parameters.AddWithValue("@COMMAND", updateCmd)
        cmd.Parameters.AddWithValue("@UPDATED", 0)

        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        If cmd.ExecuteNonQuery Then
            notify("Role successfully flagged for update. Authorization pending", "success")
        Else
            notify("Error updating role", "error")
        End If
        con.Close()
        grdAddedRoles.EditIndex = -1
        getAddedRoles()
    End Sub

    Protected Function isUniqueRole(ByVal roleName As String) As Boolean
        cmd = New SqlCommand("select RoleID from MASTER_ROLES where RoleName='" & roleName & "'", con)
        Dim ds As New DataSet
        adp = New SqlDataAdapter(cmd)
        adp.Fill(ds, "MASTER_ROLES")
        If ds.Tables(0).Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.MaintainScrollPositionOnPostBack = True
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Dim surl As String = HttpContext.Current.Request.Url.AbsoluteUri
            surl = Mid(surl, surl.LastIndexOf("/") + 2)
            If Not IsPostBack Then
                Dim dd_Module As DataTable
                dd_Module = Objclsdb.UserHasPermissionForModule(Session("Role").ToString().Trim(), surl)
                If (dd_Module Is Nothing) Or (dd_Module.Rows.Count <= 0) Then
                    Response.Redirect(urlPermission)
                Else
                    getAddedRoles()
                End If
            End If
        Catch ex As Exception
            WriteLogFile(Session("UserId"), Request.Url.ToString & " --- Page_Load()", ex.ToString)
        End Try
    End Sub

    Private Sub grdAddedRoles_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdAddedRoles.RowDataBound
        Try
            If (e.Row.RowType = DataControlRowType.DataRow And grdAddedRoles.EditIndex = e.Row.RowIndex) Then
                Dim cmbDash = DirectCast(e.Row.FindControl("cmbDashboardViewEdit"), DropDownList)
                Try
                    cmbDash.SelectedValue = DirectCast(e.Row.FindControl("txtDashboardViewEdit"), TextBox).Text
                Catch ex As Exception
                    cmbDash.ClearSelection()
                End Try
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class