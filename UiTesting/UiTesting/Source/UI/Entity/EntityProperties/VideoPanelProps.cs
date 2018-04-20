using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace UiTesting.Source
{
    /// <summary>
    /// Extends from EntityProps, Adding properties generic to the Video Panel itself
    /// </summary>
    public class VideoPanelProps : EntityProps
    {
        /// <summary>
        /// Video, the acutal loaded video file in which to play
        /// </summary>
        public Video Video { get; set; }

        /// <summary>
        /// Empty, the texture in which to display the vidoe onto
        /// </summary>
        public Sprite empty { get; set; }
    }
}
