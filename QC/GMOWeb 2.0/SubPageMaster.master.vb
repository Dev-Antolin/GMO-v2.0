
Partial Class SubPageMaster
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim a As String = Me.Session("click")
        If Me.Session("click") = "CreateNewMemo" Then
            CreateNewMemo.ForeColor = Drawing.Color.LightSkyBlue
        ElseIf Me.Session("click") = "Memorandum" Then
            Memorandum.ForeColor = Drawing.Color.LightSkyBlue
        ElseIf Me.Session("click") = "Log" Then
            Log.ForeColor = Drawing.Color.LightSkyBlue
        End If
    End Sub
End Class

