using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using UiTesting.Source.Data;
using UiTesting.Source.Event;
using UiTesting.Source.Input;
using static UiTesting.Source.UiManager;

namespace UiTesting.Source
{
    #region Public Enums
    public enum EntityState
    {
        Default,
        MouseHover,
        MouseDown
    }

    public enum Anchor
    {
        Center,
        TopLeft,
        TopRight,
        TopCenter,
        BottomLeft,
        BottomRight,
        BottomCenter,
        CenterLeft,
        CenterRight,
        Auto,
        AutoCenter,
        AutoInLine,
        None
    }

    public enum LayoutState
    {
        None,
        FillParent,
        Halfparent,
        QuarterParent,
        SizeToContent
    }
    #endregion

    public class GamePadActions
    {
        public GamePadButton Click = GamePadButton.A;
        public GamePadButton Press = GamePadButton.Y;
        public GamePadButton Left = GamePadButton.LeftStickLeft;
        public GamePadButton Right = GamePadButton.LeftStickRight;
        public GamePadButton Up = GamePadButton.LeftStickUp;
        public GamePadButton Down = GamePadButton.LeftStickDown;
        public GamePadButton NextControl = GamePadButton.RightShoulder;
        public GamePadButton PrevControl = GamePadButton.LeftShoulder;
        public GamePadButton ContextMenu = GamePadButton.X;
    }

    public class Entity : IDisposable
    {
        #region Fields

        internal static List<Entity> Stack = new List<Entity>();

        private UiManager p_UiManager = null;

        internal bool p_HiddenInternalEntity = false;

        private string p_Name;
        public string Identifier = string.Empty;

        private EntityState p_State;
        protected LayoutState p_LayoutState = LayoutState.None;

        private Entity p_ParentEntity;

        private List<Entity> p_Children = new List<Entity>();
        private List<Entity> p_SortedChildren = new List<Entity>();

        private bool p_IsHidden;
        private bool p_IsFocused = false;

        private bool p_IsDragging;
        private bool p_IsDraggable = false;
        protected bool p_IsInteractable = false;
        private bool p_DrawExternally = false;

        private bool p_LimitDraggingToParentBounds = true;

        protected bool p_NeedToSortChildren = true;
        private bool p_NeedToSetDragOffset;

        private Color p_OverlayColor = Color.White;

        internal Rectangle p_DrawArea;
        internal Rectangle p_TargetArea;
        internal Rectangle p_InternalDrawArea;
        internal Rectangle p_ExternalDrawArea;

        internal Rectangle p_DraggableArea;

        internal Rectangle p_ChildernsBounds;
        
        private Vector2 p_DragOffset;

        private bool p_IsMouseOver;
        private bool p_IsTargetable = true;

        protected int p_indexInParent;

        protected Anchor p_Anchor;
        internal Vector2 p_BaseSize;
        internal Vector2 p_size;
        protected Vector2 p_Offset;
        protected Vector2 p_Padding;
        protected Vector2 p_Margin;

        protected bool p_NeedsRectUpdate = false;
        protected int p_destinationRectVerision = 0;
        protected int p_parentLastDestinationRectVerision = 0;

        protected bool DoEventsIfDirectParentIsLocked = false;

        protected bool p_ClickThrough = false;
        protected bool p_IsFirstUpdate = true;
        protected bool p_IsFirstDraw = true;

        private Stopwatch Stopwatch = new Stopwatch();

        public int ZOrder;

        private float p_LocalScale = 1f;

        protected Point p_LastScrollVal = Point.Zero;

        public bool Disabled = false;

        public bool Locked = false;

        private bool p_IsCurrentlyDisabled = false;

        internal bool p_Visible = true;

        protected bool InheritParentState = false;

        internal Vector2 p_SpaceBefore;

        internal Vector2 p_SpaceAfter = new Vector2(0, 10);

        public bool PromiscuousClicksMode = false;

        public bool UseActualSizeForCollision = false;

        public object AttachedData = null;

        public static Vector2 DefaultSize = Vector2.Zero;

        public Point ExtraMargin = Point.Zero;

        private bool p_CanFocus = true;
        private bool p_Enabled = true;
        private bool p_Passive = false;
        private bool p_Invalidated = true;
        private bool p_Hovered = false;
        private bool p_IsMoving = false;
        private bool[] p_Pressed = new bool[32];
        private bool p_Suspended = false;
        private bool p_Inside = false;

        private Entity p_Root = null;

        private bool p_IsRoot = false;

        private bool p_StayOnTop = false;
        private bool p_StayOnBack = false;

        private GamePadActions p_GamePadActions = new GamePadActions();


  
        #endregion

        #region Properties

        public UiManager UiManager { get { return p_UiManager; } set { p_UiManager = value; } }

        public string Name { get { return p_Name; } set { p_Name = value; } }

        public EntityState State { get { return p_State; } set { p_State = value; } }

        public Entity Parent { get { return p_ParentEntity; } set { p_ParentEntity = value; } }

        public bool IsHidden { get { return p_IsHidden; } set { p_IsHidden = value;  MarkasRectUpdate(); } }

        public bool IsFocused { get { return p_IsFocused; } set { if (p_IsFocused != value) { p_IsFocused = value; DoOnFocusChange(); } } }

        public bool IsTargetable { get { return p_IsTargetable; } set { p_IsTargetable = value; } }

        public Color OverlayColor { get { return p_OverlayColor; } set { p_OverlayColor = value; } }

        public bool IsDragging { get { return p_IsDragging; } set { p_IsDragging = value; } }
        public bool IsDraggable { get { return p_IsDraggable; } set { p_NeedToSetDragOffset = p_IsDraggable != value; p_IsDraggable = value; MarkasRectUpdate(); } }
        public bool LimitDraggingToParentBounds { get { return p_LimitDraggingToParentBounds; } set { p_LimitDraggingToParentBounds = value; } }

        public Rectangle MiddleDraggableArea { get { return p_DraggableArea; } set { p_DraggableArea = value; } }

        public List<Entity> Children { get { return p_Children; } }

        public virtual int Priority { get { return p_indexInParent; } }

        public Vector2 Size
        {
            get { return p_size; }
            set { if(p_size != value) { p_size = value; MarkasRectUpdate(); } }
        }
        
        protected float GlobalScale { get { return GetActiveUserInterface().GlobalScale; } }

        public bool ClickThrough { get { return p_ClickThrough; } set { p_ClickThrough = value; } }

        protected Vector2 ScaleSize { get { return p_size * GlobalScale * LocalScale; } }
        
