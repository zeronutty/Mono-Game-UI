using Microsoft.Xna.Framework;
using UiTesting.Source;

namespace UiTesting.Debuging
{
    public static class UITesting
    {
        public static void AddUI(UiManager userInterface)
        {
            userInterface.Root = new RootPanel();

            ButtonProps DebugButtonProps = new ButtonProps
            {
                //Default Entity
                EntityName = "Show Debug",
                Size = new Vector2(50, 20),
                LocalPosition = new Vector2(5, 5),
                EntityAnchor = Anchor.TopLeft,
                EntityLayoutState = LayoutState.None,
                OverlayColor = Color.Blue,

                //Button
                ClickedTexture = SpritesData.B_CLSprite,
                Texture = SpritesData.B_BGSprite,
                HoverColor = Color.DarkBlue
            };

            //PanelProps TestPanelProps = new PanelProps
            //{
            //    //Default Entity
            //    EntityName = "Test Panel",
            //    Size = new Vector2(300, 300),
            //    LocalPosition = new Vector2(0, 0),
            //    EntityAnchor = Anchor.TopLeft,
            //    EntityLayoutState = LayoutState.FillParent,
            //    OverlayColor = Color.White,

            //    //Panel
            //    Backgroundtexture = SpritesData.P_BGSprite,
            //};

            //TableLayoutProps TestTableLayoutProps = new TableLayoutProps
            //{
            //    //Default Entity
            //    EntityName = "Test Panel",
            //    Size = new Vector2(270, 560),
            //    LocalPosition = new Vector2(0, 0),
            //    EntityAnchor = Anchor.TopLeft,
            //    EntityLayoutState = LayoutState.None,
            //    OverlayColor = Color.White,

            //    //TableLayout
            //    Columns = 5,
            //    Rows = 10
            //};

            //PanelTopBarProps PanelTopBarTestProps = new PanelTopBarProps
            //{
            //    //Default Entity
            //    EntityName = "TopBar",
            //    Size = new Vector2(300, 30),
            //    LocalPosition = new Vector2(0, 100),
            //    EntityAnchor = Anchor.TopCenter,
            //    EntityLayoutState = LayoutState.None,
            //    OverlayColor = Color.LightGray,

            //    //Panel
            //    BackGroundTexture = SpritesData.TB_BGSprite,

            //    ExitButton = new Button(
            //        new ButtonProps
            //        {
            //            //Default Entity
            //            EntityName = "Exit",
            //            Size = new Vector2(21, 21),
            //            LocalPosition = new Vector2(4, 0),
            //            EntityAnchor = Anchor.TopRight,
            //            EntityLayoutState = LayoutState.None,
            //            OverlayColor = Color.White,

            //            //Button
            //            Texture = SpritesData.B_BGSprite,
            //            ClickedTexture = SpritesData.B_CLSprite,
            //            Icon = new Icon(new IconProps
            //            {
            //                //Default Entity
            //                EntityName = "ExitIcon",
            //                Size = new Vector2(21, 21),
            //                LocalPosition = new Vector2(4, 0),
            //                EntityAnchor = Anchor.Center,
            //                EntityLayoutState = LayoutState.FillParent,
            //                OverlayColor = Color.Brown,

            //                //Icon
            //                IconTexture = SpritesData.I_ExitButton
            //            }),
            //            HoverColor = Color.Red
            //        }),

            //    MinimizeButton = new Button(
            //        new ButtonProps
            //        {
            //            //Default Entity
            //            EntityName = "Minimize",
            //            Size = new Vector2(21, 21),
            //            LocalPosition = new Vector2(54, 0),
            //            EntityAnchor = Anchor.TopRight,
            //            EntityLayoutState = LayoutState.None,
            //            OverlayColor = Color.White,

            //            //Button
            //            Texture = SpritesData.B_BGSprite,
            //            ClickedTexture = SpritesData.B_CLSprite,
            //            Icon = new Icon(new IconProps
            //            {
            //                //Default Entity
            //                EntityName = "ExitIcon",
            //                Size = new Vector2(21, 21),
            //                LocalPosition = new Vector2(4, 0),
            //                EntityAnchor = Anchor.Center,
            //                EntityLayoutState = LayoutState.FillParent,
            //                OverlayColor = Color.Brown,

            //                //Icon
            //                IconTexture = SpritesData.I_MinimizeButton
            //            }),
            //            HoverColor = Color.LightGray
            //        }),

            //    MaximizeButton = new Button(
            //        new ButtonProps
            //        {
            //            //Default Entity
            //            EntityName = "Maximize",
            //            Size = new Vector2(21, 21),
            //            LocalPosition = new Vector2(29, 0),
            //            EntityAnchor = Anchor.TopRight,
            //            EntityLayoutState = LayoutState.None,
            //            OverlayColor = Color.White,

            //            //Button
            //            Texture = SpritesData.B_BGSprite,
            //            ClickedTexture = SpritesData.B_CLSprite,
            //            Icon = new Icon(new IconProps
            //            {
            //                //Default Entity
            //                EntityName = "ExitIcon",
            //                Size = new Vector2(21, 21),
            //                LocalPosition = new Vector2(4, 0),
            //                EntityAnchor = Anchor.Center,
            //                EntityLayoutState = LayoutState.FillParent,
            //                OverlayColor = Color.Brown,

            //                //Icon
            //                IconTexture = SpritesData.I_MaximizeButton
            //            }),
            //            HoverColor = Color.LightGray
            //        }),

            //    ScalePlus = new Button(
            //        new ButtonProps
            //        {
            //            //Default Entity
            //            EntityName = "ScalePlus",
            //            Size = new Vector2(18, 18),
            //            LocalPosition = new Vector2(29, 2),
            //            EntityAnchor = Anchor.TopLeft,
            //            EntityLayoutState = LayoutState.None,
            //            OverlayColor = Color.White,

            //            //Button
            //            Texture = SpritesData.B_BGSprite,
            //            ClickedTexture = SpritesData.B_CLSprite,
            //            Icon = new Icon(new IconProps
            //            {
            //                //Default Entity
            //                EntityName = "ScalePlusIcon",
            //                Size = new Vector2(21, 21),
            //                LocalPosition = new Vector2(4, 0),
            //                EntityAnchor = Anchor.Center,
            //                EntityLayoutState = LayoutState.FillParent,
            //                OverlayColor = Color.Brown,

            //                //Icon
            //                IconTexture = SpritesData.I_ScalePlusButton
            //            }),
            //            HoverColor = Color.LightGray
            //        }),

            //    ScaleMinus = new Button(
            //        new ButtonProps
            //        {
            //            //Default Entity
            //            EntityName = "ScaleMinus",
            //            Size = new Vector2(18, 18),
            //            LocalPosition = new Vector2(4, 2),
            //            EntityAnchor = Anchor.TopLeft,
            //            EntityLayoutState = LayoutState.None,
            //            OverlayColor = Color.White,

            //            //Button
            //            Texture = SpritesData.B_BGSprite,
            //            ClickedTexture = SpritesData.B_CLSprite,
            //            Icon = new Icon(new IconProps
            //            {
            //                //Default Entity
            //                EntityName = "ScaleMinusIcon",
            //                Size = new Vector2(21, 21),
            //                LocalPosition = new Vector2(4, 0),
            //                EntityAnchor = Anchor.Center,
            //                EntityLayoutState = LayoutState.FillParent,
            //                OverlayColor = Color.Brown,

            //                //Icon
            //                IconTexture = SpritesData.I_ScaleMinusButton
            //            }),
            //            HoverColor = Color.LightGray
            //        }),

            //    Header = new Text(
            //        new TextProps
            //        {
            //            //Default Entity
            //            EntityName = "ScaleMinus",
            //            Size = new Vector2(18, 18),
            //            LocalPosition = new Vector2(0, 0),
            //            EntityAnchor = Anchor.Center,
            //            EntityLayoutState = LayoutState.None,
            //            OverlayColor = Color.White,

            //            //Default Text
            //            Text = "Inventory",
            //            TextColor = Color.Black,
            //            Draggable = false,
            //        })

            //};

            Button button = new Button(DebugButtonProps);
            //button.OnClick = (Entity btn) => { userInterface.DebugMode = !userInterface.DebugMode; userInterface.OpenClosePropPanel(userInterface.p_testform.Visible); };
            userInterface.AddChildToRoot(button);

            //PanelTopBar panelTopBar = new PanelTopBar(PanelTopBarTestProps);
            //panelTopBar.IsDraggable = true;
            //userInterface.AddChildToRoot(panelTopBar);

            //Panel panel = new Panel(TestPanelProps);
            //panel.IsDraggable = false;
            //panel.ScaledPadding = new Vector2(5, 5);
            //panel.PanelOverflowBehavior = PanelOverflowBehavior.Overflow;
            ////panel.ScrollBar.AdjustMaxAutomatically = true;
            //panelTopBar.AddChild(panel);

            //PanelTabs panelTabs = new PanelTabs(new EntityProps { EntityAnchor = Anchor.Auto });
            //panel.AddChild(panelTabs);
            //{
            //    PanelTabs.TabData tab = panelTabs.AddTab("Tab 1");
            //    tab.button.AddChild(new Paragraph(new ParagraphProps { EntityName = "Tab One", Text = "Tab one" }));
            //    tab.panel.AddChild(new MultiColorParagraph(new ParagraphProps { TextAlginment = TextAlginment.Centered, WrapWords = true, EntityName = "MultiColorTest", Text = @"{{GREEN}}PanelTab creates a group of internal panels with toggle buttons to switch between them." }));

            //    DropDown dropDown = new DropDown(new DropDownProps
            //    {
            //        EntityName = "DropDown",
            //        Size = new Vector2(0, 30),
            //        ShowArrow = true,
            //        SelectedOptionImage = SpritesData.DD_SeletedOptionSprite,              

            //        EntityAnchor = Anchor.TopCenter,

            //        DropDownPanelHeight = 200,
            //    });


            //    dropDown.AddItem("Fuck");
            //    dropDown.AddItem("Shit");
            //    dropDown.AddItem("Bollocks");
            //    dropDown.AddItem("Cunts");
            //    dropDown.AddItem("Fannys");
            //    dropDown.AddItem("Dick");
            //    dropDown.AddItem("Anus's");
            //    dropDown.AddItem("NobsCheese");
            //    dropDown.AddItem("Cleggnuts");

            //    tab.panel.AddChild(dropDown);
            //}



            //// add second panel
            //{
            //    PanelTabs.TabData tab = panelTabs.AddTab("Tab 2");
            //    tab.button.AddChild(new Paragraph(new ParagraphProps { EntityName = "Tab Two", Text = "Tab Two" }));
            //    tab.panel.AddChild(new MultiColorParagraph(new ParagraphProps { TextAlginment = TextAlginment.Centered, WrapWords = true, EntityName = "MultiColorTextTwo", Text = @"{{RED}}Awesome, {{DEFAULT}}you got to {{BLUE}}tab2! Maybe something interesting in tab3?"}));

            //    CheckBox checkBox = new CheckBox(new CheckBoxProps { Size = new Vector2(0, 30), EntityAnchor = Anchor.TopCenter, Text = "Runescape", IsChecked = false });
            //    tab.panel.AddChild(checkBox);

            //    RadioButton radioButton = new RadioButton(new RadioButtonProps { Size = new Vector2(0, 30), EntityAnchor = Anchor.BottomCenter, Text = "Click me 3" , IsChecked = false });

            //    tab.panel.AddChild(radioButton);

            //    RadioButton radioButton2 = new RadioButton(new RadioButtonProps { Size = new Vector2(0, 30), EntityAnchor = Anchor.BottomCenter, Text = "Click me 2", IsChecked = false, LocalPosition = new Vector2(0, 30)});

            //    tab.panel.AddChild(radioButton2);

            //    RadioButton radioButton3 = new RadioButton(new RadioButtonProps { Size = new Vector2(0, 30), EntityAnchor = Anchor.BottomCenter, Text = "Click me 1", IsChecked = true, LocalPosition = new Vector2(0, 60)});

            //    tab.panel.AddChild(radioButton3);
            //}


            //// add third panel
            //{
            //    PanelTabs.TabData tab = panelTabs.AddTab("Tab 3");
            //    tab.button.AddChild(new Paragraph(new ParagraphProps { EntityName = "Tab Three", Text = "Tab Three" }));
            //    //tab.panel.AddChild(new Paragraph(new ParagraphProps { EntityName = "Tab There Paragraph", Text = @"Nothing to see here." }));

            //    ProgressBar progressBar = new ProgressBar(new ProgressBarProps
            //    {
            //        EntityName = "ProgressBarTest",
            //        Min = 0,
            //        Max = 10,
            //        EntityAnchor = Anchor.Auto,
            //        BarTexture = SpritesData.PB_BGSprite,
            //        CaptionText = "100%",
            //        FillTexture = SpritesData.PB_PSprite
            //    });

            //    tab.panel.AddChild(progressBar);

            //    //TextInput textInput = new TextInput(new TextInputProps { UseMultiLine = true, Size = new Vector2(0, 100), EntityAnchor = Anchor.Center });
            //    //textInput.PlaceHolderText = @"Type Here";
            //    //tab.panel.AddChild(textInput);

            //    //TableLayout tableLayout = new TableLayout(new TableLayoutProps { Columns = 5, Rows = 5, EntityAnchor = Anchor.TopCenter, Size = new Vector2(0,0)});
            //    //tab.panel.AddChild(tableLayout);

            //    //for (int x = 0; x < 5; x++)
            //    //{
            //    //    for (int y = 0; y < 5; y++)
            //    //    {
            //    //        tableLayout.AddElementAtIndex(x, y, new Panel(new PanelProps {Backgroundtexture = ContentLoader.GetTextureByName("tan_pressed"), OverlayColor = Color.White }));
            //    //    }
            //    //}

            //    TreeViewLayout treeViewLayout = new TreeViewLayout(new PanelProps
            //    {
            //        Size = new Vector2(300, 600),
            //        Backgroundtexture = SpritesData.P_BGSprite,
            //        EntityAnchor = Anchor.TopLeft,
            //        LocalPosition = new Vector2(30, 30),
            //    });

            //    treeViewLayout.Padding = new Vector2(30, 30);
            //    userInterface.AddChildToRoot(treeViewLayout);

            //    for(int i = 0; i < 20; i++)
            //    {
            //        TreeViewElement checkBox = new TreeViewElement(new CheckBoxProps { Size = new Vector2(0, 30), Text = "Element" + i, EntityAnchor = Anchor.AutoInLine });
            //        TreeViewElement checkBox2 = new TreeViewElement(new CheckBoxProps { Size = new Vector2(0, 30), Text = "Element" + i, EntityAnchor = Anchor.AutoInLine });
            //        Paragraph paragraph = new Paragraph(new ParagraphProps { Size = new Vector2(0, 20), Text = "Element " + i + " Child", EntityAnchor = Anchor.AutoInLine });
            //        Paragraph paragraph2 = new Paragraph(new ParagraphProps { Size = new Vector2(0, 20), Text = "Element " + i + " Child", EntityAnchor = Anchor.AutoInLine });
            //        Paragraph paragraph3 = new Paragraph(new ParagraphProps { Size = new Vector2(0, 20), Text = "Element " + i + " Child", EntityAnchor = Anchor.AutoInLine });

            //        if (i < 10)
            //        {
            //            checkBox.AddChild(checkBox2);
            //            checkBox2.AddChild(paragraph);
            //            checkBox2.AddChild(paragraph2);
            //            checkBox2.AddChild(paragraph3);
            //        }

            //            treeViewLayout.AddChild(checkBox);

            //    }

            TextBox textBox = new TextBox(new TextBoxProps { Size = new Vector2(300,300), EntityAnchor = Anchor.TopLeft, LocalPosition = new Vector2(400,400), CaretSprite = SpritesData.TB_Caret});
                userInterface.AddChildToRoot(textBox);
        
        }
    }
}
