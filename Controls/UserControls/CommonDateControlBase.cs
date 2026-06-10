using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebForm.Controls
{
    /// <summary>
    /// 共通日付入力コントロールが公開する基本操作を定義します。
    /// </summary>
    public interface ICommonDateControl
    {
        /// <summary>
        /// 入力欄に表示する日付文字列を取得または設定します。
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// 入力欄の日付を <see cref="DateTime"/> として取得または設定します。
        /// </summary>
        DateTime? SelectedDate { get; set; }

        /// <summary>
        /// 編集可能な入力欄ではなく参照用ラベルで表示するかどうかを取得または設定します。
        /// </summary>
        bool ReadOnly { get; set; }
    }

    /// <summary>
    /// Web Forms の日付入力ユーザーコントロールに共通する表示、解析、参照表示、休日判定を提供します。
    /// </summary>
    public abstract class CommonDateControlBase : UserControl, ICommonDateControl
    {
        /// <summary>
        /// HTML の日付入力で使用する日付形式を表します。
        /// </summary>
        protected const string HtmlDateFormat = "yyyy-MM-dd";

        /// <summary>
        /// 画面表示で使用する標準の日付形式を表します。
        /// </summary>
        protected const string DisplayDateFormat = "yyyy/MM/dd";

        /// <summary>
        /// 派生コントロールの日付入力欄を取得します。
        /// </summary>
        protected abstract TextBox DateInput { get; }

        /// <summary>
        /// ReadOnly 時に表示する日付ラベルを取得します。
        /// </summary>
        protected abstract Label DateDisplayLabel { get; }

        /// <summary>
        /// 編集可能な日付入力 UI 全体を含むコンテナを取得します。
        /// </summary>
        protected abstract WebControl DateInputContainer { get; }

        /// <summary>
        /// 入力欄に表示する日付文字列を取得または設定します。
        /// </summary>
        public string Text
        {
            get
            {
                return DateInput.Text;
            }
            set
            {
                DateInput.Text = NormalizeDateText(value);
                ApplyLabelText();
            }
        }

        /// <summary>
        /// 入力欄の日付を <see cref="DateTime"/> として取得または設定します。
        /// </summary>
        public DateTime? SelectedDate
        {
            get
            {
                DateTime parsedDate;
                return TryParseText(DateInput.Text, out parsedDate) ? parsedDate.Date : (DateTime?)null;
            }
            set
            {
                DateInput.Text = value.HasValue
                    ? FormatInputDate(value.Value)
                    : string.Empty;
                ApplyLabelText();
            }
        }

        /// <summary>
        /// ReadOnly 表示時に使用する日付書式を取得または設定します。
        /// </summary>
        public string DisplayFormat
        {
            get
            {
                string value = ViewState["DisplayFormat"] as string;
                return string.IsNullOrEmpty(value) ? DisplayDateFormat : value;
            }
            set
            {
                ViewState["DisplayFormat"] = value;
                ApplyLabelText();
            }
        }

        /// <summary>
        /// 編集可能な入力欄ではなく参照用ラベルで表示するかどうかを取得または設定します。
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                object value = ViewState["ReadOnly"];
                return value != null && (bool)value;
            }
            set
            {
                ViewState["ReadOnly"] = value;
                ApplyReadOnlyState();
            }
        }

        /// <summary>
        /// 描画直前に入力欄の属性、ReadOnly 表示、共通アセット、派生コントロール固有アセットを設定します。
        /// </summary>
        /// <param name="e">イベントデータ。</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ConfigureInput();
            ApplyReadOnlyState();
            RegisterCommonAssets();
            RegisterPickerAssets();
        }

        /// <summary>
        /// 日付入力欄に共通の HTML 属性を設定します。
        /// </summary>
        protected virtual void ConfigureInput()
        {
            DateInput.TextMode = TextBoxMode.SingleLine;
            DateInput.Attributes["placeholder"] = DisplayDateFormat;
            DateInput.Attributes["autocomplete"] = "off";
            DateInput.Attributes["data-common-date-input"] = "true";
        }

        /// <summary>
        /// 入力欄へ設定する日付文字列に変換します。
        /// </summary>
        /// <param name="date">変換する日付。</param>
        /// <returns>入力欄に表示する日付文字列。</returns>
        protected virtual string FormatInputDate(DateTime date)
        {
            return date.ToString(DisplayDateFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 派生コントロール固有の日付ピッカー用 CSS や JavaScript を登録します。
        /// </summary>
        protected virtual void RegisterPickerAssets()
        {
        }

        /// <summary>
        /// ページヘッダーへ外部スタイルシートを一度だけ登録します。
        /// </summary>
        /// <param name="key">重複登録を防ぐためのキー。</param>
        /// <param name="href">登録するスタイルシートの URL。</param>
        protected void RegisterStylesheet(string key, string href)
        {
            if (Page == null || Page.Header == null || Page.Items[key] != null)
            {
                return;
            }

            HtmlLink link = new HtmlLink();
            link.ID = key;
            link.Href = href;
            link.Attributes["rel"] = "stylesheet";
            Page.Header.Controls.Add(link);
            Page.Items[key] = true;
        }

        /// <summary>
        /// ページへ起動時 JavaScript を一度だけ登録します。
        /// </summary>
        /// <param name="key">重複登録を防ぐためのキー。</param>
        /// <param name="script">登録する JavaScript。</param>
        protected void RegisterStartupScript(string key, string script)
        {
            if (Page != null && !Page.ClientScript.IsStartupScriptRegistered(typeof(CommonDateControlBase), key))
            {
                Page.ClientScript.RegisterStartupScript(typeof(CommonDateControlBase), key, script, true);
            }
        }

        /// <summary>
        /// 現在の ReadOnly 設定に合わせて入力欄と表示ラベルの表示状態を切り替えます。
        /// </summary>
        private void ApplyReadOnlyState()
        {
            ApplyLabelText();
            ApplyDateKindCss();

            DateInputContainer.Visible = !ReadOnly;
            DateDisplayLabel.Visible = ReadOnly;
            DateDisplayLabel.Style["display"] = "block";
            DateDisplayLabel.Style["width"] = "100%";
            DateDisplayLabel.Style["text-align"] = "center";
        }

        /// <summary>
        /// 入力欄の値を参照表示用ラベルへ反映します。
        /// </summary>
        private void ApplyLabelText()
        {
            DateTime parsedDate;
            DateDisplayLabel.Text = TryParseText(DateInput.Text, out parsedDate)
                ? parsedDate.ToString(DisplayFormat, CultureInfo.CurrentCulture)
                : DateInput.Text;
        }

        /// <summary>
        /// 入力欄の日付が土曜日、日曜日、祝日の場合に対応する CSS クラスを付け替えます。
        /// </summary>
        private void ApplyDateKindCss()
        {
            global::CssHelper.RemoveCssClass(DateInput, "common-date-saturday-input");
            global::CssHelper.RemoveCssClass(DateInput, "common-date-holiday-input");

            DateTime parsedDate;
            if (!TryParseText(DateInput.Text, out parsedDate))
            {
                return;
            }

            if (IsJapaneseHoliday(parsedDate) || parsedDate.DayOfWeek == DayOfWeek.Sunday)
            {
                global::CssHelper.AddCssClass(DateInput, "common-date-holiday-input");
            }
            else if (parsedDate.DayOfWeek == DayOfWeek.Saturday)
            {
                global::CssHelper.AddCssClass(DateInput, "common-date-saturday-input");
            }
        }

        /// <summary>
        /// すべての日付入力コントロールで使用する共通 CSS と JavaScript を登録します。
        /// </summary>
        private void RegisterCommonAssets()
        {
            if (Page == null)
            {
                return;
            }

            RegisterStylesheetBlock();
            //RegisterStartupScript("common-date-base-script", GetCommonDateScript());
            RegisterStartupScript(
                "common-date-standard-" + ClientID,
                "CommonDateControl.bindStandard('" + DateInput.ClientID + "');");
        }

        /// <summary>
        /// 日付入力欄、カレンダー、日付ピッカーで共有するインライン CSS をページヘッダーへ登録します。
        /// </summary>
        private void RegisterStylesheetBlock()
        {
            const string key = "common_date_style";
            if (Page.Header == null || Page.Items[key] != null)
            {
                return;
            }

            HtmlGenericControl style = new HtmlGenericControl("style");
            style.ID = key;
            style.InnerHtml = @"
.common-date-icon { cursor: pointer; user-select: none; }
.common-date-holiday-input { color: #b42318; background-color: #fff1f1; border-color: #f1a7a7; }
.common-date-saturday-input { color: #175cd3; background-color: #eff6ff; border-color: #9ec5fe; }
.ui-datepicker-calendar .common-date-holiday a,
.ui-datepicker-calendar .common-date-holiday span { color: #b42318 !important; background: #fff1f1 !important; }
.ui-datepicker-calendar .common-date-saturday a,
.ui-datepicker-calendar .common-date-saturday span { color: #175cd3 !important; background: #eff6ff !important; }
.flatpickr-day.common-date-holiday { color: #b42318; background: #fff1f1; border-color: #fff1f1; }
.flatpickr-day.common-date-saturday { color: #175cd3; background: #eff6ff; border-color: #eff6ff; }
.common-calendar-date-host { position: relative; }
.common-calendar-date-popup { position: absolute; top: calc(100% + 4px); left: 0; z-index: 1080; min-width: 20rem; max-width: min(22rem, 90vw); background: #fff; border: 1px solid #cbd5e1; border-radius: 0.375rem; padding: 0.5rem; }
.common-calendar-date-popup table { width: 100%; }
";
            Page.Header.Controls.Add(style);
            Page.Items[key] = true;
        }

        /// <summary>
        /// 解析可能な日付文字列を標準表示形式へ正規化します。
        /// </summary>
        /// <param name="text">正規化する日付文字列。</param>
        /// <returns>解析できた場合は標準表示形式の日付文字列。解析できない場合は元の文字列。</returns>
        private string NormalizeDateText(string text)
        {
            DateTime parsedDate;
            return TryParseText(text, out parsedDate)
                ? FormatInputDate(parsedDate)
                : text;
        }

        /// <summary>
        /// 日付文字列を標準表示形式、HTML 日付形式、現在カルチャの順で解析します。
        /// </summary>
        /// <param name="text">解析する日付文字列。</param>
        /// <param name="parsedDate">解析できた日付。解析できない場合は既定値。</param>
        /// <returns>解析に成功した場合は true。それ以外の場合は false。</returns>
        protected static bool TryParseText(string text, out DateTime parsedDate)
        {
            return DateTime.TryParseExact(text, DisplayDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate)
                || DateTime.TryParseExact(text, HtmlDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate)
                || DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.None, out parsedDate);
        }

        /// <summary>
        /// 指定した日付が日本の祝日または振替休日・国民の休日に該当するかどうかを判定します。
        /// </summary>
        /// <param name="date">判定する日付。</param>
        /// <returns>日本の祝日または休日に該当する場合は true。それ以外の場合は false。</returns>
        protected static bool IsJapaneseHoliday(DateTime date)
        {
            if (IsJapaneseBaseHoliday(date))
            {
                return true;
            }

            if (IsJapaneseBaseHoliday(date.AddDays(-1)) && IsJapaneseBaseHoliday(date.AddDays(1)))
            {
                return true;
            }

            DateTime cursor = date.AddDays(-1);
            while (IsJapaneseBaseHoliday(cursor))
            {
                if (cursor.DayOfWeek == DayOfWeek.Sunday)
                {
                    return true;
                }

                cursor = cursor.AddDays(-1);
            }

            return false;
        }

        /// <summary>
        /// 指定した日付が振替休日や国民の休日を除く日本の祝日に該当するかどうかを判定します。
        /// </summary>
        /// <param name="date">判定する日付。</param>
        /// <returns>祝日に該当する場合は true。それ以外の場合は false。</returns>
        private static bool IsJapaneseBaseHoliday(DateTime date)
        {
            switch (date.Month)
            {
                case 1:
                    return date.Day == 1 || IsNthMonday(date, 2);
                case 2:
                    return date.Day == 11 || date.Day == 23;
                case 3:
                    return date.Day == GetVernalEquinoxDay(date.Year);
                case 4:
                    return date.Day == 29;
                case 5:
                    return date.Day >= 3 && date.Day <= 5;
                case 7:
                    return IsNthMonday(date, 3);
                case 8:
                    return date.Day == 11;
                case 9:
                    return IsNthMonday(date, 3) || date.Day == GetAutumnalEquinoxDay(date.Year);
                case 10:
                    return IsNthMonday(date, 2);
                case 11:
                    return date.Day == 3 || date.Day == 23;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 指定した日付がその月の第 n 月曜日に該当するかどうかを判定します。
        /// </summary>
        /// <param name="date">判定する日付。</param>
        /// <param name="nth">月内で何回目の月曜日かを表す値。</param>
        /// <returns>指定した第 n 月曜日に該当する場合は true。それ以外の場合は false。</returns>
        private static bool IsNthMonday(DateTime date, int nth)
        {
            return date.DayOfWeek == DayOfWeek.Monday && ((date.Day - 1) / 7) + 1 == nth;
        }

        /// <summary>
        /// 指定した年の春分の日の日付を計算します。
        /// </summary>
        /// <param name="year">計算対象の年。</param>
        /// <returns>春分の日の日。</returns>
        private static int GetVernalEquinoxDay(int year)
        {
            return (int)Math.Floor(20.8431 + (0.242194 * (year - 1980)) - Math.Floor((year - 1980) / 4.0));
        }

        /// <summary>
        /// 指定した年の秋分の日の日付を計算します。
        /// </summary>
        /// <param name="year">計算対象の年。</param>
        /// <returns>秋分の日の日。</returns>
        private static int GetAutumnalEquinoxDay(int year)
        {
            return (int)Math.Floor(23.2488 + (0.242194 * (year - 1980)) - Math.Floor((year - 1980) / 4.0));
        }

        /// <summary>
        /// 日付入力欄の色分けとカレンダーアイコン操作に使用する共通 JavaScript を取得します。
        /// </summary>
        /// <returns>ページへ登録する JavaScript 文字列。</returns>
        private static string GetCommonDateScript()
        {
            return @"
window.CommonDateControl = window.CommonDateControl || (function () {
    function pad(value) { return value < 10 ? '0' + value : String(value); }
    function key(date) { return date.getFullYear() + '/' + pad(date.getMonth() + 1) + '/' + pad(date.getDate()); }
    function parse(value) {
        var match = /^(\d{4})[-\/](\d{2})[-\/](\d{2})$/.exec(value || '');
        return match ? new Date(Number(match[1]), Number(match[2]) - 1, Number(match[3])) : null;
    }
    function nthMonday(date, nth) { return date.getDay() === 1 && Math.floor((date.getDate() - 1) / 7) + 1 === nth; }
    function vernal(year) { return Math.floor(20.8431 + 0.242194 * (year - 1980) - Math.floor((year - 1980) / 4)); }
    function autumnal(year) { return Math.floor(23.2488 + 0.242194 * (year - 1980) - Math.floor((year - 1980) / 4)); }
    function baseHoliday(date) {
        var month = date.getMonth() + 1;
        var day = date.getDate();
        if (month === 1 && day === 1) return '元日';
        if (month === 1 && nthMonday(date, 2)) return '成人の日';
        if (month === 2 && day === 11) return '建国記念の日';
        if (month === 2 && day === 23) return '天皇誕生日';
        if (month === 3 && day === vernal(date.getFullYear())) return '春分の日';
        if (month === 4 && day === 29) return '昭和の日';
        if (month === 5 && day === 3) return '憲法記念日';
        if (month === 5 && day === 4) return 'みどりの日';
        if (month === 5 && day === 5) return 'こどもの日';
        if (month === 7 && nthMonday(date, 3)) return '海の日';
        if (month === 8 && day === 11) return '山の日';
        if (month === 9 && nthMonday(date, 3)) return '敬老の日';
        if (month === 9 && day === autumnal(date.getFullYear())) return '秋分の日';
        if (month === 10 && nthMonday(date, 2)) return 'スポーツの日';
        if (month === 11 && day === 3) return '文化の日';
        if (month === 11 && day === 23) return '勤労感謝の日';
        return '';
    }
    function addDays(date, days) {
        var result = new Date(date.getFullYear(), date.getMonth(), date.getDate());
        result.setDate(result.getDate() + days);
        return result;
    }
    function holidayName(date) {
        var name = baseHoliday(date);
        if (name) return name;
        if (baseHoliday(addDays(date, -1)) && baseHoliday(addDays(date, 1))) return '国民の休日';
        var cursor = addDays(date, -1);
        while (baseHoliday(cursor)) {
            if (cursor.getDay() === 0) return '振替休日';
            cursor = addDays(cursor, -1);
        }
        return '';
    }
    function applyInputColor(input) {
        var date = parse(input.value);
        input.classList.remove('common-date-holiday-input', 'common-date-saturday-input');
        if (!date) return;
        if (holidayName(date) || date.getDay() === 0) input.classList.add('common-date-holiday-input');
        else if (date.getDay() === 6) input.classList.add('common-date-saturday-input');
    }
    return {
        holidayName: holidayName,
        dateKey: key,
        applyInputColor: applyInputColor,
        bindStandard: function (id) {
            var input = document.getElementById(id);
            if (!input) return;
            applyInputColor(input);
            input.addEventListener('change', function () { applyInputColor(input); });
            var icon = input.closest('.input-group') && input.closest('.input-group').querySelector('.common-date-icon');
            if (icon) icon.addEventListener('click', function () {
                if (input.showPicker) input.showPicker();
                else input.focus();
            });
        }
    };
}());
";
        }
    }
}
