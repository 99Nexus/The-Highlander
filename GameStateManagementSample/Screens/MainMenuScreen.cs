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
        private SpriteFont startGameMenuFont;
        private SpriteFont aboutGameMenuFont;
        private SpriteFont optionsMenuFont;
        private SpriteFont exitMenuFont;
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
            startGameMenuFont = content.Load<SpriteFont>(@"spritefonts\screen_fonts\start_screen_font");
            aboutGameMenuFont = content.Load<SpriteFont>(@"spritefonts\screen_fonts\start_screen_font");
            optionsMenuFont = content.Load<SpriteFont>(@"spritefonts\screen_fonts\start_screen_font");
            exitMenuFont = content.Load<SpriteFont>(@"spritefonts\screen_fonts\start_screen_font");
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
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            // Center menu entrys in the viewport
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

            //Start screen logo
            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height-200);
            Vector2 startScreenLogoSize = new Vector2(startScreenLogo.Width, startScreenLogo.Height);
            Vector2 startScreenLogoPosition = (viewportSize - startScreenLogoSize) / 2;

            //Start Game
            viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textSizeStartGame = startGameMenuFont.MeasureString("Start Game");
            Vector2 textPositionStartGame = (viewportSize - textSizeStartGame) / 2;

            //About Game
            viewportSize = new Vector2(viewport.Width, viewport.Height + 100);
            Vector2 textSizeAboutGame = aboutGameMenuFont.MeasureString("About");
            Vector2 textPositionAboutGame = (viewportSize - textSizeAboutGame) / 2;

            //Options
            viewportSize = new Vector2(viewport.Width, viewport.Height + 200);
            Vector2 textSizeOptions = optionsMenuFont.MeasureString("Options");
            Vector2 textPositionOptions = (viewportSize - textSizeOptions) / 2;

            //Exit
            viewportSize = new Vector2(viewport.Width, viewport.Height + 300);
            Vector2 textSizeExit = exitMenuFont.MeasureString("Exit");
            Vector2 textPositionExit = (viewportSize - textSizeExit) / 2;


            spriteBatch.Begin();

            // Draw the start screen logo and menu
            spriteBatch.Draw(startScreenLogo, startScreenLogoPosition, Color.White);
            spriteBatch.DrawString(startGameMenuFont, "Start Game", textPositionStartGame, Color.LightGray);
            spriteBatch.DrawString(aboutGameMenuFont, "About", textPositionAboutGame, Color.LightGray);
            spriteBatch.DrawString(optionsMenuFont, "Options", textPositionOptions, Color.LightGray);
            spriteBatch.DrawString(exitMenuFont, "Exit", textPositionExit, Color.LightGray);

            // Create our menu entries.
            MenuEntry startGameMenuEntry = new MenuEntry(startGameMenuFont);
            MenuEntry aboutGameMenuEntry = new MenuEntry(aboutGameMenuFont);
            MenuEntry optionsMenuEntry = new MenuEntry(optionsMenuFont);
            MenuEntry exitMenuEntry = new MenuEntry(exitMenuFont);

            // Hook up menu event handlers.
            startGameMenuEntry.Selected += StartGameMenuEntrySelected;
            aboutGameMenuEntry.Selected += AboutGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;
            
            // Add entries to the menu.
            MenuEntries.Add(startGameMenuEntry);
            MenuEntries.Add(aboutGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);

            spriteBatch.End();
        }

        #endregion Draw
    }
}