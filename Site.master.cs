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
            }
        }
    }
}
