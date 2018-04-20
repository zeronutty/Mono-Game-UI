using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    public class ConsoleTextWriter : TextWriter
    {
        public ConsoleTextWriter()
        {
        }

        public override Encoding Encoding => throw new NotImplementedException();

        public override bool Equals(object obj)
        {
            var writer = obj as ConsoleTextWriter;
            return writer != null &&
                   EqualityComparer<Encoding>.Default.Equals(Encoding, writer.Encoding);
        }

        public override int GetHashCode()
        {
            return -513917040 + EqualityComparer<Encoding>.Default.GetHashCode(Encoding);
        }

        public override void WriteLine()
        {
            base.WriteLine();
        }

        public override void WriteLine(string value)
        {
            base.WriteLine(value);

            Paragraph paragraph = UiManager.DefaultParagraph(value, Anchor.AutoInLine, Microsoft.Xna.Framework.Color.Black, null, new Microsoft.Xna.Framework.Vector2(0, 20), null);

            UiManager.GetActiveUserInterface().ConsoleWindow.AddChild(paragraph);

        }
    }
}
