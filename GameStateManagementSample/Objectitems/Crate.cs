using GameStateManagement.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStateManagement.ObjectItem
{
    class Crate : GameObject
    {
        public Crate(Vector2 pos)
        {
            isVisible = false;
            position = pos;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"graphics\objects_items\Crate");
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont sprite)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}