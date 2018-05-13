Public Class frmLoadBankInfo
    Private SqlCon As SQL
    Private Temp(11) As String
    Private Counter, Counter2 As Integer
    Private Start As Boolean

    Private Sub btnRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRead.Click
        If Me.cboAccountNo.Text <> "" Then
            Start = False
            Counter = 0
            Counter2 = 0

            Me.btnRead.Enabled = False
            For Each TblElement As System.Windows.Forms.HtmlElement In Me.WebBrowser1.Document.All
                Dim Taghi As String = TblElement.InnerText
                If TblElement.InnerText = "مانده" And Start = False Then Start = True

                MessageBox.Show(TblElement.InnerText)

                If UCase(TblElement.TagName.ToString).Contains("TD") And Counter < 12 And Start = True Then
                    Temp(Counter) = TblElement.InnerText
                    Counter += 1
                ElseIf Counter = 11 Then
                    If Temp(0) <> "مانده" And Start = True Then
                        ' SaveToBank(Temp(0), Temp(1), Temp(2), Temp(3), Temp(4), Temp(5), Temp(6), Temp(7), Temp(8), Temp(9))
                        Counter2 += 1
                    End If
                    Counter = 0
                End If
            Next
            MessageBox.Show("کلیه اطلاعات با موفقیت خوانده و ثبت گردید" & vbCrLf & "تعداد رکوردهای ثبت شده " & Counter2 & " می باشد ", "ثبت اطلاعات در بانک داده", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Me.btnRead.Enabled = True
        Else
            MessageBox.Show("لطفاً شماره حساب را انتخاب کنید")
        End If

    End Sub

    Private Sub SaveToBank(ByVal Mande As String, ByVal Mablagh As String, ByVal Sharh As String, ByVal VarizKonnade As String, ByVal Serial As String, ByVal Shenase As String, ByVal CodeHesab As String, ByVal Shobe As String, ByVal Zaman As String, ByVal Tarikh As String)

        SqlCon = New SQL
        Try


            'Mande = Mande.Replace(",", "")
            'Mablagh = Mablagh.Replace(",", "")
            Dim cmd4 As New Data.SqlClient.SqlCommand("Insert Into BankMellat (Mande,Mablagh,Sharh,VarizKonnade,Serial,Shenase,CodeHesab,Shobe,Zaman,Tarikh,AccountNO) Values (N'" & Mande & "',N'" & Mablagh & "',N'" & Sharh & "',N'" & VarizKonnade & "',N'" & Serial & "',N'" & Shenase & "',N'" & CodeHesab & "',N'" & Shobe & "',N'" & Zaman & "',N'" & Tarikh & "',N'" & Me.cboAccountNo.Text & "')", SqlCon.SqlCon)
            SqlCon.SqlCon.Open()
            cmd4.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            SqlCon.SqlCon.Close()
        End Try

    End Sub


    Public Shared Function TranslateNumerals(ByVal sIn As String) As String

        Dim enc As New System.Text.UTF8Encoding

        Dim utf8Decoder As System.Text.Decoder

        utf8Decoder = enc.GetDecoder

        Dim sTranslated = New System.Text.StringBuilder

        Dim cTransChar(1) As Char

        Dim bytes() As Byte = {217, 160}

        ' Start Converting characters into Arabic mode. 

        Dim aChars() As Char = sIn.ToCharArray

        For Each c As Char In aChars

            If Char.IsDigit(c) Then

                bytes(1) = 160 + CInt(Char.GetNumericValue(c))

                utf8Decoder.GetChars(bytes, 0, 2, cTransChar, 0)

                sTranslated.Append(cTransChar(0))

            Else

                sTranslated.Append(c)

            End If

        Next

        TranslateNumerals = sTranslated.ToString

    End Function


    Private Sub OpenDLG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenDLG.Click
        OpenFileDialog1.Title = "Please Select a File"
        OpenFileDialog1.InitialDirectory = "C:temp"
        OpenFileDialog1.ShowDialog()

        Me.WebBrowser1.Navigate(Me.OpenFileDialog1.FileName.ToString)


    End Sub

    Private Shared Function ArabicToWestern(ByVal input As String) As String
        Dim western As System.Text.StringBuilder = New System.Text.StringBuilder
        For Each num As Char In input
            western.Append(Char.GetNumericValue(num))
        Next
        Return western.ToString
    End Function



End Class

