
Partial Class Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SecureBank.endSession(Session("SessionID"))
            Session.RemoveAll()
            Session.Clear()
            Session.Abandon()
            Response.Redirect("Login.aspx")
        Catch ex As Exception

        End Try
    End Sub
End Class
