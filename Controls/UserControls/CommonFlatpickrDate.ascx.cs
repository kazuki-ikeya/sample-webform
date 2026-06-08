using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Controls
{
    [ToolboxData("<{0}:CommonFlatpickrDate runat=\"server\"></{0}:CommonFlatpickrDate>")]
    public partial class CommonFlatpickrDate : CommonDateControlBase
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
