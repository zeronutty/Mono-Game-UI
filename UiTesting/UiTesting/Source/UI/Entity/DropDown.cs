using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiTesting.Source.Input;
using static UiTesting.Source.UiManager;

namespace UiTesting.Source
{
    public class DropDown : Entity
    {
        #region Feilds

        private string p_PlaceHolderText = "Click to Select";

        new public static Vector2 DefaultSize = new Vector2(0, 30f);

        Panel p_SelectedTextPanel;
        Paragraph p_SelectedTextParagraph;
        Image p_ArrowDownImage;

        SelectList p_SelectList;

        public static int SelectedPanelHeight = 30;

        public bool AutoSetListHeight = false;

        public static int ArrowSize = 16;

        public EventCallback OnListChange = null;
        #endregion

        #region Properties
        public string DefaultText
        {
            get { return p_PlaceHolderText; }
            set { p_PlaceHolderText = value; if (SelectedIndex == -1) p_SelectedTextParagraph.Text = p_PlaceHolderText; }
        }

        public Panel SelectedTextPanel { get { return p_SelectedTextPanel; } }
        public bool AllowReselectValue { get { return p_SelectList.AllowReselectValue; } set { p_SelectList.AllowReselectValue = value; } }
        public SelectList SelectList { get { return p_SelectList; } }
        public Paragraph SelectedTextPanelParagraph { get { return p_SelectedTextParagraph; } }
        public Image ArrowDownImage { get { return p_ArrowDownImage; } }

        public bool ListVisible
        {
            get { return p_SelectList.Visible; }
            set { p_SelectList.Visible = value; OnDropDownVisibilityChange(); }
        }

        public override int Priority
        {
            get { return 100 - p_indexInParent; }
        }

        public string SelectedValue { get { return p_SelectList.SelectedValue; } set { p_SelectList.SelectedValue = value; } }

        public int SelectedIndex { get { return p_SelectList.SelectedIndex; } set { p_SelectList.SelectedIndex = value; } }

        public int ScrollPosition { get { return p_SelectList.ScrollPosition; } set { p_SelectList.ScrollPosition = value; } }

        public int Count { get { return p_SelectList.Count; } }

        public bool Empty { get { return p_SelectList.Empty; } }

        #endregion

        #region Methods
        public DropDown(DropDownProps dropDownProps) : base(dropDownProps)
        {
            float DropDownPanelHeight = dropDownProps.DropDownPanelHeight != 0 ? dropDownProps.DropDownPanelHeight : 220f;

            p_Padding = Vector2.Zero;

            UseActualSizeForCollision = true;

            p_SelectedTextPanel = new Panel(new PanelProps { EntityName = Name + ": SelectedPanel", Size = dropDownProps.Size, EntityAnchor = Anchor.TopLeft });
            p_SelectedTextParagraph = DefaultParagraph(string.Empty, Anchor.CenterLeft);
            p_SelectedTextParagraph.UseActualSizeForCollision = false;
            p_SelectedTextPanel.AddChild(p_SelectedTextParagraph, true);
            p_SelectedTextPanel.p_HiddenInternalEntity = true;

            p_ArrowDownImage = new Image(new ImageProps
            {
                EntityName = p_SelectedTextPanel.Name + ": ArrowImage",
                texture = SpritesData.DD_DownArrowSprite,
                DrawMode = SpriteDrawMode.Stretch,
                EntityAnchor = Anchor.CenterRight,
                Size = new Vector2(ArrowSize, ArrowSize),
                LocalPosition = new Vector2(0, 0),
            });

            p_SelectedTextPanel.AddChild(p_ArrowDownImage, true);
            p_ArrowDownImage.p_HiddenInternalEntity = true;
            p_ArrowDownImage.Visible = dropDownProps.ShowArrow;

            p_SelectList = new SelectList( new SelectListProps
                {
                    Size = new Vector2(0, DropDownPanelHeight),
                    EntityAnchor = Anchor.TopCenter,
                    LocalPosition = Vector2.Zero,
                    SelectedOptionsImageTexture = dropDownProps.SelectedOptionImage
                });

            p_SelectList.SetOffset(new Vector2(0, SelectedPanelHeight));
            p_SelectList.p_SpaceBefore = Vector2.Zero;
            p_SelectList.p_HiddenInternalEntity = true;

            AddChild(p_SelectedTextPanel);
            AddChild(p_SelectList);

            p_SelectList.OnValueChange = (Entity entity) =>
            {
                ListVisible = false;

                p_SelectedTextParagraph.Text = (SelectedValue ?? DefaultText);
            };

            //p_SelectList.OnClick = (Entity entity) =>
            //{
            //    ListVisible = false;
            //};

            p_SelectList.Visible = false;

            //p_SelectedTextPanel.OnClick = (Entity self) =>
            //{
            //    ListVisible = !ListVisible;
            //};

            p_SelectedTextParagraph.Text = (SelectedValue ?? DefaultText);

            p_SelectList.PropagateEventsTo(this);

            p_SelectedTextPanel.PropagateEventsTo(this);
        }

