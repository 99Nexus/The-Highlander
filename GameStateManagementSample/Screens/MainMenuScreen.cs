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
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen());
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
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit?";

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
        
        #region Draw
 
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

            //Start screen logo
            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height-200);
            Vector2 startScreenLogoSize = new Vector2(startScreenLogo.Width, startScreenLogo.Height);
            Vector2 startScreenLogoPosition = (viewportSize - startScreenLogoSize) / 2;

            //Start Game
            viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textSizeStartGame = font.MeasureString("Start Game");
            Vector2 textPositionStartGame = (viewportSize - textSizeStartGame) / 2;

            //Options
            viewportSize = new Vector2(viewport.Width, viewport.Height + 100);
            Vector2 textSizeOptions = font.MeasureString("Options");
            Vector2 textPositionOptions = (viewportSize - textSizeOptions) / 2;

            //About
            viewportSize = new Vector2(viewport.Width, viewport.Height + 200);
            Vector2 textSizeAboutGame = font.MeasureString("About");
            Vector2 textPositionAboutGame = (viewportSize - textSizeAboutGame) / 2;

            //Exit
            viewportSize = new Vector2(viewport.Width, viewport.Height + 300);
            Vector2 textSizeExit = font.MeasureString("Exit");
            Vector2 textPositionExit = (viewportSize - textSizeExit) / 2;


            spriteBatch.Begin();

            // Draw the start screen logo and menu
            spriteBatch.Draw(startScreenLogo, startScreenLogoPosition, Color.White);
            //spriteBatch.DrawString(font, "Start Game", textPositionStartGame, Color.LightGray);
            //spriteBatch.DrawString(font, "Options", textPositionOptions, Color.LightGray);
            //spriteBatch.DrawString(font, "About", textPositionAboutGame, Color.LightGray);
            //spriteBatch.DrawString(font, "Exit", textPositionExit, Color.LightGray);

            // Create our menu entries.
            MenuEntry startGameMenuEntry = new MenuEntry("Start Game");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry aboutGameMenuEntry = new MenuEntry("About");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Hook up menu event handlers.
            startGameMenuEntry.Selected += StartGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            aboutGameMenuEntry.Selected += AboutGameMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;
            
            // Add entries to the menu.
            MenuEntries.Add(startGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(aboutGameMenuEntry);
            MenuEntries.Add(exitMenuEntry);

            spriteBatch.End();
        }

        #endregion Draw
    }
}