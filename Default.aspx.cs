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
                DataTable dataSource = CreateSampleDropDownDataSource();
                CommonDropDown1.DataValueField = "Key";
                CommonDropDown1.DataTextField = "Name";
                CommonDropDown1.DataSource = dataSource;
                CommonDropDown1.SelectedValue = "2";

                BaseDropDown1.DataValueField = "Key";
                BaseDropDown1.DataTextField = "Name";
                BaseDropDown1.DataSource = dataSource;
                BaseDropDown1.DataBind();
                BaseDropDown1.SelectedValue = "2";

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
            CommonDropDown1.ReadOnly = CommonReadOnlyCheckBox.Checked;
            BaseDropDown1.ReadOnly = BaseReadOnlyCheckBox.Checked;
        }

        /// <summary>
        /// POST 結果を画面に表示します。
        /// </summary>
        /// <param name="actionName">実行された操作名。</param>
        private void ShowPostResult(string actionName)
        {
            string commonRoleKbn = CommonDropDown1.SelectedValue;
            string baseRoleKbn = BaseDropDown1.SelectedValue;

            StatusLabel.Text =
                "CommonDropDown ReadOnly: " + CommonDropDown1.ReadOnly +
                " / BaseDropDown ReadOnly: " + BaseDropDown1.ReadOnly;
            MessageLabel.Text =
                "操作: " + actionName + "<br />" +
                "IsPostBack: " + IsPostBack + "<br />" +
                "CommonDropDown 選択値: " + Server.HtmlEncode(commonRoleKbn) + "<br />" +
                "BaseDropDown 選択値: " + Server.HtmlEncode(baseRoleKbn);
        }

        /// <summary>
        /// CommonDropDown に渡すサンプル DataTable を作成します。
        /// </summary>
        /// <returns>Value 列と Text 列を持つ DataTable。</returns>
        private static DataTable CreateSampleDropDownDataSource()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Key", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));

            dataTable.Rows.Add(string.Empty, "未選択");
            dataTable.Rows.Add("1", "管理者");
            dataTable.Rows.Add("2", "一般ユーザー");
            dataTable.Rows.Add("3", "ゲスト");

            return dataTable;
        }
    }
}
