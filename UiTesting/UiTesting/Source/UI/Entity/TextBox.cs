using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UiTesting.Source.Input;

namespace UiTesting.Source
{
    #region Enums

    public enum TextBoxMode
    {
        Normal,
        Password,
        Multiline
    }

    #endregion

    public class TextBox : Panel
    {
        #region Structs

        private struct Selection
        {
            private int start;
            private int end;

            public int Start
            {
                get
                {
                    if (start > end && start != -1 && end != 1) return end;
                    else return start;
                }
                set
                {
                    start = value;
                }
            }

            public int End
            {
                get
                {
                    if (end < start && start != -1 && end != -1) return start;
                    else return end;
                }
                set
                {
                    end = value;
                }
            }

            public bool IsEmpty
            {
                get { return Start == -1 && End == -1; }
            }

            public int Length
            {
                get { return IsEmpty ? 0 : (End - Start); }
            }

            public Selection(int start, int end)
            {
                this.start = start;
                this.end = end;
            }

            public void Clear()
            {
                Start = -1;
                End = -1;
            }
        }

        #endregion

        #region Fields

        private bool p_ShowCursor = false;
        private double p_FlashTime = 0;
        private int p_CaretPosX;
        private int p_CaretPosY;
        private char p_PasswordChar = '*';
        private TextBoxMode p_mode = TextBoxMode.Normal;
        private string p_shownText = "";
        private bool p_ReadOnly = false;
        private bool p_DrawBorders = true;
        private Selection p_selection = new Selection(-1, -1);
        private bool p_CaretVisible = true;
        private List<string> p_Lines = new List<string>();
        private int p_LinesDrawn = 0;
        private int p_CharsDrawn = 0;
        private SpriteFont p_Font = null;
        private bool p_WordWrap = false;
        private string p_Separtor = "\n";
        private string p_Text = string.Empty;
        private string p_Buffer = string.Empty;
        private bool p_AutoSelection = true;
        private string p_Placeholder = string.Empty;
        private Color p_PlaceholderColor = Color.LightGray;

        #endregion

        #region Properties

        public string PlaceHolder
        {
            get { return p_Placeholder; }
            set { p_Placeholder = value; }
        }

        public Color PlaceHolderColor
        {
            get { return p_PlaceholderColor; }
            set { p_PlaceholderColor = value; }
        }

        private int CaretPosX
        {
            get
            {
                return p_CaretPosX;
            }
            set
            {
                p_CaretPosX = value;

                if (p_CaretPosX < 0) p_CaretPosX = 0;
                if (p_CaretPosX > Lines[CaretPosY].Length) p_CaretPosX = Lines[CaretPosY].Length;
            }
        }

        private int CaretPosY
        {
            get
            {
                return p_CaretPosY;
            }
            set
            {
                p_CaretPosY = value;

                if (p_CaretPosY < 0) p_CaretPosY = 0;
                if (p_CaretPosY > Lines.Count - 1) p_CaretPosY = Lines.Count - 1;
            }
        }

        private int Pos
        {
            get { return GetPos(CaretPosX, CaretPosY); }
            set
            {
                CaretPosY = GetPosY(value);
                CaretPosX = GetPosX(value);
            }
        }

        public virtual bool WordWrap
        {
            get { return p_WordWrap; }
            set
            {
                p_WordWrap = value;
            }
        }

        public virtual char PasswordChar
        {
            get { return p_PasswordChar; }
            set { p_PasswordChar = value; }
        }

        public virtual bool CaretVisible
        {
            get { return p_CaretVisible; }
            set { p_CaretVisible = value; }
        }

        public virtual TextBoxMode Mode
        {
            get { return p_mode; }
            set
            {
                if(value != TextBoxMode.Multiline)
                {
                    Text = Text.Replace(p_Separtor, "");
                }
                p_mode = value;
                p_selection.Clear();
            }
        }

        public virtual bool ReadOnly
        {
            get { return p_ReadOnly; }
            set { p_ReadOnly = value; }
        }

        public virtual bool DrawBorders
        {
            get { return p_DrawBorders; }
            set { p_DrawBorders = value; }
        }

        public virtual int CursorPosition
        {
            get { return Pos; }
            set { Pos = value; }
        }

