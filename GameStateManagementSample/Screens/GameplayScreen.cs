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

        private ContentManager content;


        //private SpriteFont gameFont;

        private Texture2D[] theHighlander = new Texture2D[3];

        int spriteCounter = 0;



        private Vector2 playerPosition = new Vector2(0,0);
        
        private Vector2 enemyPosition = new Vector2(100, 100);

        private Random random = new Random();

        private float pauseAlpha;

        private float angle = 0;

        private TheHighlander highlander;



        //amer

        private SpriteFont einFont;

        private GameMenuInfo gameMenuInfo;

        private ScoreManager scoreManager;
        
        private SpriteFont score;

        private Texture2D bar;

        //test enemy
        private Enemy enemy;
        private Texture2D theEnemy;

        //test Explosion
        private Explosion explosion;
        private Texture2D theExplosion;
        
        // they have a methode and list maybe helpful for later
        //private List<Enemy> enemyList = new List<Enemy>();
        //private List<Explosion> explosionList = new List<Explosion>();


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

            //
            scoreManager = ScoreManager.Load();

            //load the health bar img
            bar = content.Load<Texture2D>(@"graphics\game_menu_graphics\bar_0upgrade\bar_10_0upg");

            //score
            score = content.Load<SpriteFont>(@"graphics\game_menu_graphics\score");


            // this is for testing the position of the player
            einFont = content.Load<SpriteFont>("einFont");
            
            // Load sprites and textures
            theHighlander[0] = content.Load<Texture2D>(@"graphics\starships\the_highlander_1");
            theHighlander[1] = content.Load<Texture2D>(@"graphics\starships\the_highlander_2");
            theHighlander[2] = content.Load<Texture2D>(@"graphics\starships\the_highlander_3");

            // Objects
            highlander = new TheHighlander(theHighlander[0],einFont,"Player1",0)
            {
                Position = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2, ScreenManager.GraphicsDevice.Viewport.Height - theHighlander[0].Height - 5),
                
                Origin = new Vector2(theHighlander[spriteCounter].Width / 2, theHighlander[spriteCounter].Height / 2),
                
            };

            theEnemy = content.Load<Texture2D>(@"graphics\starships\the_highlander_3");

            enemy = new Enemy(theEnemy, einFont, 1, 0, 0, 0);

            theExplosion = content.Load<Texture2D>(@"explosion");
            explosion = new Explosion(theExplosion, new Vector2(enemy.position.X, enemy.position.Y));
            scoreManager = ScoreManager.Load();

            gameMenuInfo = new GameMenuInfo(bar,score, highlander.Player.Playername,highlander.Player.Value)
            {

            };

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


        #region Loading Enemy
        /*
        public void LoadEnemies()
        {
            /*
            if(enemyList.Count < 1)
            {
                enemyList.Add(new Enemy(theEnemy, einFont, 1, 0, 0, 0));
            }
            

            enemyList.Add(new Enemy(theEnemy, einFont, 1, 0, 0, 0));

            if (!enemyList[0].isVisible)
            {
                enemyList.RemoveAt(0);
                
            }


            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }

            }
        }

        public void ManageExplosions()
        {
            for (int i = 0; i < explosionList.Count; i++)
            {
                if (!explosionList[i].isVisible)
                {
                    explosionList.RemoveAt(i);
                    i--;
                }

            }
        }
        */
        #endregion Loading Enemy & Manage Explosions

        #region Update and Draw

        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            /*
            foreach(Enemy e in enemyList)
            {
                if (e.enemyBox.Intersects(highlander.highlanderBox))
                {
                    e.isVisible = false;
                }
                e.Update(gameTime);
            }
            */

            if (highlander.highlanderBox.Intersects(enemy.enemyBox))
            {
                /*
                if (--highlander.Shield == 0)
                {
                    highlander.isVisible = false;
                }


                if (--enemy.AsctualShield == 0) { 
                    enemy.isVisible = false;
                }
                */
                enemy.isVisible = false;
            }

            if (enemy.isVisible) {
                enemy.Update(gameTime);
            }
            else
            {
                explosion.Update(gameTime);
            }

            highlander.Update(gameTime);

            

            //LoadEnemies();
            base.Update(gameTime, otherScreenHasFocus, false);
            
            /*
            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);
            */
           // if (IsActive)
            //{
                // Apply some random jitter to make the enemy move around.
                //const float randomization = 10;

               // enemyPosition.X += (float)(random.NextDouble() - 0.5) * randomization;
                //enemyPosition.Y += (float)(random.NextDouble() - 0.5) * randomization;

                // Apply a stabilizing force to stop the enemy moving off the screen.

                //ich hab das aus kommentiert
                /*
                  Vector2 targetPosition = new Vector2(
                    ScreenManager.GraphicsDevice.Viewport.Width / 2 - theHighlander[0].Width / 2,
                    200);
                */

                //Vector2 targetPosition = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2 - theHighlander[0].Height /2);

                //enemyPosition = Vector2.Lerp(enemyPosition, targetPosition, 0.05f);

                /*
                 //TODO
                //amer
                //when tha game end save the Score and Player name in the list
                // this 2 lines we should put them when the game end
                //which is till now unclear when the game will end
                scoreManager.Add(new Score(highlander.Player.Playername, highlander.Player.Value));
                ScoreManager.Save(scoreManager);
                
                //List load
                //this will use it in the next menu option
                
                scoreManager = ScoreManager.Load();

                spriteBatch.DrawString(score,"HighScore:\n" +
                string.Join("\n", scoreManager.Highscore.Select(c => c.Playername + ": "+ c.Value).ToArray()),
                new Vector2(GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Width /2, GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Hight /2 ),
                Color.White);
                */


                // TODO: this game isn't very fun! You could probably improve
                // it by inserting something more interesting in this space :-)
           // }
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

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
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

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);

            ScreenManager screenManager = this.ScreenManager;

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            SpriteBatch _spriteBatch = ScreenManager.SpriteBatch;

            // Count up counter to change sprite
            if (spriteCounter < 2)
            {
                spriteCounter++;
            }
            else
            {
                spriteCounter = 0;
            }

            enemy.Draw(gameTime, spriteBatch);
            explosion.Draw(spriteBatch);

            /*
            foreach(Enemy e in enemyList)
            {
                e.Draw(gameTime, eSpriteBatch);
            }*/

            highlander.Draw(gameTime, spriteBatch, _spriteBatch);
            highlander.Texture = theHighlander[spriteCounter];


            gameMenuInfo.Draw(gameTime, spriteBatch,highlander.Player.Playername, highlander.Player.Value);
            
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