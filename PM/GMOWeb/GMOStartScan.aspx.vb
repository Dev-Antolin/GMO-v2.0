﻿Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports clsGMOUser
Partial Class GMOStartScan
    Inherits System.Web.UI.Page
    Dim sFileDirVis As String = System.Web.HttpContext.Current.Server.MapPath("") & "\VisminImageMemo\"
    Dim sFileDirLuz As String = System.Web.HttpContext.Current.Server.MapPath("") & "\LuzonImageMemo\"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.Session("Click") = "CreateNewMemo"
        End If
        Me.Session("Oyeh") = 0
        Me.Session("Plus") = 0
        ImageDelete()
        CheckLogin()
        scanmemo.ForeColor = System.Drawing.Color.Blue
        uploadmemo.ForeColor = System.Drawing.Color.Blue
        attachmemo.ForeColor = System.Drawing.Color.Blue
    End Sub
    Private Sub CheckLogin()
        If Me.Session("_fullname") = "" Then
            Response.Redirect("Login.aspx")
        End If
    End Sub
    Private Sub DeleteFile(ByVal strFileName As String)
        If strFileName.Trim().Length > 0 Then
            Dim fi As New FileInfo(strFileName)
            If (fi.Exists) Then
                fi.Delete()
            End If
        End If
    End Sub
    Sub ImageDelete()
        Dim Del As String
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            For Each Del In System.IO.Directory.GetFiles(sFileDirVis, "*.jpg")
                System.IO.File.Delete(Del)
            Next Del
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            For Each Del In System.IO.Directory.GetFiles(sFileDirLuz, "*.jpg")
                System.IO.File.Delete(Del)
            Next Del
        End If
    End Sub
End Class
