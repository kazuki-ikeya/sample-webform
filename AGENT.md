# AGENTS.md

## プロジェクト概要

このリポジトリは ASP.NET Web Forms（.NET Framework / C#）のプロジェクトです。

主な構成:
- 画面: `.aspx`
- 画面コードビハインド: `.aspx.cs`
- デザイナ: `.aspx.designer.cs`
- 共通部品: `.ascx`
- 共通部品コードビハインド: `.ascx.cs`
- 設定: `Web.config`

## 基本方針

- ASP.NET Core / Razor / Blazor の構成に変更しない
- Web Forms の既存構成を尊重する
- 既存の namespace、フォルダ構成、命名規則に合わせる
- 既存ファイルを大きく作り替えない
- 変更は最小限にする
- 変更前に関連ファイルを確認する
- 不要なライブラリを追加しない
- 自動生成ファイルの扱いに注意する
- xmlコメントは日本語で付与する

## Web Forms の実装ルール

### 画面

- `.aspx` には UI 定義を書く
- `.aspx.cs` にはイベント処理を書く
- `.designer.cs` は原則として直接編集しない
- `runat="server"` が必要なサーバーコントロールには必ず付ける
- サーバー側から参照するコントロールには `ID` を付ける

### Page_Load

- 初期表示処理は原則として `if (!IsPostBack)` の中で行う
- PostBack 時に入力値や選択値が消えないようにする

例:

```csharp
protected void Page_Load(object sender, EventArgs e)
{
    if (!IsPostBack)
    {
        Initialize();
    }
}