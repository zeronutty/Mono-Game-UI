using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{

    public class Image :Entity
    {
        SpriteDrawMode SpriteDrawMode;

        public Vector2 FrameWidth = Vector2.One * 0.15f;

        public Sprite Texture;

        public Rectangle? SourceRectangle = null;

        public Image(ImageProps imageProps) : base(imageProps)
        {
            SpriteDrawMode = imageProps.DrawMode;
            Texture = imageProps.texture;
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {               
            Texture.Draw(spriteBatch, SpriteDrawMode, p_DrawArea, OverlayColor); 
            
            base.DrawEntity(spriteBatch);
        }
    }
}
