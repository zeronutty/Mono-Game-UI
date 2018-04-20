using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    public class Text : Entity
    {
        #region Fields
        protected string p_Text = string.Empty;

        private Rectangle p_actualDestRect;

        public Color BackgroundColor = Color.Transparent;

        protected SpriteFont p_CurrentFont;

        protected Vector2 p_Position;

        protected Vector2 p_FontOrigin;

        private bool p_WrapText = true;

        protected string p_ProcessedText;

        private bool AlignToCenter = false;
        #endregion

        #region Properties
        public string text { get { return p_Text; } set { p_Text = value; MarkasRectUpdate(); } }

        public bool WrapText { get { return p_WrapText; } set { p_WrapText = value; MarkasRectUpdate(); } }

        #endregion

        #region Methods

        public Text(TextProps textProps) : base(textProps)
        {
            text = textProps.Text;
            BackgroundColor = textProps.TextColor;
            IsDraggable = textProps.Draggable;
        }

        public void CalculateTextActualRectangleWithWrap()
        {
            SpriteFont font = GetCurrentFont();
            if(font != p_CurrentFont)
            {
                MarkasRectUpdate();
                p_CurrentFont = font;
            }

            float actualScale = GlobalScale;

            string newProcessedText = p_Text;
            if(newProcessedText != p_ProcessedText)
            {
                p_ProcessedText = newProcessedText;
                MarkasRectUpdate();
            }

            if(p_WrapText)
            {
                newProcessedText = newProcessedText.TrimEnd(' ');
            }

            p_FontOrigin = Vector2.Zero;
            p_Position = new Vector2(p_DrawArea.X, p_DrawArea.Y);
            Vector2 size = font.MeasureString(p_ProcessedText);

            bool alreadyCentered = false;
            switch(p_Anchor)
            {
                case Anchor.Center:
                    p_FontOrigin = size / 2;
                    p_Position += new Vector2(p_DrawArea.Width / 2, p_DrawArea.Height / 2);
                    alreadyCentered = true;
                    break;
                case Anchor.AutoCenter:
                case Anchor.TopCenter:
                    p_FontOrigin = new Vector2(size.X / 2, 0);
                    p_Position += new Vector2(p_DrawArea.Width / 2, 0f);
                    alreadyCentered = true;
                    break;
                case Anchor.TopRight:
                    p_FontOrigin = new Vector2(size.X, 0);
                    p_Position += new Vector2(p_DrawArea.Width, 0f);
                    break;
                case Anchor.BottomCenter:
                    p_FontOrigin = new Vector2(size.X / 2, size.Y);
                    p_Position += new Vector2(p_DrawArea.Width / 2, p_DrawArea.Height);
                    alreadyCentered = true;
                    break;
                case Anchor.BottomRight:
                    p_FontOrigin = new Vector2(size.X, size.Y);
                    p_Position += new Vector2(p_DrawArea.Width, p_DrawArea.Height);
                    break;
                case Anchor.BottomLeft:
                    p_FontOrigin = new Vector2(0f, size.Y);
                    p_Position += new Vector2(0f, p_DrawArea.Height);
                    break;
                case Anchor.CenterLeft:
                    p_FontOrigin = new Vector2(0f, size.Y / 2);
                    p_Position += new Vector2(0f, p_DrawArea.Height / 2);
                    break;
                case Anchor.CenterRight:
                    p_FontOrigin = new Vector2(size.X, size.Y / 2);
                    p_Position += new Vector2(p_DrawArea.Width, p_DrawArea.Height / 2);
                    break;
            }

            if (AlignToCenter && !alreadyCentered)
            {
                p_FontOrigin.X = size.X / 2;
                p_Position.X = p_DrawArea.X + p_DrawArea.Width / 2;
            }

            p_actualDestRect.X = (int)p_Position.X - (int)(p_FontOrigin.X * actualScale);
            p_actualDestRect.Y = (int)p_Position.Y - (int)(p_FontOrigin.Y * actualScale);
            p_actualDestRect.Width = (int)((size.X) * actualScale);
            p_actualDestRect.Height = (int)((size.Y) * actualScale);
        }

        public override void UpdateDestinationRects()
        {
            base.UpdateDestinationRects();

            CalculateTextActualRectangleWithWrap();

            p_DrawArea = GetActualDestinationRectangle();
            p_DraggableArea = p_DrawArea;         
        }

        override public Rectangle GetActualDestinationRectangle()
        {
            return p_actualDestRect;
        }

        protected SpriteFont GetCurrentFont()
        {
            return ContentLoader.GetFontByName("BitStreamRoman");
        }

        public Vector2 GetCharacterActualSize()
        {
            SpriteFont font = p_CurrentFont;
            float scale = 6 * GlobalScale * LocalScale;
            return font.MeasureString(" ") * scale;
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            if(BackgroundColor.A > 0)
            {
                //Draw BackGround
            }

            spriteBatch.DrawString(p_CurrentFont, p_ProcessedText, p_Position, Color.Black, 0, p_FontOrigin, GlobalScale * LocalScale, SpriteEffects.None, 0.5f);

            base.DrawEntity(spriteBatch);
        }
        #endregion
    }
}
