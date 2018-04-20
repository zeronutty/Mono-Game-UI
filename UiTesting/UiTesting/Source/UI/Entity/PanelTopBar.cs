using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    public class PanelTopBar : Entity
    {
        #region Fields

        private Sprite p_BackgroundSprite;

        private Button p_ExitButton;
        private Button p_MinimizeButton;
        private Button p_MaximizeButton;

        private Button p_ScalePlusButton;
        private Button p_ScaleMinusButton;

        private Text p_HeaderText;

        private List<Entity> InternalChildren = new List<Entity>();
        #endregion

        #region Properties
        #endregion

        #region Methods
        public PanelTopBar(PanelTopBarProps panelTopBarProps) : base (panelTopBarProps)
        {
            p_ExitButton = panelTopBarProps.ExitButton;
            p_MinimizeButton = panelTopBarProps.MinimizeButton;
            p_MaximizeButton = panelTopBarProps.MaximizeButton;

            p_ScaleMinusButton = panelTopBarProps.ScaleMinus;
            p_ScalePlusButton = panelTopBarProps.ScalePlus;

            p_HeaderText = panelTopBarProps.Header;

            p_BackgroundSprite = panelTopBarProps.BackGroundTexture;

            AddChild(p_ExitButton);
            AddChild(p_MinimizeButton);
            AddChild(p_MaximizeButton);
            AddChild(p_ScaleMinusButton);
            AddChild(p_ScalePlusButton);
            AddChild(p_HeaderText);

            p_ScaleMinusButton.OnClick = (Entity entity) => { LocalScale -= 0.1f; };
            p_ScalePlusButton.OnClick = (Entity entity) => { LocalScale += 0.1f; };

            ScaledPadding = new Vector2(5, 5);

            DrawingExternally = true;
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            p_BackgroundSprite.Draw(spriteBatch, SpriteDrawMode.Panel, p_DrawArea, OverlayColor);

            base.DrawEntity(spriteBatch);
        }

        public override Rectangle CalculateExternalRectangle()
        {
            p_ExternalDrawArea = new Rectangle(p_DrawArea.X, p_DrawArea.Bottom, p_DrawArea.Width, 500);

            return base.CalculateExternalRectangle();
        }

        public override Entity AddChild(Entity child, bool inheritParentState = false, int index = -1)
        {
            if(Children.Count >= 6)
            {
                child.DrawExternally = true;
            }

            return base.AddChild(child, inheritParentState, index);
        }
        #endregion
    }
}
