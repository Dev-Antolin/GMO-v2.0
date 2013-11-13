Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports clsGMOUser
Imports System.Web.Services

Partial Class AttachMemo
    Inherits System.Web.UI.Page
    Dim DMemo As String = ""
    Dim CMemo As String = ""
    Dim MemoPath As String = ""
    Dim sFileV As String = "~/VisminImageMemo/"
    Dim sFileL As String = "~/LuzonImageMemo/"
    Dim sFileServerVis As String = System.Web.HttpContext.Current.Server.MapPath("") & "\MemoImageVismin\"
    Dim sFileDirVis As String = System.Web.HttpContext.Current.Server.MapPath("") & "\VisminImageMemo\"
    Dim sFileServerLuz As String = System.Web.HttpContext.Current.Server.MapPath("") & "\MemoImageLuzon\"
    Dim sFileDirLuz As String = System.Web.HttpContext.Current.Server.MapPath("") & "\LuzonImageMemo\"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Create_trxnumber()
        CheckLogin()
        txtStartDate.Attributes.Add("onkeypress", "return charD(event)")
        txtStartDate.Attributes("onchange") = "TextDateChanged(this.value)"
        txtTRXNumber.Visible = False
        txtCircular.Attributes.Add("onkeypress", "return numerals(event)")
        txtSubject.Attributes.Add("onkeypress", "return Chars(event)")
        btnUpload.Attributes.Add("onclick", "return skm_LockScreen('One Moment Please...');")
    End Sub

    Private Sub CheckLogin()
        If Me.Session("_fullname") = "" Then
            Response.Redirect("Login.aspx")
        End If
    End Sub
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If HttpContext.Current.Session("validateCircular") = "false" Then
            Exit Sub
        End If
        UploadFile()

    End Sub
    Public Sub UploadFile()
        If txtStartDate.Text = "" Then
            txtStartDate.Focus()
            Statuslabel.Text = "Date is empty!"
            Exit Sub
        End If
        If txtStartDate.Text > Date.Now Then
            txtStartDate.Focus()
            Statuslabel.Text = "Advance date is invalid!"
            Exit Sub
        End If
        If txtStartDate.Text.Length < 8 Then
            txtStartDate.Focus()
            Statuslabel.Text = "Invalid Date!"
            Exit Sub
        End If
        If txtStartDate.Text = "" OrElse Not IsDate(txtStartDate.Text) Then
            txtStartDate.Focus()
            Statuslabel.Text = "Invalid Date!"
            Exit Sub
        End If
        'VISMIN UPLOAD
        If Trim(Me.Session("_zonecode")) = "VISMIN" Then
            Dim i As Integer = Request.Files.Count
            If i = 0 Then
                Statuslabel.Text = "No file to upload!"
            Else
                For i = 0 To Request.Files.Count - 1
                    Dim PostedFile As HttpPostedFile = Request.Files(i)
                    Dim strExtensionName As String = System.IO.Path.GetExtension(PostedFile.FileName)
                    If PostedFile.ContentLength > 0 Then
                        If strExtensionName.Equals(".jpg") OrElse strExtensionName.Equals(".jpeg") OrElse strExtensionName.Equals(".JPG") OrElse strExtensionName.Equals(".JPEG") Then
                            Dim FileName As String = System.IO.Path.GetFileName(PostedFile.FileName)
                            Dim Filenme As String = "VUM"
                            Dim d As String
                            d = Format(Date.Now, "01hhmmsstt")
                            PostedFile.SaveAs(Server.MapPath(sFileV & d.ToString & Filenme.ToString) + FileName)
                        Else
                            Statuslabel.Text = "Only JPEG file(s) are accepted. Try again!"
                            ImageDelete()
                            Exit Sub
                        End If
                    Else
                        Statuslabel.Text = "Fill in attachment and try again!"
                        ImageDelete()
                        Exit Sub
                    End If
                Next

                Dim xname() As String = System.IO.Directory.GetFiles(sFileDirVis, "*.jpg")
                Dim y As Integer
                Dim path As String = ""
                For y = 0 To xname.Count - 1
                    path += xname(y).Replace(sFileDirVis, sFileServerVis).ToString & ","
                Next
                MemoPath = path.Substring(0, path.Length - 1)

                Session.Add("circularno", txtCircular.Text)
                Session.Add("attachtype", "attach")
                Response.Redirect("GMOcreate.aspx")
                'If Insert_Creator() Then
                '    If Insert_Upload() Then
                '        MoveFiles(sFileDirVis, sFileServerVis)
                '        Response.Write("<script language=javascript>")
                '        Response.Write("alert('" & "File(s) upload successfull!" & "')")
                '        Response.Write("</script>")
                '        Response.Write("<script language=javascript>")
                '        Response.Write("window.location = 'UploadMemo.aspx'")
                '        Response.Write("</script>")
                '    Else
                '        Response.Write("<script language=javascript>")
                '        Response.Write("alert('" & "File upload unsuccessfull!" & "')")
                '        Response.Write("</script>")
                '        Response.Write("<script language=javascript>")
                '        Response.Write("window.location = 'UploadMemo.aspx'")
                '        Response.Write("</script>")
                '        delete_Creator()
                '        ImageDelete()
                '        Exit Sub
                '    End If
                'Else
                '    Statuslabel.Text = "Data inserted is overloaded!"
                '    ImageDelete()
                '    Exit Sub
                'End If
            End If
        End If
        'LUZON UPLOAD
        If Trim(Me.Session("_zonecode")) = "LUZON" Then
            Dim i As Integer = Request.Files.Count
            If i = 0 Then
                Statuslabel.Text = "No file to upload!"
            Else
                For i = 0 To Request.Files.Count - 1
                    Dim PostedFile As HttpPostedFile = Request.Files(i)
                    Dim strExtensionName As String = System.IO.Path.GetExtension(PostedFile.FileName)
                    If PostedFile.ContentLength > 0 Then
                        If strExtensionName.Equals(".jpg") OrElse strExtensionName.Equals(".jpeg") OrElse strExtensionName.Equals(".JPG") OrElse strExtensionName.Equals(".JPEG") Then
                            Dim FileName As String = System.IO.Path.GetFileName(PostedFile.FileName)
                            Dim Filenme As String = "LUM"
                            Dim d As String
                            d = Format(Date.Now, "02hhmmsstt")
                            PostedFile.SaveAs(Server.MapPath(sFileL & d.ToString & Filenme.ToString) + FileName)
                        Else
                            Statuslabel.Text = "Only JPEG file(s) are accepted. Try again!"
                            ImageDelete()
                            Exit Sub
                        End If
                    Else
                        Statuslabel.Text = "Fill in attachment and try again!"
                        ImageDelete()
                        Exit Sub
                    End If
                Next

                Dim xname() As String = System.IO.Directory.GetFiles(sFileDirLuz, "*.jpg")
                Dim y As Integer
                Dim path As String = ""
                For y = 0 To xname.Count - 1
                    path += xname(y).Replace(sFileDirLuz, sFileServerLuz).ToString & ","
                Next
                MemoPath = path.Substring(0, path.Length - 1)

                Session.Add("circularno", txtCircular.Text)
                Session.Add("attachtype", "attach")
                Response.Redirect("GMOcreate.aspx")
                'If Insert_Creator() Then
                '    If Insert_Upload() Then
                '        MoveFiles(sFileDirLuz, sFileServerLuz)
                '        Response.Write("<script language=javascript>")
                '        Response.Write("alert('" & "File(s) upload successfull!" & "')")
                '        Response.Write("</script>")
                '        Response.Write("<script language=javascript>")
                '        Response.Write("window.location = 'UploadMemo.aspx'")
                '        Response.Write("</script>")
                '    Else
                '        Response.Write("<script language=javascript>")
                '        Response.Write("alert('" & "File upload unsuccessfull!" & "')")
                '        Response.Write("</script>")
                '        Response.Write("<script language=javascript>")
                '        Response.Write("window.location = 'UploadMemo.aspx'")
                '        Response.Write("</script>")
                '        delete_Creator()
                '        ImageDelete()
                '        Exit Sub
                '    End If
                'Else
                '    Statuslabel.Text = "Data inserted is overloaded!"
                '    ImageDelete()
                '    Exit Sub
                'End If
            End If
        End If
    End Sub
    Public Function Insert_Creator() As Boolean
        Dim oCom As New SqlCommand
        Dim oTrans As SqlTransaction
        Dim Con As New SqlConnection
        DMemo = Format(Date.Now, "yyyy-MM-dd hh:mm:ss tt")
        CMemo = txtStartDate.Text & " " & "8:30 AM" 'default time for archive uplodate  added last 11/27/2010
        'CMemo = txtStartDate.Text & " " & "8:30 AM" 'default time for archive uplodate 
        'CMemo = Format(Date.Now, "yyyy-MM-dd hh:mm:ss tt")
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
            Catch ex As Exception
                'MsgBox(ex.Message())
                oTrans.Rollback()
                Con.Close()
                oCom.Dispose()
                Return False
            Finally
                Con.Close()
                oCom.Dispose()
            End Try
        End If
        Return True
    End Function
    Public Function delete_Creator() As Boolean
        Dim oCom As New SqlCommand
        Dim oTrans As SqlTransaction
        Dim Con As New SqlConnection
        DMemo = Format(Date.Now, "yyyy-MM-dd hh:mm:ss tt")
        CMemo = txtStartDate.Text
        'CMemo = Format(Date.Now, "yyyy-MM-dd hh:mm:ss tt")
        Dim MemoFrom As String = "GM's OFFICE"
        Dim a As String = "delete from memoheader where trxnumber = '" + txtTRXNumber.Text + "'"
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
                'MsgBox(ex.Message())
                oTrans.Rollback()
                Con.Close()
                oCom.Dispose()
                Return False
            Finally
                Con.Close()
                oCom.Dispose()
            End Try
        End If
        Return True
    End Function
    Public Function Insert_Upload() As Boolean
        Dim Employee As String = "None"
        Dim oCom As New SqlCommand
        Dim oTrans As SqlTransaction
        Dim Con As New SqlConnection
        Dim MemoTo As String = "GM's OFFICE"
        Dim a As String = "insert into memoGenMan(trxnumber, memoto, GenMan, confirmedreceived, confirmeddate, " & _
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
                'MsgBox(ex.Message())
                oTrans.Rollback()
                Con.Close()
                oCom.Dispose()
                Return False
            Finally
                Con.Close()
                oCom.Dispose()
            End Try
        End If
        Return True
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
        Con.Close()
        Com.Dispose()
    End Function
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
