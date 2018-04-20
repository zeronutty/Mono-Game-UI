using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    /// <summary>
    /// Extends from EntityProps, Adding properties generic to the Icon itself
    /// </summary>
    public class IconProps : EntityProps
    {
        /// <summary>
        /// IconTexture, is the texture2D for the icon itself to be drawn
        /// </summary>
        public Sprite IconTexture { get; set; }
    }
}
