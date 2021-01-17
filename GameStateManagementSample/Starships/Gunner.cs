using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement.Starships
{
    class Gunner : Enemy
    {
        public Gunner(Vector2 position, Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode) : base(position, 13, 2, 3f, end, playerPosition, keepDistanceToPlayer, movementMode, 3000)
        { }

        public override void LoadContent(ContentManager content)
        {
            texture = new Texture2D[3];
            texture[0] = content.Load<Texture2D>(@"graphics\starships\gunner1");
            texture[1] = content.Load<Texture2D>(@"graphics\starships\gunner2");
            texture[2] = content.Load<Texture2D>(@"graphics\starships\gunner3");
            base.LoadContent(content);
        }
    }
}
