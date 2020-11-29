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
    public Rectangle boundingbox;
    public Texture2D texture;
    public Vector2 origin;
    public Vector2 position;
    public bool isVisible;
    public float speed;
    public int steps;
    public Laser(Texture2D newTexture)
    {
        speed = 10;
        texture = newTexture;
        isVisible = false;
        steps = 0;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, Color.White);
    }
}