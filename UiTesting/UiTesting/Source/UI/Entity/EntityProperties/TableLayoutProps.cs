
namespace UiTesting.Source
{
    /// <summary>
    /// Extends from EntityProps, Adding properties generic to the Table Layout itself
    /// </summary>
    public class TableLayoutProps : EntityProps
    {
        /// <summary>
        /// Rows, defines how many rows the table consists of
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Columns, defines how many columns the table consists of
        /// </summary>
        public int Columns { get; set; }
    }
}
