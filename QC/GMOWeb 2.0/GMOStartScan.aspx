<%@ Page Language="VB" MasterPageFile="~/LeftPageMaster.master" AutoEventWireup="false" CodeFile="GMOStartScan.aspx.vb" Inherits="GMOStartScan" title="GMO Web Version 2.0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContentPlaceHolder" Runat="Server">
    <div id="content2">
<p style="margin-left:10px; margin-top:10px;">&nbsp;<asp:HyperLink ID="scanmemo" runat="server" NavigateUrl="~/TwainUploadASPNET.htm">Scan Memo</asp:HyperLink></p>
<p style="margin-left:10px; margin-top:10px;">&nbsp;<asp:HyperLink ID="uploadmemo" runat="server" NavigateUrl="~/UploadMemo.aspx">Upload Memo</asp:HyperLink></p>
<p style="margin-left:10px; margin-top:10px;">&nbsp;<asp:HyperLink ID="attachmemo" runat="server" NavigateUrl="~/AttachMemo.aspx">Attach and Send Memo</asp:HyperLink></p>
<%--<a href="http://192.168.12.143/Sample/TwainUploadASPNET.htm">Start Scan</a>--%>
<%--<a href="http://192.168.12.214/GMOWebs/TwainUploadASPNET.htm">Start Scan</a>--%>
</div>
</asp:Content>

