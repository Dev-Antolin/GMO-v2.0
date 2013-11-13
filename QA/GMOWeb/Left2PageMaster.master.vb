
Partial Class Left2PageMaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TASK OF A PERSON
        If Trim(Me.Session("_task")) = ("GMO-ASSISTANT") Then
            lblDept.Text = "GM'S Assistant"

        ElseIf Trim(Me.Session("_task")) = ("REGIONAL MAN") Or Trim(Me.Session("_task")) = ("Regional Man") Then
            lblDept.Text = "Regional Manager"

        ElseIf Trim(Me.Session("_task")) = ("AREA MANAGER") Or Trim(Me.Session("_task")) = ("Area Manager") Then
            lblDept.Text = "Area Manager"

        ElseIf Trim(Me.Session("_task")) = ("BM/BOSMAN") Or Trim(Me.Session("_task")) = ("Bm/Bosman") Then
            lblDept.Text = "Branch Manager"
        Else
            lblDept.Text = "Division Manager"
        End If
        'LABEL OF TASK
        If Trim(Me.Session("_task")) = ("GMO-ASSISTANT") Then
            lblCostCenter.Text = "GM'S Office"

        ElseIf Trim(Me.Session("_task")) = ("REGIONAL MAN") Or Trim(Me.Session("_task")) = ("Regional Man") Then
            lblCostCenter.Text = Me.Session("_compcode")

        ElseIf Trim(Me.Session("_task")) = ("AREA MANAGER") Or Trim(Me.Session("_task")) = ("Area Manager") Then
            lblCostCenter.Text = Me.Session("_compcode")

        ElseIf Trim(Me.Session("_task")) = ("BM/BOSMAN") Or Trim(Me.Session("_task")) = ("Bm/Bosman") Then
            lblCostCenter.Text = Me.Session("_compcode")
        Else
            lblCostCenter.Text = Me.Session("_compcode")
        End If
        lblDate.Text = Format(Date.Now, "yyyy-MM-dd")
        lblTime.Text = Format(TimeOfDay, "hh:mm tt")
        lblDeptName.Text = Me.Session("_CompName")
    End Sub
End Class

