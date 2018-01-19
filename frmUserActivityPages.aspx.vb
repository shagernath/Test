Imports System.Data
Imports System.Data.SqlClient

Partial Class frmUserActivityPages
    Inherits System.Web.UI.Page
    Dim cmd As SqlCommand
    Dim adp As SqlDataAdapter
    Dim con As New SqlConnection

    Protected Sub loadSessions()
        Try
            cmd = New SqlCommand("SELECT SESSION_ID, start_time AS [LOGIN TIME], END_TIME AS [LOGOUT TIME], IP_ADDRESS AS [IP ADDRESS], MACHINE_NAME AS [MACHINE NAME], BROWSER, convert(time,END_TIME-START_TIME) AS SESSION_TIME FROM SESSION_LOG WHERE USERID='" & cmbUsernames.SelectedValue & "' and START_TIME between convert(datetime, '" & bdpFromDate.Text.ToString & "', 120) and convert(datetime,  '" & bdptoDate.Text.ToString & "', 120) order by start_time DESC", con)
            'msgbox(cmd.CommandText)
            Dim ds As New DataSet
            adp = New SqlDataAdapter(cmd)
            adp.Fill(ds, "session")
            If ds.Tables(0).Rows.Count > 0 Then
                grdSessions.DataSource = ds.Tables(0)
            Else
                grdSessions.DataSource = Nothing
            End If
            grdSessions.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        'Page.Header.Title = "Credit Management: Print Amortization Schedule"
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
        If Not IsPostBack Then
            getUsernames()
            loadSessions()
        End If
    End Sub

    Protected Sub grdSessions_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSessions.RowDataBound
        Try
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                Dim txtLoanReqID As TextBox
                txtLoanReqID = DirectCast(e.Row.FindControl("txtsESSION"), TextBox)
                Dim loanID = txtLoanReqID.Text

                Dim lnkBtn As HyperLink
                lnkBtn = DirectCast(e.Row.FindControl("HyperLink1"), HyperLink)
                lnkBtn.NavigateUrl = "rptUserActivityPages.aspx?SessionID=" & loanID
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

    Protected Sub getUsernames()
        Try
            cmd = New SqlCommand("select USERID,USER_LOGIN from MASTER_USERS", con)
            adp = New SqlDataAdapter(cmd)
            Dim ds As New DataSet
            adp.Fill(ds, "USERS")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbUsernames.DataSource = ds.Tables(0)
                cmbUsernames.DataTextField = "USER_LOGIN"
                cmbUsernames.DataValueField = "USER_LOGIN"
            Else
                cmbUsernames.DataSource = Nothing
            End If
            cmbUsernames.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
        Try
            loadSessions()
        Catch ex As Exception

        End Try
    End Sub
End Class