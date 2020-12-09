using GameStateManagement.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagement.Starships
{
    #region Fields

    public enum MovementMode
    {
        HORIZONTAL,
        VERTICAL,
        PATROL
    }

    public class Enemy
    {
        // Graphical attributes
        private Texture2D[] texture;
        private SpriteFont spriteFont;
        private string shieldString;
        private Vector2 textureSize;
        public Rectangle enemyBox;
        public bool isVisible;
        public int spriteCounter = 0;

        // Shooting attributes
        public Vector2 laserVelocity;
        public Texture2D laserTexture;
        public float laserDelay;
        public List<Laser> laserList;
        float rotationLaser;

        // State attributes
        private static int actualShield;
        private int maxShield;
        private int weaponPower;
        private float linearVelocity;

        // Movement attributes
        private Vector2 position;
        private float Rotation;
        private double keepDistanceToPlayer;
        public float rotationVelocity = 3f;
        public bool turnDirectionToStartPoint = false;
        public Vector2 start;
        public Vector2 end;
        public Vector2 playerPosition;
        public Vector2 Direction;
        public MovementMode movementMode;

        // Properties
        public Texture2D Texture;
        public SpriteFont SpriteFont;
        public int AsctualShield;
        public int WeaponPower;
        public double LinearVelocity;
        public Vector2 Position;
        public Vector2 Origin;
        public MovementMode MovementMode;

        public List<Laser> bulletList;
        #endregion Fields

        #region Inialization

        public Enemy(Vector2 position, int maxShield, int weaponPower, float linearVelocity,
            Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode) 
        {
            this.Position = position;
            this.maxShield = maxShield;
            this.weaponPower = weaponPower;
            this.linearVelocity = linearVelocity;
            actualShield = maxShield;
            this.shieldString = actualShield + " | " + maxShield;
            this.start = position;
            this.end = end;
            this.playerPosition = playerPosition;
            this.keepDistanceToPlayer = keepDistanceToPlayer;
            this.movementMode = movementMode;
            this.isVisible = true;

            laserList = new List<Laser>();
            laserDelay = 50;

            // Set values for the actual movement mode
            changeMovementMode(movementMode);
        }

        public void LoadContent(ContentManager content)
        {
            texture = new Texture2D[3];
            texture[0] = content.Load<Texture2D>(@"graphics\starships\tanker1");
            texture[1] = content.Load<Texture2D>(@"graphics\starships\tanker2");
            texture[2] = content.Load<Texture2D>(@"graphics\starships\tanker3");

            spriteFont = content.Load<SpriteFont>(@"spritefonts\starship_fonts\enemy_health_font");

            textureSize = new Vector2(texture[spriteCounter].Width, texture[spriteCounter].Height);
            Origin = new Vector2(texture[spriteCounter].Width / 2, texture[spriteCounter].Height / 2);

            laserTexture = content.Load<Texture2D>(@"graphics\game_objects\theHighlanderLaser");
        }

        #endregion Initialization

        #region Logic

        public void ShootOnPlayer()
        {

        }

        public void MoveHorizontally()
        {
            // If the end point has a greater x value
            if (start.X < end.X)
            {
                // If end point is reached
                if (Position.X >= end.X)
                {
                    // Set Rotation to correct Rotation and then continue movement
                    if (Rotation <= (Math.PI * 0.5) && (Rotation + MathHelper.ToRadians(rotationVelocity)) < (Math.PI * 0.5))
                        Rotation += MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = true;
                }
                    
                // If start point is reached
                if (Position.X <= start.X)
                {
                    // Set Rotation to correct Rotation and then continue movement
                    if (Rotation >= -(Math.PI * 0.5) && (Rotation - MathHelper.ToRadians(rotationVelocity)) > -(Math.PI * 0.5))
                        Rotation -= MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = false;
                }

                // On way to end point
                if (Position.X < end.X && turnDirectionToStartPoint == false)
                {
                    // Set Rotation to correct Rotation if player was near
                    Rotation = (float)-(Math.PI / 2);
                    Position.X += linearVelocity;
                }

                // On way to start point
                if (Position.X > start.X && turnDirectionToStartPoint == true)
                {
                    // Set Rotation to correct Rotation if player was near
                    Rotation = (float)(Math.PI / 2);
                    Position.X -= linearVelocity;
                }
            }

            // If the start point has a greater x value
            else if (start.X > end.X)
            {
                // If end point is reached
                if (Position.X <= end.X)
                {
                    // Set Rotation to correct Rotation and then continue movement
                    if (Rotation >= -(Math.PI * 0.5) && (Rotation - MathHelper.ToRadians(rotationVelocity)) > -(Math.PI * 0.5))
                        Rotation -= MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = true;
                }

                // If start point is reached
                if (Position.X >= start.X)
                {
                    // Set Rotation to correct Rotation and then continue movement
                    if (Rotation <= (Math.PI * 0.5) && (Rotation + MathHelper.ToRadians(rotationVelocity)) < (Math.PI * 0.5))
                        Rotation += MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = false;
                }

                // On way to end point
                if (Position.X > end.X && turnDirectionToStartPoint == false)
                {
                    // Set Rotation to correct Rotation if player was near
                    Rotation = (float)(Math.PI / 2);
                    Position.X -= linearVelocity;
                }

                // On way to start point
                if (Position.X < start.X && turnDirectionToStartPoint == true)
                {
                    // Set Rotation to correct Rotation if player was near
                    Rotation = (float)-(Math.PI / 2);
                    Position.X += linearVelocity;
                }
            }
        }

        public void MoveVertically()
        {
            // If the end point has a greater y value
            if(start.Y < end.Y)
            {
                // If end point is reached
                if (Position.Y == end.Y)
                {
                    // Set Rotation to correct Rotation and then continue movement
                    if (Rotation <= Math.PI && (Rotation + MathHelper.ToRadians(rotationVelocity)) < Math.PI)
                        Rotation += MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = true;
                }

                // If start point is reached
                if (Position.Y == start.Y)
                {
                    // Set Rotation to correct Rotation and then continue movement
                    if (Rotation >= 0 && (Rotation - MathHelper.ToRadians(rotationVelocity)) > 0)
                        Rotation -= MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = false;
                }

                // On way to end point
                if (Position.Y < end.Y && turnDirectionToStartPoint == false)
                {
                    // Set Rotation to correct Rotation if player was near
                    Rotation = 0;
                    Position.Y += linearVelocity;
                }

                // On way to start point
                if (Position.Y > start.Y && turnDirectionToStartPoint == true)
                {
                    // Set Rotation to correct Rotation if player was near
                    Rotation = (float)Math.PI;
                    Position.Y -= linearVelocity;
                }
            }

            // If the start point has a greater y value
            if (start.Y > end.Y)
            {
                // If end point is reached
                if (Position.Y == end.Y)
                {
                    // Set Rotation to correct Rotation and then continue movement
                    if (Rotation >= 0 && (Rotation - MathHelper.ToRadians(rotationVelocity)) > 0)
                        Rotation -= MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = true;
                }

                // If start point is reached
                if (Position.Y == start.Y)
                {
                    // Set Rotation to correct Rotation and then continue movement
                    if (Rotation <= Math.PI && (Rotation - MathHelper.ToRadians(rotationVelocity)) < Math.PI)
                        Rotation += MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = false;
                }

                // On way to end point
                if (Position.Y > end.Y && turnDirectionToStartPoint == false)
                {
                    // Set Rotation to correct Rotation if player was near
                    Rotation = (float)Math.PI;
                    Position.Y -= linearVelocity;
                }

                // On way to start point
                if (Position.Y < start.Y && turnDirectionToStartPoint == true)
                {
                    // Set Rotation to correct Rotation if player was near
                    Rotation = 0;
                    Position.Y += linearVelocity;
                }
            }

        }

        public void TurnShipToPlayer()
        {
            Vector2 distanceVector;

            distanceVector.X = playerPosition.X - Position.X;
            distanceVector.Y = playerPosition.Y - Position.Y;
            Rotation = (float)(Math.Atan2(distanceVector.Y, distanceVector.X) - (Math.PI / 2));
            rotationLaser = (float)(Rotation + Math.PI);
            Direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - rotationLaser), -(float)Math.Sin(MathHelper.ToRadians(90) - rotationLaser));
        }

        public void Move()
        {
            // Move if the distance to the player is high enough
            if (CheckIfDistanceToPlayerIsValid())
            {
                switch (movementMode)
                {
                    case MovementMode.HORIZONTAL:
                        MoveHorizontally();
                        break;

                    case MovementMode.VERTICAL:
                        MoveVertically();
                        break;

                    case MovementMode.PATROL:
                        TurnShipToPlayer();
                        break;
                }
            }

            // Stop moving and turn the ship towards the player
            else
            {
                TurnShipToPlayer();
                Shoot();
                //UpdateLaser();
            }
        }

        public void changeMovementMode(MovementMode movementMode)
        {
            // set movement Mode
            MovementMode = movementMode;

            // set bool for correct movement
            turnDirectionToStartPoint = false;

            // set start Rotation for actual movement mode
            switch(movementMode)
            {
                case MovementMode.HORIZONTAL:
                    if (start.X > end.X)
                        Rotation = (float)(Math.PI / 2);
                    else if (start.X < end.X)
                        Rotation = (float)-(Math.PI / 2);
                    break;
                case MovementMode.VERTICAL:
                    if (start.Y > end.Y)
                        Rotation = 0;
                    else if (start.Y < end.Y)
                        Rotation = (float)Math.PI;
                    break;
                default:
                    Rotation = 0;
                    break;
            }
        }

        public void UpdateActualShieldValue()
        {
            // Update the shield string
            shieldString = actualShield + " | " + maxShield;
        }

        public Vector2 CalculateShieldPosition()
        {
            Vector2 shieldStringSize = spriteFont.MeasureString(shieldString);

            return new Vector2(Position.X - (shieldStringSize.X / 2), Position.Y - 70);
        }

        public double CalculateDistanceToPlayer()
        {
            Vector2 distanceVector;

            distanceVector.X = playerPosition.X - Position.X;
            distanceVector.Y = playerPosition.Y - Position.Y;

            return distanceVector.Length();
        }

        public bool CheckIfDistanceToPlayerIsValid()
        {
            // Add texture width to keep distance from not only the center
            // of the texture
            if (CalculateDistanceToPlayer() > (keepDistanceToPlayer + (texture[spriteCounter].Width * 5)))
                return true;
            else
                return false;
        }

        #endregion Logic

        #region Shoot
        public void Shoot()
        {
            if (laserDelay >= 0)
                laserDelay--;

            if (laserDelay <= 0)
            {
                Laser newLaser = new Laser(laserTexture);

                newLaser.position = new Vector2(Position.X - (int)Rotation - (newLaser.texture.Width / 2),
                                                Position.Y + 27 - (texture[0].Height / 2));
                newLaser.Origin = new Vector2(laserTexture.Width / 2, laserTexture.Height / 2);
                newLaser.rotation = rotationLaser;
                newLaser.direction = Direction;
                newLaser.isVisible = true;

                if (laserList.Count() < 1000000000)
                    laserList.Add(newLaser);
            }

            if (laserDelay == 0)
                laserDelay = 50;
        }

        public void UpdateLaser()
        {
            foreach (Laser l in laserList.ToList())
            {
                if (l.steps++ < 80)
                    l.position += l.direction * (l.speed);
                else
                    l.isVisible = false;

                for (int i = 0; i < laserList.Count; i++)
                {
                    if (!laserList[i].isVisible)
                    {
                        laserList.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        #endregion Shoot

        #region Update and Draw

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            enemyBox = new Rectangle((int)Position.X, (int)Position.Y, texture[spriteCounter].Width, texture[spriteCounter].Height);

            // Update player position for movement and action
            this.playerPosition = playerPosition;

            //Update the shield value after hit
            UpdateActualShieldValue();

            // Update laser position
            UpdateLaser();

            // Update position and movement
            Move();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // If not defeated
            if(actualShield > 0)
            {
                // Count up to change sprite
                if (spriteCounter < 2)
                    spriteCounter++;
                else
                    spriteCounter = 0;

                // Shoot
                foreach (Laser l in laserList)
                {
                    l.Draw(spriteBatch);
                }

                spriteBatch.DrawString(spriteFont, shieldString, CalculateShieldPosition(), Color.Green);
                spriteBatch.Draw(texture[spriteCounter], Position, null, Color.White, Rotation, Origin, 1, SpriteEffects.None, 0);
            }
        }

        #endregion Update and Draw
    }
}
