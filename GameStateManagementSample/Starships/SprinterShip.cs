using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement.Starships
{
    public class SprinterShip : Enemy
    {
        public SprinterShip(Vector2 position,
            Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode) :
            base(position, 2, 1, 4f, end, playerPosition, keepDistanceToPlayer, movementMode, 150)
        { }

        public override void LoadContent(ContentManager content)
        {
            texture = new Texture2D[3];
            texture[0] = content.Load<Texture2D>(@"graphics\starships\sprinterShip1");
            texture[1] = content.Load<Texture2D>(@"graphics\starships\sprinterShip2");
            texture[2] = content.Load<Texture2D>(@"graphics\starships\sprinterShip3");
            base.LoadContent(content);
        }
    }
}
