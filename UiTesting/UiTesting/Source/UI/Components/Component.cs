using System;

using Microsoft.Xna.Framework;

namespace UiTesting.Source.UI.Components
{
    public class Component : Disposable
    {
        #region Fields

        private UiManager2 p_UiManager2 = null;
        private bool p_Initialized = false;

        #endregion

        #region Properties

        public virtual UiManager2 UiManager2 { get { return p_UiManager2; } set { p_UiManager2 = value; } }
        public virtual bool Initialized { get { return p_Initialized; } }

        #endregion

        #region Constructors

        public Component(UiManager2 uiManager2)
        {
            if(uiManager2 != null)
            {
                p_UiManager2 = uiManager2;
            }
            else
            {
                throw new Exception("Component cannot be created without a UiManager");
            }
        }

        #endregion

        #region Destructors

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {

            }
            base.Dispose(disposing);
        }

        #endregion

        #region Methods

        public virtual void Init()
        {
            p_Initialized = true;
        }

        protected internal virtual void Update(GameTime gameTime)
        {

        }

        #endregion
    }
}
