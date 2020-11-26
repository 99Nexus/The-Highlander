using GameStateManagement.Screens;
using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStateManagement.GameObjects
{
    public class Camera
    {
        #region Fields

        public Matrix Transform;
        public GameScreen GameScreen;

        #endregion Fields

        #region Initialization

        public Camera(GameScreen GameScreen)
        {
            this.GameScreen = GameScreen;
        }

        #endregion Initialization

        #region Logic

        // Camera follows the player
        public void Follow(TheHighlander target)
        {
            Matrix position = Matrix.CreateTranslation(
                -target.Position.X - (target.Rectangle.Width / 2),
                -target.Position.Y - (target.Rectangle.Height / 2),
                0);

            Matrix offset = Matrix.CreateTranslation(
                (GameScreen.ScreenManager.GraphicsDevice.Viewport.Width / 2) + (target.texture[0].Width / 2),
                (GameScreen.ScreenManager.GraphicsDevice.Viewport.Height / 2) + (target.texture[0].Height / 2),
                0);

            Transform = position * offset;
        }

        // Camera follows the health
        public void Follow(HealthBar target)
        {
            Matrix position = Matrix.CreateTranslation(
                -target.Position.X - (target.Rectangle.Width / 2),
                -target.Position.Y - (target.Rectangle.Height / 2),
                0);

            Matrix offset = Matrix.CreateTranslation(120, 50, 0);

            Transform = position * offset;
        }

        // Camera follows the score
        public void Follow(Highscore target)
        {
            Matrix position = Matrix.CreateTranslation(
                -target.Position.X - (target.Rectangle.Width / 2),
                -target.Position.Y - (target.Rectangle.Height / 2),
                0);

            Matrix offset = Matrix.CreateTranslation((GameScreen.ScreenManager.GraphicsDevice.Viewport.Width - 120), 50, 0);

            Transform = position * offset;
        }

        #endregion Logic
    }
}
