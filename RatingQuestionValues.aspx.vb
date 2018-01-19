Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class RatingQuestionValues
    Inherits System.Web.UI.Page
    Dim cmd As SqlCommand
    Dim con As New SqlConnection
    Dim connection As String
    Dim adp As New SqlDataAdapter
    Public Shared criteriaEditID As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.MaintainScrollPositionOnPostBack = True
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            If Not IsPostBack Then
                loadClientTypes()
                loadCategories()
                loadQuestions()
                getRatings()
            End If
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

    Protected Sub loadCategories()
        Try
            cmbCategory.Items.Clear()
            cmd = New SqlCommand("select * from PARA_RATING_CATEGORIES where CLIENT_TYPE='" & cmbApplicantType.SelectedValue & "'", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "Categories")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbCategory.DataSource = ds.Tables(0)
                cmbCategory.DataTextField = "CATEGORY"
                cmbCategory.DataValueField = "ID"
            Else
                cmbCategory.DataSource = Nothing
            End If
            cmbCategory.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub loadQuestions()
        Try
            cmbQuestion.Items.Clear()
            cmd = New SqlCommand("select * from PARA_RATING_QUESTIONS where CATEGORY_ID='" & cmbCategory.SelectedValue & "'", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "Questions")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbQuestion.DataSource = ds.Tables(0)
                cmbQuestion.DataTextField = "QUESTION"
                cmbQuestion.DataValueField = "ID"
            Else
                cmbQuestion.DataSource = Nothing
            End If
            cmbQuestion.DataBind()
            getRatings()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub getRatings()
        Try
            grdRateCriteria.DataSource = Nothing
            grdRateCriteria.DataBind()

            cmd = New SqlCommand("select * from PARA_RATING_VALUES where QUESTION_ID='" & cmbQuestion.SelectedValue & "'", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "VALUES")
            If ds.Tables(0).Rows.Count > 0 Then
                grdRateCriteria.DataSource = ds.Tables(0)
            Else
                grdRateCriteria.DataSource = Nothing
            End If
            grdRateCriteria.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub btnAddRating_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRating.Click
        Try
            If Not IsNumeric(Trim(txtRating.Text)) Then
                msgbox("Enter number for rating")
                txtRating.Focus()
                Exit Sub
            End If
            cmd = New SqlCommand("select * from PARA_RATING_VALUES where QUESTION_ID='" & cmbQuestion.SelectedValue & "' and COMMENT='" & Trim(txtActualValue.Text) & "'", con)
            adp = New SqlDataAdapter(cmd)
            Dim ds As New DataSet
            adp.Fill(ds, "RATINGS")
            If ds.Tables(0).Rows.Count > 0 Then
                cmd = New SqlCommand("update PARA_RATING_VALUES set CALC_VALUE='" & Trim(txtRating.Text) & "' where QUESTION_ID='" & cmbQuestion.SelectedValue & "' and COMMENT='" & Trim(txtActualValue.Text) & "'", con)
            Else
                cmd = New SqlCommand("insert into PARA_RATING_VALUES([COMMENT],[CALC_VALUE],[QUESTION_ID]) values('" & BankString.removeSpecialCharacter(Trim(txtActualValue.Text)) & "','" & Trim(txtRating.Text) & "','" & cmbQuestion.SelectedValue & "')", con)
            End If
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            txtActualValue.Text = ""
            txtRating.Text = ""
            getRatings()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub cmbApplicantType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbApplicantType.SelectedIndexChanged

        grdRateCriteria.DataSource = Nothing
        grdRateCriteria.DataBind()
        loadCategories()
        cmbCategory_SelectedIndexChanged(sender, New System.EventArgs)
    End Sub

    Protected Sub cmbCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged
        loadQuestions()
    End Sub

    Protected Sub cmbQuestion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbQuestion.SelectedIndexChanged
        getRatings()
    End Sub

    Protected Sub grdRateCriteria_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdRateCriteria.RowCancelingEdit
        grdRateCriteria.EditIndex = -1
        getRatings()
    End Sub

    Protected Sub grdRateCriteria_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdRateCriteria.RowDeleting
        criteriaEditID = DirectCast(grdRateCriteria.Rows(e.RowIndex).FindControl("txtRateID"), TextBox).Text
        'cmd = New SqlCommand("delete from MASTER_ROLES where RoleID='" & rolesEditID & "'", con)
        'send to TEMP_ROLES table for authorization
        cmd = New SqlCommand("update PARA_RATING_VALUES set active=0 where ID='" & criteriaEditID & "'", con)
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
        getRatings()
    End Sub

    Protected Sub grdRateCriteria_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdRateCriteria.RowEditing
        criteriaEditID = DirectCast(grdRateCriteria.Rows(e.NewEditIndex).FindControl("txtRateID"), TextBox).Text
        grdRateCriteria.EditIndex = e.NewEditIndex
        getRatings()
    End Sub

    Protected Sub grdRateCriteria_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdRateCriteria.RowUpdating
        If Trim(criteriaEditID) = "" Or IsDBNull(criteriaEditID) Then
            msgbox("No record selected for update")
            Exit Sub
        End If
        Dim actVal As String = DirectCast(grdRateCriteria.Rows(e.RowIndex).FindControl("txtActValEdit"), TextBox).Text

        Dim calc As String = DirectCast(grdRateCriteria.Rows(e.RowIndex).FindControl("txtRatingEdit"), TextBox).Text

        Dim oldActVal, oldCalc As String
        oldActVal = ""
        oldCalc = ""

        cmd = New SqlCommand("select * from PARA_RATING_VALUES where ID='" & criteriaEditID & "'", con)
        Dim ds1 As New DataSet
        adp = New SqlDataAdapter(cmd)
        adp.Fill(ds1, "VALUES")
        If ds1.Tables(0).Rows.Count > 0 Then
            oldActVal = ds1.Tables(0).Rows(0).Item("COMMENT").ToString
            oldCalc = ds1.Tables(0).Rows(0).Item("CALC_VALUE").ToString
        End If

        cmd = New SqlCommand("update PARA_RATING_VALUES set COMMENT='" & BankString.removeSpecialCharacter(actVal) & "', CALC_VALUE='" & calc & "' where ID='" & criteriaEditID & "'", con)

        'Dim updateCmd = "update MASTER_ROLES set RoleName=''" & RoleName & "'', USER_STATUS=''" & status & "'' where RoleID=''" & rolesEditID & "''"
        'cmd = New SqlCommand("insert into TEMP_ROLES (ACTION,OLD_RoleID,RoleID,OLD_RoleName,RoleName,OLD_USER_STATUS,USER_STATUS,USER_MODIFIED_BY,USER_MODIFIED_DATE,COMMAND,UPDATED) values ('UPDATE','" & rolesEditID & "','" & rolesEditID & "','" & oldRoleName & "','" & RoleName & "','" & oldUserStatus & "','" & status & "','" & Session("UserID") & "','" & Date.Now & "','" & updateCmd & "',0)", con)
        'msgbox(cmd.CommandText)

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
        grdRateCriteria.EditIndex = -1
        getRatings()
    End Sub

    Protected Sub loadClientTypes()
        Try
            cmd = New SqlCommand("select * from PARA_CLIENT_TYPES", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "Clients")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbApplicantType.DataSource = ds.Tables(0)
                cmbApplicantType.DataValueField = "CLIENT_TYPE"
                cmbApplicantType.DataTextField = "CLIENT_TYPE"
            Else
                cmbApplicantType.DataSource = Nothing
            End If
            cmbApplicantType.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
End Class
