using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagement.Starships
{
    class Doomer : Enemy
    {
        public Doomer(Vector2 position, int maxShield, int weaponPower, float linearVelocity,
            Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode) :
            base(position, maxShield, weaponPower, linearVelocity, end, playerPosition, keepDistanceToPlayer, movementMode)
        {
            score = 5000;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            texture = new Texture2D[3];
            texture[0] = content.Load<Texture2D>(@"graphics\starships\doomer1");
            texture[1] = content.Load<Texture2D>(@"graphics\starships\doomer2");
            texture[2] = content.Load<Texture2D>(@"graphics\starships\doomer3");
        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            base.Update(gameTime, playerPosition);

            //if (actualShield == 8)
            //spawn mini enemies
        }
    }
}
