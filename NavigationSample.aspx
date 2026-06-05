<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NavigationSample.aspx.cs" Inherits="WebForm.NavigationSample" MasterPageFile="~/Site.master" %>

<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">ヘッダー・ツリーメニュー サンプル</asp:Content>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="card border-0 shadow-sm">
        <div class="card-body">
            <div class="mb-3">
                <label for="<%= HeaderTitleTextBox.ClientID %>" class="form-label">共通ヘッダータイトル</label>
                <asp:TextBox ID="HeaderTitleTextBox" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="<%= HeaderDescriptionTextBox.ClientID %>" class="form-label">共通ヘッダー説明</label>
                <asp:TextBox ID="HeaderDescriptionTextBox" runat="server" CssClass="form-control" />
            </div>
            <asp:Button
                ID="ChangeHeaderButton"
                runat="server"
                Text="共通ヘッダーを変更"
                CssClass="btn btn-primary"
                OnClick="ChangeHeaderButton_Click" />
        </div>
    </div>
</asp:Content>
