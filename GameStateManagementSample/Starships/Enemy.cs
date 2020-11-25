using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStateManagement.Starships
{

    public enum MovementMode
    {
        HORIZONTAL,
        VERTICAL,
        DIAGONAL,
        CIRCLE,
        PURSIUT
    }

    class Enemy
    {
        // Graphical attributes
        private Texture2D texture;
        private SpriteFont spriteFont;
        private string shieldString;
        private Vector2 textureSize;

        // State attributes
        private static int actualShield;
        private int maxShield;
        private int weaponPower;
        private float linearVelocity;

        // Movement attributes
        private Vector2 position;
        private float rotation;
        private double keepDistanceToPlayer;
        public float rotationVelocity = 3f;
        public bool turnDirectionToStartPoint = false;
        public Vector2 start;
        public Vector2 end;
        public Vector2 playerPosition;
        public MovementMode movementMode;

        // Movement attributes for diagonal movement
        Vector2 direction;
        Vector2 distance;
        float correctRotation;

        // Properties
        public Texture2D Texture;
        public SpriteFont SpriteFont;
        public int AsctualShield;
        public int WeaponPower;
        public double LinearVelocity;
        public float Rotation;
        public Vector2 Position;
        public Vector2 Origin;
        public MovementMode MovementMode;

        public Enemy(Texture2D texture, SpriteFont spriteFont, Vector2 position, int maxShield, int weaponPower, float linearVelocity, Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode) 
        {
            this.texture = texture;
            this.spriteFont = spriteFont;
            this.Position = position;
            this.maxShield = maxShield;
            this.weaponPower = weaponPower;
            this.linearVelocity = linearVelocity;
            actualShield = maxShield;
            this.shieldString = actualShield + " | " + maxShield;
            this.textureSize = new Vector2(texture.Width, texture.Height);
            this.start = position;
            this.end = end;
            this.playerPosition = playerPosition;
            this.keepDistanceToPlayer = keepDistanceToPlayer;
            this.movementMode = movementMode;

            // Set values for the actual movement mode
            changeMovementMode(movementMode);
        }

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
                    // Set rotation to correct rotation and then continue movement
                    if (rotation <= (Math.PI * 0.5) && (rotation + MathHelper.ToRadians(rotationVelocity)) < (Math.PI * 0.5))
                        rotation += MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = true;
                }
                    
                // If start point is reached
                if (Position.X <= start.X)
                {
                    // Set rotation to correct rotation and then continue movement
                    if (rotation >= -(Math.PI * 0.5) && (rotation - MathHelper.ToRadians(rotationVelocity)) > -(Math.PI * 0.5))
                        rotation -= MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = false;
                }

                // On way to end point
                if (Position.X < end.X && turnDirectionToStartPoint == false)
                {
                    // Set rotation to correct rotation if player was near
                    rotation = (float)-(Math.PI / 2);
                    Position.X += linearVelocity;
                }

                // On way to start point
                if (Position.X > start.X && turnDirectionToStartPoint == true)
                {
                    // Set rotation to correct rotation if player was near
                    rotation = (float)(Math.PI / 2);
                    Position.X -= linearVelocity;
                }
            }

            // If the start point has a greater x value
            else if (start.X > end.X)
            {
                // If end point is reached
                if (Position.X <= end.X)
                {
                    // Set rotation to correct rotation and then continue movement
                    if (rotation >= -(Math.PI * 0.5) && (rotation - MathHelper.ToRadians(rotationVelocity)) > -(Math.PI * 0.5))
                        rotation -= MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = true;
                }

                // If start point is reached
                if (Position.X >= start.X)
                {
                    // Set rotation to correct rotation and then continue movement
                    if (rotation <= (Math.PI * 0.5) && (rotation + MathHelper.ToRadians(rotationVelocity)) < (Math.PI * 0.5))
                        rotation += MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = false;
                }

                // On way to end point
                if (Position.X > end.X && turnDirectionToStartPoint == false)
                {
                    // Set rotation to correct rotation if player was near
                    rotation = (float)(Math.PI / 2);
                    Position.X -= linearVelocity;
                }

                // On way to start point
                if (Position.X < start.X && turnDirectionToStartPoint == true)
                {
                    // Set rotation to correct rotation if player was near
                    rotation = (float)-(Math.PI / 2);
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
                    // Set rotation to correct rotation and then continue movement
                    if (rotation <= Math.PI && (rotation + MathHelper.ToRadians(rotationVelocity)) < Math.PI)
                        rotation += MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = true;
                }

                // If start point is reached
                if (Position.Y == start.Y)
                {
                    // Set rotation to correct rotation and then continue movement
                    if (rotation >= 0 && (rotation - MathHelper.ToRadians(rotationVelocity)) > 0)
                        rotation -= MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = false;
                }

                // On way to end point
                if (Position.Y < end.Y && turnDirectionToStartPoint == false)
                {
                    // Set rotation to correct rotation if player was near
                    rotation = 0;
                    Position.Y += linearVelocity;
                }

                // On way to start point
                if (Position.Y > start.Y && turnDirectionToStartPoint == true)
                {
                    // Set rotation to correct rotation if player was near
                    rotation = (float)Math.PI;
                    Position.Y -= linearVelocity;
                }
            }

            // If the start point has a greater y value
            if (start.Y > end.Y)
            {
                // If end point is reached
                if (Position.Y == end.Y)
                {
                    // Set rotation to correct rotation and then continue movement
                    if (rotation >= 0 && (rotation - MathHelper.ToRadians(rotationVelocity)) > 0)
                        rotation -= MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = true;
                }

                // If start point is reached
                if (Position.Y == start.Y)
                {
                    // Set rotation to correct rotation and then continue movement
                    if (rotation <= Math.PI && (rotation - MathHelper.ToRadians(rotationVelocity)) < Math.PI)
                        rotation += MathHelper.ToRadians(rotationVelocity);
                    else
                        turnDirectionToStartPoint = false;
                }

                // On way to end point
                if (Position.Y > end.Y && turnDirectionToStartPoint == false)
                {
                    // Set rotation to correct rotation if player was near
                    rotation = (float)Math.PI;
                    Position.Y -= linearVelocity;
                }

                // On way to start point
                if (Position.Y < start.Y && turnDirectionToStartPoint == true)
                {
                    // Set rotation to correct rotation if player was near
                    rotation = 0;
                    Position.Y += linearVelocity;
                }
            }

        }

        public void MoveDiagonally()
        {
            // Start is left above end
            if(start.X < end.X && start.Y < end.Y)
            {
                // If end point is reached
                if (Position.X == end.X && Position.Y == end.Y)
                {

                }

                // If start point is reached
                if (Position.X == start.X && Position.Y == start.Y)
                {

                }

                // On way to end point
                if (Position.X < end.X && Position.Y < end.Y  & turnDirectionToStartPoint == false)
                {

                }

                // On way to start point
                if (Position.X > start.X && Position.Y > start.Y && turnDirectionToStartPoint == true)
                {

                }
            }

            // Start is right above end
            if (start.X > end.X && start.Y < end.Y)
            {

            }

            // Start is left under end
            if (start.X < end.X && start.Y > end.Y)
            {

            }

            // Start is right under end
            if (start.X > end.X && start.Y > end.Y)
            {

            }
        }

        public void TurnShipToPlayer()
        {
            Vector2 distanceVector;

            distanceVector.X = playerPosition.X - Position.X;
            distanceVector.Y = playerPosition.Y - Position.Y;
            rotation = (float)(Math.Atan2(distanceVector.Y, distanceVector.X) - (Math.PI / 2));
        }

        public void Move()
        {
            // Move if the distance to the player is high enough
            if (CheckIfDistanceToPlayerIsValid())
            {
                if (movementMode == MovementMode.HORIZONTAL)
                    MoveHorizontally();

                else if (movementMode == MovementMode.VERTICAL)
                    MoveVertically();

                else if (movementMode == MovementMode.DIAGONAL)
                    MoveDiagonally();
            }

            // Stop moving and turn the ship towards the player
            else
                TurnShipToPlayer();
        }

        public void changeMovementMode(MovementMode movementMode)
        {
            // set movement Mode
            MovementMode = movementMode;

            // set bool for correct movement
            turnDirectionToStartPoint = false;

            // set start rotation for right movement mode
            switch(movementMode)
            {
                case MovementMode.HORIZONTAL:
                    if (start.X > end.X)
                        rotation = (float)(Math.PI / 2);
                    else if (start.X < end.X)
                        rotation = (float)-(Math.PI / 2);
                    break;
                case MovementMode.VERTICAL:
                    if (start.Y > end.Y)
                        rotation = 0;
                    else if (start.Y < end.Y)
                        rotation = (float)Math.PI;
                    break;
                case MovementMode.DIAGONAL:
                    if(start.X < end.X && start.Y < end.Y)
                    {
                        distance = end - start;
                        rotation = (float)(Math.Atan2(distance.Y, distance.X) - (Math.PI/2));
                    }
                    break;
                default:
                    rotation = 0;
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
            if (CalculateDistanceToPlayer() > (keepDistanceToPlayer + (texture.Width * 1.3)))
                return true;
            else
                return false;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // Update player position for movement and action
            this.playerPosition = playerPosition;

            // Update distance to the Player

            //Update the shield value after hit
            UpdateActualShieldValue();

            // Update position and movement
            Move();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // If not defeated
            if(actualShield > 0)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(spriteFont, shieldString, CalculateShieldPosition(), Color.Green);
                spriteBatch.Draw(texture, Position, null, Color.White, rotation, Origin, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(spriteFont, Position.X.ToString() + " | " + Position.Y.ToString() + " - " + start.X + " - " + end.X, new Vector2(200, 300), Color.White);
                spriteBatch.DrawString(spriteFont, rotation.ToString(), new Vector2(200, 340), Color.White);
                spriteBatch.DrawString(spriteFont, correctRotation.ToString(), new Vector2(200, 370), Color.White);
                spriteBatch.DrawString(spriteFont, direction.X + " | " + direction.Y , new Vector2(200, 400), Color.White);
                spriteBatch.DrawString(spriteFont, "start", start, Color.White);
                spriteBatch.DrawString(spriteFont, "end", end, Color.White);
                spriteBatch.End();
            }
        }
    }
}
