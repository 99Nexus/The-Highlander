#region File Description

//-----------------------------------------------------------------------------
// InputScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
#endregion Using Statements

namespace GameStateManagement
{

    class InputScreen : MenuScreen
    {
        #region Fields

        ContentManager content;

        private Keys[] lastPreesedKeys = new Keys[5];

        private static String playerName = String.Empty;

        public static String PlayerNameIS
        {
            get { return playerName; }
            set { playerName = value; }
        }

        private Texture2D inputScreenTexture;
        private PlayerIndex? playerIndex;

        #endregion Fields

        #region Initialization

        public InputScreen(object sender, PlayerIndexEventArgs e) : base("")
        {
            playerIndex = e.PlayerIndex;
            TransitionOnTime = TimeSpan.FromSeconds(0.0);
            TransitionOffTime = TimeSpan.FromSeconds(0.0);
        }

        public override void LoadContent()
        {
            content = new ContentManager(ScreenManager.Game.Services, "Content");
            inputScreenTexture = content.Load<Texture2D>("graphics/screen_graphics/inputScreen");
            // load the saved Score list, if there is not any created list then a new list will be created

        }

        #endregion Initialization

        #region Update and Draw

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (playerName.Length < 15 || Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                GetKeys();
            }
        }
        /// <summary>
        /// Handler for when the user has chosen a menu entry.
        /// </summary>
        protected override void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
        {
            if (playerName.Length > 0)
            {
                //loadscore
                LoadingScreen.Load(ScreenManager, true, playerIndex, new GameplayScreen());
            }
        }

        /// <summary>
        /// Handler for when the user has cancelled the menu.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            playerName = String.Empty;
            ExitScreen();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            spriteBatch.Begin();
            
            spriteBatch.Draw(inputScreenTexture, new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2 - 250,
                ScreenManager.GraphicsDevice.Viewport.Height / 2 - 150), Color.White);
            
            spriteBatch.DrawString(font, playerName, new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2 - 165, ScreenManager.GraphicsDevice.Viewport.Height / 2 - 15), Color.Green * TransitionAlpha);

            //spriteBatch.DrawString(font, playerName, new Vector2(235, 224), Color.Green * TransitionAlpha);

            spriteBatch.End();
        }

        public void GetKeys()
        {
            //I press here key a
            KeyboardState kbState = Keyboard.GetState();
            //this key a will add here pressedKeys
            Keys[] pressedKeys = kbState.GetPressedKeys();

            foreach (Keys key in lastPreesedKeys)
            {
                if (!pressedKeys.Contains(key))
                {
                    //key a is no longer pressed
                    OnKeyUp(key);
                }
            }

            foreach (Keys key in pressedKeys)
            {
                if (!lastPreesedKeys.Contains(key))
                {
                    //new key I pressed
                    OnKeyDown(key);
                }
            }
            lastPreesedKeys = pressedKeys;
        }

        public void OnKeyDown(Keys key)
        {
            if (key == Keys.Back && playerName.Length > 0)
            {
                playerName = playerName.Remove(playerName.Length - 1);
            }
            if (ValidKey(key))
            {
                playerName += key.ToString();
            }
        }

        private bool ValidKey(Keys k)
        {
            if (k.ToString().Length > 1)
            {
                return false;
            }
            else
                return true;
        }

        public void OnClick() { }

        public void OnKeyUp(Keys key)
        {
            if (true)
            {

            }
        }
        #endregion Update and Draw
    }
}