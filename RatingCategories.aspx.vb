Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web

Partial Class RatingCategories
    Inherits System.Web.UI.Page
    Dim cmd As SqlCommand
    Dim con As New SqlConnection
    Dim adp As New SqlDataAdapter
    Dim connection As String
    Dim urlPermission As String = "PermissionDenied.aspx"
    Public Shared criteriaEditID As Double

    Protected Sub btnAddCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCategory.Click
        Try
            cmd = New SqlCommand("insert into PARA_RATING_CATEGORIES (CLIENT_TYPE,CATEGORY) values ('" & BankString.removeSpecialCharacter(cmbClientType.Text) & "','" & BankString.removeSpecialCharacter(txtClientCategory.Text) & "')", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            If cmd.ExecuteNonQuery Then
                msgbox("Category successfully saved")
                getCategories()
            Else
                msgbox("Error saving category")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.MaintainScrollPositionOnPostBack = True
            Page.Header.Title = "Credit Management : Client Rating"
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            If Not IsPostBack Then
                loadClientTypes()
                getCategories()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
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

    Protected Sub getCategories()
        Try
            cmd = New SqlCommand("select * from PARA_RATING_CATEGORIES where CLIENT_TYPE='" & cmbClientType.SelectedValue & "'", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "Category")
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

    Protected Sub cmbClientType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbClientType.SelectedIndexChanged
        getCategories()
    End Sub

    Protected Sub grdCategories_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdCategories.RowCancelingEdit
        grdCategories.EditIndex = -1
        getCategories()
    End Sub

    Protected Sub grdCategories_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCategories.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow And grdCategories.EditIndex = e.Row.RowIndex) Then
            'msgbox(DirectCast(e.Row.FindControl("grdUsers_txtUserType"), TextBox).Text)
            Dim cmbCliType = DirectCast(e.Row.FindControl("cmbClientTypeEdit"), DropDownList)
            loadClientTypesCombo(cmbCliType)
            Try
                cmbCliType.SelectedValue = DirectCast(e.Row.FindControl("txtOldClientType"), TextBox).Text
            Catch ex As Exception
                'cmbCliType.SelectedItem.Text = ""
            End Try
        End If
    End Sub

    Protected Sub grdCategories_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdCategories.RowDeleting
        criteriaEditID = DirectCast(grdCategories.Rows(e.RowIndex).FindControl("txtGrdCategory"), TextBox).Text
        cmd = New SqlCommand("update PARA_RATING_CATEGORIES set active=0 where ID='" & criteriaEditID & "'", con)
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        If cmd.ExecuteNonQuery Then
            msgbox("Successfully deactivated")
        Else
            msgbox("Error deactivating")
        End If
        con.Close()
        getCategories()
    End Sub

    Protected Sub grdCategories_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdCategories.RowEditing
        criteriaEditID = DirectCast(grdCategories.Rows(e.NewEditIndex).FindControl("txtGrdCategory"), TextBox).Text
        grdCategories.EditIndex = e.NewEditIndex
        getCategories()
    End Sub

    Protected Sub grdCategories_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdCategories.RowUpdating
        If Trim(criteriaEditID) = "" Or IsDBNull(criteriaEditID) Then
            msgbox("No record selected for update")
            Exit Sub
        End If
        Dim cliType As String = DirectCast(grdCategories.Rows(e.RowIndex).FindControl("cmbClientTypeEdit"), DropDownList).SelectedValue

        Dim cat As String = DirectCast(grdCategories.Rows(e.RowIndex).FindControl("txtCategoryEdit"), TextBox).Text

        Dim oldCliType, oldCat As String
        oldCliType = ""
        oldCat = ""

        cmd = New SqlCommand("select * from PARA_RATING_CATEGORIES where ID='" & criteriaEditID & "'", con)
        Dim ds1 As New DataSet
        adp = New SqlDataAdapter(cmd)
        adp.Fill(ds1, "VALUES")
        If ds1.Tables(0).Rows.Count > 0 Then
            oldCliType = ds1.Tables(0).Rows(0).Item("CLIENT_TYPE").ToString
            oldCat = ds1.Tables(0).Rows(0).Item("CATEGORY").ToString
        End If

        cmd = New SqlCommand("update PARA_RATING_CATEGORIES set CLIENT_TYPE='" & BankString.removeSpecialCharacter(cliType) & "', CATEGORY='" & BankString.removeSpecialCharacter(cat) & "' where ID='" & criteriaEditID & "'", con)

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
        grdCategories.EditIndex = -1
        getCategories()
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

    Protected Sub loadClientTypesCombo(ByVal drdDown As DropDownList)
        Try
            cmd = New SqlCommand("select * from PARA_CLIENT_TYPES", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "Clients")
            If ds.Tables(0).Rows.Count > 0 Then
                drdDown.DataSource = ds.Tables(0)
                drdDown.DataValueField = "CLIENT_TYPE"
                drdDown.DataTextField = "CLIENT_TYPE"
            Else
                drdDown.DataSource = Nothing
            End If
            drdDown.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
End Class
