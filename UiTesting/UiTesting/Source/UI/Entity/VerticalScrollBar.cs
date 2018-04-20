using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiTesting.Source.Input;

namespace UiTesting.Source
{
    public class VerticalScrollBar : Slider
    {
        float _frameActualHeight = 0f;
        int _markHeight = 20;

        public bool AdjustMaxAutomatically = false;

        new public static Vector2 DefaultSize = new Vector2(30f, 0f);

        public VerticalScrollBar(SliderProps sliderProps, bool adjustMaxAutomatically = false) : base(sliderProps)
        {
            Size = sliderProps.Size;
            DoEventsIfDirectParentIsLocked = true;

            AdjustMaxAutomatically = adjustMaxAutomatically;
        }

        override protected void DoOnMouseReleased()
        {

            var mousePos = GetMousePos(p_LastScrollVal.ToVector2());

            if (mousePos.Y <= p_DrawArea.Y + _frameActualHeight)
            {
                Value = p_Value - GetStepSize();
            }

            else if (mousePos.Y >= p_DrawArea.Bottom - _frameActualHeight)
            {
                Value = p_Value + GetStepSize();
            }

            base.DoOnMouseReleased();
        }

        override protected void DoWhileMouseDown()
        {
            var mousePos = GetMousePos(p_LastScrollVal.ToVector2());

            if ((mousePos.Y >= p_DrawArea.Y + _frameActualHeight * 0.5) && (mousePos.Y <= p_DrawArea.Bottom - _frameActualHeight * 0.5))
            {
                float relativePos = (mousePos.Y - p_DrawArea.Y - _frameActualHeight * 0.5f - _markHeight * 0.5f);
                float internalHeight = (p_DrawArea.Height - _frameActualHeight) - _markHeight * 0.5f;
                float relativeVal = (relativePos / internalHeight);
                Value = (int)Math.Round(Min + relativeVal * (Max - Min));
            }

            WhileMouseDown?.Invoke(this);
        }

        override public void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            if (UiManager.GetActiveUserInterface().ActiveEntity != this)
            {
                CalcAutoMaxValue();
            }

            Texture2D texture = ContentLoader.GetTextureByName("tan_pressed");
            Texture2D markTexture = ContentLoader.GetTextureByName("tan_pressed");

            float FrameHeight = 0.14f;

            UiManager.GetActiveUserInterface().drawUtils.Draw(spriteBatch, texture, p_DrawArea, texture.Bounds, Color.White);

            Vector2 frameSizeTexture = new Vector2(texture.Width, texture.Height * FrameHeight);
            Vector2 frameSizeRender = frameSizeTexture;
            float ScaleYfac = p_DrawArea.Width / frameSizeRender.X;

            int markWidth = p_DrawArea.Width;
            _markHeight = (int)(((float)markTexture.Height / (float)markTexture.Width) * (float)markWidth);

            _frameActualHeight = FrameHeight * texture.Height * ScaleYfac;

            float markY = p_DrawArea.Y + _frameActualHeight + _markHeight * 0.5f + (p_DrawArea.Height - _frameActualHeight * 2 - _markHeight) * (GetValueAsPercent());
            Rectangle markDest = new Rectangle(p_DrawArea.X, (int)Math.Round(markY) - _markHeight / 2, markWidth, _markHeight);

            UiManager.GetActiveUserInterface().drawUtils.Draw(spriteBatch, markTexture, markDest, markTexture.Bounds, Color.DarkGray);

            base.DrawEntity(spriteBatch, true);
        }

        override protected void DoAfterUpdate()
        {
            if (p_IsInteractable &&
                (UiManager.GetActiveUserInterface().ActiveEntity == this ||
                UiManager.GetActiveUserInterface().ActiveEntity == Parent ||
                (UiManager.GetActiveUserInterface().ActiveEntity != null && UiManager.GetActiveUserInterface().ActiveEntity.IsDeepChildOf(Parent))))
            {
                if (InputUtils.MouseWheelChange != 0)
                {
                    Value = p_Value - InputUtils.MouseWheelChange * GetStepSize();
                }
            }
        }

        private void CalcAutoMaxValue()
        {
            if (AdjustMaxAutomatically)
            {
                int newMax = 0;
                int parentTop = Parent.InternalDrawArea.Y;

                foreach (var child in Parent.GetChildren())
                {
                    if (child == this) continue;

                    if (child.p_HiddenInternalEntity) continue;

                    int bottom = child.GetActualDestinationRectangle().Bottom;

                    int currNewMax = bottom - parentTop;
                    newMax = Math.Max(newMax, currNewMax);
                }

                newMax -= Parent.InternalDrawArea.Height - 4;
                newMax = Math.Max(newMax, 0);

                if (newMax != Max)
                {
                    Max = (uint)newMax;
                }

                StepsCount = (Max - Min) / 10;
            }
        }

        override protected void DoOnMouseWheelScroll()
        {
            Value = p_Value - InputUtils.MouseWheelChange * GetStepSize();
        }
    }
}
