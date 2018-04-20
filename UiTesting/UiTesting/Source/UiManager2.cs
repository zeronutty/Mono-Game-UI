using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UiTesting.Source.Content;
using UiTesting.Source.Input;
using UiTesting.Source.UI.Components;
using UiTesting.Source.UI.Rendering;

[assembly: CLSCompliant(false)]

namespace UiTesting.Source
{
    public class UiManager2 : DrawableGameComponent
    {
        #region Structs
        private struct UiEntityStates
        {
            public Entity[] Entities;
            public int Click;
            public Entity Over;
        }
        #endregion

        #region Const

        internal const int _MenuDelay = 500;
        internal const int _ToolTipDelay = 500;
        internal const int _DoubleClickTime = 500;
        internal const int _TextureResizeIncrement = 32;
        internal const RenderTargetUsage _RenderTargetUsage = RenderTargetUsage.DiscardContents;

        #endregion

        #region Fields

        private bool p_deviceReset = false;
        private bool p_RenderTargetValid = false;
        private RenderTarget2D p_RenderTarget = null;
        private int p_TargetFrames = 60;
        private long p_DrawTime = 0;
        private long p_UpdateTime = 0;
        private GraphicsDeviceManager p_Graphics;
        private ArchiveManager p_Content = null;
        private UiRenderer p_Renderer = null;
        private InputSystem p_Input = null;
        private bool p_InputEnabled = true;
        private List<Component> p_Components = null;
        private List<Entity> p_Entities = null;
        private List<Entity> p_OrderList = null;
        private Entity p_FocusedEntity = null;
        //private ModalContainer p_ModalWindow = null;
        private float p_GlobalDepth = 0.0f;
        private int p_ToolTipDelay = _ToolTipDelay;
        private bool p_ToolTipsEnabled = true;
        private int p_MenuDelay = _MenuDelay;
        private int p_DoubleClickTime = _DoubleClickTime;
        private int p_TextureResizeIncrement = _TextureResizeIncrement;
        private bool p_LogUnHandledExceptions = true;
        private UiEntityStates p_States = new UiEntityStates();
        //private KeybroadLayout p_KeybroadLayout = new KeybroadLayout();
        //private List<KeybroadLayout> p_KeyBroadLayouts = new List<KeybroadLayouts>();
        private bool p_Disposing = false;
        private bool p_UseGuide = false;
        private bool p_AutoUnFocus = false;
        private bool p_AutoCreateRenderTarget = true;
        //private Cursor p_Cursor = null;
        private bool p_SofwareCursor = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a Value Indicating wether the UiManager is in the process of disposing (being deleted)
        /// </summary>
        public virtual bool Disposing { get { return p_Disposing; } }

        /// <summary>
        /// Get or Sets an application Cursor.
        /// </summary>
        //public Cursor Cursor { get { return p_Cursor; } set { p_Cursor = value; } }

        /// <summary>
        /// Should the software cursor be drawn and used, quick and dirty way of displing cursor
        /// </summary>
        public bool ShowSoftwareCursor { get { return p_SofwareCursor; } set { p_SofwareCursor = value; } }

        /// <summary>
        /// Returns associated <see cref="Game"/> Component.
        /// </summary>
        public virtual new Game Game { get { return base.Game; } }

        /// <summary>
        /// Returns associated <see cref="GraphicsDevice"/> Component.
        /// </summary>
        public virtual new GraphicsDevice GraphicsDevice { get { return base.GraphicsDevice; } }

        /// <summary>
        /// Returns accociated <see cref="GraphicsDeviceManager"/> Component.
        /// </summary>
        public virtual GraphicsDeviceManager Graphics { get { return p_Graphics; } }

        /// <summary>
        /// Returns <see cref="UiRenderer"/> used for Rendering ui.
        /// </summary>
        public virtual UiRenderer Renderer { get { return p_Renderer; } }

        /// <summary>
        /// Returns <see cref="ArchiceManager"/> used ror loading assets.
        /// </summary>
        public virtual ArchiveManager Content { get { return p_Content; } }

