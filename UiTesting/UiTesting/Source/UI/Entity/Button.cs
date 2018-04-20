using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace UiTesting.Source
{
    public class Button : Entity
    {
        #region Fields
        private bool p_Checked = false;

        public bool ToggleMode = false;

        private Sprite p_ClickedSprite;
        private Sprite p_BackgroundSprite;

        new public static Vector2 DefaultSize = new Vector2(0f, 30f);
        #endregion

        #region Properties

        public bool Clicked { get; private set; }

        public Color HoverColor { get; set; }

        public bool Checked { get { return p_Checked == true; } set { p_Checked = value; DoOnValueChange(); } }
        #endregion

        #region Methods

        public Button(ButtonProps buttonProps) : base(buttonProps)
        {
            IsDraggable = false;

            OverlayColor = buttonProps.OverlayColor != Color.Transparent ? buttonProps.OverlayColor : Color.White;
            HoverColor = buttonProps.HoverColor != Color.Transparent ? buttonProps.HoverColor : Color.LightGray;

            p_ClickedSprite = buttonProps.ClickedTexture;
            p_BackgroundSprite = buttonProps.Texture;

            if(buttonProps.Icon != null)
            {
                AddChild(buttonProps.Icon);
            }

            ScaledPadding = new Vector2(5, 5);
        }

        override protected void DoOnClick()
        {
            if(ToggleMode)
            {
                Checked = !Checked;
            }

            base.DoOnClick();
        }

        public override bool IsNaturallyInteractable()
        {
            return true;
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            Color color;

            if (Checked) { State = EntityState.MouseDown; }

            if (State == EntityState.MouseHover)
                color = HoverColor;
            else
                color = OverlayColor;

            if (State == EntityState.MouseDown && p_ClickedSprite != null)
            {
                p_ClickedSprite.Draw(spriteBatch, SpriteDrawMode.Stretch, p_DrawArea, color);
            }
            else
            {
                p_BackgroundSprite.Draw(spriteBatch, SpriteDrawMode.Stretch, p_DrawArea, color);
            }

            base.DrawEntity(spriteBatch);
        }
        #endregion
    }
}
