<%@ Page Language="VB" MasterPageFile="~/LeftPageMaster.master" AutoEventWireup="false" CodeFile="GMOLog.aspx.vb" Inherits="GMOLog" title="GMO Web Version 1.5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContentPlaceHolder" Runat="Server">
<%--    <div id="content2">--%>

 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </asp:ToolkitScriptManager>

<%--<table style="width:100px; top: 1px; left: 6px; position: absolute; height: 26px; right: 4px;">--%>
<div style=" float:left; width: 330px;">
<table style="width: 325px">
        <tr>
            <td style="width: 59px; height: 35px;">
                <asp:Label ID="Label1" runat="server" Text="Date from"></asp:Label>
            </td>
            <td style="width: 4px; text-align:left; height: 35px;">
                :</t:</td>
            <td style="text-align:left; height: 35px; width: 133px;">
                <asp:TextBox ID="txtdatefrom" runat="server" Width="83px" Height="15px"
                AutoComplete="off" TabIndex="1" EnableTheming="True" MaxLength="10"></asp:TextBox>
                <asp:Image ID="Image2" runat="server" Height="16px" 
                    ImageUrl="~/Images/calendar.png" Width="20px" />&nbsp;
                <asp:CalendarExtender ID="CalendarExtender1" runat="server"   Enabled="True" 
                TargetControlID="txtdatefrom" PopupPosition="BottomRight" 
                 PopupButtonID="image2"/>
               </td>
        </tr>
        <tr>
            <td style="width: 59px; height: 35px;">
                &nbsp;&nbsp;Date to
            <td style="width: 4px; text-align:left; height: 35px;">
                :</t:</td>
            <td style="text-align:left; width: 133px; height: 35px;">
                <asp:TextBox ID="txtdateto" runat="server" Width="83px" Height="15px" ></asp:TextBox>
                <asp:Image ID="Image7" runat="server" Height="16px" 
                    ImageUrl="~/Images/calendar.png" Width="20px" />&nbsp;
                <asp:CalendarExtender ID="CalendarExtender2" runat="server"  Enabled="True" 
                TargetControlID="txtdateto" PopupPosition="BottomRight" 
                 PopupButtonID="image7"/>
            </td>
        </tr>
        <tr>
            <td style="width: 59px; ">
                &nbsp;&nbsp;Division
               <td style="width: 4px; text-align:left; ">
                    :</td>
            <td style="text-align:left; width: 133px;">
                <asp:DropDownList ID="DivisionDPL" runat="server"  AutoPostBack="True" Height="22px" Width="237px">
                                   </asp:DropDownList>
                                
            </td>
        </tr>
        <tr>
            <td style="width: 59px; height: 37px;">
                &nbsp;&nbsp;&nbsp;Region</td>  
                        
           <td style="width: 4px; text-align:left; height: 37px;">:</td>
            <td style="text-align:left; height: 37px; width: 133px;">
            <asp:DropDownList ID="RegionDPL" runat="server" Height="22px" Width="239px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 59px; height: 35px;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Area&nbsp;</td>
            <td style="width: 4px; text-align:left; height: 35px;">
                :</td>
            <td style="text-align:left; height: 35px; width: 133px;">
           <asp:DropDownList ID="AreaDPL" runat="server" Height="22px" Width="239px" 
                    AutoPostBack="True" TabIndex = "2">
                   
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 59px; height: 22px;">
                &nbsp;&nbsp;Branch&nbsp;</td>
            <td style="width: 4px; text-align:left; height: 22px;">
                :</td>
            <td style="text-align:left; height: 22px; width: 133px;">
                <asp:DropDownList ID="BranchDPL" runat="server" Height="22px" Width="239px" 
                    AutoPostBack="True" TabIndex ="3">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="height: 22px;" colspan="3">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                <asp:Label ID="DateErrorLbl" runat="server" 
                    style="color: #FF0000; font-weight: 700; " 
                    Text="Please input valid date." EnableViewState="False" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 29px;" colspan="3">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                <asp:Button ID="Clearbtn" runat="server" Text="Clear" Width="71px" />
                &nbsp;<asp:Button ID="Searchbtn" runat="server" Text="Search" Width="71px" />
            </td>
        </tr>
    </table>
</div>
<div style="width:860px; height:400px; overflow:scroll; padding-top:10px;  margin-left:500px;">
    <asp:Panel ID="char" runat="server" >
    <%--<div  style="overflow:scroll ; width:775px;">--%>
        <asp:GridView ID="gvRecipientsList" runat="server" AllowPaging="false" BorderStyle="Solid" BorderWidth="1px"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" BorderColor="black"
            GridLines="None" OnPageIndexChanging="gvRecipientsList_PageIndexChanging" 
            OnSorting="gvRecipientsList_OnSorting" PageSize="15" Width="887px">
           <%-- <PagerSettings FirstPageImageUrl="~/Images/38.png" 
                LastPageImageUrl="~/Images/37.png" Mode="NextPreviousFirstLast" 
                NextPageImageUrl="~/Images/31.png" PreviousPageImageUrl="~/Images/32.png" />--%>
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField DataField="Circular" HeaderText="Circular">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="Subject" HeaderText="Subject">
                    <ItemStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="Fullname" HeaderText="Name">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="CReceived" HeaderText="Activity">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                </asp:BoundField>
                <asp:BoundField DataField="CostCenters" HeaderText="Cost Center">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                </asp:BoundField>
                <asp:BoundField DataField="Division" HeaderText="Dept. Name">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="CDate" HeaderText="Date">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
   </asp:Panel> 
</div>
</asp:Content>

