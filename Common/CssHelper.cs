using System;
using System.Linq;
using System.Web.UI.WebControls;

public static class CssHelper
{
    public static void AddCssClass(WebControl control, string cssClass)
    {
        if (string.IsNullOrWhiteSpace(cssClass))
        {
            return;
        }

        var classes = control.CssClass
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        if (!classes.Contains(cssClass))
        {
            classes.Add(cssClass);
        }

        control.CssClass = string.Join(" ", classes);
    }

    public static void RemoveCssClass(WebControl control, string cssClass)
    {
        if (string.IsNullOrWhiteSpace(cssClass))
        {
            return;
        }

        var classes = control.CssClass
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(c => c != cssClass);

        control.CssClass = string.Join(" ", classes);
    }
}