Public Class frmViewBankInfo
    Private SqlCon As New SQL
    Private Cmd As New SqlClient.SqlCommand
    Private Sdr As SqlClient.SqlDataReader
    Private tbl As New DataTable
    Private dvw As DataView
    Private b As Boolean


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Cmd.Connection = SqlCon.SqlCon
            tbl.Clear()
            Cmd.CommandText = "Select Mande as 'مانده حساب',Mablagh as 'مبلغ گردش',Sharh as 'شرح',VarizKonnade as 'واریز کننده/ذینفع',Serial as 'شماره سریال',Shenase as 'شناسه واریز کننده',CodeHesab as 'کد حسابگری',Shobe as 'شعبه',Zaman as 'زمان',Tarikh as 'تاریخ',AccountNo as 'شماره حساب مقصد' from bankMellat Where Varizkonnade Like N'%" & GetArabicNumber(Me.txtAccountNo.Text) & "%'"
            SqlCon.SqlCon.Open()
            Sdr = Cmd.ExecuteReader
            Dim fc As Integer
            While (Sdr.Read)
                'populating columns
                If Not b Then
                    For fc = 0 To Sdr.FieldCount - 1
                        tbl.Columns.Add(Sdr.GetName(fc))
                    Next
                    b = True
                End If
                'populating rows
                Dim row As DataRow = tbl.NewRow

                For fc = 0 To Sdr.FieldCount - 1
                    row(fc) = Sdr(fc)

                Next
                tbl.Rows.Add(row)
            End While
            dvw = New DataView(tbl)
            Me.DataGridView1.DataSource = dvw
        Catch ex As Exception

        Finally
            SqlCon.SqlCon.Close()
        End Try
    End Sub

    Private Function GetArabicNumber(ByVal Number As String)
        Dim Value As String = Number
        Value = Value.Replace("0", "۰")
        Value = Value.Replace("1", "۱")
        Value = Value.Replace("2", "۲")
        Value = Value.Replace("3", "۳")
        Value = Value.Replace("4", "۴")
        Value = Value.Replace("5", "۵")
        Value = Value.Replace("6", "۶")
        Value = Value.Replace("7", "۷")
        Value = Value.Replace("8", "۸")
        Value = Value.Replace("9", "۹")
        Return Value
    End Function


End Class