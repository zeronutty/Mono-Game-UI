using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    public enum SpriteDrawMode
    {
        Stretch,
        Panel,
    }

    public class Sprite
    {
        #region Fields
        #endregion

        #region Properties

        public Texture2D Texture { get; set; }
        public Rectangle Bounds { get { return Texture.Bounds; } }
        
        #endregion

        #region Methods

        public Sprite()
        {

        }

        public Sprite(Texture2D texture) : base()
        {
            Texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteDrawMode drawMode, Rectangle destRect, Color color)
        {
            switch (drawMode)
            {
                case SpriteDrawMode.Stretch:
                    spriteBatch.Draw(Texture, destRect, Texture.Bounds, color);
                    break;
                case SpriteDrawMode.Panel:
                    spriteBatch.DrawSliced(Texture, destRect, Texture.Bounds, color);
                    break;
            }
        }
        #endregion
    }
}
