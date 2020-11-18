#region File Description

//-----------------------------------------------------------------------------
// Score.cs
//amer
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements
using System;
using System.Collections.Generic;
using System.Text;
#endregion Using Statements

namespace GameStateManagement
{
    public class Score
    {

        public string Playername { get; set; }
        public int Value { get; set; }

        public Score(string playerName, int playerScore)
        {
            this.Playername = playerName;
            this.Value = playerScore;
        }
        
    }
}
