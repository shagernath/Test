
Partial Class RatingStatement
    Inherits System.Web.UI.Page

    Protected Sub btnViewRating_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewRating.Click
        Try
            Dim strscript As String
           
            strscript = "<script langauage=JavaScript>"
            strscript += "window.open('rptRatingStatement.aspx?LOANID=" & txtLoanAppNo.Text & "');"
            strscript += "</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "newwin", strscript)

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
