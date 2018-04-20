using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using UiTesting.Source.Logging;

namespace UiTesting.Source.Input
{
    public enum MouseButton
    {
        Left,
        Right,
        Middle
    }

    enum SpecialChars
    {
        Null = 0,
        Delete = 127,
        Backspace = 8,
        Space = 32,
        ArrowLeft = 1,
        ArrowRight = 2
    }

    public static class InputUtils
    {
        #region Feilds

        public static Dictionary<string, ActionMap> ActionMaps = new Dictionary<string, ActionMap>();

        public static GameTime CurrentGameTime;

        public static MouseState p_NewMouseState;
        public static MouseState p_OldMouseState;

        private static KeyboardState p_newKeybroadState;
        private static KeyboardState p_OldKeybroadState;

        private static Vector2 p_newMousePos;
        private static Vector2 p_oldMousePos;


        public static float KeysTypeCooldown = 0.6f;
        static char p_CurrCharaterInput = (char)SpecialChars.Null;

        static Keys p_currCharaterInputKey = Keys.Escape;

        static float p_KeyboardInputCooldown = 0f;

        static bool p_newKeyIsPressed = false;

        static bool p_capLock = false;

        public static int MouseWheel = 0;
        public static int MouseWheelChange = 0;

        public static bool TextInputing = false;

        #endregion

        #region Properties
        #endregion

        #region Methods

        #region KeyBroad Events

        public static bool IsKeyPressed(Keys _key)
        {
            return p_newKeybroadState.IsKeyDown(_key);
        }

        public static bool IsKeyTriggered(Keys _key)
        {
            return ((p_newKeybroadState.IsKeyDown(_key)) && (!p_OldKeybroadState.IsKeyDown(_key)));
        }

        #endregion

        #region Mouse Events

