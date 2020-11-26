using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Color = Microsoft.Xna.Framework.Color;

namespace GameStateManagement.Starships
{
    class Laser
    {
        public Texture2D texture;

        public Vector2 position;
        public float velocity = 4f;
        public Vector2 origin;
        public Vector2 direction;
        public float rotation;

        public bool isVisible;

        public Laser(Texture2D newTexture)
        {
            texture = newTexture;
            isVisible = false;
        }

        public Laser(Vector2 direction, float rotation)
        {
            this.direction = direction;
            this.rotation = rotation;
        }

        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            position = playerPos;
            position += velocity * direction;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
