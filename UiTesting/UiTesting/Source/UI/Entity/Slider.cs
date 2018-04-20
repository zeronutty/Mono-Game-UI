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
    public class Slider : Entity
    {
        protected uint p_Min;

        protected uint p_Max;

        protected uint p_StepsCount = 0;

        protected int p_Value;

        protected float p_FrameActualWidth = 0f;

        protected int p_MarkWidth = 20;

        new public static Vector2 DefaultSize = new Vector2(0f, 30f);


        public Slider(SliderProps sliderPorps) : base(sliderPorps)
        {
            Size = new Vector2(-1, -1);
            Min = sliderPorps.Min;
            Max = sliderPorps.Max;

            p_StepsCount = Max - Min;

            p_Value = (int)(Min + (Max - Min) / 2);
        }

        public int GetStepSize()
        {
            if (StepsCount > 0)
            {
                if (Max - Min == StepsCount)
                {
                    return 1;
                }
                return (int)System.Math.Max(((Max - Min) / StepsCount + 1), 2);
            }
            else
            {
                return 1;
            }
        }

        protected int NormalizeValue(int value)
        {
            float stepSize = (float)GetStepSize();
            value = (int)(System.Math.Round(((double)value) / stepSize) * stepSize);

            value = (int)System.Math.Min(System.Math.Max(value, Min), Max);

            return value;
        }

        public int Value
        {
            get { return p_Value; }

            set
            {
                int prevVal = p_Value;
                p_Value = NormalizeValue(value);
                if (prevVal != p_Value) { DoOnValueChange(); }
            }
        }

        public uint Min
        {
            get { return p_Min; }
            set { if (p_Min != value) { p_Min = value; Value = Value; } }
        }

        public uint Max
        {
            get
            {
                return p_Max;
            }
            set
            {
                if (p_Max != value)
                {
                    p_Max = value;

                    if (Value > Max) Value = (int)Max;
                }
            }
        }

        public uint StepsCount
        {
            get { return p_StepsCount; }

            set { p_StepsCount = value; Value = Value; }
        }

        override public bool IsNaturallyInteractable()
        {
            return true;
        }

        override protected void DoWhileMouseDown()
        {
            var mousePos = GetMousePos();
            mousePos += p_LastScrollVal.ToVector2();

            if (mousePos.X <= p_DrawArea.X + p_FrameActualWidth)
            {
                Value = (int)Min;
            }
            else if (mousePos.X >= p_DrawArea.Right - p_FrameActualWidth)
            {
                Value = (int)Max;
            }
            else
            {
                float val = ((mousePos.X - p_DrawArea.X - p_FrameActualWidth + p_MarkWidth / 2) / (p_DrawArea.Width - p_FrameActualWidth * 2));
                Value = (int)(Min + val * (Max - Min));
            }

            base.DoWhileMouseDown();
        }

        public float GetValueAsPercent()
        {
            return (float)(p_Value - Min) / (float)(Max - Min);
        }

        override public void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            if (BaseDraw == null)
            {
                Texture2D texture = ContentLoader.GetTextureByName("tan_pressed");
                Texture2D markTexture = ContentLoader.GetTextureByName("tan_pressed");

                float frameWidth = 0.03f;

                UiManager.GetActiveUserInterface().drawUtils.Draw(spriteBatch, texture, p_DrawArea, texture.Bounds ,Color.White);

                Vector2 frameSizeTexture = new Vector2(texture.Width * frameWidth, texture.Height);
                Vector2 frameSizeRender = frameSizeTexture;
                float ScaleXfac = p_DrawArea.Height / frameSizeRender.Y;

                int markHeight = p_DrawArea.Height;
                p_MarkWidth = (int)(((float)markTexture.Width / (float)markTexture.Height) * (float)markHeight);

                p_FrameActualWidth = frameWidth * texture.Width * ScaleXfac;

                float markX = p_DrawArea.X + p_FrameActualWidth + p_MarkWidth * 0.5f + (p_DrawArea.Width - p_FrameActualWidth * 2 - p_MarkWidth) * GetValueAsPercent();
                Rectangle markDest = new Rectangle((int)System.Math.Round(markX) - p_MarkWidth / 2, p_DrawArea.Y, p_MarkWidth, markHeight);

                UiManager.GetActiveUserInterface().drawUtils.Draw(spriteBatch, markTexture, markDest, markTexture.Bounds, Color.White);

                base.DrawEntity(spriteBatch);
            }
            else { base.DrawEntity(spriteBatch); }
        }

        override protected void DoOnMouseWheelScroll()
        {
            Value = p_Value + InputUtils.MouseWheelChange * GetStepSize();
        }
    }
}
