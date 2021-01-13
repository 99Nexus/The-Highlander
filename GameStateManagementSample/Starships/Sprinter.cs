using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement.Starships
{
    public class Sprinter : Enemy
    {
        public Sprinter(Vector2 position,
            Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode) :
            base(position, 12, 1, 4f, end, playerPosition, keepDistanceToPlayer, movementMode, 2000)
        { }

        public override void LoadContent(ContentManager content)
        {
            texture = new Texture2D[3];
            texture[0] = content.Load<Texture2D>(@"graphics\starships\sprinter1");
            texture[1] = content.Load<Texture2D>(@"graphics\starships\sprinter2");
            texture[2] = content.Load<Texture2D>(@"graphics\starships\sprinter3");
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            base.Update(gameTime, playerPosition);
        }
    }
}