        public virtual string SelectedText
        {
            get
            {
                if(p_selection.IsEmpty)
                {
                    return "";
                }
                else
                {
                    return Text.Substring(p_selection.Start, p_selection.Length);
                }
            }
        }

        public virtual int SelectionStart
        {
            get
            {
                if(p_selection.IsEmpty)
                {
                    return Pos;
                }
                else
                {
                    return p_selection.Start;
                }
            }
            set
            {
                Pos = value;
                if (Pos < 0) Pos = 0;
                if (Pos > Text.Length) Pos = Text.Length;
                p_selection.Start = Pos;
                if (p_selection.End == -1) p_selection.End = Pos;
            }
        }
        public virtual bool AutoSelection
        {
            get { return p_AutoSelection; }
            set { p_AutoSelection = value; }
        }
      
        public virtual int SelectionLength
        {
            get
            {
                return p_selection.Length;
            }
            set
            {
                if (value == 0)
                {
                    p_selection.End = p_selection.Start;
                }
                else if (p_selection.IsEmpty)
                {
                    p_selection.Start = 0;
                    p_selection.End = value;
                }
                else if (!p_selection.IsEmpty)
                {
                    p_selection.End = p_selection.Start + value;
                }

                if (!p_selection.IsEmpty)
                {
                    if (p_selection.Start < 0) p_selection.Start = 0;
                    if (p_selection.Start > Text.Length) p_selection.Start = Text.Length;
                    if (p_selection.End < 0) p_selection.End = 0;
                    if (p_selection.End > Text.Length) p_selection.End = Text.Length;
                }
            }
        }
        private List<string> Lines
        {
            get { return p_Lines; }
            set { p_Lines = value; }
        }

        public string Text
        {
            get { return p_Text; }
            set
            {
                if (WordWrap) value = WrapWords(value, p_DrawArea.Width);

                if(p_mode != TextBoxMode.Multiline && value != null)
                {
                    value = value.Replace(p_Separtor, "");
                }

                p_Text = value;

                p_Lines = SplitLines(p_Text);

                if (p_DrawArea != null) MarkasRectUpdate();
            }
        }

        #endregion

        #region Constructors

        public TextBox(TextBoxProps textBoxProps) : base(textBoxProps)
        {
            Lines.Add("");

            p_Font = ContentLoader.GetFontByName("BitStreamRoman");
            p_mode = TextBoxMode.Multiline;
            p_WordWrap = true;
        }

        #endregion

        #region Methods

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            base.DrawEntity(spriteBatch, BaseDraw);

            Text = "fffffffff ffffffffffff fffffffff ffffff fffff fffff ffffffffffff ff";

            Color col = Color.Black;

            Rectangle r = p_InternalDrawArea;
            bool drawsel = !p_selection.IsEmpty;
            string tempText = "";

            if(Text != null && p_Font != null)
            {
                DeterminePages();

                if(p_mode == TextBoxMode.Multiline)
                {
                    p_shownText = Text;
                    tempText = Lines[CaretPosY];
                }
                else if(p_mode == TextBoxMode.Password)
                {
                    p_shownText = "";
                    for(int i = 0; i < Text.Length; i++)
                    {
                        p_shownText = p_shownText + PasswordChar;
                    }
                    tempText = p_shownText;
                }
                else
                {
                    p_shownText = Text;
                    tempText = Lines[CaretPosY];
                }
                
                if(p_mode != TextBoxMode.Multiline)
                {
                    p_LinesDrawn = 0;
                }

                if(string.IsNullOrEmpty(p_Text))
                {
                    Rectangle rx = new Rectangle(r.Left, r.Top, r.Width, r.Height);
                    spriteBatch.DrawString(p_Font, p_Placeholder, rx.Location.ToVector2(), PlaceHolderColor);
                }

                //if(drawsel)
                //{
                //    DrawSelection();
                //}

                int sizey = (int)p_Font.LineSpacing;

                if(p_ShowCursor && p_CaretVisible)
                {
                    Vector2 size = Vector2.Zero;
                    if(CaretPosX > 0 && CaretPosX <= tempText.Length)
                    {
                        size = p_Font.MeasureString(tempText.Substring(0, CaretPosX));
                    }
                    if(size.Y == 0)
                    {
                        size = p_Font.MeasureString(" ");
                        size.X = 0;
                    }

                    int m = r.Height - p_Font.LineSpacing;

                    Rectangle rc = new Rectangle(r.Left + (int)size.X, r.Top + m / 2, 7, p_Font.LineSpacing);

                    if(p_mode == TextBoxMode.Multiline)
                    {
                        rc = new Rectangle(r.Left + (int)size.X, r.Top + (int)(CaretPosX) * p_Font.LineSpacing, 7, p_Font.LineSpacing);
                    }

                }

                for(int i = 0; i < p_LinesDrawn + 1; i++)
                {
                    int ii = i;
                    if (ii >= Lines.Count || ii < 0) break;

                    if(Lines[ii] != "")
                    {
                        if(p_mode == TextBoxMode.Multiline)
                        {
                            spriteBatch.DrawString(p_Font, Lines[ii], new Vector2(r.Left, r.Top + (i * sizey)), col);
                        }
                        else
                        {
                            Rectangle rx = new Rectangle(r.Left, r.Top, r.Width, r.Height);
                            spriteBatch.DrawString(p_Font, p_shownText, rx.Location.ToVector2(), col);
                        }
                    }
                }
            }
        }

