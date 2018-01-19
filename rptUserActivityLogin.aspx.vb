Imports System.Data.SqlClient
Imports System.Data

Partial Class rptUserActivityLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = String.Empty Then
                'not logged in redirect to login page
                Response.Redirect("~/Login.aspx", False)
                Exit Sub
            End If

            Dim userName = Request.QueryString("user")
            Dim fromDate = Request.QueryString("fromDate")
            Dim toDate = Request.QueryString("toDate")


            Dim myreport = New CrystalDecisions.CrystalReports.Engine.ReportDocument()

            myreport.Load(Server.MapPath("rptUserActivityLogin.rpt"))

            Dim myParameterFields As New CrystalDecisions.Shared.ParameterFields()
            Dim myParameterField1 As New CrystalDecisions.Shared.ParameterField()
            Dim myDiscreteValue1 As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim myParameterField2 As New CrystalDecisions.Shared.ParameterField()
            Dim myDiscreteValue2 As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim myParameterField3 As New CrystalDecisions.Shared.ParameterField()
            Dim myDiscreteValue3 As New CrystalDecisions.Shared.ParameterDiscreteValue()

            myParameterField1.ParameterFieldName = "pUser"
            myDiscreteValue1.Value = userName
            myParameterField1.CurrentValues.Add(myDiscreteValue1)
            myParameterFields.Add(myParameterField1)

            myParameterField2.ParameterFieldName = "pfromDate"
            myDiscreteValue2.Value = fromDate
            myParameterField2.CurrentValues.Add(myDiscreteValue2)
            myParameterFields.Add(myParameterField2)

            myParameterField3.ParameterFieldName = "ptoDate"
            myDiscreteValue3.Value = toDate
            myParameterField3.CurrentValues.Add(myDiscreteValue3)
            myParameterFields.Add(myParameterField3)

            CrystalReportViewer1.ReportSource = myreport
            CrystalReportViewer1.ParameterFieldInfo = myParameterFields
            CrystalReportViewer1.RefreshReport()
        Catch ex As Exception

        End Try
    End Sub
End Class
