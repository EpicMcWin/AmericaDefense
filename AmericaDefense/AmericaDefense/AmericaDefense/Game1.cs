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
        enum Gamestates { TitleScreen, Playing, Options, GameOver }
        Gamestates gameState = Gamestates.TitleScreen;
        //Gamestates gameState = Gamestates.TitleScreen;
        Texture2D titleScreen;
        Texture2D optionsMenu;
        Texture2D gameOver;
        float gameOverTime = 0;
        
        public static Texture2D healthBar;
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
        public static SoundEffect gunshot;
        public static SoundEffectInstance gunshotInstance;
        public static SoundEffect gunshot2;
        public static SoundEffectInstance gunshotInstance2;
        public static int baseHealth = 500;
        public static Texture2D projectiles;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1040;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.IsFullScreen = false;
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
            optionsMenu = Content.Load<Texture2D>("OptionsMenu");
            gameOver = Content.Load<Texture2D>("GameOver");
            healthBar = Content.Load<Texture2D>("healthbar");
            screenBounds = new Rectangle(0, 0, 1920, 1280);

            gunshot = Content.Load<SoundEffect>("gunshot");
            gunshotInstance = gunshot.CreateInstance();
            gunshot2 = Content.Load<SoundEffect>("gunshot2");
            gunshotInstance2 = gunshot2.CreateInstance();

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
            gameOverTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            MouseState ms = Mouse.GetState();
            switch (gameState)
            {

                case Gamestates.TitleScreen:
                    Rectangle startButton = new Rectangle(25, 236, 202, 48);
                    Rectangle optionsButton = new Rectangle(25, 292, 273, 43);
                    Rectangle exitButton = new Rectangle(22, 344, 147, 50);

                    if (ms.X > 25 && ms.X < 400 && ms.Y > 260 && ms.Y < 350 && ms.LeftButton == ButtonState.Pressed)
                    {
                        gameState = Gamestates.Playing;
                    }

                    if (ms.X > 25 && ms.X < 580 && ms.Y > 360 && ms.Y < 420 && ms.LeftButton == ButtonState.Pressed)
                    {
                        gameState = Gamestates.Options;
                    }

                    if (ms.X > 25 && ms.X < 280 && ms.Y > 430 && ms.Y < 500 && ms.LeftButton == ButtonState.Pressed)
                    {
                        Exit();
                    }
                    break;

                case Gamestates.Options:
                    if (ms.X > 390 && ms.X < 770 && ms.Y > 913 && ms.Y < 990 && ms.LeftButton == ButtonState.Pressed)
                        gameState = Gamestates.TitleScreen;
                    break;

                case Gamestates.Playing:
                    if (baseHealth <= 0)
                    {
                        gameState = Gamestates.GameOver;
                        gameOverTime = 0;
                    }
                    break;
                case Gamestates.GameOver:
                    
                    if (gameOverTime > 5)
                    {
                        gameState = Gamestates.TitleScreen;
                    }
                    break;


            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            Window.Title = "Round: " + NaziManager.counter + "    Funds: " + TowerManager.funds + "     Base Strength: " + baseHealth;
            // TODO: Add your update logic here
            if (gameState == Gamestates.Playing)
            {
                naziManager.Update(gameTime);
                towerManager.Update(gameTime);
            }
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
            spriteBatch.Begin();

            if (gameState == Gamestates.TitleScreen)
            {
                spriteBatch.Draw(titleScreen,
                new Rectangle(0, 0,
                this.Window.ClientBounds.Width,
                this.Window.ClientBounds.Height),
                Color.White);
            }
            else if (gameState == Gamestates.Options)
            {
                spriteBatch.Draw(optionsMenu,
                new Rectangle(0, 0,
                this.Window.ClientBounds.Width,
                this.Window.ClientBounds.Height),
                Color.White);
            }
            else if (gameState == Gamestates.GameOver)
            {
                spriteBatch.Draw(gameOver,
                    new Rectangle(0, 0,
                this.Window.ClientBounds.Width,
                this.Window.ClientBounds.Height),
                Color.White);
            }
            else
            {
                naziManager.Draw(spriteBatch);
                towerManager.Draw(spriteBatch);
                map.Draw(mapDisplayDevice, viewport);

                if (baseHealth <= 500 && baseHealth > 375)
                {
                    spriteBatch.Draw(healthBar,
                        new Rectangle(1285, 755, 57, 18),
                        new Rectangle(171, 0, 57, 18),
                        Color.White);
                }
                else if (baseHealth <= 375 && baseHealth > 250)
                {
                    spriteBatch.Draw(healthBar,
                        new Rectangle(1285, 755, 57, 18),
                        new Rectangle(113, 0, 57, 18),
                        Color.White);
                }

                else if (baseHealth <= 250 && baseHealth > 125)
                {
                    spriteBatch.Draw(healthBar,
                        new Rectangle(1285, 755, 57, 18),
                        new Rectangle(57, 0, 57, 18),
                        Color.White);
                }

                else if (baseHealth <= 125 && baseHealth > 0)
                {
                    spriteBatch.Draw(healthBar,
                        new Rectangle(1285, 755, 57, 18),
                        new Rectangle(0, 0, 57, 18),
                        Color.White);
                }
            }
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

