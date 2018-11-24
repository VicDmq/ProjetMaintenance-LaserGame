using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Scoreboards : IScoreboards
    {
        private readonly List<Interaction> gameInteractions;

        private readonly List<Player> gamePlayers;

        public Scoreboards()
        {
            this.gameInteractions = new List<Interaction>();
            this.gamePlayers = new List<Player>();
        }

        public List<Interaction> GameInteractions
        {
            get { return this.gameInteractions; }
        }

        public List<Player> GamePlayers
        {
            get { return this.gamePlayers; }
        }
    }
}