        /// <summary>
        /// Returns List of Components added tot eh manager
        /// </summary>
        public virtual List<Component> Components { get { return p_Components; } }

        public virtual List<Entity> Entites { get { return p_Entities; } }

        public virtual float GlobalDepth { get { return p_GlobalDepth; } set { p_GlobalDepth = value; } }

        public virtual int ToolTipDelay { get { return p_ToolTipDelay; } set { p_ToolTipDelay = value; } }

        public virtual int MenuDelay { get { return p_MenuDelay; } set { p_MenuDelay = value; } }

        public virtual int DoubleClickTime { get { return p_DoubleClickTime; } set { p_DoubleClickTime = value; } }

        public virtual int TextureResizeIncrement { get { return p_TextureResizeIncrement; } set { p_TextureResizeIncrement = value; } }

        public virtual bool ToolTipsEnabled { get { return p_ToolTipsEnabled; } set { p_ToolTipsEnabled = value; } }

        public virtual bool LogUnhandledExceptions { get { return p_LogUnHandledExceptions; } set { p_LogUnHandledExceptions = value; } }

        public virtual bool InputEnabled { get { return p_InputEnabled; } set { p_InputEnabled = value; } }

        public virtual RenderTarget2D RenderTarget { get { return p_RenderTarget; } set { p_RenderTarget = value; } }

        public virtual int TargetFrames { get { return p_TargetFrames; } set { p_TargetFrames = value; } }

        //public virtual List<KeybroadLayout> KeybroadLayouts { get { return p_KeybroadLayouts; } set { p_KeybroadLayouts = value; } }

        public bool UseGuide { get { return p_UseGuide; } set { p_UseGuide = value; } }

        public virtual bool AutoUnFocus { get { return p_AutoUnFocus; } set { p_AutoUnFocus = value; } }

        public virtual bool AutoCreateRenderTarget { get { return p_AutoCreateRenderTarget; } set { p_AutoCreateRenderTarget = value; } }

        //public virtual KeybroadLayout KeybroadLayout
        //{
        //    get
        //    {
        //        if(p_KeybroadLayout == null)
        //        {
        //            p_KeybroadLayout = new KeybroadLayout();
        //        }
        //        return p_KeybroadLayout;
        //    }
        //    set
        //    {
        //        p_KeybroadLayout = value;
        //    }
        //}

        public virtual int TargetWidth
        {
            get
            {
                if (p_RenderTarget != null)
                {
                    return p_RenderTarget.Width;
                }
                else return ScreenWidth;
            }
        }

        public virtual int TargetHeight
        {
            get
            {
                if (p_RenderTarget != null)
                {
                    return p_RenderTarget.Height;
                }
                else return ScreenHeight;
            }
        }

        public virtual int ScreenWidth
        {
            get
            {
                if (GraphicsDevice != null)
                {
                    return GraphicsDevice.PresentationParameters.BackBufferWidth;
                }
                else return 0;
            }
        }

        public virtual int ScreenHeight
        {
            get
            {
                if (GraphicsDevice != null)
                {
                    return GraphicsDevice.PresentationParameters.BackBufferHeight;
                }
                else return 0;
            }
        }

        //public virtual ModalContainer ModalWindow
        //{
        //    get
        //    {
        //        return p_ModalWindow;
        //    }
        //    internal set
        //    {
        //        p_ModalWindow = value;

        //        if(value != null)
        //        {
        //            value.ModalResult = ModalResult.None;

        //            value.Visible = true;
        //            value.Focused = true;
        //        }
        //    }
        //}

