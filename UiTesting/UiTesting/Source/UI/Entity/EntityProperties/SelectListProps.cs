using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    /// <summary>
    /// Extends from PanelProps, Adding properties generic to the SelectList itself
    /// </summary>
    public class SelectListProps : PanelProps
    {
        /// <summary>
        /// SelectedOptionTexture, This texture is for the button part of the selectlist
        /// </summary>
        public Sprite SelectedOptionsImageTexture { get; set; }
    }
}
