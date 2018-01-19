Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports CreditManager

Partial Class MenuOrder
    Inherits System.Web.UI.Page
    'for editing roles grid and users grid
    Public Shared rolesEditID, usersEditID, moduleEditID As String

    Dim adp As New SqlDataAdapter
    Dim cmd As SqlCommand
    Dim con As New SqlConnection
    Dim connection As String
    Dim ds As New DataSet()
    Dim Objclsdb As New CreditManager
    Function GetSignatory() As DataSet
        Try
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
        End Try
    End Function

    Sub LoadRole(ByVal CBox As DropDownList)
        Try
            Dim ds As DataSet = GetSignatory()
            CBox.DataSource = ds.Tables(0)
            CBox.DataTextField = "RoleName"
            CBox.DataValueField = "RoleID"
            CBox.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cmbModuleCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbModuleCategory.SelectedIndexChanged
        Try
            getModulesByCategory(Trim(cmbModuleCategory.SelectedValue))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddl_Role_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Role.SelectedIndexChanged
        getModulesByCategory(Trim(cmbModuleCategory.SelectedValue))
    End Sub

    Protected Sub getModulesByCategory(Optional ByVal category As String = "")
        Try
            If category = "" Then
                'cmd = New SqlCommand("select * from MASTER_MODULES order by ModuleId", con)
                cmd = New SqlCommand("Select * from MASTER_PERMISSIONS where RoleId='" & ddl_Role.SelectedValue & "' order by Ordering", con)
            Else
                'cmd = New SqlCommand("select * from MASTER_MODULES where MODULE_CATEGORY='" & category & "' order by ModuleId", con)
                cmd = New SqlCommand("Select mp.* from MASTER_PERMISSIONS mp left join [MASTER_MODULES] mm on mp.[ModuleID]=mm.[ModuleID] where mp.RoleId='" & ddl_Role.SelectedValue & "' and mm.MODULE_CATEGORY='" & category & "' order by Ordering", con)
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
        Catch ex As Exception
            ErrorLogging.WriteLogFile(Session("UserId"), Request.Url.ToString & " --- getModulesByCategory()", ex.ToString)
        End Try
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
                cmbModuleCategory.DataValueField = "ID"
                'cmbCategory.DataValueField = "CATEGORY"
            Else
                cmbCategory.DataSource = Nothing
            End If
            'cmbCategory.Items.Add("")
            cmbCategory.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.MaintainScrollPositionOnPostBack = True
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Dim surl As String = HttpContext.Current.Request.Url.AbsoluteUri
            surl = Mid(surl, surl.LastIndexOf("/") + 2)
            If Not IsPostBack Then
                Dim dd_Module As DataTable
                dd_Module = Objclsdb.UserHasPermissionForModule(Session("Role").ToString().Trim(), surl)
                LoadRole(ddl_Role)
                'GetModuleDetails()
                loadModuleCategories(cmbModuleCategory)
                getModulesByCategory(Trim(cmbModuleCategory.SelectedValue))
                'End If

            End If
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    'Protected Sub btn_SaveRole_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_SaveRole.Click
    '    updateRolePermissions()
    'End Sub
    Protected Sub UpdatePreference(sender As Object, e As EventArgs) Handles btn_SaveRole.Click
        Dim locationIds As Integer() = (From p In Request.Form("lblPermissionId").Split(",")
                                        Select Integer.Parse(p)).ToArray()
        Dim preference As Integer = 1
        For Each locationId As Integer In locationIds
            Me.UpdatePreference(locationId, preference)
            preference += 1
        Next
        notify("New menu order saved", "success")
        getModulesByCategory(Trim(cmbModuleCategory.SelectedValue))
        'Response.Redirect(Request.Url.AbsoluteUri)
    End Sub

    Private Sub GetModuleDetails()
        Try
            'adp = New SqlDataAdapter("GetModuleDetails", con)
            adp = New SqlDataAdapter("Select * from MASTER_PERMISSIONS where RoleId='" & ddl_Role.SelectedValue & "'", con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.Fill(ds)
            gv_ModuleDetails.DataSource = ds
            gv_ModuleDetails.DataBind()
        Catch ex As Exception
            ErrorLogging.WriteLogFile(Session("UserId"), Request.Url.ToString & " --- GetModuleDetails()", ex.ToString)
        End Try
    End Sub
    Private Sub UpdatePreference(locationId As Integer, preference As Integer)
        Dim constr As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("UPDATE [MASTER_PERMISSIONS] SET [ORDERING] = @Preference WHERE Id = @Id")
                Using sda As New SqlDataAdapter()
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.AddWithValue("@Id", locationId)
                    cmd.Parameters.AddWithValue("@Preference", preference)
                    cmd.Connection = con
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
        End Using
    End Sub
End Class