        protected override void DoOnValueChange()
        {
            ListVisible = false;
            base.DoOnValueChange();
        }

        protected override Rectangle GetDrawRectForAutoAnchors()
        {
            p_SelectedTextPanel.UpdateDestinationRectsIfNeeded();
            return base.GetDrawRectForAutoAnchors();
        }
        
        override public bool IsPointInsideEntity(Vector2 point)
        {
            point += p_LastScrollVal.ToVector2();

            Rectangle rect;

            if(ListVisible)
            {
                p_SelectList.UpdateDestinationRectsIfNeeded();
                rect = p_SelectList.GetActualDestinationRectangle();
                rect.Height += SelectedPanelHeight;
                rect.Y -= SelectedPanelHeight;
            }
            else
            {
                p_SelectedTextPanel.UpdateDestinationRectsIfNeeded();
                rect = p_SelectedTextPanel.GetActualDestinationRectangle();
            }

            return (point.X >= rect.Left && point.X <= rect.Right &&
                    point.Y >= rect.Top && rect.Y <= rect.Bottom);
        }

        private void OnDropDownVisibilityChange()
        {
            p_ArrowDownImage.Texture = ListVisible ? SpritesData.DD_UpArrowSprite : SpritesData.DD_DownArrowSprite;

            p_SelectList.IsFocused = true;
            GetActiveUserInterface().ActiveEntity = p_SelectList;

            p_SelectList.UpdateDestinationRects();

            if (p_SelectList.Visible) p_SelectList.ScrollToSelected();

            MarkasRectUpdate();

            if(AutoSetListHeight)
            {
                p_SelectList.MatchHeightToList();
            }
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            base.DrawEntity(spriteBatch);
        }

        protected override void DoAfterUpdate()
        {
            if(ListVisible)
            {
                var mousePosition = GetMousePos();
                if(InputUtils.IsAnyMouseButtonPressed() && !IsPointInsideEntity(mousePosition))
                {
                    if(!IsPointInsideEntity(mousePosition))
                    {
                        ListVisible = false;
                    }
                }
            }

            base.DoAfterUpdate();
        }

        public void Unselect()
        {
            p_SelectList.UnSelect();
        }

        public void AddItem(string value)
        {
            p_SelectList.AddItem(value);
        }

        public void AddItem(string value, int index)
        {
            p_SelectList.AddItem(value, index);
        }

        public void RemoveItem(string value)
        {
            p_SelectList.RemoveItem(value);
        }

        public void RemoveItem(int index)
        {
            p_SelectList.RemoveItem(index);
        }

        public void ClearItems()
        {
            p_SelectList.ClearItems();
        }

        public override bool IsNaturallyInteractable()
        {
            return true;
        }

        public void ScrollToSelected()
        {
            p_SelectList.ScrollToSelected();
        }

        public void ScrollToEnd()
        {
            p_SelectList.ScrollToEnd();
        }
        #endregion
    }
}
