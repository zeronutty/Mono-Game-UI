using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using UiTesting.Source.Input;
using UiTesting.Source.Forms;
using UiTesting.Source.Logging;
using Microsoft.Xna.Framework.Input;
using UiTesting.Source.Event;
using UiTesting.Source.Data;
using System.Media;

namespace UiTesting.Source
{
    public enum CursorType
    {
        Default,
        SizeLeft,
        SizeUp,
        SizeLeftDi,
        SizeRightDi,
        Dragging
    }

    public delegate void EventCallback(Entity entity);

    public delegate Entity GenerateTooltipFunc(Entity entity);

    public delegate Paragraph DefaultParagraphGenerator(string text, Anchor anchor, Color? color = null, float? scale = null, Vector2? size = null, Vector2? offset = null, Vector2? padding = null);

    public class UiManager
    {
        private struct ControlStates
        {
            public Entity[] Buttons;
            public int Click;
            public Entity Over;
        }

        #region Singleton
        private static UiManager p_Active;
        #endregion

        #region Fields

        private ContentManager p_ContentManager;
        private GraphicsDeviceManager p_GraphicsDeviceManager;
        private FPSCounter fps;
        public DrawUtils drawUtils = null;
        public DebugUtils debugUtils = null;

        private int p_ScreenWidth;
        private int p_ScreenHeight;

        private Texture2D p_CursorTexture;
        private Texture2D p_Cursor;
        private Texture2D[] p_Cursors = new Texture2D[6];
        private int p_CursorWidth = 32;
        private Point p_CursorOffset = Point.Zero;

        public bool ShowCursor = true;

        public float CursorScale = 1f;

        private Entity p_TargetEntity;
        private Entity p_DragTargetEntity;
        public Entity ActiveEntity = null;

        private bool p_wasEventHandled;

        private float p_GlobalScale = 1f;

        public bool DebugMode = false;

        public PropertiesPanel p_testform;

        private Game p_Game;

        private RenderTarget2D p_renderTarget = null;
        private bool p_UseRenderTarget = true;
        public Matrix? RendertargetTranformMatrix = null;
        public bool IncludeCursorInRenderTarget = true;

        public BlendState BlendState = BlendState.AlphaBlend;
        public SamplerState SamplerState = SamplerState.PointClamp;
        string UpdatesPreSecound;
        GameTime p_gameTime;

        public static int NumberOfUIEntities = 0;

        private ConsoleTextWriter ConsoleTextWriter;

        private InputSystem p_Input = null;
        private bool p_InputEnabled = true;

        private List<Entity> p_UiEntities = new List<Entity>();
        private List<Entity> p_OrderList = new List<Entity>();

        private Entity p_FocusedEntity = null;

        private ControlStates p_States = new ControlStates();

        private bool p_AutoUnFocus = true;
        #endregion

        #region Properties
        public RootPanel Root { get; set; }

        public ConsoleWindow ConsoleWindow { get; set; }

        public int ScreenWidth { get { return p_ScreenWidth; } set { p_ScreenWidth = value; } }
        public int ScreenHeight { get { return p_ScreenHeight; } set { p_ScreenHeight = value; } }

        public Entity TargetEntity { get { return p_TargetEntity; } set { p_TargetEntity = value; } }
        public Entity DragTargetEntity { get { return p_DragTargetEntity; } set { p_DragTargetEntity = value; } }

        public bool WasEventHandled { get { return p_wasEventHandled; } set { p_wasEventHandled = value; } }

        public float GlobalScale { get { return p_GlobalScale; } set { p_GlobalScale = value; } }

        public Text MouseX;
        public Text MouseY;

        public Text TargetText;
        public Text DragTargetText;

        public PropertiesPanel DebugPanel { get { return p_testform; } }

        public bool UseRenderTarget { get { return p_UseRenderTarget; } set { p_UseRenderTarget = value; DisposeRenderTarget(); } }

        public RenderTarget2D RenderTarget { get { return p_renderTarget; } }

        public InputSystem Input { get { return p_Input; } set { p_Input = value; } }

        public bool InputEnabled { get { return p_InputEnabled; } set { p_InputEnabled = value; } }

        public List<Entity> UiEntities { get { return p_UiEntities; } }

