using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Controls
{
    [ToolboxData("<{0}:CommonCalendarDate runat=\"server\"></{0}:CommonCalendarDate>")]
    public partial class CommonCalendarDate : CommonDateControlBase
    {
        protected override TextBox DateInput
        {
            get { return DateTextBox; }
        }

        protected override Label DateDisplayLabel
        {
            get { return DateLabel; }
        }

        protected override WebControl DateInputContainer
        {
            get { return DateInputPanel; }
        }

        protected override void ConfigureInput()
        {
            base.ConfigureInput();
            DateInput.Attributes["lang"] = "ja";

            if (SelectedDate.HasValue)
            {
                DateCalendar.SelectedDate = SelectedDate.Value;

                if (DateCalendar.VisibleDate == DateTime.MinValue)
                {
                    DateCalendar.VisibleDate = SelectedDate.Value;
                }
            }
        }

        protected void CalendarToggleButton_Click(object sender, EventArgs e)
        {
            SyncTextToCalendar();
            CalendarPanel.Visible = !CalendarPanel.Visible;
        }

        protected void DateCalendar_SelectionChanged(object sender, EventArgs e)
        {
            SelectedDate = DateCalendar.SelectedDate;
            CalendarPanel.Visible = false;
        }

        protected void DateCalendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            CalendarPanel.Visible = true;
        }

        protected void DateCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsOtherMonth)
            {
                return;
            }

            if (IsJapaneseHoliday(e.Day.Date) || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                e.Cell.ForeColor = Color.FromArgb(180, 35, 24);
                e.Cell.BackColor = Color.FromArgb(255, 241, 241);
                e.Cell.ToolTip = IsJapaneseHoliday(e.Day.Date) ? "祝日" : "日曜日";
            }
            else if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday)
            {
                e.Cell.ForeColor = Color.FromArgb(23, 92, 211);
                e.Cell.BackColor = Color.FromArgb(239, 246, 255);
                e.Cell.ToolTip = "土曜日";
            }
        }

        private void SyncTextToCalendar()
        {
            DateTime parsedDate;
            if (TryParseText(DateInput.Text, out parsedDate))
            {
                DateCalendar.SelectedDate = parsedDate.Date;
                DateCalendar.VisibleDate = parsedDate.Date;
            }
            else if (DateCalendar.VisibleDate == DateTime.MinValue)
            {
                DateCalendar.VisibleDate = DateTime.Today;
            }
        }
    }
}
