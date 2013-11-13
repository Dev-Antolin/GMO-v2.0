Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports clsGMOUser
Imports System.Diagnostics
Imports System.Threading
Imports SendToMail_DLL
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Web.Services


Partial Class GMOCreate
    Inherits System.Web.UI.Page
    Public strTask As String = ""
    Public Employee As String = ""
    Dim DMemo As String = ""
    Dim CMemo As String = ""
    Dim xxx As String = ""
    Dim sFileV As String = "~/VisminImageMemo/"
    Dim sFileL As String = "~/LuzonImageMemo/"
    Dim currentfile As String = ""
    Dim MemoPath As String = ""
    Dim sFileServerVis As String = System.Web.HttpContext.Current.Server.MapPath("") & "\MemoImageVismin\"
    Dim sFileServerLuz As String = System.Web.HttpContext.Current.Server.MapPath("") & "\MemoImageLuzon\"
    Dim wee As Integer
    Dim sFileDirVis As String = System.Web.HttpContext.Current.Server.MapPath("") & "\VisminImageMemo\"
    Dim sFileDirLuz As String = System.Web.HttpContext.Current.Server.MapPath("") & "\LuzonImageMemo\"
    Dim xffV As Integer = System.IO.Directory.GetFiles(sFileDirVis).Length
    Dim xffL As Integer = System.IO.Directory.GetFiles(sFileDirLuz).Length

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("attachtype") = "attach" Then
            txtCircular.Text = Trim(Session("circularno").ToString)
            txtCircular.Enabled = False
            imgDisplay.Visible = False
            btnPrev.Visible = False
            btnNext.Visible = False
        End If

        Create_trxnumber()
        CheckLogin()
        txtSearch.Attributes.Add("onkeypress", "return Chars(event)")
        txtSubject.Attributes.Add("onkeypress", "return Chars(event)")
        btnSendD.Attributes.Add("onclick", "return skm_LockScreen('One Moment Please...');")
        lblCount.Visible = False
        txtTRXNumber.Visible = False
        If Not IsPostBack Then
            If txtSearch.Text = "" Then
                txtSearch.Visible = True
            Else
                txtSearch.Visible = False
            End If
        End If
        If Not IsPostBack Then
            ImageShow()
        End If
        txtCircular.Attributes.Add("onkeypress", "return numerals(event)")
    End Sub
    Private Sub CheckLogin()
        If Me.Session("_fullname") = "" Then
            Response.Redirect("Login.aspx")
        End If
    End Sub
    Protected Sub ddlRecipients_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRecipients.SelectedIndexChanged
        If ddlRecipients.Text = "" Then
            Response.Write("<script language=javascript>")
            Response.Write("alert('" & "You select blank space." & "')")
            Response.Write("</script>")
            Response.Write("<script language=javascript>")
            Response.Write("window.location = 'GMOCreate.aspx'")
            Response.Write("</script>")
            Exit Sub
        End If
        If ddlRecipients.Text = "Regional Manager" Then
            Dim dt As DataTable = DirectCast(ViewState("SelectedRecords"), DataTable)
            If Not dt Is Nothing Then
                dt.Clear()
                gvRecipientsL2S.DataSource = Nothing
                gvRecipientsL2S.DataBind()
            End If
            strTask = "Regional Man"
            Dim ds As New DataSet
            ds = Select_Regional()
            If Not ds Is Nothing Then
                gvRecipients.DataSource = ds.Tables(0)
                gvRecipients.DataBind()
            End If
            txtSearch.Visible = True
        End If
        If ddlRecipients.Text = "Area Manager" Then
            Dim dt As DataTable = DirectCast(ViewState("SelectedRecords"), DataTable)
            If Not dt Is Nothing Then
                dt.Clear()
                gvRecipientsL2S.DataSource = Nothing
                gvRecipientsL2S.DataBind()
            End If
            strTask = "Area Manager"
            Dim ds As New DataSet
            ds = Select_Regional()
            If Not ds Is Nothing Then
                gvRecipients.DataSource = ds.Tables(0)
                gvRecipients.DataBind()
            End If
            txtSearch.Visible = True
        End If
        If ddlRecipients.Text = "Branch Manager" Then
            Dim dt As DataTable = DirectCast(ViewState("SelectedRecords"), DataTable)
            If Not dt Is Nothing Then
                dt.Clear()
                gvRecipientsL2S.DataSource = Nothing
                gvRecipientsL2S.DataBind()
            End If
            strTask = "BM"
            Dim DS As New DataSet
            DS = Select_Regional()
            If Not DS Is Nothing Then
                gvRecipients.DataSource = DS.Tables(0)
                gvRecipients.DataBind()
            End If
            txtSearch.Visible = True
        End If
        If ddlRecipients.Text = "Division Manager" Then
            Dim dt As DataTable = DirectCast(ViewState("SelectedRecords"), DataTable)
            If Not dt Is Nothing Then
                dt.Clear()
                gvRecipientsL2S.DataSource = Nothing
                gvRecipientsL2S.DataBind()
            End If
            strTask = "DM"
            Dim DS As New DataSet
            DS = Select_Regional()
            If Not DS Is Nothing Then
                gvRecipients.DataSource = DS.Tables(0)
                gvRecipients.DataBind()
            End If
            txtSearch.Visible = True
        End If
    End Sub
    Public Function Select_Regional() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection()
        _ConnectionString()
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                If strTask = "Regional Man" Then
                    Dim x As String
                    x = "select (replace(rtrim(sur_name),'ñ','n')+', '+ replace(rtrim(first_name),'ñ','n')) as fullname " & _
                        "from irregionalmanagers " & _
                        "where zonecode='VISMIN' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
                If strTask = "Area Manager" Then
                    Dim x As String
                    x = "select (replace(rtrim(sur_name),'ñ','n')+', '+ replace(rtrim(first_name),'ñ','n')) as fullname " & _
                        "from irareamanagers " & _
                        "where zonecode='VISMIN' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
                If strTask = "BM" Then
                    Dim x As String
                    x = "select (replace(rtrim(sur_name),'ñ','n')+', '+ replace(rtrim(first_name),'ñ','n')) as fullname " & _
                        "from irbranchmanager " & _
                        "where zonecode='VISMIN' and task like '%" + strTask.Trim + "%' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
                If strTask = "DM" Then
                    Dim x As String
                    x = "select (replace(rtrim(wa.sur_name),'ñ','n')+', '+ replace(rtrim(wa.first_name),'ñ','n')) as fullname from webaccounts wa inner join irdivision ird on wa.costcenter = ird.costcenter where(wa.task like '%DIVMAN%' or wa.task='GMO-GENMAN') and wa.zonecode ='VISMIN' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
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
                If strTask = "Regional Man" Then
                    Dim x As String
                    x = "select (replace(rtrim(sur_name),'ñ','n')+', '+ replace(rtrim(first_name),'ñ','n')) as fullname " & _
                        "from irregionalmanagers " & _
                        "where zonecode='LUZON' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
                If strTask = "Area Manager" Then
                    Dim x As String
                    x = "select (replace(rtrim(sur_name),'ñ','n')+', '+ replace(rtrim(first_name),'ñ','n')) as fullname " & _
                        "from irareamanagers " & _
                        "where zonecode='LUZON' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
                If strTask = "BM" Then
                    Dim x As String
                    x = "select (replace(rtrim(sur_name),'ñ','n')+', '+ replace(rtrim(first_name),'ñ','n')) as fullname " & _
                        "from irbranchmanager " & _
                        "where zonecode='LUZON' and task like '%" + strTask.Trim + "%' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
                If strTask = "DM" Then
                    Dim x As String
                    x = "select (replace(rtrim(wa.sur_name),'ñ','n')+', '+ replace(rtrim(wa.first_name),'ñ','n')) as fullname from webaccounts wa inner join irdivision ird on wa.costcenter = ird.costcenter where(wa.task like '%DIVMAN%' or wa.task='GMO-GENMAN') and wa.zonecode ='LUZON' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
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
    Public Function Execute_Dataset(ByVal as_sql As String) As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim sqlConn As New SqlConnection
        Dim sqlAdapter As SqlDataAdapter
        Dim sqlDataset As New DataSet
        Execute_Dataset = Nothing
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
    Private Sub BindPrimaryGrid()
        Dim constr As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString()
        Dim query As String = "select (replace(rtrim(sur_name),'ñ','n')+', '+ replace(rtrim(first_name),'ñ','n')) as fullname from webaccounts"
        Dim con As New SqlConnection(constr)
        Dim sda As New SqlDataAdapter(query, con)
        Dim dt As New DataTable()
        sda.Fill(dt)
        gvRecipients.DataSource = dt
        gvRecipients.DataBind()
    End Sub
    Private Sub BindSecondaryGrid()
        Dim dt As DataTable = DirectCast(ViewState("SelectedRecords"), DataTable)
        gvRecipientsL2S.DataSource = dt
        gvRecipientsL2S.DataBind()
    End Sub
    Private Sub GetData()
        Dim dt As DataTable
        If ViewState("SelectedRecords") IsNot Nothing Then
            dt = DirectCast(ViewState("SelectedRecords"), DataTable)
        Else
            dt = CreateDataTable()
        End If
        Dim chkAll As CheckBox = DirectCast(gvRecipients.HeaderRow.Cells(0).FindControl("cbAll"), CheckBox)
        For i As Integer = 0 To gvRecipients.Rows.Count - 1
            If chkAll.Checked = True Then
                dt = AddRow(gvRecipients.Rows(i), dt)
                lblCount.ForeColor = Drawing.Color.Red
                lblCount.Text = dt.Rows.Count & " recipient(s) selected."
                txtSearch.Visible = True
            Else
                Dim chk As CheckBox = DirectCast(gvRecipients.Rows(i).Cells(0).FindControl("cb1"), CheckBox)
                'Select every row.
                If chk.Checked = True Then
                    dt = AddRow(gvRecipients.Rows(i), dt)
                    lblCount.ForeColor = Drawing.Color.Red
                    lblCount.Text = dt.Rows.Count & " recipient(s) selected."
                    txtSearch.Visible = True
                Else
                    dt = RemoveRow(gvRecipients.Rows(i), dt)
                    lblCount.ForeColor = Drawing.Color.Red
                    lblCount.Text = dt.Rows.Count & " recipient(s) selected."
                    txtSearch.Visible = True
                End If
            End If
        Next
        If lblCount.Text <> "0 recipient(s) selected." Then
            lblCount.Visible = True
        Else
            lblCount.Visible = False
        End If
        ViewState("SelectedRecords") = dt
    End Sub
    Private Sub SetData()
        Dim chkAll As CheckBox = DirectCast(gvRecipients.HeaderRow.Cells(0).FindControl("cbAll"), CheckBox)
        chkAll.Checked = True
        If ViewState("SelectedRecords") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("SelectedRecords"), DataTable)
            For i As Integer = 0 To gvRecipients.Rows.Count - 1
                Dim chk As CheckBox = DirectCast(gvRecipients.Rows(i).Cells(0).FindControl("cb1"), CheckBox)
                If chk IsNot Nothing Then
                    Dim dr As DataRow() = dt.[Select]("fullname = '" & gvRecipients.Rows(i).Cells(1).Text & "'")
                    chk.Checked = dr.Length > 0
                    If Not chk.Checked Then
                        chkAll.Checked = False
                    End If
                End If
            Next
        End If
    End Sub
    Private Function CreateDataTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("fullname")
        dt.AcceptChanges()
        Return dt
    End Function
    Private Function AddRow(ByVal gvRow As GridViewRow, ByVal dt As DataTable) As DataTable
        Dim dr As DataRow() = dt.Select("fullname = '" & gvRow.Cells(1).Text & "'")
        Dim cS As String = ""
        If dr.Length <= 0 Then
            dt.Rows.Add()
            dt.Rows(dt.Rows.Count - 1)("fullname") = gvRow.Cells(1).Text
            dt.AcceptChanges()
        End If
        Return dt
    End Function
    Private Function RemoveRow(ByVal gvRow As GridViewRow, ByVal dt As DataTable) As DataTable
        Dim dr As DataRow() = dt.Select("fullname = '" & gvRow.Cells(1).Text & "'")
        If dr.Length > 0 Then
            dt.Rows.Remove(dr(0))
            dt.AcceptChanges()
        End If
        Return dt
    End Function
    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GetData()
        gvRecipients.PageIndex = e.NewPageIndex
        BindPrimaryGrid()
        SetData()
    End Sub
    Protected Sub CheckBox_CheckChanged(ByVal sender As Object, ByVal e As EventArgs)
        GetData()
        SetData()
        BindSecondaryGrid()
    End Sub
    Public Function _selectFullname() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection()
        _ConnectionString()
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                If ddlRecipients.Text = "Regional Manager" Then
                    strTask = "Regional Man"
                    Dim x As String
                    x = "select (replace(rtrim(sur_name),'ñ','n')+', '+ replace(rtrim(first_name),'ñ','n')) as fullname from webaccounts where fullname like '%" + txtSearch.Text.Trim + "%' and task='" + strTask.Trim + "' and zonecode= 'vismin' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
                If ddlRecipients.Text = "Area Manager" Then
                    strTask = "Area Manager"
                    Dim x As String
                    x = "select (replace(rtrim(sur_name),'ñ','n')+', '+ replace(rtrim(first_name),'ñ','n')) as fullname from webaccounts where fullname like '%" + txtSearch.Text.Trim + "%' and task='" + strTask.Trim + "' and zonecode= 'vismin' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
                If ddlRecipients.Text = "Branch Manager" Then
                    strTask = "BM/BOSMAN"
                    Dim x As String
                    x = "select (replace(rtrim(sur_name),'ñ','n')+', '+ replace(rtrim(first_name),'ñ','n')) as fullname from webaccounts where fullname like '%" + txtSearch.Text.Trim + "%' and task='" + strTask.Trim + "' and zonecode= 'vismin' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
                If ddlRecipients.Text = "Division Manager" Then
                    strTask = "DM"
                    Dim x As String
                    'x = "select (replace(rtrim(wa.sur_name),'ñ','n')+', '+ replace(rtrim(wa.first_name),'ñ','n')) as fullname from webaccounts wa inner join irdivision ird on wa.costcenter = ird.costcenter where fullname like '%%' and (wa.task like ('%DIVMAN%') or wa.task='GMO-GENMAN') and wa.zonecode ='VISMIN' order by fullname asc"
                    x = "select (replace(rtrim(wa.sur_name),'ñ','n')+', '+ replace(rtrim(wa.first_name),'ñ','n')) as fullname from webaccounts wa inner join irdivision ird on wa.costcenter = ird.costcenter where fullname like '%" & txtSearch.Text & "%' and wa.task in ('HRMD-DIVMAN','SPD-ACTDIVMAN','IAD-DIVMAN','FD-DIVMAN','MMD-DIVMAN','MIS-DIVMAN','BOS-DIVMAN','FSD-DIVMAN','CAD-DIVMAN','MKTG-DIVMAN','ALL-DIVMAN','GMO-GENMAN') and wa.zonecode ='VISMIN' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
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
                If ddlRecipients.Text = "Regional Manager" Then
                    strTask = "Regional Man"
                    Dim x As String
                    x = "select (replace(rtrim(sur_name),'ñ','n')+', '+ replace(rtrim(first_name),'ñ','n')) as fullname from webaccounts where fullname like '%" + txtSearch.Text.Trim + "%' and task='" + strTask.Trim + "' and zonecode= 'luzon' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
                If ddlRecipients.Text = "Area Manager" Then
                    strTask = "Area Manager"
                    Dim x As String
                    x = "select (replace(rtrim(sur_name),'ñ','n')+', '+ replace(rtrim(first_name),'ñ','n')) as fullname from webaccounts where fullname like '%" + txtSearch.Text.Trim + "%' and task='" + strTask.Trim + "' and zonecode= 'luzon' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
                If ddlRecipients.Text = "Branch Manager" Then
                    strTask = "BM/BOSMAN"
                    Dim x As String
                    x = "select (replace(rtrim(sur_name),'ñ','n')+', '+ replace(rtrim(first_name),'ñ','n')) as fullname from webaccounts where fullname like '%" + txtSearch.Text.Trim + "%' and task='" + strTask.Trim + "' and zonecode= 'luzon' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
                If ddlRecipients.Text = "Division Manager" Then
                    strTask = "DM"
                    Dim x As String
                    x = "select (replace(rtrim(wa.sur_name),'ñ','n')+', '+ replace(rtrim(wa.first_name),'ñ','n')) as fullname from webaccounts wa inner join irdivision ird on wa.costcenter = ird.costcenter where fullname like '%" & txtSearch.Text & "%' and wa.task in ('HRMD-DIVMAN','SPD-ACTDIVMAN','IAD-DIVMAN','FD-DIVMAN','MMD-DIVMAN','MIS-DIVMAN','BOS-DIVMAN','FSD-DIVMAN','CAD-DIVMAN','MKTG-DIVMAN','ALL-DIVMAN','GMO-GENMAN') and wa.zonecode ='LUZON' order by fullname asc"
                    ds = Execute_Dataset(x)
                End If
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
    Public Sub Create_trxnumber()
        Dim numRegion As Integer
        Dim _Mnth As String
        Dim _Yr As String
        Dim _Increment As String = ""
        Dim ds As New DataSet

        If Trim(Me.Session("_task")) = "GMO-ASTGENMAN" AndAlso Trim(Me.Session("_zonecode")) = "VISMIN" Then
            numRegion = 2
        Else
            numRegion = 1
        End If

        _Mnth = Format(Date.Now, "MM")
        _Yr = Format(Date.Now, "yyyy")

        _Increment = Auto_trxnumber(numRegion)
        If _Increment = "error" Then
            Exit Sub
        End If

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            txtTRXNumber.Text = numRegion & _Mnth & _Yr & _Increment
        Else
            txtTRXNumber.Text = numRegion & _Mnth & _Yr & _Increment
        End If
    End Sub
    Public Function Auto_trxnumber(ByVal numRegion As Integer) As String
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        Dim _Yr As String
        _Yr = Format(Date.Now, "yyyy")
        _DatabaseConnection1()
        _ConnectionString1()
        Try
            Dim b As String = ""
            If numRegion = 1 Then
                b = "select max(trxnumber) +1 as auto_num from memoheader where trxnumber like '1%' and trxnumber like '%" & _Yr.Trim & "%'"
            ElseIf numRegion = 2 Then
                b = "select max(trxnumber) +1 as auto_num from memoheader where trxnumber like '2%' and trxnumber like '%" & _Yr.Trim & "%'"
            End If
            If b <> "" Then
                ds = Execute_Dataset1(b)
                If Not ds Is Nothing AndAlso ds.Tables(0).Rows(0).Item(0).ToString.Trim <> "" Then
                    Return ds.Tables(0).Rows(0).Item(0).ToString.Substring(7, 3)
                Else
                    Return "001"
                End If
            Else
                Return "error"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Con.Close()
            Com.Dispose()
            Return "error"
        End Try
        Con.Close()
        Com.Dispose()
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
                    Execute_Dataset1 = sqlDataset
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
    End Function
    Protected Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If txtSearch.Text = "Search Name..." Or txtSearch.Text = "" Then
            Dim sd As DataSet
            sd = _selectFullname()
            If Not sd Is Nothing Then
                gvRecipients.DataSource = sd.Tables(0)
                gvRecipients.DataBind()
                txtSearch.Visible = True
                txtSearch.Focus()
                txtSearch.BackColor = Drawing.Color.White
            End If
            Exit Sub
        Else
            Dim ds As DataSet
            ds = _selectFullname()
            If Not ds Is Nothing Then
                gvRecipients.DataSource = ds.Tables(0)
                gvRecipients.DataBind()
                txtSearch.Visible = True
                txtSearch.Focus()
                txtSearch.BackColor = Drawing.Color.White
                Exit Sub
            Else
                lblCount.Visible = True
                lblCount.ForeColor = Drawing.Color.Red
                txtSearch.Visible = True
                txtSearch.BackColor = Drawing.Color.Pink
                txtSearch.Focus()
                lblCount.Text = "No record found."
            End If
        End If
    End Sub
    Public Function Insert_Creator() As Boolean
        Dim oCom As New SqlCommand
        Dim oTrans As SqlTransaction
        Dim Con As New SqlConnection
        DMemo = Format(Date.Now, "yyyy-MM-dd hh:mm:ss tt")
        CMemo = Format(Date.Now, "yyyy-MM-dd hh:mm:ss tt")
        Dim MemoFrom As String = "GM's OFFICE"
        Dim a As String = "insert into memoheader(trxnumber, createdby, circularNumber, subject, MemoFrom, " & _
                          "MemoPath, Memodate, createdate, zonecode)values" & _
                          "('" + txtTRXNumber.Text.Trim + "', '" + Trim(Me.Session("_fullname")) + "'," & _
                          "'" + txtCircular.Text.Trim + "', '" + txtSubject.Text.Trim.Replace("'", "''") + "'," & _
                          "'" + MemoFrom.Trim.Replace("'", "''") + "', '" + MemoPath.Trim.Replace("'", "''") + "'," & _
                          "'" + DMemo.Trim + "', '" + CMemo.Trim + "', '" + Trim(Me.Session("_zonecode")) + "')"
        Con.ConnectionString = constr2
        If Con.State = ConnectionState.Closed Then
            Con.Open()
            oTrans = Con.BeginTransaction()
            oCom = Con.CreateCommand
            oCom.CommandTimeout = 0
            oCom.Transaction = oTrans
            Try
                oCom.CommandText = a
                oCom.ExecuteNonQuery()
                oTrans.Commit()
                oCom.Dispose()
                Return True
            Catch ex As Exception
                oTrans.Rollback()
                Con.Close()
                oCom.Dispose()
                Return False
            End Try
        End If
    End Function
    Public Sub Delete_Creator()
        Dim oCom As New SqlCommand
        Dim oTrans As SqlTransaction
        Dim Con As New SqlConnection
        Dim a As String = "delete from memoheader where trxnumber = '" + txtTRXNumber.Text.Trim + "'"
        Con.ConnectionString = constr2
        If Con.State = ConnectionState.Closed Then
            Con.Open()
            oTrans = Con.BeginTransaction()
            oCom = Con.CreateCommand
            oCom.CommandTimeout = 0
            oCom.Transaction = oTrans
            Try
                oCom.CommandText = a
                oCom.ExecuteNonQuery()
                oTrans.Commit()
                oCom.Dispose()
            Catch ex As Exception
                MsgBox(ex.Message())
                oTrans.Rollback()
                Con.Close()
                oCom.Dispose()
                Exit Sub
            Finally
                Con.Close()
            End Try
        End If
    End Sub
    Public Function ActionSave() As Boolean
        Dim RM As String = ""
        Dim AM As String = ""
        Dim RM2 As String = ""
        Dim EA As String = ""
        Dim EA2 As String = ""
        Dim EA3 As String = ""
        Dim EA4 As String = ""
        Dim Memo As String = "1 memo received"
        Dim r_s As Integer = Nothing
        Dim r_r As String = ""
        Dim r_b As String = ""
        Dim D As GridViewRow
        Dim row As Integer = 0
        Dim send As New SendToEmail
        If gvRecipientsL2S.Rows.Count <> 0 Then
            D = gvRecipientsL2S.Rows(0)
        Else
            lblCount.Visible = True
            lblCount.ForeColor = Drawing.Color.Red
            lblCount.Text = "No Recipient(s) to send!"
            txtSearch.BackColor = Drawing.Color.White
            txtSearch.Visible = True
            Delete_Creator()
            Exit Function
        End If
        For Each D In gvRecipientsL2S.Rows
            Try
                Employee = D.Cells(row).Text.Trim()
                Dim oCom As New SqlCommand
                Dim oTrans As SqlTransaction
                Dim Con As New SqlConnection
                Dim MemoTo As String = "GM's OFFICE"
                If ddlRecipients.Text = "Division Manager" Then
                    Dim a As String = "insert into memodivision(trxnumber, memoto, division, confirmedreceived, confirmeddate, " & _
                    "status, statmodifydate, statmodifyby)values('" + txtTRXNumber.Text.Trim + "'," & _
                    "'" + MemoTo.Trim.Replace("'", "''") + "','" + Employee.Trim + "', '', '', '', '', '')"
                    Con.ConnectionString = constr2
                    If Con.State = ConnectionState.Closed Then
                        Con.Open()
                        oTrans = Con.BeginTransaction()
                        oCom = Con.CreateCommand
                        oCom.CommandTimeout = 0
                        oCom.Transaction = oTrans
                        Try
                            oCom.CommandText = a
                            oCom.ExecuteNonQuery()
                            oTrans.Commit()
                            oCom.Dispose()
                        Catch ex As Exception
                            MsgBox(ex.Message())
                            oTrans.Rollback()
                            Con.Close()
                            oCom.Dispose()
                            Return False
                        Finally
                            Con.Close()
                        End Try
                    End If
                    Dim dis As New DataSet
                    dis = Select_Emailadd4()
                    If Not dis Is Nothing Then
                        EA4 = dis.Tables(0).Rows(0).Item(0).ToString.Trim
                        If send.send(SMTPAdmin.Trim, EA4.Trim, "", Memo.Trim, SMTPSubject.Trim, "", SMTPServer.Trim, SMTPUser.Trim, SMTPPass.Trim) = False Then
                            'lblCount.Visible = True
                            'lblCount.ForeColor = Drawing.Color.Red
                            'lblCount.Text = "Email add of recipient(s) not exist!."
                            'txtSearch.Visible = True
                            'Delete_Creator()
                            'Return False
                            lblCount.Text = Employee.Trim + "" + +"have no email."
                        End If
                    End If
                End If

                If ddlRecipients.Text = "Regional Manager" Then
                    Dim a As String = "insert into memoregion(trxnumber, memoto, region, confirmedreceived, confirmeddate, " & _
                    "status, statmodifydate, statmodifyby)values('" + txtTRXNumber.Text.Trim + "'," & _
                    "'" + MemoTo.Trim.Replace("'", "''") + "','" + Employee.Trim + "', '', '', '', '', '')"
                    Con.ConnectionString = constr2
                    If Con.State = ConnectionState.Closed Then
                        Con.Open()
                        oTrans = Con.BeginTransaction()
                        oCom = Con.CreateCommand
                        oCom.CommandTimeout = 0
                        oCom.Transaction = oTrans
                        Try
                            oCom.CommandText = a
                            oCom.ExecuteNonQuery()
                            oTrans.Commit()
                            oCom.Dispose()
                        Catch ex As Exception
                            MsgBox(ex.Message())
                            oTrans.Rollback()
                            Con.Close()
                            oCom.Dispose()
                            Return False
                        Finally
                            Con.Close()
                        End Try
                    End If
                    Dim dis As New DataSet
                    dis = Select_Emailadd1()
                    If Not dis Is Nothing Then
                        EA = dis.Tables(0).Rows(0).Item(0).ToString.Trim
                        If send.send(SMTPAdmin.Trim, EA.Trim, "", Memo.Trim, SMTPSubject.Trim, "", SMTPServer.Trim, SMTPUser.Trim, SMTPPass.Trim) = False Then
                            'lblCount.Visible = True
                            'lblCount.ForeColor = Drawing.Color.Red
                            'lblCount.Text = "Email add of recipient(s) not exist!."
                            'txtSearch.Visible = True
                            'Delete_Creator()
                            'Return False
                            lblCount.Text = Employee.Trim + "" + +"have no email."
                        End If
                    End If
                End If
                If ddlRecipients.Text = "Area Manager" Then
                    Dim ds As New DataSet
                    ds = CallRegion()
                    If Not ds Is Nothing Then
                        RM = ds.Tables(0).Rows(0).Item(0).ToString.Trim
                    Else
                        RM = ""
                    End If
                    Dim b As String = "insert into memoarea(trxnumber, memoto, area, confirmedreceived," & _
                    "confirmeddate, status, statmodifydate, statmodifyby)values('" + txtTRXNumber.Text.Trim + "'," & _
                    "'" + MemoTo.Trim.Replace("'", "''") + "','" + Employee.Trim + "', '', '', '', '', '')"
                    Dim b1 As String = "insert into memoregion(trxnumber, memoto, region, confirmedreceived," & _
                    "confirmeddate, status, statmodifydate, statmodifyby)values('" + txtTRXNumber.Text.Trim + "'," & _
                    "'" + MemoTo.Trim.Replace("'", "''") + "','" + RM.Trim + "', '', '', '', '', '')"
                    Con.ConnectionString = constr2
                    If Con.State = ConnectionState.Closed Then
                        Con.Open()
                        oTrans = Con.BeginTransaction()
                        oCom = Con.CreateCommand
                        oCom.CommandTimeout = 0
                        oCom.Transaction = oTrans
                        Try
                            oCom.CommandText = b
                            oCom.ExecuteNonQuery()
                            oCom.CommandText = b1
                            oCom.ExecuteNonQuery()
                            oTrans.Commit()
                            oCom.Dispose()
                        Catch ex As Exception
                            MsgBox(ex.Message())
                            oTrans.Rollback()
                            Con.Close()
                            oCom.Dispose()
                            Return False
                        Finally
                            Con.Close()
                        End Try
                    End If

                    Dim sid As New DataSet
                    sid = Select_Emailadd2()
                    If Not sid Is Nothing Then
                        EA = sid.Tables(0).Rows(0).Item(0).ToString.Trim
                        If r_s = 0 Then
                            EA2 = sid.Tables(0).Rows(0).Item(1).ToString.Trim
                        Else
                            If r_r = EA2 Then
                                EA2 = ""
                            Else
                                EA2 = sid.Tables(0).Rows(0).Item(1).ToString.Trim
                            End If
                        End If
                        If send.send(SMTPAdmin.Trim, EA.Trim, EA2.Trim, Memo.Trim, SMTPSubject.Trim, "", SMTPServer.Trim, SMTPUser.Trim, SMTPPass.Trim) = False Then
                            'lblCount.Visible = True
                            'lblCount.ForeColor = Drawing.Color.Red
                            'lblCount.Text = "Email add of recipient(s) not exist!."
                            'txtSearch.Visible = True
                            'Delete_Creator()
                            'Return False
                            lblCount.Text = Employee.Trim + " " + RM.Trim + " have no email."
                        End If
                        r_r = EA2
                        r_s = r_s + 1
                    End If
                End If
                If ddlRecipients.Text = "Branch Manager" Then
                    Dim ds As New DataSet
                    ds = CallAreaRegion()
                    If Not ds Is Nothing Then
                        AM = ds.Tables(0).Rows(0).Item(0).ToString.Trim
                        RM = ds.Tables(0).Rows(0).Item(1).ToString.Trim
                    Else
                        AM = ""
                        RM = ""
                    End If
                    Dim c As String = "insert into memobranch(trxnumber, memoto, Branch, confirmedreceived," & _
                    "confirmeddate, status, statmodifydate, statmodifyby)values('" + txtTRXNumber.Text.Trim + "'," & _
                    "'" + MemoTo.Trim.Replace("'", "''") + "','" + Employee.Trim + "', '', '', '','', '')"
                    Dim c2 As String = "insert into memoarea(trxnumber, memoto, area, confirmedreceived," & _
                    "confirmeddate, status, statmodifydate, statmodifyby)values('" + txtTRXNumber.Text.Trim + "'," & _
                    "'" + MemoTo.Trim.Replace("'", "''") + "','" + AM.Trim + "', '', '', '', '', '')"
                    Dim c3 As String = "insert into memoregion(trxnumber, memoto, region, confirmedreceived," & _
                    "confirmeddate, status, statmodifydate, statmodifyby)values('" + txtTRXNumber.Text.Trim + "'," & _
                    "'" + MemoTo.Trim.Replace("'", "''") + "','" + RM.Trim + "', '', '', '', '', '')"
                    Con.ConnectionString = constr2
                    If Con.State = ConnectionState.Closed Then
                        Con.Open()
                        oTrans = Con.BeginTransaction()
                        oCom = Con.CreateCommand
                        oCom.CommandTimeout = 0
                        oCom.Transaction = oTrans
                        Try
                            oCom.CommandText = c
                            oCom.ExecuteNonQuery()
                            oCom.CommandText = c2
                            oCom.ExecuteNonQuery()
                            oCom.CommandText = c3
                            oCom.ExecuteNonQuery()
                            oTrans.Commit()
                            oCom.Dispose()
                        Catch ex As Exception
                            MsgBox(ex.Message())
                            oTrans.Rollback()
                            Con.Close()
                            oCom.Dispose()
                            Return False
                        Finally
                            Con.Close()
                        End Try
                    End If
                    Dim dise As New DataSet
                    dise = Select_Emailadd3()
                    If Not dise Is Nothing Then
                        EA = dise.Tables(0).Rows(0).Item(0).ToString.Trim
                        If r_s = 0 Then
                            EA2 = dise.Tables(0).Rows(0).Item(1).ToString.Trim
                            EA3 = dise.Tables(0).Rows(0).Item(2).ToString.Trim
                        Else
                            If r_r = EA2 Then
                                EA2 = ""
                            Else
                                EA2 = dise.Tables(0).Rows(0).Item(1).ToString.Trim
                            End If
                            If r_b = EA3 Then
                                EA3 = ""
                            Else
                                EA3 = dise.Tables(0).Rows(0).Item(2).ToString.Trim
                            End If
                        End If
                        If send.send(SMTPAdmin.Trim, EA.Trim, EA2.Trim & "," & EA3.Trim, Memo.Trim, SMTPSubject.Trim, "", SMTPServer.Trim, SMTPUser.Trim, SMTPPass.Trim) = False Then
                            'lblCount.Visible = True
                            'lblCount.ForeColor = Drawing.Color.Red
                            'lblCount.Text = "Email add of recipient(s) not exist!."
                            'txtSearch.Visible = True
                            'Delete_Creator()
                            'Return False
                            lblCount.Text = Employee.Trim + "" + +RM.Trim + AM.Trim + "have no email."
                        End If
                        r_r = EA2
                        r_b = EA3
                        r_s = r_s + 1
                    End If
                End If
            Catch ex As Exception
                'MsgBox(ex.Message())
                'Return False
                lblCount.Text = "have no email."
            End Try
        Next
        Return True
    End Function
    Public Function CallRegion() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection()
        _ConnectionString()

        Try
            If Trim(Me.Session("_zonecode")) = "VISMIN" Then
                Dim x As String
                x = "select top 1(select top 1(replace(rtrim(rm.sur_name),'ñ','n')+', '+ replace(rtrim(rm.first_name),'ñ','n')) " & _
                    "from IRRegionalmanagers rm where rm.Class_03 = wb.Class_03 AND rm.zonecode=wa.zonecode) AS RMName " & _
                    "from WebAccounts wa " & _
                    "INNER JOIN WebBranches wb ON wa.comp=wb.bedrnr and wa.zonecode = wb.zonecode " & _
                    "WHERE (select (replace(rtrim(wa.sur_name),'ñ','n')+', '+ replace(rtrim(wa.first_name),'ñ','n')) as fullname) = '" + Employee.Trim + "'"
                ds = Execute_Dataset(x)
            End If
            If Trim(Me.Session("_zonecode")) = "LUZON" Then
                Dim x As String
                x = "select top 1(select top 1(replace(rtrim(rm.sur_name),'ñ','n')+', '+ replace(rtrim(rm.first_name),'ñ','n')) " & _
                    "from IRRegionalmanagers rm where rm.Class_03 = wb.Class_03 AND rm.zonecode=wa.zonecode) AS RMName " & _
                    "from WebAccounts wa " & _
                    "INNER JOIN WebBranches wb ON wa.comp=wb.bedrnr and wa.zonecode = wb.zonecode " & _
                    "WHERE (select (replace(rtrim(wa.sur_name),'ñ','n')+', '+ replace(rtrim(wa.first_name),'ñ','n')) as fullname) = '" + Employee.Trim + "'"
                ds = Execute_Dataset(x)
            End If
        Catch ex As Exception
            Con.Close()
            Com.Dispose()
        End Try
        Con.Close()
        Com.Dispose()
        Return ds
    End Function
    Public Function CallAreaRegion() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection()
        _ConnectionString()

        Try
            If Trim(Me.Session("_zonecode")) = "VISMIN" Then
                Dim x As String
                x = "select top 1(select top 1(replace(rtrim(am.sur_name),'ñ','n')+', '+ replace(rtrim(am.first_name),'ñ','n')) " & _
                    "from IRAreamanagers as am where am.Class_04 = wb.Class_04 AND am.zonecode=wa.zonecode) AS AMName, " & _
                    "(select top 1(replace(rtrim(rm.sur_name),'ñ','n')+', '+ replace(rtrim(rm.first_name),'ñ','n')) " & _
                    "from IRRegionalmanagers rm where rm.Class_03 = wb.Class_03 AND rm.zonecode=wa.zonecode) AS RMName " & _
                    "from WebAccounts wa " & _
                    "INNER JOIN WebBranches wb ON wa.comp=wb.bedrnr and wa.zonecode = wb.zonecode " & _
                    "WHERE (select (replace(rtrim(wa.sur_name),'ñ','n')+', '+ replace(rtrim(wa.first_name),'ñ','n')) as fullname) = '" + Employee.Trim + "'"
                ds = Execute_Dataset(x)
            End If
            If Trim(Me.Session("_zonecode")) = "LUZON" Then
                Dim x As String
                x = "select top 1(select top 1(replace(rtrim(am.sur_name),'ñ','n')+', '+ replace(rtrim(am.first_name),'ñ','n')) " & _
                    "from IRAreamanagers as am where am.Class_04 = wb.Class_04 AND am.zonecode=wa.zonecode) AS AMName, " & _
                    "(select top 1(replace(rtrim(rm.sur_name),'ñ','n')+', '+ replace(rtrim(rm.first_name),'ñ','n')) " & _
                    "from IRRegionalmanagers rm where rm.Class_03 = wb.Class_03 AND rm.zonecode=wa.zonecode) AS RMName " & _
                    "from WebAccounts wa " & _
                    "INNER JOIN WebBranches wb ON wa.comp=wb.bedrnr and wa.zonecode = wb.zonecode " & _
                    "WHERE (select (replace(rtrim(wa.sur_name),'ñ','n')+', '+ replace(rtrim(wa.first_name),'ñ','n')) as fullname) = '" + Employee.Trim + "'"
                ds = Execute_Dataset(x)
            End If
        Catch ex As Exception
            Con.Close()
            Com.Dispose()
        End Try
        Con.Close()
        Com.Dispose()
        Return ds
    End Function
    Protected Sub btnSendD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendD.Click
        If HttpContext.Current.Session("validateCircular") = "false" Then
            Exit Sub
        End If
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Dim xname() As String = System.IO.Directory.GetFiles(sFileDirVis, "*.jpg")
            Dim y As Integer
            Dim path As String = ""
            For y = 0 To xname.Count - 1
                path += xname(y).Replace(sFileDirVis, sFileServerVis).ToString & ","
            Next
            MemoPath = path.Substring(0, path.Length - 1)
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Dim xname() As String = System.IO.Directory.GetFiles(sFileDirLuz, "*.jpg")
            Dim y As Integer
            Dim path As String = ""
            For y = 0 To xname.Count - 1
                path += xname(y).Replace(sFileDirLuz, sFileServerLuz).ToString & ","
            Next
            MemoPath = path.Substring(0, path.Length - 1)
        End If
        If ddlRecipients.Text = "" Then
            lblCount.Visible = True
            lblCount.ForeColor = Drawing.Color.Red
            lblCount.Text = "Insert Recipients!"
            Exit Sub
        End If
        If txtCircular.Text = "" Then
            lblCount.Visible = True
            lblCount.ForeColor = Drawing.Color.Red
            lblCount.Text = "Fill up circular!"
            txtSearch.Visible = True
            Exit Sub
        End If
        If txtSubject.Text = "" Then
            lblCount.Visible = True
            lblCount.ForeColor = Drawing.Color.Red
            lblCount.Text = "No Subject!"
            txtSearch.Visible = True
            Exit Sub
        End If
        If Insert_Creator() Then
            If ActionSave() Then
                If Trim(Me.Session("_zonecode")) = "VISMIN" Then
                    MoveFiles(sFileDirVis, sFileServerVis)
                End If
                If Trim(Me.Session("_zonecode")) = "LUZON" Then
                    MoveFiles(sFileDirLuz, sFileServerLuz)
                End If
                Response.Write("<script language=javascript>")
                Response.Write("alert('" & "Memo file is sent successfully!" & "')")
                Response.Write("</script>")
                Response.Write("<script language=javascript>")
                Response.Write("window.location = 'GMOStartScan.aspx'")
                Response.Write("</script>")
            End If
        Else
            lblCount.Visible = True
            lblCount.ForeColor = Drawing.Color.Red
            lblCount.Text = "Error upon inserting data."
            Exit Sub
        End If
    End Sub
    Sub ImageShow()
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Dim xname() As String = System.IO.Directory.GetFiles(sFileDirVis)
            Dim y As Integer
            currentfile = xname(y)
            If currentfile.EndsWith(".jpg") = True OrElse currentfile.EndsWith(".jpeg") = True Then
                If xffV = 0 Then
                    imgDisplay.ImageUrl = "~/Images/Scan Images.jpg"
                    Delete_Creator()
                Else
                    xxx = sFileV + System.IO.Path.GetFileName(currentfile)
                    imgDisplay.ImageUrl = xxx
                End If
            End If
            If wee = 0 Then
                btnPrev.Enabled = False
            End If
            If wee = xffV - 2 Then
                btnNext.Enabled = False
            Else
                btnNext.Enabled = True
            End If
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Dim xname() As String = System.IO.Directory.GetFiles(sFileDirLuz)
            Dim y As Integer
            currentfile = xname(y)
            If currentfile.EndsWith(".jpg") = True OrElse currentfile.EndsWith(".jpeg") = True Then
                If xffL = 0 Then
                    imgDisplay.ImageUrl = "~/Images/Scan Images.jpg"
                    Delete_Creator()
                Else
                    xxx = sFileL + System.IO.Path.GetFileName(currentfile)
                    imgDisplay.ImageUrl = xxx
                End If
            End If
            If wee = 0 Then
                btnPrev.Enabled = False
            End If
            If wee = xffL - 2 Then
                btnNext.Enabled = False
            Else
                btnNext.Enabled = True
            End If
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
    Public Function Select_Emailadd4() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection()
        _ConnectionString()
        Try
            Dim x As String
            x = "SELECT top 1(SELECT TOP 1 wa.emailAdd from IRdivision rm " & _
                "where rm.Class_03 = wb.Class_03 AND rm.zonecode=wa.zonecode) AS RMName " & _
                "from WebAccounts wa " & _
                "INNER JOIN WebBranches wb ON wa.comp=wb.bedrnr and wa.zonecode = wb.zonecode " & _
                "WHERE (select (replace(rtrim(wa.sur_name),'ñ','n')+', '+ replace(rtrim(wa.first_name),'ñ','n')) as fullname) = '" + Employee.Trim + "'"
            ds = Execute_Dataset(x)
        Catch ex As Exception
            Con.Close()
            Com.Dispose()
        End Try
        Con.Close()
        Com.Dispose()
        Return ds
    End Function
    Public Function Select_Emailadd1() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection()
        _ConnectionString()
        Try
            Dim x As String
            x = "SELECT top 1(SELECT TOP 1 wa.emailAdd from IRRegionalmanagers rm " & _
                "where rm.Class_03 = wb.Class_03 AND rm.zonecode=wa.zonecode) AS RMName " & _
                "from WebAccounts wa " & _
                "INNER JOIN WebBranches wb ON wa.comp=wb.bedrnr and wa.zonecode = wb.zonecode " & _
                "WHERE (select (replace(rtrim(wa.sur_name),'ñ','n')+', '+ replace(rtrim(wa.first_name),'ñ','n')) as fullname) = '" + Employee.Trim + "'"
            ds = Execute_Dataset(x)
        Catch ex As Exception
            Con.Close()
            Com.Dispose()
        End Try
        Con.Close()
        Com.Dispose()
        Return ds
    End Function
    Public Function Select_Emailadd2() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection()
        _ConnectionString()
        Try
            Dim x As String
            x = "SELECT top 1(SELECT TOP 1 wa.emailAdd from IRAreamanagers am " & _
                "where am.Class_04 = wb.Class_04 AND am.zonecode=wa.zonecode) AS AMName, " & _
                "(SELECT TOP 1 rm.emailAdd from IRRegionalmanagers rm " & _
                "where rm.Class_03 = wb.Class_03 AND rm.zonecode=wa.zonecode) AS RMName " & _
                "from WebAccounts wa " & _
                "INNER JOIN WebBranches wb ON wa.comp=wb.bedrnr and wa.zonecode = wb.zonecode " & _
                "WHERE (select (replace(rtrim(wa.sur_name),'ñ','n')+', '+ replace(rtrim(wa.first_name),'ñ','n')) as fullname) = '" + Employee.Trim + "'"
            ds = Execute_Dataset(x)
        Catch ex As Exception
            Con.Close()
            Com.Dispose()
        End Try
        Con.Close()
        Com.Dispose()
        Return ds
    End Function
    Public Function Select_Emailadd3() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection()
        _ConnectionString()
        Try
            Dim x As String
            x = "SELECT top 1(SELECT TOP 1 wa.emailAdd FROM IRBranchManager bm " & _
                "WHERE bm.comp=wb.bedrnr AND bm.zonecode=wa.zonecode ) AS BMName, " & _
                "(SELECT TOP 1 am.emailAdd from IRAreamanagers am " & _
                "where am.Class_04 = wb.Class_04 AND am.zonecode=wa.zonecode) AS AMName, " & _
                "(SELECT TOP 1 rm.emailAdd from IRRegionalmanagers rm " & _
                "where rm.Class_03 = wb.Class_03 AND rm.zonecode=wa.zonecode) AS RMName " & _
                "from WebAccounts wa " & _
                "INNER JOIN WebBranches wb ON wa.comp=wb.bedrnr and wa.zonecode = wb.zonecode " & _
                "WHERE (select (replace(rtrim(wa.sur_name),'ñ','n')+', '+ replace(rtrim(wa.first_name),'ñ','n')) as fullname) = '" + Employee.Trim + "'"
            ds = Execute_Dataset(x)
        Catch ex As Exception
            Con.Close()
            Com.Dispose()
        End Try
        Con.Close()
        Com.Dispose()
        Return ds
    End Function
    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Dim xnamesx() As String = System.IO.Directory.GetFiles(sFileDirVis)
            If Me.Session("Oyeh") < xffV Then
                wee = Me.Session("Oyeh")
                wee = wee + 1
                If wee = xffV - 1 Then
                    btnNext.Enabled = False
                Else
                    If wee = xffV - 2 Then
                        btnNext.Enabled = False
                    Else
                        btnNext.Enabled = True
                    End If
                End If
                currentfile = xnamesx(wee)
                xxx = sFileV + System.IO.Path.GetFileName(currentfile)
                imgDisplay.ImageUrl = xxx
                Me.Session.Add("Oyeh", wee)
                btnPrev.Enabled = True
            End If
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Dim xnamesx() As String = System.IO.Directory.GetFiles(sFileDirLuz)
            If Me.Session("Oyeh") < xffL Then
                wee = Me.Session("Oyeh")
                wee = wee + 1
                If wee = xffL - 1 Then
                    btnNext.Enabled = False
                Else
                    If wee = xffL - 2 Then
                        btnNext.Enabled = False
                    Else
                        btnNext.Enabled = True
                    End If
                End If
                currentfile = xnamesx(wee)
                xxx = sFileL + System.IO.Path.GetFileName(currentfile)
                imgDisplay.ImageUrl = xxx
                Me.Session.Add("Oyeh", wee)
                btnPrev.Enabled = True
            End If
        End If
    End Sub
    Protected Sub btnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Dim xnamesx() As String = System.IO.Directory.GetFiles(sFileDirVis)
            Me.Session("Oyeh") = Me.Session("Oyeh") - 1
            If Me.Session("Oyeh") <= 0 Then
                btnPrev.Enabled = False
            Else
                btnPrev.Enabled = True
            End If
            currentfile = xnamesx(Me.Session("Oyeh"))
            xxx = sFileV + System.IO.Path.GetFileName(currentfile)
            imgDisplay.ImageUrl = xxx
            btnNext.Enabled = True
        End If
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Dim xnamesx() As String = System.IO.Directory.GetFiles(sFileDirLuz)
            Me.Session("Oyeh") = Me.Session("Oyeh") - 1
            If Me.Session("Oyeh") <= 0 Then
                btnPrev.Enabled = False
            Else
                btnPrev.Enabled = True
            End If
            currentfile = xnamesx(Me.Session("Oyeh"))
            xxx = sFileL + System.IO.Path.GetFileName(currentfile)
            imgDisplay.ImageUrl = xxx
            btnNext.Enabled = True
        End If
    End Sub
    Public Sub MoveFiles(ByVal sourcePath As String, ByVal DestinationPath As String)
        If (Directory.Exists(sourcePath)) Then
            For Each fName As String In Directory.GetFiles(sourcePath)
                If File.Exists(fName) Then
                    Dim dFile As String = String.Empty
                    dFile = Path.GetFileName(fName)
                    If dFile = "Thumbs.db" Then
                        Exit Sub
                    End If
                    Dim dFilePath As String = String.Empty
                    dFilePath = DestinationPath + dFile
                    File.Move(fName, dFilePath)
                End If
            Next
        End If
    End Sub
    <WebMethod()> _
   Public Sub validateCircular(ByVal sender As Object, ByVal args As ServerValidateEventArgs)
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
            'Return "no connection"
        End Try

        args.IsValid = True
        HttpContext.Current.Session("validateCircular") = "true"
        Try
            Com = Con.CreateCommand
            Com.CommandText = "select circularnumber from memoheader where circularnumber = '" & txtCircular.Text & "'"
            dr = Com.ExecuteReader
            If Not dr Is Nothing Then
                While dr.Read
                    args.IsValid = False
                    HttpContext.Current.Session("validateCircular") = "false"
                End While
            End If
            dr.Close()
        Catch ex As Exception
            'Return "you're not authorized"
        End Try
        Con.Close()
        Com.Dispose()
    End Sub
End Class
