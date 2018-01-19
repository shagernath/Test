Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web

Partial Class RatingQuestions
    Inherits System.Web.UI.Page
    Dim cmd As SqlCommand
    Dim con As New SqlConnection
    Dim adp As New SqlDataAdapter
    Dim connection As String
    Dim urlPermission As String = "PermissionDenied.aspx"
    Public Shared criteriaEditID As Double

    Protected Sub btnAddQuestion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddQuestion.Click
        Try
            cmd = New SqlCommand("insert into PARA_RATING_QUESTIONS (CLIENT_TYPE,CATEGORY_ID,QUESTION) values ('" & BankString.removeSpecialCharacter(cmbClientType.Text) & "','" & cmbRatingCategory.SelectedValue & "','" & txtQuestion.Text & "')", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            If cmd.ExecuteNonQuery Then
                msgbox("Question successfully saved")
                getQuestions()
            Else
                msgbox("Error saving question")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.MaintainScrollPositionOnPostBack = True
            Page.Header.Title = "Credit Management : Questions for Rating"
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            If Not IsPostBack Then
                loadClientTypes()
                loadCategories()
                getQuestions()
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

    Protected Sub loadCategories()
        Try
            cmd = New SqlCommand("select * from PARA_RATING_CATEGORIES where CLIENT_TYPE='" & cmbClientType.Text & "'", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "CATEGORY")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbRatingCategory.DataSource = ds.Tables(0)
                cmbRatingCategory.DataValueField = "ID"
                cmbRatingCategory.DataTextField = "CATEGORY"
            Else
                cmbRatingCategory.DataSource = Nothing
            End If
            cmbRatingCategory.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub cmbClientType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbClientType.SelectedIndexChanged
        loadCategories()
        cmbRatingCategory_SelectedIndexChanged(sender, New System.EventArgs)
    End Sub

    Protected Sub cmbRatingCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRatingCategory.SelectedIndexChanged
        getQuestions()
    End Sub

    Protected Sub getQuestions()
        Try
            cmd = New SqlCommand("select * from PARA_RATING_QUESTIONS prq,PARA_RATING_CATEGORIES prc where prc.ID=prq.CATEGORY_ID and prq.CLIENT_TYPE='" & cmbClientType.SelectedValue & "' and prq.CATEGORY_ID='" & cmbRatingCategory.SelectedValue & "'", con)
            'msgbox(cmd.CommandText)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "Questions")
            If ds.Tables(0).Rows.Count > 0 Then
                grdQuestions.DataSource = ds.Tables(0)
            Else
                grdQuestions.DataSource = Nothing
            End If
            grdQuestions.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub grdQuestions_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdQuestions.RowCancelingEdit
        grdQuestions.EditIndex = -1
        getQuestions()
    End Sub

    Protected Sub grdQuestions_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdQuestions.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow And grdQuestions.EditIndex = e.Row.RowIndex) Then
            'msgbox(DirectCast(e.Row.FindControl("grdUsers_txtUserType"), TextBox).Text)
            Dim lblCliType = DirectCast(e.Row.FindControl("lblGrdCliType"), Label)

            Dim cmbCatEdit = DirectCast(e.Row.FindControl("cmbGrdCategoryEdit"), DropDownList)

            Try
                lblCliType.Text = DirectCast(e.Row.FindControl("txtClientTypeEdit"), TextBox).Text
            Catch ex As Exception
                'cmbCliType.SelectedItem.Text = ""
            End Try
            cmd = New SqlCommand("select * from PARA_RATING_CATEGORIES where CLIENT_TYPE='" & lblCliType.Text & "'", con)
            Dim ds1 As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds1, "CATEGORIES")
            If ds1.Tables(0).Rows.Count > 0 Then
                cmbCatEdit.DataSource = ds1.Tables(0)
                cmbCatEdit.DataTextField = "CATEGORY"
                cmbCatEdit.DataValueField = "ID"
            Else
                cmbCatEdit.DataSource = Nothing
            End If
            cmbCatEdit.DataBind()
            Try
                cmbCatEdit.SelectedValue = DirectCast(e.Row.FindControl("txtGrdCatID"), TextBox).Text
            Catch ex As Exception
                cmbCatEdit.SelectedItem.Text = ""
            End Try
        End If
    End Sub

    Protected Sub loadEditCategories(ByVal cmbCT As DropDownList, ByVal cmbCat As DropDownList)
        Try
            cmd = New SqlCommand("select * from PARA_RATING_CATEGORIES where CLIENT_TYPE='" & cmbCT.SelectedValue & "'", con)
            Dim ds1 As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds1, "CATEGORIES")
            If ds1.Tables(0).Rows.Count > 0 Then
                cmbCat.DataSource = ds1.Tables(0)
                cmbCat.DataTextField = "CATEGORY"
                cmbCat.DataValueField = "ID"
            Else
                cmbCat.DataSource = Nothing
            End If
            cmbCat.DataBind()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub grdQuestions_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdQuestions.RowDeleting
        criteriaEditID = DirectCast(grdQuestions.Rows(e.RowIndex).FindControl("txtGrdQuestionID"), TextBox).Text
        cmd = New SqlCommand("update PARA_RATING_QUESTIONS set active=0 where ID='" & criteriaEditID & "'", con)
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
        getQuestions()
    End Sub

    Protected Sub grdQuestions_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdQuestions.RowEditing
        criteriaEditID = DirectCast(grdQuestions.Rows(e.NewEditIndex).FindControl("txtGrdQuestionID"), TextBox).Text
        grdQuestions.EditIndex = e.NewEditIndex
        getQuestions()
    End Sub

    Protected Sub grdQuestions_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdQuestions.RowUpdating
        If Trim(criteriaEditID) = "" Or IsDBNull(criteriaEditID) Then
            msgbox("No record selected for update")
            Exit Sub
        End If
        Dim cliType As String = DirectCast(grdQuestions.Rows(e.RowIndex).FindControl("lblGrdCliType"), Label).Text
        Dim cat As String = DirectCast(grdQuestions.Rows(e.RowIndex).FindControl("cmbGrdCategoryEdit"), DropDownList).SelectedValue

        Dim qsn As String = DirectCast(grdQuestions.Rows(e.RowIndex).FindControl("txtQuestionEdit"), TextBox).Text

        Dim oldCliType, oldCat, oldQsn As String
        oldCliType = ""
        oldCat = ""
        oldQsn = ""

        cmd = New SqlCommand("select * from PARA_RATING_QUESTIONS where ID='" & criteriaEditID & "'", con)
        Dim ds1 As New DataSet
        adp = New SqlDataAdapter(cmd)
        adp.Fill(ds1, "QUESTIONS")
        If ds1.Tables(0).Rows.Count > 0 Then
            oldCliType = ds1.Tables(0).Rows(0).Item("CLIENT_TYPE").ToString
            oldCat = ds1.Tables(0).Rows(0).Item("CATEGORY_ID").ToString
            oldQsn = ds1.Tables(0).Rows(0).Item("QUESTION").ToString
        End If

        cmd = New SqlCommand("update PARA_RATING_QUESTIONS set CLIENT_TYPE='" & cliType & "', CATEGORY_ID='" & cat & "',QUESTION='" & qsn & "' where ID='" & criteriaEditID & "'", con)

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
        grdQuestions.EditIndex = -1
        getQuestions()
    End Sub

    Protected Sub cmbGrdCliType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cmbCT = DirectCast(grdQuestions.Rows(grdQuestions.EditIndex).FindControl("cmbGrdCliType"), DropDownList)
        Dim cmbCat = DirectCast(grdQuestions.Rows(grdQuestions.EditIndex).FindControl("cmbGrdCategoryEdit"), DropDownList)
        loadEditCategories(cmbCT, cmbCat)
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
End Class
