﻿Imports System
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

Partial Class GMOView
    Inherits System.Web.UI.Page
    Dim Pics As String = ""
    Dim que As New Queue
    Dim i As Integer = 0
    Dim s As String
    Dim r As String
    Dim p As Integer
    Dim Sepa() As String = Nothing
    Dim dsp As String = ""
    Dim sFileServerVis As String = "~/MemoImageVismin/"
    Dim sFileServerLuz As String = "~/MemoImageLuzon/"
    Dim fileName As String
    Dim Fpdf As String = ""
    Dim trans As String = ""
    Dim FTrans As String = ""
    Dim pdfLoc As String = ""
    Public Function Select_Recipient() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()
        Try
            Dim y As String
            y = "select memopath from recipientslistcircular where MemoFrom ='GM''s OFFICE' and subject='" + Trim(Me.Session("_Subject")).Replace("'", "''").Replace("&#241;", "ñ") + "' and CircularNumber='" + Trim(Me.Session("_Circular")) + "'"
            ds = Execute_Dataset(y)
        Catch ex As Exception
            MsgBox(ex.Message)
            Con.Close()
            Com.Dispose()
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
        End Try
        Con.Close()
        Com.Dispose()
    End Function
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
            'dsp = que.Peek.Trim.Replace("D:\Silvher's Vb.net 08\GMOWeb\MemoImageVismin\", "~/MemoImageVismin/")
            dsp = que.Peek.Trim.Replace("C:\GMOWeb\MemoImageVismin\", "~/MemoImageVismin/")
            imgDisplayMe.ImageUrl = dsp.Trim
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
        Else
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
            'dsp = que.Peek.Trim.Replace("D:\Silvher's Vb.net 08\GMOWeb\MemoImageLuzon\", "~/MemoImageLuzon/")
            dsp = que.Peek.Trim.Replace("C:\GMOWeb\MemoImageLuzon\", "~/MemoImageLuzon/")
            imgDisplayMe.ImageUrl = dsp.Trim
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
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("GMOMemoRandum.aspx")
    End Sub

    Protected Sub btnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        Dim tin As String = ""
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            If Me.Session("Plus") < que.Count Then
                Me.Session("Plus") = Me.Session("Plus") - 1
                If Me.Session("Plus") = 0 Then
                    btnPrev.Enabled = False
                    btnNext.Enabled = True
                End If
                'tin = que(Me.Session("Plus")).Trim.Replace("D:\Silvher's Vb.net 08\GMOWeb\MemoImageVismin\", "~/MemoImageVismin/")
                tin = que(Me.Session("Plus")).Trim.Replace("C:\GMOWeb\MemoImageVismin\", "~/MemoImageVismin/")
                imgDisplayMe.ImageUrl = tin.Trim
                lblCountNumber.Text = Me.Session("Plus") + 1 & " of " & que.Count
            End If
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            If Me.Session("Plus") < que.Count Then
                Me.Session("Plus") = Me.Session("Plus") - 1
                If Me.Session("Plus") = 0 Then
                    btnPrev.Enabled = False
                    btnNext.Enabled = True
                End If
                'tin = que(Me.Session("Plus")).Trim.Replace("D:\Silvher's Vb.net 08\GMOWeb\MemoImageLuzon\", "~/MemoImageLuzon/")
                tin = que(Me.Session("Plus")).Trim.Replace("C:\GMOWeb\MemoImageLuzon\", "~/MemoImageLuzon/")
                imgDisplayMe.ImageUrl = tin.Trim
                lblCountNumber.Text = Me.Session("Plus") + 1 & " of " & que.Count
            End If
        End If
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim Ini As String = ""
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            If Me.Session("Plus") < que.Count Then
                i = Me.Session("Plus")
                i = i + 1
                If i = que.Count - 1 Then
                    btnNext.Enabled = False
                End If
                'Ini = que(i).ToString.Trim.Replace("D:\Silvher's Vb.net 08\GMOWeb\MemoImageVismin\", "~/MemoImageVismin/")
                Ini = que(i).ToString.Trim.Replace("C:\GMOWeb\MemoImageVismin\", "~/MemoImageVismin/")
                imgDisplayMe.ImageUrl = Ini.Trim
                Me.Session.Add("Plus", i)
                lblCountNumber.Text = i.ToString + 1 & " of " & que.Count
                btnPrev.Enabled = True
            End If
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            If Me.Session("Plus") < que.Count Then
                i = Me.Session("Plus")
                i = i + 1
                If i = que.Count - 1 Then
                    btnNext.Enabled = False
                End If
                'Ini = que(i).ToString.Trim.Replace("D:\Silvher's Vb.net 08\GMOWeb\MemoImageLuzon\", "~/MemoImageLuzon/")
                Ini = que(i).ToString.Trim.Replace("C:\GMOWeb\MemoImageLuzon\", "~/MemoImageLuzon/")
                imgDisplayMe.ImageUrl = Ini.Trim
                Me.Session.Add("Plus", i)
                lblCountNumber.Text = i.ToString + 1 & " of " & que.Count
                btnPrev.Enabled = True
            End If
        End If
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        ConvertFile()
    End Sub
    Public Sub ConvertFile()
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
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
                Fpdf = System.IO.Path.GetFileName(dsp)
                trans = Fpdf.Substring(0, Fpdf.Length - 4)
                NSeries = trans & ".pdf"
                pdfLoc = sFileServerVis + NSeries.Trim
            Catch ex As Exception
                'lblMessage.Text = ex.ToString()
            Finally
                doc.Close()
            End Try
            DownloadFile(pdfLoc.Trim, True)
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Dim doc As New Document(iTextSharp.text.PageSize.LEGAL, 42, 45, 52, 45)
            Dim writer As PdfCopy = Nothing
            Dim NSeries As String = ""
            Try
                For Each Me.r In Sepa
                    Fpdf = System.IO.Path.GetFileName(r)
                    trans = Fpdf.Substring(0, Fpdf.Length - 4)
                    Dim path As String = Server.MapPath(sFileServerLuz)
                    fileName = trans & ".pdf"
                    PdfWriter.GetInstance(doc, New FileStream(path & fileName, FileMode.Create))
                    doc.Open()

                    Dim jpg As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(Server.MapPath(sFileServerLuz & Fpdf))
                    jpg.ScalePercent(31.8F)
                    doc.Add(jpg)
                    doc.NewPage()
                Next r
                Fpdf = System.IO.Path.GetFileName(dsp)
                trans = Fpdf.Substring(0, Fpdf.Length - 4)
                NSeries = trans & ".pdf"
                pdfLoc = sFileServerLuz + NSeries.Trim
            Catch ex As Exception
                'lblMessage.Text = ex.ToString()
            Finally
                doc.Close()
            End Try
            DownloadFile(pdfLoc.Trim, True)
        End If
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
End Class
