using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source.Inventory
{
    public class Inventory
    {
        public Item[] Items = new Item[10];

        public Inventory()
        {

        }

        //public void ConstructInventoryUI()
        //{
        //    PanelTopBarProps PanelTopBarTestProps = new PanelTopBarProps
        //    {
        //        //Default Entity
        //        EntityName = "TopBar",
        //        BaseSize = new Vector2(300, 30),
        //        StartOffset = new Vector2(0, 100),
        //        EntityAnchor = Anchor.TopCenter,
        //        EntityLayoutState = LayoutState.None,
        //        MiniumSize = new Vector2(20, 20),
        //        OverlayColor = Color.LightGray,

        //        //Panel
        //        BackGroundTexture = ContentLoader.GetTextureByName("tan_pressed"),

        //        ExitButton = new Button(
        //         new ButtonProps
        //         {
        //                //Default Entity
        //                EntityName = "Exit",
        //             BaseSize = new Vector2(21, 21),
        //             StartOffset = new Vector2(4, 0),
        //             EntityAnchor = Anchor.TopRight,
        //             EntityLayoutState = LayoutState.None,
        //             MiniumSize = new Vector2(10, 10),
        //             OverlayColor = Color.White,

        //                //Button
        //                ButtonTexture = ContentLoader.GetTextureByName("Tan"),
        //             ClickedButtonTexture = ContentLoader.GetTextureByName("tan_pressed"),
        //             ButtonIcon = new Icon(new IconProps
        //             {
        //                    //Default Entity
        //                    EntityName = "ExitIcon",
        //                 BaseSize = new Vector2(21, 21),
        //                 StartOffset = new Vector2(4, 0),
        //                 EntityAnchor = Anchor.Center,
        //                 EntityLayoutState = LayoutState.FillParent,
        //                 MiniumSize = new Vector2(10, 10),
        //                 OverlayColor = Color.Brown,

        //                    //Icon
        //                    IconTexture = ContentLoader.GetTextureByName("ExitButton")
        //             }),
        //             HoverColor = Color.Red
        //         }),

        //        MinimizeButton = new Button(
        //         new ButtonProps
        //         {
        //                //Default Entity
        //                EntityName = "Minimize",
        //             BaseSize = new Vector2(21, 21),
        //             StartOffset = new Vector2(54, 0),
        //             EntityAnchor = Anchor.TopRight,
        //             EntityLayoutState = LayoutState.None,
        //             MiniumSize = new Vector2(10, 10),
        //             OverlayColor = Color.White,

        //                //Button
        //                ButtonTexture = ContentLoader.GetTextureByName("Tan"),
        //             ClickedButtonTexture = ContentLoader.GetTextureByName("tan_pressed"),
        //             ButtonIcon = new Icon(new IconProps
        //             {
        //                    //Default Entity
        //                    EntityName = "ExitIcon",
        //                 BaseSize = new Vector2(21, 21),
        //                 StartOffset = new Vector2(4, 0),
        //                 EntityAnchor = Anchor.Center,
        //                 EntityLayoutState = LayoutState.FillParent,
        //                 MiniumSize = new Vector2(10, 10),
        //                 OverlayColor = Color.Brown,

        //                    //Icon
        //                    IconTexture = ContentLoader.GetTextureByName("MinimizeButton")
        //             }),
        //             HoverColor = Color.LightGray
        //         }),

        //        MaximizeButton = new Button(
        //         new ButtonProps
        //         {
        //                //Default Entity
        //                EntityName = "Maximize",
        //             BaseSize = new Vector2(21, 21),
        //             StartOffset = new Vector2(29, 0),
        //             EntityAnchor = Anchor.TopRight,
        //             EntityLayoutState = LayoutState.None,
        //             MiniumSize = new Vector2(10, 10),
        //             OverlayColor = Color.White,

        //                //Button
        //                ButtonTexture = ContentLoader.GetTextureByName("Tan"),
        //             ClickedButtonTexture = ContentLoader.GetTextureByName("tan_pressed"),
        //             ButtonIcon = new Icon(new IconProps
        //             {
        //                    //Default Entity
        //                    EntityName = "ExitIcon",
        //                 BaseSize = new Vector2(21, 21),
        //                 StartOffset = new Vector2(4, 0),
        //                 EntityAnchor = Anchor.Center,
        //                 EntityLayoutState = LayoutState.FillParent,
        //                 MiniumSize = new Vector2(10, 10),
        //                 OverlayColor = Color.Brown,

        //                    //Icon
        //                    IconTexture = ContentLoader.GetTextureByName("MaximizeButton")
        //             }),
        //             HoverColor = Color.LightGray
        //         }),

        //        ScalePlus = new Button(
        //         new ButtonProps
        //         {
        //                //Default Entity
        //                EntityName = "ScalePlus",
        //             BaseSize = new Vector2(18, 18),
        //             StartOffset = new Vector2(29, 2),
        //             EntityAnchor = Anchor.TopLeft,
        //             EntityLayoutState = LayoutState.None,
        //             MiniumSize = new Vector2(10, 10),
        //             OverlayColor = Color.White,

        //                //Button
        //                ButtonTexture = ContentLoader.GetTextureByName("Tan"),
        //             ClickedButtonTexture = ContentLoader.GetTextureByName("tan_pressed"),
        //             ButtonIcon = new Icon(new IconProps
        //             {
        //                    //Default Entity
        //                    EntityName = "ScalePlusIcon",
        //                 BaseSize = new Vector2(21, 21),
        //                 StartOffset = new Vector2(4, 0),
        //                 EntityAnchor = Anchor.Center,
        //                 EntityLayoutState = LayoutState.FillParent,
        //                 MiniumSize = new Vector2(10, 10),
        //                 OverlayColor = Color.Brown,

        //                    //Icon
        //                    IconTexture = ContentLoader.GetTextureByName("ScalePlus")
        //             }),
        //             HoverColor = Color.LightGray
        //         }),

        //        ScaleMinus = new Button(
        //         new ButtonProps
        //         {
        //                //Default Entity
        //                EntityName = "ScaleMinus",
        //             BaseSize = new Vector2(18, 18),
        //             StartOffset = new Vector2(4, 2),
        //             EntityAnchor = Anchor.TopLeft,
        //             EntityLayoutState = LayoutState.None,
        //             MiniumSize = new Vector2(10, 10),
        //             OverlayColor = Color.White,

        //                //Button
        //                ButtonTexture = ContentLoader.GetTextureByName("Tan"),
        //             ClickedButtonTexture = ContentLoader.GetTextureByName("tan_pressed"),
        //             ButtonIcon = new Icon(new IconProps
        //             {
        //                    //Default Entity
        //                    EntityName = "ScaleMinusIcon",
        //                 BaseSize = new Vector2(21, 21),
        //                 StartOffset = new Vector2(4, 0),
        //                 EntityAnchor = Anchor.Center,
        //                 EntityLayoutState = LayoutState.FillParent,
        //                 MiniumSize = new Vector2(10, 10),
        //                 OverlayColor = Color.Brown,

        //                    //Icon
        //                    IconTexture = ContentLoader.GetTextureByName("ScaleMinus")
        //             }),
        //             HoverColor = Color.LightGray
        //         }),

        //        Header = new Text(
        //         new TextProps
        //         {
        //                //Default Entity
        //                EntityName = "ScaleMinus",
        //             BaseSize = new Vector2(18, 18),
        //             StartOffset = new Vector2(0, 0),
        //             EntityAnchor = Anchor.Center,
        //             EntityLayoutState = LayoutState.None,
        //             MiniumSize = new Vector2(10, 10),
        //             OverlayColor = Color.White,

        //                //Default Text
        //                Text = "Inventory",
        //             TextColor = Color.Black,
        //             Draggable = false,
        //         })

        //    };

        //    PanelProps TestPanelProps = new PanelProps
        //    {
        //        //Default Entity
        //        EntityName = "Parent",
        //        BaseSize = new Vector2(300, 300),
        //        StartOffset = new Vector2(0, 0),
        //        EntityAnchor = Anchor.TopLeft,
        //        EntityLayoutState = LayoutState.FillParent,
        //        MiniumSize = new Vector2(250, 250),
        //        OverlayColor = Color.White,

        //        //Panel
        //        AddTopBar = true,
        //        Backgroundtexture = ContentLoader.GetTextureByName("tan_pressed"),
        //        TopBarTexture = ContentLoader.GetTextureByName("tan_pressed")
        //    };

            

            

        //    for (int i = 0; i < 50; i++)
        //    {
        //        Panel Element = new Panel(new PanelProps
        //        {
        //            //Default Entity
        //            EntityName = "InvSlot: " + i,
        //            BaseSize = new Vector2(45, 45),
        //            StartOffset = new Vector2(0, 0),
        //            EntityAnchor = Anchor.AutoInLine,
        //            EntityLayoutState = LayoutState.None,
        //            MiniumSize = new Vector2(0, 0),
        //            OverlayColor = Color.LightGray,

        //            //TableElement
        //            Backgroundtexture = ContentLoader.GetTextureByName("tan_pressed")
        //        });
        //        Element.PanelOverflowBehavior = PanelOverflowBehavior.Overflow;
        //        panel.AddChild(Element);
        //    }
        //}

        //public void AddUiToUserinterface()
        //{
        //    PanelTopBar panelTopBar = new PanelTopBar(PanelTopBarTestProps);
        //    panelTopBar.IsDraggable = true;
        //    UserInterface.GetActiveUserInterface().AddChildToRoot(panelTopBar);

        //    Panel panel = new Panel(TestPanelProps);
        //    panel.IsDraggable = false;
        //    panel.ScaledPadding = new Vector2(10, 10);
        //    panel.PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll;
        //    panel.ScrollBar.AdjustMaxAutomatically = true;
        //    panelTopBar.AddChild(panel);
        //}
    }
}
