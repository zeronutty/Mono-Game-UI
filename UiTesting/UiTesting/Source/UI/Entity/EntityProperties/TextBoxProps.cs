
namespace UiTesting.Source
{
    /// <summary>
    /// TextBoxProps, the default properties for any textbox
    /// </summary>
    public class TextBoxProps : PanelProps
    {
        /// <summary>
        /// Text, The base text for the textbox, before inout modifys it.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// CaretSprite, the Sprite def for the caret.
        /// </summary>
        public Sprite CaretSprite { get; set; }
    }
}
