using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Controls
{
    [ToolboxData("<{0}:CommonDate runat=\"server\"></{0}:CommonDate>")]
    public partial class CommonDate : CommonDateControlBase
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
            get { return DateInputGroup; }
        }

        protected override void ConfigureInput()
        {
            base.ConfigureInput();
            DateInput.TextMode = TextBoxMode.Date;
            DateInput.Attributes["lang"] = "ja";
        }

        protected override string FormatInputDate(DateTime date)
        {
            return date.ToString(HtmlDateFormat, CultureInfo.InvariantCulture);
        }
    }
}