        public List<Entity> OrderList { get { return p_OrderList; } }

        public Entity FocusedControl
        {
            get { return p_FocusedEntity; }
            internal set
            {
                if (value != null && value.Visible && !value.IsLocked())
                {
                    if (value != null && value.CanFocus)
                    {
                        if (p_FocusedEntity == null || (p_FocusedEntity != null && value.Root != p_FocusedEntity.Root) || !value.IsRoot)
                        {
                            if (p_FocusedEntity != null && p_FocusedEntity != value)
                            {
                                p_FocusedEntity.IsFocused = false;
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
                                p_FocusedEntity.IsFocused = false;
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
        #endregion

        #region Events

        public EventCallback OnMouseDown = null;
        public EventCallback OnMouseReleased = null;
        public EventCallback WhileMouseDown = null;
        public EventCallback WhileMouseHover = null;
        public EventCallback OnClick = null;
        public EventCallback OnValueChange = null;
        public EventCallback OnMouseEnter = null;
        public EventCallback OnMouseLeave = null;
        public EventCallback OnMouseWheelScroll = null;
        public EventCallback OnStartDrag = null;
        public EventCallback OnStopDrag = null;
        public EventCallback WhileDragging = null;
        public EventCallback BeforeUpdate = null;
        public EventCallback AfterUpdate = null;
        public EventCallback BeforeDraw = null;
        public EventCallback AfterDraw = null;
        public EventCallback OnVisiblityChange = null;
        public EventCallback OnEntitySpawn = null;
        public EventCallback OnFocusChange = null;

        static public DefaultParagraphGenerator DefaultParagraph =
            (string text, Anchor anchor, Color? color, float? scale, Vector2? size, Vector2? offset, Vector2? padding) =>
            {
                if (color != null)
                {
                    return new Paragraph(new ParagraphProps
                    {
                        Text = text,
                        EntityAnchor = anchor,
                        OverlayColor = color ?? Color.White,
                        Size = size ?? Vector2.Zero,
                        LocalPosition = offset ?? Vector2.Zero,
                        Padding = padding ?? new Vector2(10, 10),
                        WrapWords = true,
                        BreakWords = true,
                        AddHypen = false,
                        HighlightColor = Color.Transparent,
                    });
                }

                return new Paragraph(new ParagraphProps
                {
                    Text = text,
                    EntityAnchor = anchor,
                    Size = size ?? Vector2.Zero,
                    LocalPosition = offset ?? Vector2.Zero,
                    Padding = padding ?? new Vector2(10, 10),
                    WrapWords = true,
                    BreakWords = true,
                    AddHypen = false,
                    HighlightColor = Color.Transparent,
                });
            };
        #endregion

        #region Method
        public static UiManager GetActiveUserInterface()
        {
            if (p_Active == null)
            {
                p_Active = new UiManager();
            }

            return p_Active;

        }

        public UiManager()
        {

        }
    
        public UiManager(ContentManager content)
        {
            p_ContentManager = content;
        }

        public void Initialize(ContentManager content, GraphicsDeviceManager graphicsDeviceManager, Game game)
        {
            p_ContentManager = content;
            p_GraphicsDeviceManager = graphicsDeviceManager;
            InputUtils.Initialize();
            fps = new FPSCounter();
            
            p_Game = game;

            p_ScreenHeight = p_GraphicsDeviceManager.PreferredBackBufferHeight;
            p_ScreenWidth = p_GraphicsDeviceManager.PreferredBackBufferWidth;             
        }

        public void LoadContent()
        {
            Debuging.UITesting.AddUI(this);

            Color color = Color.Black;
            color *= 0.25f;

            ConsoleWindow = new ConsoleWindow(new PanelProps { Size = new Vector2(0, p_ScreenHeight / 3), EntityAnchor = Anchor.TopLeft, Backgroundtexture = SpritesData.B_BGSprite, OverlayColor = color });

            Root.AddChild(ConsoleWindow);

            ConsoleTextWriter = new ConsoleTextWriter();

            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");
            LogHelper.Log(LogTarget.Console, LogLevel.Info, this + ": Initialize");

            drawUtils = new DrawUtils();
            debugUtils = new DebugUtils();

            p_testform = new PropertiesPanel();

            p_testform.Init();

            p_testform.FormClosed += P_testform_FormClosed;

            p_Cursors[0] = p_ContentManager.Load<Texture2D>("Textures/Cursors/Default_Cursor");
            p_Cursors[1] = p_ContentManager.Load<Texture2D>("Textures/Cursors/Default_S_BL");
            p_Cursors[2] = p_ContentManager.Load<Texture2D>("Textures/Cursors/Default_S_BR");
            p_Cursors[3] = p_ContentManager.Load<Texture2D>("Textures/Cursors/Default_S_Horz");
            p_Cursors[4] = p_ContentManager.Load<Texture2D>("Textures/Cursors/Default_S_Vert");
            p_Cursors[5] = p_ContentManager.Load<Texture2D>("Textures/Cursors/Default_S_Dragging");

            p_CursorTexture = p_Cursors[0];

            p_Cursor = p_Cursors[0];

            p_testform.Hide();          
        }

        private void P_testform_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            DebugMode = false;
        }

        public void SetCursor(CursorType cursorType)
        {
            switch (cursorType)
            {
                case CursorType.Default:
                    p_Cursor = p_Cursors[0];
                    break;
                case CursorType.SizeLeft:
                    p_Cursor = p_Cursors[3];
                    break;
                case CursorType.SizeUp:
                    p_Cursor = p_Cursors[4];
                    break;
                case CursorType.SizeLeftDi:
                    p_Cursor = p_Cursors[1];
                    break;
                case CursorType.SizeRightDi:
                    p_Cursor = p_Cursors[2];
                    break;
                case CursorType.Dragging:
                    p_Cursor = p_Cursors[5];
                    break;
            }
        }

        public void SetCursor(Texture2D texture, int drawWidth = 32, Point? offset = null)
        {
            p_CursorTexture = texture;
            p_CursorWidth = drawWidth;
            p_CursorOffset = offset ?? Point.Zero;
        }

        public void AddChildToRoot(Entity child)
        {
            Root.AddChild(child);
        }

        public void RemoveChildFromRoot(Entity child)
        {
            Root.RemoveChild(child);
        }

        public virtual void AddEntity(Entity entity)
        {
            if(entity != null)
            {
                UiEntities.Add(entity);
                entity.UiManager = this;
                entity.Parent = null;
                if (FocusedControl == null) entity.IsFocused = true;
            }
        }

        public virtual void RemoveEntity(Entity entity)
        {
            if(entity != null)
            {
                if (entity.IsFocused) entity.IsFocused = false;
                UiEntities.Remove(entity);
            }
        }

        public virtual void BringToFront(Entity entity)
        {
            if (entity != null && !entity.StayOnBack)
            {
                List<Entity> list = (entity.Parent == null) ? UiEntities : entity.Parent.Children;
                if(list.Contains(entity))
                {
                    list.Remove(entity);
                    if(!entity.StayOnTop)
                    {
                        int pos = list.Count;
                        for(int i = list.Count - 1; i >= 0; i--)
                        {
                            if(!list[i].StayOnTop)
                            {
                                break;
                            }
                            pos = i;
                        }
                        list.Insert(pos, entity);
                    }
                    else
                    {
                        list.Add(entity);
                    }
                }
            }
        }

        public virtual void SendToBack(Entity entity)
        {
            if (entity != null && !entity.StayOnTop)
            {
                List<Entity> list = (entity.Parent == null) ? UiEntities : entity.Parent.Children;
                if (list.Contains(entity))
                {
                    list.Remove(entity);
                    if (!entity.StayOnBack)
                    {
                        int pos = list.Count;
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (!list[i].StayOnBack)
                            {
                                break;
                            }
                            pos = i;
                        }
                        list.Insert(pos, entity);
                    }
                    else
                    {
                        list.Insert(0, entity);
                    }
                }
            }
        }

        public void OnWindowResize()
        {

        }

        public void Update(GameTime gameTime)
        {
            InputUtils.Update(gameTime);

            if (InputUtils.IsActionTriggered("Open Console"))
            {
                ConsoleWindow.Visible = !ConsoleWindow.Visible;
            }

            p_gameTime = gameTime;

            string FPS = string.Format("UpdatesPS: {0}", (int)fps.AverageFramesPreSecond);

            SetCursor(CursorType.Default);

            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            if (p_DragTargetEntity != null && !InputUtils.IsMouseButtonLeftPressed())
            {
                p_DragTargetEntity = null;
            }

            Entity target = null;
            bool wasEventHandled = false;
            Root.Update(ref target, ref p_DragTargetEntity, ref wasEventHandled, gameTime, Point.Zero);

            if(InputUtils.IsMouseButtonLeftPressed())
            {
                ActiveEntity = target;
            }

            p_testform.SetMousePosText(InputUtils.GetMousePosition().X.ToString(), InputUtils.GetMousePosition().Y.ToString());

            p_testform.SetEntityTargets(target, p_DragTargetEntity);

            ActiveEntity = ActiveEntity ?? Root;

            p_TargetEntity = target;
            WasEventHandled = wasEventHandled;

            fps.Update(deltaTime);
            UpdatesPreSecound = string.Format("UpdatesPS: {0}", (int)fps.AverageFramesPreSecond);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            fps.Update(p_gameTime.ElapsedGameTime.TotalSeconds);

            int newScreenWidth = spriteBatch.GraphicsDevice.Viewport.Width;
            int newScreenHeight = spriteBatch.GraphicsDevice.Viewport.Height;

            if (ScreenWidth != newScreenWidth || ScreenHeight != newScreenHeight)
            {
                ScreenWidth = newScreenWidth;
                ScreenHeight = newScreenHeight;
                Root.MarkasRectUpdate();
            }

            if (UseRenderTarget)
            {
                if (p_renderTarget == null || p_renderTarget.Width != ScreenWidth || p_renderTarget.Height != ScreenHeight)
                {
                    DisposeRenderTarget();
                    p_renderTarget = new RenderTarget2D(spriteBatch.GraphicsDevice,
                        ScreenWidth, ScreenHeight, false,
                        spriteBatch.GraphicsDevice.PresentationParameters.BackBufferFormat,
                        spriteBatch.GraphicsDevice.PresentationParameters.DepthStencilFormat, 0,
                        RenderTargetUsage.PreserveContents);
                }
                else
                {
                    spriteBatch.GraphicsDevice.SetRenderTarget(p_renderTarget);
                    spriteBatch.GraphicsDevice.Clear(Color.Transparent);
                }
            }

            Root.Draw(spriteBatch);

            if (ShowCursor && (IncludeCursorInRenderTarget || !UseRenderTarget))
            {
                DrawCursor(spriteBatch);
            }

            string FPS = string.Format("DrawsPS: {0}", (int)fps.AverageFramesPreSecond);

            spriteBatch.Begin();
            spriteBatch.DrawString(ContentLoader.GetFontByName("NullFont"), FPS, new Vector2(ScreenWidth - ContentLoader.GetFontByName("NullFont").MeasureString("DrawsPS: 0000").X, 1), Color.Black);
            spriteBatch.DrawString(ContentLoader.GetFontByName("NullFont"), UpdatesPreSecound, new Vector2(ScreenWidth - ContentLoader.GetFontByName("NullFont").MeasureString("UpdatesPS: 0000").X, ContentLoader.GetFontByName("NullFont").MeasureString("UpdatesPS: 0000").Y + 1), Color.Black);
            spriteBatch.End();

            debugUtils.BeforeDrawDebugs(spriteBatch);
            spriteBatch.Begin();
            debugUtils.DrawDebugs(spriteBatch);
            spriteBatch.End();

            if (UseRenderTarget)
            {
                spriteBatch.GraphicsDevice.SetRenderTarget(null);
            }
        }

        public void DrawCursor(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState, SamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise);

            float cursorSize = CursorScale * GlobalScale * ((float)p_CursorWidth / (float)p_CursorTexture.Width);

            Vector2 cursorPos = InputUtils.GetMousePosition();

            spriteBatch.Draw(p_CursorTexture,
                new Rectangle(
                    (int)(cursorPos.X + p_CursorOffset.X + cursorSize), (int)(cursorPos.Y + p_CursorOffset.Y + cursorSize),
                    (int)(p_CursorTexture.Width * cursorSize), (int)(p_CursorTexture.Height * cursorSize)),
            Color.White);

            spriteBatch.End();
        }

        public void DrawMainRenderTarget(SpriteBatch spriteBatch)
        {
            if (RenderTarget != null && !RenderTarget.IsDisposed)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: RendertargetTranformMatrix);
                spriteBatch.Draw(RenderTarget, new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
                spriteBatch.End();

                debugUtils.AfterDrawDebugs(spriteBatch);
            }

            if (!IncludeCursorInRenderTarget)
            {
                DrawCursor(spriteBatch);
            }

        
        }

        public Vector2 GetTransFormedCursorPos(Vector2? addVector)
        {
            addVector = addVector ?? Vector2.Zero;

            if(UseRenderTarget && RendertargetTranformMatrix != null && !IncludeCursorInRenderTarget)
            {
                var matrix = Matrix.Invert(RendertargetTranformMatrix.Value);
                return InputUtils.TransformCursorPos(matrix) + Vector2.Transform(addVector.Value, matrix);
            }

            return InputUtils.GetMousePosition() + addVector.Value;
        }

        public void OpenClosePropPanel(bool IsOpen)
        {
            if(IsOpen == true)
            {
                p_testform.Hide();
            }
            else
            {
                p_testform.Show();
            }
        }

        private void DisposeRenderTarget()
        {
            if(p_renderTarget != null)
            {
                p_renderTarget.Dispose();
                p_renderTarget = null;
            }
        }

        public void Dispose()
        {
            DisposeRenderTarget();
        }

        #region Input

        private bool CheckOrder(Entity entity, Point pos)
        {
            if (!CheckPosition(entity, pos)) return false;

            for(int i = OrderList.Count - 1; i > OrderList.IndexOf(entity); i--)
            {
                Entity en = OrderList[i];

                if(CheckPosition(en, pos) && CheckParent(en, pos))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckState(Entity entity)
        {
            return (entity != null && entity.IsVisible() && Root != null);
        }

        private bool CheckParent(Entity entity, Point pos)
        {
            if(entity.Parent != null)
            {
                Entity parent = entity.Parent;
                Entity root = entity.Root;

                Rectangle pr = new Rectangle(parent.p_DrawArea.Left,
                                             parent.p_DrawArea.Top,
                                             parent.p_DrawArea.Width,
                                             parent.p_DrawArea.Height);

                Rectangle rr = new Rectangle(root.p_DrawArea.Left,
                                             root.p_DrawArea.Top,
                                             root.p_DrawArea.Width,
                                             root.p_DrawArea.Height);

                return (rr.Contains(pos) && pr.Contains(pos));
            }

            return true;
        }

        private bool CheckPosition(Entity entity, Point pos)
        {
            return (entity.p_DrawArea.Left <= pos.X &&
                    entity.p_DrawArea.Top <= pos.Y &&
                    entity.p_DrawArea.Left + entity.p_DrawArea.Width >= pos.X &&
                    entity.p_DrawArea.Top + entity.p_DrawArea.Height >= pos.Y &&
                    CheckParent(entity, pos));
        }

        private bool CheckButtons(int index)
        {
            for(int i = 0; i < p_States.Buttons.Length; i++)
            {
                if (i == index) continue;
                if (p_States.Buttons[i] != null) return false;
            }

            return true;
        }

        private void TabNextEntity(Entity entity)
        {
            int start = OrderList.IndexOf(entity);
            int i = start;

            do
            {
                if (i < OrderList.Count - 1) i += 1;
                else i = 0;
            }
            while ((OrderList[i].Root != entity.Root || !OrderList[i].CanFocus || OrderList[i].IsRoot) && i != start);
            OrderList[i].IsFocused = true;
        }

        private void TabPrevEntity(Entity entity)
        {

            int start = OrderList.IndexOf(entity);
            int i = start;

            do
            {
                if (i > 0) i -= 1;
                else i = OrderList.Count - 1;
            }
            while ((OrderList[i].Root != entity.Root || !OrderList[i].CanFocus || OrderList[i].IsRoot) && i != start);
            OrderList[i].IsFocused = true;
        }

        private void ProcessArrows(Entity entity, KeyEventArgs kbe, GamePadEventArgs gpe)
        {
            Entity c = entity;
            if (c.Parent != null && c.Parent.Children != null)
            {
                int index = -1;

                if ((kbe.Key == Keys.Left && !kbe.Handled) ||
                    (gpe.Button == c.GamePadActions.Left && !gpe.Handled))
                {
                    int miny = int.MaxValue;
                    int minx = int.MinValue;
                    for (int i = 0; i < (c.Parent.Children).Count; i++)
                    {
                        Entity cx = (c.Parent.Children)[i];
                        if (cx == c || !cx.Visible || !cx.Enabled || cx.Passive || !cx.CanFocus) continue;

                        int cay = (int)(c.Top + (c.Height / 2));
                        int cby = (int)(cx.Top + (cx.Height / 2));

                        if (Math.Abs(cay - cby) <= miny && (cx.Left + cx.Width) >= minx && (cx.Left + cx.Width) <= c.Left)
                        {
                            miny = Math.Abs(cay - cby);
                            minx = cx.Left + cx.Width;
                            index = i;
                        }
                    }
                }
                else if ((kbe.Key == Keys.Right && !kbe.Handled) ||
                         (gpe.Button == c.GamePadActions.Right && !gpe.Handled))
                {
                    int miny = int.MaxValue;
                    int minx = int.MaxValue;
                    for (int i = 0; i < (c.Parent.Children).Count; i++)
                    {
                        Entity cx = (c.Parent.Children)[i];
                        if (cx == c || !cx.Visible || !cx.Enabled || cx.Passive || !cx.CanFocus) continue;

                        int cay = (int)(c.Top + (c.Height / 2));
                        int cby = (int)(cx.Top + (cx.Height / 2));

                        if (Math.Abs(cay - cby) <= miny && cx.Left <= minx && cx.Left >= (c.Left + c.Width))
                        {
                            miny = Math.Abs(cay - cby);
                            minx = cx.Left;
                            index = i;
                        }
                    }
                }
                else if ((kbe.Key == Keys.Up && !kbe.Handled) ||
                         (gpe.Button == c.GamePadActions.Up && !gpe.Handled))
                {
                    int miny = int.MinValue;
                    int minx = int.MaxValue;
                    for (int i = 0; i < (c.Parent.Children).Count; i++)
                    {
                        Entity cx = (c.Parent.Children)[i];
                        if (cx == c || !cx.Visible || !cx.Enabled || cx.Passive || !cx.CanFocus) continue;

                        int cax = (int)(c.Left + (c.Width / 2));
                        int cbx = (int)(cx.Left + (cx.Width / 2));

                        if (Math.Abs(cax - cbx) <= minx && (cx.Top + cx.Height) >= miny && (cx.Top + cx.Height) <= c.Top)
                        {
                            minx = Math.Abs(cax - cbx);
                            miny = cx.Top + cx.Height;
                            index = i;
                        }
                    }
                }
                else if ((kbe.Key == Keys.Down && !kbe.Handled) ||
                         (gpe.Button == c.GamePadActions.Down && !gpe.Handled))
                {
                    int miny = int.MaxValue;
                    int minx = int.MaxValue;
                    for (int i = 0; i < (c.Parent.Children).Count; i++)
                    {
                        Entity cx = (c.Parent.Children)[i];
                        if (cx == c || !cx.Visible || !cx.Enabled || cx.Passive || !cx.CanFocus) continue;

                        int cax = (int)(c.Left + (c.Width / 2));
                        int cbx = (int)(cx.Left + (cx.Width / 2));

                        if (Math.Abs(cax - cbx) <= minx && cx.Top <= miny && cx.Top >= (c.Top + c.Height))
                        {
                            minx = Math.Abs(cax - cbx);
                            miny = cx.Top;
                            index = i;
                        }
                    }
                }

                if (index != -1)
                {
                    (c.Parent.Children)[index].IsFocused = true;
                    kbe.Handled = true;
                    gpe.Handled = true;
                }
            }
        }

        #region Mouse
        private void MouseDownProcess(object sender, MouseEventArgs e)
        {
            List<Entity> c = new List<Entity>();
            c.AddRange(OrderList);

            if(p_AutoUnFocus && p_FocusedEntity != null && p_FocusedEntity.Root != Root)
            {
                bool hit = false;

                foreach(Entity en in UiEntities)
                {
                    if(en.p_DrawArea.Contains(e.Position))
                    {
                        hit = true;
                        break;
                    }
                }
                if(!hit)
                {
                    for(int i = 0; i < Entity.Stack.Count; i++)
                    {
                        if(Entity.Stack[i].Visible && Entity.Stack[i].p_DrawArea.Contains(e.Position))
                        {
                            hit = true;
                            break;
                        }
                    }
                }
                if (!hit) p_FocusedEntity.IsFocused = false;
            }

            for(int i = c.Count - 1; i >= 0; i--)
            {
                if (CheckState(c[i]) && CheckPosition(c[i], e.Position))
                {
                    p_States.Buttons[(int)e.Button] = c[i];
                    c[i].SendMessage(Message.MouseDown, e);

                    if (p_States.Click == -1)
                    {
                        p_States.Click = (int)e.Button;

                        if (FocusedControl != null)
                        {
                            FocusedControl.MarkasRectUpdate();
                        }
                        c[i].IsFocused = true;
                    }
                    return;
                }
            }

            if(Root != null)
            {
                SystemSounds.Beep.Play();
            }
        }

        private void MouseUpProcess(object sender, MouseEventArgs e)
        {
            Entity en = p_States.Buttons[(int)e.Button];
            if(en != null)
            {
                if(CheckPosition(en,e.Position) && CheckOrder(en, e.Position) && p_States.Click == (int)e.Button && CheckButtons((int)e.Button))
                {
                    en.SendMessage(Message.Click, e);
                }
                p_States.Click = -1;
                en.SendMessage(Message.MouseUp, e);
                p_States.Buttons[(int)e.Button] = null;
                MouseMoveProcess(sender, e);
            }
        }

        private void MousePressProcess(object sender, MouseEventArgs e)
        {
            Entity en = p_States.Buttons[(int)e.Button];
            if (en != null)
            {
                if(CheckPosition(en, e.Position))
                {
                    en.SendMessage(Message.MousePress, e);
                }
            }
        }

        private void MouseMoveProcess(object sender, MouseEventArgs e)
        {
            List<Entity> list = new List<Entity>();
            list.AddRange(OrderList);

            for(int i = list.Count - 1; i >= 0; i--)
            {
                bool chpos = CheckPosition(list[i], e.Position);
                bool chsta = CheckState(list[i]);

                if(chsta && ((chpos && p_States.Over == list[i]) || (p_States.Buttons[(int)e.Button] == list[i])))
                {
                    list[i].SendMessage(Message.MouseMove, e);
                    break;
                }
            }

            for (int i = list.Count - 1; i >= 0; i--)
            {
                bool chpos = CheckPosition(list[i], e.Position);
                bool chsta = CheckState(list[i]);

                if (chsta && !chpos && p_States.Over == list[i] && p_States.Buttons[(int)e.Button] == null)
                {
                    p_States.Over = null;
                    list[i].SendMessage(Message.MouseOut, e);
                    break;
                }
            }

            for (int i = list.Count - 1; i >= 0; i--)
            {
                bool chpos = CheckPosition(list[i], e.Position);
                bool chsta = CheckState(list[i]);

                if (chsta && chpos && p_States.Over != list[i] && p_States.Buttons[(int)e.Button] == null)
                {
                    if (p_States.Over != null)
                    {
                        p_States.Over.SendMessage(Message.MouseOut, e);
                    }

                    p_States.Over = list[i];
                    list[i].SendMessage(Message.MouseOut, e);
                    break;
                }
                else if (p_States.Over == list[i]) break;
            }
        }

        private void MouseScrollProcess(object sender, MouseEventArgs e)
        {
            List<Entity> list = new List<Entity>();
            list.AddRange(OrderList);

            for(int i = list.Count - 1; i >= 0; i--)
            {
                bool chpos = CheckPosition(list[i], e.Position);
                bool chsta = CheckState(list[i]);

                if(chsta && chpos && p_States.Over == list[i])
                {
                    list[i].SendMessage(Message.MouseScroll, e);
                    break;
                }
            }
        }
        #endregion

        #region GamePad

        private void GamePadDownProcess(object sender, GamePadEventArgs e)
        {
            Entity en = FocusedControl;

            if(en != null && CheckState(en))
            {
                if(p_States.Click == -1)
                {
                    p_States.Click = (int)e.Button;
                }
                p_States.Buttons[(int)e.Button] = en;
                en.SendMessage(Message.GamePadDown, e);

                if(e.Button == en.GamePadActions.Click)
                {
                    en.SendMessage(Message.Click, new MouseEventArgs(new MouseState(), MouseButtons.None, Point.Zero));
                }
            }
        }

        private void GamePadUpProcess(object sender, GamePadEventArgs e)
        {
            Entity en = p_States.Buttons[(int)e.Button];

            if(en != null)
            {
                if(e.Button == en.GamePadActions.Press)
                {
                    en.SendMessage(Message.Click, new MouseEventArgs(new MouseState(), MouseButtons.None, Point.Zero));
                }
                p_States.Click = -1;
                p_States.Buttons[(int)e.Button] = null;
                en.SendMessage(Message.GamePadUp, e);
            }
        }

        private void GamePadPressProcess(object sender, GamePadEventArgs e)
        {
            Entity en = p_States.Buttons[(int)e.Button];
            if(en != null)
            {
                en.SendMessage(Message.GamePadPress, e);

                if ((e.Button == en.GamePadActions.Right ||
                   e.Button == en.GamePadActions.Left ||
                   e.Button == en.GamePadActions.Up ||
                   e.Button == en.GamePadActions.Down) && !e.Handled && CheckButtons((int)e.Button))
                {
                    ProcessArrows(en, new KeyEventArgs(), e);
                    GamePadDownProcess(sender, e);
                }
                else if(e.Button == en.GamePadActions.NextControl && !e.Handled && CheckButtons((int)e.Button))
                {
                    TabNextEntity(en);
                    GamePadDownProcess(sender, e);
                }
                else if(e.Button == en.GamePadActions.PrevControl && !e.Handled && CheckButtons((int)e.Button))
                {
                    TabPrevEntity(en);
                    GamePadDownProcess(sender, e);
                }
            }
        }

        #endregion

        #region Keybroad

        private void KeyDownProcess(object sender, KeyEventArgs e)
        {
            Entity en = FocusedControl;

            if(en != null && CheckState(en))
            {
                if(p_States.Click == -1)
                {
                    p_States.Click = (int)MouseButtons.None;
                }
                p_States.Buttons[(int)MouseButtons.None] = en;
                en.SendMessage(Message.KeyDown, e);

                if(e.Key == Keys.Enter)
                {
                    en.SendMessage(Message.Click, new MouseEventArgs(new MouseState(), MouseButtons.None, Point.Zero));
                }
            }
        }

        private void KeyUpProcess(object sender, KeyEventArgs e)
        {
            Entity en = p_States.Buttons[(int)MouseButtons.None];

            if(en != null)
            {
                if(e.Key == Keys.Space)
                {
                    en.SendMessage(Message.Click, new MouseEventArgs(new MouseState(), MouseButtons.None, Point.Zero));
                }
                p_States.Click = -1;
                p_States.Buttons[(int)MouseButtons.None] = null;
                en.SendMessage(Message.KeyUp, e);
            }
        }

        private void KeyPressProcess(object sender, KeyEventArgs e)
        {
            Entity en = p_States.Buttons[(int)MouseButtons.None];
            if(en != null)
            {
                en.SendMessage(Message.KeyPress, e);

                if ((e.Key == Keys.Right ||
                   e.Key == Keys.Left ||
                   e.Key == Keys.Up ||
                   e.Key == Keys.Down) && !e.Handled && CheckButtons((int)MouseButtons.None))
                {
                    ProcessArrows(en, e, new GamePadEventArgs(PlayerIndex.One));
                    KeyDownProcess(sender, e);
                }
                else if (e.Key == Keys.Tab && !e.Shift && !e.Handled && CheckButtons((int)MouseButtons.None))
                {
                    TabNextEntity(en);
                    KeyDownProcess(sender, e);
                }
                else if (e.Key == Keys.Tab && e.Shift && !e.Handled && CheckButtons((int)MouseButtons.None))
                {
                    TabPrevEntity(en);
                    KeyDownProcess(sender, e);
                }
            }
        }

        #endregion

        #endregion

        #endregion
    }
}
