using System;

namespace WebForm.Models
{
    /// <summary>
    /// 伝票検索一覧に表示する伝票データです。
    /// </summary>
    public class SlipModel
    {
        /// <summary>
        /// 伝票番号を取得または設定します。
        /// </summary>
        public string SlipNo { get; set; }

        /// <summary>
        /// タイトルを取得または設定します。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 伝票日付を取得または設定します。
        /// </summary>
        public DateTime SlipDate { get; set; }

        /// <summary>
        /// 備考を取得または設定します。
        /// </summary>
        public string Remarks { get; set; }
    }
}
