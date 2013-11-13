<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login_GMO.aspx.vb" Inherits="Login_GMO" %>

<%@ Register Assembly="FlashControl" Namespace="Bewise.Web.UI.WebControls" TagPrefix="Bewise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GMO || Login</title>
    <meta http-equiv="CACHE-CONTROL" content="NO-STORE" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <link rel="shortcut icon" href="Image/application.ico" />
    <link href="GMOStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" EnablePageMethods="true" />
    <div id="wrapper">
        <div class="header">
            <div class="header_interior">
                <div class="head_interior-left_column">
                    <img src="Image/MLBig.gif" alt="Logo" style="margin-right: 10px;" />
                </div>
            </div>
            <div class="right-Login">
                <asp:TextBox ID="txtUserName" CssClass="txtUppercase WriteBackColor" style="margin-right: -5px;" runat="server"></asp:TextBox>
                <ajaxToolkit:TextBoxWatermarkExtender ID="txtUserName_TextBoxWatermarkExtender" 
                    runat="server" Enabled="True" TargetControlID="txtUserName" 
                    WatermarkCssClass="watermarked" WatermarkText="Username">
                </ajaxToolkit:TextBoxWatermarkExtender>
                <asp:TextBox ID="txtPassword" CssClass="WriteBackColor" style="margin-right: -5px;" runat="server" TextMode="Password"></asp:TextBox>
                <ajaxToolkit:TextBoxWatermarkExtender ID="txtPassword_TextBoxWatermarkExtender" 
                    runat="server" Enabled="True" TargetControlID="txtPassword"
                    WatermarkCssClass="watermarked" WatermarkText="94016508">
                </ajaxToolkit:TextBoxWatermarkExtender>
                <asp:Button ID="btnLogIn" CssClass="BtnLogin" runat="server" Text="Log In" BackColor="White" 
                    BorderColor="White" BorderStyle="None" Font-Bold="True" Font-Size="Small" 
                    Width="50px" />
                <%--<asp:LinkButton ID="lbLogIn" runat="server" ForeColor="Black">Log In</asp:LinkButton>--%>
            </div>
        </div>
        <div class=" content_body" style="height: 353px">
            <div id="content_interior2" style="height: 334px">
                <div class="menu-horizontal">
                    <div class="versionLogin">
                        GMO Version 2.0
                    </div>
                    <div class="subHeaderError">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="White" Font-Bold="False"></asp:Label>
                    </div>
                    <div id="navbar">
                            <Bewise:FlashControl ID="FlashControl1" runat="server" Height="208px" 
                                MovieUrl="~/Animation/WebLoginAnimation.swf" Width="339px" Loop="True" />                             
                    </div>
                </div>
            </div>
            <div id="footer">
                <div class="left">
                &nbsp; M. Lhuillier Financial Services Inc.
                </div>
                <br class="doNotDisplay doNotPrint" />
                <div class="right">
                All Rights Reserved. &nbsp;
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>

