#region File Description

//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;
using GameStateManagement.Screens;
using System.Collections.Generic;
using GameStateManagement.GameObjects;
using GameStateManagement.GameManager;
using GameStateManagement.MapClasses;
using GameStateManagement.ObjectItem;

#endregion Using Statements

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    internal class GameplayScreen : GameScreen
    {
        #region Fields

        // Management objects
        private ContentManager content;
        private ScoreManager scoreManager;
        private CollisionManager collisionManager;

        // Game Objects
        private TheHighlander highlander;
        private HealthBar healthBar;
        private Highscore highscore;
        private Enemy enemy;
        private Camera camera;
        private Camera cameraBar;
        private Camera cameraHighscore;
        private Explosion explosion;
        private List<Enemy> enemyList;

        // Object-Items
        private Alarm alarm;
        private Generator generator;
        private ControlSystem cs;
        private Crate crate;

        // Other objects
        private Random random = new Random();
        private SpriteFont einFont;
        private float pauseAlpha;

        // Map objects
        MainMap mainMap;


        ///those have a methode and list maybe helpful for later
        ///private List<Enemy> enemyList = new List<Enemy>();
        ///private List<Explosion> explosionList = new List<Explosion>();

        #endregion Fields

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");



            scoreManager = ScoreManager.Load();

            einFont = content.Load<SpriteFont>("einFont");

            enemyList = new List<Enemy>();

            // Objects declaration
            highlander = new TheHighlander(einFont, "Player1", 0, this);
            healthBar = new HealthBar(highlander);
            highscore = new Highscore(highlander);

            mainMap = new MainMap(content, highlander);
            mainMap.LoadContent(content);


            //Object-Items
            generator = new Generator(new Vector2(mainMap.maps[0].levels[0].position.X + 370, mainMap.maps[0].levels[0].position.Y + 500), highlander);
            generator.LoadContent(content);

            alarm = new Alarm(new Vector2(mainMap.maps[0].levels[1].position.X + 500, mainMap.maps[0].levels[1].position.Y + 100), highlander);
            alarm.LoadContent(content);

            cs = new ControlSystem(new Vector2(mainMap.maps[0].levels[3].position.X + 1000, mainMap.maps[0].levels[3].position.Y + 50), highlander);
            cs.LoadContent(content);


            // test
            crate = new Crate(new Vector2(mainMap.maps[0].levels[3].position.X + 1200, mainMap.maps[0].levels[3].position.Y + 150), highlander);
            crate.LoadContent(content);


            // spawn-position for the Highlander
            highlander.Position = mainMap.maps[0].levels[1].spawnPosition;

            enemy = new Enemy(new Vector2(1000, 900), 2, 1, 2f, new Vector2(1000, 1100),
                highlander.Position, 20.0, MovementMode.VERTICAL);


            explosion = new Explosion(new Vector2(enemy.Position.X, enemy.Position.Y));

            /*explosion for the level 1
            explosion = new Explosion(new Vector2(generator.position.X, generator.position.Y));*/


            // Manager
            collisionManager = new CollisionManager(mainMap, highlander);


            // Load content calls
            highlander.LoadContent(content);
            healthBar.LoadContent(content);
            highscore.LoadContent(content);
            explosion.LoadContent(content);
            enemy.LoadContent(content);

            //TEST
            mainMap.maps[0].enemies.Add(enemy);

            // Camera declaration
            camera = new Camera(this);
            cameraBar = new Camera(this);
            cameraHighscore = new Camera(this);

            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }

        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }

        #endregion Initialization


        #region Update and Draw

        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            highlander.Update(gameTime);
            healthBar.Update(gameTime);
            mainMap.Update(gameTime);
            

            camera.Follow(highlander);
            cameraBar.Follow(healthBar);
            cameraHighscore.Follow(highscore);

            collisionManager.ManageCollisions();

            alarm.Update(gameTime, highlander);
            cs.Update(gameTime, highlander);

            base.Update(gameTime, otherScreenHasFocus, false);
        }

        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            /// The game pauses either if the user presses the pause button, or if
            /// they unplug the active gamepad. This requires us to keep track of
            /// whether a gamepad was ever plugged in, because we don't want to pause
            /// on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                highlander.HandleInput();
            }
        }

        public void CallGameOverScreen()
        {
            ScreenManager.AddScreen(new GameOverScreen(), ControllingPlayer);
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                  Color.Black, 0, 0);

            ScreenManager screenManager = this.ScreenManager;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin(transformMatrix: camera.Transform);

            

            mainMap.Draw(spriteBatch);
            foreach (Map m in mainMap.maps)
            {
                m.Draw(spriteBatch);
                m.Draw(spriteBatch,einFont);
                foreach (Level l in m.levels)
                {
                    l.Draw(spriteBatch, einFont);
                }
            }
            
            enemy.Draw(gameTime, spriteBatch);

            alarm.Draw(spriteBatch, einFont);

            generator.Draw(spriteBatch, einFont);

            cs.Draw(spriteBatch, einFont);

            crate.Draw(spriteBatch, einFont);

            highlander.Draw(gameTime, spriteBatch);

            

            explosion.Draw(spriteBatch);


            spriteBatch.End();

            spriteBatch.Begin(transformMatrix: cameraBar.Transform);

            healthBar.Draw(gameTime, spriteBatch);

            spriteBatch.End();


            spriteBatch.Begin(transformMatrix: cameraHighscore.Transform);

            highscore.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }

        }

        #endregion Update and Draw
    }
}