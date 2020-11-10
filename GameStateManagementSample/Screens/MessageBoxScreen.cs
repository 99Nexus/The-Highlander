#region File Description

//-----------------------------------------------------------------------------
// MessageBoxScreen.cs
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
    /// A popup message box screen, used to display "are you sure?"
    /// confirmation messages.
    /// </summary>
    internal class MessageBoxScreen : GameScreen
    {
        #region Fields

        private string message;
        private Texture2D gradientTexture;

        #endregion Fields

        #region Events

        public event EventHandler<PlayerIndexEventArgs> Accepted;

        public event EventHandler<PlayerIndexEventArgs> Cancelled;

        #endregion Events

        #region Initialization

        /// <summary>
        /// Constructor automatically includes the standard "A=ok, B=cancel"
        /// usage text prompt.
        /// </summary>
        public MessageBoxScreen(string message)
        {
            this.message = message;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);

            IsPopup = true;
        }

        /// <summary>
        /// Loads graphics content for this screen. This uses the shared ContentManager
        /// provided by the Game class, so the content will remain loaded forever.
        /// Whenever a subsequent MessageBoxScreen tries to load this same content,
        /// it will just get back another reference to the already loaded data.
        /// </summary>
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            gradientTexture = content.Load<Texture2D>(@"graphics\screen_graphics\esc_menu_background");
        }

        #endregion Initialization

        #region Handle Input

        /// <summary>
        /// Responds to user input, accepting or cancelling the message box.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            PlayerIndex playerIndex;

            // We pass in our ControllingPlayer, which may either be null (to
            // accept input from any player) or a specific index. If we pass a null
            // controlling player, the InputState helper returns to us which player
            // actually provided the input. We pass that through to our Accepted and
            // Cancelled events, so they can tell which player triggered them.
            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                // Raise the accepted event, then exit the message box.
                if (Accepted != null)
                    Accepted(this, new PlayerIndexEventArgs(playerIndex));

                ExitScreen();
            }
            else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
            {
                // Raise the cancelled event, then exit the message box.
                if (Cancelled != null)
                    Cancelled(this, new PlayerIndexEventArgs(playerIndex));

                ExitScreen();
            }
        }

        #endregion Handle Input

        #region Draw

        /// <summary>
        /// Draws the message box.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;
            String yesOption = "Yes = Space, Enter";
            String noOption = "No = Esc";

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            // Center the message text in the viewport.
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

            // Question
            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height-30);
            Vector2 questionSize = font.MeasureString(message);
            Vector2 questionPosition = (viewportSize - questionSize) / 2;

            // Yes-option
            viewportSize = new Vector2(viewport.Width, viewport.Height + 80);
            Vector2 yesOptionSize = font.MeasureString(yesOption);
            Vector2 yesOptionPosition = (viewportSize - yesOptionSize) / 2;

            // No-option
            viewportSize = new Vector2(viewport.Width, viewport.Height + 160);
            Vector2 noOptionSize = font.MeasureString(noOption);
            Vector2 noOptionPosition = (viewportSize - noOptionSize) / 2;

            // Background
            viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 backgroudSize = new Vector2(gradientTexture.Width, gradientTexture.Height);
            Vector2 backgroundPosition = (viewportSize - backgroudSize) / 2;

            // Fade the popup alpha during transitions.
            Color color = Color.White * TransitionAlpha;
            Color optionsColor = new Color(130, 2, 2);

            spriteBatch.Begin();

            // Draw the background rectangle.
            spriteBatch.Draw(gradientTexture, backgroundPosition, color);

            // Draw the message box text.
            spriteBatch.DrawString(font, message, questionPosition, optionsColor);
            spriteBatch.DrawString(font, yesOption, yesOptionPosition, optionsColor);
            spriteBatch.DrawString(font, noOption, noOptionPosition, optionsColor);

            spriteBatch.End();
        }

        #endregion Draw
    }
}