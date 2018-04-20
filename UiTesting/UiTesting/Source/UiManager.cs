using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using UiTesting.Source.Input;
using UiTesting.Source.Forms;
using UiTesting.Source.Logging;
using Microsoft.Xna.Framework.Input;

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

        public void AddChildToRoot(Entity child)
        {
            Root.AddChild(child);
        }

        public void RemoveChildFromRoot(Entity child)
        {
            Root.RemoveChild(child);
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

        #endregion
    }
}
