using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    /// <summary>
    /// Extends from SliderProps, Adding properties generic to the ProgressBar itself
    /// </summary>
    public class ProgressBarProps : SliderProps
    {
        /// <summary>
        /// BarTexture, The texture of the background of the progress bar.
        /// </summary>
        public Sprite BarTexture { get; set; }

        /// <summary>
        /// FillTexture, The texture of which is use to fill the bar at a precentage
        /// </summary>
        public Sprite FillTexture { get; set; }

        /// <summary>
        /// CaptionText, Optional Text to be deisplayed in the center of the progress bar (displayng precentage).
        /// </summary>
        public string CaptionText { get; set; }
    }
}
