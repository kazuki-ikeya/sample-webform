using System.Web.UI.WebControls;

public static class DisplayModeHelper
{
    public static void Apply(
        Label displayLabel,
        WebControl inputControl,
        bool isDisplayMode,
        string displayText)
    {
        displayLabel.Text = displayText;

        displayLabel.Visible = isDisplayMode;
        inputControl.Visible = !isDisplayMode;

        if (isDisplayMode)
        {
            displayLabel.Width = Unit.Percentage(100);
            CssHelper.AddCssClass(displayLabel, "display-label");
        }
        else
        {
            CssHelper.RemoveCssClass(displayLabel, "display-label");
        }
    }
}