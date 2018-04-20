using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    public class ProgressBar : Slider
    {
        #region Fields

        new public static Vector2 DefaultSize = new Vector2(0, 52f);

        public Image ProgressFill;

        public Sprite BarTexture;

        public Paragraph Caption;


        #endregion

        #region Properties
        #endregion

        #region Methods


        public ProgressBar(ProgressBarProps progressBarProps) : base(progressBarProps)
        {
            OverlayColor = Color.White;
            p_size.X = 0;
            p_size.Y = 50;

            BarTexture = progressBarProps.BarTexture;

            p_Padding = Vector2.Zero;
            ProgressFill = new Image(new ImageProps { texture = progressBarProps.FillTexture, DrawMode = SpriteDrawMode.Panel, EntityAnchor = Anchor.CenterLeft, OverlayColor = Color.Green});
            ProgressFill.p_HiddenInternalEntity = true;
            AddChild(ProgressFill, true);

            Caption = new Paragraph(new ParagraphProps { Text = progressBarProps.CaptionText, EntityAnchor = Anchor.Center, OverlayColor = Color.Black});
            Caption.ClickThrough = true;
            Caption.p_HiddenInternalEntity = true;
            AddChild(Caption);
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            float ProgressbarFrameWidth = 0;

            BarTexture.Draw(spriteBatch, SpriteDrawMode.Panel, p_DraggableArea, OverlayColor);

            Vector2 frameSizeTexture = new Vector2(BarTexture.Bounds.Width * ProgressbarFrameWidth, BarTexture.Bounds.Height);
            Vector2 frameSizeRender = frameSizeTexture;
            float ScaleXfac = p_DrawArea.Height / frameSizeRender.Y;

            p_FrameActualWidth = ProgressbarFrameWidth * BarTexture.Bounds.Width * ScaleXfac;

            int markWidth = (int)((p_DrawArea.Width - p_FrameActualWidth * 2) * GetValueAsPercent());
            ProgressFill.SetOffset(new Vector2(p_FrameActualWidth / GlobalScale, 0));
            ProgressFill.Size = new Vector2(markWidth, p_InternalDrawArea.Height) / GlobalScale;
            ProgressFill.Visible = markWidth > 0;

            float percent = GetValueAsPercent() * 100;

            Caption.Text = percent + "%";

            base.DrawEntity(spriteBatch, true);
        }

        #endregion
    }
}
