using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStateManagement.Starships
{
    class Enemy
    {
        // Attributes
        private Texture2D texture;
        private SpriteFont spriteFont;
        private static int actualShield;
        private int maxShield;
        private int weaponPower;
        private double speed;
        private float rotation;
        private string shieldString;
        private Vector2 textureSize;

        // Properties
        public Texture2D Texture;
        public SpriteFont SpriteFont;
        public int AsctualShield;
        public int WeaponPower;
        public double Speed;
        public float Rotation;
        public Vector2 Position;
        public Vector2 Origin;

        public Enemy(Texture2D texture, SpriteFont spriteFont, int maxShield, int weaponPower, double speed, float rotation) 
        {
            this.texture = texture;
            this.spriteFont = spriteFont;
            this.maxShield = maxShield;
            this.weaponPower = weaponPower;
            this.speed = speed;
            this.rotation = rotation;
            actualShield = maxShield;
            this.shieldString = actualShield + " | " + maxShield;
            this.textureSize = new Vector2(texture.Width, texture.Height);
        }

        public void ShootOnPlayer(Vector2 playerPosition)
        {

        }

        public void Move(Vector2 playerPosition)
        {

        }
        public void CalculateActualShieldValue()
        {

        }

        public Vector2 CalculateShieldPosition()
        {
            Vector2 shieldStringSize = spriteFont.MeasureString(shieldString);
            Vector2 shieldPosition = (textureSize - shieldStringSize) / 2;

            return new Vector2(shieldPosition.X, Position.Y - 20);
        }

        public void Update(GameTime gameTime)
        {
            CalculateActualShieldValue();
            shieldString = actualShield + " | " + maxShield;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // If not defeated
            if(actualShield > 0)
            {
                spriteBatch.DrawString(spriteFont, shieldString, CalculateShieldPosition(), Color.White);
                spriteBatch.Draw(texture, Position, null, Color.White, rotation, Origin, 1, SpriteEffects.None, 0);
            }
        }
    }
}
