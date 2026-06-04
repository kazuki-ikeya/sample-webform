using System;
using System.Collections.Generic;
using System.Linq;
using WebForm.Models;

namespace WebForm.Services
{
    /// <summary>
    /// 伝票検索画面用のサンプルデータを提供します。
    /// </summary>
    public class SlipSampleService
    {
        /// <summary>
        /// 検索条件に一致する伝票データを取得します。
        /// </summary>
        /// <param name="slipNo">伝票番号。</param>
        /// <param name="title">タイトル。</param>
        /// <param name="slipDate">伝票日付。</param>
        /// <returns>検索条件に一致する伝票データ。</returns>
        public IList<SlipModel> Search(string slipNo, string title, DateTime? slipDate)
        {
            IEnumerable<SlipModel> query = CreateSampleData();

            if (!string.IsNullOrWhiteSpace(slipNo))
            {
                query = query.Where(slip => slip.SlipNo.IndexOf(slipNo, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(slip => slip.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (slipDate.HasValue)
            {
                query = query.Where(slip => slip.SlipDate.Date == slipDate.Value.Date);
            }

            return query
                .Where(slip => IsRequiredDataValid(slip))
                .ToList();
        }

        /// <summary>
        /// サンプル伝票データを作成します。
        /// </summary>
        /// <returns>サンプル伝票データ。</returns>
        private static IList<SlipModel> CreateSampleData()
        {
            return new List<SlipModel>
            {
                new SlipModel { SlipNo = "SLP-0001", Title = "事務用品購入", SlipDate = new DateTime(2026, 6, 1), Remarks = "月初購入分" },
                new SlipModel { SlipNo = "SLP-0002", Title = "交通費精算", SlipDate = new DateTime(2026, 6, 2), Remarks = "営業訪問" },
                new SlipModel { SlipNo = "SLP-0003", Title = "会議費", SlipDate = new DateTime(2026, 6, 3), Remarks = "部門会議" },
                new SlipModel { SlipNo = "SLP-0004", Title = "備品修理", SlipDate = new DateTime(2026, 6, 4), Remarks = string.Empty },
                new SlipModel { SlipNo = "SLP-0005", Title = "ソフトウェア利用料", SlipDate = new DateTime(2026, 6, 5), Remarks = "年間契約" },
                new SlipModel { SlipNo = "SLP-0006", Title = "研修費", SlipDate = new DateTime(2026, 6, 8), Remarks = "新人研修" },
                new SlipModel { SlipNo = "SLP-0007", Title = "消耗品補充", SlipDate = new DateTime(2026, 6, 10), Remarks = null },
                new SlipModel { SlipNo = "SLP-0008", Title = "出張宿泊費", SlipDate = new DateTime(2026, 6, 11), Remarks = "大阪出張" },
                new SlipModel { SlipNo = "SLP-0009", Title = "通信費", SlipDate = new DateTime(2026, 6, 12), Remarks = "携帯回線" },
                new SlipModel { SlipNo = "SLP-0010", Title = "資料印刷費", SlipDate = new DateTime(2026, 6, 15), Remarks = "提案資料" },
                new SlipModel { SlipNo = "SLP-0011", Title = "郵送費", SlipDate = new DateTime(2026, 6, 16), Remarks = "契約書送付" },
                new SlipModel { SlipNo = "SLP-0012", Title = "設備点検費", SlipDate = new DateTime(2026, 6, 17), Remarks = "定期点検" },
                new SlipModel { SlipNo = "SLP-0013", Title = "清掃費", SlipDate = new DateTime(2026, 6, 18), Remarks = "臨時清掃" },
                new SlipModel { SlipNo = "SLP-0014", Title = "広告掲載費", SlipDate = new DateTime(2026, 6, 19), Remarks = "求人広告" },
                new SlipModel { SlipNo = "SLP-0015", Title = "会場利用料", SlipDate = new DateTime(2026, 6, 22), Remarks = "説明会" }
            };
        }

        /// <summary>
        /// 一覧上の必須項目が設定されているかどうかを判定します。
        /// </summary>
        /// <param name="slip">伝票データ。</param>
        /// <returns>必須項目が設定されている場合は true。それ以外の場合は false。</returns>
        private static bool IsRequiredDataValid(SlipModel slip)
        {
            return slip != null
                && !string.IsNullOrWhiteSpace(slip.SlipNo)
                && !string.IsNullOrWhiteSpace(slip.Title)
                && slip.SlipDate != DateTime.MinValue;
        }
    }
}
