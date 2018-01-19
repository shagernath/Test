<%@ Application Language="VB" %>
<%@ Import Namespace="System.Threading" %>

<script RunAt="server">
    Private Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        Dim cookieName As String = FormsAuthentication.FormsCookieName
        Dim authCookie As HttpCookie = Context.Request.Cookies(cookieName)

        If Nothing Is authCookie Then
            'There is no authentication cookie.
            Return
        End If
        Dim authTicket As FormsAuthenticationTicket = Nothing
        Try
            authTicket = FormsAuthentication.Decrypt(authCookie.Value)
        Catch ex As Exception
            'Write the exception to the Event Log.
            Return
        End Try
        If Nothing Is authTicket Then
            'Cookie failed to decrypt.
            Return
        End If
        'When the ticket was created, the UserData property was assigned a
        'pipe-delimited string of group names.
        Dim groups() As String = authTicket.UserData.Split(New Char() {"|"c})
        'Create an Identity.
        Dim id As New System.Security.Principal.GenericIdentity(authTicket.Name, "LdapAuthentication")
        'This principal flows throughout the request.
        Dim principal As New System.Security.Principal.GenericPrincipal(id, groups)
        Context.User = principal
    End Sub

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
        Dim ex As Exception = Server.GetLastError()
        If TypeOf ex Is ThreadAbortException Then
            Return
        End If
        ErrorLogging.WriteLogFile(Session("UserId"), HttpContext.Current.Request.Url.ToString, ex.ToString)
        'Response.Redirect("unexpectederror.htm")
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends.
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer
        ' or SQLServer, the event is not raised.
    End Sub
</script>