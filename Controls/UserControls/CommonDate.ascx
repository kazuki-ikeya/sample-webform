<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonDate.ascx.cs" Inherits="WebForm.Controls.CommonDate" %>

<asp:Panel ID="DateInputGroup" runat="server" CssClass="input-group">
    <asp:TextBox ID="DateTextBox" runat="server" CssClass="form-control" />
    <span class="input-group-text common-date-icon" title="カレンダーを開く" aria-hidden="true">&#128197;</span>
</asp:Panel>
<asp:Label ID="DateLabel" runat="server" Visible="false" CssClass="form-control-plaintext text-center w-100" />
