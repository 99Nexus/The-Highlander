#region File Description

//-----------------------------------------------------------------------------
// HighScoreMenuScreen.cs
//amer
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

#endregion Using Statements

namespace GameStateManagement
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class HighScoreScreen : MenuScreen
    {
        #region Fields

        private ScoreManager scoreManager;
        private ContentManager content;
        SpriteBatch spriteBatch;
        private SpriteFont scoreFont;

        #endregion Fields


        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public HighScoreScreen() : base("")
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.0);
            TransitionOffTime = TimeSpan.FromSeconds(0.0);
        }

        public override void LoadContent()
        {
            content = ScreenManager.Game.Content;
            // assign the font style to the variable score
            scoreFont = content.Load<SpriteFont>(@"spritefonts\game_menu_fonts\score_font");
            // load the saved Score list, if there is not any created list then a new list will be created
            scoreManager = ScoreManager.Load();
        }

        /// <summary>
        /// Handler for when the user has chosen a menu entry.
        /// </summary>
        protected override void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
        {
            ExitScreen();
        }

        #endregion Initialization

        #region Draw

        public override void Draw(GameTime gameTime)
        {

            spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.DrawString(scoreFont, string.Join("\n",
                scoreManager.Highscore.Select(c => c.Playername + ": " + c.Value).ToArray()),
                new Vector2(GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Width / 2 - 75,
                GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Height / 2 - 150),
                Color.GreenYellow);

            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion Draw
    }
}