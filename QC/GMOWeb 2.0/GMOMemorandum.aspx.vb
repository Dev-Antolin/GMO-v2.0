Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports clsGMOUser

Partial Class GMOMemorandum
    Inherits System.Web.UI.Page
    Dim sFileDirVis As String = System.Web.HttpContext.Current.Server.MapPath("") & "\VisminImageMemo\"
    Dim sFileDirLuz As String = System.Web.HttpContext.Current.Server.MapPath("") & "\LuzonImageMemo\"
    Private ds As New DataSet()
    Private dv As New DataView()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.Session("click") = "Memorandum"
        End If
        Me.Session("Oyeh") = 0
        Me.Session("Plus") = 0
        ImageDelete()
        CheckLogin()
        txtCircular.Attributes.Add("onkeypress", "return numerals(event)")
        txtSubject.Attributes.Add("onkeypress", "return Chars(event)")
        If Not IsPostBack Then
            ds = Select_RecipientList()
            If Not ds Is Nothing Then
                gvRecipientsList.DataSource = ds.Tables(0)
                gvRecipientsList.DataBind()
            End If
            txtCircular.Focus()
        End If
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
    Public Function Select_RecipientList() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        _DatabaseConnection1()
        _ConnectionString1()

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim x As String
                x = "select Circularnumber, Subject, CreateDate, Trxnumber from RecipientsListCircular where zonecode = 'vismin' order by Createdate desc"
                ds = Execute_Dataset(x)
            Catch ex As Exception
                MsgBox(ex.Message)
                Con.Close()
                Com.Dispose()
            End Try
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Try
                Dim x As String
                x = "select Circularnumber, Subject, CreateDate, Trxnumber from RecipientsListCircular where zonecode = 'luzon' order by Createdate desc"
                ds = Execute_Dataset(x)
            Catch ex As Exception
                MsgBox(ex.Message)
                Con.Close()
                Com.Dispose()
            End Try
        End If
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
    End Function
    Protected Sub gvRecipientsList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvRecipientsList.SelectedIndexChanged
        Dim None As String = GMONone()
        If None = True Then
            Me.Session.Add("_Circular", gvRecipientsList.SelectedRow.Cells(1).Text)
            Me.Session.Add("_Subject", gvRecipientsList.SelectedRow.Cells(2).Text)
            'Me.Session.Add("_CreateDate", gvRecipientsList.SelectedRow.Cells(3).Text)
            Me.Session.Add("_SentTo", gvRecipientsList.SelectedRow.Cells(3).Text)
            Response.Redirect("GMOView.aspx")
        Else
            Me.Session.Add("_Circular", gvRecipientsList.SelectedRow.Cells(1).Text)
            Me.Session.Add("_Subject", gvRecipientsList.SelectedRow.Cells(2).Text)
            Me.Session.Add("_CreateDate", gvRecipientsList.SelectedRow.Cells(3).Text)
            Response.Redirect("GMOMemorandumDetail.aspx")
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If txtCircular.Text = "" AndAlso txtSubject.Text = "" Then
            lblMessage.ForeColor = Drawing.Color.Red
            lblMessage.Text = "Please input Circular # or Subject!"
            txtCircular.Focus()
            Exit Sub
        End If
        If txtCircular.Text = txtCircular.Text AndAlso txtSubject.Text = txtSubject.Text Then
            Dim ds As DataSet
            ds = Select_SearchDetail()
            If Not ds Is Nothing Then
                gvRecipientsList.DataSource = ds.Tables(0)
                gvRecipientsList.DataBind()
                lblMessage.ForeColor = Drawing.Color.Red
                lblMessage.Text = gvRecipientsList.Rows.Count & " detail(s) found."
            Else
                gvRecipientsList.DataSource = Nothing
                gvRecipientsList.DataBind()
                lblMessage.ForeColor = Drawing.Color.Red
                lblMessage.Text = "No detail(s) found!."
                txtCircular.Text = ""
                txtSubject.Text = ""
                txtCircular.Focus()
            End If
        End If
    End Sub
    Public Function Select_SearchDetail() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim x As String
                x = "select Circularnumber, Subject, Createdate  from RecipientsListCircular where circularnumber like '" + txtCircular.Text.Trim + "%' and subject like '%" + txtSubject.Text.Trim.Replace("'", "''") + "%' and  zonecode = 'vismin' order by circularnumber desc"
                ds = Execute_Dataset(x)
            Catch ex As Exception
                MsgBox(ex.Message)
                Con.Close()
                Com.Dispose()
            End Try
            Con.Close()
            Com.Dispose()
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Try
                Dim x As String
                x = "select Circularnumber, Subject, Createdate from RecipientsListCircular where circularnumber like '" + txtCircular.Text.Trim + "%' and subject like '%" + txtSubject.Text.Trim.Replace("'", "''") + "%' and zonecode = 'luzon' order by circularnumber desc"
                ds = Execute_Dataset(x)
            Catch ex As Exception
                MsgBox(ex.Message)
                Con.Close()
                Com.Dispose()
            End Try
            Con.Close()
            Com.Dispose()
        End If
        Return ds
    End Function
    Public Function Select_SearchCircular() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim x As String
                x = "select Circularnumber, Subject, Createdate from RecipientsListCircular where circularnumber like '" + txtCircular.Text.Trim + "%' and  zonecode = 'vismin' order by circularnumber desc"
                ds = Execute_Dataset(x)
            Catch ex As Exception
                MsgBox(ex.Message)
                Con.Close()
                Com.Dispose()
            End Try
            Con.Close()
            Com.Dispose()
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Try
                Dim x As String
                x = "select Circularnumber, Subject, Createdate from RecipientsListCircular where circularnumber like '" + txtCircular.Text.Trim + "%' and zonecode = 'luzon' order by circularnumber desc"
                ds = Execute_Dataset(x)
            Catch ex As Exception
                MsgBox(ex.Message)
                Con.Close()
                Com.Dispose()
            End Try
            Con.Close()
            Com.Dispose()
        End If
        Return ds
    End Function
    Public Function Select_SearchSubject() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim x As String
                x = "select Circularnumber, Subject, Createdate from RecipientsList where subject like '" + txtSubject.Text.Trim + "%' and  zonecode = 'vismin' order by circularnumber desc"
                ds = Execute_Dataset(x)
            Catch ex As Exception
                MsgBox(ex.Message)
                Con.Close()
                Com.Dispose()
            End Try
            Con.Close()
            Com.Dispose()
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Try
                Dim x As String
                x = "select Circularnumber, Subject, Createdate from RecipientsList where subject like '" + txtSubject.Text.Trim + "%' and zonecode = 'luzon' order by circularnumber desc"
                ds = Execute_Dataset(x)
            Catch ex As Exception
                MsgBox(ex.Message)
                Con.Close()
                Com.Dispose()
            End Try
            Con.Close()
            Com.Dispose()
        End If
        Return ds
    End Function

    Public Function GMONone() As String
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim dr As SqlDataReader

        _DatabaseConnection1()
        _ConnectionString1()

        Try
            Con.ConnectionString = constr2
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
        Catch exc As Exception
            Return "no connection"
        End Try

        Try
            Com = Con.CreateCommand
            Com.CommandText = "select circularnumber from recipientslist where sentto = 'none' and circularnumber = '" & gvRecipientsList.SelectedRow.Cells(1).Text & "'"
            dr = Com.ExecuteReader
            If dr.Read() Then
                dr.Close()
                Return "true"
            Else
                Return "false"
            End If
        Catch ex As Exception
            Return "false"
        End Try
        Con.Close()
        Com.Dispose()
    End Function

    Protected Sub gvRecipientsList_OnSorting(ByVal sender As [Object], ByVal e As GridViewSortEventArgs)
        ViewState("sortExpr") = e.SortExpression
        gvRecipientsList.DataSource = Select_RecipientList()
        gvRecipientsList.DataBind()
    End Sub
    Protected Sub gvRecipientsList_PageIndexChanging(ByVal sender As [Object], ByVal e As GridViewPageEventArgs)
        gvRecipientsList.PageIndex = e.NewPageIndex
        gvRecipientsList.DataSource = Select_RecipientList()
        gvRecipientsList.DataBind()
    End Sub
End Class
