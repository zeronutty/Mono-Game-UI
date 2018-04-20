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
    public class TextInput : Panel
    {
        #region Fields
        string p_value = string.Empty;

        int p_caret = -1;

        public Paragraph TextParagraph;

        public Paragraph PlaceHolderParagraph;

        protected bool p_UseMultiLine;

        VerticalScrollBar p_scrollBar;

        public string ValueWhenEmpty = null;

        float _caretAnim = 0f;

        string p_PlaceHolderText = string.Empty;

        public int CharacterLimit = 0;

        public bool LimitBySize = false;

        public char? HideInputWithChar;

        public static float CaretBlinkSpeed = 2f;

        new public static Vector2 DefaultSize = new Vector2(0f, 65f);

        string p_actuaDisplayText = string.Empty;

        public List<ITextValidator> Validators = new List<ITextValidator>();
        #endregion

        #region Properties

        public string PlaceHolderText { get { return p_PlaceHolderText; } set { p_PlaceHolderText = p_UseMultiLine ? value : value.Replace("\n", string.Empty); } }

        public string Value { get { return p_value; } set { if (value != null) { p_value = p_UseMultiLine ? value : value.Replace("\n", string.Empty); FixCaretPosition(); } } }

        public int Caret { get { return p_caret; } set { p_caret = value; } }

        public int ScrollPosition { get { return p_scrollBar.Value; } set { p_scrollBar.Value = value; } }

        #endregion

        #region Methods

        public TextInput(TextInputProps textInputProps) : base(textInputProps)
        {
            p_UseMultiLine = textInputProps.UseMultiLine;

            p_Padding = new Vector2(20, 20);

            if(textInputProps.UseMultiLine && textInputProps.Size.Y == -1)
            {
                p_size.Y *= 4;
            }

            LimitBySize = !p_UseMultiLine;

            TextParagraph = UiManager.DefaultParagraph(string.Empty, p_UseMultiLine ? Anchor.TopLeft : Anchor.CenterLeft);
            TextParagraph.p_HiddenInternalEntity = true;
            AddChild(TextParagraph, true);

            PlaceHolderParagraph = UiManager.DefaultParagraph(string.Empty, p_UseMultiLine ? Anchor.TopLeft : Anchor.CenterLeft);
            PlaceHolderParagraph.p_HiddenInternalEntity = true;
            AddChild(PlaceHolderParagraph, true);

            if(p_UseMultiLine)
            {
                p_scrollBar = new VerticalScrollBar(new SliderProps { Min = 0, Max = 0, EntityAnchor = Anchor.CenterRight, LocalPosition = new Vector2(-8, 0), Size = new Vector2(10,0)});
                p_scrollBar.Value = 0;
                p_scrollBar.Visible = false;
                p_scrollBar.p_HiddenInternalEntity = true;
                AddChild(p_scrollBar, false);
            }

            TextParagraph.WarpsWords = p_UseMultiLine;
            PlaceHolderParagraph.WarpsWords = p_UseMultiLine;

            MultiColorParagraph colorTextParagraph = TextParagraph as MultiColorParagraph;
            if(colorTextParagraph != null)
            {
                colorTextParagraph.EnableColorInstructions = false;
            }
        }

        public override bool IsNaturallyInteractable()
        {
            return true;
        }

        public void ScrollToCaret()
        {
            if (p_scrollBar == null)
            {
                return;
            }

            if(p_caret >= p_value.Length)
            {
                p_caret = -1;
            }

            if(p_caret == -1)
            {
                p_scrollBar.Value = (int)p_scrollBar.Max;
            }
            else
            {
                TextParagraph.Text = p_value;
                TextParagraph.CalcTextActualRectWithWarp();
                string processedValueText = TextParagraph.GetProcessedText();
                int currLine = processedValueText.Substring(0, p_caret).Split('\n').Length;
                p_scrollBar.Value = currLine - 1;
            }
        }

        public void ResetCaret(bool scrollToCaret)
        {
            Caret = -1;
            if(scrollToCaret)
            {
                ScrollToCaret();
            }
        }

        protected string PrepareInputTextForDisplay(bool usePlaceholder, bool showCaret)
        {
            string caretShow = showCaret ? ((int)_caretAnim % 2 == 0 ? "|" : " ") : string.Empty;

            if(HideInputWithChar != null)
            {
                var hiddenVal = new string(HideInputWithChar.Value, p_value.Length);
                TextParagraph.Text = hiddenVal.Insert(p_caret >= 0 ? p_caret : hiddenVal.Length, caretShow);
            }
            else
            {
                TextParagraph.Text = p_value.Insert(p_caret >= 0 ? p_caret : p_value.Length, caretShow);
            }

            PlaceHolderParagraph.Text = p_PlaceHolderText;

            Paragraph currParagraph = usePlaceholder ? PlaceHolderParagraph : TextParagraph;
            TextParagraph.UpdateDestinationRectsIfNeeded();

            return currParagraph.GetProcessedText();
        }

        protected override void DoOnClick()
        {
            base.DoOnClick();

            if (p_value.Length > 0)
            {
                Vector2 actualParagraphPos = new Vector2(p_InternalDrawArea.Location.X, p_InternalDrawArea.Location.Y);
                Vector2 realtiveOffset = GetMousePos(-actualParagraphPos);

                Vector2 charSize = TextParagraph.GetCharacterActualSize();
                int x = (int)(realtiveOffset.X / charSize.X);
                p_caret = x;

                if (p_UseMultiLine)
                {
                    TextParagraph.Text = p_value;
                    TextParagraph.CalcTextActualRectWithWarp();
                    string processedValueText = TextParagraph.GetProcessedText();

                    int y = (int)(realtiveOffset.Y / charSize.Y) + p_scrollBar.Value;

                    List<string> lines = new List<string>(processedValueText.Split('\n'));
                    for (int i = 0; i < y && i < lines.Count; i++)
                    {
                        p_caret += lines[i].Length + 1;
                    }
                }

                if (p_caret >= p_value.Length)
                {
                    p_caret = -1;
                }
            }
            else
            {
                p_caret = -1;
            }
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            base.DrawEntity(spriteBatch, BaseDraw);

            bool showPlaceHolder = !(IsFocused || p_value.Length > 0);
            Paragraph currParagraph = showPlaceHolder ? PlaceHolderParagraph : TextParagraph;

            p_actuaDisplayText = PrepareInputTextForDisplay(showPlaceHolder, IsFocused);

            if(p_UseMultiLine && p_actuaDisplayText != null)
            {
                int linesFit = p_InternalDrawArea.Height / (int)(System.Math.Max(currParagraph.GetCharacterActualSize().Y, 1));
                int linesInText = p_actuaDisplayText.Split('\n').Length;

                if(linesInText > linesFit)
                {
                    float prevWidth = currParagraph.Size.X;
                    currParagraph.Size = new Vector2(p_InternalDrawArea.Width / GlobalScale - p_scrollBar.p_DrawArea.Width, 0);
                    if(currParagraph.Size.X != prevWidth)
                    {
                        p_actuaDisplayText = PrepareInputTextForDisplay(showPlaceHolder, IsFocused);
                        linesInText = p_actuaDisplayText.Split('\n').Length;
                    }

                    p_scrollBar.Max = (uint)System.Math.Max(linesInText - linesFit, 2);
                    p_scrollBar.StepsCount = p_scrollBar.Max;
                    p_scrollBar.Visible = true;

                    List<string> lines = new List<string>(p_actuaDisplayText.Split('\n'));
                    int from = System.Math.Min(p_scrollBar.Value, lines.Count - 1);
                    int size = System.Math.Min(linesFit, lines.Count - from);
                    lines = lines.GetRange(from, size);
                    p_actuaDisplayText = string.Join("\n", lines);
                    currParagraph.Text = p_actuaDisplayText;
                }
                else
                {
                    currParagraph.Size = Vector2.Zero;
                    p_scrollBar.Visible = false;
                }
            }

            TextParagraph.Visible = !showPlaceHolder;
            PlaceHolderParagraph.Visible = showPlaceHolder;
        }

        private void FixCaretPosition()
        {
            if(p_caret < -1) { p_caret = 0; }
            if(p_caret >= p_value.Length || p_value.Length == 0) { p_caret = -1; }
        }

        private bool ValidateInput(ref string newValue, string oldValue)
        {
            if(CharacterLimit != 0 && newValue.Length > CharacterLimit)
            {
                newValue = oldValue;
                return false;
            }

            if(!p_UseMultiLine && newValue.Contains("\n"))
            {
                newValue = oldValue;
                return false;
            }

            if(LimitBySize)
            {
                PrepareInputTextForDisplay(false, false);

                Rectangle TextSize = TextParagraph.GetActualDestinationRectangle();

                if(p_UseMultiLine && TextSize.Height >= p_InternalDrawArea.Height)
                {
                    newValue = oldValue;
                    return false;
                }
                else if(TextSize.Width >= p_InternalDrawArea.Width)
                {
                    newValue = oldValue;
                    return false;
                }
            }

            foreach(var v in Validators)
            {
                if(!v.ValidateText(ref newValue, oldValue))
                {
                    newValue = oldValue;
                    return false;
                }
            }

            return true;
        }

        protected override void DoBeforeUpdate()
        {
            _caretAnim += (float)InputUtils.CurrentGameTime.ElapsedGameTime.TotalSeconds * CaretBlinkSpeed;

            if(IsFocused)
            {
                FixCaretPosition();

                int pos =  p_caret;

                string oldVal = p_value;
                p_value = InputUtils.GetTextInput(p_value, ref pos);

                p_caret = pos;

                if(p_value != oldVal)
                {
                    if(!ValidateInput(ref p_value, oldVal))
                    {
                        p_value = oldVal;
                    }

                    if(p_value != oldVal)
                    {
                        DoOnValueChange();
                    }

                    ScrollToCaret();

                    FixCaretPosition();
                }
            }

            base.DoBeforeUpdate();
        }

        protected override void DoOnFocusChange()
        {
            base.DoOnFocusChange();

            if(ValueWhenEmpty != null && Value.Length == 0)
            {
                Value = ValueWhenEmpty;
            }
        }

        #endregion
    }
}
