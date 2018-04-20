using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    public class ColorInstruction
    {
        private bool p_useFillColor = false;

        private Color p_color = Color.White;

        internal static Dictionary<string, Color> p_colors = new Dictionary<string, Color>()
        {
            { "RED", Color.Red },
            { "BLUE", Color.Blue },
            { "GREEN", Color.Green },
            { "YELLOW", Color.Yellow },
            { "BROWN", Color.Brown },
            { "BLACK", Color.Black },
            { "WHITE", Color.White },
            { "CYAN", Color.Cyan },
            { "PINK", Color.Pink },
            { "GRAY", Color.Gray },
            { "MAGENTA", Color.Magenta },
            { "ORANGE", Color.Orange },
            { "PURPLE", Color.Purple },
            { "SILVER", Color.Silver },
            { "GOLD", Color.Gold },
            { "TEAL", Color.Teal },
            { "NAVY", Color.Navy },
        };

        public static void AddCustomColor(string key, Color color)
        {
            p_colors[key] = color;
        }

        public ColorInstruction(string sColor)
        {
            if(sColor == "DEFAULT")
            {
                p_useFillColor = true;
            }
            else
            {
                p_color = StringToColor(sColor);
            }
        }

        public Color StringToColor(string sColor)
        {
            Color outColor;
            if(!p_colors.TryGetValue(sColor, out outColor))
            {
                return Color.White;
            }
            return outColor;
        }

        public bool UseFillColor { get { return p_useFillColor; } }

        public Color color { get { return p_color; } }
    }

    public class MultiColorParagraph : Paragraph
    {
        #region Feilds
        bool p_EnableColorInstructions = true;

        private string DrawString = string.Empty;

        Dictionary<int, ColorInstruction> p_ColorInstructions = new Dictionary<int, ColorInstruction>();
        #endregion

        #region Properties
        public bool EnableColorInstructions { get { return p_EnableColorInstructions; } set { p_EnableColorInstructions = value;  ParseColorInstructions(); } }

        public override string Text {
            get { return p_Text; }
            set { if (p_Text != value) { p_Text = value; ParseColorInstructions(); MarkasRectUpdate(); } } }
        #endregion

        #region Methods
        public MultiColorParagraph(ParagraphProps paragraphProps) : base(paragraphProps)
        {

        }

        private void ParseColorInstructions()
        {
            p_ColorInstructions.Clear();

            if (!EnableColorInstructions) { return; }

            if(p_Text.Contains("{{"))
            {
                int iLastLength = 0;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("{{[^{}]*}}");

                System.Text.RegularExpressions.MatchCollection matchCollection = regex.Matches(p_Text);
                foreach(System.Text.RegularExpressions.Match m in matchCollection)
                {
                    string sColor = m.Value.Substring(2, m.Value.Length - 4);

                    p_ColorInstructions.Add(m.Index - iLastLength, new ColorInstruction(sColor));
                    iLastLength += m.Value.Length;
                }

                p_Text = regex.Replace(p_Text, string.Empty);
            }
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            if (p_ColorInstructions.Count > 0)
            {
                int iTextIndex = 0;
                Color color = OverlayColor;
                Vector2 oCharacterSize = GetCharacterActualSize();
                Vector2 oCurrentPosition = new Vector2(p_Position.X - oCharacterSize.X, p_Position.Y);
                foreach (char c in p_ProcessedText)
                {                   
                    ColorInstruction colorInstruction;
                    if(p_ColorInstructions.TryGetValue(iTextIndex, out colorInstruction))
                    {
                        if(colorInstruction.UseFillColor)
                        {
                            color = Color.Black;
                        }
                        else
                        {
                            color = colorInstruction.color;
                        }
                    }

                    if(c == '\n')
                    {
                        oCurrentPosition.X = p_Position.X - oCharacterSize.X;
                        oCurrentPosition.Y += p_CurrentFont.LineSpacing * p_ActualScale;
                    }
                    else
                    {
                        iTextIndex++;
                        oCurrentPosition.X += oCharacterSize.X;
                    }

                    Color fillColor = UiManager.GetActiveUserInterface().drawUtils.FixColorOpacity(color);
                    
                    spriteBatch.DrawString(p_CurrentFont, c.ToString(), oCurrentPosition, fillColor, 0, p_FontOrigin, p_ActualScale, SpriteEffects.None, 0.5f);

                    base.DrawEntity(spriteBatch, true);
                }
            }
            else
            {
                base.DrawEntity(spriteBatch);
            }
        }
        #endregion
    }
}
