
Partial Class LeftPageMaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TASK OF A PERSON
        If Trim(Me.Session("_task")) = ("GMO-GENASST") Or Trim(Me.Session("_task")) = ("GMO-ASTGENMAN") Or Trim(Me.Session("_task")) = ("GMO-SECGM") Or Trim(Me.Session("_task")) = ("GMO-HELPDESK") Then
            lblDept.Text = "GM's Assistant"
        End If
        If Trim(Me.Session("_task")) = ("REGIONAL MAN") Or Trim(Me.Session("_task")) = ("Regional Man") Then
            lblDept.Text = "Regional Manager"
        End If
        If Trim(Me.Session("_task")) = ("AREA MANAGER") Or Trim(Me.Session("_task")) = ("Area Manager") Then
            lblDept.Text = "Area Manager"
        End If
        If Trim(Me.Session("_task")) = ("BM/BOSMAN") Or Trim(Me.Session("_task")) = ("Bm/Bosman") Then
            lblDept.Text = "Branch Manager"
        End If
        If Trim(Me.Session("_task")) = ("MIS") Or Trim(Me.Session("_task")) = ("GM-VISMIN") Or Trim(Me.Session("_task")) = ("IAD Manager") Or Trim(Me.Session("_task")) = ("CAD-DIVMAN") Or Trim(Me.Session("_task")) = ("ALL-DIVMAN") Then
            lblDept.Text = "Division Manager"
        End If
        'LABEL OF TASK
        If Trim(Me.Session("_task")) = ("GMO-GENASST") Or Trim(Me.Session("_task")) = ("GMO-ASTGENMAN") Or Trim(Me.Session("_task")) = ("GMO-SECGM") Or Trim(Me.Session("_task")) = ("GMO-HELPDESK") Then
            lblCostCenter.Text = "GM's Office"
        End If
        If Trim(Me.Session("_task")) = ("REGIONAL MAN") Or Trim(Me.Session("_task")) = ("Regional Man") Then
            lblCostCenter.Text = Me.Session("_compcode")
        End If
        If Trim(Me.Session("_task")) = ("AREA MANAGER") Or Trim(Me.Session("_task")) = ("Area Manager") Then
            lblCostCenter.Text = Me.Session("_compcode")
        End If
        If Trim(Me.Session("_task")) = ("BM/BOSMAN") Or Trim(Me.Session("_task")) = ("Bm/Bosman") Then
            lblCostCenter.Text = Me.Session("_compcode")
        End If
        If Trim(Me.Session("_task")) = ("MIS") Or Trim(Me.Session("_task")) = ("GM-VISMIN") Or Trim(Me.Session("_task")) = ("IAD Manager") Or Trim(Me.Session("_task")) = ("CAD-DIVMAN") Or Trim(Me.Session("_task")) = ("ALL-DIVMAN") Then
            lblCostCenter.Text = Me.Session("_compcode")
        End If
        lblDate.Text = Format(Date.Now, "yyyy-MM-dd")
        lblTime.Text = Format(TimeOfDay, "hh:mm tt")
        lblDeptName.Text = Me.Session("_CompName")
    End Sub
End Class

