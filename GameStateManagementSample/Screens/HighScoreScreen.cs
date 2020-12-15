#region File Description

//-----------------------------------------------------------------------------
// HighScoreMenuScreen.cs
//amer
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using GameStateManagementSample;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

#endregion Using Statements

namespace GameStateManagement
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    internal class HighScoreScreen : MenuScreen
    {

        #region Fields

        private ScoreManager scoreManager;
        private ContentManager content;
        SpriteBatch spriteBatch;
        private SpriteFont score;

        #endregion Fields


        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public HighScoreScreen()
            : base("")
        {
        }


        
        public override void LoadContent()
        {
            content = ScreenManager.Game.Content;
            // assign the font style to the variable score
            score = content.Load<SpriteFont>("einFont");
            // load the saved Score list, if there is not any created list then a new list will be created
            scoreManager = ScoreManager.Load();
        }


        #endregion Initialization


        #region Draw

        public override void Draw(GameTime gameTime)
        {

            spriteBatch = ScreenManager.SpriteBatch;
            
            spriteBatch.Begin();

            spriteBatch.DrawString(score,"TEST TEST: \n" + string.Join("\n",
                scoreManager.Highscore.Select(c => c.Playername + ": " + c.Value).ToArray()),
                new Vector2(GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Width / 2 -75,
                GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Height / 2 -150),
                Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion Draw

    }
}