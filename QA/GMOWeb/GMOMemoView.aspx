<%@ Page Language="VB" MasterPageFile="~/LeftPageMaster.master" AutoEventWireup="false" CodeFile="GMOMemoView.aspx.vb" Inherits="GMOMemoView" title="GMO Web Version 2.0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContentPlaceHolder" Runat="Server">
<div id="content2">
<div id="content" align="center">

    <asp:Image ID="imgDisplayMe" runat="server" Height="850px" Width="650px" />

</div>
        &nbsp;<br />
        &nbsp;<asp:Button ID="btnBack" runat="server" Text="Back" 
        
        style="top: 4px; left: 6px; position: absolute; height: 26px; width: 50px" />
    <br />
        &nbsp;<asp:Button ID="btnPrint" runat="server" Text="Download" 
        style="top: 4px; left: 58px; position: absolute; height: 26px; width: 76px;" />
        &nbsp;<asp:Button ID="btnPrev" runat="server" Text="Prev" Width="50px" 
        style="top: 4px; left: 144px; position: absolute; height: 26px" />
        &nbsp;<asp:Button ID="btnNext" runat="server" Text="Next" Width="50px" 
        style="top: 4px; left: 196px; position: absolute; height: 26px" />
    <asp:Label ID="lblCountNumber" runat="server" 
        style="top: 11px; left: 257px; position: absolute; height: 13px; width: 102px"></asp:Label>
</div>
</asp:Content>

