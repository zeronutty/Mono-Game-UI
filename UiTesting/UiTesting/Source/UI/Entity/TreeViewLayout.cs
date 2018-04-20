
using System.Collections.Generic;

namespace UiTesting.Source
{
    public class TreeViewLayout : Panel
    {
        #region Fields

        #endregion

        #region Properties
        #endregion

        #region Methods

        public TreeViewLayout(PanelProps panelProps) : base(panelProps)
        {
            PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll;
        }
        #endregion
    }
}
