using System.Web.UI;
using WebForm.Common;

public partial class DisplayTextBox : UserControl, IDisplayModeControl
{
    public bool IsDisplayMode
    {
        get => (bool?)ViewState[nameof(IsDisplayMode)] ?? false;
        set
        {
            ViewState[nameof(IsDisplayMode)] = value;
            ApplyDisplayMode();
        }
    }

    public string Text
    {
        get => txtValue.Text;
        set => txtValue.Text = value;
    }

    public string DisplayText => txtValue.Text;

    public void ApplyDisplayMode()
    {
        DisplayModeHelper.Apply(
            lblDisplay,
            txtValue,
            IsDisplayMode,
            DisplayText);
    }
}