        protected Vector2 ScaledOffset { get { return p_Offset * GlobalScale * LocalScale; } }

        public Vector2 Padding { get { return p_Padding; } set { p_Padding = value; } }

        public Vector2 Margin { get { return p_Margin; } set { p_Margin = value; } }

        public Vector2 ScaledMargin { get { return p_Margin * GlobalScale * LocalScale; } set { p_Margin = value; } }

        public Vector2 ScaledPadding { get { return p_Padding * GlobalScale * LocalScale; } set { p_Padding = value; } }

        protected Vector2 ScaledSpaceAfter { get { return p_SpaceAfter * GlobalScale; } }

        protected Vector2 ScaledSpaceBefore { get { return p_SpaceBefore * GlobalScale; } }

        public bool IsMouseOver { get { return p_IsMouseOver; } }

        public float LocalScale { get { return MathHelper.Clamp(p_LocalScale, 0.7f, 1.5f); } set { p_LocalScale = value;  MarkasRectUpdate(); } }

        protected virtual Point OverflowScrollVal { get { return Point.Zero; } }

        protected Vector2 GetMousePos(Vector2? addVector = null)
        {
            return GetActiveUserInterface().GetTransFormedCursorPos(addVector);
        }

        public Rectangle InternalDrawArea { get { return p_InternalDrawArea; } }

        public int Top { get { return p_DrawArea.Top; } }
        public int Bottom { get { return p_DrawArea.Bottom; } }
        public int Left { get { return p_DrawArea.Left; } }
        public int Right { get { return p_DrawArea.Right; } }

        public int Height { get { return p_DrawArea.Height; } }
        public int Width { get { return p_DrawArea.Width; } }

        public bool DrawExternally { get { return p_DrawExternally; } set { p_DrawExternally = value; } }

        public bool DrawingExternally { get; set; }

        public float Scale { get; set; }

        public bool Visible
        {
            get { return p_Visible; }
            set { p_Visible = value; DoOnVisibilityChange(); }
        }

        public Rectangle ChildernsBounds { get => p_ChildernsBounds; set => p_ChildernsBounds = value; }

        public bool CanFocus { get { return p_CanFocus; } set { p_CanFocus = value; } }
        public bool Enabled { get { return p_Enabled; } set { p_Enabled = value; } }
        public bool Passive { get { return p_Passive; } set { p_Passive = value; } }

        public bool IsRoot { get { return p_IsRoot; } }

        public Entity Root { get { return p_Root; } }

        public bool StayOnTop { get { return p_StayOnTop; } set { p_StayOnTop = value; } }
        public bool StayOnBack { get { return p_StayOnBack; } set { p_StayOnBack = value; } }

        public GamePadActions GamePadActions { get { return p_GamePadActions; } }

        public virtual bool Suspended { get { return p_Suspended; } set { p_Suspended = value; } }

        internal protected virtual bool Hovered { get { return p_Hovered; } }
        internal protected virtual bool Inside { get { return p_Inside; } }
        internal protected virtual bool[] Pressed { get { return p_Pressed; } }

        protected virtual bool IsMoving { get { return p_IsMoving; } set { p_IsMoving = value; } }

        internal protected virtual bool IsPressed
        {
            get
            {
                for (int i = 0;i< p_Pressed.Length-1;i++)
                {
                    if (p_Pressed[i]) return true;
                }
                return false;
            }
        }

        #endregion

        #region Events

        #region EventCallBacks
        public EventCallback OnMouseDown2 = null;

        public EventCallback OnMouseReleased = null;

        public EventCallback WhileMouseDown = null;

        public EventCallback WhileMouseHover = null;

        public EventCallback OnClick2 = null;

        public EventCallback OnValueChange = null;

        public EventCallback OnMouseEnter = null;

        public EventCallback OnMouseLeave = null;

        public EventCallback OnMouseWheelScroll = null;

        public EventCallback OnStartDrag = null;

        public EventCallback OnStopDrag = null;

        public EventCallback WhileDragging = null;

        public EventCallback BeforeDraw = null;

        public EventCallback AfterDraw = null;

        public EventCallback BeforeUpdate = null;

        public EventCallback AfterUpdate = null;

        public EventCallback OnVisiblityChange = null;

        public EventCallback OnFocusChange = null;
        #endregion

        #region EventMethods

        virtual protected void DoOnMouseDown()
        {
            OnMouseDown2?.Invoke(this);
            GetActiveUserInterface().OnMouseDown?.Invoke(this);
        }

        virtual protected void DoOnMouseReleased()
        {
            OnMouseReleased?.Invoke(this);
            GetActiveUserInterface().OnMouseReleased?.Invoke(this);
        }

        virtual protected void DoOnClick()
        {
            OnClick2?.Invoke(this);
            GetActiveUserInterface().OnClick?.Invoke(this);
        }

        virtual protected void DoWhileMouseDown()
        {
            WhileMouseDown?.Invoke(this);
            GetActiveUserInterface().WhileMouseDown?.Invoke(this);
        }

        virtual protected void DoWhileMouseHover()
        {
            WhileMouseHover?.Invoke(this);
            GetActiveUserInterface().WhileMouseHover?.Invoke(this);
        }

        virtual protected void DoOnValueChange()
        {
            OnValueChange?.Invoke(this);
            GetActiveUserInterface().OnValueChange?.Invoke(this);
        }

        virtual protected void DoOnMouseEnter()
        {
            OnMouseEnter?.Invoke(this);
            GetActiveUserInterface().OnMouseEnter?.Invoke(this);
        }

        virtual protected void DoOnMouseLeave()
        {
            OnMouseLeave?.Invoke(this);
            GetActiveUserInterface().OnMouseLeave?.Invoke(this);
        }

        virtual protected void DoOnStartDrag()
        {
            OnStartDrag?.Invoke(this);
            GetActiveUserInterface().OnStartDrag?.Invoke(this);
        }

        virtual protected void DoOnStopDrag()
        {
            OnStopDrag?.Invoke(this);
            GetActiveUserInterface().OnStopDrag?.Invoke(this);
        }

        virtual protected void DoWhileDragging()
        {
            WhileDragging?.Invoke(this);
            GetActiveUserInterface().WhileDragging?.Invoke(this);
        }

        virtual protected void DoOnMouseWheelScroll()
        {
            OnMouseWheelScroll?.Invoke(this);
            GetActiveUserInterface().OnMouseWheelScroll?.Invoke(this);
        }

