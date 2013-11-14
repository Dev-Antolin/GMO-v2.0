Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports clsGMOUser
Partial Class GMORAB
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'txtCircular.Attributes.Add("onkeypress", "return numerals(event)")
        Me.Session("Plus") = 0
        CheckLogin()
        Dim DS As New DataSet
        DS = Select_ReceivedMemo()
        If Not DS Is Nothing Then
            gvReceiver.DataSource = DS.Tables(0)
            gvReceiver.DataBind()
        End If
    End Sub
    Private Sub CheckLogin()
        If Me.Session("_fullname") = "" Then
            Response.Redirect("Login.aspx")
        End If
    End Sub
    Public Function Select_ReceivedMemo() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        Try
            Dim x As String
            x = "select Circularnumber, subject, memofrom, memopath, createdate from recipientslist where sentto ='" + Trim(Me.Session("_fullname")) + "' order by createdate desc"
            ds = Execute_Dataset(x)
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
    Protected Sub gvReceiver_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvReceiver.SelectedIndexChanged
        Me.Session.Add("_Circular", gvReceiver.SelectedRow.Cells(1).Text)
        Me.Session.Add("_Subject", gvReceiver.SelectedRow.Cells(2).Text)
        Me.Session.Add("_MemoFrom", gvReceiver.SelectedRow.Cells(3).Text.Replace("'", "''"))
        Response.Redirect("GMORABView.aspx")
    End Sub

    'Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
    '    If txtCircular.Text = "" AndAlso txtSubject.Text = "" Then
    '        lblMessage.ForeColor = Drawing.Color.Red
    '        lblMessage.Text = "Insert Circular # or Subject!."
    '        txtCircular.Focus()
    '        Exit Sub
    '    End If
    '    If txtCircular.Text = txtCircular.Text AndAlso txtSubject.Text = "" Then
    '        Dim CS As New DataSet
    '        CS = Select_SearchCircular()
    '        If Not CS Is Nothing Then
    '            gvReceiver.DataSource = CS.Tables(0)
    '            gvReceiver.DataBind()
    '            lblMessage.ForeColor = Drawing.Color.Red
    '            lblMessage.Text = gvReceiver.Rows.Count & " detail(s) found."
    '        Else
    '            gvReceiver.DataSource = Nothing
    '            gvReceiver.DataBind()
    '            lblMessage.ForeColor = Drawing.Color.Red
    '            lblMessage.Text = "No detail(s) found!."
    '            txtCircular.Text = ""
    '            txtSubject.Text = ""
    '            txtCircular.Focus()
    '        End If
    '        Dim DS As New DataSet
    '        DS = Select_ReceivedMemo()
    '        If Not DS Is Nothing Then
    '            gvReceiver.DataSource = DS.Tables(0)
    '            gvReceiver.DataBind()
    '        End If
    '        txtCircular.Focus()
    '        Exit Sub
    '    End If
    '    If txtSubject.Text = txtSubject.Text AndAlso txtCircular.Text = "" Then
    '        Dim SS As New DataSet
    '        SS = Select_SearchSubject()
    '        If Not SS Is Nothing Then
    '            gvReceiver.DataSource = SS.Tables(0)
    '            gvReceiver.DataBind()
    '            lblMessage.ForeColor = Drawing.Color.Red
    '            lblMessage.Text = gvReceiver.Rows.Count & " detail(s) found."
    '        Else
    '            gvReceiver.DataSource = Nothing
    '            gvReceiver.DataBind()
    '            lblMessage.ForeColor = Drawing.Color.Red
    '            lblMessage.Text = "No detail(s) found!."
    '            txtCircular.Text = ""
    '            txtSubject.Text = ""
    '            txtCircular.Focus()
    '        End If
    '        Dim DS As New DataSet
    '        DS = Select_ReceivedMemo()
    '        If Not DS Is Nothing Then
    '            gvReceiver.DataSource = DS.Tables(0)
    '            gvReceiver.DataBind()
    '        End If
    '        txtCircular.Focus()
    '        Exit Sub
    '    End If
    '    If txtCircular.Text = txtCircular.Text AndAlso txtSubject.Text = txtSubject.Text Then
    '        Dim ds As DataSet
    '        ds = Select_SearchDetail()
    '        If Not ds Is Nothing Then
    '            gvReceiver.DataSource = ds.Tables(0)
    '            gvReceiver.DataBind()
    '            lblMessage.ForeColor = Drawing.Color.Red
    '            lblMessage.Text = gvReceiver.Rows.Count & " detail(s) found."
    '        Else
    '            gvReceiver.DataSource = Nothing
    '            gvReceiver.DataBind()
    '            lblMessage.ForeColor = Drawing.Color.Red
    '            lblMessage.Text = "No detail(s) found!."
    '            txtCircular.Text = ""
    '            txtSubject.Text = ""
    '            txtCircular.Focus()
    '        End If
    '        Dim Dr As New DataSet
    '        Dr = Select_ReceivedMemo()
    '        If Not Dr Is Nothing Then
    '            gvReceiver.DataSource = Dr.Tables(0)
    '            gvReceiver.DataBind()
    '        End If
    '        txtCircular.Focus()
    '    End If
    'End Sub
    'Public Function Select_SearchDetail() As DataSet
    '    Dim Con As New SqlConnection
    '    Dim Com As New SqlCommand
    '    Dim ds As New DataSet
    '    _DatabaseConnection1()
    '    _ConnectionString1()

    '    If Trim(Me.Session("_zonecode")) = "VISMIN" Then
    '        Try
    '            Dim x As String
    '            x = "select * from RecipientsList where circularnumber like '%" + txtCircular.Text.Trim + "%' and subject like '%" + txtSubject.Text.Trim + "%' and sentto = '" + Trim(Me.Session("_fullname")) + "' and zonecode = 'vismin' order by circularnumber desc"
    '            ds = Execute_Dataset(x)
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '        Con.Close()
    '        Com.Dispose()
    '    End If
    '    If Trim(Me.Session("_zonecode")) = "LUZON" Then
    '        Try
    '            Dim x As String
    '            x = "select * from RecipientsList where circularnumber like '%" + txtCircular.Text.Trim + "%' and subject like '%" + txtSubject.Text.Trim + "%' sentto = '" + Trim(Me.Session("_fullname")) + "' and zonecode = 'luzon' order by circularnumber desc"
    '            ds = Execute_Dataset(x)
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '        Con.Close()
    '        Com.Dispose()
    '    End If
    '    Return ds
    'End Function
    'Public Function Select_SearchCircular() As DataSet
    '    Dim Con As New SqlConnection
    '    Dim Com As New SqlCommand
    '    Dim ds As New DataSet
    '    _DatabaseConnection1()
    '    _ConnectionString1()

    '    If Trim(Me.Session("_zonecode")) = "VISMIN" Then
    '        Try
    '            Dim x As String
    '            x = "select * from RecipientsList where circularnumber like '%" + txtCircular.Text.Trim + "%' and sentto = '" + Trim(Me.Session("_fullname")) + "' and  zonecode = 'vismin' order by circularnumber desc"
    '            ds = Execute_Dataset(x)
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '        Con.Close()
    '        Com.Dispose()
    '    End If
    '    If Trim(Me.Session("_zonecode")) = "LUZON" Then
    '        Try
    '            Dim x As String
    '            x = "select * from RecipientsList where circularnumber like '%" + txtCircular.Text.Trim + "%' sentto = '" + Trim(Me.Session("_fullname")) + "' and zonecode = 'luzon' order by circularnumber desc"
    '            ds = Execute_Dataset(x)
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '        Con.Close()
    '        Com.Dispose()
    '    End If
    '    Return ds
    'End Function
    'Public Function Select_SearchSubject() As DataSet
    '    Dim Con As New SqlConnection
    '    Dim Com As New SqlCommand
    '    Dim ds As New DataSet
    '    _DatabaseConnection1()
    '    _ConnectionString1()

    '    If Trim(Me.Session("_zonecode")) = "VISMIN" Then
    '        Try
    '            Dim x As String
    '            x = "select * from RecipientsList where subject like '%" + txtSubject.Text.Trim + "%' and sentto = '" + Trim(Me.Session("_fullname")) + "' and  zonecode = 'vismin' order by circularnumber desc"
    '            ds = Execute_Dataset(x)
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '        Con.Close()
    '        Com.Dispose()
    '    End If
    '    If Trim(Me.Session("_zonecode")) = "LUZON" Then
    '        Try
    '            Dim x As String
    '            x = "select * from RecipientsList where subject like '%" + txtSubject.Text.Trim + "%' and sentto = '" + Trim(Me.Session("_fullname")) + "' and zonecode = 'luzon' order by circularnumber desc"
    '            ds = Execute_Dataset(x)
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '        Con.Close()
    '        Com.Dispose()
    '    End If
    '    Return ds
    'End Function
End Class
