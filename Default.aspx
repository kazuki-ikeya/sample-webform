<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm._Default" %>
<%@ Register Src="~/Controls/UserControls/CommonDropDown.ascx" TagPrefix="uc" TagName="CommonDropDown" %>
<%@ Register Src="~/Controls/UserControls/CommonLabel.ascx" TagPrefix="uc" TagName="CommonLabel" %>
<%@ Register Src="~/Controls/UserControls/ModeDate.ascx" TagPrefix="uc" TagName="ModeDate" %>
<%@ Register Src="~/Controls/UserControls/ModeDropDown.ascx" TagPrefix="uc" TagName="ModeDropDown" %>
<%@ Register TagPrefix="cc" Namespace="WebForm.Controls" Assembly="WebForm" %>

<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">ヘッダー・ツリーメニュー サンプル</asp:Content>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
   
        <main class="container py-4">
            <h1 class="h3 mb-4">DropDown ReadOnly サンプル</h1>
            <div class="panel-button-list">
                <asp:LinkButton ID="PanelSearchButton" runat="server" CssClass="panel-button" CausesValidation="false" CommandName="Search" OnClick="PanelButton_Click">
                    <span class="panel-button-title">検索</span>
                    <span class="panel-button-text">条件を指定して一覧を確認します。</span>
                </asp:LinkButton>
                <asp:LinkButton ID="PanelRegisterButton" runat="server" CssClass="panel-button" CausesValidation="false" CommandName="Register" OnClick="PanelButton_Click">
                    <span class="panel-button-title">登録</span>
                    <span class="panel-button-text">新しいデータの入力を開始します。</span>
                </asp:LinkButton>
                <asp:LinkButton ID="PanelExportButton" runat="server" CssClass="panel-button" CausesValidation="false" CommandName="Export" OnClick="PanelButton_Click">
                    <span class="panel-button-title">出力</span>
                    <span class="panel-button-text">表示中の内容を出力します。</span>
                </asp:LinkButton>
            </div>
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

                <div class="field mb-3">
                    <cc:CustomLabel ID="ModeDateLabel" runat="server" AssociatedControlID="ModeDate1" Text="ModeDate" Required="true" Description="未来日付禁止と外部検証を確認する日付入力です。" />
                    <uc:ModeDate ID="ModeDate1" runat="server" RejectFutureDate="true" FutureDateErrorMessage="未来日または日曜日は入力できません。" ValidationGroup="DefaultTest" />
                </div>

                <div class="check-field form-check">
                    <asp:CheckBox ID="ModeDateReadOnlyCheckBox" runat="server" Text="ModeDate を ReadOnly にする" AutoPostBack="true" OnCheckedChanged="ReadOnlyCheckBox_CheckedChanged" CssClass="form-check-label" />
                </div>

                <div class="actions d-flex align-items-center gap-3">
                    <asp:Button ID="PostButton" runat="server" Text="POST 送信" CssClass="btn btn-primary" OnClick="PostButton_Click" ValidationGroup="DefaultTest" />
                    <asp:Label ID="StatusLabel" runat="server" CssClass="text-muted" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </div>

                <div class="alert alert-light border">
                    <asp:ValidationSummary ID="DefaultValidationSummary" runat="server" ValidationGroup="DefaultTest" CssClass="text-danger fw-semibold" />
                    <asp:Label ID="MessageLabel" runat="server" />
                </div>

                <div class="border rounded p-3 mt-3">
                    <h2 class="h5 mb-3">NLog 動作確認</h2>
                    <asp:Button ID="NLogSampleButton" runat="server" Text="NLog に出力" CssClass="btn btn-secondary" CausesValidation="false" OnClick="NLogSampleButton_Click" />
                    <asp:Label ID="NLogSampleResultLabel" runat="server" CssClass="d-block mt-2 text-muted" />
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
   
</asp:Content>
