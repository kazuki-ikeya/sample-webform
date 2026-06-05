<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModeDate.ascx.cs" Inherits="WebForm.Controls.ModeDate" %>

<asp:TextBox ID="ModeDateTextBox" runat="server" TextMode="Date" CssClass="form-control" />
<asp:Label ID="ModeDateLabel" runat="server" Visible="false" CssClass="form-control-plaintext text-center w-100" />
<asp:CustomValidator
    ID="FutureDateValidator"
    runat="server"
    ControlToValidate="ModeDateTextBox"
    Display="Dynamic"
    CssClass="text-danger small"
    OnServerValidate="ServerValidate" />
