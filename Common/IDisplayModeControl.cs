using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForm.Common
{
    public interface IDisplayModeControl
    {
        bool IsDisplayMode { get; set; }

        string DisplayText { get; }

        void ApplyDisplayMode();
    }
}