        public override void Update(ref Entity targetEntity, ref Entity dragTargetEntity, ref bool wasEventHandled, GameTime gameTime, Point scrollVal)
        {
            base.Update(ref targetEntity, ref dragTargetEntity, ref wasEventHandled, gameTime, scrollVal);

            bool sc = p_ShowCursor;

            p_ShowCursor = IsFocused;

            if(IsFocused)
            {
                Text = Text.Insert(CaretPosX, InputUtils.GetTextInput(Text, ref p_CaretPosX));
                p_FlashTime += gameTime.ElapsedGameTime.TotalSeconds;
                p_ShowCursor = p_FlashTime < 0.5;
                if (p_FlashTime > 1) p_FlashTime = 0;
            }

            if (sc != p_ShowCursor) MarkasRectUpdate();
        }

        private void DeterminePages()
        {
            if(p_DrawArea != null)
            {
                int sizey = (int)p_Font.LineSpacing;
                p_LinesDrawn = (int)(p_DrawArea.Height / sizey);
                if (p_LinesDrawn > Lines.Count) p_LinesDrawn = Lines.Count;

                p_CharsDrawn = p_DrawArea.Width - 1;
            }
        }

        private string GetMaxLine()
        {
            int max = 0;
            int x = 0;

            for(int i = 0; i < Lines.Count; i++)
            {
                if(Lines[i].Length > max)
                {
                    max = Lines[i].Length;
                    x = i;
                }
            }

            return Lines.Count > 0 ? Lines[x] : "";
        }

        private int GetStringWidth(string text, int count)
        {
            if (count > text.Length) count = text.Length;
            return (int)p_Font.MeasureString(text.Substring(0, count)).X;
        }

        private int GetFitChars(string text, int width)
        {
            int ret = p_Text.Length;
            int size = 0;

            for(int i = 0; i < p_Text.Length; i++)
            {
                size = (int)p_Font.MeasureString(p_Text.Substring(0, i)).X;
                if(size > width)
                {
                    ret = i;
                    break;
                }
            }

            return ret;
        }

        private int FindPrevWord(string text)
        {
            bool letter = false;

            int p = Pos - 1;
            if (p < 0) p = 0;
            if (p >= text.Length) p = text.Length - 1;

            for(int i = p; i >= 0; i--)
            {
                if(char.IsLetterOrDigit(text[i]))
                {
                    letter = true;
                    continue;
                }
                if(letter && !char.IsLetterOrDigit(text[i]))
                {
                    return i + 1;
                }
            }

            return 0;
        }

        private int FindNextWord(string text)
        {
            bool space = false;

            for(int i = Pos; i < text.Length - 1; i++)
            {
                if(!char.IsLetterOrDigit(text[i]))
                {
                    space = true;
                    continue;
                }
                if(space && char.IsLetterOrDigit(text[i]))
                {
                    return i;
                }
            }

            return text.Length;
        }

