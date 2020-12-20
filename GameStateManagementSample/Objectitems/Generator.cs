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
    class Generator : GameObject
    {
        public int actualShield;
        private int maxShield;
        private string shieldString;
        public int damageBuffer;
        public int maxDamageBuffer;
        private SpriteFont spriteFont;
        private TheHighlander highlander;

        public Generator(Vector2 pos)
        {
            isVisible = true;
            this.maxShield = 5;
            actualShield = maxShield;
            damageBuffer = 0;
            maxDamageBuffer = 20;
            isVisible = true;
            this.position = pos;
        }



        public void UpdateActualShieldValue(int damage)
        {
            if (actualShield - damage < 1)
            {
                //public void ManageExplosions()
                actualShield = 0;
            }
            else
            {
                actualShield -= damage;

                // Update the shield string
                shieldString = actualShield + " | " + maxShield;
            }
        }


        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"graphics\objects_items\Generator");
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }


        public override void Update()
        {
            //hier muss eine Bedingung hin und dann wird das Objekt dort visible
            
        }


        public override void Draw(SpriteBatch spriteBatch, SpriteFont sprite)
        {
            spriteBatch.Draw(texture, position, Color.White);

            spriteBatch.DrawString(sprite, new string("Destroy the \n generator"), new Vector2(position.X + 20, position.Y + 126), Color.Black);
        }

    }
}