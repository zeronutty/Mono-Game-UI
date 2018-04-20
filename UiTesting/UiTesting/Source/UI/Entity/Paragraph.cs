using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace UiTesting.Source
{
    public enum FontStyle
    {
        Regular,
        Bold,
        Italic
    }

    public enum TextAlginment
    {
        TopLeft,
        Left,
        Right,
        Centered
    }

    public class Paragraph : Entity
    {
        #region Fields
        protected string p_Text = string.Empty;

        protected bool p_WrapWords = true;

        public Color HighlightColor = Color.Transparent;

        public Point HighlightColorPadding = new Point(10, 10);

        public bool HighlightColorUseBoxSize = false;

        Rectangle p_ActualDrawRect = new Rectangle();

        protected string p_ProcessedText;

        protected SpriteFont p_CurrentFont;

        protected float p_ActualScale;

        protected Vector2 p_Position;

        protected Vector2 p_FontOrigin;

        private bool p_BreakWordsIfNeeded = true;

        private bool p_AddHypenWhenBreakWords = false;

        public static float BaseSize = 1f;

        public string[] Lines;
        public int MaxNumberOfCharsPerLine = 0;
        #endregion

        #region Properties
        public virtual string Text
        {
            get { return p_Text; }
            set { if (p_Text != value) { p_Text = value; MarkasRectUpdate(); } }
        }

        public bool WarpsWords
        {
            get { return p_WrapWords; }
            set { p_WrapWords = value; MarkasRectUpdate(); }
        }

        public bool BreakWordsIfNeeded
        {
            get { return p_BreakWordsIfNeeded; }
            set { p_BreakWordsIfNeeded = value; MarkasRectUpdate(); }
        }

        public bool AddHypenWhenBreakWords
        {
            get { return p_AddHypenWhenBreakWords; }
            set { p_AddHypenWhenBreakWords = value; MarkasRectUpdate(); }
        }

        public bool AlignToCenter { set; get; }

        public FontStyle TextStyle { get; set; }

        public TextAlginment TextAlginment { get; set; }
        #endregion

        #region Methods
        public Paragraph(ParagraphProps paragraphProps) : base(paragraphProps)
        {
            Text = paragraphProps.Text != string.Empty ? paragraphProps.Text : string.Empty;
            OverlayColor = paragraphProps.OverlayColor != Color.Transparent ? paragraphProps.OverlayColor : Color.Black;
            p_AddHypenWhenBreakWords = paragraphProps.AddHypen;
            p_BreakWordsIfNeeded = paragraphProps.BreakWords;
            p_WrapWords = paragraphProps.WrapWords;
            HighlightColor = paragraphProps.HighlightColor;

            TextAlginment = paragraphProps.TextAlginment;

            Scale = 1;
            p_IsInteractable = false;
            IsTargetable = false;
        }

        public override Rectangle GetActualDestinationRectangle()
        {
            return p_ActualDrawRect;
        }

        public Vector2 GetCharacterActualSize()
        {
            SpriteFont font = GetCurrentFont();
            float scale = Scale * BaseSize * GlobalScale * LocalScale;
            return font.MeasureString(" ") * scale;
        }

        public static int BreakLine(string text, int pos, int max)
        {
            // Find last whitespace in line
            int i = max - 1;
            while (i >= 0 && !Char.IsWhiteSpace(text[pos + i]))
                i--;
            if (i < 0)
                return max; // No whitespace found; break at maximum length
                            // Find start of whitespace
            while (i >= 0 && Char.IsWhiteSpace(text[pos + i]))
                i--;
            // Return length of text before whitespace
            return i + 1;
        }

        public string WrapText(SpriteFont font, string text, float maxLineWidth, float fontSize)
        {
            int i, next;
            StringBuilder sb = new StringBuilder();

            int maxCharperline = (int)(maxLineWidth / font.MeasureString(" ").X);

            // Lucidity check
            if (maxCharperline < 1)
                return text;

            // Parse each line of text
            for (i = 0; i < text.Length; i = next)
            {
                // Find end of line
                int EndOfLine = text.IndexOf("\r\n", i);

                if (EndOfLine == -1)
                    next = EndOfLine = text.Length;
                else
                    next = EndOfLine + "\r\n".Length;

                // Copy this line of text, breaking into smaller lines as needed
                if (EndOfLine > i)
                {
                    do
                    {
                        int len = EndOfLine - i;

                        if (len > maxCharperline)
                            len = BreakLine(text, i, maxCharperline);

                        sb.Append(text, i, len);
                        sb.Append("\r\n");

                        // Trim whitespace following break
                        i += len;

                        while (i < EndOfLine && Char.IsWhiteSpace(text[i]))
                            i++;

                    } while (EndOfLine > i);
                }
                else sb.Append("\r\n"); // Empty line
            }

            string ReturnString = sb.ToString();

            if (ReturnString.Length > 0)
            {
                ReturnString.TrimEnd('\n');
            }

            Lines = ReturnString.Split('\n');

            return ReturnString; 
        }

        public string GetProcessedText()
        {
            return p_ProcessedText;
        }

        public SpriteFont GetCurrentFont()
        {
            return ContentLoader.GetFontByName("BitStreamRoman");
        }

        public override void UpdateDestinationRects()
        {
            base.UpdateDestinationRects();

            CalcTextActualRectWithWarp();
        }

        public Vector2 GetCharacterActualPosition(int index)
        {
            

            return new Vector2();
        }

        public void CalcTextActualRectWithWarp()
        {
            SpriteFont font = GetCurrentFont();
            if (font != p_CurrentFont)
            {
                MarkasRectUpdate();
                p_CurrentFont = font;
            }

            float actualScale = Scale * BaseSize * GlobalScale * LocalScale;
            if (actualScale != p_ActualScale)
            {
                MarkasRectUpdate();
                p_ActualScale = actualScale;
            }

            MaxNumberOfCharsPerLine = (int)(p_DrawArea.Width / p_CurrentFont.MeasureString(" ").X);

            string newProcessedText = Text;
            if (WarpsWords)
            {
                newProcessedText = WrapText(font, newProcessedText, p_DrawArea.Width, p_ActualScale);
            }

            if (newProcessedText != p_ProcessedText)
            {
                p_ProcessedText = newProcessedText;
                MarkasRectUpdate();
            }

            p_FontOrigin = Vector2.Zero;
            p_Position = new Vector2(p_DrawArea.X, p_DrawArea.Y);
            Vector2 size = font.MeasureString(p_ProcessedText);

            bool alreadyCentered = false;

            //switch(p_Anchor)
            //{
            //    case Anchor.Center:
            //        p_FontOrigin = size / 2;
            //        p_Position += new Vector2(p_DrawArea.Width / 2, p_DrawArea.Height / 2);
            //        alreadyCentered = true;
            //        break;
            //    case Anchor.AutoCenter:
            //    case Anchor.TopCenter:
            //        p_FontOrigin = new Vector2(size.X / 2, 0);
            //        p_Position = new Vector2(p_DrawArea.Width / 2, 0);
            //        alreadyCentered = true;
            //        break;
            //    case Anchor.TopRight:
            //        p_FontOrigin = new Vector2(size.X, 0);
            //        p_Position += new Vector2(p_DrawArea.Width, 0f);
            //        break;
            //    case Anchor.BottomCenter:
            //        p_FontOrigin = new Vector2(size.X / 2, size.Y);
            //        p_Position += new Vector2(p_DrawArea.Width / 2, p_DrawArea.Height);
            //        alreadyCentered = true;
            //        break;
            //    case Anchor.BottomRight:
            //        p_FontOrigin = new Vector2(size.X, size.Y);
            //        p_Position += new Vector2(p_DrawArea.Width, p_DrawArea.Height);
            //        break;
            //    case Anchor.BottomLeft:
            //        p_FontOrigin = new Vector2(0f, size.Y);
            //        p_Position += new Vector2(0f, p_DrawArea.Height);
            //        break;
            //    case Anchor.CenterLeft:
            //        p_FontOrigin = new Vector2(0f, size.Y / 2);
            //        p_Position += new Vector2(0f, p_DrawArea.Height / 2);
            //        break;
            //    case Anchor.CenterRight:
            //        p_FontOrigin = new Vector2(size.X, size.Y / 2);
            //        p_Position += new Vector2(p_DrawArea.Width, p_DrawArea.Height / 2);
            //        break;
            //}

            switch (TextAlginment)
            {
                case TextAlginment.TopLeft:
                    p_FontOrigin = Vector2.Zero;
                    p_Position = new Vector2(p_DrawArea.X, p_DrawArea.Y);
                    break;
                case TextAlginment.Left:
                    p_FontOrigin = new Vector2(0f, size.Y / 2);
                    p_Position += new Vector2(0f, p_DrawArea.Height / 2);
                    break;
                case TextAlginment.Right:
                    p_FontOrigin = new Vector2(size.X, size.Y / 2);
                    p_Position += new Vector2(p_DrawArea.Width, p_DrawArea.Height / 2);
                    break;
                case TextAlginment.Centered:
                    p_FontOrigin = size / 2;
                    p_Position += new Vector2(p_DrawArea.Width / 2, p_DrawArea.Height / 2);
                    break;
            }

            if (AlignToCenter && !alreadyCentered)
            {
                p_FontOrigin.X = size.X / 2;
                p_Position.X = p_DrawArea.X + p_DrawArea.Width / 2;
            }

            p_ActualDrawRect.X = (int)p_Position.X - (int)(p_FontOrigin.X * p_ActualScale);
            p_ActualDrawRect.Y = (int)p_Position.Y - (int)(p_FontOrigin.Y * p_ActualScale);
            p_ActualDrawRect.Width = (int)((size.X) * p_ActualScale);
            p_ActualDrawRect.Height = (int)((size.Y) * p_ActualScale);
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            if (BaseDraw == null)
            {
                if (HighlightColor.A > 0 || State == EntityState.MouseHover)
                {
                    Color backColor = UiManager.GetActiveUserInterface().drawUtils.FixColorOpacity(HighlightColor);
                    var rect = HighlightColorUseBoxSize ? p_DrawArea : p_ActualDrawRect;

                    if (HighlightColorUseBoxSize)
                        rect.Height = (int)(rect.Height / GlobalScale / LocalScale);

                    var Padding = new Point((int)(HighlightColorPadding.X * GlobalScale * LocalScale), (int)(HighlightColorPadding.Y * GlobalScale * LocalScale));
                    rect.Location -= Padding;
                    rect.Size += Padding + Padding;

                    Texture2D New = ContentLoader.GetTextureByName("WhiteTexture");

                    spriteBatch.Draw(New, rect, HighlightColor);
                }

                Color fillColor = OverlayColor;

                Vector2 fontOrigin = new Vector2((int)p_FontOrigin.X, (int)p_FontOrigin.Y);

                spriteBatch.DrawString(p_CurrentFont, p_ProcessedText, p_Position, fillColor, 0, fontOrigin, p_ActualScale, SpriteEffects.None, 0.5f);

                base.DrawEntity(spriteBatch);
            }
            else if (BaseDraw == true)
            {
                base.DrawEntity(spriteBatch);
            }
        }
        #endregion
    }
}
