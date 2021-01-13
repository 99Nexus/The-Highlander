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
        public Texture2D[] texture;
        public SpriteFont spriteFont;
        public string shieldString;
        public Vector2 textureSize;
        public bool isVisible;
        public int spriteCounter = 0;

        // Shooting attributes
        public Vector2 laserVelocity;
        public Texture2D laserTexture;
        public float laserDelay;
        public List<Laser> laserList;
        public Laser actualLaser;
        public float rotationLaser;

        // State attributes
        public int actualShield;
        public int maxShield;
        public int weaponPower;
        public float linearVelocity;
        public int damageBuffer;
        public int maxDamageBuffer;
        public int score = 0;

        // Movement attributes
        public Vector2 position;
        public float Rotation;
        public double keepDistanceToPlayer;
        public float rotationVelocity = 3f;
        public bool turnDirectionToStartPoint;
        public Vector2 start;
        public Vector2 end;
        public Vector2 playerPosition;
        public Vector2 Direction;
        public Rectangle Rectangle;
        public MovementMode movementMode;

        // Properties
        public Texture2D Texture;
        public SpriteFont SpriteFont;
        public int WeaponPower;
        public double LinearVelocity;
        public Vector2 Position;
        public Vector2 Origin;
        public MovementMode MovementMode;

        #endregion Fields

        #region Inialization

        public Enemy(Vector2 position, Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode)
        {
            this.Position = position;
            actualShield = maxShield;
            this.shieldString = actualShield + " | " + maxShield;
            this.start = position;
            this.end = end;
            this.playerPosition = playerPosition;
            this.keepDistanceToPlayer = keepDistanceToPlayer;
            this.movementMode = movementMode;
            this.isVisible = true;
            damageBuffer = 0;
            maxDamageBuffer = 20;

            laserList = new List<Laser>();
            laserDelay = 50;

            // Set values for the actual movement mode
            changeMovementMode(movementMode);
        }


        public Enemy(Vector2 position, int maxShield, int weaponPower, float linearVelocity,
            Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode, int score)
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
            damageBuffer = 0;
            maxDamageBuffer = 20;
            this.score = score;

            laserList = new List<Laser>();
            laserDelay = 50;

            // Set values for the actual movement mode
            changeMovementMode(movementMode);
        }

        public virtual void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"spritefonts\starship_fonts\enemy_health_font");

            textureSize = new Vector2(texture[spriteCounter].Width, texture[spriteCounter].Height);
            Origin = new Vector2(texture[spriteCounter].Width / 2, texture[spriteCounter].Height / 2);

            laserTexture = content.Load<Texture2D>(@"graphics\game_objects\theHighlanderLaser");

            Rectangle = new Rectangle((int)Position.X - (texture[spriteCounter].Width / 2),
                          (int)Position.Y - (texture[spriteCounter].Height / 2),
                          texture[spriteCounter].Width,
                          texture[spriteCounter].Height);
        }

        #endregion Initialization

        #region Logic

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
            if (start.Y < end.Y)
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
            }
        }

        public void changeMovementMode(MovementMode movementMode)
        {
            // set movement Mode
            MovementMode = movementMode;

            // set bool for correct movement
            turnDirectionToStartPoint = false;

            // set start Rotation for actual movement mode
            switch (movementMode)
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

        public void UpdateActualShieldValue(int damage)
        {
            if (actualShield - damage < 1)
            {
                //public void ManageExplosions()
                actualShield = 0;
            }
            else
            {
                actualShield -= damage;

                // Update the shield string
                shieldString = actualShield + " | " + maxShield;
            }
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

                newLaser.Position = new Vector2(Position.X, Position.Y);
                newLaser.Origin = new Vector2(laserTexture.Width / 2, laserTexture.Height / 2);
                newLaser.rotation = rotationLaser;
                newLaser.direction = Direction;
                newLaser.isVisible = true;

                if (laserList.Count() < 1000000000)
                {
                    laserList.Add(newLaser);
                    newLaser.id = laserList.Count();
                }
            }

            if (laserDelay == 0)
                laserDelay = 50;
        }

        public void UpdateLaser()
        {
            foreach (Laser l in laserList.ToList())
            {
                if (l.steps++ < 80)
                    l.Position += l.direction * (l.speed);
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

        public virtual void Update(GameTime gameTime, Vector2 playerPosition)
        {
            Rectangle = new Rectangle((int)Position.X - (texture[spriteCounter].Width / 2),
                                      (int)Position.Y - (texture[spriteCounter].Height / 2),
                                      texture[spriteCounter].Width,
                                      texture[spriteCounter].Height);

            // Update player position for movement and action
            this.playerPosition = playerPosition;

            // Update laser position
            UpdateLaser();

            foreach (Laser l in laserList.ToList())
            {
                l.Update(gameTime);
                actualLaser = l;
            }

            // Update position and movement
            Move();

            damageBuffer++;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // If not defeated
            if (actualShield > 0)
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
