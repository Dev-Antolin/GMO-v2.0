Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports clsGMOUser
Partial Class Login_GMO
    Inherits System.Web.UI.Page
    Dim fullname As String
    Protected Sub btnLogIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogIn.Click
        If txtPassword.Text = "" And txtUserName.Text = "" Then
            lblMsg.Visible = True
            lblMsg.Text = "No Username and Password!"
            txtUserName.Focus()
            Exit Sub
        End If
        If txtPassword.Text = "" And txtUserName.Text = txtUserName.Text Then
            lblMsg.Visible = True
            lblMsg.Text = "Please input password!"
            txtPassword.Focus()
            Exit Sub
        End If
        If txtUserName.Text = "" And txtPassword.Text = txtPassword.Text Then
            lblMsg.Visible = True
            lblMsg.Text = "Please input username!"
            txtUserName.Focus()
            Exit Sub
        End If
        Dim LoginMsg As String = Search_Me()
        If LoginMsg = "true" Then
            If Trim(Me.Session("_task")) = ("GMO-GENASST") Or Trim(Me.Session("_task")) = ("GMO-ASTGENMAN") Or Trim(Me.Session("_task")) = ("GMO-SECGM") Or Trim(Me.Session("_task")) = ("GMO-HELPDESK") Then
                Response.Redirect("GMOStartScan.aspx")

            ElseIf Trim(Me.Session("_task")) = ("REGIONAL MAN") Or Trim(Me.Session("_task")) = ("Regional Man") Then
                Response.Redirect("GMORAB.aspx")

            ElseIf Trim(Me.Session("_task")) = ("AREA MANAGER") Or Trim(Me.Session("_task")) = ("Area Manager") Then
                Response.Redirect("GMORAB.aspx")

            ElseIf Trim(Me.Session("_task")) = ("BM/BOSMAN") Or Trim(Me.Session("_task")) = ("Bm/Bosman") Or Trim(Me.Session("_task")) = ("LPT/BM-A") Or Trim(Me.Session("_task")) = ("LPT/BM-R") Or Trim(Me.Session("_task")) = ("LPT/BM-A/BOSMAN") Or Trim(Me.Session("_task")) = ("LPT/BM-R/BOSMAN") Or Trim(Me.Session("_task")) = ("LPTL/BM/LPT/BOSMAN") Then
                Response.Redirect("GMORAB.aspx")
            Else

                LoginMsg = search_divisionMngr()
                If LoginMsg = "true" Then
                    Response.Redirect("GMORAB.aspx")
                ElseIf LoginMsg = "you're not authorized" Then
                    lblMsg.Visible = True
                    lblMsg.Text = "You're not Authorized!"
                    txtPassword.Text = ""
                    txtUserName.Text = ""
                    txtUserName.Focus()
                    Exit Sub
                ElseIf LoginMsg = "no connection" Then
                    lblMsg.Visible = True
                    lblMsg.Text = "Connection Failed!"
                    txtPassword.Focus()
                End If
            End If
        ElseIf LoginMsg = "you're not authorized" Then
            lblMsg.Visible = True
            lblMsg.Text = "You're not Authorized!"
            txtPassword.Text = ""
            txtUserName.Text = ""
            txtUserName.Focus()
            Exit Sub
        ElseIf LoginMsg = "no connection" Then
            lblMsg.Visible = True
            lblMsg.Text = "Connection Failed!"
            txtPassword.Focus()
            End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtUsername.Focus()
        Me.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Me.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1))
        Me.Response.Cache.SetValidUntilExpires(False)
        lblMsg.Visible = False
        Me.Session("_fullname") = ""
    End Sub
    Public Function Search_Me() As String
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim dr As SqlDataReader

        _DatabaseConnection()
        _ConnectionString()

        Try
            Con.ConnectionString = constr
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
        Catch exc As Exception
            Return "no connection"
        End Try
        Try
            Com = Con.CreateCommand
            Com.CommandText = "select (replace(rtrim(sur_name),'ñ','n')+', '+ replace(rtrim(first_name),'ñ','n')) as fullname1, " & _
            "wa.task, wa.comp, wa.costcenter, wb.bedrnm ,wa.zonecode,wa.fullname from Webaccounts as wa " & _
            "inner join webbranches as wb on wa.comp = wb.bedrnr and wa.zonecode= wb.zonecode " & _
            "where usr_id = '" + txtUserName.Text + "' and passwrd like '" + txtPassword.Text + "'"
            dr = Com.ExecuteReader
            dr.Read()
            If Not dr Is Nothing Then
                Me.Session.Add("_fullname", dr.Item("fullname1"))
                Me.Session.Add("_task", dr.Item("task"))
                Me.Session.Add("_compcode", dr.Item("comp"))
                Me.Session.Add("_ccenter", dr.Item("costcenter"))
                Me.Session.Add("_compname", dr.Item("bedrnm"))
                Me.Session.Add("_zonecode", dr.Item("zonecode"))
                fullname = dr.Item("fullname")
                dr.Close()
                Return "true"
            Else
                Return "you're not authorized"
            End If
        Catch ex As Exception
            Return "you're not authorized"
        End Try
        Con.Close()
        Com.Dispose()
    End Function
    Public Function search_divisionMngr() As String
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim dr As SqlDataReader

        _DatabaseConnection()
        _ConnectionString()

        Try
            Con.ConnectionString = constr
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
        Catch exc As Exception
            Return "no connection"
        End Try
        Try
            Com = Con.CreateCommand
            Com.CommandText = "select wa.fullname from webaccounts wa inner join irdivision ird on wa.costcenter = ird.costcenter where task like '%DIVMAN%' or task like '%GMO-GENMAN%'"
            dr = Com.ExecuteReader

            While dr.Read()
                Dim name_DivMngr As String = Trim(UCase(dr.Item("fullname")).ToString)
                fullname = Trim(UCase(fullname).ToString)
                If fullname = name_DivMngr Then
                    dr.Close()
                    Return "true"
                End If
            End While
            Return "you're not authorized"
        Catch ex As Exception
            Return "you're not authorized"
        End Try
        Con.Close()
        Com.Dispose()
    End Function
End Class

