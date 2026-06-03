<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm._Default" %>
<%@ Register Src="~/Controls/CommonDropDown.ascx" TagPrefix="uc" TagName="CommonDropDown" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>CommonDropDown POST サンプル</title>
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
            <h1>CommonDropDown POST サンプル</h1>
            <div class="sample-form">
                <div class="field">
                    <asp:Label ID="DropDownLabel" runat="server" AssociatedControlID="CommonDropDown1" Text="test" />
                    <uc:CommonDropDown ID="CommonDropDown1" runat="server" />
                </div>

                <div class="check-field">
                    <asp:CheckBox ID="ReadOnlyCheckBox" runat="server" Text="ReadOnly にする" AutoPostBack="true" OnCheckedChanged="ReadOnlyCheckBox_CheckedChanged" />
                </div>

                <div class="actions">
                    <asp:Button ID="PostButton" runat="server" Text="POST 送信" CssClass="button" OnClick="PostButton_Click" />
                    <asp:Label ID="StatusLabel" runat="server" />
                </div>

                <div class="message">
                    <asp:Label ID="MessageLabel" runat="server" />
                </div>
            </div>
        </main>
    </form>
</body>
</html>