        public static bool IsMouseButtonLeftPressed()
        {
            return p_NewMouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool IsMouseButtonMiddlePressed()
        {
            return p_NewMouseState.MiddleButton == ButtonState.Pressed;
        }

        public static bool IsMouseButtonRightPressed()
        {
            return p_NewMouseState.RightButton == ButtonState.Pressed;
        }

        public static bool IsMouseButtonLeftReleased()
        {
            return p_NewMouseState.LeftButton == ButtonState.Released;
        }

        public static bool IsMouseButtonMiddleReleased()
        {
            return p_NewMouseState.MiddleButton == ButtonState.Released;
        }

        public static bool IsMouseButtonRightReleased()
        {
            return p_NewMouseState.RightButton == ButtonState.Released;
        }

        public static bool IsMouseButtonLeftTiggered()
        {
            return ((p_NewMouseState.LeftButton == ButtonState.Pressed) && (p_OldMouseState.LeftButton == ButtonState.Released));
        }

        public static bool IsMouseButtonMiddleTiggered()
        {
            return ((p_NewMouseState.MiddleButton == ButtonState.Pressed) && (p_OldMouseState.MiddleButton == ButtonState.Released));
        }

        public static bool IsMouseButtonRightTiggered()
        {
            return ((p_NewMouseState.RightButton == ButtonState.Pressed) && (p_OldMouseState.RightButton == ButtonState.Released));
        }

        public static bool IsAnyMouseButtonPressed()
        {
            return ((p_NewMouseState.LeftButton == ButtonState.Pressed) || (p_NewMouseState.MiddleButton == ButtonState.Pressed) || (p_NewMouseState.RightButton == ButtonState.Pressed));
        }

        #endregion

        #region Getters

        public static Vector2 GetMousePosition()
        {
            return new Vector2(p_NewMouseState.X, p_NewMouseState.Y);
        }

        public static Rectangle GetMouseRect()
        {
            return new Rectangle((int)GetMousePosition().X, (int)GetMousePosition().Y, 10, 10);
        }

        public static Vector2 MousePositionDiff
        {
            get { return p_newMousePos - p_oldMousePos; }
        }

        public static bool IsMouseOverUIElement()
        {
            return UiManager.GetActiveUserInterface().TargetEntity != null;
        }

        #endregion

        #region ActionBinds

        public static bool IsActionPressed(string _action)
        {
            ActionMap actionMap;

            if (ActionMaps.TryGetValue(_action, out actionMap))
            {
                return IsActionMapPressed(ActionMaps[_action]);
            }
            else
            {
                LogHelper.Log(LogTarget.Console, LogLevel.Warning, "Action Map Does Not Exisit");
                return false;
            }
            
        }

        private static bool IsActionMapPressed(ActionMap _action)
        {
            for(int i = 0; i < _action.keyBroadBinds.Count; i++)
            {
                if(IsKeyPressed(_action.keyBroadBinds[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsActionTriggered(string _action)
        {
            ActionMap actionMap;

            if (ActionMaps.TryGetValue(_action, out actionMap))
            {
                return IsActionMapTriggered(ActionMaps[_action]);
            }
            else
            {
                LogHelper.Log(LogTarget.Console, LogLevel.Warning, "Action Map " + _action + " Does Not Exisit");
                return false;
            }
        }

        private static bool IsActionMapTriggered(ActionMap _action)
        {
            for (int i = 0; i < _action.keyBroadBinds.Count; i++)
            {
                if (IsKeyTriggered(_action.keyBroadBinds[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public static void ResetKeyBinds()
        {
            ActionMaps.Add("Open Console", new ActionMap());
            ActionMaps["Open Console"].ActionName = "Open Console";
            ActionMaps["Open Console"].keyBroadBinds.Add(Keys.Oem8);
        }

        public static void LoadKeyBinds()
        {
            string Path = "Content/KeyBinds.Xml";

            FileStream stream = File.Open(Path, FileMode.OpenOrCreate, FileAccess.Read);
            XmlSerializer serializer = new XmlSerializer(typeof(List<ActionMap>));
            try
            {
                List<ActionMap> list = new List<ActionMap>();
                    list = (List<ActionMap>)serializer.Deserialize(stream);

                foreach(ActionMap am in list)
                {
                    ActionMaps.Add(am.ActionName, am);
                }
            }
            catch(Exception e)
            {
                Logging.LogHelper.Log(Logging.LogTarget.Console, Logging.LogLevel.Error, "Input: Could Not Load KeyBinds" + e.ToString());
            }
            finally
            {
                stream.Close();
            }
        }

        public static void SaveKeyBinds()
        {
            string Path = "Content/KeyBinds.xml";

            FileStream stream = File.Open(Path, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(List<ActionMap>));
            try
            {
                List<ActionMap> list = new List<ActionMap>();
                foreach(ActionMap am in ActionMaps.Values)
                {
                    list.Add(am);
                }

                serializer.Serialize(stream, list);
            }
            catch(Exception e)
            {
                Logging.LogHelper.Log(Logging.LogTarget.Console, Logging.LogLevel.Error, "Input: Could Not Load KeyBinds" + e.ToString());
            }
            finally
            {
                stream.Close();
            }
        }

        public static void AddKeyBind(string Name ,Keys key)
        {
            ActionMap actionMap = new ActionMap();
            actionMap.ActionName = Name;
            actionMap.keyBroadBinds.Add(key);

            ActionMaps.Add(Name, actionMap);
        }

        public static void RemoveAction(string name)
        {
            ActionMaps.Remove(name);
        }

        #endregion

        public static Vector2 TransformCursorPos(Matrix? transform)
        {
            var newMousePos = p_newMousePos;
            if (transform != null)
            {
                return Vector2.Transform(newMousePos, transform.Value) - new Vector2(transform.Value.Translation.X, transform.Value.Translation.Y);
            }
            return newMousePos;
        }

        public static void Initialize()
        { 
            LoadKeyBinds();
        }

        public static void Update(GameTime gameTime)
        {
            CurrentGameTime = gameTime;

            p_OldMouseState = p_NewMouseState;
            p_NewMouseState = Mouse.GetState();

            p_oldMousePos = p_newMousePos;
            p_newMousePos = new Vector2(p_NewMouseState.X, p_NewMouseState.Y);

            p_OldKeybroadState = p_newKeybroadState;
            p_newKeybroadState = Keyboard.GetState();

            int prevMouseWheel = MouseWheel;
            MouseWheel = p_NewMouseState.ScrollWheelValue;
            MouseWheelChange = Math.Sign(MouseWheel - prevMouseWheel);

            if (p_newKeybroadState.IsKeyDown(Keys.CapsLock) && !p_OldKeybroadState.IsKeyDown(Keys.CapsLock))
            {
                p_capLock = !p_capLock;
            }

            if (p_KeyboardInputCooldown > 0f)
            {
                p_newKeyIsPressed = false;
                p_KeyboardInputCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (p_CurrCharaterInput != (char)SpecialChars.Null && !p_newKeybroadState.IsKeyDown(p_currCharaterInputKey))
            {
                p_CurrCharaterInput = (char)SpecialChars.Null;
            }

            foreach (Keys key in System.Enum.GetValues(typeof(Keys)))
            {
                if (p_newKeybroadState.IsKeyDown(key) && !p_OldKeybroadState.IsKeyDown(key))
                {
                    OnKeyPressed(key);
                }
            }
        }

        #endregion







        private static void OnKeyPressed(Keys key)
        {
            NewKeyTextInput(key);
        }

        private static void NewKeyTextInput(Keys key)
        {
            p_KeyboardInputCooldown = KeysTypeCooldown;
            p_newKeyIsPressed = true;

            bool isShiftDown = p_newKeybroadState.IsKeyDown(Keys.LeftShift) || p_newKeybroadState.IsKeyDown(Keys.RightShift);

            Keys prevKey = p_currCharaterInputKey;
            p_currCharaterInputKey = key;

            switch (key)
            {
                case Keys.Space:
                    p_CurrCharaterInput = (char)SpecialChars.Space;
                    return;
                case Keys.Left:
                    p_CurrCharaterInput = (char)SpecialChars.ArrowLeft;
                    return;
                case Keys.Right:
                    p_CurrCharaterInput = (char)SpecialChars.ArrowRight;
                    return;
                case Keys.Delete:
                    p_CurrCharaterInput = (char)SpecialChars.Delete;
                    return;
                case Keys.Back:
                    p_CurrCharaterInput = (char)SpecialChars.Backspace;
                    return;

                case Keys.CapsLock:
                case Keys.RightShift:
                case Keys.LeftShift:
                    p_newKeyIsPressed = false;
                    return;

                case Keys.Enter:
                    p_CurrCharaterInput = '\n';
                    return;

                case Keys.D0:
                case Keys.NumPad0:
                    p_CurrCharaterInput = (isShiftDown && key == Keys.D0) ? ')' : '0';
                    return;

                case Keys.D9:
                case Keys.NumPad9:
                    p_CurrCharaterInput = (isShiftDown && key == Keys.D9) ? '(' : '9';
                    return;

                case Keys.D8:
                case Keys.NumPad8:
                    p_CurrCharaterInput = (isShiftDown && key == Keys.D8) ? '*' : '8';
                    return;

                case Keys.D7:
                case Keys.NumPad7:
                    p_CurrCharaterInput = (isShiftDown && key == Keys.D7) ? '&' : '7';
                    return;

                case Keys.D6:
                case Keys.NumPad6:
                    p_CurrCharaterInput = (isShiftDown && key == Keys.D6) ? '^' : '6';
                    return;

                case Keys.D5:
                case Keys.NumPad5:
                    p_CurrCharaterInput = (isShiftDown && key == Keys.D5) ? '%' : '5';
                    return;

                case Keys.D4:
                case Keys.NumPad4:
                    p_CurrCharaterInput = (isShiftDown && key == Keys.D4) ? '$' : '4';
                    return;

                case Keys.D3:
                case Keys.NumPad3:
                    p_CurrCharaterInput = (isShiftDown && key == Keys.D3) ? '£' : '3';
                    return;

                case Keys.D2:
                case Keys.NumPad2:
                    p_CurrCharaterInput = (isShiftDown && key == Keys.D2) ? '\"' : '2';
                    return;

                case Keys.D1:
                case Keys.NumPad1:
                    p_CurrCharaterInput = (isShiftDown && key == Keys.D1) ? '!' : '1';
                    return;

                case Keys.OemQuestion:
                    p_CurrCharaterInput = isShiftDown ? '?' : '/';
                    return;

                case Keys.OemQuotes:
                    p_CurrCharaterInput = isShiftDown ? '@' : '\'';
                    return;

                case Keys.OemSemicolon:
                    p_CurrCharaterInput = isShiftDown ? ':' : ';';
                    return;

                case Keys.OemTilde:
                    p_CurrCharaterInput = isShiftDown ? '~' : '`';
                    return;
                    
                case Keys.OemOpenBrackets:
                    p_CurrCharaterInput = isShiftDown ? '{' : '[';
                    return;

                case Keys.OemCloseBrackets:
                    p_CurrCharaterInput = isShiftDown ? '}' : ']';
                    return;

                case Keys.OemPlus:
                case Keys.Add:
                    p_CurrCharaterInput = (isShiftDown || key == Keys.Add) ? '+' : '=';
                    return;

                case Keys.OemMinus:
                case Keys.Subtract:
                    p_CurrCharaterInput = isShiftDown ? '_' : '-';
                    return;

                case Keys.OemPeriod:
                case Keys.Decimal:
                    p_CurrCharaterInput = isShiftDown ? '>' : '.';
                    return;

                case Keys.Divide:
                    p_CurrCharaterInput = isShiftDown ? '?' : '/';
                    return;

                case Keys.Multiply:
                    p_CurrCharaterInput = '*';
                    return;

                case Keys.OemBackslash:
                    p_CurrCharaterInput = isShiftDown ? '|' : '\\';
                    return;

                case Keys.OemComma:
                    p_CurrCharaterInput = isShiftDown ? '<' : ',';
                    return;

                case Keys.Tab:
                    p_CurrCharaterInput = ' ';
                    return;

                default:
                    p_currCharaterInputKey = prevKey;
                    break;
            }

            string LastCharPressedStr = key.ToString();

            if(LastCharPressedStr.Length > 1)
            {
                return;
            }

            p_currCharaterInputKey = key;

            bool capsLock = p_capLock;
            if(isShiftDown)
            {
                capsLock = !capsLock;
            }

            p_CurrCharaterInput = (capsLock ? LastCharPressedStr.ToUpper() : LastCharPressedStr.ToLower())[0];
        }

        public static string GetTextInput(string txt, ref int pos)
        {
            if (!p_newKeyIsPressed && p_KeyboardInputCooldown > 0f)
            {
                return txt;
            }

            if (p_CurrCharaterInput == (char)SpecialChars.Null)
            {
                return txt;
            }

            if (pos == -2)
            {
                pos = txt.Length;
            }

            if (pos < 0)
            {
                pos = 0;
            }

            switch (p_CurrCharaterInput)
            {
                case (char)SpecialChars.ArrowLeft:
                    if (--pos < 0) { pos = 0; }
                    return txt;
                case (char)SpecialChars.ArrowRight:
                    if (++pos > txt.Length) { pos = txt.Length; }
                    return txt;
                case (char)SpecialChars.Backspace:
                    pos--;
                    if (pos < txt.Length && pos >= 0 && txt.Length > 0) { return txt.Remove(pos, 1); } else { pos++;  return txt; }
                case (char)SpecialChars.Delete:
                    return (pos < txt.Length && txt.Length > 0) ? txt.Remove(pos, 1) : txt;
            }

            return txt.Insert(pos++, p_CurrCharaterInput.ToString());
        }   
    }

    [XmlType("Action")]
    public class ActionMap
    {
        [XmlElement("ActionName")]
        public string ActionName;

        //[XmlArray("KeyboardBinds")]
        //[XmlArrayItem("Bind")]
        //public List<GamePadButtons> gamePadBinds = new List<GamePadButtons>();

        [XmlArray("KeyboardBinds")]
        [XmlArrayItem("Bind")]
        public List<Keys> keyBroadBinds = new List<Keys>();
    }
}
