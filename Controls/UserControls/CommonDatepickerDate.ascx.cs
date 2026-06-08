using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Controls
{
    [ToolboxData("<{0}:CommonDatepickerDate runat=\"server\"></{0}:CommonDatepickerDate>")]
    public partial class CommonDatepickerDate : CommonDateControlBase
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
    }
}
