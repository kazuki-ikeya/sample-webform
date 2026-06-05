using System;
using System.Web.UI;

namespace WebForm
{
    /// <summary>
    /// 共通ヘッダーとツリーメニューを確認するサンプル画面です。
    /// </summary>
    public partial class NavigationSample : Page
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
                HeaderTitleTextBox.Text = "NavigationSample から変更したタイトル";
                HeaderDescriptionTextBox.Text = "この文言はコンテンツページから MasterPage の公開メソッドを呼び出して設定しています。";
                ApplyHeaderText();
            }
        }

        /// <summary>
        /// 共通ヘッダー変更ボタン押下時の処理を行います。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">イベントデータ。</param>
        protected void ChangeHeaderButton_Click(object sender, EventArgs e)
        {
            ApplyHeaderText();
        }

        /// <summary>
        /// 入力されたタイトルと説明を MasterPage の共通ヘッダーに反映します。
        /// </summary>
        private void ApplyHeaderText()
        {
            var siteMaster = Master as SiteMaster;
            if (siteMaster == null)
            {
                return;
            }

            siteMaster.SetHeaderText(HeaderTitleTextBox.Text, HeaderDescriptionTextBox.Text);
        }
    }
}
