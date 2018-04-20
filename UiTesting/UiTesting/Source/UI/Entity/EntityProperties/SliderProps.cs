
namespace UiTesting.Source
{
    /// <summary>
    /// Extends from EntityProps, Adding properties generic to the Slider itself
    /// </summary>
    public class SliderProps : EntityProps
    {
        /// <summary>
        /// Max, 
        /// </summary>
        public uint Max { get; set; }

        /// <summary>
        /// Min,
        /// </summary>
        public uint Min { get; set; }
    }
}
