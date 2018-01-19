Imports System.Data
Imports System.Data.SqlClient
Imports CreditManager
Imports ErrorLogging

Partial Class ParaCreditProducts
    Inherits System.Web.UI.Page
    Public Shared typeEditID As Double
    Dim adp As New SqlDataAdapter
    Dim cmd As SqlCommand
    Dim con As New SqlConnection
    Dim connection As String
    Dim urlPermission As String = "PermissionDenied.aspx"

    Protected Sub btnAddProduct_Click(sender As Object, e As EventArgs) Handles btnAddProduct.Click
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd As New SqlCommand("insert into ParaProductTypes (Product) values (@Product)", con)
                cmd.Parameters.AddWithValue("@Product", txtProduct.Text)
                con.Open()
                If cmd.ExecuteNonQuery() Then
                    notify("Product added", "success")
                    loadParaProductType(cmbProductType)
                    txtProduct.Text = ""
                Else
                    notify("Error adding product", "error")
                End If
                con.Close()
            End Using
        End Using
    End Sub

    Protected Sub btnAddProductType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddProductType.Click
        Try
            If cmbProductType.SelectedValue = "" Then
                notify("Select product type", "error")
                cmbProductType.Focus()
            ElseIf Trim(txtDisplayName.Text) = "" Then
                notify("Enter the display name", "error")
                txtDisplayName.Focus()
            ElseIf Trim(txtMinAmt.Text) = "" Or Not IsNumeric(txtMinAmt.Text) Then
                notify("Enter numeric value for minimum amount", "error")
                txtMinAmt.Focus()
            ElseIf Trim(txtMaxAmt.Text) = "" Or Not IsNumeric(txtMaxAmt.Text) Then
                notify("Enter numeric value for maximum amount", "error")
                txtMaxAmt.Focus()
            ElseIf Trim(txtMinimumIntRate.Text) = "" Or Not IsNumeric(txtMinimumIntRate.Text) Then
                notify("Enter numeric value for minimum interest rate", "error")
                txtMinimumIntRate.Focus()
            ElseIf Trim(txtMaximumIntRate.Text) = "" Or Not IsNumeric(txtMaximumIntRate.Text) Then
                notify("Enter numeric value for maximum interest rate", "error")
                txtMaximumIntRate.Focus()
            ElseIf Trim(txtDefaultInterestRate.Text) = "" Or Not IsNumeric(txtDefaultInterestRate.Text) Then
                notify("Enter numeric value for default interest rate", "error")
                txtDefaultInterestRate.Focus()
            Else
                Using cmd As New SqlCommand("SaveCreditProduct")
                    cmd.CommandType = CommandType.StoredProcedure
                    Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        cmd.Connection = con
                        'cmd.Parameters.AddWithValue("@ClientType", cmbClientType.SelectedValue)
                        cmd.Parameters.AddWithValue("@isProduct", IIf(grdProductTypes.SelectedIndex = -1, "0", ViewState("grdID"))) ' grdProductTypes.SelectedDataKey.Value.ToString))
                        'cmd.Parameters.AddWithValue("@isProduct", "0")
                        cmd.Parameters.AddWithValue("@ProductType", cmbProductType.SelectedValue)
                        'msgbox("ydtfhjgknm")
                        cmd.Parameters.AddWithValue("@DisplayName", txtDisplayName.Text)
                        cmd.Parameters.AddWithValue("@MinAmt", txtMinAmt.Text.Replace("$", "").Replace("US", ""))
                        cmd.Parameters.AddWithValue("@MaxAmt", txtMaxAmt.Text.Replace("$", "").Replace("US", ""))
                        cmd.Parameters.AddWithValue("@MinIntRate", txtMinimumIntRate.Text)
                        cmd.Parameters.AddWithValue("@MaxIntRate", txtMaximumIntRate.Text)
                        cmd.Parameters.AddWithValue("@DefaultIntRate", txtDefaultInterestRate.Text)
                        cmd.Parameters.AddWithValue("@DefaultIntInterval", IIf(rdbInterestInterval.SelectedIndex = -1, DBNull.Value, rdbInterestInterval.SelectedValue))
                        cmd.Parameters.AddWithValue("@IntCalcMethod", IIf(rdbInterestCalcMethod.SelectedIndex = -1, DBNull.Value, rdbInterestCalcMethod.SelectedValue))
                        cmd.Parameters.AddWithValue("@IntTrigger", IIf(rdbInterestTrigger.SelectedIndex = -1, DBNull.Value, rdbInterestTrigger.SelectedValue))
                        cmd.Parameters.AddWithValue("@DaysInYear", IIf(rdbYearDays.SelectedIndex = -1, DBNull.Value, rdbYearDays.SelectedValue))
                        'cmd.Parameters.AddWithValue("@RepaymentFreq", IIf(rdbRepayFreq.SelectedIndex = -1, DBNull.Value, rdbRepayFreq.SelectedValue))
                        cmd.Parameters.AddWithValue("@HasGracePeriod", IIf(rdbGracePeriod.SelectedIndex = -1, DBNull.Value, rdbGracePeriod.SelectedValue))
                        cmd.Parameters.AddWithValue("@GracePeriodType", IIf(rdbGracePeriodType.SelectedIndex = -1, DBNull.Value, rdbGracePeriodType.SelectedValue))
                        cmd.Parameters.AddWithValue("@GracePeriodLength", IIf(Trim(txtGracePerLength.Text) = "", DBNull.Value, txtGracePerLength.Text))
                        cmd.Parameters.AddWithValue("@GracePeriodUnit", cmbGracePerLength.SelectedValue)
                        cmd.Parameters.AddWithValue("@AllowRepaymentOnWknd", IIf(rdbRepayWknd.SelectedIndex = -1, DBNull.Value, rdbRepayWknd.SelectedValue))
                        cmd.Parameters.AddWithValue("@IfRepaymentFallsOnWknd", IIf(rdbRepaymentWknd.SelectedIndex = -1, DBNull.Value, rdbRepaymentWknd.SelectedValue))
                        cmd.Parameters.AddWithValue("@AllowEditingPaymentSchedule", IIf(rdbEditPaySchedule.SelectedIndex = -1, DBNull.Value, rdbEditPaySchedule.SelectedValue))
                        cmd.Parameters.AddWithValue("@RepayOrder1", cmbRepaymentAllocationOrder1.SelectedValue)
                        cmd.Parameters.AddWithValue("@RepayOrder2", cmbRepaymentAllocationOrder2.SelectedValue)
                        cmd.Parameters.AddWithValue("@RepayOrder3", cmbRepaymentAllocationOrder3.SelectedValue)
                        cmd.Parameters.AddWithValue("@RepayOrder4", cmbRepaymentAllocationOrder4.SelectedValue)
                        cmd.Parameters.AddWithValue("@TolerancePeriodNum", IIf(txtTolerancePeriod.Text = "", DBNull.Value, txtTolerancePeriod.Text))
                        cmd.Parameters.AddWithValue("@TolerancePeriodUnit", cmbTolerancePeriod.SelectedValue)
                        cmd.Parameters.AddWithValue("@ArrearNonWorkingDays", IIf(rdbArrearNonWorking.SelectedIndex = -1, DBNull.Value, rdbArrearNonWorking.SelectedValue))
                        cmd.Parameters.AddWithValue("@PenaltyCharged", IIf(rdbPenaltyCharged.SelectedIndex = -1, DBNull.Value, rdbPenaltyCharged.SelectedValue))
                        cmd.Parameters.AddWithValue("@PenaltyOption", IIf(rdbPenaltyOption.SelectedIndex = -1, DBNull.Value, rdbPenaltyOption.SelectedValue))
                        cmd.Parameters.AddWithValue("@AmtToPenalise", IIf(rdbAmtToPenalise.SelectedIndex = -1, DBNull.Value, rdbAmtToPenalise.SelectedValue))
                        cmd.Parameters.AddWithValue("@ProductFees", cmbProductFees.SelectedValue)
                        cmd.Parameters.AddWithValue("@ProductFeeCalc", IIf(rdbProductFeeCalc.SelectedIndex = -1, DBNull.Value, rdbProductFeeCalc.SelectedValue))
                        cmd.Parameters.AddWithValue("@ProductFeeAmtPerc", IIf(txtProductFee.Text = "", DBNull.Value, txtProductFee.Text))
                        cmd.Parameters.AddWithValue("@User", Session("UserId"))
                        cmd.Parameters.AddWithValue("@RepaymentIntervalNum", txtRepaymentInterval.Text)
                        cmd.Parameters.AddWithValue("@RepaymentIntervalUnit", cmbRepaymentInterval.SelectedValue)
                        cmd.Parameters.AddWithValue("@MinimumTenure", txtMinimumTenure.Text)
                        cmd.Parameters.AddWithValue("@MaximumTenure", txtMaximumTenure.Text)
                        cmd.Parameters.AddWithValue("@DefaultTenure", txtDefaultTenure.Text)
                        cmd.Parameters.AddWithValue("@PenaltyRate", txtPenaltyRate.Text)
                        cmd.Parameters.AddWithValue("@PenaltyInterval", cmbPenaltyInterval.SelectedValue)
                        cmd.Parameters.AddWithValue("@DateCreated", txtCreatedDate.Text)
                        cmd.Parameters.AddWithValue("@Active", chkActive.Checked)
                        con.Open()
                        If cmd.ExecuteNonQuery() Then
                            notify("Product type successfully saved", "success")
                            clearAll()
                            getProducts()
                        Else
                            notify("Error creating product type", "error")
                        End If
                        con.Close()
                    End Using
                End Using
            End If
        Catch ex As Exception
            WriteLogFile(Session("UserId"), Request.Url.ToString & " --- btnAddProductType_Click()", ex.ToString)
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub clearAll()
        'Try
        '    txtShortName.Text = ""
        '    txtLongName.Text = ""
        'Catch ex As Exception
        '    msgbox(ex.Message)
        'End Try
    End Sub

    Protected Sub getProducts()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd As New SqlCommand("Select a.ID,Product as [Product Type],DisplayName as [Display Name],'ZMW' +''+ CONVERT(varchar(12),MinAmt, 1) as [Minimum Amount],'ZMW' + ''+ CONVERT(varchar(12),MaxAmt, 1) as [Maximum Amount] FROM [CreditProducts] a join [ParaProductTypes] b on a.ProductType=convert(varchar,b.id)")
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    Dim ds As New DataSet
                    Dim adp As New SqlDataAdapter(cmd)
                    adp.Fill(ds, "FUN")
                    bindGrid(ds.Tables(0), grdProductTypes)
                End Using
            End Using
        Catch ex As Exception
            WriteLogFile(Session("UserId"), Request.Url.ToString & " --- getProducts()", ex.Message)
        End Try
    End Sub

    Protected Sub loadProductData(productID As String)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd As New SqlCommand("Select *,convert(varchar,DateCreated,106) as DateCreated1 FROM [CreditProducts] where [id]='" & productID & "'")
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    Dim ds As New DataSet
                    Dim adp As New SqlDataAdapter(cmd)
                    adp.Fill(ds, "FUN")
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim dr = ds.Tables(0).Rows(0)
                        txtDisplayName.Text = dr("DisplayName")
                        Try
                            txtMinAmt.Text = FormatCurrency(dr("MinAmt"))
                        Catch ex As Exception
                            txtMinAmt.Text = ""
                        End Try
                        Try
                            txtMaxAmt.Text = FormatCurrency(dr("MaxAmt"))
                        Catch ex As Exception
                            txtMaxAmt.Text = ""
                        End Try
                        Try
                            txtMinimumIntRate.Text = FormatNumber(dr("MinIntRate"))
                        Catch ex As Exception
                            txtMinimumIntRate.Text = ""
                        End Try
                        Try
                            txtMaximumIntRate.Text = FormatNumber(dr("MaxIntRate"))
                        Catch ex As Exception
                            txtMaximumIntRate.Text = ""
                        End Try
                        Try
                            txtDefaultInterestRate.Text = FormatNumber(dr("DefaultIntRate"))
                        Catch ex As Exception
                            txtDefaultInterestRate.Text = ""
                        End Try
                        Try
                            rdbInterestInterval.SelectedValue = dr("DefaultIntInterval")
                        Catch ex As Exception
                            rdbInterestInterval.ClearSelection()
                        End Try
                        Try
                            rdbInterestCalcMethod.SelectedValue = dr("IntCalcMethod")
                        Catch ex As Exception
                            rdbInterestCalcMethod.ClearSelection()
                        End Try
                        Try
                            rdbInterestTrigger.SelectedValue = dr("IntTrigger")
                        Catch ex As Exception
                            rdbInterestTrigger.ClearSelection()
                        End Try
                        Try
                            rdbYearDays.SelectedValue = dr("DaysInYear")
                        Catch ex As Exception
                            rdbYearDays.ClearSelection()
                        End Try
                        Try
                            txtRepaymentInterval.Text = dr("RepaymentIntervalNum")
                        Catch ex As Exception
                            txtRepaymentInterval.Text = ""
                        End Try

                        Try
                            cmbRepaymentInterval.SelectedValue = dr("RepaymentIntervalUnit")
                        Catch ex As Exception
                            cmbRepaymentInterval.ClearSelection()
                        End Try
                        Try
                            rdbGracePeriod.SelectedValue = dr("HasGracePeriod")
                        Catch ex As Exception
                            rdbGracePeriod.ClearSelection()
                        End Try
                        Try
                            rdbGracePeriodType.SelectedValue = dr("GracePeriodType")
                        Catch ex As Exception
                            rdbGracePeriodType.ClearSelection()
                        End Try
                        Try
                            txtGracePerLength.Text = dr("GracePeriodLength")
                        Catch ex As Exception
                            txtGracePerLength.Text = ""
                        End Try
                        Try
                            cmbProductType.SelectedValue = dr("ProductType")
                        Catch ex As Exception
                            cmbProductType.ClearSelection()
                        End Try
                        Try
                            cmbGracePerLength.SelectedValue = dr("GracePeriodUnit")
                        Catch ex As Exception
                            cmbGracePerLength.ClearSelection()
                        End Try
                        Try
                            rdbRepayWknd.SelectedValue = dr("AllowRepaymentOnWknd")
                        Catch ex As Exception
                            rdbRepayWknd.ClearSelection()
                        End Try
                        Try
                            rdbRepaymentWknd.SelectedValue = dr("IfRepaymentFallsOnWknd")
                        Catch ex As Exception
                            rdbRepaymentWknd.ClearSelection()
                        End Try
                        Try
                            rdbEditPaySchedule.SelectedValue = dr("AllowEditingPaymentSchedule")
                        Catch ex As Exception
                            rdbEditPaySchedule.ClearSelection()
                        End Try
                        Try
                            cmbRepaymentAllocationOrder1.SelectedValue = dr("RepayOrder1")
                        Catch ex As Exception
                            cmbRepaymentAllocationOrder1.ClearSelection()
                        End Try
                        Try
                            cmbRepaymentAllocationOrder2.SelectedValue = dr("RepayOrder2")
                        Catch ex As Exception
                            cmbRepaymentAllocationOrder2.ClearSelection()
                        End Try
                        Try
                            cmbRepaymentAllocationOrder3.SelectedValue = dr("RepayOrder3")
                        Catch ex As Exception
                            cmbRepaymentAllocationOrder3.ClearSelection()
                        End Try
                        Try
                            cmbRepaymentAllocationOrder4.SelectedValue = dr("RepayOrder4")
                        Catch ex As Exception
                            cmbRepaymentAllocationOrder4.ClearSelection()
                        End Try
                        Try
                            txtTolerancePeriod.Text = dr("TolerancePeriodNum")
                        Catch ex As Exception
                            txtTolerancePeriod.Text = ""
                        End Try
                        Try
                            cmbTolerancePeriod.SelectedValue = dr("TolerancePeriodUnit")
                        Catch ex As Exception
                            cmbTolerancePeriod.ClearSelection()
                        End Try
                        Try
                            rdbArrearNonWorking.SelectedValue = dr("ArrearNonWorkingDays")
                        Catch ex As Exception
                            rdbArrearNonWorking.ClearSelection()
                        End Try
                        Try
                            rdbPenaltyCharged.SelectedValue = dr("PenaltyCharged")
                        Catch ex As Exception
                            rdbPenaltyCharged.ClearSelection()
                        End Try
                        Try
                            rdbPenaltyOption.SelectedValue = dr("PenaltyOption")
                        Catch ex As Exception
                            rdbPenaltyOption.ClearSelection()
                        End Try
                        Try
                            rdbAmtToPenalise.SelectedValue = dr("AmtToPenalise")
                        Catch ex As Exception
                            rdbAmtToPenalise.ClearSelection()
                        End Try
                        Try
                            cmbProductFees.SelectedValue = dr("ProductFees")
                        Catch ex As Exception
                            cmbProductFees.ClearSelection()
                        End Try
                        Try
                            rdbProductFeeCalc.SelectedValue = dr("ProductFeeCalc")
                        Catch ex As Exception
                            rdbProductFeeCalc.ClearSelection()
                        End Try
                        Try
                            txtProductFee.Text = FormatNumber(dr("ProductFeeAmtPerc"), 2)
                        Catch ex As Exception
                            txtProductFee.Text = ""
                        End Try
                        Try
                            txtMinimumTenure.Text = FormatNumber(dr("MinimumTenure"), 0)
                        Catch ex As Exception
                            txtMinimumTenure.Text = ""
                        End Try
                        Try
                            txtMaximumTenure.Text = FormatNumber(dr("MaximumTenure"), 0)
                        Catch ex As Exception
                            txtMaximumTenure.Text = ""
                        End Try
                        Try
                            txtDefaultTenure.Text = FormatNumber(dr("DefaultTenure"), 0)
                        Catch ex As Exception
                            txtDefaultTenure.Text = ""
                        End Try
                        Try
                            txtPenaltyRate.Text = FormatNumber(dr("PenaltyRate"), 2)
                        Catch ex As Exception
                            txtPenaltyRate.Text = ""
                        End Try
                        Try
                            cmbPenaltyInterval.SelectedValue = dr("PenaltyInterval")
                        Catch ex As Exception
                            cmbPenaltyInterval.ClearSelection()
                        End Try
                        Try
                            txtCreatedDate.Text = dr("DateCreated1")
                        Catch ex As Exception
                            txtCreatedDate.Text = ""
                        End Try
                        Try
                            chkActive.Checked = dr("Active")
                        Catch ex As Exception
                            chkActive.Checked = False
                        End Try
                    End If
                End Using
            End Using
        Catch ex As Exception
            WriteLogFile(Session("UserId"), Request.Url.ToString & " --- loadProductData()", ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.MaintainScrollPositionOnPostBack = True
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            If Not IsPostBack Then
                loadParaProductType(cmbProductType)
                getProducts()
                ViewState("grdID") = 0
            End If
        Catch ex As Exception
            WriteLogFile(Session("UserId"), Request.Url.ToString & " --- Page_Load()", ex.Message)
        End Try
    End Sub

    Private Sub grdProductTypes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdProductTypes.SelectedIndexChanged
        Dim prodId = grdProductTypes.SelectedDataKey.Value.ToString
        loadProductData(prodId)
        ViewState("grdID") = prodId
        btnAddProductType.Text = "Update"
    End Sub
End Class