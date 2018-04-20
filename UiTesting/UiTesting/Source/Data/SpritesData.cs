using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    public static class SpritesData
    {
        #region Feilds

        #region PanelSprites
        /// <summary>
        /// Panel, Back ground sprite
        /// </summary>
        public static Sprite P_BGSprite = new Sprite(ContentLoader.GetTextureByName("tan_pressed"));

        #endregion

        #region ButtonSprites
        /// <summary>
        /// Button Back Ground Sprite
        /// </summary>
        public static Sprite B_BGSprite = new Sprite(ContentLoader.GetTextureByName("Tan"));

        /// <summary>
        /// Button Clicked Sprite
        /// </summary>
        public static Sprite B_CLSprite = new Sprite(ContentLoader.GetTextureByName("tan_pressed"));

        /// <summary>
        /// Button Hover Sprite
        /// </summary>
        public static Sprite B_HSprite = new Sprite(ContentLoader.GetTextureByName(""));
        #endregion

        #region TopBarPanelSprites
        /// <summary>
        /// Top Bar panel Back ground Sprite
        /// </summary>
        public static Sprite TB_BGSprite = new Sprite(ContentLoader.GetTextureByName("tan_pressed"));

        #endregion

        #region DropDownSprites
        /// <summary>
        /// Drop down Down Arrow sprite
        /// </summary>
        public static Sprite DD_DownArrowSprite = new Sprite(ContentLoader.GetTextureByName("DownArrow"));

        /// <summary>
        /// Drop down Up Arrow sprite
        /// </summary>
        public static Sprite DD_UpArrowSprite = new Sprite(ContentLoader.GetTextureByName("UpArrow"));

        /// <summary>
        /// Drop down selected option Sprite
        /// </summary>
        public static Sprite DD_SeletedOptionSprite = new Sprite(ContentLoader.GetTextureByName("CircleButton"));
        #endregion

        #region IconSprites

        /// <summary>
        /// Top Bar Panel, Exit button icon
        /// </summary>
        public static Sprite I_ExitButton = new Sprite(ContentLoader.GetTextureByName("ExitButton"));

        /// <summary>
        /// Top Bar Panel, Scale plus button icon
        /// </summary>
        public static Sprite I_ScalePlusButton = new Sprite(ContentLoader.GetTextureByName("ScalePlus"));

        /// <summary>
        /// Top Bar Panel, Scale minus button icon
        /// </summary>
        public static Sprite I_ScaleMinusButton = new Sprite(ContentLoader.GetTextureByName("ScaleMinus"));

        /// <summary>
        /// Top Bar Panel, Maximize Panel button icon
        /// </summary>
        public static Sprite I_MaximizeButton = new Sprite(ContentLoader.GetTextureByName("MaximizeButton"));

        /// <summary>
        /// Top Bar Panel, Minimize panel button icon
        /// </summary>
        public static Sprite I_MinimizeButton = new Sprite(ContentLoader.GetTextureByName("MinimizeButton"));

        #endregion

        #region ProgressbarSprites

        /// <summary>
        /// Progress Bar, Back ground bar sprite
        /// </summary>
        public static Sprite PB_BGSprite = new Sprite(ContentLoader.GetTextureByName("Panel"));

        /// <summary>
        /// Progress bar, progress bar sprite
        /// </summary>
        public static Sprite PB_PSprite = new Sprite(ContentLoader.GetTextureByName("Panel"));

        #endregion

        #region CheckBoxSprites

        public static Sprite CB_Checked = new Sprite(ContentLoader.GetTextureByName("CheckBoxMarked"));

        public static Sprite CB_UnChecked = new Sprite(ContentLoader.GetTextureByName("CheckBoxUnMarked"));

        #endregion

        #region TreeViewSprites

        public static Sprite TV_Checked = new Sprite(ContentLoader.GetTextureByName("TV_RightArrow"));

        public static Sprite TV_UnChecked = new Sprite(ContentLoader.GetTextureByName("TV_DownArrow"));

        #endregion

        #region TextBoxSprites

        public static Sprite TB_Caret = new Sprite(ContentLoader.GetTextureByName("Caret"));

        #endregion

        #endregion
    }
}
