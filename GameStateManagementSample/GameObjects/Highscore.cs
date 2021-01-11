#region File Description

//-----------------------------------------------------------------------------
// Highscore.cs
//playerScore is a better name
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements
using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion Using Statements

namespace GameStateManagement.GameObjects
{
    //playerScore is a better name
    public class Highscore
    {
        #region Fields
        private SpriteFont sprite;
        public Vector2 Position;
        public Rectangle Rectangle;
        private TheHighlander player;
        #endregion Fields

        #region Initialization
        public Highscore(TheHighlander theHighlander)
        {
            player = theHighlander;
            Position = new Vector2(GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Width - 125, 30);
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<SpriteFont>(@"spritefonts\game_menu_fonts\score_font_screen");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 100, 55);
        }
        #endregion Initialization

        #region Draw
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(sprite, player.PlayerScore.Playername + "\n Score: " + player.PlayerScore.Value, Position, Color.White);
        }
        #endregion Draw
    }
}
