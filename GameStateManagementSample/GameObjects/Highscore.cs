using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStateManagement.GameObjects
{
    public class Highscore
    {
        private SpriteFont sprite;
        public Vector2 Position;
        public Rectangle Rectangle;
        private TheHighlander player;
        public String highscore;

        public Highscore(TheHighlander player)
        {
            this.player = player;
            highscore = player.PlayerScore.Playername + "\n Score: " + player.PlayerScore.Value;
            Position = new Vector2(GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Width - 125, 30);
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<SpriteFont>(@"spritefonts\game_menu_fonts\score_font");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 100, 55);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(sprite, highscore, Position, Color.White);
        }
    }
}
