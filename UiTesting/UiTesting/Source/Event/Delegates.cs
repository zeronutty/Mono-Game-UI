using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UiTesting.Source.Event;

namespace UiTesting.Source
{
    #region Delegates

    public delegate void EventHandler(object sender, Event.EventArgs e);
    public delegate void MouseEventHandler(object sender, MouseEventArgs e);
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);
    public delegate void GamePadEventHandler(object sender, GamePadEventArgs e);

    #endregion
}
