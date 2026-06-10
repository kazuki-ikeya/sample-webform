using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using NLog;

namespace WebForm
{
    /// <summary>
    /// サンプル画面のページクラスです。
    /// </summary>
    public partial class _Default : System.Web.UI.Page
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// ページ初期化時の処理を行います。
        /// </summary>
        /// <param name="e">イベントデータ。</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ModeDate1.CustomValidate += ModeDate1_CustomValidate;
        }

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

                ModeDropDown1.DataValueField = "Key";
                ModeDropDown1.DataTextField = "Name";
                ModeDropDown1.DataSource = dataSource;
                ModeDropDown1.SelectedValue = "2";

                ModeDate1.SelectedDate = DateTime.Today;
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

            if (!Page.IsValid)
            {
                ShowPostResult("入力エラー");
                return;
            }

            ShowPostResult("ボタン POST");
        }

        /// <summary>
        /// パネル風ボタンクリック時の処理を行います。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">イベントデータ。</param>
        protected void PanelButton_Click(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            string commandName = button == null ? string.Empty : button.CommandName;

            ApplyReadOnly();
            ShowPostResult("パネルボタン: " + commandName);
        }

        /// <summary>
        /// CommonDropDown に ReadOnly 状態を適用します。
        /// </summary>
        /// <summary>
        /// NLog のログ出力を確認するためのサンプルログを出力します。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">イベントデータ。</param>
        protected void NLogSampleButton_Click(object sender, EventArgs e)
        {
            string logDirectory = Server.MapPath("~/App_Data/logs");
            Directory.CreateDirectory(logDirectory);

            Logger.Info("NLog sample info. UserHostAddress={0}, IsPostBack={1}", Request.UserHostAddress, IsPostBack);
            Logger.Warn("NLog sample warning. Path={0}", Request.Path);
            Logger.Error("NLog sample error check. Time={0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
            LogManager.Flush();

            NLogSampleResultLabel.Text =
                "NLog にサンプルログを出力しました。出力先: " +
                Server.HtmlEncode(Path.Combine(logDirectory, "default-sample-" + DateTime.Today.ToString("yyyy-MM-dd") + ".log"));
        }

        private void ApplyReadOnly()
        {
            CommonDropDown1.ReadOnly = CommonReadOnlyCheckBox.Checked;
            BaseDropDown1.ReadOnly = BaseReadOnlyCheckBox.Checked;
            ModeDropDown1.ReadOnly = ModeReadOnlyCheckBox.Checked;
            ModeDate1.ReadOnly = ModeDateReadOnlyCheckBox.Checked;
        }

        /// <summary>
        /// POST 結果を画面に表示します。
        /// </summary>
        /// <param name="actionName">実行された操作名。</param>
        private void ShowPostResult(string actionName)
        {
            string commonRoleKbn = CommonDropDown1.SelectedValue;
            string baseRoleKbn = BaseDropDown1.SelectedValue;
            string modeRoleKbn = ModeDropDown1.SelectedValue;
            string modeDate = ModeDate1.SelectedDate.HasValue
                ? ModeDate1.SelectedDate.Value.ToString("yyyy/MM/dd")
                : ModeDate1.Text;

            StatusLabel.Text =
                "CommonDropDown ReadOnly: " + CommonDropDown1.ReadOnly +
                " / BaseDropDown ReadOnly: " + BaseDropDown1.ReadOnly +
                " / ModeDropDown ReadOnly: " + ModeDropDown1.ReadOnly +
                " / ModeDate ReadOnly: " + ModeDate1.ReadOnly;
            MessageLabel.Text =
                "操作: " + actionName + "<br />" +
                "IsPostBack: " + IsPostBack + "<br />" +
                "CommonDropDown 選択値: " + Server.HtmlEncode(commonRoleKbn) + "<br />" +
                "BaseDropDown 選択値: " + Server.HtmlEncode(baseRoleKbn) + "<br />" +
                "ModeDropDown 選択値: " + Server.HtmlEncode(modeRoleKbn) + "<br />" +
                "ModeDropDown 表示テキスト: " + Server.HtmlEncode(ModeDropDown1.SelectedText) + "<br />" +
                "ModeDate 入力値: " + Server.HtmlEncode(modeDate);
        }

        /// <summary>
        /// ModeDate に外部から追加する検証処理を行います。
        /// </summary>
        /// <param name="source">イベント発生元。</param>
        /// <param name="args">検証イベントデータ。</param>
        private void ModeDate1_CustomValidate(object source, ServerValidateEventArgs args)
        {
            DateTime inputDate;
            if (!DateTime.TryParse(args.Value, out inputDate))
            {
                return;
            }

            // 未来日付不可
            args.IsValid = inputDate.Date <= DateTime.Today;
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
