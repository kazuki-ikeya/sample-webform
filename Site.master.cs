using System;
using System.Web.UI;

namespace WebForm
{
    /// <summary>
    /// 共通レイアウト用の Master Page です。
    /// </summary>
    public partial class SiteMaster : MasterPage
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
                HeaderStatusLabel.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                ApplySelectedMenu();
            }
        }

        /// <summary>
        /// 共通ヘッダーのタイトルと説明を設定します。
        /// </summary>
        /// <param name="title">タイトル。</param>
        /// <param name="description">説明文。</param>
        public void SetHeaderText(string title, string description)
        {
            ContentTitleLabel.Text = title;
            ContentDescriptionLabel.Text = description;
        }

        /// <summary>
        /// ツリーメニュー選択変更時の処理を行います。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">イベントデータ。</param>
        protected void SampleTreeView_SelectedNodeChanged(object sender, EventArgs e)
        {
            ApplySelectedMenu();
        }

        /// <summary>
        /// 選択中メニューの内容を共通タイトルエリアに反映します。
        /// </summary>
        private void ApplySelectedMenu()
        {
            if (SampleTreeView.SelectedNode == null)
            {
                return;
            }

            SetHeaderText(
                SampleTreeView.SelectedNode.Text,
                GetDescription(SampleTreeView.SelectedNode.Value));
            SelectedValueLabel.Text = SampleTreeView.SelectedNode.Value;
            SelectedPathLabel.Text = SampleTreeView.SelectedNode.ValuePath;
        }

        /// <summary>
        /// メニュー値に対応する説明文を取得します。
        /// </summary>
        /// <param name="value">メニュー値。</param>
        /// <returns>説明文。</returns>
        private static string GetDescription(string value)
        {
            switch (value)
            {
                case "SlipSearch":
                    return "伝票の検索条件入力と一覧表示へ進むメニューです。";
                case "SlipEntry":
                    return "新しい伝票を登録するための入力画面を想定したメニューです。";
                case "SlipApproval":
                    return "申請済み伝票の承認処理を想定したメニューです。";
                case "CustomerMaster":
                case "DepartmentMaster":
                case "UserMaster":
                    return "各種マスタの検索・保守画面を想定したメニューです。";
                case "DisplaySettings":
                case "PermissionSettings":
                    return "システム設定や権限設定を想定したメニューです。";
                case "DateControlVerification":
                    return "標準日付入力、datepicker、flatpickr の動作を比較する検証画面です。";
                default:
                    return "共通ヘッダーと左側ツリーメニューを持つレイアウトのサンプルです。";
            }
        }
    }
}
