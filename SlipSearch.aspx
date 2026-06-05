<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SlipSearch.aspx.cs" Inherits="WebForm.SlipSearch" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>伝票検索</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous" />
    <style>
        body {
            font-family: "Segoe UI", Arial, sans-serif;
            margin: 40px;
            color: #222;
        }

        main {
            max-width: 980px;
        }

        h1 {
            font-size: 26px;
            margin: 0 0 20px;
        }

        .search-table {
            width: 100%;
            border-collapse: collapse;
            border: 1px solid #b6c6d8;
        }

        .search-table th,
        .search-table td {
            padding: 10px 12px;
            border: 1px solid #b6c6d8;
            vertical-align: middle;
        }

        .search-table th {
            width: 160px;
            background: #dbeafe;
            color: #17324d;
            text-align: left;
            font-weight: 600;
        }

        .search-table input {
            width: 100%;
            box-sizing: border-box;
            padding: 7px 8px;
            border: 1px solid #b6c6d8;
            border-radius: 4px;
            font: inherit;
        }

        .actions {
            margin: 16px 0 20px;
        }

        .button {
            padding: 8px 16px;
            border: 1px solid #0969da;
            border-radius: 6px;
            background: #0969da;
            color: #fff;
            cursor: pointer;
        }

        .error-message {
            display: block;
            margin: 8px 0 0;
            color: #b42318;
            font-weight: 600;
        }

        .result-grid {
            width: 100%;
            border-collapse: collapse;
        }

        .result-grid th,
        .result-grid td {
            padding: 9px 10px;
            border: 1px solid #c7d2df;
        }

        .result-grid th {
            background: #eaf2fb;
            color: #17324d;
            text-align: left;
        }

        .result-grid th a {
            color: #17324d;
            text-decoration: none;
        }

        .result-grid tr:nth-child(even) td {
            background: #f8fafc;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <main class="container py-4">
            <h1 class="h3 mb-4">伝票検索</h1>

            <table class="table table-bordered align-middle search-table">
                <tbody>
                    <tr>
                        <th>
                            <asp:Label ID="SlipNoLabel" runat="server" AssociatedControlID="SlipNoTextBox" Text="伝票番号" CssClass="form-label mb-0" />
                        </th>
                        <td>
                            <asp:TextBox ID="SlipNoTextBox" runat="server" CssClass="form-control" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="TitleLabel" runat="server" AssociatedControlID="TitleTextBox" Text="タイトル" CssClass="form-label mb-0" />
                        </th>
                        <td>
                            <asp:TextBox ID="TitleTextBox" runat="server" CssClass="form-control" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="SlipDateLabel" runat="server" AssociatedControlID="SlipDateTextBox" Text="伝票日付" CssClass="form-label mb-0" />
                        </th>
                        <td>
                            <asp:TextBox ID="SlipDateTextBox" runat="server" TextMode="Date" CssClass="form-control" />
                        </td>
                    </tr>
                </tbody>
            </table>

            <div class="actions d-flex align-items-start gap-3">
                <asp:Button ID="SearchButton" runat="server" Text="検索" CssClass="btn btn-primary" OnClick="SearchButton_Click" />
                <asp:Label ID="ErrorMessageLabel" runat="server" CssClass="text-danger fw-semibold mt-2" EnableViewState="false" />
            </div>

            <asp:GridView
                ID="SlipGridView"
                runat="server"
                AutoGenerateColumns="false"
                AllowSorting="true"
                AllowPaging="true"
                PageSize="10"
                CssClass="table table-striped table-bordered table-hover align-middle result-grid"
                EmptyDataText="該当する伝票はありません。"
                OnSorting="SlipGridView_Sorting"
                OnPageIndexChanging="SlipGridView_PageIndexChanging">
                <PagerSettings
                    Mode="NumericFirstLast"
                    FirstPageText="先頭"
                    LastPageText="最後"
                    NextPageText="次へ"
                    PreviousPageText="前へ" />
                <Columns>
                    <asp:BoundField HeaderText="伝票番号 *" DataField="SlipNo" SortExpression="SlipNo" />
                    <asp:BoundField HeaderText="タイトル *" DataField="Title" SortExpression="Title" />
                    <asp:BoundField HeaderText="伝票日付 *" DataField="SlipDate" SortExpression="SlipDate" DataFormatString="{0:yyyy/MM/dd}" />
                    <asp:BoundField HeaderText="備考" DataField="Remarks" />
                </Columns>
            </asp:GridView>
        </main>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
</body>
</html>
