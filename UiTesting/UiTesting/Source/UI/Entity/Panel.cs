using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UiTesting.Source.Input;

namespace UiTesting.Source
{
    public enum PanelOverflowBehavior
    {
        Overflow,
       
        Clipped,

        VerticalScroll,
    }

    public class Panel : Entity
    {
        #region Fields

        private Sprite p_BackGroundTexture;

        private VerticalScrollBar p_ScrollBar;

        private Rectangle p_OriginalDrawArea;

        protected RenderTarget2D p_RenderTarget2D = null;

        protected PanelOverflowBehavior p_OverFlowMode = PanelOverflowBehavior.Overflow;

     
        #endregion

        #region Properties    
        public Sprite BackGroundTexture
        {
            get { return p_BackGroundTexture; }
            set { p_BackGroundTexture = value; }
        }

        public VerticalScrollBar ScrollBar { get { return p_ScrollBar; } }

        protected override Point OverflowScrollVal { get { return p_ScrollBar == null ? Point.Zero : new Point(0, p_ScrollBar.Value); } }

        public PanelOverflowBehavior PanelOverflowBehavior
        {
            get { return p_OverFlowMode; }
            set { p_OverFlowMode = value; UpdateOverFlowMode(); }
        }
        #endregion

        #region Methods

        public Panel(PanelProps panelProps) : base(panelProps)
        {
            p_BackGroundTexture = panelProps.Backgroundtexture != null ? panelProps.Backgroundtexture : new Sprite(ContentLoader.GetTextureByName("tan_pressed"));
        }

        public void DrawBackGround(SpriteBatch spriteBatch)
        {
            p_BackGroundTexture.Draw(spriteBatch, SpriteDrawMode.Panel, p_DrawArea, OverlayColor);
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            DrawBackGround(spriteBatch);

            base.DrawEntity(spriteBatch);
        }

        protected override void BeforeDrawChildren(SpriteBatch spriteBatch)
        {
            if(p_OverFlowMode == PanelOverflowBehavior.Overflow)
            {
                return;
            }

            Rectangle RenderTargetRect = GetRenderTargetRect();
            if (p_RenderTarget2D == null || p_RenderTarget2D.Width != RenderTargetRect.Width || p_RenderTarget2D.Height != p_RenderTarget2D.Height)
            {
                p_RenderTarget2D = new RenderTarget2D(spriteBatch.GraphicsDevice,
                            RenderTargetRect.Width, RenderTargetRect.Height, false,
                            spriteBatch.GraphicsDevice.PresentationParameters.BackBufferFormat,
                            spriteBatch.GraphicsDevice.PresentationParameters.DepthStencilFormat, 0,
                            RenderTargetUsage.PreserveContents);
            }

            spriteBatch.GraphicsDevice.SetRenderTarget(p_RenderTarget2D);
            spriteBatch.GraphicsDevice.Clear(Color.Transparent);

            UiManager.GetActiveUserInterface().drawUtils.PushRenderTarget(p_RenderTarget2D);

            //foreach (Entity e in Children)
            //{
            //    if (!e.p_DrawArea.Intersects(p_InternalDrawArea))
            //    {
            //        e.Visible = false;
            //    }
            //}

            p_OriginalDrawArea = p_InternalDrawArea;
            p_InternalDrawArea.X = 0;
            p_InternalDrawArea.Y = 0;


            

            if (p_OverFlowMode == PanelOverflowBehavior.VerticalScroll)
            {
                p_InternalDrawArea.Y -= p_ScrollBar.Value;

                p_ScrollBar.SetAnchor(Anchor.CenterLeft);
                p_ScrollBar.SetOffset(new Vector2(p_InternalDrawArea.Width + 5, -p_InternalDrawArea.Y) / GlobalScale);

                if(p_ScrollBar.Parent != null)
                {
                    p_ScrollBar.BringToFront();
                }
                else
                {
                    AddChild(p_ScrollBar);
                }
            }


            ClearRectUpdateFlag(true);
        }

        protected override void AfterDrawChildren(SpriteBatch spriteBatch)
        {
            if(p_OverFlowMode == PanelOverflowBehavior.Overflow)
            {
                return;
            }

            p_InternalDrawArea = p_OriginalDrawArea;
            p_destinationRectVerision++;

            if (p_RenderTarget2D != null)
            {
                UiManager.GetActiveUserInterface().drawUtils.PopRenderTargets();

                UiManager.GetActiveUserInterface().drawUtils.StartDraw(spriteBatch, false);
                spriteBatch.Draw(p_RenderTarget2D, GetRenderTargetRect(), Color.White);
                UiManager.GetActiveUserInterface().drawUtils.EndDraw(spriteBatch);

                if(p_ScrollBar != null)
                {
                    p_InternalDrawArea.Y -= p_ScrollBar.Value;
                    p_InternalDrawArea.Width -= p_ScrollBar.GetActualDestinationRectangle().Width;
                    p_ScrollBar.UpdateDestinationRects();

                    p_InternalDrawArea = p_OriginalDrawArea;
                }
            }
        }

        private int GetScrollbarWidth()
        {
            return p_ScrollBar != null ? p_ScrollBar.GetActualDestinationRectangle().Width : 0;
        }

        private void UpdateOverFlowMode()
        {
            if(p_OverFlowMode == PanelOverflowBehavior.VerticalScroll)
            {
                if(p_ScrollBar == null)
                {
                    p_ScrollBar = new VerticalScrollBar(new SliderProps { Max = 0, Min = 0, EntityAnchor = Anchor.TopRight }, true)
                    {                       
                        Size = new Vector2(10, 0),
                        ScaledPadding = Vector2.Zero,
                        AdjustMaxAutomatically = true,
                        Name = "ScrollBar",
                        p_HiddenInternalEntity = true,
                    };
                    bool prev_needToSortChildren = p_NeedToSortChildren;
                    AddChild(p_ScrollBar);
                    p_NeedToSortChildren = prev_needToSortChildren;
                }
            }
        }

        public override Rectangle CalculateInternalRectangle()
        {
            base.CalculateInternalRectangle();
            p_InternalDrawArea.Width -= GetScrollbarWidth();
            return p_InternalDrawArea;
        }

        private Rectangle GetRenderTargetRect()
        {
            Rectangle rect = p_InternalDrawArea;
            rect.Width += GetScrollbarWidth() * 2;
            return rect;
        }

        protected override void UpdateChildren(ref Entity targetEntity, ref Entity dragTargetEntity, ref bool wasEventHandled, GameTime gameTime, Point scrollVal)
        {
            bool skipChildren = false;
            Vector2 mousePos = GetMousePos();
            if (mousePos.X < p_InternalDrawArea.Left || mousePos.X > p_InternalDrawArea.Right ||
                   mousePos.Y < p_InternalDrawArea.Top || mousePos.Y > p_InternalDrawArea.Bottom)
            {
                skipChildren = true;
            }

            if(p_ScrollBar != null)
            {
                p_ScrollBar.Disabled = true;
            }

            //if (!skipChildren)
            //{
                base.UpdateChildren(ref targetEntity, ref dragTargetEntity, ref wasEventHandled, gameTime, scrollVal);
            //}

            if(p_ScrollBar != null)
            {
                p_ScrollBar.Disabled = false;
                p_ScrollBar.Update(ref targetEntity, ref dragTargetEntity, ref wasEventHandled, gameTime, scrollVal - OverflowScrollVal);
            }
        }
        #endregion
    }
}
