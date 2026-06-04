<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm._Default" %>
<%@ Register Src="~/Controls/UserControls/CommonDropDown.ascx" TagPrefix="uc" TagName="CommonDropDown" %>
<%@ Register Src="~/Controls/UserControls/CommonLabel.ascx" TagPrefix="uc" TagName="CommonLabel" %>
<%@ Register Src="~/Controls/UserControls/ModeDropDown.ascx" TagPrefix="uc" TagName="ModeDropDown" %>
<%@ Register TagPrefix="cc" Namespace="WebForm.Controls" Assembly="WebForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>DropDown ReadOnly サンプル</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous" />
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

        .input-table {
            width: 100%;
            margin-top: 24px;
            border-collapse: collapse;
            border: 1px solid #b6c6d8;
        }

        .input-table th,
        .input-table td {
            padding: 10px 12px;
            border: 1px solid #b6c6d8;
            vertical-align: middle;
        }

        .input-table th {
            width: 180px;
            background: #dbeafe;
            color: #17324d;
            text-align: left;
            font-weight: 600;
        }

        .input-table input,
        .input-table select,
        .input-table textarea {
            width: 100%;
            box-sizing: border-box;
            padding: 7px 8px;
            border: 1px solid #b6c6d8;
            border-radius: 4px;
            font: inherit;
        }

        .input-table textarea {
            min-height: 72px;
            resize: vertical;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <main class="container py-4">
            <h1 class="h3 mb-4">DropDown ReadOnly サンプル</h1>
            <div class="sample-form">
                <div class="field mb-3">
                    <cc:CustomLabel ID="CommonDropDownLabel" runat="server" AssociatedControlID="CommonDropDown1" Text="CommonDropDown" Required="true" Description="ユーザーコントロール版のドロップダウンです。" />
                    <uc:CommonDropDown ID="CommonDropDown1" runat="server" />
                </div>

                <div class="check-field form-check">
                    <asp:CheckBox ID="CommonReadOnlyCheckBox" runat="server" Text="CommonDropDown を ReadOnly にする" AutoPostBack="true" OnCheckedChanged="ReadOnlyCheckBox_CheckedChanged" CssClass="form-check-label" />
                </div>

                <div class="field mb-3">
                    <cc:CustomLabel ID="BaseDropDownLabel" runat="server" AssociatedControlID="BaseDropDown1" Text="BaseDropDown" Required="true" Description="カスタムコントロール版のドロップダウンです。" />
                    <cc:CustomDropDown ID="BaseDropDown1" runat="server" DataValueField="Value" DataTextField="Text" />
                </div>

                <div class="check-field form-check">
                    <asp:CheckBox ID="BaseReadOnlyCheckBox" runat="server" Text="BaseDropDown を ReadOnly にする" AutoPostBack="true" OnCheckedChanged="ReadOnlyCheckBox_CheckedChanged" CssClass="form-check-label" />
                </div>

                <div class="field mb-3">
                    <cc:CustomLabel ID="ModeDropDownLabel" runat="server" AssociatedControlID="ModeDropDown1" Text="ModeDropDown" Required="true" Description="ReadOnly 時にプルダウンを非表示にしてラベルを表示するドロップダウンです。" />
                    <uc:ModeDropDown ID="ModeDropDown1" runat="server" />
                </div>

                <div class="check-field form-check">
                    <asp:CheckBox ID="ModeReadOnlyCheckBox" runat="server" Text="ModeDropDown を ReadOnly にする" AutoPostBack="true" OnCheckedChanged="ReadOnlyCheckBox_CheckedChanged" CssClass="form-check-label" />
                </div>

                <div class="actions d-flex align-items-center gap-3">
                    <asp:Button ID="PostButton" runat="server" Text="POST 送信" CssClass="btn btn-primary" OnClick="PostButton_Click" />
                    <asp:Label ID="StatusLabel" runat="server" CssClass="text-muted" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </div>

                <div class="alert alert-light border">
                    <asp:Label ID="MessageLabel" runat="server" />
                </div>
            </div>
        </main>
        <div class="container mb-3">
            <cc:CustomLabel ID="CustomLabel1" runat="server" Text="CustomLabel"></cc:CustomLabel>
            <br />
            <uc:CommonLabel ID="CommonLabel1" runat="server" Text="CommonLabel" Required="true" Description="ユーザーコントロール版の必須ラベルです。" />
        </div>
        <table class="table table-bordered align-middle input-table">
            <tbody>
                <tr>
                    <th>
                        <cc:CustomLabel runat="server" Text="氏名" Required="true" Description="利用者の氏名を入力してください。" />
                    </th>
                    <td>
                        <input type="text" name="SampleName" class="form-control" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <cc:CustomLabel runat="server" Text="メールアドレス" Required="true" Description="連絡先メールアドレスを入力してください。" />
                    </th>
                    <td>
                        <input type="email" name="SampleEmail" class="form-control" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <cc:CustomLabel runat="server" Text="権限区分" Description="利用者に付与する権限を選択してください。" />
                    </th>
                    <td>
                        <select name="SampleRole" class="form-select">
                            <option value="">未選択</option>
                            <option value="1">管理者</option>
                            <option value="2">一般ユーザー</option>
                            <option value="3">ゲスト</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>
                        <cc:CustomLabel runat="server" Text="備考" Description="補足事項があれば入力してください。" />
                    </th>
                    <td>
                        <textarea name="SampleMemo" class="form-control"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
</body>
</html>
