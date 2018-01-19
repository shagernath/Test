Imports System.Data.SqlClient
Imports System.Data

Partial Class rptUserActivityPages
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = String.Empty Then
                'not logged in redirect to login page
                Response.Redirect("~/Login.aspx", False)
                Exit Sub
            End If

            Dim SessionID = Request.QueryString("SessionID")


            Dim myreport = New CrystalDecisions.CrystalReports.Engine.ReportDocument()

            myreport.Load(Server.MapPath("rptUserActivityPages.rpt"))

            Dim myParameterFields As New CrystalDecisions.Shared.ParameterFields()
            Dim myParameterField1 As New CrystalDecisions.Shared.ParameterField()
            Dim myDiscreteValue1 As New CrystalDecisions.Shared.ParameterDiscreteValue()

            myParameterField1.ParameterFieldName = "pSessionid"
            myDiscreteValue1.Value = SessionID
            myParameterField1.CurrentValues.Add(myDiscreteValue1)
            myParameterFields.Add(myParameterField1)

            CrystalReportViewer1.ReportSource = myreport
            CrystalReportViewer1.ParameterFieldInfo = myParameterFields
            CrystalReportViewer1.RefreshReport()
        Catch ex As Exception

        End Try
    End Sub
End Class