        virtual protected void DoAfterUpdate()
        {
            AfterUpdate?.Invoke(this);
            GetActiveUserInterface().AfterUpdate?.Invoke(this);
        }

        virtual protected void DoOnVisibilityChange()
        {
            OnVisiblityChange?.Invoke(this);
            GetActiveUserInterface().OnVisiblityChange?.Invoke(this);
        }

        virtual protected void DoBeforeUpdate()
        {
            BeforeUpdate?.Invoke(this);
            GetActiveUserInterface().BeforeUpdate?.Invoke(this);
        }

        virtual protected void DoOnFocusChange()
        {
            OnFocusChange?.Invoke(this);
            GetActiveUserInterface().OnFocusChange?.Invoke(this);
        }

        protected virtual void DoOnFirstUpdate()
        {
            UiManager.GetActiveUserInterface().OnEntitySpawn?.Invoke(this);

            if (p_ParentEntity != null) { p_ParentEntity.MarkasRectUpdate(); }
        }

        virtual protected void OnAfterDraw(SpriteBatch spriteBatch)
        {
            AfterDraw?.Invoke(this);
            GetActiveUserInterface().AfterDraw?.Invoke(this);
        }

        virtual protected void OnBeforeDraw(SpriteBatch spriteBatch)
        {
            BeforeDraw?.Invoke(this);
            GetActiveUserInterface().BeforeDraw?.Invoke(this);
        }

        #endregion

