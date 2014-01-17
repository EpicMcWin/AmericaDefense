using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using xTile;
using xTile.Display;


namespace AmericaDefense
{
    //http://www.soundjay.com/gun-sound-effect.html
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        enum Gamestates { TitleScreen, Playing }
        Gamestates gameState = Gamestates.Playing;
        //Gamestates gameState = Gamestates.TitleScreen;
        Texture2D titleScreen;
        Rectangle screenBounds;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        IDisplayDevice mapDisplayDevice;
        xTile.Dimensions.Rectangle viewport;
        NaziManager naziManager;
        TowerManager towerManager;
        Texture2D FootSoldiers;
        Texture2D towers;
        public static Texture2D projectiles;
        
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1280;
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            
            
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            
            // Create a new SpriteBatch, which can be used to draw textures.
            mapDisplayDevice = new XnaDisplayDevice(Content, GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            map = Content.Load<xTile.Map>("Background");
            map.LoadTileSheets(mapDisplayDevice);
            viewport = new xTile.Dimensions.Rectangle(0, 0, 1280, 1280);

            projectiles = Content.Load<Texture2D>("New Projectiles");
            FootSoldiers = Content.Load<Texture2D>("FootSoldiers");
            towers = Content.Load<Texture2D>("Towers");
            titleScreen = Content.Load<Texture2D>("dday");
            screenBounds = new Rectangle(0, 0, 1920, 1280);

            naziManager = new NaziManager(
                FootSoldiers,
                new Rectangle(79, 51, 24, 26),
                3,
                new Rectangle(
                    0,
                    0,
                    this.Window.ClientBounds.Width,
                    this.Window.ClientBounds.Height));


            towerManager = new TowerManager(
                towers,
                new Rectangle(4, 3, 33, 46)
                );

            //TowerShotManager = new ShotManager(
            //   projectiles,
            //   new Rectangle(18, 9, 6, 6),
            //   4,
            //   2,
            //   250f,
            //   screenBounds);



            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //MouseState ms = Mouse.GetState();
            //Window.Title = "X: " + ms.X + ", Y: " + ms.Y;
            Window.Title ="Round: " + NaziManager.counter + "    Funds: " + TowerManager.funds;
            // TODO: Add your update logic here
            naziManager.Update(gameTime);
            towerManager.Update(gameTime);
            //TowerShotManager.Update(gameTime);
            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSteelBlue);

            map.Draw(mapDisplayDevice, viewport);
            
            
            spriteBatch.Begin();
            naziManager.Draw(spriteBatch);
            towerManager.Draw(spriteBatch);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
