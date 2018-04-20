using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    /// <summary>
    /// Extends from EntityProps, Adding properties generic to the TableElement itself
    /// </summary>
    public class TableElementProps : EntityProps
    {
        /// <summary>
        /// BackGroundTexture, the textrue to be drawn in the back ground of the element
        /// </summary>
        public Sprite BackGroundTexture{ get; set; }
    }
}
