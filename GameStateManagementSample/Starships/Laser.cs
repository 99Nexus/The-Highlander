using GameStateManagement.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;


public class Laser
{
    public Texture2D texture;
    public Vector2 Origin;
    public Vector2 Position;
    public Vector2 direction;
    public Rectangle Rectangle;
    public float rotation;
    public bool isVisible;
    public float speed;
    public int steps;
    public int id;

    public Laser(Texture2D newTexture)
    {
        speed = 10;
        texture = newTexture;
        isVisible = false;
        steps = 0;
    }

    public void Update(GameTime gameTime)
    {
        Rectangle = new Rectangle((int)Position.X - (texture.Width / 2), 
                                  (int)Position.Y - (texture.Height / 2), 
                                  texture.Width,
                                  texture.Height);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Position, null, Color.White, rotation, Origin, 1, SpriteEffects.None, 0);
    }
}