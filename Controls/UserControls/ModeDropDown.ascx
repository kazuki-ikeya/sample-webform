<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModeDropDown.ascx.cs" Inherits="WebForm.Controls.ModeDropDown" %>

<asp:DropDownList ID="ModeDropDownList" runat="server" DataValueField="Value" DataTextField="Text" CssClass="form-select" />
<asp:Label ID="ModeLabel" runat="server" Visible="false" CssClass="form-control-plaintext text-center w-100" />
