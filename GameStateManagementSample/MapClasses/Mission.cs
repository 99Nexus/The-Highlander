using GameStateManagement.GameObjects;
using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace GameStateManagement.MapClasses
{
    public class Mission
    {
        public String misString;
        public Vector2 position;
        public int missionNr;
        public bool isVisible;
        private bool keyPressed;
        public TheHighlander player;
        public Rectangle rectangle;
        public Camera cameraMission;

        // missions
        private SpriteFont missionMessage;

        public Mission(int missionNr, TheHighlander player)
        {
            this.missionNr = missionNr;
            misString = chooseMis(missionNr);           
            isVisible = false;
            keyPressed = false;
            this.player = player;
            position = new Vector2(100, 150);
            rectangle = new Rectangle((int)position.X, (int)position.Y, 100, 55);
        }
        
        public void LoadContent(ContentManager Content)
        {
            /*missionen[0] = "Task 1: Destroy the control system of the section";
            missionen[1] = "Task 2: Turn off the alarm signal";
            missionen[2] = "Task 3: Destroy all enemy spaceships";
            missionen[3] = "Task 4: To go further, you need to turn of the generator";
            missionen[4] = "Task 5: Take out the final boss to get part of the starmap";*/
        }

        public String chooseMis(int missionNr)
        {
            switch(missionNr)
            {

                case 1:
                    return "Task 1: Turn off the control system";

                case 2:
                    return "Task 2: Turn off the alarm signal";

                case 3:
                    return "Task 3: Destroy all enemy spaceships";

                case 4:
                    return "Task 4: To go further, you need destroy the generator";

                case 5:
                    return "Task 5: Take out the final boss to get part of the starmap";
            }
            return "";
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            if (isVisible)
                spriteBatch.DrawString(spriteFont, misString, position, Color.Green);
        }
    }
}
