using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    public class Icon : Entity
    {
        #region Feilds

        private Sprite sprite;

        #endregion

        public Icon(IconProps iconProps) : base(iconProps)
        {
            IsDraggable = false;
            IsTargetable = false;

            sprite = iconProps.IconTexture;
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw)
        {
            base.DrawEntity(spriteBatch);

            sprite.Draw(spriteBatch, SpriteDrawMode.Stretch, p_DrawArea, OverlayColor);
        }
    }
}
