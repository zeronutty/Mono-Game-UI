using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    public class CheckBox : Entity
    {
        #region Fields

        public Paragraph TextParagraph;

        protected bool p_value = false;

        private Color p_CheckedSpriteOverlayColor = Color.LightGray;

        public Sprite CheckedSprite { get; set; }
        public Color CheckedSpriteOverlayColor { get { if (p_CheckedSpriteOverlayColor == Color.Transparent) { return Color.White; } else return p_CheckedSpriteOverlayColor; } set { p_CheckedSpriteOverlayColor = value; } }

        static Vector2 CHECKBOX_SIZE = new Vector2(16, 16);

        new public static Vector2 DefaultSize = new Vector2(0f, 40f);
        #endregion

        #region Properties
        public bool Checked { get { return p_value == true; } set { p_value = value; DoOnValueChange(); } }

        public override bool IsNaturallyInteractable()
        {
            return true;
        }
        #endregion

        #region Methods
        public CheckBox(CheckBoxProps checkBoxProps) : base(checkBoxProps)
        {
            TextParagraph = UiManager.DefaultParagraph(checkBoxProps.Text, Anchor.Auto);
            TextParagraph.Size = new Vector2(0, 10);
            TextParagraph.SetOffset(new Vector2(25, 0));
            TextParagraph.p_HiddenInternalEntity = true;
            AddChild(TextParagraph, true);

            PromiscuousClicksMode = true;

            CHECKBOX_SIZE = checkBoxProps.CheckBoxSize != Vector2.Zero ? checkBoxProps.CheckBoxSize : new Vector2(16, 16);
            Checked = checkBoxProps.IsChecked;
        }

        virtual protected Sprite GetSprite()
        {
            if(Checked)
            {
                return SpritesData.CB_Checked;
            }
            else
            {
                return SpritesData.CB_UnChecked;
            }
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseBraw = null)
        {
            CheckedSprite = GetSprite();

            Vector2 actualSize = CHECKBOX_SIZE * GlobalScale;

            Rectangle dest = new Rectangle(p_DrawArea.X,
                                        (int)(p_DrawArea.Y + actualSize.Y / 2),
                                        (int)actualSize.X,
                                        (int)actualSize.Y);

            CheckedSprite.Draw(spriteBatch,SpriteDrawMode.Stretch, dest, CheckedSpriteOverlayColor);
            base.DrawEntity(spriteBatch);
        }

        protected override void DoOnClick()
        {
            Checked = !p_value;

            base.DoOnClick();
        }
        #endregion

    }
}
