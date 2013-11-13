Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports clsGMOUser
Partial Class GMOMemorandumDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnRefresh.Attributes.Add("onclick", "return skm_LockScreen('One Moment Please...');")
        If Not IsPostBack Then
            Dim Data As DataSet

            Data = Select_RecipientList()
            If Not Data Is Nothing Then
                cbAM.Enabled = False
                cbRM.Enabled = False
                cbBM.Enabled = False
                cbDM.Enabled = False
                For x = 0 To Data.Tables(0).Rows.Count - 1
                    Dim datadiv As DataSet
                    datadiv = Select_divtask()

                    Dim task As String = Data.Tables(0).Rows(x).Item(0).ToString.Trim.ToUpper
                    Session.Add("divtask", task.Trim)

                    For y = 0 To datadiv.Tables(0).Rows.Count - 1
                        Dim taskdiv As String = datadiv.Tables(0).Rows(y).Item(0).ToString.Trim.ToUpper
                        If task = taskdiv Then
                            task = "DIVMAN"
                            Exit For
                        End If
                    Next

                    Select Case task
                        Case "REGIONAL MAN"
                            cbRM.Enabled = True
                        Case "AREA MANAGER"
                            cbAM.Enabled = True
                        Case "BM/BOSMAN"
                            cbBM.Enabled = True
                        Case "DIVMAN"
                            cbDM.Enabled = True
                            'Case "GMO-GENMAN" Or "ISPD-DIVMAN" Or "MKTG-DIVMAN" Or "ALL-DIVMAN" Or "HRMD-DIVMAN" Or "IAD-DIVMAN" Or "MIS-DIVMAN" Or "CAD-DIVMAN-RECON" Or "IAD-ACTDIVMAN" Or "BOS-SECEVP" Or "MMD-DIVMAN" Or "MIS-DIVMAN" Or "FSD-MTSDMAN" Or "ALL-OPMAN" Or "CAD-DIVMAN" Or "FSD-DIVMAN"
                    End Select
                Next
            End If
        End If
    End Sub

    Protected Sub gvRecipientsList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvRecipientsList.SelectedIndexChanged
        Me.Session.Add("_Circular", gvRecipientsList.SelectedRow.Cells(1).Text)
        Me.Session.Add("_Subject", gvRecipientsList.SelectedRow.Cells(2).Text)
        Me.Session.Add("_SentTo", gvRecipientsList.SelectedRow.Cells(3).Text)
        Me.Session.Add("_CreateDate", gvRecipientsList.SelectedRow.Cells(4).Text)
        Response.Redirect("GMOMemoView.aspx")
    End Sub

    Public Function Select_divtask() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim x As String
             
                x = "SELECT distinct WA.TASK FROM [teamely].WebProjectMay2012.dbo.IRDIVISION IRD INNER JOIN [teamely].WebProjectMay2012.dbo.WEBACCOUNTS WA ON IRD.DIVISIONMANAGER = WA.FULLNAME"
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

                x = "SELECT distinct WA.TASK FROM [teamely].WebProjectMay2012.dbo.IRDIVISION IRD INNER JOIN [teamely].WebProjectMay2012.dbo.WEBACCOUNTS WA ON IRD.DIVISIONMANAGER = WA.FULLNAME"
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

    Public Function Select_RecipientList() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim x As String
                x = "SELECT distinct task FROM MEMO_SELECTION_GMOV2 WHERE CircularNumber = '" & Me.Session("_Circular") & "' and Subject = '" & Me.Session("_Subject") & "' and sentto <> '' and zonecode = 'vismin'"
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
                x = "SELECT distinct task FROM MEMO_SELECTION_GMOV2 WHERE CircularNumber = '" & Me.Session("_Circular") & "' and Subject = '" & Me.Session("_Subject") & "' and sentto <> '' and zonecode = 'luzon'"
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

    Public Function Select_RecipientListRM() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim x As String
                x = "SELECT * FROM RecipientsListRMAMBMDIV where zonecode = 'vismin'" & _
                    "and CircularNumber = '" & Me.Session("_Circular") & "' and task = 'regional man' and sentto <> '' order by Createdate desc"
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
                x = "SELECT * FROM RecipientsListRMAMBMDIV where zonecode = 'luzon'" & _
                    "and CircularNumber = '" & Me.Session("_Circular") & "' and task = 'regional man' and sentto <> '' order by Createdate desc"
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
    Public Function Select_RecipientListAM() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim x As String
                x = "SELECT * FROM RecipientsListRMAMBMDIV where zonecode = 'vismin'" & _
                    "and CircularNumber = '" & Me.Session("_Circular") & "' and task = 'area manager' and sentto <> '' order by Createdate desc"
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
                x = "SELECT * FROM RecipientsListRMAMBMDIV where zonecode = 'luzon'" & _
                    "and CircularNumber = '" & Me.Session("_Circular") & "' and task = 'area manager' and sentto <> '' order by Createdate desc"
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
    Public Function Select_RecipientListBM() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim x As String
                x = "SELECT * FROM RecipientsListRMAMBMDIV where zonecode = 'vismin'" & _
                    " and CircularNumber = '" & Me.Session("_Circular") & "' and task = 'BM/BOSMAN' and sentto <> '' order by Createdate desc"
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
                x = "SELECT * FROM RecipientsListRMAMBMDIV where zonecode = 'luzon'" & _
                    " and CircularNumber = '" & Me.Session("_Circular") & "' and task = 'BM/BOSMAN' and sentto <> '' order by Createdate desc"
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
    Public Function Select_RecipientListDM() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim x As String
                x = "SELECT * FROM RecipientsListRMAMBMDIV where zonecode = 'vismin'" & _
                    " and CircularNumber = '" & Me.Session("_Circular") & "' and task = '" & Trim(Session("divtask")) & "' and sentto <> '' order by Createdate desc"
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
                x = "SELECT * FROM RecipientsListRMAMBMDIV where zonecode = 'Luzon'" & _
                    " and CircularNumber = '" & Me.Session("_Circular") & "' and task = '" & Trim(Session("divtask")) & "' and sentto <> '' order by Createdate desc"
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

    Public Function Select_RecipientListALL() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim x As String
                x = "SELECT * FROM RecipientsListRMAMBMDIV  where zonecode = 'vismin'" & _
                    "and CircularNumber = '" & Me.Session("_Circular") & "' and task in('Regional Man', 'Area Manager', 'BM/BOSMAN') and sentto <> '' order by Createdate desc"
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
                x = "SELECT * FROM RecipientsListRMAMBMDIV  where zonecode = 'luzon'" & _
                    "and CircularNumber = '" & Me.Session("_Circular") & "' and task in('Regional Man', 'Area Manager', 'BM/BOSMAN') and sentto <> '' order by Createdate desc"
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

    Public Function Select_RecipientListRMAM() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim x As String
                x = "select * from RecipientsListRMAMBMDIV where zonecode = 'vismin'" & _
                    "and CircularNumber = '" & Me.Session("_Circular") & "' and task in('Regional Man', 'Area Manager') and sentto <> '' order by Createdate desc"
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
                x = "select * from RecipientsListRMAMBMDIV where zonecode = 'luzon'" & _
                    "and CircularNumber = '" & Me.Session("_Circular") & "' and task in('Regional Man', 'Area Manager') and sentto <> '' order by Createdate desc"
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

    Public Function Select_RecipientListAMBM() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim x As String
                x = "select * from RecipientsListRMAMBMDIV where zonecode = 'vismin'" & _
                    "and CircularNumber = '" & Me.Session("_Circular") & "' and task in('Area Manager', 'BM/BOSMAN') and sentto <> '' order by Createdate desc"
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
                x = "select * from RecipientsListRMAMBMDIV where zonecode = 'luzon'" & _
                    "and CircularNumber = '" & Me.Session("_Circular") & "' and task in('Area Manager', 'BM/BOSMAN') and sentto <> '' order by Createdate desc"
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

    Public Function Select_RecipientListRMBM() As DataSet
        Dim Con As New SqlConnection
        Dim Com As New SqlCommand
        Dim ds As New DataSet
        _DatabaseConnection1()
        _ConnectionString1()

        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Try
                Dim x As String
                x = "select * from RecipientsListRMAMBMDIV where zonecode = 'vismin'" & _
                    "and CircularNumber = '" & Me.Session("_Circular") & "' and task in('Regional Man', 'BM/BOSMAN') and sentto <> '' order by Createdate desc"
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
                x = "select * from RecipientsListRMAMBMDIV where zonecode = 'luzon'" & _
                    "and CircularNumber = '" & Me.Session("_Circular") & "' and task in('Regional Man', 'BM/BOSMAN') and sentto <> '' order by Createdate desc"
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

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If cbRM.Checked And cbAM.Checked And cbBM.Checked Then
            Dim DS As New DataSet
            DS = Select_RecipientListALL()
            If Not DS Is Nothing Then
                gvRecipientsList.DataSource = DS.Tables(0)
                gvRecipientsList.DataBind()
            End If
        ElseIf cbRM.Checked And cbAM.Checked Then
            Dim DS As New DataSet
            DS = Select_RecipientListRMAM()
            If Not DS Is Nothing Then
                gvRecipientsList.DataSource = DS.Tables(0)
                gvRecipientsList.DataBind()
            End If
        ElseIf cbAM.Checked And cbBM.Checked Then
            Dim DS As New DataSet
            DS = Select_RecipientListAMBM()
            If Not DS Is Nothing Then
                gvRecipientsList.DataSource = DS.Tables(0)
                gvRecipientsList.DataBind()
            End If
        ElseIf cbRM.Checked And cbBM.Checked Then
            Dim DS As New DataSet
            DS = Select_RecipientListRMBM()
            If Not DS Is Nothing Then
                gvRecipientsList.DataSource = DS.Tables(0)
                gvRecipientsList.DataBind()
            End If
        ElseIf cbRM.Checked Then
            Dim DS As New DataSet
            DS = Select_RecipientListRM()
            If Not DS Is Nothing Then
                gvRecipientsList.DataSource = DS.Tables(0)
                gvRecipientsList.DataBind()
            End If
        ElseIf cbAM.Checked Then
            Dim DS As New DataSet
            DS = Select_RecipientListAM()
            If Not DS Is Nothing Then
                gvRecipientsList.DataSource = DS.Tables(0)
                gvRecipientsList.DataBind()
            End If
        ElseIf cbBM.Checked Then
            Dim DS As New DataSet
            DS = Select_RecipientListBM()
            If Not DS Is Nothing Then
                gvRecipientsList.DataSource = DS.Tables(0)
                gvRecipientsList.DataBind()
            End If
        ElseIf cbDM.Checked Then
            Dim DS As New DataSet
            DS = Select_RecipientListDM()
            If Not DS Is Nothing Then
                gvRecipientsList.DataSource = DS.Tables(0)
                gvRecipientsList.DataBind()
            End If
        Else
            Dim DS As New DataSet
            If Not DS Is Nothing Then
                gvRecipientsList.DataSource = Nothing
                gvRecipientsList.DataBind()
            End If
        End If
    End Sub

    Protected Sub gvRecipientsList_OnSorting(ByVal sender As [Object], ByVal e As GridViewSortEventArgs)
        If cbRM.Checked And cbAM.Checked And cbBM.Checked Then
            ViewState("sortExpr") = e.SortExpression
            gvRecipientsList.DataSource = Select_RecipientListALL()
            gvRecipientsList.DataBind()
        ElseIf cbRM.Checked And cbAM.Checked Then
            ViewState("sortExpr") = e.SortExpression
            gvRecipientsList.DataSource = Select_RecipientListRMAM()
            gvRecipientsList.DataBind()
        ElseIf cbAM.Checked And cbBM.Checked Then
            ViewState("sortExpr") = e.SortExpression
            gvRecipientsList.DataSource = Select_RecipientListAMBM()
            gvRecipientsList.DataBind()
        ElseIf cbRM.Checked And cbBM.Checked Then
            ViewState("sortExpr") = e.SortExpression
            gvRecipientsList.DataSource = Select_RecipientListRMBM()
            gvRecipientsList.DataBind()
        ElseIf cbRM.Checked Then
            ViewState("sortExpr") = e.SortExpression
            gvRecipientsList.DataSource = Select_RecipientListRM()
            gvRecipientsList.DataBind()
        ElseIf cbAM.Checked Then
            ViewState("sortExpr") = e.SortExpression
            gvRecipientsList.DataSource = Select_RecipientListAM()
            gvRecipientsList.DataBind()
        ElseIf cbBM.Checked Then
            ViewState("sortExpr") = e.SortExpression
            gvRecipientsList.DataSource = Select_RecipientListBM()
            gvRecipientsList.DataBind()
        ElseIf cbDM.Checked Then
            ViewState("sortExpr") = e.SortExpression
            gvRecipientsList.DataSource = Select_RecipientListDM()
            gvRecipientsList.DataBind()
        Else
            ViewState("sortExpr") = e.SortExpression
            gvRecipientsList.DataSource = Nothing
            gvRecipientsList.DataBind()
        End If
    End Sub

    Protected Sub gvRecipientsList_PageIndexChanging(ByVal sender As [Object], ByVal e As GridViewPageEventArgs)
        If cbRM.Checked And cbAM.Checked And cbBM.Checked Then
            gvRecipientsList.PageIndex = e.NewPageIndex
            gvRecipientsList.DataSource = Select_RecipientListALL()
            gvRecipientsList.DataBind()
        ElseIf cbRM.Checked And cbAM.Checked Then
            gvRecipientsList.PageIndex = e.NewPageIndex
            gvRecipientsList.DataSource = Select_RecipientListRMAM()
            gvRecipientsList.DataBind()
        ElseIf cbAM.Checked And cbBM.Checked Then
            gvRecipientsList.PageIndex = e.NewPageIndex
            gvRecipientsList.DataSource = Select_RecipientListAMBM()
            gvRecipientsList.DataBind()
        ElseIf cbRM.Checked And cbBM.Checked Then
            gvRecipientsList.PageIndex = e.NewPageIndex
            gvRecipientsList.DataSource = Select_RecipientListRMBM()
            gvRecipientsList.DataBind()
        ElseIf cbRM.Checked Then
            gvRecipientsList.PageIndex = e.NewPageIndex
            gvRecipientsList.DataSource = Select_RecipientListRM()
            gvRecipientsList.DataBind()
        ElseIf cbAM.Checked Then
            gvRecipientsList.PageIndex = e.NewPageIndex
            gvRecipientsList.DataSource = Select_RecipientListAM()
            gvRecipientsList.DataBind()
        ElseIf cbBM.Checked Then
            gvRecipientsList.PageIndex = e.NewPageIndex
            gvRecipientsList.DataSource = Select_RecipientListBM()
            gvRecipientsList.DataBind()
        Else
            gvRecipientsList.PageIndex = e.NewPageIndex
            gvRecipientsList.DataSource = Nothing
            gvRecipientsList.DataBind()
        End If
    End Sub

End Class
