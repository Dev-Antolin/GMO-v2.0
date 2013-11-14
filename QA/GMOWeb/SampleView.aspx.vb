Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports clsGMOUser
Imports System.Drawing
Imports Interop.PDFCreatorPilotLib
Imports Interop.HTML2PDFAddOn
Imports Interop
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Xml
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports System.Drawing.Imaging

Partial Class SampleView
    Inherits System.Web.UI.Page
    Dim Pics As String = ""
    Dim dsp As String = ""
    Dim que As New Queue
    Dim i As Integer = 0
    Dim s As String
    Dim r As String
    Dim p As Integer
    Dim Sepa() As String = Nothing
    Dim sFileServerVis As String = "~/MemoImageVismin/"
    Dim sFileServerLuz As String = "~/MemoImageLuzon/"
    Dim sFilePDFVis As String = System.Web.HttpContext.Current.Server.MapPath("") & "\MemoPDFVismin\"
    Dim sFilePDFLuz As String = System.Web.HttpContext.Current.Server.MapPath("") & "\MemoPDFLuzon\"
    Dim sFileDirVis As String = System.Web.HttpContext.Current.Server.MapPath("") & "\MemoImageVismin\"
    Dim sFileDirLuz As String = System.Web.HttpContext.Current.Server.MapPath("") & "\MemoImageLuzon\"
    Dim fileName As String
    Dim Fpdf As String = ""
    Dim trans As String = ""
    Dim FTrans As String = ""
    Dim pdfLoc As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Dim DS As New DataSet
            DS = Select_Recipient()
            If Not DS Is Nothing Then
                Pics = DS.Tables(0).Rows(0).Item(0).ToString
                Sepa = Pics.Split(",")
                For Each Me.r In Sepa
                    s = (Me.r)
                    que.Enqueue(s)
                Next r
            End If
            'dsp = que.Peek.Trim.Replace("E:\Back up\Silvher's Vb.net 08\GMOWeb\MemoImageVismin\", "~/MemoImageVismin/")
            dsp = que.Peek.Trim.Replace("C:\GMOWeb\MemoImageVismin\", "~/MemoImageVismin/")
            imgDisplayMe.ImageUrl = dsp.Trim
            'Update_ReceivedMemo()
            If Me.Session("Plus") = 0 Then
                btnPrev.Enabled = False
            End If
            If i > que.Count Then
                btnNext.Enabled = False
            ElseIf i + 1 >= que.Count Then
                btnNext.Enabled = False
            Else
                btnNext.Enabled = True
            End If
            lblCountNumber.Text = i.ToString + 1 & " of " & que.Count
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Dim DS As New DataSet
            DS = Select_Recipient()
            If Not DS Is Nothing Then
                Pics = DS.Tables(0).Rows(0).Item(0).ToString
                Sepa = Pics.Split(",")
                For Each Me.r In Sepa
                    s = (Me.r)
                    que.Enqueue(s)
                Next r
            End If
            'dsp = que.Peek.Trim.Replace("E:\Back up\Silvher's Vb.net 08\GMOWeb\MemoImageLuzon\", "~/MemoImageLuzon/")
            dsp = que.Peek.Trim.Replace("C:\GMOWeb\MemoImageLuzon\", "~/MemoImageLuzon/")
            imgDisplayMe.ImageUrl = dsp.Trim
            'Update_ReceivedMemo()
            If Me.Session("Plus") = 0 Then
                btnPrev.Enabled = False
            End If
            If i > que.Count Then
                btnNext.Enabled = False
            ElseIf i + 1 >= que.Count Then
                btnNext.Enabled = False
            Else
                btnNext.Enabled = True
            End If
            lblCountNumber.Text = i.ToString + 1 & " of " & que.Count
        End If
    End Sub
    Private Sub CheckLogin()
        If Me.Session("_fullname") = "" Then
            Response.Redirect("Login.aspx")
        End If
    End Sub
    Public Function Select_Recipient() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()
        Try
            Dim y As String
            If Trim(Me.Session("_zonecode")) = "VISMIN" Then
                y = "select memopath from recipientslistcircular where MemoFrom ='GM''s OFFICE' and subject='" + Trim(Me.Session("_Subject")).Replace("'", "''").Replace("&#241;", "ñ") + "' and CircularNumber='" + Trim(Me.Session("_Circular")) + "' and ZoneCode = 'VISMIN'"
                ds = Execute_Dataset(y)
            End If
            If Trim(Me.Session("_zonecode")) = "LUZON" Then
                y = "select memopath from recipientslistcircular where MemoFrom ='GM''s OFFICE' and subject='" + Trim(Me.Session("_Subject")).Replace("'", "''").Replace("&#241;", "ñ") + "' and CircularNumber='" + Trim(Me.Session("_Circular")) + "' and ZoneCode = 'LUZON'"
                ds = Execute_Dataset(y)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Con.Close()
        Com.Dispose()
        Return ds
    End Function
    Public Function Execute_Dataset(ByVal as_sql As String) As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim sqlConn As New SqlConnection
        Dim sqlAdapter As SqlDataAdapter
        Dim sqlDataset As New DataSet
        Execute_Dataset = Nothing
        Try
            Try
                Con.ConnectionString = constr2
                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If
            Catch
            End Try
            sqlAdapter = New SqlDataAdapter(as_sql, Con)
            sqlAdapter.Fill(sqlDataset)
            If Not sqlDataset Is Nothing Then
                If sqlDataset.Tables(0).Rows.Count <> 0 Then
                    Execute_Dataset = sqlDataset
                    sqlDataset.Dispose()
                    sqlAdapter.Dispose()
                End If
            End If
            Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Con.Close()
            Com.Dispose()
        End Try
        Con.Close()
        Com.Dispose()
    End Function
    Public Function Select_Trxnumber() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        Try
            Dim y As String
            If Trim(Me.Session("_task")) = "REGIONAL MAN" Or Trim(Me.Session("_task")) = "Regional Man" Then
                y = "select distinct  mr.region, mr.trxnumber from memoregion as mr inner join recipientslist as rl on mr.trxnumber = rl.trxnumber where rl.CircularNumber = '" + Trim(Me.Session("_Circular")) + "' and rl.subject = '" + Trim(Me.Session("_Subject")).Replace("'", "''") + "' and mr.region = '" + Trim(Me.Session("_fullname")) + "'"
                ds = Execute_Dataset(y)
            End If
            If Trim(Me.Session("_task")) = "AREA MANAGER" Or Trim(Me.Session("_task")) = "Area Manager" Then
                y = "select distinct  ma.area, ma.trxnumber from memoarea as ma inner join recipientslist as rl on ma.trxnumber = rl.trxnumber where rl.CircularNumber = '" + Trim(Me.Session("_Circular")) + "' and rl.subject = '" + Trim(Me.Session("_Subject")).Replace("'", "''") + "' and ma.area = '" + Trim(Me.Session("_fullname")) + "'"
                ds = Execute_Dataset(y)
            End If
            If Trim(Me.Session("_task")) = "BM/BOSMAN" Or Trim(Me.Session("_task")) = "Bm/Bosman" Then
                y = "select distinct  mb.branch, mb.trxnumber from memobranch as mb inner join recipientslist as rl on mb.trxnumber = rl.trxnumber where rl.CircularNumber = '" + Trim(Me.Session("_Circular")) + "' and rl.subject = '" + Trim(Me.Session("_Subject")).Replace("'", "''") + "' and mb.branch = '" + Trim(Me.Session("_fullname")) + "'"
                ds = Execute_Dataset(y)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Con.Close()
            Com.Dispose()
        End Try
        Con.Close()
        Com.Dispose()
        Return ds
    End Function
    Public Function Update_ReceivedMemo() As DataSet
        Dim oCom As New SqlCommand
        Dim oTrans As SqlTransaction
        Dim Con As New SqlConnection
        Dim TN As String = ""
        Dim RN As String = ""
        Dim AN As String = ""
        Dim BN As String = ""
        Dim ds As New DataSet
        Dim CMemo As String = ""
        CMemo = Format(Date.Now, "yyyy-MM-dd hh:mm:ss tt")

        If Trim(Me.Session("_task")) = "REGIONAL MAN" Or Trim(Me.Session("_task")) = "Regional Man" Then
            ds = Select_Trxnumber()
            If Not ds Is Nothing Then
                RN = ds.Tables(0).Rows(0).Item(0).ToString
                TN = ds.Tables(0).Rows(0).Item(1).ToString
            End If

            Dim R As String = "update memoRegion set confirmedReceived='1', confirmedDate='" + CMemo.Trim + "' where region = '" + RN.Trim + "' and trxnumber = '" + TN.Trim + "'"

            Con.ConnectionString = constr2
            If Con.State = ConnectionState.Closed Then
                Con.Open()
                oTrans = Con.BeginTransaction()
                oCom = Con.CreateCommand
                oCom.CommandTimeout = 0
                oCom.Transaction = oTrans
                Try
                    oCom.CommandText = R
                    oCom.ExecuteNonQuery()
                    oTrans.Commit()
                    oCom.Dispose()
                Catch ex As Exception
                    MsgBox(ex.Message())
                    oTrans.Rollback()
                    Con.Close()
                    oCom.Dispose()
                Finally
                    Con.Close()
                    oCom.Dispose()
                End Try
            End If
        End If

        If Trim(Me.Session("_task")) = "AREA MANAGER" Or Trim(Me.Session("_task")) = "Area Manager" Then
            ds = Select_Trxnumber()
            If Not ds Is Nothing Then
                AN = ds.Tables(0).Rows(0).Item(0).ToString
                TN = ds.Tables(0).Rows(0).Item(1).ToString
            End If

            Dim A As String = "update memoArea set confirmedReceived='1', confirmedDate='" + CMemo.Trim + "' where area = '" + AN.Trim + "' and trxnumber = '" + TN.Trim + "'"

            Con.ConnectionString = constr2
            If Con.State = ConnectionState.Closed Then
                Con.Open()
                oTrans = Con.BeginTransaction()
                oCom = Con.CreateCommand
                oCom.CommandTimeout = 0
                oCom.Transaction = oTrans
                Try
                    oCom.CommandText = A
                    oCom.ExecuteNonQuery()
                    oTrans.Commit()
                    oCom.Dispose()
                Catch ex As Exception
                    MsgBox(ex.Message())
                    oTrans.Rollback()
                    Con.Close()
                    oCom.Dispose()
                Finally
                    Con.Close()
                End Try
            End If
        End If

        If Trim(Me.Session("_task")) = "BM/BOSMAN" Or Trim(Me.Session("_task")) = "Bm/Bosman" Then
            ds = Select_Trxnumber()
            If Not ds Is Nothing Then
                BN = ds.Tables(0).Rows(0).Item(0).ToString
                TN = ds.Tables(0).Rows(0).Item(1).ToString
            End If

            Dim B As String = "update memoBranch set confirmedReceived='1', confirmedDate='" + CMemo.Trim + "' where branch = '" + BN.Trim + "' and trxnumber = '" + TN.Trim + "'"

            Con.ConnectionString = constr2
            If Con.State = ConnectionState.Closed Then
                Con.Open()
                oTrans = Con.BeginTransaction()
                oCom = Con.CreateCommand
                oCom.CommandTimeout = 0
                oCom.Transaction = oTrans
                Try
                    oCom.CommandText = B
                    oCom.ExecuteNonQuery()
                    oTrans.Commit()
                    oCom.Dispose()
                Catch ex As Exception
                    MsgBox(ex.Message())
                    oTrans.Rollback()
                    Con.Close()
                    oCom.Dispose()
                Finally
                    Con.Close()
                    oCom.Dispose()
                End Try
            End If
        End If

        Return ds
    End Function

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("GMOMemorandumDetail.aspx")
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        ConvertFile()
    End Sub
    Private Sub DownloadFile(ByVal fname As String, ByVal forceDownload As Boolean)
        Dim path__1 As String = MapPath(fname)
        Dim name As String = Path.GetFileName(path__1)
        Dim ext As String = Path.GetExtension(path__1)
        Dim type As String = ""
        ' set known types based on file extension 
        If ext IsNot Nothing Then
            Select Case ext.ToLower()
                Case ".pdf"
                    type = "Application/pdf"
                    Exit Select
            End Select
            'Select Case ext.ToLower()
            '    Case ".jpg", ".jpeg"
            '        type = "image/jpeg"
            '        Exit Select
            'End Select
        End If
        If forceDownload Then
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" & name)
        End If
        If type <> "" Then
            Response.ContentType = type
        End If
        Response.WriteFile(path__1)
        Response.End()
    End Sub
    Private Sub ConvertImageBytesToPDF(ByVal ImageBytes As Byte(), ByVal FileName As String)
        Dim PDF As PDFCreatorPilotLib.PDFDocument4
        Dim ImageIndex As Integer
        Dim bm As Bitmap

        PDF = StartDoc()
        PDF.PageResolution = 205
        'PDF.PageResolution = 80
        ImageIndex = PDF.AddImageFromBLOB(CType(ImageBytes, Object))
        bm = New Bitmap(New System.IO.MemoryStream(ImageBytes))
        PDF.GetImageWidth(ImageIndex)
        PDF.GetImageHeight(ImageIndex)
        PDF.GetImageResolution(200)
        PDF.DrawImage(ImageIndex, 0, 0, bm.Width, bm.Height, 0)

        SavePDFDoc(PDF, FileName)
    End Sub
    Private Function StartDoc() As PDFCreatorPilotLib.PDFDocument4
        Dim PDF As PDFCreatorPilotLib.PDFDocument4
        PDF = New PDFCreatorPilotLib.PDFDocument4
        PDF.SetLicenseData("", "")
        Return PDF
    End Function

    Private Sub SavePDFDoc(ByVal PDF As PDFCreatorPilotLib.PDFDocument4, ByVal FileName As String)
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            PDF.SaveToFile("E:\Back up\Silvher's Vb.net 08\GMOWeb\MemoImageVismin\" & FileName, False)
            'PDF.SaveToFile("C:\GMOWeb\MemoImageVismin\" & FileName, False)
            PDF.SaveToFile(sDest2 & FileName, False)
            PDF = Nothing
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            PDF.SaveToFile("E:\Back up\Silvher's Vb.net 08\GMOWeb\MemoImageLuzon\" & FileName, False)
            'PDF.SaveToFile("C:\GMOWeb\MemoImageLuzon\" & FileName, False)
            PDF.SaveToFile(sDest1 & FileName, False)
            PDF = Nothing
        End If
    End Sub
    Private Function GetImageBytes(ByVal FileName As String) As Byte()
        Dim fs As System.IO.FileStream
        Dim br As System.IO.BinaryReader
        ' You can use here any stream - for example, memory stream
        ' where image data generated from some another function
        fs = New System.IO.FileStream(FileName, IO.FileMode.Open, IO.FileAccess.Read)
        br = New System.IO.BinaryReader(fs)
        Dim bytes As Byte() = br.ReadBytes(fs.Length)
        Return bytes
    End Function

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Dim Ini As String = ""
            If Me.Session("Plus") < que.Count Then
                i = Me.Session("Plus")
                i = i + 1
                If i = que.Count - 1 Then
                    btnNext.Enabled = False
                End If
                'Ini = que(i).ToString.Trim.Replace("E:\Back up\Silvher's Vb.net 08\GMOWeb\MemoImageVismin\", "~/MemoImageVismin/")
                Ini = que(i).ToString.Trim.Replace("C:\GMOWeb\MemoImageVismin\", "~/MemoImageVismin/")
                imgDisplayMe.ImageUrl = Ini.Trim
                Me.Session.Add("Plus", i)
                lblCountNumber.Text = i.ToString + 1 & " of " & que.Count
                btnPrev.Enabled = True
            End If
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Dim Ini As String = ""
            If Me.Session("Plus") < que.Count Then
                i = Me.Session("Plus")
                i = i + 1
                If i = que.Count - 1 Then
                    btnNext.Enabled = False
                End If
                'Ini = que(i).ToString.Trim.Replace("E:\Back up\Silvher's Vb.net 08\GMOWeb\MemoImageLuzon\", "~/MemoImageLuzon/")
                Ini = que(i).ToString.Trim.Replace("C:\GMOWeb\MemoImageLuzon\", "~/MemoImageLuzon/")
                imgDisplayMe.ImageUrl = Ini.Trim
                Me.Session.Add("Plus", i)
                lblCountNumber.Text = i.ToString + 1 & " of " & que.Count
                btnPrev.Enabled = True
            End If
        End If
    End Sub

    Protected Sub btnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Dim tin As String = ""
            If Me.Session("Plus") < que.Count Then
                Me.Session("Plus") = Me.Session("Plus") - 1
                If Me.Session("Plus") = 0 Then
                    btnPrev.Enabled = False
                    btnNext.Enabled = True
                End If
                'tin = que(Me.Session("Plus")).Trim.Replace("E:\Back up\Silvher's Vb.net 08\GMOWeb\MemoImageVismin\", "~/MemoImageVismin/")
                tin = que(Me.Session("Plus")).Trim.Replace("C:\GMOWeb\MemoImageVismin\", "~/MemoImageVismin/")
                imgDisplayMe.ImageUrl = tin.Trim
                lblCountNumber.Text = Me.Session("Plus") + 1 & " of " & que.Count
            End If
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Dim tin As String = ""
            If Me.Session("Plus") < que.Count Then
                Me.Session("Plus") = Me.Session("Plus") - 1
                If Me.Session("Plus") = 0 Then
                    btnPrev.Enabled = False
                    btnNext.Enabled = True
                End If
                'tin = que(Me.Session("Plus")).Trim.Replace("E:\Back up\Silvher's Vb.net 08\GMOWeb\MemoImageLuzon\", "~/MemoImageLuzon/")
                tin = que(Me.Session("Plus")).Trim.Replace("C:\GMOWeb\MemoImageLuzon\", "~/MemoImageLuzon/")
                imgDisplayMe.ImageUrl = tin.Trim
                lblCountNumber.Text = Me.Session("Plus") + 1 & " of " & que.Count
            End If
        End If
    End Sub
    Public Sub ConvertFile()
        Dim doc As New Document(iTextSharp.text.PageSize.LETTER, 42, 45, 52, 45)
        Dim writer As PdfCopy = Nothing
        Dim NSeries As String = ""
        Try
            For Each Me.r In Sepa
                Fpdf = System.IO.Path.GetFileName(r)
                trans = Fpdf.Substring(0, Fpdf.Length - 4)
                Dim path As String = Server.MapPath(sFileServerVis)
                fileName = trans & ".pdf"
                PdfWriter.GetInstance(doc, New FileStream(path & fileName, FileMode.Create))
                doc.Open()

                Dim jpg As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(Server.MapPath(sFileServerVis & Fpdf))
                jpg.ScalePercent(31.8F)
                doc.Add(jpg)
                doc.NewPage()
            Next r
        Catch ex As Exception
            'lblMessage.Text = ex.ToString()
        Finally
            doc.Close()
        End Try
        Fpdf = System.IO.Path.GetFileName(dsp)
        trans = Fpdf.Substring(0, Fpdf.Length - 4)
        NSeries = trans & ".pdf"
        'MoveFile(sFileDirVis & NSeries, sFilePDFVis)
        pdfLoc = sFileServerVis + NSeries.Trim

        DownloadFile(pdfLoc.Trim, True)
    End Sub
    Private Shared Sub MoveFile(ByVal sourceFile As String, ByVal pathServer As String)
        If File.Exists(sourceFile) Then
            Dim fileName As String = Path.GetFileNameWithoutExtension(sourceFile)
            Dim extension As String = Path.GetExtension(sourceFile)
            Dim destinationFile As [String] = pathServer & fileName & extension
            If File.Exists(destinationFile) Then
                Dim i As Integer = 0
                While File.Exists(pathServer & String.Format("{0}Copy{1}{2}", fileName, i, extension))
                    i += 1
                End While
                destinationFile = pathServer & String.Format("{0}Copy{1}{2}", fileName, i, extension)
            End If
            File.Move(sourceFile, destinationFile)
        End If
    End Sub
End Class
