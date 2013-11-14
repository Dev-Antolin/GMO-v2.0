<%@ Page Language="VB" MasterPageFile="~/LeftPageMaster.master" AutoEventWireup="false"
    CodeFile="GMOCreate.aspx.vb" Inherits="GMOCreate" Title="GMO Web Version 2.0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContentPlaceHolder" runat="Server">
    <div id="content2">
        <table style="width: 100%">
            <tr>
                <td style="width: 80px; text-align: right">
                    <asp:Label ID="Label1" runat="server" Text="Recipient"></asp:Label>
                    &nbsp;<asp:Label ID="Label3" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRecipients" runat="server" AutoPostBack="True">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Regional Manager</asp:ListItem>
                        <asp:ListItem>Area Manager</asp:ListItem>
                        <asp:ListItem>Branch Manager</asp:ListItem>
                        <asp:ListItem>Division Manager</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtTRXNumber" runat="server" Width="80px">1022010000</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 80px; text-align: right">
                    &nbsp;<asp:Label ID="Label5" runat="server" Text="Circular #"></asp:Label>
                    &nbsp;<asp:Label ID="Label6" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCircular" runat="server" Width="100px" MaxLength="15" TabIndex="1"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="* Circular number already exists!"
                        ControlToValidate="txtCircular" OnServerValidate="validateCircular" SetFocusOnError="true"
                        ValidateEmptyText="true">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 80px; text-align: right">
                    <asp:Label ID="Label2" runat="server" Text="Subject"></asp:Label>
                    &nbsp;<asp:Label ID="Label4" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSubject" runat="server" Width="494px" MaxLength="100" TabIndex="2"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnSendD" runat="server" Style="top: 82px; position: absolute; width: 56px;
            left: 528px; " Text="Send" TabIndex="4" />
        <div id="content3">
            <asp:Image ID="imgDisplay" runat="server" CssClass="topOfPage" Height="403px" Width="340px"
                ImageUrl="~/Images/Scan Images.jpg" />
        </div>
        <div id="content4">
            <asp:TextBox ID="txtSearch" runat="server" onClick="this.value='';" onBlur="this.value='Search Name...';"
                Width="222px" Style="font-style: italic; color: #000000" TabIndex="3" AutoPostBack="True">Search 
            Name...</asp:TextBox>
            <br />
            <asp:GridView ID="gvRecipients" runat="server" Style="height: 18px; top: 40px; left: 5px;
                width: 228px;" CellPadding="4" AutoGenerateColumns="False" Height="16px" ForeColor="#333333"
                GridLines="None">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cb1" runat="server" AutoPostBack="True" onclick="Check_Click(this); skm_LockScreen('One Moment Please...');"
                                OnCheckedChanged="CheckBox_CheckChanged" />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cbAll" runat="server" AutoPostBack="true" onclick="checkAll(this); skm_LockScreen('One Moment Please...');"
                                OnCheckedChanged="CheckBox_CheckChanged" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Select All" DataField="fullname">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
        <div id="content5">
            <asp:Label ID="lblCount" runat="server" Style="font-family: Arial, Helvetica, sans-serif;
                font-size: medium"></asp:Label>
            <br />
            <asp:GridView ID="gvRecipientsL2S" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="None" Width="223px">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField DataField="fullname" HeaderText="Recipients List">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
            </asp:GridView>
        </div>
        <asp:Button ID="btnPrev" runat="server" Style="top: 155px; left: 528px; position: absolute;
            height: 26px; width: 56px" Text="Prev" />
        <asp:Button ID="btnNext" runat="server" Style="top: 188px; left: 528px; position: absolute;
            height: 26px; width: 56px" Text="Next" />
    </div>
</asp:Content>
