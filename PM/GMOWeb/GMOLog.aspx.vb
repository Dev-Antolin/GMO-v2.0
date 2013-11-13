Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports clsGMOUser
Partial Class GMOLog
    Inherits System.Web.UI.Page
    Dim clsgmo As clsGMOUser
    Dim sFileDirVis As String = System.Web.HttpContext.Current.Server.MapPath("") & "\VisminImageMemo\"
    Dim sFileDirLuz As String = System.Web.HttpContext.Current.Server.MapPath("") & "\LuzonImageMemo\"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Buffer = True
        'Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D)
        'Response.Expires = -1500
        'Response.CacheControl = "no-cache"

        'Searchbtn.Enabled = False
        DivisionDPL.Enabled = True
        RegionDPL.Enabled = True
        AreaDPL.Enabled = False
        BranchDPL.Enabled = False
        If Not IsPostBack Then
            Me.Session("click") = "Log"
            txtdatefrom.Text = Date.Today
            txtdateto.Text = Date.Today
        End If
        Me.Session("Oyeh") = 0
        Me.Session("Plus") = 0
        ImageDelete()
        CheckLogin()
        'txtSearch.Attributes.Add("onkeypress", "return Chars(event)")
        'Dim ds As New DataSet
        'ds = Select_Stat_Recipient()
        'If Not ds Is Nothing Then
        '    gvRecipientsList.DataSource = ds.Tables(0)
        '    gvRecipientsList.DataBind()
        'End If
        'For x = 0 To gvRecipientsList.Rows.Count - 1
        '    If gvRecipientsList.Rows(x).Cells(3).Text = 1 Then
        '        gvRecipientsList.Rows(x).Cells(3).Text = "Open"
        '    End If
        'Next
        'region()
        'DIVISION()
    End Sub
    Private Sub CheckLogin()
        If Me.Session("_fullname") = "" Then
            Response.Redirect("Login_GMO.aspx")
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
    Public Function Select_RecipientsName() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        Dim searchName As String = Trim(Session("RecipientsName"))
        _DatabaseConnection1()
        _ConnectionString1()
        Dim datefrom As String = CDate(txtdatefrom.Text).ToString("MM/dd/yyyy")
        Dim dateto As String = CDate(txtdateto.Text).ToString("MM/dd/yyyy")
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim y As String
                y = "select Circular, Subject, Fullname, CReceived, CostCenters, Division, CDate from Memo_Recipients_name where fullname like '%" & searchName.Trim.Replace("'", "''") & "%' and zonecode = 'vismin' " & _
                    "and division = '" & Session("Branchname") & "' and convert(varchar,CDate,101) between '" & datefrom & "' and " & _
                    "'" & dateto & "' order by CDate desc "
                ds = Execute_Dataset(y)
                If ds Is Nothing Then
                    DateErrorLbl.Text = "No MEMO was found!"
                    DateErrorLbl.Visible = True
                End If
            Catch ex As Exception
                DateErrorLbl.Text = (ex.Message)
                DateErrorLbl.Visible = True
            End Try
            Con.Close()
            Com.Dispose()
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Try
                Dim y As String
                y = "select Circular, Subject, Fullname, CReceived, CostCenters, Division, CDate from Memo_Recipients_name where fullname like '%" & searchName.Trim.Replace("'", "''") & "%' and zonecode = 'luzon' " & _
               "and division = '" & Session("Branchname") & "' and convert(varchar,CDate,101) between '" & datefrom & "' and " & _
               "'" & dateto & "' order by CDate desc "
                ds = Execute_Dataset(y)
                If ds Is Nothing Then
                    DateErrorLbl.Text = "No MEMO was found!"
                    DateErrorLbl.Visible = True
                End If
            Catch ex As Exception
                DateErrorLbl.Text = (ex.Message)
                DateErrorLbl.Visible = True
            End Try
            Con.Close()
            Com.Dispose()
        End If
        Return ds
    End Function
    Public Function Select_Stat_Recipient() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim y As String
                Dim month As Integer = Now.Month
                Dim year As Integer = Now.Year
                y = "exec Memo_Recipients_StatusV_GMOv2 'vismin','" & month & "','" & year & "'"
                ds = Execute_Dataset(y)
            Catch ex As Exception
                DateErrorLbl.Text = (ex.Message)
                DateErrorLbl.Visible = True
            End Try
            Con.Close()
            Com.Dispose()
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Try
                Dim y As String
                Dim month As Integer = Now.Month
                Dim year As Integer = Now.Year
                y = "exec Memo_Recipients_StatusL_GMOv2 'luzon','" & month & "','" & year & "'"
                ds = Execute_Dataset(y)
            Catch ex As Exception
                DateErrorLbl.Text = (ex.Message)
                DateErrorLbl.Visible = True
            End Try
            Con.Close()
            Com.Dispose()
        End If
        Return ds
    End Function
    Public Sub Select_RecipientsName_Region()
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        Dim regionMan As String
        Dim branchname As String
        _DatabaseConnection()
        _ConnectionString()
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim y As String
                y = "select sur_name,bedrnm from irregionalmanagers where zonecode = 'vismin' and class_03 = '" & RegionDPL.Text & "'"
                ds = Execute_Dataset1(y)
                regionMan = ds.Tables(0).Rows(0).Item(0)
                branchname = ds.Tables(0).Rows(0).Item(1)
                Session.Add("RecipientsName", Trim(regionMan))
                Session.Add("BranchName", Trim(branchname))
            Catch ex As Exception
                DateErrorLbl.Text = (ex.Message)
                DateErrorLbl.Visible = True
            End Try
            Con.Close()
            Com.Dispose()
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Try
                Dim y As String
                y = "select sur_name,bedrnm from irregionalmanagers where zonecode = 'vismin' and class_03 = '" & RegionDPL.Text & "'"
                ds = Execute_Dataset1(y)
                regionMan = ds.Tables(0).Rows(0).Item(0)
                branchname = ds.Tables(0).Rows(0).Item(1)
                Session.Add("RecipientsName", Trim(regionMan))
                Session.Add("BranchName", Trim(branchname))
            Catch ex As Exception
                DateErrorLbl.Text = (ex.Message)
                DateErrorLbl.Visible = True
            End Try
            Con.Close()
            Com.Dispose()
        End If
    End Sub
    Public Sub Select_RecipientsName_Area()
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        Dim areaMan As String
        Dim branchname As String
        _DatabaseConnection()
        _ConnectionString()
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim y As String
                y = "select sur_name,bedrnm from irareamanagers where zonecode = 'vismin' and class_04 = '" & AreaDPL.Text & "'"
                ds = Execute_Dataset1(y)
                areaMan = ds.Tables(0).Rows(0).Item(0)
                branchname = ds.Tables(0).Rows(0).Item(1)
                Session.Add("RecipientsName", Trim(areaMan))
                Session.Add("BranchName", Trim(branchname))
            Catch ex As Exception
                DateErrorLbl.Text = (ex.Message)
                DateErrorLbl.Visible = True
            End Try
            Con.Close()
            Com.Dispose()
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Try
                Dim y As String
                y = "select sur_name,bedrnm from irareamanagers where zonecode = 'vismin' and class_04 = '" & AreaDPL.Text & "'"
                ds = Execute_Dataset1(y)
                areaMan = ds.Tables(0).Rows(0).Item(0)
                branchname = ds.Tables(0).Rows(0).Item(1)
                Session.Add("RecipientsName", Trim(areaMan))
                Session.Add("BranchName", Trim(branchname))
            Catch ex As Exception
                DateErrorLbl.Text = (ex.Message)
                DateErrorLbl.Visible = True
            End Try
            Con.Close()
            Com.Dispose()
        End If
    End Sub
    Public Sub Select_RecipientsName_Branch()
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        Dim branchMan As String
        Dim branchName As String
        _DatabaseConnection()
        _ConnectionString()
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim y As String
                y = "select sur_name,bedrnm from irbranchmanager where zonecode = 'vismin' and bedrnm = '" & BranchDPL.Text & "'"
                ds = Execute_Dataset1(y)
                branchMan = ds.Tables(0).Rows(0).Item(0)
                branchName = ds.Tables(0).Rows(0).Item(1)
                Session.Add("RecipientsName", Trim(branchMan))
                Session.Add("BranchName", Trim(branchName))
            Catch ex As Exception
                DateErrorLbl.Text = (ex.Message)
                DateErrorLbl.Visible = True
            End Try
            Con.Close()
            Com.Dispose()
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Try
                Dim y As String
                y = "select sur_name,bedrnm from irbranchmanager where zonecode = 'luzon' and bedrnm = '" & BranchDPL.Text & "'"
                ds = Execute_Dataset1(y)
                branchMan = ds.Tables(0).Rows(0).Item(0)
                branchName = ds.Tables(0).Rows(0).Item(1)
                Session.Add("RecipientsName", Trim(branchMan))
                Session.Add("BranchName", Trim(branchName))
            Catch ex As Exception
                DateErrorLbl.Text = (ex.Message)
                DateErrorLbl.Visible = True
            End Try
            Con.Close()
            Com.Dispose()
        End If
    End Sub
    Public Sub Select_RecipientsName_Division()
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        Dim DivMan As String
        Dim surname As String
        _DatabaseConnection()
        _ConnectionString()
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim y As String
                y = "select divisionmanager from irdivision where zonecode = 'vismin' and division = '" & DivisionDPL.Text & "'"
                ds = Execute_Dataset1(y)
                DivMan = ds.Tables(0).Rows(0).Item(0)
                DivMan = Trim(DivMan)
                Dim index As Integer = DivMan.LastIndexOf(" ")
                index += 1
                surname = DivMan.Substring(index)
                Session.Add("RecipientsName", Trim(surname))
                Session.Add("BranchName", "Cebu Head Office")
            Catch ex As Exception
                DateErrorLbl.Text = "No Division Manager"
                DateErrorLbl.Visible = True
            End Try
            Con.Close()
            Com.Dispose()
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Try
                Dim y As String
                y = "select divisionmanager from irdivision where zonecode = 'vismin' and division = '" & DivisionDPL.Text & "'"
                ds = Execute_Dataset1(y)
                DivMan = ds.Tables(0).Rows(0).Item(0)
                DivMan = Trim(DivMan)
                Dim index As Integer = DivMan.LastIndexOf(" ")
                index += 1
                surname = DivMan.Substring(index)
                Session.Add("RecipientsName", Trim(surname))
                Session.Add("BranchName", "Cebu Head Office")
            Catch ex As Exception
                DateErrorLbl.Text = "No Division Manager"
                DateErrorLbl.Visible = True
            End Try
            Con.Close()
            Com.Dispose()
        End If
    End Sub
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
    Public Function Execute_Dataset1(ByVal as_sql As String) As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim sqlConn As New SqlConnection
        Dim sqlAdapter As SqlDataAdapter
        Dim sqlDataset As New DataSet
        Execute_Dataset1 = Nothing
        Try
            Try
                Con.ConnectionString = constr
                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If
            Catch
            End Try
            sqlAdapter = New SqlDataAdapter(as_sql, Con)
            sqlAdapter.Fill(sqlDataset)
            If Not sqlDataset Is Nothing Then
                If sqlDataset.Tables(0).Rows.Count <> 0 Then
                    Execute_Dataset1 = sqlDataset
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

    Protected Sub gvRecipientsList_OnSorting(ByVal sender As [Object], ByVal e As GridViewSortEventArgs)
        ViewState("sortExpr") = e.SortExpression
        gvRecipientsList.DataSource = Select_Stat_Recipient()
        gvRecipientsList.DataBind()
    End Sub
    Protected Sub gvRecipientsList_PageIndexChanging(ByVal sender As [Object], ByVal e As GridViewPageEventArgs)
        gvRecipientsList.PageIndex = e.NewPageIndex
        gvRecipientsList.DataSource = Select_Stat_Recipient()
        gvRecipientsList.DataBind()
    End Sub

    Private Sub division()
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
            DateErrorLbl.Text = "No Connection"
            DateErrorLbl.Visible = True
        End Try
        Try
            Com = Con.CreateCommand
            Com.CommandText = "SELECT DISTINCT upper(DIVISION) from IRDIVISION WHERE ZONECODE = '" + Session("_ZONECODE") + "' and divisionmanager is not null"
            dr = Com.ExecuteReader
            DivisionDPL.Items.Clear()
            DivisionDPL.Items.Add("")
            While dr.Read
                DivisionDPL.Items.Add(Trim(dr(0).ToString))
            End While
        Catch ex As Exception
            DateErrorLbl.Text = "You are not Authrized!"
            DateErrorLbl.Visible = True
        End Try
        Con.Close()
        Com.Dispose()
    End Sub
    Private Sub region()
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
            DateErrorLbl.Text = "No Connection"
            DateErrorLbl.Visible = True
        End Try
        Try
            Com = Con.CreateCommand
            Com.CommandText = "SELECT DISTINCT upper(Region)as Region from IRRegionCode WHERE ZONECODE = '" + Session("_ZONECODE") + "' AND REGION <> 'HO'"
            dr = Com.ExecuteReader
            'RegionDPL.Items.Clear()
            RegionDPL.Items.Add("")
            While dr.Read
                RegionDPL.Items.Add(Trim(dr(0).ToString))
            End While
            'RegionDPL.SelectedIndex = 0
        Catch ex As Exception
            DateErrorLbl.Text = "You are not Authrized!"
            DateErrorLbl.Visible = True
        End Try
        Con.Close()
        Com.Dispose()
    End Sub
    Private Sub area()
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim dr As SqlDataReader
        Dim ds As New DataSet

        _DatabaseConnection()
        _ConnectionString()

        Try
            Con.ConnectionString = constr
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
        Catch exc As Exception
            DateErrorLbl.Text = "No Connection"
            DateErrorLbl.Visible = True
        End Try
        Try
            Com = Con.CreateCommand
            Com.CommandText = "SELECT DISTINCT upper(Area) as Area from IRAreaCode where region = '" + RegionDPL.Text + "' " & _
           "AND ZONECODE = '" + Session("_ZONECODE") + "'"
            dr = Com.ExecuteReader

            AreaDPL.Items.Add("")
            While dr.Read
                AreaDPL.Items.Add(Trim(dr(0).ToString))
            End While
        Catch ex As Exception
            DateErrorLbl.Text = "You are not Authrized!"
            DateErrorLbl.Visible = True
        End Try
        Con.Close()
        Com.Dispose()
    End Sub
    Private Sub branch()
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim dr As SqlDataReader
        Dim ds As New DataSet

        _DatabaseConnection()
        _ConnectionString()

        Try
            Con.ConnectionString = constr
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
        Catch exc As Exception
            DateErrorLbl.Text = "No Connection"
            DateErrorLbl.Visible = True
        End Try
        Try
            Com = Con.CreateCommand
            Com.CommandText = "SELECT DISTINCT bedrnm as branch from webbranches where class_03 = '" + RegionDPL.Text + "' " & _
                            " and class_04 = '" + AreaDPL.Text + "' AND ZONECODE = '" + Session("_ZONECODE") + "'"
            dr = Com.ExecuteReader

            BranchDPL.Items.Add("")
            While dr.Read
                BranchDPL.Items.Add(Trim(dr(0).ToString))
            End While
        Catch ex As Exception
            DateErrorLbl.Text = "You are not Authrized!"
            DateErrorLbl.Visible = True
        End Try
        Con.Close()
        Com.Dispose()
    End Sub
    Protected Sub Clearbtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Clearbtn.Click
        RegionDPL.Enabled = True
        DivisionDPL.Enabled = True
        AreaDPL.Enabled = False
        BranchDPL.Enabled = False
        AreaDPL.Items.Clear()
        BranchDPL.Items.Clear()
        RegionDPL.Text = ""
        DivisionDPL.Text = ""
        AreaDPL.Text = ""
        BranchDPL.Text = ""
    End Sub
    Protected Sub DivisionDPL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DivisionDPL.SelectedIndexChanged
        RegionDPL.Enabled = False
        AreaDPL.Enabled = False
        BranchDPL.Enabled = False
        Searchbtn.Enabled = True
        RegionDPL.Text = ""
        AreaDPL.Items.Clear()
        BranchDPL.Items.Clear()
    End Sub
    Protected Sub RegionDPL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RegionDPL.SelectedIndexChanged
        Me.Session("Region") = RegionDPL.SelectedValue
        Searchbtn.Enabled = True
        AreaDPL.Enabled = True
        AreaDPL.Items.Clear()
        BranchDPL.Items.Clear()
        DivisionDPL.Enabled = False
        DivisionDPL.Text = ""
        area()
    End Sub
    Protected Sub Searchbtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Searchbtn.Click
        For i As Integer = 0 To gvRecipientsList.Columns.Count - 1
            For j As Integer = 0 To gvRecipientsList.Rows.Count - 1
                gvRecipientsList.Rows(j).Cells(i).Text = ""
            Next
        Next
        Dim sd As New DataSet
        If DivisionDPL.Text <> "" Then
            Select_RecipientsName_Division()
            sd = Select_RecipientsName()
        ElseIf RegionDPL.Text <> "" And AreaDPL.Text = "" And BranchDPL.Text = "" Then
            Select_RecipientsName_Region()
            sd = Select_RecipientsName()
        ElseIf RegionDPL.Text <> "" And AreaDPL.Text <> "" And BranchDPL.Text = "" Then
            Select_RecipientsName_Area()
            sd = Select_RecipientsName()
        ElseIf RegionDPL.Text <> "" And AreaDPL.Text <> "" And BranchDPL.Text <> "" Then
            Select_RecipientsName_Branch()
            sd = Select_RecipientsName()
        End If
        If Not sd Is Nothing Then
            gvRecipientsList.DataSource = sd.Tables(0)
            gvRecipientsList.DataBind()
            For x = 0 To gvRecipientsList.Rows.Count - 1
                If gvRecipientsList.Rows(x).Cells(3).Text = 1 Then
                    gvRecipientsList.Rows(x).Cells(3).Text = "Open"
                End If
            Next
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Not IsPostBack Then
            Dim ds As New DataSet
            ds = Select_Stat_Recipient()
            If Not ds Is Nothing Then
                gvRecipientsList.DataSource = ds.Tables(0)
                gvRecipientsList.DataBind()
            End If
            For x = 0 To gvRecipientsList.Rows.Count - 1
                If gvRecipientsList.Rows(x).Cells(3).Text = 1 Then
                    gvRecipientsList.Rows(x).Cells(3).Text = "Open"
                End If
            Next
            region()
            division()
        End If
    End Sub

    Protected Sub AreaDPL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AreaDPL.SelectedIndexChanged
        branch()
        BranchDPL.Enabled = True
        AreaDPL.Enabled = True
    End Sub

End Class
