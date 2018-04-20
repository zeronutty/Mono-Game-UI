using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    /// <summary>
    /// Extends from EntityProps, Adding properties generic to the TopBar itself
    /// </summary>
    public class PanelTopBarProps : EntityProps
    {
        /// <summary>
        /// ExitButton, Optional Exit button for your top bar panel, this button will
        /// close your panel interface.
        /// </summary>
        public Button ExitButton { get; set; }

        /// <summary>
        /// MinimizeButton, Optional Minimize button for your top bar panel, this button
        /// will minimize your panel for you
        /// </summary>
        public Button MinimizeButton { get; set; }

        /// <summary>
        /// MaximizeButton, Optional Maximize button for your top bar panel, this button
        /// will maximize your panel for you
        /// </summary>
        public Button MaximizeButton { get; set; }

        /// <summary>
        /// ScalePlus, Optional Scale Plus button for your top bar panel, this button
        /// will increase the local scale of the the top bar and its child entities
        /// </summary>
        public Button ScalePlus { get; set; }

        /// <summary>
        /// ScaleMinus, Optional Scale Minus button for your top bar panel, this button
        /// will decrease the local scale of the top bar and its child entities
        /// </summary>
        public Button ScaleMinus { get; set; }

        /// <summary>
        /// Header, Optional Text for your top bar panel, this will draw text in the center
        /// of the top bar, in Header style
        /// </summary>
        public Text Header { get; set; }

        /// <summary>
        /// BackGroundTexture, the texture for being display as the back for the top bar panel
        /// </summary>
        public Sprite BackGroundTexture { get; set; }
    }
}
