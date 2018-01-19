Imports System.Data
Imports System.Data.SqlClient
Imports DateFormat

Partial Class frmLoanRepayments
    Inherits System.Web.UI.Page
    Dim con As New SqlConnection
    Dim connection As String
    Dim cmd As SqlCommand
    Dim adp As SqlDataAdapter
    Public ds As New DataSet

    Protected Sub btnCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheck.Click
        Try
            Dim strscript As String

            strscript = "<script langauage=JavaScript>"
            strscript += "window.open('rptLoanRepayment.aspx?from=" & getSaveDate(bdpFromDate.SelectedDate) & "&to=" & getSaveDate(bdpToDate.SelectedDate) & "');"
            strscript += "</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "newwin", strscript)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
        If Not IsPostBack Then

        End If
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
End Class
