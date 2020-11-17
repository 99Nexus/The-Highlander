using System;
using System.Collections.Generic;
using System.Text;

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
