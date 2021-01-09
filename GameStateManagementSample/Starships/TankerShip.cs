﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagement.Starships
{
    public class TankerShip : Enemy
    {
        public TankerShip(Vector2 position, int maxShield, int weaponPower, float linearVelocity,
            Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode) :
            base(position, maxShield, weaponPower, linearVelocity, end, playerPosition, keepDistanceToPlayer, movementMode)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            texture = new Texture2D[3];
            texture[0] = content.Load<Texture2D>(@"graphics\starships\tankerShip1");
            texture[1] = content.Load<Texture2D>(@"graphics\starships\tankerShip2");
            texture[2] = content.Load<Texture2D>(@"graphics\starships\tankerShip3");
        }
    }
}
