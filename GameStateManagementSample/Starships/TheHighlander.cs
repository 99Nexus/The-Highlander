using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStateManagement.Starships
{
    class TheHighlander
    {
        #region Fields

        private Texture2D texture;
        private float rotation;
        public float rotationVelocity = 3f;
        public float linearVelocity = 4f;

        // Properties
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Origin;

        

        //amer, liber das lassen für testen
        private SpriteFont einFont;

        #endregion Fields

        #region Initialization

        public TheHighlander(Texture2D texture, SpriteFont einFont)
        {
            this.texture = texture;
            this.einFont = einFont;


        }

        #endregion Initialization

        #region Update and Draw

        public void Update(GameTime gameTime)
        {

        }
        
        //mit dieser Methode wird überprüft ob die Position erlaubt oder nicht  
        public bool IsValid()
        {

            if (Position.Y > GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Height - 35 || Position.Y < 25.0)
            {
                return false;
                
            }
            else if (Position.X > GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Width - 45  || Position.X < 25.0)
            {
                return false;
            }

            return true;
        }


        public void HandleInput()
        {            
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                rotation -= MathHelper.ToRadians(rotationVelocity);
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                rotation += MathHelper.ToRadians(rotationVelocity);

            Vector2 direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - rotation));



            //erst überprüfen ob das Bildschirm im Fullscreen oder nicht
            //Dann wenn Up gedrückt wird, wird die Methode Move()
            if (!GameStateManagementGame.Newgame.Graphics.IsFullScreen) { 
                if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                    Move(direction);
                }
            }
            //Im FullScreen
            else if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                Move(direction);
            }

        }

        //Hier wird überprüfen ob die Position erlaubt ist oder nicht
        //wenn nicht erlaubt, wird die Posistion des Spielers in der Mitte eingesetzt

        public void Move( Vector2 direction)
        {
            if (IsValid())
            {
                Position += direction * linearVelocity;
            }
            else
            {
                Position = new Vector2(GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Width / 2,
                       GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Height - 50);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, SpriteBatch _spriteBatch)
        {
            
            spriteBatch.Begin();
            spriteBatch.Draw(texture, Position, null, Color.White, rotation, Origin, 1, SpriteEffects.None, 0);
            spriteBatch.End();
            _spriteBatch.Begin();
            
            _spriteBatch.DrawString(einFont, new string("Y " + Position.Y.ToString()), new Vector2(30, 100), Color.Black);
            _spriteBatch.DrawString(einFont, new string("X " + Position.X.ToString()), new Vector2(30, 130), Color.Black);
            
            _spriteBatch.End();
        }

        #endregion Update and Draw
    }
}
