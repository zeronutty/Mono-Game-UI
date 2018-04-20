
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    /// <summary>
    /// Extends from EntityProps, Adding properties generic to the Paragraph itself
    /// </summary>
    public class ParagraphProps : EntityProps
    {
        /// <summary>
        /// Text, This is the actual text to be drawn
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// WrapWord, Weather or not to wrap words to a new line
        /// </summary>
        public bool WrapWords { get; set; }

        /// <summary>
        /// Font, The font to use while drawing this text
        /// </summary>
        public SpriteFont Font { get; set; }

        /// <summary>
        /// HighlightColor, the color inwhich to highlight the background of the text
        /// </summary>
        public Color HighlightColor { get; set; }

        /// <summary>
        /// BreakWords, weather of not to break words to make them fit when wraping words
        /// </summary>
        public bool BreakWords { get; set; }

        /// <summary>
        /// AddHypen, weather of not to add a - to a broken word due to wraping.
        /// </summary>
        public bool AddHypen { get; set; }

        /// <summary>
        /// TextAlgiment, the algimentmode of the text.
        /// </summary>
        public TextAlginment TextAlginment { get; set; }
    }
}
