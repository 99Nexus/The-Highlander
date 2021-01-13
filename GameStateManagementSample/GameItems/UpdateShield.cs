using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace GameStateManagement.GameItems
{
    class UpdateShield : GameItem
    {
        public TheHighlander player;
        public UpdateShield(Vector2 pos)
        {
            position = pos;
        }


        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"graphics\game_items\HealthPoint");


            rectangle = new Rectangle((int)position.X - (texture.Width / 2),
                                      (int)position.Y - (texture.Height / 2),
                                      texture.Width,
                                      texture.Height);
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void shieldBonus()
        {
            if(player.shield < 13)
            {
                player.shield += 1;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
