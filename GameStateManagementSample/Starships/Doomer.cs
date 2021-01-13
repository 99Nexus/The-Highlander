using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement.Starships
{
    public class Doomer : Enemy
    {
        public Doomer(Vector2 position,
            Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode) :
            base(position, 17, 2, 4f, end, playerPosition, keepDistanceToPlayer, movementMode, 5000)
        { }

        public override void LoadContent(ContentManager content)
        {
            texture = new Texture2D[3];
            texture[0] = content.Load<Texture2D>(@"graphics\starships\doomer1");
            texture[1] = content.Load<Texture2D>(@"graphics\starships\doomer2");
            texture[2] = content.Load<Texture2D>(@"graphics\starships\doomer3");
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            base.Update(gameTime, playerPosition);
        }
    }
}
