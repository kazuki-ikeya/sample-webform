<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NavigationSample.aspx.cs" Inherits="WebForm.NavigationSample" MasterPageFile="~/Site.master" %>

<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">ヘッダー・ツリーメニュー サンプル</asp:Content>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="alert alert-info mb-0">
        左側のツリーメニューを選択すると、MasterPage 側のタイトル、説明、選択情報が切り替わります。
    </div>
</asp:Content>
