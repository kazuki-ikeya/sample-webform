<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NavigationSample.aspx.cs" Inherits="WebForm.NavigationSample" MasterPageFile="~/Site.master" %>

<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">ヘッダー・ツリーメニュー サンプル</asp:Content>

<asp:Content ID="HeadContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style>
        body {
            background: #f6f8fb;
            color: #1f2937;
        }

        .app-header {
            min-height: 64px;
            border-bottom: 1px solid #cbd5e1;
            background: #0f4c81;
            color: #fff;
        }

        .app-shell {
            min-height: calc(100vh - 64px);
        }

        .side-menu {
            width: 280px;
            min-height: calc(100vh - 64px);
            border-right: 1px solid #cbd5e1;
            background: #fff;
        }

        .content-area {
            min-width: 0;
        }

        .tree-menu a {
            color: #1f2937;
            text-decoration: none;
        }

        .tree-menu a:hover {
            color: #0d6efd;
            text-decoration: underline;
        }

        .tree-menu table {
            margin: 2px 0;
        }
    </style>
</asp:Content>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="app-shell d-flex">
        <aside class="side-menu p-3">
            <div class="fw-semibold mb-3">メニュー</div>
            <asp:TreeView
                ID="SampleTreeView"
                runat="server"
                CssClass="tree-menu"
                ExpandDepth="2"
                ShowLines="true"
                OnSelectedNodeChanged="SampleTreeView_SelectedNodeChanged">
                <Nodes>
                    <asp:TreeNode Text="ダッシュボード" Value="Dashboard" Selected="true" />
                    <asp:TreeNode Text="伝票管理" Value="Slip">
                        <asp:TreeNode Text="伝票検索" Value="SlipSearch" />
                        <asp:TreeNode Text="伝票登録" Value="SlipEntry" />
                        <asp:TreeNode Text="伝票承認" Value="SlipApproval" />
                    </asp:TreeNode>
                    <asp:TreeNode Text="マスタ管理" Value="Master">
                        <asp:TreeNode Text="取引先マスタ" Value="CustomerMaster" />
                        <asp:TreeNode Text="部門マスタ" Value="DepartmentMaster" />
                        <asp:TreeNode Text="ユーザーマスタ" Value="UserMaster" />
                    </asp:TreeNode>
                    <asp:TreeNode Text="設定" Value="Settings">
                        <asp:TreeNode Text="画面設定" Value="DisplaySettings" />
                        <asp:TreeNode Text="権限設定" Value="PermissionSettings" />
                    </asp:TreeNode>
                </Nodes>
            </asp:TreeView>
        </aside>

        <main class="content-area flex-grow-1 p-4">
            <div class="card border-0 shadow-sm">
                <div class="card-body">
                    <asp:Label ID="ContentTitleLabel" runat="server" CssClass="h4 d-block mb-3" />
                    <asp:Label ID="ContentDescriptionLabel" runat="server" CssClass="text-muted d-block mb-4" />

                    <div class="row g-3">
                        <div class="col-md-4">
                            <div class="border rounded p-3 bg-light">
                                <div class="fw-semibold">選択値</div>
                                <asp:Label ID="SelectedValueLabel" runat="server" CssClass="text-muted" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="border rounded p-3 bg-light">
                                <div class="fw-semibold">階層</div>
                                <asp:Label ID="SelectedPathLabel" runat="server" CssClass="text-muted" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="border rounded p-3 bg-light">
                                <div class="fw-semibold">状態</div>
                                <span class="text-muted">選択中のメニュー内容を表示しています。</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </main>
    </div>
</asp:Content>
