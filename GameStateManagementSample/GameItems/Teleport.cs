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
        private Texture2D testText;
        public Teleport(Texture2D texture, Vector2 pos)
        {
            position = pos;
            this.testText = texture;
        }

        public Teleport(Vector2 pos)
        {
            position = pos;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"graphics\objects_items\Crate");
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(testText, position, Color.White);
        }
    }
}
