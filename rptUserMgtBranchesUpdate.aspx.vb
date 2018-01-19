Imports CrystalDecisions.CrystalReports.Engine

Partial Class rptUserMgtBranchesUpdate
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = String.Empty Then
                'not logged in redirect to login page
                Response.Redirect("~/Login.aspx", False)
                Exit Sub
            End If

            Dim DecQuery As New BankEncryption64

            Dim query = DecQuery.Decrypt(Request.QueryString("qry").Replace(" ", "+"), "lovely12345")


            Dim myreport = New CrystalDecisions.CrystalReports.Engine.ReportDocument()

            Dim cryRpt As ReportDocument = New ReportDocument()
            Dim kk As String = ""

            kk = Server.MapPath("rptUserMgtBranchesUpdate.rpt")
            cryRpt.Load(kk)
            'cryRpt.SetDatabaseLogon("sa", "")
            cryRpt.SetParameterValue(0, query)
            CrystalReportViewer1.ReportSource = cryRpt
        Catch ex As Exception

        End Try
    End Sub
End Class
