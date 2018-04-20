using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    public class PanelTabs : Entity
    {
        #region Fields
        private List<TabData> p_tabs = new List<TabData>();

        private ContainerPanel p_buttonsPanel;

        private ContainerPanel p_panelsPanel;

        private ContainerPanel p_internalRoot;

        TabData p_ActiveTab = null;
        #endregion

        #region Properties
        public TabData ActiveTab
        {
            get { return p_ActiveTab; }
        }
        #endregion

        #region Methods
        public PanelTabs(EntityProps entityProps) : base(entityProps)
        {
            p_Padding = Vector2.Zero;

            p_internalRoot = new ContainerPanel(new EntityProps { EntityName = Name + ": Root", Size = Vector2.Zero,  EntityAnchor = Anchor.TopCenter });
            p_internalRoot.p_SpaceBefore = p_internalRoot.p_SpaceAfter = p_internalRoot.Padding = Vector2.Zero;
            AddChild(p_internalRoot);

            p_buttonsPanel = new ContainerPanel(new EntityProps { EntityName = Name + ": Button", Size = Vector2.Zero, EntityAnchor = Anchor.TopCenter });
            p_buttonsPanel.p_SpaceBefore = p_buttonsPanel.p_SpaceAfter = p_buttonsPanel.Padding = Vector2.Zero;
            p_internalRoot.AddChild(p_buttonsPanel);

            p_panelsPanel = new ContainerPanel(new EntityProps { EntityName = Name + ": Panel", Size = Vector2.Zero, EntityAnchor = Anchor.TopCenter, LocalPosition = new Vector2(0,0)});
            p_panelsPanel.p_SpaceBefore = p_panelsPanel.p_SpaceAfter = p_panelsPanel.Padding = Vector2.Zero;
            p_internalRoot.AddChild(p_panelsPanel);

            p_panelsPanel.p_HiddenInternalEntity = true;
            p_buttonsPanel.p_HiddenInternalEntity = true;
            p_internalRoot.p_HiddenInternalEntity = true;
        }

        private float GetButtonsHeight(bool withGlobalScale)
        {
            if (p_tabs.Count == 0) return 0;
            return (p_tabs[0].button.GetActualDestinationRectangle().Height / (withGlobalScale ? 1f : GlobalScale) / LocalScale);
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            p_internalRoot.Padding = -Parent.Padding;

            float buttonHeight = GetButtonsHeight(false);
            p_panelsPanel.SetOffset(new Vector2(0, buttonHeight));

            var parentSize = GetActualDestinationRectangle().Size;
            p_internalRoot.Size = new Vector2(parentSize.X, parentSize.Y - GetButtonsHeight(true)) / GlobalScale / LocalScale;

            base.DrawEntity(spriteBatch);
        }

        public void SelectTab(string name)
        {
            foreach(TabData tab in p_tabs)
            {
                if(tab.name == name)
                {
                    tab.button.Checked = true;
                    return;
                }
            }
        }

        public TabData AddTab(string name)
        {
            Panel newPanel = new Panel(new PanelProps { Size = Vector2.Zero, EntityAnchor = Anchor.TopCenter });
            Button newButton = new Button(new ButtonProps { Texture = SpritesData.B_BGSprite, ClickedTexture = SpritesData.B_CLSprite, EntityName = name, EntityAnchor = Anchor.AutoInLine, Size = new Vector2(-1,-1)});

            TabData newTab = new TabData(name, newPanel, newButton);

            newTab.panel.AttachedData = newTab;

            p_tabs.Add(newTab);

            float width = 1f / (float)p_tabs.Count;
            if(width == 1) { width = 0; }
            foreach(TabData data in p_tabs)
            {
                data.button.Size = new Vector2(width, data.button.Size.Y);
            }

            newTab.button.ToggleMode = true;
            newTab.button.Checked = false;

            newTab.button.Name = "Tab-Button" + name;
            newTab.panel.Name = "Tab-Panel" + name;

            newTab.panel.Visible = false;

            newTab.button.OnValueChange = (Entity entity) =>
            {
                Button self = (Button)(entity);

                Panel prevActive = p_ActiveTab != null ? p_ActiveTab.panel : null;
                p_ActiveTab = null;

                if(self.Checked)
                {
                    foreach(TabData data in p_tabs)
                    {
                        Button iterButton = data.button;
                        if(iterButton != self && iterButton.Checked)
                        {
                            iterButton.Checked = false;
                        }
                    }
                }

                Panel selfPanel = p_panelsPanel.Find<Panel>("Tab-Panel" + name);

                selfPanel.Visible = self.Checked;

                if(self.Checked)
                {
                    p_ActiveTab = (TabData)selfPanel.AttachedData;
                }

                if(p_ActiveTab == null && prevActive == selfPanel)
                {
                    self.Checked = true;
                }

                if (self.Checked)
                {
                    DoOnValueChange();
                }
            };

            p_panelsPanel.AddChild(newTab.panel);
            p_buttonsPanel.AddChild(newTab.button);

            if(p_tabs.Count == 1)
            {
                newTab.button.Checked = true;
            }

            MarkasRectUpdate();

            return newTab;
        }
        #endregion

        public class TabData
        {
            public Panel panel;

            public Button button;

            readonly public string name;

            public TabData(string tabName, Panel tabPanel, Button tabButton)
            {
                name = tabName;
                panel = tabPanel;
                button = tabButton;
            }
        }
    }
}
