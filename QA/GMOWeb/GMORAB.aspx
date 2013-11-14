﻿<%@ Page Language="VB" MasterPageFile="~/Left2PageMaster.master" AutoEventWireup="false" CodeFile="GMORAB.aspx.vb" Inherits="GMORAB" title="GMO Web Version 1.5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContent2PlaceHolder" Runat="Server">
<div id="content2">
<div id="conte">
    <asp:GridView ID="gvReceiver" runat="server" AutoGenerateColumns="False" 
         Height="100px" Width="750px" CellPadding="4" 
        ForeColor="#333333" GridLines="None" PageSize="16">
         <RowStyle BorderColor="Black" BackColor="#EFF3FB" />
         <Columns>
             <asp:CommandField ShowSelectButton="True" SelectText="View" >
                 <ItemStyle HorizontalAlign="Center" Width="30px" />
             </asp:CommandField>
                 <asp:BoundField HeaderText="Circular Number" DataField="CircularNumber" >
                     <ItemStyle HorizontalAlign="Center" Width="130px" />
             </asp:BoundField>
                 <asp:BoundField HeaderText="Subject" DataField="Subject" />
                <asp:BoundField HeaderText="From" DataField="MemoFrom" >
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
             </asp:BoundField>
             <asp:BoundField HeaderText="Memo Date" DataField="Createdate">
                 <ItemStyle HorizontalAlign="Center" />
             </asp:BoundField>
         </Columns>
         <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
         <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
         <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
         <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
         <EditRowStyle BackColor="#2461BF" />
         <AlternatingRowStyle BackColor="White" />
     </asp:GridView>
</div>
    <%--<asp:Button ID="btnSearch" runat="server" 
        style="top: 26px; left: 453px; position: absolute; width: 56px; right: 661px;" 
        Text="Search" />
    <table style="width: 63%">
        <tr>
            <td style="text-align: right; width: 75px">
                <asp:Label ID="Label1" runat="server" Text="Circular #"></asp:Label>
                &nbsp;<asp:Label ID="Label3" runat="server" Text=":"></asp:Label>
            </td>
            <td style="width: 370px">
                <asp:TextBox ID="txtCircular" runat="server" Width="65px" MaxLength="8"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 75px">
                <asp:Label ID="Label2" runat="server" Text="Subject"></asp:Label>
                &nbsp;<asp:Label ID="Label4" runat="server" Text=":"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtSubject" runat="server" Width="365px"></asp:TextBox>
            </td>
        </tr>
    </table>--%>
</div>
</asp:Content>

