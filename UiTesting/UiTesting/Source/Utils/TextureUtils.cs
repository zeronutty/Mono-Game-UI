using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    public static class TextureUtils
    {
        public static Texture2D pixel;

        private static void CreatePixel(SpriteBatch spriteBatch)
        {
            pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float thickness)
        {
            DrawLine(spriteBatch, new Vector2(rect.X, rect.Y), new Vector2(rect.Right, rect.Y), color, thickness); // top
            DrawLine(spriteBatch, new Vector2(rect.X + 1f, rect.Y), new Vector2(rect.X + 1f, rect.Bottom + thickness), color, thickness); // left
            DrawLine(spriteBatch, new Vector2(rect.X, rect.Bottom), new Vector2(rect.Right, rect.Bottom), color, thickness); // bottom
            DrawLine(spriteBatch, new Vector2(rect.Right + 1f, rect.Y), new Vector2(rect.Right + 1f, rect.Bottom + thickness), color, thickness); // right
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness)
        {
            // calculate the distance between the two vectors
            float distance = Vector2.Distance(point1, point2);

            // calculate the angle between the two vectors
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

            DrawLine(spriteBatch, point1, distance, angle, color, thickness);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness)
        {
            if (pixel == null)
            {
                CreatePixel(spriteBatch);
            }

            // stretch the pixel between the two vectors
            spriteBatch.Draw(pixel,
                             point,
                             null,
                             color,
                             angle,
                             Vector2.Zero,
                             new Vector2(length, thickness),
                             SpriteEffects.None,
                             0);
        }

        public static void DrawSliced(this SpriteBatch spriteBatch, Texture2D texture, Rectangle DestRect, Rectangle ScoureRect, Color color)
        {
            ScoureRect = ScoureRect != Rectangle.Empty ? ScoureRect : texture.Bounds;

            Rectangle[] ScoutRects = CreatePatches(ScoureRect, 5, 5, 5, 5);
            Rectangle[] DistRects = CreatePatches(DestRect, 5, 5, 5, 5);

            for (int i = 0; i < ScoutRects.Length; i++)
            {
                spriteBatch.Draw(texture, DistRects[i], ScoutRects[i], color);
            }
        }

        public static Rectangle[] CreatePatches(Rectangle rectangle, int LeftPadding, int RightPadding, int TopPadding, int BottomPadding)
        {
            var x = rectangle.X;
            var y = rectangle.Y;
            var w = rectangle.Width;
            var h = rectangle.Height;
            var middleWidth = w - LeftPadding - RightPadding;
            var middleHeight = h - TopPadding - BottomPadding;
            var bottomY = y + h - BottomPadding;
            var rightX = x + w - RightPadding;
            var leftX = x + LeftPadding;
            var topY = y + TopPadding;
            var patches = new[]
            {
                new Rectangle(x,      y,        LeftPadding,  TopPadding),      // top left
                new Rectangle(leftX,  y,        middleWidth,  TopPadding),      // top middle
                new Rectangle(rightX, y,        RightPadding, TopPadding),      // top right
                new Rectangle(x,      topY,     LeftPadding,  middleHeight),    // left middle
                new Rectangle(leftX,  topY,     middleWidth,  middleHeight),    // middle
                new Rectangle(rightX, topY,     RightPadding, middleHeight),    // right middle
                new Rectangle(x,      bottomY,  LeftPadding,  BottomPadding),   // bottom left
                new Rectangle(leftX,  bottomY,  middleWidth,  BottomPadding),   // bottom middle
                new Rectangle(rightX, bottomY,  RightPadding, BottomPadding)    // bottom right
            };
            return patches;
        }
    }
}
