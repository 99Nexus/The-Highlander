using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStateManagement.GameObjects
{
    public class HealthBar
    {
        #region Fields

        public Texture2D texture;
        public Texture2D[] textureUpgrade0;
        public Texture2D[] textureUpgrade1;
        public Texture2D[] textureUpgrade2;
        public Vector2 Position;
        public Rectangle Rectangle;
        private TheHighlander player;

        #endregion Fields

        #region Initialization

        public HealthBar(TheHighlander player)
        {
            this.player = player;
            Position = new Vector2(0, 0);
        }

        public void LoadContent(ContentManager content)
        {
            textureUpgrade0 = new Texture2D[11];
            textureUpgrade1 = new Texture2D[14];
            textureUpgrade2 = new Texture2D[15];

            for (int i = 1; i < 11; i++)
                textureUpgrade0[i] = content.Load<Texture2D>(@"graphics\game_menu_graphics\bar_0upgrade\bar_" + i + "_0upg");

            for (int i = 1; i < 13; i++)
                textureUpgrade1[i] = content.Load<Texture2D>(@"graphics\game_menu_graphics\bar_1upgrade\bar_" + i + "_1upg");

            for (int i = 1; i < 14; i++)
                textureUpgrade2[i] = content.Load<Texture2D>(@"graphics\game_menu_graphics\bar_2upgrade\bar_" + i);

            texture = textureUpgrade0[1];
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }

        #endregion Initialization

        #region Logic

        public void GetActualHealthBar()
        {
            switch (player.updateLevel)
            {
                // Before healthbar update
                case 0:
                    texture = textureUpgrade0[player.shield];
                    break;

                // After first update
                case 1:
                    texture = textureUpgrade1[player.shield];
                    break;

                // After second update
                case 2:
                    texture = textureUpgrade2[player.shield];
                    break;
            }
        }

        #endregion Logic

        #region Update and Draw

        public void Update(GameTime gameTime)
        {
            GetActualHealthBar();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

        #endregion Update and Draw
    }
}
