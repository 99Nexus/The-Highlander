using GameStateManagement.GameObjects;
using GameStateManagement.Starships;
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
        public TheHighlander player;
        public bool keyPressed = false;
        public Vector2 Origin;
        public GameObject(Vector2 pos, TheHighlander player)
        {
            isVisible = true;
            this.position = pos;
            this.player = player;
        }

        public double CalculateDistanceToPlayer()
        {
            Vector2 distanceVector;

            distanceVector.X = player.Position.X - position.X;
            distanceVector.Y = player.Position.Y - position.Y;

            return distanceVector.Length();
        }

    

        public virtual void Update(GameTime gameTime, TheHighlander highlander)
        {
            //hier muss eine Bedingung hin und dann wird das Objekt dort visible
            if (Keyboard.GetState().IsKeyDown(Keys.E) && CalculateDistanceToPlayer() <= 80)
            {
                keyPressed = true;
            }

        }


        public virtual void Draw(SpriteBatch spriteBatch, SpriteFont sprite)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, Origin, 1, SpriteEffects.None, 0);
        }
        
    }
}