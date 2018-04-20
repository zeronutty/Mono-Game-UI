using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    /// <summary>
    /// Extends from EntityProps, Adding properties generic to the Drop down menu itself
    /// </summary>
    public class DropDownProps : EntityProps
    {
        /// <summary>
        /// ShowArrow, weather or not to display the dropdown arrow image, on the button.
        /// </summary>
        public bool ShowArrow { get; set; }

        /// <summary>
        /// SelectedOptionImage, the texture that is displayed on the top of the dropdown.
        /// </summary>
        public Sprite SelectedOptionImage { get; set; }

        /// <summary>
        /// DropDownPanelHeight, This int is for setting the height of the dropped down panel.
        /// </summary>
        public int DropDownPanelHeight { get; set; }
    }
}
