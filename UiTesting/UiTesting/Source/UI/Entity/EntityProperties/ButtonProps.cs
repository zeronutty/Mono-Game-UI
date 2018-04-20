
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    /// <summary>
    /// Extends from EntityProps, Adding properties generic to the button itself
    /// </summary>
    public class ButtonProps : EntityProps
    {
        #region Properties

        /// <summary>
        /// Default Sprite for the button, use for when not being interacted with.
        /// </summary>
        public Sprite Texture { get; set; }

        /// <summary>
        /// Clicked Sprite, for when the button has been clicked.
        /// </summary>
        public Sprite ClickedTexture { get; set; }

        /// <summary>
        /// Hover Sprite, For when the mouse is hovering over the button.
        /// </summary>
        public Sprite HoverTexture { get; set; }

        /// <summary>
        /// Hover Color, used for when not appling a hover texture, to give the user
        /// some feedback
        /// </summary>
        public Color HoverColor{get; set;}

        /// <summary>
        /// Icon, optional icon that will be displayed in the center of the button
        /// </summary>
        public Icon Icon { get; set; }

        /// <summary>
        /// Text, optional text that will be displayed in the center of the button
        /// </summary>
        public string Text { get; set; }
        #endregion
    }
}
