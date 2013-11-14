<%@ Page Language="VB" MasterPageFile="~/LeftPageMaster.master" AutoEventWireup="false" CodeFile="GMOMemorandum.aspx.vb" Inherits="GMOMemorandum" title="GMO Web Version 2.0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContentPlaceHolder" Runat="Server">
    <div id="content2">
<div id="conten" dir="ltr">
    <asp:GridView ID="gvRecipientsList" runat="server" AutoGenerateColumns="False" 
        OnPageIndexChanging="gvRecipientsList_PageIndexChanging" OnSorting="gvRecipientsList_OnSorting"
         Height="100px" Width="750px" CellPadding="4" 
        ForeColor="#333333" GridLines="None" AllowPaging="True" PageSize="15">
         <PagerSettings FirstPageImageUrl="~/Images/38.png" 
             LastPageImageUrl="~/Images/37.png" Mode="NextPreviousFirstLast" 
             NextPageImageUrl="~/Images/31.png" PreviousPageImageUrl="~/Images/32.png" />
         <RowStyle BackColor="#EFF3FB" />
         <Columns>
<%--                <asp:BoundField HeaderText="Recipients" DataField="SentTo" >
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
             </asp:BoundField>--%>
             <asp:CommandField ShowSelectButton="True" SelectText="View" >
                 <ItemStyle HorizontalAlign="Center" Width="30px" />
             </asp:CommandField>
                 <asp:BoundField HeaderText="Circular Number" DataField="CircularNumber" >
                     <ItemStyle HorizontalAlign="Center" Width="130px" />
             </asp:BoundField>
                 <asp:BoundField HeaderText="Subject" DataField="Subject" />
             <asp:BoundField HeaderText="Memo Date" DataField="CreateDate">
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
    <asp:Button ID="btnSearch" runat="server" 
        style="top: 28px; left: 453px; position: absolute; width: 56px; right: 334px;" 
        Text="Search" />
    <table style="width: 63%">
        <tr>
            <td style="text-align: right; width: 75px">
                <asp:Label ID="Label1" runat="server" Text="Circular #"></asp:Label>
                &nbsp;<asp:Label ID="Label3" runat="server" Text=":"></asp:Label>
            </td>
            <td style="width: 370px">
                <asp:TextBox ID="txtCircular" runat="server" Width="100px" MaxLength="15"></asp:TextBox>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
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
        </table>
</div>
</asp:Content>

