#region File Description

//-----------------------------------------------------------------------------
// Level.cs
//amer
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;
using GameStateManagement.Screens;
using System.Collections.Generic;
using GameStateManagement.GameObjects;

#endregion Using Statements

namespace GameStateManagement.GameObjects
{
    public class Level
    {
        public Texture2D LevelBackground { get; set; }
        public Vector2 backgroundPosition;
        
        //this will make the change of the background more flexible
        public Level(Texture2D texture)
        {
            LevelBackground = texture;
            backgroundPosition = new Vector2(0, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(LevelBackground, backgroundPosition, Color.White);
        }
    }
}
