using Microsoft.Xna.Framework;

namespace UiTesting.Source
{
    /// <summary>
    /// Extends from EntityProps, Adding properties generic to the Text itself
    /// </summary>
    public class TextProps : EntityProps
    {
        #region Properties

        /// <summary>
        /// Text, the actual string value of the text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// TextColor, The color inwhich to draw the text 
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// Draggble, weather or not this text entity is draggable
        /// </summary>
        public bool Draggable { get; set; }

        #endregion
    }
}
