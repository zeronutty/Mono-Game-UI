using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    /// <summary>
    /// Extends from EntityProps, Adding properties generic to the panel itself
    /// </summary>
    public class PanelProps : EntityProps
    {
        #region Properties

        /// <summary>
        /// BackgroundTexure, the back ground texture for the panel to draw.
        /// </summary>
        public Sprite Backgroundtexture { get; set; }

        #endregion
    }
}
