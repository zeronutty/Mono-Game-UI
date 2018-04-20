using Microsoft.Xna.Framework;

namespace UiTesting.Source
{
    /// <summary>
    /// The base Entity Properties, use for defining aspect of a given entity at the base level
    /// </summary>
    public class EntityProps
    {
        #region Properties

        /// <summary>
        /// Entity Name, used mostly in debugging 
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// The overall size of the entity, 0 On ether x/y will result in filling the parent
        /// where as a value between 0.1 and 1 will result in a precentage of the parent rect 
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// Local Position, relative to the Entity's Parent. Can also be defined as offset from anchor 
        /// </summary>
        public Vector2 LocalPosition { get; set; }

        /// <summary>
        /// Global Position, relative to the entire size width and height
        /// </summary>
        public Vector2 GlobalPosition { get; set; }

        /// <summary>
        /// Padding, How much padding does this entity have, use for setting childern
        /// </summary>
        public Vector2 Padding { get; set; }

        /// <summary>
        /// Margin How much Margin does this entity have, use for lining siblings together.
        /// </summary>
        public Vector2 Margin { get; set; }

        /// <summary>
        /// Entity Anchor, Use for resizing and positioning with inside the entity's parent 
        /// </summary>
        public Anchor EntityAnchor { get; set; }

        /// <summary>
        /// LayoutState, use for auto anchors for quick a lining this object relative to other
        /// auto anchor entities
        /// </summary>
        public LayoutState EntityLayoutState { get; set; }

        /// <summary>
        /// Overlaying color, use for tinting Entity's textures, for quickly changing the color.
        /// </summary>
        public Color OverlayColor { get; set; }

        #endregion
    }
}
