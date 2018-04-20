using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    public class ConsoleWindow : Panel
    {
        #region Fields
        public Paragraph ConsoleText;

        public List<string> Lines = new List<string>();
        #endregion

        #region Properties
        #endregion

        #region Methods
        public ConsoleWindow (PanelProps panelProps) : base(panelProps)
        {
            ConsoleText = UiManager.DefaultParagraph(string.Empty, Anchor.TopLeft, Color.Black, null, new Vector2(0, 0.75f), null, new Vector2(5, 5));
            PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll;

            AddChild(ConsoleText);

            Visible = false;
        }

        public void AddText(string text)
        {
            ConsoleText.Text += text;
        }

        public void NewLineText(string text)
        {
            ConsoleText.Text += text + '\n';
            Lines.Add(text);
        }
        #endregion
    }
}
