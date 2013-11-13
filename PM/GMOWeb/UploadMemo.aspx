<%@ Page Language="VB" MasterPageFile="~/LeftPageMaster.master" AutoEventWireup="false"
    CodeFile="UploadMemo.aspx.vb" Inherits="UploadMemo" Title="GMO Web Version 2.0" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftContentPlaceHolder" runat="Server">
    <div id="content2">
        <table style="width: 100%">
            <tr>
                <td align="right" style="width: 79px">
                    <asp:Label ID="Label1" runat="server" Text="Memo Date "></asp:Label>
                    <asp:Label ID="Label5" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" TabIndex="1" Width="97px" onMouseDown="whichButton(event)"
                        onKeyDown="return noCTRL(event)"></asp:TextBox>
                    <asp:CalendarExtender ID="calDateBehaviour" runat="server" Enabled="True" TargetControlID="txtStartDate"
                        PopupPosition="Left">
                    </asp:CalendarExtender>
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* No Date!"
                        ControlToValidate="txtStartDate"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <td align="right" style="width: 79px">
                <asp:Label ID="Label2" runat="server" Text="Circular No. "></asp:Label>
                <asp:Label ID="Label6" runat="server" Text=":"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCircular" runat="server" MaxLength="15" Width="100px" TabIndex="2"
                    onMouseDown="whichButton(event)" onKeyDown="return noCTRL(event)"></asp:TextBox>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="* Circular number already exists!"
                    ControlToValidate="txtCircular" OnServerValidate="validateCircular" SetFocusOnError="true"
                    ValidateEmptyText="true">
                </asp:CustomValidator>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* No Circular!"
                    ControlToValidate="txtCircular"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtTRXNumber" runat="server" Width="80px">1022010000</asp:TextBox>
            </td>
            <tr>
                <td align="right" style="width: 79px">
                    <asp:Label ID="Label3" runat="server" Text="Subject "></asp:Label>
                    <asp:Label ID="Label7" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSubject" runat="server" Width="366px" MaxLength="100" TabIndex="3"
                        onMouseDown="whichButton(event)" onKeyDown="return noCTRL(event)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* No Subject!"
                        ControlToValidate="txtSubject"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 79px">
                    <asp:Label ID="Label4" runat="server" Text="File "></asp:Label>
                    <asp:Label ID="Label8" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <input id="AddFile" type="button" value="Add File" onclick="AddFileUpload()" />
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="105px" TabIndex="5" />
                    <asp:Label ID="Statuslabel" runat="server" Font-Size="Small" Style="color: #FF0000"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 79px; height: 16px">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    <div id="FileUploadContainer">
                        <!--FileUpload Controls will be added here -->
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 79px; height: 16px;">
                    &nbsp;
                </td>
                <td align="center" style="height: 16px">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
