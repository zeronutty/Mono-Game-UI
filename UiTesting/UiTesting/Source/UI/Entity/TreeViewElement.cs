using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    public class TreeViewElement : CheckBox
    {
        private Vector2 p_previousSize;

        public TreeViewElement(CheckBoxProps checkBoxProps) : base(checkBoxProps)
        {
            p_previousSize = checkBoxProps.Size;
            p_SpaceAfter = new Vector2(0, 2);
            p_SpaceBefore = new Vector2(0, 2);

            p_LayoutState = LayoutState.SizeToContent;
        }

        protected override void DoOnClick()
        {
            base.DoOnClick();

            if (GetChildren().Count > 1)
            {
                foreach (Entity e in GetChildren(true))
                {
                    e.SetOffset(new Vector2(30, 0));

                    e.Visible = Checked;
                }
            }
        }

        protected override Sprite GetSprite()
        {
            if (GetChildren().Count > 1)
            {
                if (Checked)
                {
                    return SpritesData.TV_Checked;
                }
                else
                {
                    return SpritesData.TV_UnChecked;
                }
            }
            else
            {
                return SpritesData.I_ScaleMinusButton;
            }
        }

        public override Entity AddChild(Entity child, bool inheritParentState = false, int index = -1)
        {
            child.Visible = Checked;

            TextParagraph.Visible = true;

            return base.AddChild(child, inheritParentState, index);          
        }
    }
}
