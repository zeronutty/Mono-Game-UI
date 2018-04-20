using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    /// <summary>
    /// Extends from EntityProps, Adding properties generic to the Image itself
    /// </summary>
    public class ImageProps : EntityProps
    {
        /// <summary>
        /// DrawMode, use for setting the drawmode, for drawing a scretched texture or a 9 sliced texture.
        /// </summary>
        public SpriteDrawMode DrawMode { get; set; }

        /// <summary>
        /// texture, the texture for the image to draw
        /// </summary>
        public Sprite texture { get; set; }
    }
}