        private int GetPosY(int pos)
        {
            if (pos >= Text.Length) return Lines.Count - 1;

            int p = pos;
            for(int i = 0; i < Lines.Count; i++)
            {
                p -= Lines[i].Length + p_Separtor.Length;
                if(p < 0)
                {
                    p = p + Lines[i].Length + p_Separtor.Length;
                    return i;
                }
            }

            return 0;
        }

        private int GetPosX(int pos)
        {
            if (pos >= Text.Length) return Lines[Lines.Count - 1].Length;

            int p = pos;
            for(int i = 0; i < Lines.Count; i++)
            {
                p -= Lines[i].Length + p_Separtor.Length;
                if(p < 0)
                {
                    p = p + Lines[i].Length + p_Separtor.Length;
                    return p;
                }
            }

            return 0;
        }


        private int GetPos(int x, int y)
        {
            int p = 0;

            for(int i = 0; i < y; i++)
            {
                p += Lines[i].Length + p_Separtor.Length;
            }

            p += x;

            return p;
        }

        private int CharAtPos(Point pos)
        {
            int x = pos.X;
            int y = pos.Y;
            int px = 0;
            int py = 0;

            if(p_mode == TextBoxMode.Multiline)
            {
                py = (int)((y - p_DrawArea.Top) / p_Font.LineSpacing);
                if (py < 0) py = 0;
                if (py >= Lines.Count) py = Lines.Count - 1;
            }
            else
            {
                py = 0;
            }

            string str = p_mode == TextBoxMode.Multiline ? Lines[py] : p_shownText;

            if(str != null && str != "")
            {
                for(int i = 1; i <= Lines[py].Length; i++)
                {
                    Vector2 v = p_Font.MeasureString(str.Substring(0, i)) - (p_Font.MeasureString(str[i - 1].ToString()) / 3);
                    if(x <= (p_DrawArea.Left + (int)v.X))
                    {
                        px = i - 1;
                        break;
                    }
                }
                if(x > p_DrawArea.Left + ((int)p_Font.MeasureString(str).X) - (p_Font.MeasureString(str[str.Length - 1].ToString()).X / 3)) px = str.Length;
            }

            return GetPos(px, py);
        }

        private void GetText(IAsyncResult result)
        {
            
        }

        private string WrapWords(string text, int size)
        {
            string ret = "";
            string line = "";

            string[] words = text.Replace("\v", "").Split(" ".ToCharArray());

            for(int i = 0; i < words.Length; i++)
            {
                if(p_Font.MeasureString(line + words[i]).X > size)
                {
                    ret += line + "\n";
                    line = words[i] + " ";
                }
                else
                {
                    line += words[i] + " ";
                }
            }

            ret += line;

            return ret.Remove(ret.Length - 1, 1);                
        }

        public virtual void SelectAll()
        {
            if(p_Text.Length > 0)
            {
                p_selection.Start = 0;
                p_selection.End = Text.Length;
            }
        }

        private List<string> SplitLines(string text)
        {
            if (p_Buffer != text)
            {
                p_Buffer = text;
                List<string> list = new List<string>();
                string[] s = text.Split(new char[] { p_Separtor[0] });
                list.Clear();

                list.AddRange(s);

                if (p_CaretPosY < 0) p_CaretPosY = 0;
                if (p_CaretPosY > list.Count - 1) p_CaretPosY = list.Count - 1;

                if (p_CaretPosX < 0) p_CaretPosX = 0;
                if (p_CaretPosX > list[CaretPosY].Length) p_CaretPosX = list[CaretPosY].Length;

                return list;
            }
            else return p_Lines;
        }

