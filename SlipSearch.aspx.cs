using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.Models;
using WebForm.Services;

namespace WebForm
{
    /// <summary>
    /// 伝票検索画面です。
    /// </summary>
    public partial class SlipSearch : Page
    {
        private const string SortExpressionViewStateKey = "SortExpression";
        private const string SortDirectionViewStateKey = "SortDirection";
        private const string SortableIcon = "↕";
        private const string AscendingIcon = "▲";
        private const string DescendingIcon = "▼";

        private readonly SlipSampleService slipSampleService = new SlipSampleService();

        /// <summary>
        /// ページ読み込み時の処理を行います。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">イベントデータ。</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState[SortExpressionViewStateKey] = "SlipNo";
                ViewState[SortDirectionViewStateKey] = SortDirection.Ascending;
                BindGrid();
            }
        }

        /// <summary>
        /// 検索ボタンクリック時の処理を行います。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">イベントデータ。</param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ErrorMessageLabel.Text = string.Empty;
            SlipGridView.PageIndex = 0;
            BindGrid();
        }

        /// <summary>
        /// 一覧ヘッダークリック時のソート処理を行います。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">ソートイベントデータ。</param>
        protected void SlipGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (!IsSortableExpression(e.SortExpression))
            {
                return;
            }

            string currentExpression = ViewState[SortExpressionViewStateKey] as string;
            SortDirection currentDirection = GetCurrentSortDirection();

            if (string.Equals(currentExpression, e.SortExpression, StringComparison.OrdinalIgnoreCase))
            {
                ViewState[SortDirectionViewStateKey] = currentDirection == SortDirection.Ascending
                    ? SortDirection.Descending
                    : SortDirection.Ascending;
            }
            else
            {
                ViewState[SortExpressionViewStateKey] = e.SortExpression;
                ViewState[SortDirectionViewStateKey] = SortDirection.Ascending;
            }

            BindGrid();
        }

        /// <summary>
        /// 一覧のページ移動時の処理を行います。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">ページ移動イベントデータ。</param>
        protected void SlipGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SlipGridView.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 検索結果を一覧にバインドします。
        /// </summary>
        private void BindGrid()
        {
            ApplyGridHeaderText();

            DateTime? slipDate;
            if (!TryGetSlipDate(out slipDate))
            {
                SlipGridView.DataSource = new List<SlipModel>();
                SlipGridView.DataBind();
                return;
            }

            IList<SlipModel> slips = slipSampleService.Search(
                SlipNoTextBox.Text.Trim(),
                TitleTextBox.Text.Trim(),
                slipDate);

            IList<SlipModel> sortedSlips = ApplySort(slips);
            SlipGridView.DataSource = sortedSlips;
            SlipGridView.DataBind();
        }

        /// <summary>
        /// 伝票日付の入力値を取得します。
        /// </summary>
        /// <param name="slipDate">変換後の伝票日付。</param>
        /// <returns>未入力または日付形式が正しい場合は true。不正な日付の場合は false。</returns>
        private bool TryGetSlipDate(out DateTime? slipDate)
        {
            slipDate = null;

            string input = SlipDateTextBox.Text.Trim();
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }

            DateTime parsedDate;
            if (DateTime.TryParse(input, CultureInfo.CurrentCulture, DateTimeStyles.None, out parsedDate))
            {
                slipDate = parsedDate.Date;
                return true;
            }

            ErrorMessageLabel.Text = "伝票日付は日付形式で入力してください。";
            return false;
        }

        /// <summary>
        /// ViewState のソート条件に従って一覧を並べ替えます。
        /// </summary>
        /// <param name="slips">伝票データ。</param>
        /// <returns>並べ替え後の伝票データ。</returns>
        private IList<SlipModel> ApplySort(IEnumerable<SlipModel> slips)
        {
            string sortExpression = ViewState[SortExpressionViewStateKey] as string;
            SortDirection sortDirection = GetCurrentSortDirection();

            switch (sortExpression)
            {
                case "Title":
                    return sortDirection == SortDirection.Ascending
                        ? slips.OrderBy(slip => slip.Title).ToList()
                        : slips.OrderByDescending(slip => slip.Title).ToList();
                case "SlipDate":
                    return sortDirection == SortDirection.Ascending
                        ? slips.OrderBy(slip => slip.SlipDate).ToList()
                        : slips.OrderByDescending(slip => slip.SlipDate).ToList();
                case "SlipNo":
                default:
                    return sortDirection == SortDirection.Ascending
                        ? slips.OrderBy(slip => slip.SlipNo).ToList()
                        : slips.OrderByDescending(slip => slip.SlipNo).ToList();
            }
        }

        /// <summary>
        /// 現在のソート方向を取得します。
        /// </summary>
        /// <returns>現在のソート方向。</returns>
        private SortDirection GetCurrentSortDirection()
        {
            object value = ViewState[SortDirectionViewStateKey];
            return value == null ? SortDirection.Ascending : (SortDirection)value;
        }

        /// <summary>
        /// GridView のヘッダーにソート状態を表示します。
        /// </summary>
        private void ApplyGridHeaderText()
        {
            SlipGridView.Columns[0].HeaderText = CreateSortableHeaderText("伝票番号 *", "SlipNo");
            SlipGridView.Columns[1].HeaderText = CreateSortableHeaderText("タイトル *", "Title");
            SlipGridView.Columns[2].HeaderText = CreateSortableHeaderText("伝票日付 *", "SlipDate");
            SlipGridView.Columns[3].HeaderText = "備考";
        }

        /// <summary>
        /// ソート可能列のヘッダー文字列を作成します。
        /// </summary>
        /// <param name="headerText">ヘッダー文字列。</param>
        /// <param name="sortExpression">ソート式。</param>
        /// <returns>ソートアイコンを含むヘッダー文字列。</returns>
        private string CreateSortableHeaderText(string headerText, string sortExpression)
        {
            string currentExpression = ViewState[SortExpressionViewStateKey] as string;
            if (!string.Equals(currentExpression, sortExpression, StringComparison.OrdinalIgnoreCase))
            {
                return headerText + " " + SortableIcon;
            }

            return headerText + " " + (GetCurrentSortDirection() == SortDirection.Ascending ? AscendingIcon : DescendingIcon);
        }

        /// <summary>
        /// 指定されたソート式がソート可能かどうかを判定します。
        /// </summary>
        /// <param name="sortExpression">ソート式。</param>
        /// <returns>ソート可能な場合は true。それ以外の場合は false。</returns>
        private static bool IsSortableExpression(string sortExpression)
        {
            return string.Equals(sortExpression, "SlipNo", StringComparison.OrdinalIgnoreCase)
                || string.Equals(sortExpression, "Title", StringComparison.OrdinalIgnoreCase)
                || string.Equals(sortExpression, "SlipDate", StringComparison.OrdinalIgnoreCase);
        }
    }
}
