<%@ Page Language="VB" MasterPageFile="~/LeftPageMaster.master" AutoEventWireup="false" CodeFile="GMOMemorandumDetail.aspx.vb" Inherits="GMOMemorandumDetail" title="GMO Web Version 2.0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContentPlaceHolder" Runat="Server">
       <div id="content2">
<div id="SamConten">
    <asp:GridView ID="gvRecipientsList" runat="server" AutoGenerateColumns="False"
       OnPageIndexChanging="gvRecipientsList_PageIndexChanging" OnSorting="gvRecipientsList_OnSorting"
         Height="100px" Width="750px" CellPadding="4" 
        ForeColor="#333333" GridLines="None" PageSize="15" AllowPaging="True">
         <PagerSettings FirstPageImageUrl="~/Images/38.png" 
             LastPageImageUrl="~/Images/37.png" Mode="NextPreviousFirstLast" 
             NextPageImageUrl="~/Images/31.png" PreviousPageImageUrl="~/Images/32.png" />
         <RowStyle BackColor="#EFF3FB" />
         <Columns>
             <asp:CommandField ShowSelectButton="True" SelectText="View" >
                 <ItemStyle HorizontalAlign="Center" Width="30px" />
             </asp:CommandField>
                 <asp:BoundField HeaderText="Circular Number" DataField="CircularNumber" >
                     <ItemStyle HorizontalAlign="Center" Width="130px" />
             </asp:BoundField>
                 <asp:BoundField HeaderText="Subject" DataField="Subject" />
             <asp:BoundField HeaderText="Recipients" DataField="SentTo" >
                     <ItemStyle HorizontalAlign="Center" Width="150px" />
             </asp:BoundField>
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
    <table style="width: 85%">
        <tr>
            <td style="text-align: right; width: 125px; height: 2px;">
            </td>
            <td style="height: 2px">
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 125px">
                <asp:Label ID="Label2" runat="server" Text="Choose Manager(s)"></asp:Label>
                <asp:Label ID="Label1" runat="server" Text=":"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="cbRM" runat="server" Text="Regional Manager" />
                <asp:CheckBox ID="cbAM" runat="server" Text="Area Manager" />
                <asp:CheckBox ID="cbBM" runat="server" Text="Branch Manager" />
                <asp:CheckBox ID="cbDM" runat="server" Text="Division Manager" />
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 125px">
    <asp:Button ID="btnRefresh" runat="server" 
        style="top: 7px; left: 610px; position: absolute; width: 62px; right: 293px;" 
        Text="Refresh" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</div>

</asp:Content>

