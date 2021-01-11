#region File Description

//-----------------------------------------------------------------------------
// MapStructure.cs
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
#endregion Using Statements

namespace GameStateManagement.MapClasses
{
    public abstract class MapStructure
    {
        #region Fields
        public Vector2 position { get; set; }
        public Texture2D texture2D { get; set; }
        #endregion Fields

        #region Initialization
        public MapStructure() { }

        public abstract void LoadContent(ContentManager content);

        public abstract void Draw(SpriteBatch spriteBatch, SpriteFont sprite, GameTime gameTime);
        public abstract void Update(GameTime gameTime, TheHighlander theHighlander);
        #endregion Initialization
    }
}
