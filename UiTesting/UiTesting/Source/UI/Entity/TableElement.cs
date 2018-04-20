using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    public class TableElement : Entity
    {
        private Sprite p_BackGroundTexture;
        private Text DebugText;

        public TableElement(TableElementProps tableElementProps) : base(tableElementProps)
        {
            p_BackGroundTexture = tableElementProps.BackGroundTexture;

            DebugText = new Text(new TextProps
            {
                //Default Entity
                EntityName = "Text",
                Size = new Vector2(1, 1),
                LocalPosition = new Vector2(0, 0),
                EntityAnchor = Anchor.Center,
                EntityLayoutState = LayoutState.None,
                OverlayColor = Color.White,

                //Default Text
                Text = "X: " + p_TargetArea.X.ToString() + " Y: " + p_TargetArea.Y.ToString() + " W: " + p_TargetArea.Width.ToString() + " H: " + p_TargetArea.Height.ToString(),
                TextColor = Color.Black,
                Draggable = false,
            });

            AddChild(DebugText);
        }

        public override void UpdateDestinationRects()
        {
            CalculateDragAreas(p_DrawArea);
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            p_BackGroundTexture.Draw(spriteBatch, SpriteDrawMode.Panel, p_DrawArea, OverlayColor);

            base.DrawEntity(spriteBatch);
        }
    }
}
