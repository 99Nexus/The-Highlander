using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement.Starships
{
    public class TankerShip : Enemy
    {
        public TankerShip(Vector2 position, Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode) : base(position, 3, 1, 2f, end, playerPosition, keepDistanceToPlayer, movementMode, 100)
        { }

        public override void LoadContent(ContentManager content)
        {
            texture = new Texture2D[3];
            texture[0] = content.Load<Texture2D>(@"graphics\starships\tankerShip1");
            texture[1] = content.Load<Texture2D>(@"graphics\starships\tankerShip2");
            texture[2] = content.Load<Texture2D>(@"graphics\starships\tankerShip3");
            base.LoadContent(content);
        }
    }
}
