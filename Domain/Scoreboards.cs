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

        public string GetGlobalScore()
        {
            return "Executing GetGlobalScore";
        }

        public string GetScorePlayer(string playerName)
        {
            return "Executing GetPlayerScore with arg : " + playerName;
        }

        public string GetShootByPlayers(string playerName)
        {
            return "Executing GetShootByPlayer with arg : " + playerName;
        }

        public string GetShootAtPlayers(string playerName)
        {
            return "Executing GetShootAtPlayer with arg : " + playerName;
        }

        public string GetAllPlayerStatistics(string playerName)
        {
            return "Executing GetAllPlayerStatistics with arg : " + playerName;
        }

        public string GetShootByPlayer(string playerName, string shooterName)
        {
            return "Executing GetShootByPlayer with args : " + playerName + " and " + shooterName;
        }

        public string GetShootAtPlayer(string playerName, string targetName)
        {
            return "Executing GetShootAtPlayer with args : " + playerName + " and " + targetName;
        }

        public string GetShootByPlayerAtPosition(string playerName, string shooterName, string positionName)
        {
            return "Executing GetShootByPlayerAtPosition with args : " + playerName + " and " + shooterName + " and " + positionName;
        }

        public string GetShootAtPlayerAtPosition(string playerName, string targetName, string positionName)
        {
            return "Executing GetShootAtPlayerAtPosition with args : " + playerName + " and " + targetName + " and " + positionName;
        }
    }
}
