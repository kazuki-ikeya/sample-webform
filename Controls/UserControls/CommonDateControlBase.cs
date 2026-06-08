using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebForm.Controls
{
    public interface ICommonDateControl
    {
        string Text { get; set; }

        DateTime? SelectedDate { get; set; }

        bool ReadOnly { get; set; }
    }

    public abstract class CommonDateControlBase : UserControl, ICommonDateControl
    {
        protected const string HtmlDateFormat = "yyyy-MM-dd";
        protected const string DisplayDateFormat = "yyyy/MM/dd";

        protected abstract TextBox DateInput { get; }

        protected abstract Label DateDisplayLabel { get; }

        protected abstract WebControl DateInputContainer { get; }

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

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ConfigureInput();
            ApplyReadOnlyState();
            RegisterCommonAssets();
            RegisterPickerAssets();
        }

        protected virtual void ConfigureInput()
        {
            DateInput.TextMode = TextBoxMode.SingleLine;
            DateInput.Attributes["placeholder"] = DisplayDateFormat;
            DateInput.Attributes["autocomplete"] = "off";
            DateInput.Attributes["data-common-date-input"] = "true";
        }

        protected virtual string FormatInputDate(DateTime date)
        {
            return date.ToString(DisplayDateFormat, CultureInfo.InvariantCulture);
        }

        protected virtual void RegisterPickerAssets()
        {
        }

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

        protected void RegisterStartupScript(string key, string script)
        {
            if (Page != null && !Page.ClientScript.IsStartupScriptRegistered(typeof(CommonDateControlBase), key))
            {
                Page.ClientScript.RegisterStartupScript(typeof(CommonDateControlBase), key, script, true);
            }
        }

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

        private void ApplyLabelText()
        {
            DateTime parsedDate;
            DateDisplayLabel.Text = TryParseText(DateInput.Text, out parsedDate)
                ? parsedDate.ToString(DisplayFormat, CultureInfo.CurrentCulture)
                : DateInput.Text;
        }

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

        private void RegisterCommonAssets()
        {
            if (Page == null)
            {
                return;
            }

            RegisterStylesheetBlock();
            RegisterStartupScript("common-date-base-script", GetCommonDateScript());
            RegisterStartupScript(
                "common-date-standard-" + ClientID,
                "CommonDateControl.bindStandard('" + DateInput.ClientID + "');");
        }

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

        private string NormalizeDateText(string text)
        {
            DateTime parsedDate;
            return TryParseText(text, out parsedDate)
                ? FormatInputDate(parsedDate)
                : text;
        }

        protected static bool TryParseText(string text, out DateTime parsedDate)
        {
            return DateTime.TryParseExact(text, DisplayDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate)
                || DateTime.TryParseExact(text, HtmlDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate)
                || DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.None, out parsedDate);
        }

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

        private static bool IsNthMonday(DateTime date, int nth)
        {
            return date.DayOfWeek == DayOfWeek.Monday && ((date.Day - 1) / 7) + 1 == nth;
        }

        private static int GetVernalEquinoxDay(int year)
        {
            return (int)Math.Floor(20.8431 + (0.242194 * (year - 1980)) - Math.Floor((year - 1980) / 4.0));
        }

        private static int GetAutumnalEquinoxDay(int year)
        {
            return (int)Math.Floor(23.2488 + (0.242194 * (year - 1980)) - Math.Floor((year - 1980) / 4.0));
        }

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
