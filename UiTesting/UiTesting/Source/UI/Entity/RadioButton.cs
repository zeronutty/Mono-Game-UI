using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    public class RadioButton : CheckBox
    {
        #region Fields

        new public static Vector2 DefaultSize = new Vector2(0f, 50f);

        public bool CanUnCheck = false;

        #endregion

        #region Properties
        #endregion

        #region Methods

        public RadioButton(RadioButtonProps radioButtonProps) : base(radioButtonProps)
        {

        }

        protected override void DoOnValueChange()
        {
            if(!Checked)
            {
                return;
            }

            if (Parent != null)
            {
                foreach (Entity e in Parent.GetChildren())
                {
                    if (e == this)
                    {
                        continue;
                    }

                    if (e is RadioButton)
                    {
                        RadioButton radio = (RadioButton)e;
                        if (radio.Checked) { radio.Checked = false; }
                    }
                }
            }

            base.DoOnValueChange();
        }

        protected override void DoOnClick()
        {
            base.DoOnClick();

            if(!CanUnCheck)
            {
                Checked = true;
            }
        }

        #endregion
    }
}
