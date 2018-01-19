Imports System.Data
Imports System.Data.SqlClient

Partial Class frmUserActivityLogin
    Inherits System.Web.UI.Page
    Dim cmd As SqlCommand
    Dim adp As SqlDataAdapter
    Dim con As New SqlConnection

    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
        Try
            Dim strscript As String

            strscript = "<script langauage=JavaScript>"
            strscript += "window.open('rptUserActivityLogin.aspx?user=" & cmbUsernames.SelectedValue & "&fromDate=" & bdpDateFrom.Text.ToString & "&toDate=" & bdpDateTo.Text.ToString & "');"
            strscript += "</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "newwin", strscript)
        Catch ex As Exception
            msgbox(ex.Message)
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.MaintainScrollPositionOnPostBack = True
            'Page.Header.Title = "Credit Management: Print User Login Report"
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            If Not IsPostBack Then
                getUsernames()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class