        public event EventHandler Click;
        public event EventHandler DoubleClick;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MousePress;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseOver;
        public event MouseEventHandler MouseOut;
        public event MouseEventHandler MouseScroll;
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyPress;
        public event KeyEventHandler KeyUp;
        public event GamePadEventHandler GamePadDown;
        public event GamePadEventHandler GamePadUp;
        public event GamePadEventHandler GamePadPress;
        public event EventHandler MoveBegin;
        public event EventHandler MoveEnd;
        public event EventHandler ResizeBegin;
        public event EventHandler ResizeEnd;
        public event EventHandler ColorChanged;
        public event EventHandler TextColorChanged;
        public event EventHandler BackColorChanged;
        public event EventHandler TextChanged;
        public event EventHandler AnchorChanged;
        public event EventHandler SkinChanging;
        public event EventHandler SkinChanged;
        public event EventHandler ParentChanged;
        public event EventHandler RootChanged;
        public event EventHandler VisibleChanged;
        public event EventHandler EnabledChanged;
        public event EventHandler AlphaChanged;
        public event EventHandler FocusLost;
        public event EventHandler FocusGained;

        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            if (MouseUp != null) MouseUp.Invoke(this, e);
        }

        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            if (MouseDown != null) MouseDown.Invoke(this, e);
        }

        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            if (MouseMove != null) MouseMove.Invoke(this, e);
        }

        protected virtual void OnMouseOver(MouseEventArgs e)
        {
            if (MouseOver != null) MouseOver.Invoke(this, e);
        }

        protected virtual void OnMouseOut(MouseEventArgs e)
        {
            if (MouseOut != null) MouseOut.Invoke(this, e);
        }

        protected virtual void OnClick(Event.EventArgs e)
        {
            if (Click != null) Click.Invoke(this, e);
        }

        protected virtual void OnDoubleClick(Event.EventArgs e)
        {
            if (DoubleClick != null) DoubleClick.Invoke(this, e);
        }      

        protected virtual void OnMoveBegin(Event.EventArgs e)
        {
            if (MoveBegin != null) MoveBegin.Invoke(this, e);
        }

        protected virtual void OnMoveEnd(Event.EventArgs e)
        {
            if (MoveEnd != null) MoveEnd.Invoke(this, e);
        }

        protected virtual void OnResizeBegin(Event.EventArgs e)
        {
            if (ResizeBegin != null) ResizeBegin.Invoke(this, e);
        }

        protected virtual void OnResizeEnd(Event.EventArgs e)
        {
            if (ResizeEnd != null) ResizeEnd.Invoke(this, e);
        }

        protected virtual void OnKeyUp(KeyEventArgs e)
        {
            if (KeyUp != null) KeyUp.Invoke(this, e);
        }

        protected virtual void OnKeyDown(KeyEventArgs e)
        {
            if (KeyDown != null) KeyDown.Invoke(this, e);
        }

        protected virtual void OnKeyPress(KeyEventArgs e)
        {
            if (KeyPress != null) KeyPress.Invoke(this, e);
        }

        protected virtual void OnGamePadUp(GamePadEventArgs e)
        {
            if (GamePadUp != null) GamePadUp.Invoke(this, e);
        }

        protected virtual void OnGamePadDown(GamePadEventArgs e)
        {
            if (GamePadDown != null) GamePadDown.Invoke(this, e);
        }

        protected virtual void OnGamePadPress(GamePadEventArgs e)
        {
            if (GamePadPress != null) GamePadPress.Invoke(this, e);
        }

        protected virtual void OnColorChanged(Event.EventArgs e)
        {
            if (ColorChanged != null) ColorChanged.Invoke(this, e);
        }

        protected virtual void OnTextColorChanged(Event.EventArgs e)
        {
            if (TextColorChanged != null) TextColorChanged.Invoke(this, e);
        }

        protected virtual void OnBackColorChanged(Event.EventArgs e)
        {
            if (BackColorChanged != null) BackColorChanged.Invoke(this, e);
        }

        protected virtual void OnTextChanged(Event.EventArgs e)
        {
            if (TextChanged != null) TextChanged.Invoke(this, e);
        }

        protected virtual void OnAnchorChanged(Event.EventArgs e)
        {
            if (AnchorChanged != null) AnchorChanged.Invoke(this, e);
        }

        protected internal virtual void OnSkinChanged(Event.EventArgs e)
        {
            if (SkinChanged != null) SkinChanged.Invoke(this, e);
        }

        protected internal virtual void OnSkinChanging(Event.EventArgs e)
        {
            if (SkinChanging != null) SkinChanging.Invoke(this, e);
        }

        protected virtual void OnParentChanged(Event.EventArgs e)
        {
            if (ParentChanged != null) ParentChanged.Invoke(this, e);
        }

        protected virtual void OnRootChanged(Event.EventArgs e)
        {
            if (RootChanged != null) RootChanged.Invoke(this, e);
        }

        protected virtual void OnVisibleChanged(Event.EventArgs e)
        {
            if (VisibleChanged != null) VisibleChanged.Invoke(this, e);
        }

        protected virtual void OnEnabledChanged(Event.EventArgs e)
        {
            if (EnabledChanged != null) EnabledChanged.Invoke(this, e);
        }

        protected virtual void OnAlphaChanged(Event.EventArgs e)
        {
            if (AlphaChanged != null) AlphaChanged.Invoke(this, e);
        }

        protected virtual void OnFocusLost(Event.EventArgs e)
        {
            if (FocusLost != null) FocusLost.Invoke(this, e);
        }
  
        protected virtual void OnFocusGained(Event.EventArgs e)
        {
            if (FocusGained != null) FocusGained.Invoke(this, e);
        }

        protected virtual void OnMousePress(MouseEventArgs e)
        {
            if (MousePress != null) MousePress.Invoke(this, e);
        }

        protected virtual void OnMouseScroll(MouseEventArgs e)
        {
            if (MouseScroll != null) MouseScroll.Invoke(this, e);
        }

        #endregion

        #region Methods

        #region Virtual Methods

        virtual public Rectangle GetActualDestinationRectangle()
        {
            return p_DrawArea;
        }

        virtual public void UpdateDestinationRects()
        {
            p_DrawArea = CalculateDestinationRectangle();
            p_InternalDrawArea = CalculateInternalRectangle();
            p_ExternalDrawArea = CalculateExternalRectangle();

            p_TargetArea = p_DrawArea;

            p_NeedsRectUpdate = false;

            p_destinationRectVerision++;
            if (p_ParentEntity != null) { p_parentLastDestinationRectVerision = p_ParentEntity.p_destinationRectVerision; }
        }

        virtual public void UpdateDestinationRectsIfNeeded()
        {
            if (Parent != null && (p_NeedsRectUpdate || (p_parentLastDestinationRectVerision != p_ParentEntity.p_destinationRectVerision)))
            {
                UpdateDestinationRects();
            }
        }

        virtual public Rectangle CalculateChildernMinSizeRect()
        {
            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;

            foreach(Entity e in p_Children)
            {
                if(e.p_DrawArea.X < x || x == 0)
                {
                    x = e.p_DrawArea.X;
                }

                if (e.p_DrawArea.Y < y || y == 0)
                {
                    y = e.p_DrawArea.Y;
                }

                if(e.p_DrawArea.Right > width || width == 0)
                {
                    width = e.p_DrawArea.Right;
                }

                if (e.p_DrawArea.Bottom > height || height == 0)
                {
                    height = e.p_DrawArea.Bottom;
                }
            }

            return new Rectangle(x, y, width - x, height - y);
        }

        virtual public Rectangle CalculateInternalRectangle()
        {
            Vector2 padding = ScaledPadding;
            p_InternalDrawArea = GetActualDestinationRectangle();
            p_InternalDrawArea.X += (int)ScaledPadding.X;
            p_InternalDrawArea.Y += (int)ScaledPadding.Y;
            p_InternalDrawArea.Width -= (int)ScaledPadding.X * 2;
            p_InternalDrawArea.Height -= (int)ScaledPadding.Y * 2;
            return p_InternalDrawArea;
        }

        virtual public Rectangle CalculateExternalRectangle()
        {
            return p_ExternalDrawArea;
        }

        virtual public Rectangle CalculateDestinationRectangle()
        {
            Rectangle rect = new Rectangle();

            if (p_ParentEntity == null)
                return rect;

            if (!(p_ParentEntity is RootPanel))
            {
                if (LocalScale != p_ParentEntity.LocalScale)
                {
                    LocalScale = p_ParentEntity.LocalScale;
                }
            }

            p_ParentEntity.UpdateDestinationRectsIfNeeded();

            Rectangle parentDestinationRect;

            if (p_DrawExternally)
                parentDestinationRect = p_ParentEntity.p_ExternalDrawArea;
            else
                parentDestinationRect = p_ParentEntity.p_InternalDrawArea;


            if(p_LayoutState == LayoutState.SizeToContent)
            {       
                p_size.X = p_BaseSize.X + ChildernBounds().Width;
                p_size.Y = p_BaseSize.Y + ChildernBounds().Height;   
            }

            Vector2 size = ScaleSize;
            rect.Width = (size.X == 0f ? parentDestinationRect.Width : (size.X > 0f && size.X < 1f ? (int)(parentDestinationRect.Width * p_size.X) : (int)size.X));
            rect.Height = (size.Y == 0f ? parentDestinationRect.Height : (size.Y > 0f && size.Y < 1f ? (int)(parentDestinationRect.Height * p_size.Y) : (int)size.Y));

            if (rect.Width < 1) { rect.Width = 1; }
            if (rect.Height < 1) { rect.Height = 1; }

            int parent_left = parentDestinationRect.X;
            int parent_top = parentDestinationRect.Y;
            int parent_right = parent_left + parentDestinationRect.Width;
            int parent_bottom = parent_top + parentDestinationRect.Height;
            int parent_center_x = parent_left + parentDestinationRect.Width / 2;
            int parent_center_y = parent_top + parentDestinationRect.Height / 2;

            Anchor anchor = p_Anchor;
            LayoutState layoutState = p_LayoutState;

            Vector2 offset = ScaledOffset;
           
            if (p_IsDraggable && !p_NeedToSetDragOffset)
            {
                anchor = Anchor.TopLeft;
                offset = p_DragOffset;
            }

            if (layoutState == LayoutState.None || layoutState == LayoutState.SizeToContent)
            {

                switch (anchor)
                {
                    case Anchor.Auto:
                    case Anchor.AutoInLine:
                    case Anchor.TopLeft:
                        rect.X = parent_left + (int)offset.X;
                        rect.Y = parent_top + (int)offset.Y;
                        break;

                    case Anchor.TopRight:
                        rect.X = parent_right - rect.Width - (int)offset.X;
                        rect.Y = parent_top + (int)offset.Y;
                        break;

                    case Anchor.TopCenter:
                    case Anchor.AutoCenter:
                        rect.X = parent_center_x - rect.Width / 2 + (int)offset.X;
                        rect.Y = parent_top + (int)offset.Y;
                        break;

                    case Anchor.BottomLeft:
                        rect.X = parent_left + (int)offset.X;
                        rect.Y = parent_bottom - rect.Height - (int)offset.Y;
                        break;

                    case Anchor.BottomRight:
                        rect.X = parent_right - rect.Width - (int)offset.X;
                        rect.Y = parent_bottom - rect.Height - (int)offset.Y;
                        break;

                    case Anchor.BottomCenter:
                        rect.X = parent_center_x - rect.Width / 2 + (int)offset.X;
                        rect.Y = parent_bottom - rect.Height - (int)offset.Y;
                        break;

                    case Anchor.CenterLeft:
                        rect.X = parent_left + (int)offset.X;
                        rect.Y = parent_center_y - rect.Height / 2 + (int)offset.Y;
                        break;

                    case Anchor.CenterRight:
                        rect.X = parent_right - rect.Width - (int)offset.X;
                        rect.Y = parent_center_y - rect.Height / 2 + (int)offset.Y;
                        break;

                    case Anchor.Center:
                        rect.X = parent_center_x - rect.Width / 2 + (int)offset.X;
                        rect.Y = parent_center_y - rect.Height / 2 + (int)offset.Y;
                        break;

                    case Anchor.None:
                        break;
                }
            }
            else
            {

                switch (layoutState)
                {               
                    case LayoutState.FillParent:
                        rect = parentDestinationRect;
                        break;
                    case LayoutState.Halfparent:
                        break;
                    case LayoutState.QuarterParent:
                        break;
                    case LayoutState.None:
                        break;
                }
            }

            if((anchor == Anchor.Auto || anchor == Anchor.AutoInLine || anchor == Anchor.AutoCenter) && p_ParentEntity != null)
            {
                Entity prevEntity = GetPreviousEntity(true, true);

                if(prevEntity != null)
                {
                    prevEntity.UpdateDestinationRectsIfNeeded();

                    if(anchor == Anchor.AutoInLine)
                    {
                        rect.X = prevEntity.p_DrawArea.Right + (int)(offset.X + prevEntity.ScaledSpaceAfter.X + ScaledSpaceBefore.X);
                        rect.Y = prevEntity.p_DrawArea.Y;
                    }

                    if((anchor == Anchor.AutoInLine && rect.Right > p_ParentEntity.p_InternalDrawArea.Right) || anchor == Anchor.Auto || anchor == Anchor.AutoCenter)
                    {
                        if(anchor != Anchor.AutoCenter)
                        {
                            rect.X = parent_left + (int)offset.X;
                        }

                        rect.Y = prevEntity.GetDrawRectForAutoAnchors().Bottom + (int)(offset.Y + prevEntity.ScaledSpaceAfter.Y + ScaledSpaceBefore.Y);
                    }
                }
            }


            if (p_IsDraggable)
            {
                if (p_NeedToSetDragOffset)
                {
                    p_DragOffset.X = rect.X - parent_left;
                    p_DragOffset.Y = rect.Y - parent_top;
                    p_NeedToSetDragOffset = false;
                }

                if (p_IsDragging)
                {
                    if (LimitDraggingToParentBounds)
                    {
                        if (!DrawingExternally)
                        {
                            if (rect.X < parent_left) { rect.X = parent_left; p_DragOffset.X = 0; }
                            if (rect.Y < parent_top) { rect.Y = parent_top; p_DragOffset.Y = 0; }
                            if (rect.Right > parent_right) { p_DragOffset.X -= rect.Right - parent_right; ; rect.X -= rect.Right - parent_right; }
                            if (rect.Bottom > parent_bottom) { p_DragOffset.Y -= rect.Bottom - parent_bottom; rect.Y -= rect.Bottom - parent_bottom; }
                        }
                        else
                        {
                            if (rect.X < parent_left) { rect.X = parent_left; p_DragOffset.X = 0; }
                            if (rect.Y < parent_top) { rect.Y = parent_top; p_DragOffset.Y = 0; }
                            if (rect.Right > parent_right) { p_DragOffset.X -= rect.Right - parent_right; ; rect.X -= rect.Right - parent_right; }
                            if (rect.Bottom + p_ExternalDrawArea.Height > parent_bottom){ p_DragOffset.Y -= (rect.Bottom + p_ExternalDrawArea.Height) - parent_bottom; rect.Y -= (rect.Bottom + p_ExternalDrawArea.Height) - parent_bottom; }
                        }
                    }
                }
            }

            

            p_DrawArea = rect;

            CalculateDragAreas(rect);

            return rect;
        }

        virtual public void CalculateDragAreas(Rectangle rect)
        {
            p_DraggableArea = rect;
            p_TargetArea = p_DrawArea;
        }

        virtual public bool IsNaturallyInteractable()
        {
            return false;
        }

        virtual protected void UpdateChildren(ref Entity targetEntity, ref Entity dragTargetEntity, ref bool wasEventHandled, GameTime gameTime, Point scrollVal)
        {
            List<Entity> childrenSorted = GetSortedChildren();

            for (int i = childrenSorted.Count - 1; i >= 0; i--)
            {
                childrenSorted[i].Update(ref targetEntity, ref dragTargetEntity, ref wasEventHandled, gameTime, scrollVal);
            }
        }

        public virtual void Update(ref Entity targetEntity, ref Entity dragTargetEntity, ref bool wasEventHandled, GameTime gameTime, Point scrollVal)
        {
            p_LastScrollVal = scrollVal;

            if (p_IsFirstUpdate)
            {
                DoOnFirstUpdate();
                p_IsFirstUpdate = false;
            }

            if (InheritParentState)
            {
                p_State = p_ParentEntity.State;
                p_IsMouseOver = p_ParentEntity.p_IsMouseOver;
                IsFocused = p_ParentEntity.IsFocused;
                p_IsCurrentlyDisabled = p_ParentEntity.p_IsCurrentlyDisabled;
                return;
            }

            Vector2 mousePos = GetMousePos();

            scrollVal += OverflowScrollVal;

            p_IsCurrentlyDisabled = IsDisabled();

            if (p_IsCurrentlyDisabled || IsLocked() || !p_Visible)
            {
                if (Locked)
                {
                    for (int i = p_Children.Count - 1; i >= 0; i++)
                    {
                        if (p_Children[i].DoEventsIfDirectParentIsLocked)
                        {
                            p_Children[i].Update(ref targetEntity, ref dragTargetEntity, ref wasEventHandled, gameTime, scrollVal);
                        }
                    }
                }

                if (p_IsInteractable)
                {
                    if (p_State == EntityState.MouseHover)
                    {
                        DoOnMouseLeave();
                    }
                    else if (p_State == EntityState.MouseDown)
                    {
                        DoOnMouseReleased();
                        DoOnMouseLeave();
                    }
                }

                p_IsInteractable = false;
                p_State = EntityState.Default;
                return;
            }

            if (ClickThrough)
            {
                UpdateChildren(ref targetEntity, ref dragTargetEntity, ref wasEventHandled, gameTime, scrollVal);
                return;
            }

            UpdateDestinationRectsIfNeeded();

            p_IsInteractable = true;

            DoBeforeUpdate();

            EntityState prevState = p_State;

            bool prevMouseOver = p_IsMouseOver;

            if (!wasEventHandled)
            {
                if (!InheritParentState)
                {
                    p_IsMouseOver = false;
                    State = EntityState.Default;

                    if (IsPointInsideEntity(mousePos) && IsTargetable)
                    {
                        if (targetEntity == null || targetEntity.p_ParentEntity != p_ParentEntity)
                        {
                            targetEntity = this;
                        }

                        p_IsMouseOver = true;


                        State = (IsFocused || PromiscuousClicksMode || InputUtils.IsMouseButtonLeftPressed()) && 
                            InputUtils.IsMouseButtonLeftPressed() ? EntityState.MouseDown : EntityState.MouseHover;
                    }
                }

                if (InputUtils.IsMouseButtonLeftTiggered())
                {
                    IsFocused = true;
                }
            }
            else if (InputUtils.IsMouseButtonLeftTiggered())
            {
                IsFocused = true;
            }

            UpdateChildren(ref targetEntity, ref dragTargetEntity, ref wasEventHandled, gameTime, scrollVal);

            if ((p_IsDraggable || IsNaturallyInteractable()) && dragTargetEntity == null && InputUtils.IsMouseButtonLeftPressed())
            {
                if (IsPointInsideRectangle(mousePos, p_DraggableArea))
                {
                    dragTargetEntity = this;
                }
            }

            if (targetEntity == this && (dragTargetEntity == null || dragTargetEntity == this))
            {
                wasEventHandled = true;

                if (State == EntityState.MouseDown)
                {
                    DoWhileMouseDown();
                }
                else
                {
                    DoWhileMouseHover();
                }

                if (p_IsMouseOver && !prevMouseOver)
                {
                    DoOnMouseEnter();
                }

                if (prevState != p_State)
                {
                   
                    if (InputUtils.IsMouseButtonLeftPressed())
                    {
                        DoOnMouseDown();
                    }

                    if (InputUtils.IsMouseButtonLeftReleased())
                    {
                        DoOnMouseReleased();
                    }

                    if (InputUtils.IsMouseButtonLeftTiggered())
                    {
                        DoOnClick();
                    }
                }
            }
            else
            {
                State = EntityState.Default;
            }

            if (!p_IsMouseOver && prevMouseOver)
            {
                DoOnMouseLeave();
            }

            if (targetEntity == this || GetActiveUserInterface().ActiveEntity == this)
            {
                if (InputUtils.MouseWheelChange != 0)
                {
                    DoOnMouseWheelScroll();
                }
            }

            if (p_IsDraggable && (dragTargetEntity == this) && IsFocused)
            {
                if (!p_IsDragging && InputUtils.MousePositionDiff.Length() != 0)
                {
                    Entity parent = p_ParentEntity;
                    RemoveFromParent();
                    parent.AddChild(this);

                    p_IsDragging = true;
                    DoOnStartDrag();
                }

                if (p_IsDragging)
                {

                    p_DragOffset += InputUtils.MousePositionDiff;
                    dragTargetEntity = this;
                    DoWhileDragging();
                }
            }
            else if (p_IsDragging)
            {
                p_IsDragging = false;
                DoOnStopDrag();
                MarkasRectUpdate();
            }

            if (p_IsDragging)
            {
                MarkasRectUpdate();
            }

            DoAfterUpdate();
        }

        public virtual void DrawEntity(SpriteBatch spriteBatch, bool? BaseBraw = null)
        {
            if (GetActiveUserInterface().DebugMode)
            {                 
                spriteBatch.DrawRectangle(p_TargetArea, Color.Black, 1f);

                //GetActiveUserInterface().debugUtils.AddDrawDebug("Entity" + Name, DebugDrawMode.Rectangle, _drawRect: p_DrawArea, color: Color.Black);

                spriteBatch.DrawRectangle(p_DrawArea, Color.Blue, 2f);         
            }
        }

        protected virtual void BeforeDrawChildren(SpriteBatch spriteBatch)
        {

        }

        protected virtual void AfterDrawChildren(SpriteBatch spriteBatch)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!p_Visible)
            {
                return;
            }

            if (p_IsFirstDraw)
            {
                Stopwatch = Stopwatch.StartNew();
            }

            p_IsCurrentlyDisabled = IsDisabled();

            UpdateDestinationRectsIfNeeded();

            GetActiveUserInterface().drawUtils.StartDraw(spriteBatch, false);
            DrawEntity(spriteBatch);
            GetActiveUserInterface().drawUtils.EndDraw(spriteBatch);

            if (p_IsFirstDraw)
            {
                Logging.LogHelper.Log(Logging.LogTarget.Console, Logging.LogLevel.Warning, this.Name + ": " + Stopwatch.ElapsedMilliseconds.ToString());
                Stopwatch.Stop();              
            }

            BeforeDrawChildren(spriteBatch);

            List<Entity> childrenSorted = GetSortedChildren();

            foreach (Entity child in childrenSorted)
            {
                child.Draw(spriteBatch);
            }

            AfterDrawChildren(spriteBatch);

            p_IsFirstDraw = false;
        }

        protected virtual Rectangle GetDrawRectForAutoAnchors()
        {
            return GetActualDestinationRectangle();
        }

        public virtual Entity AddChild(Entity child, bool inheritParentState = false, int index = -1)
        {
            if (child.Parent != null)
                return null;

            p_NeedToSortChildren = true;

            child.InheritParentState = inheritParentState;

            child.p_ParentEntity = this;

            if (index == -1 || index >= p_Children.Count)
            {
                index = p_Children.Count;
            }

            child.p_indexInParent = index;
            p_Children.Insert(index, child);

            for (int i = index + 1; i < p_Children.Count; i++)
            {
                p_Children[i].p_indexInParent += 1;
            }

            child.p_parentLastDestinationRectVerision = p_destinationRectVerision - 1;

            child.MarkasRectUpdate();
            MarkasRectUpdate();

            p_ChildernsBounds.Width += (int)child.Size.X;
            p_ChildernsBounds.Height += (int)child.Size.Y;

            return child;
        }

        public virtual void PropagateEventsTo(Entity other)
        {
            OnMouseDown2 += (Entity entity) => { other.OnMouseDown2?.Invoke(other); };
            OnMouseReleased += (Entity entity) => { other.OnMouseReleased?.Invoke(other); };
            WhileMouseDown += (Entity entity) => { other.WhileMouseDown?.Invoke(other); };
            WhileMouseHover += (Entity entity) => { other.WhileMouseHover?.Invoke(other); };
            OnClick2 += (Entity entity) => { other.OnClick2?.Invoke(other); };
            OnValueChange += (Entity entity) => { other.OnValueChange?.Invoke(other); };
            OnMouseEnter += (Entity entity) => { other.OnMouseEnter?.Invoke(other); };
            OnMouseLeave += (Entity entity) => { other.OnMouseLeave?.Invoke(other); };
            OnMouseWheelScroll += (Entity entity) => { other.OnMouseWheelScroll?.Invoke(other); };
            OnStartDrag += (Entity entity) => { other.OnStartDrag?.Invoke(other); };
            OnStopDrag += (Entity entity) => { other.OnStopDrag?.Invoke(other); };
            WhileDragging += (Entity entity) => { other.WhileDragging?.Invoke(other); };
            BeforeDraw += (Entity entity) => { other.BeforeDraw?.Invoke(other); };
            AfterDraw += (Entity entity) => { other.AfterDraw?.Invoke(other); };
            BeforeUpdate += (Entity entity) => { other.BeforeUpdate?.Invoke(other); };
            AfterUpdate += (Entity entity) => { other.AfterUpdate?.Invoke(other); };
        }

        public virtual bool IsPointInsideEntity(Vector2 mousePos)
        {
            mousePos += p_LastScrollVal.ToVector2();

            Rectangle rect = UseActualSizeForCollision ? GetActualDestinationRectangle() : p_TargetArea;

            return (mousePos.X > rect.Left - ExtraMargin.X && mousePos.X < rect.Right + ExtraMargin.X &&
                    mousePos.Y > rect.Top - ExtraMargin.Y && mousePos.Y < rect.Bottom + ExtraMargin.Y);
        }

        public virtual void OpenEntity()
        {
            Visible = true;
        }

        public virtual void CloseEntity()
        {
            Visible = false;
        }
        #endregion

        #region Public Methods

        public Entity(EntityProps entityProps)
        {
            NumberOfUIEntities++;

            MarkasRectUpdate();

            Vector2 defaultSize = EntityDefaultSize;

            p_Name = entityProps.EntityName != null ? entityProps.EntityName : "Entity: " + NumberOfUIEntities;

            p_Offset = entityProps.LocalPosition;
            p_size = entityProps.Size != Vector2.Zero ? entityProps.Size : defaultSize;
            p_BaseSize = p_size;

            p_Padding = new Vector2(10, 10);
            p_Margin = entityProps.Margin;

            p_Anchor = entityProps.EntityAnchor;
            p_LayoutState = entityProps.EntityLayoutState;

            p_OverlayColor = entityProps.OverlayColor != Color.Transparent ? entityProps.OverlayColor : Color.White;

            if(p_size.X == -1) { p_size.X = defaultSize.X; }
            if(p_size.Y == -1) { p_size.Y = defaultSize.Y; }

            Stack.Add(this);
        }

        public Vector2 EntityDefaultSize
        {
            get
            {
                System.Type type = GetType();

                while(true)
                {
                    FieldInfo feild = type.GetField("DefaultSize", BindingFlags.Public | BindingFlags.Static);
                    if(feild != null)
                    {
                        return (Vector2)(feild.GetValue(null));
                    }

                    type = type.BaseType;                                 
                }
            }
        }

        public void MarkasRectUpdate()
        {
            p_NeedsRectUpdate = true;
        }

        public T Find<T>(string name, bool recursive = false) where T : Entity
        {
            foreach (Entity child in p_Children)
            {
                if (child.p_HiddenInternalEntity)
                    continue;

                if (child.Name == name && child is T)
                {
                    return (T)child;
                }

                if (recursive)
                {
                    T ret = child.Find<T>(name, recursive);

                    if (ret != null)
                    {
                        return ret;
                    }
                }
            }

            return null;
        }

        public Entity Find(string name, bool recursive = false)
        {
            return Find<Entity>(name, recursive);
        }

        public bool IsPointInsideRectangle(Vector2 point, Rectangle rect)
        {
            return (point.X > rect.Left && point.X < rect.Right && point.Y > rect.Top && point.Y < rect.Bottom);
        }

        public void RemoveChild(Entity child)
        {
            if(child.p_ParentEntity != this)
                return;

            p_NeedToSortChildren = true;

            child.p_ParentEntity = null;
            child.p_indexInParent = -1;
            p_Children.Remove(child);

            int index = 0;
            foreach(Entity itrchild in p_Children)
            {
                itrchild.p_indexInParent = index++;
            }

            p_ChildernsBounds.Width -= (int)child.Size.X;
            p_ChildernsBounds.Height -= (int)child.Size.Y;

            child.MarkasRectUpdate();
            MarkasRectUpdate();
        }

        public void ClearChildren()
        {
            foreach(Entity c in p_Children)
            {
                c.Parent = null;
                c.p_indexInParent = -1;
                c.MarkasRectUpdate();
            }
            p_Children.Clear();

            MarkasRectUpdate();
        }

        public void RemoveFromParent()
        {
            if(p_ParentEntity != null)
            {
                p_ParentEntity.RemoveChild(this);
            }
        }       

        public bool IsDeepChildOf(Entity other)
        {
            Entity parent = this;
            while (parent != null)
            {
                if (parent.p_ParentEntity == other) { return true; }
                parent = parent.p_ParentEntity;
            }

            // not child of
            return false;
        }

        public bool IsDisabled()
        {
            Entity parent = this;
            while (parent != null)
            {
                if (parent.Disabled) { return true; }
                parent = parent.Parent;
            }

            return false;
        }

        public bool IsLocked()
        {
            Entity parent = this;
            while (parent != null)
            {
                if (parent.Locked)
                {
                    if (DoEventsIfDirectParentIsLocked)
                    {
                        if (parent == p_ParentEntity)
                        {
                            parent = parent.p_ParentEntity;
                        }
                    }

                    return true;
                }

                parent = parent.p_ParentEntity;
            }

            return false;
        }

        public bool IsVisible()
        {
            Entity parent = this;
            while(parent != null)
            {
                if (!parent.p_Visible) { return false; }
                parent = parent.p_ParentEntity;
            }

            return true;
        }

        public void BringToFront()
        {
            Entity parent = p_ParentEntity;
            parent.RemoveChild(this);
            parent.AddChild(this);
        }

        public void SetAnchor(Anchor anchor)
        {
            p_Anchor = anchor;
            MarkasRectUpdate();
        }

        public void SetOffset(Vector2 offset)
        {
            if (p_IsDragging)
            {
                p_DragOffset = offset;
                MarkasRectUpdate();
            }

            else if (p_Offset != offset)
            {
                p_Offset = offset;
                MarkasRectUpdate();
            }
        }

        public Rectangle ChildernBounds()
        {
            int x = 0, y = 0, width = 0, height = 0;

            foreach (Entity e in GetChildren())
            {
                if (e.Visible)
                {
                    if (e.p_DrawArea.X < x) { x = e.p_DrawArea.X; }

                    if (e.p_DrawArea.Y < y) { y = e.p_DrawArea.Y; }

                    if (e.p_BaseSize.X > 1) { width += (e.p_DrawArea.Width + (int)e.p_SpaceAfter.X + (int)e.p_SpaceBefore.X); }

                    if (e.p_BaseSize.Y > 1) { height += (e.p_DrawArea.Height + (int)e.p_SpaceAfter.Y + (int)e.p_SpaceBefore.Y); }
                }
            }

            return new Rectangle(x, y, width, height);
        }

        public virtual void SendMessage(Message message, Event.EventArgs e)
        {
            MessageProcess(message, e);
        }

        public virtual void Invalidate()
        {
            p_Invalidated = true;

            if(p_ParentEntity != null)
            {
                p_ParentEntity.Invalidate();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Protected/Priviate/Internal Methods

        protected List<Entity> GetSortedChildren()
        {
            if (p_NeedToSortChildren)
            {

                p_SortedChildren = new List<Entity>(p_Children);

                p_SortedChildren.Sort((x, y) => x.Priority.CompareTo(y.Priority));

                p_NeedToSortChildren = false;
            }

            return p_SortedChildren;
        }

        internal void ClearRectUpdateFlag(bool updateChildren = false)
        {
            p_NeedsRectUpdate = false;
            if (updateChildren)
            {
                int o = p_parentLastDestinationRectVerision;
                p_destinationRectVerision++;
            }
        }       

        protected Entity GetPreviousEntity(bool skipInvisibles = false, bool skipNonAutos = false)
        {
            if (p_ParentEntity == null) return null;

            List<Entity> siblings = p_ParentEntity.GetChildren();
            Entity prev = null;
            foreach(Entity s in siblings)
            {
                if(s == this)
                {
                    break;
                }

                if(skipInvisibles && !s.Visible)
                {
                    continue;
                }

                if(skipNonAutos == true && (s.p_Anchor != Anchor.Auto && s.p_Anchor != Anchor.AutoCenter && s.p_Anchor != Anchor.AutoInLine))
                {
                    continue;
                }

                prev = s;
            }

            return prev;
        }

        protected virtual void MessageProcess(Message message, Event.EventArgs e)
        {
            switch (message)
            {
                case Message.Click:
                    {
                        ClickProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.MouseDown:
                    {
                        MouseDownProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.MouseUp:
                    {
                        MouseUpProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.MousePress:
                    {
                        MousePressProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.MouseScroll:
                    {
                        MouseScrollProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.MouseMove:
                    {
                        MouseMoveProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.MouseOver:
                    {
                        MouseOverProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.MouseOut:
                    {
                        MouseOutProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.GamePadDown:
                    {
                        GamePadDownProcess(e as GamePadEventArgs);
                        break;
                    }
                case Message.GamePadUp:
                    {
                        GamePadUpProcess(e as GamePadEventArgs);
                        break;
                    }
                case Message.GamePadPress:
                    {
                        GamePadPressProcess(e as GamePadEventArgs);
                        break;
                    }
                case Message.KeyDown:
                    {
                        KeyDownProcess(e as KeyEventArgs);
                        break;
                    }
                case Message.KeyUp:
                    {
                        KeyUpProcess(e as KeyEventArgs);
                        break;
                    }
                case Message.KeyPress:
                    {
                        KeyPressProcess(e as KeyEventArgs);
                        break;
                    }
            }
        }

        #region GamePad
        
        private void GamePadPressProcess(GamePadEventArgs e)
        {
            Invalidate();
            if (!Suspended) OnGamePadPress(e);
        }

        private void GamePadUpProcess(GamePadEventArgs e)
        {
            Invalidate();
            
            if(e.Button == GamePadActions.Press && p_Pressed[(int)e.Button])
            {
                p_Pressed[(int)e.Button] = false;
            }

            if (!Suspended) OnGamePadUp(e);
        }

        private void GamePadDownProcess(GamePadEventArgs e)
        {
            Invalidate();

            //ToolTipOut();

            if(e.Button == GamePadActions.Press && !IsPressed)
            {
                p_Pressed[(int)e.Button] = true;
            }

            if (!Suspended) OnGamePadDown(e);
        }

        #endregion

        #region Keybroad

        private void KeyPressProcess(KeyEventArgs e)
        {

        }

        private void KeyDownProcess(KeyEventArgs e)
        {

        }

        private void KeyUpProcess(KeyEventArgs e)
        {

        }

        #endregion

        #region Mouse

        private void MouseDownProcess(MouseEventArgs e)
        {

        }

        private void MouseUpProcess(MouseEventArgs e)
        {

        }

        private void MousePressProcess(MouseEventArgs e)
        {

        }

        private void MouseScrollProcess(MouseEventArgs e)
        {

        }

        private void MouseOverProcess(MouseEventArgs e)
        {

        }

        private void MouseOutProcess(MouseEventArgs e)
        {

        }

        private void MouseMoveProcess(MouseEventArgs e)
        {

        }
        
        private void ClickProcess(Event.EventArgs e)
        {

        }
        #endregion

        #endregion

        #region Getters

        public Vector2 GetPosition()
        {
            return new Vector2(p_DrawArea.X, p_DrawArea.Y);
        }

        public Vector2 GetLocalPosition()
        {
            return new Vector2(p_Offset.X, p_Offset.Y);
        }

        public string GetName()
        {
            return Name;
        }

        public Object GetAttachedData()
        {
            return AttachedData;
        }

        public List<Entity> GetChildren(bool? skipinvisibles = false)
        {
            if(skipinvisibles == true)
            {
                List<Entity> c = new List<Entity>();
                
                foreach(Entity e in p_Children)
                {
                    if(e.p_HiddenInternalEntity == false)
                    {
                        c.Add(e);
                    }
                }

                return c;
            }

            return p_Children;
        }

        public Entity GetParent()
        {
            return Parent;
        }

        #endregion

        #endregion
    }
}
