using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    public class RootPanel : Entity
    {
        public RootPanel() : base(new EntityProps { EntityName = "Root", Size = Vector2.Zero, LocalPosition = Vector2.Zero, EntityAnchor = Anchor.Center })
        {
            ClickThrough = true;
            p_Padding = Vector2.Zero;
        }

        public override Rectangle CalculateDestinationRectangle()
        {
            int width = UiManager.GetActiveUserInterface().ScreenWidth;
            int height = UiManager.GetActiveUserInterface().ScreenHeight;

            return new Rectangle(0, 0, width, height);
        }

        public override void UpdateDestinationRectsIfNeeded()
        {
            if(p_NeedsRectUpdate)
            {
                UpdateDestinationRects();
            }
        }

        public override void Update(ref Entity targetEntity, ref Entity dragTargetEntity, ref bool wasEventHandled, GameTime gameTime, Point ScrollVal)
        {
            base.Update(ref targetEntity, ref dragTargetEntity, ref wasEventHandled, gameTime, ScrollVal);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsHidden)
                return;

            List<Entity> childrenSorted = GetSortedChildren();

            foreach(Entity child in childrenSorted)
            {
                child.Draw(spriteBatch);
            }
        }
    }
}
