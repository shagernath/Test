
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "onLoad", "DisplaySessionTimeout()", True)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
