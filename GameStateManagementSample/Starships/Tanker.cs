﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagement.Starships
{
    public class Tanker : Enemy
    {
        public Tanker(Vector2 position, int maxShield, int weaponPower, float linearVelocity, 
            Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode) : 
            base(position, maxShield, weaponPower, linearVelocity, end, playerPosition, keepDistanceToPlayer, movementMode)
        {
            score = 1000;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            texture = new Texture2D[3];
            texture[0] = content.Load<Texture2D>(@"graphics\starships\tanker1");
            texture[1] = content.Load<Texture2D>(@"graphics\starships\tanker2");
            texture[2] = content.Load<Texture2D>(@"graphics\starships\tanker3");
        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            base.Update(gameTime, playerPosition);

            //if (actualShield == 7)
            //spawn mini enemies
        }
    }
}
