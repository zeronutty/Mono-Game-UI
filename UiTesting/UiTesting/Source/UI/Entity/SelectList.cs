using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static UiTesting.Source.UiManager;

namespace UiTesting.Source
{
    struct ParagraphData
    {
        public SelectList list;
        public int relativeIndex;
        public ParagraphData(SelectList _list, int _relativeIndex)
        {
            list = _list;

            relativeIndex = _relativeIndex;
        }
    }

    public class SelectList : Panel
    {
        #region Fields

        string p_value = null;
        int p_index = -1;

        Point p_prevSize = Point.Zero;

        List<Paragraph> p_paragraphs = new List<Paragraph>();

        List<Image> p_ParagraphImages = new List<Image>();

        VerticalScrollBar p_scrollbar;

        bool p_hadResizeWhileNotVisible = false;

        public int ExtraSpaceBetweenLines;

        public float ItemScale = 1f;

        public EventCallback OnListChange = null;

        public bool ClipTextIfOverFlow = true;

        public string AddWhenClipping = "..";

        public bool LockSelection = false;

        public Dictionary<int, bool> LockedItems = new Dictionary<int, bool>();

        List<string> p_list = new List<string>();

        public bool AllowReselectValue = false;

        public int MaxItems = 0;

        new public static Vector2 DefaultSize = new Vector2(0f, 220f);

        private Sprite p_selectedParagraphImage;
        #endregion

        #region Properties

        public int Count { get { return p_list.Count; } }

        public bool Empty { get { return p_list.Count == 0; } }

        public string SelectedValue { get { return p_value; } set { Select(value); } }

        public int SelectedIndex { get { return p_index; } set { Select(value); } }

        public int ScrollPosition { get { return p_scrollbar.Value; } set { p_scrollbar.Value = value; } }
        #endregion

        #region Methods

        public SelectList(SelectListProps selectlistProps) : base(selectlistProps)
        {
            p_Padding = new Vector2(30, 22);

            p_scrollbar = new VerticalScrollBar(new SliderProps
            {
                EntityName = Name + ": ScrollBar",
                Min = 0,
                Max = 10,
                Size = new Vector2(10, 0),
                EntityAnchor = Anchor.CenterRight,
                LocalPosition = new Vector2(-12, 0)
            });

            p_scrollbar.Value = 0;
            p_scrollbar.Visible = false;
            p_scrollbar.p_HiddenInternalEntity = true;
            AddChild(p_scrollbar);

            p_selectedParagraphImage = selectlistProps.SelectedOptionsImageTexture;
        }

        public void OnListChanged()
        {
            if(Parent != null)
            {
                OnResize();
            }

            if(SelectedIndex >= p_list.Count)
            {
                UnSelect();
            }

            OnListChange?.Invoke(this);
        }

        public void AddItem(string value)
        {
            if(MaxItems != 0 && Count >= MaxItems) { return; }
            p_list.Add(value);
            OnListChanged();
        }

        public void AddItem(string value, int index)
        {
            if(MaxItems != 0 && Count >= MaxItems) { return; }
            p_list.Insert(index, value);
            OnListChanged();
        }

        public void RemoveItem(string value)
        {
            p_list.Remove(value);
            OnListChanged();
        }

        public void RemoveItem(int index)
        {
            p_list.RemoveAt(index);
            OnListChanged();
        }

        public void ClearItems()
        {
            p_list.Clear();
            OnListChanged();
        }

        public override bool IsNaturallyInteractable()
        {
            return true;
        }

        public void MatchHeightToList()
        {
            if (p_list.Count == 0) return;
            if (p_paragraphs.Count == 0) OnResize();
            var height = p_list.Count * (p_paragraphs[0].GetCharacterActualSize().Y / GlobalScale + p_paragraphs[0].p_SpaceAfter.Y) + p_Padding.Y * 2;
            Size = new Vector2(Size.X, height);
        }

        public void ScrollToSelected()
        {
            if(p_scrollbar != null && p_scrollbar.Visible)
            {
                p_scrollbar.Value = SelectedIndex;
            }
        }

        public void ScrollToEnd()
        {
            if (p_scrollbar != null && p_scrollbar.Visible)
            {
                p_scrollbar.Value = p_list.Count;
            }
        }

        protected virtual void OnCreateListParagraph(Paragraph paragraph)
        {

        }

        override protected void OnBeforeDraw(SpriteBatch spriteBatch)
        {
            base.OnBeforeDraw(spriteBatch);
            if(p_hadResizeWhileNotVisible)
            {
                OnResize();
            }
        }

