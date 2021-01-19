#region File Description

//-----------------------------------------------------------------------------
// MainMap.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements
using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement.ObjectItem;
#endregion Using Statements

namespace GameStateManagement.MapClasses
{
    public class MainMap : MapStructure
    {
        #region Fields
        public Map[] maps;

        //Borders
        public Rectangle[] rectangles;
        public Rectangle topMainBorder;
        public Rectangle leftMainBorder;
        public Rectangle verticalMainBorder;
        public Rectangle horizontalMainBorder;
        public Rectangle bottomMainBorder;
        public Rectangle rightMainBorder;

        public TheHighlander player;

        // Mission
        public Mission prevMission;

        public static GameScreen gameScreen;
        #endregion Fields

        #region Initialization
        public MainMap(ContentManager content, TheHighlander theHighlander)
        {
            rectangles = new Rectangle[6];
            rectangles[0] = topMainBorder = new Rectangle(0, 0, 4000, 10);
            rectangles[1] = leftMainBorder = new Rectangle(0, 0, 10, 4000);
            rectangles[2] = verticalMainBorder = new Rectangle(2000, 0, 10, 4000);
            rectangles[3] = horizontalMainBorder = new Rectangle(0, 2000, 4000, 10);
            rectangles[4] = bottomMainBorder = new Rectangle(0, 4000, 4000, 10);
            rectangles[5] = leftMainBorder = new Rectangle(4000, 0, 10, 4000);

            maps = new Map[4];
            position = new Vector2(0, 0);
            player = theHighlander;
            prevMission = new Mission(1, player);
            CreateMaps(content);
        }

        public override void LoadContent(ContentManager content)
        {
            texture2D = content.Load<Texture2D>(@"mapGraphics\mainMap");

            LoadMapsTextures(content);
        }

        public void LoadMapsTextures(ContentManager content)
        {
            int m = 1;
            for (int i = 0; i < this.maps.Length; i++)
            {
                maps[i].LoadContent(content);
                maps[i].texture2D = content.Load<Texture2D>(@"mapGraphics\map" + m++);
            }
        }

        //create Maps
        public void CreateMaps(ContentManager content)
        {
            int m = 1;
            for (int i = 0; i < maps.Length; i++)
            {
                //create new lvl and assign map Number
                maps[i] = new Map(player, m++);
                maps[i].LoadContent(content);
                SetPositions(maps[i],content);
            }
        }

        public void SetPositions(Map map, ContentManager content)
        {
            switch (map.mapNumber)
            {
                case 1:
                    map.position = new Vector2(0, 0);
                    break;
                case 2:
                    map.position = new Vector2(2000, 0);
                    break;
                case 3:
                    map.position = new Vector2(0, 2000);
                    break;
                case 4:
                    map.position = new Vector2(2000, 2000);
                    break;
            }
            map.CreateLevels(content);
        }

        #endregion Initialization

        #region Update and Draw

        public override void Draw(SpriteBatch spriteBatch, SpriteFont sprite, GameTime gameTime)
        {
            spriteBatch.Draw(texture2D, position, Color.White);
        }

        public override void Update(GameTime gameTime, TheHighlander theHighlander)
        {
            foreach (Map m in maps)
            {
                m.Update(gameTime, theHighlander);

                foreach(Level l in m.levels)
                {
                    if (player.spawnPosLvl == l.spawnPosition || player.Position == l.spawnPosition)
                    {     
                        l.mission.isVisible = true;
                        if(l.isCompleted)
                        {
                            l.mission.isVisible = false;
                        }

                        if (prevMission.missionNr != l.mission.missionNr)
                        {
                            prevMission.isVisible = false;                             
                        }
                        prevMission = l.mission;
                    }  
                }
            }
        }
        #endregion Update and Draw
    }
}
