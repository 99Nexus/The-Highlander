using GameStateManagement.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using GameStateManagement.Starships;

namespace GameStateManagement.ObjectItem
{
    public class Alarm : GameObject
    {
        private bool ePressed;
        private TheHighlander highlander;

        public Alarm(Vector2 pos)
        {
            isVisible = true;
            this.position = pos;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"graphics\objects_items\AlarmSystem");
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public override void Update()
        {
            
        }

        public void Update(GameTime gameTime, TheHighlander highlander)
        {
            //hier muss eine Bedingung hin und dann wird das Objekt dort visible
            if(Keyboard.GetState().IsKeyDown(Keys.E) && (highlander.Position.X == this.position.X + 40 || highlander.Position.X == this.position.X - 40
                                                || highlander.Position.Y == this.position.Y + 40 || highlander.Position.Y == this.position.Y - 40))
            {
                ePressed = true;
            }
            
        }


        public override void Draw(SpriteBatch spriteBatch, SpriteFont sprite)
        {
            spriteBatch.Draw(texture, position, Color.White);

            if (!ePressed)
            {
                spriteBatch.DrawString(sprite, new string("Press 'E' to set \n off the alarm"), new Vector2(position.X, position.Y + 100), Color.Black);
            }
        }
    }
}