        protected virtual void OnResize()
        {
            if(!IsVisible())
            {
                p_hadResizeWhileNotVisible = true;
                return;
            }

            p_hadResizeWhileNotVisible = false;

            ClearChildren();

            p_paragraphs.Clear();

            UpdateDestinationRects();

            int i = 0;
            while(true)
            {            
                Paragraph paragraph = DefaultParagraph(".", Anchor.Auto);
                paragraph.Name = "Option: " + i;
                paragraph.PromiscuousClicksMode = true;
                paragraph.IsTargetable = true;
                paragraph.WarpsWords = false;
                paragraph.Scale = paragraph.Scale * ItemScale;
                paragraph.p_SpaceAfter = paragraph.p_SpaceAfter + new Vector2(0, ExtraSpaceBetweenLines - 2);
                paragraph.ExtraMargin.Y = ExtraSpaceBetweenLines / 2 + 3;
                paragraph.AttachedData = new ParagraphData(this, i++);
                paragraph.UseActualSizeForCollision = false;
                paragraph.Size = new Vector2(0, (paragraph.GetCharacterActualSize().Y + ExtraSpaceBetweenLines));
                paragraph.HighlightColorPadding = new Point((int)p_Padding.X, 5);
                paragraph.HighlightColorUseBoxSize = true;
                paragraph.p_HiddenInternalEntity = true;
                paragraph.PropagateEventsTo(this);
                AddChild(paragraph);

                OnCreateListParagraph(paragraph);

                p_paragraphs.Add(paragraph);

                paragraph.OnClick += (Entity entity) =>
                {
                    ParagraphData data = (ParagraphData)entity.AttachedData;
                    if (!data.list.LockSelection)
                    {
                        data.list.Select(data.relativeIndex, true);
                    }
                };

                paragraph.UpdateDestinationRects();

                Image image = new Image(new ImageProps { texture = p_selectedParagraphImage, EntityAnchor = Anchor.TopLeft });
                float size = paragraph.Size.Y;
                image.Size = new Vector2(size, size);
                image.p_SpaceAfter = new Vector2(0, ExtraSpaceBetweenLines - 2);
                image.ExtraMargin.Y = ExtraSpaceBetweenLines / 2 + 3;
                image.SetOffset(new Vector2(-20, (paragraph.p_DrawArea.Y - p_InternalDrawArea.Y)));
                image.AttachedData = i;
                image.Visible = false;
                AddChild(image);

                p_ParagraphImages.Add(image);

                if ((paragraph.GetActualDestinationRectangle().Bottom > p_DrawArea.Bottom - ScaledPadding.Y) || i > p_list.Count)
                {
                    RemoveChild(paragraph);
                    RemoveChild(image);
                    p_paragraphs.Remove(paragraph);
                    p_ParagraphImages.Remove(image);
                    break;
                }
            }

            if (p_paragraphs.Count > 0 && p_paragraphs.Count < p_list.Count)
            {
                AddChild(p_scrollbar, false);

                p_scrollbar.Max = (uint)(p_list.Count - p_paragraphs.Count);
                if(p_scrollbar.Max < 2) { p_scrollbar.Max = 2; }
                p_scrollbar.StepsCount = p_scrollbar.Max;
                p_scrollbar.Visible = true;
            }
            else
            {
                p_scrollbar.Visible = false;
                if(p_scrollbar.Value > 0) { p_scrollbar.Value = 0; }
            }       
        }

        public void PropagateEventsTo(SelectList other)
        {
            PropagateEventsTo((Entity)other);
            OnListChange += (Entity entity) => { other.OnListChange?.Invoke(other); };
        }

        public void PropagateEventsTo(DropDown other)
        {
            PropagateEventsTo((Entity)other);
            OnListChange += (Entity entity) => { other.OnListChange?.Invoke(other); };
        }

        public void UnSelect()
        {
            Select(-1, false);
        }

        protected void Select(string value)
        {
            if(!AllowReselectValue && value == p_value) { return; }

            if(value == null)
            {
                p_value = value;
                p_index = -1;
                DoOnValueChange();
                return;
            }

            p_index = p_list.IndexOf(value);
            if(p_index == -1)
            {
                p_value = null;
            }

            p_value = value;

            DoOnValueChange();
        }

        protected void Select(int index, bool relativToScrollbar = false)
        {
            if(relativToScrollbar)
            {
                index += p_scrollbar.Value;
            }

            if(!AllowReselectValue && index == p_index) { return; }

            if(index >= -1 && index >= p_list.Count)
            {
                return;
            }

            p_value = index > -1 ? p_list[index] : null;
            p_index = index;

            DoOnValueChange();
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            if((p_prevSize.Y != p_InternalDrawArea.Size.Y) || p_hadResizeWhileNotVisible)
            {
                OnResize();
            }

            p_prevSize = p_InternalDrawArea.Size;

            base.DrawEntity(spriteBatch);

            for(int i = 0; i < p_paragraphs.Count; i++)
            {
                int item_index = i + (int)p_scrollbar.Value;

                var par = p_paragraphs[i];
                var img = p_ParagraphImages[i];

                if(item_index < p_list.Count)
                {
                    par.Text = p_list[item_index];
                    par.HighlightColor.A = 0;
                    par.Visible = true;
                    img.Visible = false;

                    if (ClipTextIfOverFlow)
                    {
                        var charWidth = par.GetCharacterActualSize().X;
                        var toClip = (charWidth * par.Text.Length) - p_InternalDrawArea.Width;
                        if (toClip > 0)
                        {
                            var charsToClip = (int)System.Math.Ceiling(toClip / charWidth) + AddWhenClipping.Length + 1;

                            if (charsToClip < par.Text.Length)
                            {
                                par.Text = par.Text.Substring(0, par.Text.Length - charsToClip) + AddWhenClipping;
                            }
                            else
                            {
                                par.Text = AddWhenClipping;
                            }
                        }
                    }

                    bool isLocked = false;
                    LockedItems.TryGetValue(item_index, out isLocked);
                    par.Locked = isLocked;

                    Paragraph paragraph = p_paragraphs[i];
                    if (paragraph.State == EntityState.MouseHover)
                    {
                        Rectangle drawArea = paragraph.GetActualDestinationRectangle();
                        paragraph.State = EntityState.MouseDown;
                        paragraph.HighlightColor = Color.Black;
                        paragraph.HighlightColor *= 0.25f;
                    }
                }
                else
                {
                    par.Visible = false;
                    par.Text = string.Empty;
                }
            }

           

            int SelectedParagraphIndex = p_index;
            if(SelectedParagraphIndex != -1)
            {
                int i = SelectedParagraphIndex - p_scrollbar.Value;
                if(i >= 0 && i < p_paragraphs.Count)
                {
                                    
                    Image image = p_ParagraphImages[i];
                    image.Visible = true;
                }
            }
        }

        #endregion
    }
}
