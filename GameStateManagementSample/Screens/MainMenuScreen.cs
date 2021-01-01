#region File Description

//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

#endregion Using Statements

namespace GameStateManagement
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    internal class MainMenuScreen : MenuScreen
    {

        #region Fields

        private Texture2D startScreenLogo;


        #endregion Fields

        #region Initialization

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("")
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.0);
            TransitionOffTime = TimeSpan.FromSeconds(0.0);

            // Create our menu entries.
            MenuEntry startGameMenuEntry = new MenuEntry("Start Game");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry highScoreMenuEntry = new MenuEntry("High Score");
            MenuEntry aboutGameMenuEntry = new MenuEntry("About");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Hook up menu event handlers.
            startGameMenuEntry.Selected += StartGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            highScoreMenuEntry.Selected += highScoreMenuEntrySelected;
            aboutGameMenuEntry.Selected += AboutGameMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(startGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(highScoreMenuEntry);
            MenuEntries.Add(aboutGameMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            startScreenLogo = content.Load<Texture2D>(@"graphics\screen_graphics\start_screen_logo");
            
        }

        #endregion Initialization

        #region Handle Input

        /// <summary>
        /// Event handler for when the Start Game menu entry is selected.
        /// </summary>
        private void StartGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new InputScreen(sender, e), e.PlayerIndex);
            /*
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen());
            */
        }

        /// <summary>
        /// Event handler for when the About Game menu entry is selected.
        /// </summary>
        private void AboutGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new AboutGameScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        private void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// Event handler for when the High Score menu entry is selected.
        /// </summary>
        private void highScoreMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new HighScoreScreen(), e.PlayerIndex);
        }


        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Do you want to exit Captain?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }

        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }

        #endregion Handle         

        #region Update and Draw

        /// <summary>
        /// Draws the start screen logo
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager screenManager = this.ScreenManager;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = screenManager.Font;

            // Center menu entrys in the viewport
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height-250);
            Vector2 startScreenLogoSize = new Vector2(startScreenLogo.Width, startScreenLogo.Height);
            Vector2 startScreenLogoPosition = (viewportSize - startScreenLogoSize) / 2;
            
            
            //amer; full screen switch, logo postion
            if (GameStateManagementGame.Newgame.Graphics.IsFullScreen)
            {
                startScreenLogoPosition.X =  (viewportSize.X - startScreenLogoSize.X) / 2;
                startScreenLogoPosition.Y = 0 ;
            }
            else
            {
                startScreenLogoPosition = (viewportSize - startScreenLogoSize) / 2;
            }
            
                spriteBatch.Begin();

            // Draw the start screen logo and menu
            spriteBatch.Draw(startScreenLogo, startScreenLogoPosition, Color.White);
            
            spriteBatch.End();

            // Call Draw method of the class MainMenuScreen to draw the menu entrys too
            base.Draw(gameTime);
        }
        #endregion Update and Draw
    }
}