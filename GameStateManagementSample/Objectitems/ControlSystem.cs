using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement.Starships;

namespace GameStateManagement.ObjectItem
{
    public class ControlSystem : GameObject
    {
        public ControlSystem(Vector2 pos, TheHighlander theHighlander) : base(pos, theHighlander) { }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"graphics\objects_items\ControlSystem");
            rectangle = new Rectangle((int)position.X - (texture.Width / 2),
                                      (int)position.Y - (texture.Height / 2),
                                      texture.Width,
                                      texture.Height);

            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont sprite)
        {
            base.Draw(spriteBatch, sprite);
            spriteBatch.DrawString(sprite, CalculateDistanceToPlayer().ToString(), new Vector2(position.X - 20, position.Y + 100), Color.Black);

            if (CalculateDistanceToPlayer() <= 100 && !keyPressed)
            {
                spriteBatch.DrawString(sprite, new string("Press 'E' to turn on \n the control system"), new Vector2(position.X - 10, position.Y + 75), Color.Black);
            }
        }
    }
}
