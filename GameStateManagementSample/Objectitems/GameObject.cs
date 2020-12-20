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
    public abstract class GameObject
    {
        public Rectangle rectangle;
        public Vector2 position;
        public Texture2D texture;
        public bool isVisible;

        public GameObject()
        {
        }

        public abstract void Update();
        

        public abstract void Draw(SpriteBatch spriteBatch, SpriteFont sprite);
        
    }
}