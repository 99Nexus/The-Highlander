using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStateManagement.GameItems
{
    public class Teleport : GameItem
    {

        bool isVisible;

        public Teleport(Vector2 pos)
        {
            position = pos;
            isVisible = false;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"graphics\game_items\Teleport");

            // Rectangle für die Kollision, um den Spieler, wenn er den Teleporter berührt woanders zu spawnen
            rectangle = new Rectangle((int)position.X - (texture.Width / 2),
                                      (int)position.Y - (texture.Height / 2),
                                      texture.Width,
                                      texture.Height);
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
