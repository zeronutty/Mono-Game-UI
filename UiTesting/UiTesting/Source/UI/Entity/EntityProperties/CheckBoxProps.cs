using Microsoft.Xna.Framework;

namespace UiTesting.Source
{
    /// <summary>
    /// Extends from ButtonProps, Adding properties generic to the CheckBox itself
    /// </summary>
    public class CheckBoxProps : EntityProps
    {
        /// <summary>
        /// IsChecked, bool from setting default checked state
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Text, The text the goes along with the checkbox
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// CheckBoxSize, Is actual size of the check's, check image
        /// </summary>
        public Vector2 CheckBoxSize { get; set; }
    }
}
