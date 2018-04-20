
namespace UiTesting.Source
{
    /// <summary>
    /// Extends from PanelProps, Adding properties generic to the TextBox itself
    /// </summary>
    public class TextInputProps : PanelProps
    {
        /// <summary>
        /// UseMultiLine, weather or not to make the textbox a multi line textbox
        /// </summary>
        public bool UseMultiLine { get; set; }
    }
}
