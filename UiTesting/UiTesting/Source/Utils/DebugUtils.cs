using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    public enum DebugDrawMode
    {
        Rectangle,
        Line,
        Text,
        Point,
        None
    }

    public class DebugUtils
    {
        private Dictionary<string, DebugDraw> p_DrawDebugs = new Dictionary<string, DebugDraw>();
        private List<Entity> EntitiesToDebug = new List<Entity>();

        private RenderTarget2D RenderTarget2D;

        private Rectangle DrawRectangle = new Rectangle(0,0,0,0);
        private Rectangle OrginRect;

        public DebugUtils()
        {

        }

        public void BeforeDrawDebugs(SpriteBatch spriteBatch)
        {
            int ScreenWidth = UiManager.GetActiveUserInterface().ScreenWidth;
            int ScreenHeight = UiManager.GetActiveUserInterface().ScreenHeight;

            DrawRectangle = new Rectangle(0, 0, ScreenWidth, ScreenHeight);

            DisposeRenderTarget();

            RenderTarget2D = new RenderTarget2D(spriteBatch.GraphicsDevice, ScreenWidth, ScreenHeight);

            spriteBatch.GraphicsDevice.SetRenderTarget(RenderTarget2D);
            spriteBatch.GraphicsDevice.Clear(Color.Transparent);

            UiManager.GetActiveUserInterface().drawUtils.PushRenderTarget(RenderTarget2D);

            OrginRect = DrawRectangle;
            DrawRectangle.X = 0;
            DrawRectangle.Y = 0;
        }

        public void DrawDebugs(SpriteBatch spriteBatch)
        {
            foreach(KeyValuePair<string, DebugDraw> e in p_DrawDebugs)
            {
                switch (e.Value.DrawMode)
                {
                    case DebugDrawMode.Rectangle:
                        spriteBatch.DrawRectangle(e.Value.DrawRect, e.Value.Color, 1f);
                        break;
                    case DebugDrawMode.Line:
                        spriteBatch.DrawLine(e.Value.DrawLine.StartPoint, e.Value.DrawLine.EndPoint, e.Value.Color, 1f);
                        break;
                    case DebugDrawMode.Text:
                        spriteBatch.DrawString(ContentLoader.GetFontByName("NullFont"), e.Value.DrawString, e.Value.DistinationRect, e.Value.Color);
                        break;
                    case DebugDrawMode.Point:
                        var pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                        pixel.SetData(new[] { Color.White });
                        spriteBatch.Draw(pixel, e.Value.DrawPoint.ToVector2(), e.Value.Color);
                        break;
                    case DebugDrawMode.None:
                        break;
                }
            }

            foreach(Entity e in EntitiesToDebug)
            {
                spriteBatch.DrawRectangle(e.p_DrawArea, Color.Black, 1f);
            }
        }

        public void AfterDrawDebugs(SpriteBatch spriteBatch)
        {
            UiManager.GetActiveUserInterface().drawUtils.PopRenderTargets();

            UiManager.GetActiveUserInterface().drawUtils.StartDraw(spriteBatch, false);
            spriteBatch.Draw(RenderTarget2D, DrawRectangle, Color.White);
            UiManager.GetActiveUserInterface().drawUtils.EndDraw(spriteBatch);
        }

        public void AddDrawDebug(string _name = "none", DebugDrawMode _drawMode = DebugDrawMode.None, Vector2? _distRect = null, Rectangle? _drawRect = null, Point? _drawPoint = null, Line _drawLine = null, string _drawText = null, Color? color = null)
        {
            DebugDraw debugDraw = new DebugDraw
            {
                Name = _name,
                DrawMode = _drawMode,
                DistinationRect = _distRect ?? Vector2.Zero,
                DrawRect = _drawRect ?? Rectangle.Empty,
                DrawPoint = _drawPoint ?? Point.Zero,
                DrawString = _drawText,
                Color = color ?? Color.White        
            };

            if(!p_DrawDebugs.ContainsKey(debugDraw.Name))
                p_DrawDebugs.Add(debugDraw.Name, debugDraw);
        }

        public void AddEntityToDebug(ref Entity debug)
        {
            if(!EntitiesToDebug.Contains(debug))
            {
                EntitiesToDebug.Add(debug);
            }
        }

        private void DisposeRenderTarget()
        {
            if (RenderTarget2D != null)
            {
                RenderTarget2D.Dispose();
                RenderTarget2D = null;
            }
        }

    }

    public class DebugDraw
    {
        public string Name;
        public DebugDrawMode DrawMode;
        public Vector2 DistinationRect;
        public Rectangle DrawRect;
        public Point DrawPoint;
        public Line DrawLine;
        public string DrawString;
        public Color Color;
    }

    public class Line
    {
        public Vector2 StartPoint;
        public Vector2 EndPoint;
    }
}
