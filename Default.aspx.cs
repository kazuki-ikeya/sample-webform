using System;
using System.Data;

namespace WebForm
{
    /// <summary>
    /// サンプル画面のページクラスです。
    /// </summary>
    public partial class _Default : System.Web.UI.Page
    {
        /// <summary>
        /// ページ読み込み時の処理を行います。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">イベントデータ。</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CommonDropDown1.DataSource = CreateSampleDropDownDataSource();
                CommonDropDown1.SelectedValue = "2";
                ApplyReadOnly();

                ShowPostResult("初期表示");
            }
        }

        /// <summary>
        /// ReadOnly チェックボックス変更時の処理を行います。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">イベントデータ。</param>
        protected void ReadOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ApplyReadOnly();
            ShowPostResult("ReadOnly 切替 POST");
        }

        /// <summary>
        /// POST 送信ボタンクリック時の処理を行います。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">イベントデータ。</param>
        protected void PostButton_Click(object sender, EventArgs e)
        {
            ApplyReadOnly();
            ShowPostResult("ボタン POST");
        }

        /// <summary>
        /// CommonDropDown に ReadOnly 状態を適用します。
        /// </summary>
        private void ApplyReadOnly()
        {
            CommonDropDown1.ReadOnly = ReadOnlyCheckBox.Checked;
        }

        /// <summary>
        /// POST 結果を画面に表示します。
        /// </summary>
        /// <param name="actionName">実行された操作名。</param>
        private void ShowPostResult(string actionName)
        {
            string roleKbn = CommonDropDown1.SelectedValue;

            StatusLabel.Text = "現在の ReadOnly: " + CommonDropDown1.ReadOnly;
            MessageLabel.Text =
                "操作: " + actionName + "<br />" +
                "IsPostBack: " + IsPostBack + "<br />" +
                "選択された権限区分: " + Server.HtmlEncode(roleKbn);
        }

        /// <summary>
        /// CommonDropDown に渡すサンプル DataTable を作成します。
        /// </summary>
        /// <returns>Value 列と Text 列を持つ DataTable。</returns>
        private static DataTable CreateSampleDropDownDataSource()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Value", typeof(string));
            dataTable.Columns.Add("Text", typeof(string));

            dataTable.Rows.Add(string.Empty, "\u672a\u9078\u629e");
            dataTable.Rows.Add("1", "\u7ba1\u7406\u8005");
            dataTable.Rows.Add("2", "\u4e00\u822c\u30e6\u30fc\u30b6\u30fc");
            dataTable.Rows.Add("3", "\u30b2\u30b9\u30c8");

            return dataTable;
        }
    }
}
