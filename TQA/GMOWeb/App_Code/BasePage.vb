Imports Microsoft.VisualBasic

Public Class BasePage
    Inherits System.Web.UI.Page

    Public Sub LockScreenAfterClick(ByVal wc As WebControl, ByVal message As String)
        AddLockScreenScript()

        wc.Attributes("onclick") = String.Format("skm_LockScreen('{0}');", message.Replace("'", "\'"))
    End Sub

    Public Sub LockScreenAfterDDLChange(ByVal ddl As DropDownList, ByVal message As String)
        AddLockScreenScript()

        ddl.Attributes("onchange") = String.Format("skm_LockScreen('{0}');", message.Replace("'", "\'"))
    End Sub


    Private Sub AddLockScreenScript()
        'Add the JavaScript and <div> elements for freezing the screen
        If Not ClientScript.IsClientScriptIncludeRegistered("skm_LockScreen") Then
            'Register the JavaScript Include
            ClientScript.RegisterClientScriptInclude("skm_LockScreen", Page.ResolveUrl("~/Scripts/LockScreen.js?version=1.0"))

            'Add the <div> elements
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "skm_LockScreen_divs", _
                                                    "<div id=""skm_LockBackground"" class=""LockOff""></div><div id=""skm_LockPane"" class=""LockOff""><div id=""skm_LockPaneText"">&nbsp;</div></div>", _
                                                    False)
        End If
    End Sub
End Class
