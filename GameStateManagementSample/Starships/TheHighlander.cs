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

        #endregion Fields

        #region Initialization

        public TheHighlander(Texture2D texture)
        {
            this.texture = texture;
        }

        #endregion Initialization

        #region Update and Draw

        public void Update(GameTime gameTime)
        {

        }

        public void HandleInput()
        {
            /*
            Vector2 movement = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {

                if (rotation >= -1)
                    rotation -= 0.05f;

                if (rotation < -1)
                    movement.X--;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {

                if (rotation <= 1)
                    rotation += 0.05f;

                if (rotation > 1)
                    movement.X++;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                movement.Y--;

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                movement.Y++;


            if (movement.Length() > 1)
                movement.Normalize();

            position += movement * 2;
            */

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                rotation -= MathHelper.ToRadians(rotationVelocity);
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                rotation += MathHelper.ToRadians(rotationVelocity);

            Vector2 direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - rotation));

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                Position += direction * linearVelocity;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, Position, null, Color.White, rotation, Origin, 1, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        #endregion Update and Draw
    }
}
