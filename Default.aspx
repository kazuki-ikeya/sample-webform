<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm._Default" %>
<%@ Register Src="~/Controls/UserControls/CommonDropDown.ascx" TagPrefix="uc" TagName="CommonDropDown" %>
<%@ Register Src="~/Controls/UserControls/CommonLabel.ascx" TagPrefix="uc" TagName="CommonLabel" %>
<%@ Register TagPrefix="cc" Namespace="WebForm.Controls" Assembly="WebForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>DropDown ReadOnly サンプル</title>
    <style>
        body {
            font-family: "Segoe UI", Arial, sans-serif;
            margin: 40px;
            color: #222;
        }

        main {
            max-width: 720px;
        }

        h1 {
            font-size: 28px;
            margin-bottom: 12px;
        }

        .sample-form {
            display: grid;
            gap: 16px;
            margin-top: 24px;
        }

        .field {
            display: grid;
            gap: 6px;
        }

        .field label,
        .check-field label {
            font-weight: 600;
        }

        .actions {
            display: flex;
            align-items: center;
            gap: 12px;
        }

        .button {
            padding: 8px 14px;
            border: 1px solid #0969da;
            border-radius: 6px;
            background: #0969da;
            color: #fff;
            cursor: pointer;
        }

        .message {
            padding: 16px;
            border: 1px solid #d0d7de;
            border-radius: 6px;
            background: #f6f8fa;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <main>
            <h1>DropDown ReadOnly サンプル</h1>
            <div class="sample-form">
                <div class="field">
                    <cc:CustomLabel ID="CommonDropDownLabel" runat="server" AssociatedControlID="CommonDropDown1" Text="CommonDropDown" Required="true" Description="ユーザーコントロール版のドロップダウンです。" />
                    <uc:CommonDropDown ID="CommonDropDown1" runat="server" />
                </div>

                <div class="check-field">
                    <asp:CheckBox ID="CommonReadOnlyCheckBox" runat="server" Text="CommonDropDown を ReadOnly にする" AutoPostBack="true" OnCheckedChanged="ReadOnlyCheckBox_CheckedChanged" />
                </div>

                <div class="field">
                    <cc:CustomLabel ID="BaseDropDownLabel" runat="server" AssociatedControlID="BaseDropDown1" Text="BaseDropDown" Required="true" Description="カスタムコントロール版のドロップダウンです。" />
                    <cc:CustomDropDown ID="BaseDropDown1" runat="server" DataValueField="Value" DataTextField="Text" />
                </div>

                <div class="check-field">
                    <asp:CheckBox ID="BaseReadOnlyCheckBox" runat="server" Text="BaseDropDown を ReadOnly にする" AutoPostBack="true" OnCheckedChanged="ReadOnlyCheckBox_CheckedChanged" />
                </div>

                <div class="actions">
                    <asp:Button ID="PostButton" runat="server" Text="POST 送信" CssClass="button" OnClick="PostButton_Click" />
                    <asp:Label ID="StatusLabel" runat="server" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </div>

                <div class="message">
                    <asp:Label ID="MessageLabel" runat="server" />
                </div>
            </div>
        </main>
        <div>
            <cc:CustomLabel ID="CustomLabel1" runat="server" Text="CustomLabel"></cc:CustomLabel>
            <br />
            <uc:CommonLabel ID="CommonLabel1" runat="server" Text="CommonLabel" Required="true" Description="ユーザーコントロール版の必須ラベルです。" />
        </div>
    </form>
</body>
</html>