        #endregion
    }
    //    public class TextBox : Panel 
    //    {
    //        #region Fields

    //        private Paragraph p_Text;

    //        private Sprite p_CaretSprite;
    //        private Rectangle p_CaretDrawRect;
    //        private Color p_CaretColor;
    //        private int CaretPosX = -1;
    //        private int LastCaretPosX = 0;
    //        private int CaretPosY = 0;
    //        private float p_CaretAnimStep = 0f;
    //        private static float p_CaretBlinkSpeed = 2f;
    //        private bool DrawCaret = false;

    //        private char p_hiddenTextChar = '*';
    //        private bool p_HideTextWithChar = false;

    //        private string p_editText = string.Empty;
    //        #endregion

    //        #region Properties

    //        public Paragraph Text { get { return p_Text; } set { p_Text = value; MarkasRectUpdate(); } }

    //        #endregion

    //        #region Methods

    //        public TextBox(TextBoxProps textBoxProps) : base(textBoxProps)
    //        {
    //            p_Text = UserInterface.DefaultParagraph(string.Empty, Anchor.TopLeft, Color.Black, null, Vector2.Zero, null, null);
    //            AddChild(p_Text);

    //            p_CaretSprite = textBoxProps.CaretSprite;
    //            p_CaretDrawRect = new Rectangle(p_DrawArea.Y, p_DrawArea.X, p_CaretSprite.Texture.Width, p_CaretSprite.Texture.Height);
    //            p_CaretColor = Color.Black;
    //            CaretPosX = -1;
    //        }

    //        protected string PrepareTextForDipaly(bool showCaret)
    //        {
    //            if(p_HideTextWithChar)
    //            {
    //                var HiddenVal = new string(p_hiddenTextChar, p_editText.Length);
    //                p_Text.Text = HiddenVal;
    //            }
    //            else
    //            {
    //                p_Text.Text = p_editText;
    //            }

    //            Paragraph currentParagraph = p_Text;

    //            p_Text.UpdateDestinationRectsIfNeeded();

    //            return currentParagraph.GetProcessedText();
    //        }

    //        protected override void DoBeforeUpdate()
    //        {
    //            p_CaretAnimStep += (float)InputUtils.CurrentGameTime.ElapsedGameTime.TotalSeconds * p_CaretBlinkSpeed;

    //            p_CaretColor = (int)p_CaretAnimStep % 2 == 0 ? Color.Black : Color.Transparent;

    //            DrawCaret = IsFocused;

    //            if (IsFocused)
    //            {
    //                UpdateCaretPosition();
    //                InputUtils.TextInputing = true;
    //                p_editText = InputUtils.GetTextInput(p_editText, ref CaretPosX);
    //            }

    //            InputUtils.TextInputing = false;

    //            p_Text.Text = p_editText;

    //            p_Text.UpdateDestinationRectsIfNeeded();

    //            p_Text.Text = p_Text.GetProcessedText();

    //            base.DoBeforeUpdate();
    //        }

    //        private void UpdateCaretPosition()
    //        {
    //            if (LastCaretPosX < 0) LastCaretPosX = 0;

    //            if (CaretPosX - LastCaretPosX <= 0 && CaretPosX > 0)
    //            {
    //                CaretPosY--;
    //                LastCaretPosX -= p_Text.Lines[CaretPosY].Length - 1;
    //            }
    //            else if (CaretPosX - LastCaretPosX >= p_Text.Lines[CaretPosY].Length)
    //            {
    //                LastCaretPosX += (p_Text.Lines[CaretPosY].Length - 1);
    //                LastCaretPosX += ((p_Text.MaxNumberOfCharsPerLine * (CaretPosY + 1)) - LastCaretPosX);

    //                if (LastCaretPosX < 0) LastCaretPosX = 0;

    //                //int diff = (CaretPosX - LastCaretPosX);

    //                if (CaretPosX - LastCaretPosX > 0)
    //                {
    //                    CaretPosY++;
    //                }
    //            }

    //            int CaretY = p_Text.p_DrawArea.Y + ((int)p_Text.GetCurrentFont().MeasureString(" ").Y * CaretPosY - 1);
    //            int CaretX = p_Text.p_DrawArea.X + ((int)p_Text.GetCurrentFont().MeasureString(" ").X * (CaretPosX - LastCaretPosX));
    //            int CaretWidth = 1;
    //            int CaretHeight = (int)p_Text.GetCurrentFont().LineSpacing;

    //            p_CaretDrawRect = new Rectangle(CaretX, CaretY, CaretWidth, CaretHeight);
    //        }

    //        private bool ValidateInput(ref string newValue, string oldValue)
    //        {
    //            return true;
    //        }

    //        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
    //        { 
    //            base.DrawEntity(spriteBatch, BaseDraw);

    //            if (DrawCaret)
    //            {
    //                p_CaretSprite.Draw(spriteBatch, SpriteDrawMode.Stretch, p_CaretDrawRect, p_CaretColor);
    //            }
    //        }

    //        #endregion
    //    }

}
