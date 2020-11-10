#region File Description

//-----------------------------------------------------------------------------
// AboutGameScreen.cs
//-----------------------------------------------------------------------------

#endregion File Description

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameStateManagement
{
    class AboutGameScreen : MenuScreen
    {
        #region Fields

        private Texture2D about_game_text;
        private GraphicsDeviceManager graphics;
        private ContentManager content;

        #endregion Fields

        #region Initialization

        // Constructor
        public AboutGameScreen()
            : base("")
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.0);
            TransitionOffTime = TimeSpan.FromSeconds(0.0);
        }

        #endregion Initialization


        #region LoadContent

        //Bild hochladen
        public override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            ContentManager content = ScreenManager.Game.Content;

            about_game_text = content.Load<Texture2D>(@"graphics\screen_graphics\about_game_text");

        }

        #endregion Initialization

        #region Draw

        public override void Draw(GameTime gameTime)
        {

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height + 50);
            Vector2 textSize = new Vector2(about_game_text.Width, about_game_text.Height);
            Vector2 textPosition = (viewportSize - textSize) / 2;

            // Anfang des SpriteBatches
            spriteBatch.Begin();

            spriteBatch.Draw(about_game_text, textPosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion Draw

    }
}
