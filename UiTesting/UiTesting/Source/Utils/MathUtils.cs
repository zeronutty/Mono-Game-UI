using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    public class MathUtils
    {


    }

    public class CustomRectangle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int Width { get { return X2 - X; } }
        public int Height { get { return Y2 - Y; } }

        public CustomRectangle()
        {

        }

        public CustomRectangle(int x, int y, int x2, int y2)
        {
            X = x;
            Y = y;
            X2 = x2;
            Y2 = y2;
        }
    }
}
