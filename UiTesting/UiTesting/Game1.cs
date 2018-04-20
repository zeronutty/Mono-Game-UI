using System;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using UiTesting.Source;
using UiTesting.Source.Forms;
using UiTesting.Source.Input;

namespace UiTesting
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        FPSCounter fps;
        UiManager Active;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content"; 
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //IsMouseVisible = true;
       
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.ApplyChanges();

            Active = UiManager.GetActiveUserInterface();

            Active.Initialize(Content, graphics, this);

            Active.IncludeCursorInRenderTarget = false;

            Active.UseRenderTarget = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ContentLoader.LoadContent(Content);

            Active.LoadContent();

            // TODO: use this.Content to load your game content here
            fps = new FPSCounter();

            Active.OnClick = (Entity entity) => { Active.p_testform.SetEventEntity(entity, Events.OnClick); };
            Active.OnMouseDown = (Entity entity) => { Active.p_testform.SetEventEntity(entity, Events.OnMouseDown); };
            Active.OnMouseEnter = (Entity entity) => { Active.p_testform.SetEventEntity(entity, Events.OnMouseEnter); };
            Active.OnMouseLeave = (Entity entity) => { Active.p_testform.SetEventEntity(entity, Events.OnMouseLeave); };
            Active.OnMouseReleased = (Entity entity) => { Active.p_testform.SetEventEntity(entity, Events.OnMouseReleased); };
            Active.OnMouseWheelScroll = (Entity entity) => { Active.p_testform.SetEventEntity(entity, Events.OnMouseWheelScroll); };
            Active.OnStartDrag = (Entity entity) => { Active.p_testform.SetEventEntity(entity, Events.OnStartDrag); };
            Active.OnStopDrag = (Entity entity) => { Active.p_testform.SetEventEntity(entity, Events.OnStopDrag); };
            Active.OnFocusChange = (Entity entity) => { Active.p_testform.SetEventEntity(entity, Events.OnFocusChange); };
            Active.OnValueChange = (Entity entity) => { Active.p_testform.SetEventEntity(entity, Events.OnValueChange); };
            Active.WhileDragging = (Entity entity) => { Active.p_testform.SetEventEntity(entity, Events.WhileDragging); };
            Active.WhileMouseDown = (Entity entity) => { Active.p_testform.SetEventEntity(entity, Events.WhileMouseDown); };
            Active.WhileMouseHover = (Entity entity) => { Active.p_testform.SetEventEntity(entity, Events.WhileMouseHover); };
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            //if (!IsActive)
            //    return;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            fps.Update(deltaTime);

            UiManager.GetActiveUserInterface().Update(gameTime);
        
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {       
            UiManager.GetActiveUserInterface().Draw(spriteBatch);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            UiManager.GetActiveUserInterface().DrawMainRenderTarget(spriteBatch);


            base.Draw(gameTime);
        }
    }
}
