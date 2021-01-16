﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Color = Microsoft.Xna.Framework.Color;

namespace GameStateManagement.Screens
{
    class WinScreen : MenuScreen
    {
        private const int V = 12;
        public Texture2D texture;
        private ContentManager content;
        //private SpriteFont font;

        private int score;
        private string playerName;

        MenuEntry goBackToMainMenuMenuEntry;

        #region Initialization

        public WinScreen(string pName, int pscore)
            : base("")
        {
            score = pscore;
            playerName = pName;
            
            TransitionOnTime = TimeSpan.FromSeconds(0.0);

            TransitionOffTime = TimeSpan.FromSeconds(0.0);
            
            goBackToMainMenuMenuEntry = new MenuEntry("Back To Main Menu");
            
            goBackToMainMenuMenuEntry.Selected += GoBackToMainMenuMenuEntrySelected;

            MenuEntries.Add(goBackToMainMenuMenuEntry);
        }

        #endregion Initialization

        public override void LoadContent()
        {
            content = new ContentManager(ScreenManager.Game.Services, "Content");
            //font = content.Load<SpriteFont>(@"spritefonts\game_menu_fonts\score_font");
            texture = content.Load<Texture2D>(@"graphics\screen_graphics\winScreen");
            
        }

        private void GoBackToMainMenuMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            InputScreen.PlayerNameIS = String.Empty;
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;
            int Width = ScreenManager.GraphicsDevice.Viewport.Width;
            int Height = ScreenManager.GraphicsDevice.Viewport.Height;
            goBackToMainMenuMenuEntry.Position = new Vector2(Width / 2 - 170, Height / 2 + 70);

            spriteBatch.Begin();
            goBackToMainMenuMenuEntry.Draw(this, true, gameTime);
            spriteBatch.Draw(texture, new Vector2(Width / 2 - 250, Height / 2 - 150), Color.White);

            spriteBatch.DrawString(font, playerName + "          " + score, new Vector2(Width / 2 - 70, Height / 2 -20), Color.Green * TransitionAlpha);
            goBackToMainMenuMenuEntry.Draw(this, true, gameTime);
            spriteBatch.End();
        }
    }
}
