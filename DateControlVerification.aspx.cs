using System;

namespace WebForm
{
    public partial class DateControlVerification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StandardDate.SelectedDate = DateTime.Today;
                CalendarDate.SelectedDate = DateTime.Today;
                DatePickerDate.SelectedDate = DateTime.Today;
                FlatpickrDate.SelectedDate = DateTime.Today;

                ApplyReadOnly();
                ShowPostResult("初期表示");
            }
        }

        protected void CommonDateReadOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ApplyReadOnly();
            ShowPostResult("参照表示切替");
        }

        protected void PostButton_Click(object sender, EventArgs e)
        {
            ApplyReadOnly();
            ShowPostResult("送信");
        }

        private void ApplyReadOnly()
        {
            StandardDate.ReadOnly = CommonDateReadOnlyCheckBox.Checked;
            CalendarDate.ReadOnly = CommonDateReadOnlyCheckBox.Checked;
            DatePickerDate.ReadOnly = CommonDateReadOnlyCheckBox.Checked;
            FlatpickrDate.ReadOnly = CommonDateReadOnlyCheckBox.Checked;
        }

        private void ShowPostResult(string actionName)
        {
            StatusLabel.Text = "参照表示: " + StandardDate.ReadOnly;
            MessageLabel.Text =
                "処理: " + Server.HtmlEncode(actionName) + "<br />" +
                "IsPostBack: " + IsPostBack + "<br />" +
                "標準日付入力: " + Server.HtmlEncode(GetDateDisplayText(StandardDate)) + "<br />" +
                "Calendar: " + Server.HtmlEncode(GetDateDisplayText(CalendarDate)) + "<br />" +
                "datepicker: " + Server.HtmlEncode(GetDateDisplayText(DatePickerDate)) + "<br />" +
                "flatpickr: " + Server.HtmlEncode(GetDateDisplayText(FlatpickrDate));
        }

        private static string GetDateDisplayText(Controls.ICommonDateControl dateControl)
        {
            return dateControl.SelectedDate.HasValue
                ? dateControl.SelectedDate.Value.ToString("yyyy/MM/dd")
                : dateControl.Text;
        }
    }
}
