using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement.ObjectItem
{
    public class Crate : GameObject
    {
        public Crate(Vector2 pos, TheHighlander theHighlander) : base(pos, theHighlander) { }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"graphics\objects_items\Crate");
            rectangle = new Rectangle((int)position.X - (texture.Width / 2),
                                      (int)position.Y - (texture.Height / 2),
                                      texture.Width,
                                      texture.Height);
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont sprite)
        {
            base.Draw(spriteBatch, sprite);
        }
    }
}