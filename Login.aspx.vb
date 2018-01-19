Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient

Partial Class Login
    Inherits System.Web.UI.Page
    Dim adp As New SqlDataAdapter
    Dim cmd As SqlCommand
    Dim con As New SqlConnection
    Dim connection As String
    Dim Password As String
    Dim userId As String
    Dim x As String
    Public Shared Function passwordIsShort(ByVal minPasswordLength As Integer, ByVal enteredPasswordLength As Integer) As Boolean
        If enteredPasswordLength < minPasswordLength Then
            Return True
        Else
            Return False
        End If
    End Function

    Function GetVersion() As String
        Dim sVer As String = ""

        'Dim myBuildInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath)
        sVer = "VERSION.: 1.0" '& myBuildInfo.FileVersion
        Return sVer
    End Function

    Public Sub msgbox(ByVal strMessage As String)

        'finishes server processing, returns to client.
        Dim strScript As String = "<script language=JavaScript>"
        strScript += "window.alert(""" & strMessage & """);"
        strScript += "</script>"
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = strScript
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub btn_Login_Click(ByVal sender As Object, ByVal e As EventArgs)
        userId = txt_UserId.Value.ToString.Trim()
        Password = txt_Password.Value.ToString.Trim()
        Try

            Dim dtLogin = getLoginParameters()
            'cmd = New SqlCommand("select * from PARA_LOGIN", con)
            'Dim ds As New DataSet
            'adp = New SqlDataAdapter(cmd)
            'adp.Fill(ds, "PARA_LOGIN")

            If userNotYetActivated(txt_UserId.Value.ToString) Then
                lblLoginError.Text = "Your account has not yet been activated. Contact the administrator"
                Exit Sub
            End If

            If Not userExists(txt_UserId.Value.ToString) Then
                lblLoginError.Text = "Username not found"
                Exit Sub
            Else
                If isLockedOut(CInt(dtLogin.Rows(0).Item("MaximumLoginAttempts")), CInt(getLockCount(txt_UserId.Value.ToString))) Then
                    lblLoginError.Text = "Your account has been locked. Contact the administrator."
                    Exit Sub
                End If
                If txt_Password.Value.ToString <> userPassword(txt_UserId.Value.ToString) Then
                    lockUser(txt_UserId.Value.ToString)
                    lblLoginError.Text = "Wrong password entered. Lock count " & getLockCount(txt_UserId.Value.ToString) & "/" & dtLogin.Rows(0).Item("MaximumLoginAttempts") & ""
                    Exit Sub
                End If
            End If

            If Trim(Session("SessionID")) = "" Or IsDBNull(Session("SessionID")) Then
            Else
                SecureBank.endSession(Session("SessionID"))
            End If
            ''msgbox(Date.Now.TimeOfDay.ToString)
            'If txt_UserId.Text <> "admin" Then
            '    If Not isValidLoginTime() Then
            '        lblLoginError.Text = "The system has been shut down as a daily procedure. Contact the administrator for more details."
            '        Exit Sub
            '    End If
            'End If
            cmd = New SqlCommand("UserAuthentication1", con)
            'cmd = New SqlCommand("UserAuthentication", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@UserName", userId)
            cmd.Parameters.AddWithValue("@Password", Password)
            con.Open()
            x = cmd.ExecuteScalar()
            If x Is Nothing Then
                lockUser(txt_UserId.Value.ToString)
                lblLoginError.Text = "Incorrect username or password"
            Else
                FormsAuthentication.RedirectFromLoginPage(txt_UserId.Value.ToString, False)
                Dim dt As New DataTable()
                dt = getUserLoginDetails(userId, Password)
                Dim lockCount = dt.Rows(0).Item("LOCK_COUNT")
                'msgbox(dt.Rows.Count)
                'msgbox(lockCount & "," & dtLogin.Rows(0).Item("MaximumLoginAttempts"))
                If IsDBNull(lockCount) Or Trim(lockCount) = "" Then
                    lockCount = 0
                End If
                'If isLockedOut(CInt(lockCount), dtLogin.Rows(0).Item("MaximumLoginAttempts")) Then
                '    lblLoginError.Text = "Your account has been locked"
                '    Exit Sub
                'End If

                If dt.Rows.Count = 0 Then
                    ClientScript.RegisterStartupScript(GetType(Page), "anil", "<script>alert('')</script>")
                Else
                    Session("UserID") = txt_UserId.Value.ToString.Trim()
                    Session("ID") = dt.Rows(0)("ID")
                    Session("ROLE") = dt.Rows(0)("USER_ROLE")
                    Session("BRANCHCODE") = dt.Rows(0)("USER_BRANCH")
                    Session("BRANCHNAME") = dt.Rows(0)("BNCH_NAME")
                    Session("CustEMailID") = dt.Rows(0)("USER_EMAIL_ID")
                    Session("DASHBOARD") = dt.Rows(0)("DASHBOARD")
                    Session("ROLEDESC") = getRoleDescription(Session("ROLE"))
                    Session("Timeout") = dtLogin.Rows(0).Item("SessionTimeout")

                    'If (x = "1001") Then
                    '    Response.Redirect("SignPermission.aspx")
                    'ElseIf (x = "1002") Then
                    '    Response.Redirect("frmChequeSubmission.aspx")
                    'Else
                    '    Response.Redirect("ChequeDetail.aspx")
                    'End If
                    Session("SessionID") = Guid.NewGuid().ToString("N")
                    SecureBank.recordSession(Session("SessionID"))
                    'Exit Sub

                    If passwordExpired(Session("UserID")) Then
                        Session("PasswordExpired") = "True"
                        Response.Redirect("ChangePassword.aspx")
                        'lblLoginError.Text = "Your password has expired"
                        'Exit Sub
                    End If

                    If passwordIsShort(CInt(dtLogin.Rows(0).Item("MinimumPasswordLength")), CInt(txt_Password.Value.ToString.Length)) Then
                        'If passwordIsShort(8, 5) Then
                        Session("PasswordTooShort") = "True"
                        Response.Redirect("ChangePassword.aspx")
                    Else
                        If isDefaultPassword(dtLogin.Rows(0).Item("DefaultPassword"), txt_Password.Value.ToString) Then
                            Session("DefaultPassword") = "True"
                            Response.Redirect("ChangePassword.aspx")
                        Else
                            Session("DefaultPassword") = "False"
                        End If
                        Session("PasswordTooShort") = "False"
                        resetlockUser(txt_UserId.Value.ToString)
                        Response.Redirect("index.aspx")
                    End If
                End If
                'End If
            End If
        Catch ex As Exception
        Finally
            con.Close()
        End Try
    End Sub

    Protected Function getLockCount(ByVal userID As String) As String
        Try
            cmd = New SqlCommand("select LOCK_COUNT from MASTER_USERS where USER_LOGIN='" & userID & "'", con)
            Dim dsLock As New DataSet
            Dim adpLock As SqlDataAdapter
            adpLock = New SqlDataAdapter(cmd)
            adpLock.Fill(dsLock, "LOCK")
            If dsLock.Tables(0).Rows.Count > 0 Then
                Return dsLock.Tables(0).Rows(0).Item("LOCK_COUNT")
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Function getLoginParameters() As DataTable
        Try
            cmd = New SqlCommand("select * from PARA_LOGIN", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "LOGIN")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Function getRoleDescription(ByVal roleID As Double) As String
        Try
            cmd = New SqlCommand("select ROLE_DESCRIPTION from MASTER_ROLES where RoleID='" & roleID & "'", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Dim roleDesc = ""
            con.Open()
            roleDesc = cmd.ExecuteScalar
            con.Close()
            Return roleDesc
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Function hasSpecialPermissions(ByVal mModule As String, ByVal user As String) As Boolean
        ''afer access to certain page has been denied,
        ''check if permissions are in special_permissions table
        ''and still valid
        Try
            cmd = New SqlCommand("select ID from SPECIAL_PERMISSIONS where UserID='" & user & "' and ModuleID='" & mModule & "' and EndDate > getDate() and StartDate < getDate()", con)
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
            Return False
        End Try
    End Function

    Protected Function isDefaultPassword(ByVal defPwd As String, ByVal enteredPwd As String) As Boolean
        If defPwd = enteredPwd Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Function isLockedOut(ByVal lockNumber As Integer, ByVal lockedTimes As Integer) As Boolean
        ''check value of lock_count in users table for user
        ''if lock_count<lockNumber then continue login process else advise user that account is locked
        ''if lockNumber=0 then number of attempts is unlimited
        If lockedTimes >= lockNumber Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Function isValidLoginTime() As Boolean
        ''first confirm that passed arguments are valid time objects
        ''then validate if system time is within range
        ''update error label on login page if out of range
        Try
            Dim uptime, downtime As String
            cmd = New SqlCommand("select uptime,downtime from PARA_LOGIN", con)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "PARA_LOGIN")
            If ds.Tables(0).Rows.Count > 0 Then
                uptime = ds.Tables(0).Rows(0).Item("UPTIME").ToString
                downtime = ds.Tables(0).Rows(0).Item("DOWNTIME").ToString
                If IsDate(uptime) And IsDate(downtime) Then
                    If uptime < Date.Now.TimeOfDay.ToString And downtime > Date.Now.TimeOfDay.ToString Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub lockUser(ByVal user As String)
        ''if user is correct and password wrong, increase lock_count by 1
        ''if value in parameters table has been reached, update error label on login page and deny access through isLockedOut function
        ''if login success reset lock_count to 0
        Try
            cmd = New SqlCommand("update MASTER_USERS set LOCK_COUNT=isnull(LOCK_COUNT,0) + 1 where USER_LOGIN='" & user & "'", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
        'Label8.Text = GetVersion()
        Session.RemoveAll()
        If Not IsPostBack Then
            If Request.QueryString("sess") = "exp" Then
                lblLoginError.Text = "Your session has expired. Please login to continue"
                ClientScript.RegisterStartupScript(Me.GetType(), "HideLabel", "<script type=""text/javascript"">setTimeout(""document.getElementById('" & lblLoginError.ClientID & "').style.display='none'"",10000)</script>")
            End If
        End If
    End Sub
    Protected Function passwordExpired(ByVal user As String) As Boolean
        ''period is in days
        ''check if number of days since password was updated is still in range
        ''return true to continue with normal login and false to redirect to change password page
        Dim period As Integer
        Dim pDate As Date
        cmd = New SqlCommand("select PasswordExpiryPeriod from PARA_LOGIN", con)
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        period = cmd.ExecuteScalar
        con.Close()
        If period = 0 Or IsDBNull(period) Then
            ''NO LIMIT, so allow login
            Return True
        Else
            cmd = New SqlCommand("select PWD_DATE from MASTER_USERS where USER_LOGIN='" & user & "'", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            pDate = cmd.ExecuteScalar
            con.Close()
            If IsDBNull(pDate) Then
                ''CHANGE DATE NOT FOUND, just allow login
                Return True
            ElseIf IsDate(pDate) Then
                ''its a valid date, so continue with check
                Dim expDate As Date = DateAdd(DateInterval.Day, period, pDate)
                If expDate <= Date.Now Then
                    ''not yet expired
                    Return True
                Else
                    ''expired, update label
                    Return False
                End If
            End If
        End If
        Return False
    End Function

    Protected Sub resetlockUser(ByVal user As String)
        ''if user is correct and password wrong, increase lock_count by 1
        ''if value in parameters table has been reached, update error label on login page and deny access through isLockedOut function
        ''if login success reset lock_count to 0
        Try
            cmd = New SqlCommand("update MASTER_USERS set LOCK_COUNT=0 where USER_LOGIN='" & user & "'", con)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch
        End Try
    End Sub

    Protected Function userExists(ByVal userID As String) As Boolean
        Try
            Dim ds As New DataSet
            cmd = New SqlCommand("select * from MASTER_USERS where USER_LOGIN='" & userID & "'", con)
            ds = New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "USERS")
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Function userNotYetActivated(ByVal userID As String) As Boolean
        Try
            Dim ds As New DataSet
            cmd = New SqlCommand("select * from MASTER_USERS where USER_LOGIN='" & userID & "'", con)
            ds = New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "USERS")
            If ds.Tables(0).Rows.Count > 0 Then
                Return False
            Else
                cmd = New SqlCommand("select * from TEMP_USERS where ACTION='Insert' and USER_LOGIN='" & userID & "' and UPDATED='0'", con)
                Dim dsUsers As New DataSet
                Dim adpUsers As SqlDataAdapter = New SqlDataAdapter(cmd)
                adpUsers.Fill(dsUsers, "USERS")
                If dsUsers.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Protected Function userPassword(ByVal userID As String) As String
        Try
            cmd = New SqlCommand("select USER_PWD from MASTER_USERS where USER_LOGIN='" & userID & "'", con)
            Dim dsPwd As New DataSet
            Dim adpPwd As SqlDataAdapter = New SqlDataAdapter(cmd)
            adpPwd.Fill(dsPwd, "PWD")
            If dsPwd.Tables(0).Rows.Count > 0 Then
                Return dsPwd.Tables(0).Rows(0).Item("USER_PWD")
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Function getUserLoginDetails(ByVal userId As String, ByVal Password As String) As DataTable
        Try
            Dim ds As New DataSet()
            'adp = New SqlDataAdapter("UserAuthenticationDetails", con)
            adp = New SqlDataAdapter("UserAuthenticationDetails1", con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.AddWithValue("@UserName", userId)
            adp.SelectCommand.Parameters.AddWithValue("@Password", Password)
            adp.Fill(ds)
            Dim dt = New DataTable()
            dt = ds.Tables(0)
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class