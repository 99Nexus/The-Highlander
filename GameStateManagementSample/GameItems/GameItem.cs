using System;
using System.Collections.Generic;
using System.Text;
using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameStateManagement.GameItems
{
    public abstract class GameItem
    {
        public Rectangle rectangle;
        public Vector2 position;
        public Texture2D texture;
        public bool isVisible = false;
        public Vector2 Origin;


        public virtual void LoadContent(ContentManager content) {}

        public void Update()
        {
            isVisible = true;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        
    }
}