        public virtual Entity FocusedEntity
        {
            get
            {
                return p_FocusedEntity;
            }
            internal set
            {
                if (value != null && value.Visible && value.Enabled)
                {
                    if (value != null && value.CanFocus)
                    {
                        if (p_FocusedEntity == null || (p_FocusedEntity != null && value.Root != p_FocusedEntity.Root) || !value.IsRoot)
                        {
                            if (p_FocusedEntity != null && p_FocusedEntity != value)
                            {
                                p_FocusedEntity.Focused = false;
                            }
                            p_FocusedEntity = value;
                        }
                    }
                    else if (value != null && !value.CanFocus)
                    {
                        if (p_FocusedEntity != null && value.Root != p_FocusedEntity.Root)
                        {
                            if (p_FocusedEntity != value.Root)
                            {
                                p_FocusedEntity.Focused = false;
                            }
                            p_FocusedEntity = value.Root;
                        }
                        else if (p_FocusedEntity == null)
                        {
                            p_FocusedEntity = value.Root;
                        }
                    }
                    BringToFront(value.Root);
                }
                else if (value == null)
                {
                    p_FocusedEntity = value;
                }

            }
        }

        internal virtual List<Entity> OrderList { get { return p_OrderList; } }

        #endregion

        #region Events

        //public event DeviceEventHandler DeviceSettingsChanged;

        //public event WindowClosingEventHandler WindowClosing;

        #endregion

        #region Constructors

        public UiManager2(Game game, GraphicsDeviceManager graphics) : base(game)
        {
            p_Disposing = false;

            p_Content = new ArchiveManager(game.Services);
            p_Input = new InputSystem(this, new InputOffset(0, 0, 1f, 1f));
            p_Components = new List<Component>();
            //p_Controls = new List<Controls>();
            p_OrderList = new List<Entity>();

            this.p_Graphics = graphics;

            p_States.Click = -1;
            p_States.Over = null;

            //p_Input.MouseDown += new MouseEventHandler(MouseDownProcess);
            //p_Input.MouseUp += new MouseEventHandler(MouseUpProcess);
            //p_Input.MousePress += new MouseEventHandler(MousePressProcess);
            //p_Input.MouseMove += new MouseEventHandler(MouseMoveProcess);
            //p_Input.MouseScroll += new MouseEventHandler(MouseScrollProcess);

            //p_Input.GamePadDown += new GamePadEventHandler(GamePadDownProcess);
            //p_Input.GamePadUp += new GamePadEventHandler(GamePadUpProcess);
            //p_Input.GamePadPress += new GamePadEventHandler(GamePadPressProcess);

            //p_Input.keyDown += new KeyEventHandler(KeyDownProcess);
            //p_Input.keyUp += new KeyEventHandler(KeyUpProcess);
            //p_Input.keyPress += new KeyEventHandler(KeyPressProcess);

            //keyboardLayouts.Add(new KeyboardLayout());
            //keyboardLayouts.Add(new CzechKeyboardLayout());
            //keyboardLayouts.Add(new GermanKeyboardLayout());
        }

        public UiManager2(Game game) : this(game, game.Services.GetService(typeof(IGraphicsDeviceManager)) as GraphicsDeviceManager)
        {

        }
        #endregion

        #region Destructors

        protected override void Dispose(bool disposing)
        {
            if (p_Disposing)
            {
                this.p_Disposing = true;

                //if(p_Controls != null)
                //{
                //    int c = p_Controls.Count;
                //    for(int i = 0; i < c; i++)
                //    {
                //        if (p_Controls.Count > 0) p_Controls[0].Dispose();
                //    }
                //}
            }

            if(p_Components != null)
            {
                int c = p_Components.Count;
                for(int i = 0; i < c; i++)
                {
                    if (p_Components.Count > 0) p_Components[0].Dispose();
                }
            }

            if(p_Content != null)
            {
                p_Content.Unload();
                p_Content.Dispose();
                p_Content = null;
            }

            if(p_Renderer != null)
            {
                p_Renderer.Dispose();
                p_Renderer = null;
            }

            if(p_Input != null)
            {
                p_Input.Dispose();
                p_Input = null;
            }

            if(GraphicsDevice != null)
            {
                //GraphicsDevice.DeviceReset -= new System.EventHandler<System.EventArgs>(GraphicsDevice_DeviceReset);
            }

            base.Dispose(disposing);
        }

        #endregion

    }
}
