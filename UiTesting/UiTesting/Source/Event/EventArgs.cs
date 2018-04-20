using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using UiTesting.Source.Input;

namespace UiTesting.Source.Event
{
    public class EventArgs : System.EventArgs
    {
        public bool Handled = false;

        public EventArgs()
        {

        }
    }

    public class KeyEventArgs : EventArgs
    {
        public Keys Key = Keys.None;
        public bool Control = false;
        public bool Shift = false;
        public bool Alt = false;
        public bool Caps = false;

        public KeyEventArgs()
        {

        }

        public KeyEventArgs(Keys key)
        {
            Key = key;
            Control = false;
            Shift = false;
            Alt = false;
            Caps = false;
        }

        public KeyEventArgs(Keys key, bool control, bool shift, bool alt, bool caps)
        {
            Key = key;
            Control = control;
            Shift = shift;
            Alt = alt;
            Caps = caps;
        }
    }

    public class MouseEventArgs : EventArgs
    {
        public MouseState State = new MouseState();
        public MouseButtons Button = MouseButtons.None;
        public Point Position = new Point(0, 0);
        public Point Difference = new Point(0, 0);

        public MouseScrollDirection ScrollDirection = MouseScrollDirection.None;

        public MouseEventArgs()
        {

        }

        public MouseEventArgs(MouseState state, MouseButtons button, Point position)
        {
            State = state;
            Button = button;
            Position = position;
        }

        public MouseEventArgs(MouseState state, MouseButtons button, Point position, MouseScrollDirection scrollDirection) : this(state, button, position)
        {
            ScrollDirection = scrollDirection;
        }

        public MouseEventArgs(MouseEventArgs e) : this(e.State, e.Button, e.Position)
        {
            Difference = e.Difference;
            ScrollDirection = e.ScrollDirection;
        }
    }

    public class GamePadEventArgs
    {
        public PlayerIndex PlayerIndex = PlayerIndex.One;
        public GamePadState State = new GamePadState();
        public GamePadButton Button = GamePadButton.None;
        public GamePadVectors Vectors;

        public GamePadEventArgs(PlayerIndex playerIndex)
        {
            PlayerIndex = playerIndex;
        }

        public GamePadEventArgs(PlayerIndex playerIndex, GamePadButton button)
        {
            PlayerIndex = playerIndex;
            Button = button;
        }
    }
}
