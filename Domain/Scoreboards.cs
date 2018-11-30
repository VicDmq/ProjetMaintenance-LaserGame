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


        public int PlayerShootedByAtPosition(string shooter, string target, string position)
        {
            int numberTouchedAtPosition = 0;
            
            foreach(Interaction interaction in GameInteractions)
            {
                if (interaction.Shooter.Name == shooter && interaction.Target.Name == target && interaction.Position.Name == position)
                    numberTouchedAtPosition++;
            }      
            return numberTouchedAtPosition;
        }


        public int[] PlayerShootedBy(string shooter, string target)
        {
            int[] shotPositions = new int[Positions.GetAllPosition().Length];

            for(int i = 0; i < Positions.GetAllPosition().Length; i++)
                {
                    shotPositions[i] = PlayerShootedByAtPosition(shooter, target, Positions.GetAllPosition()[i]);
                }
            return shotPositions;
        }

        public string[] GetTargetPossibilities(string shooter)
        {
            string[] targets = new string[GamePlayers.Count - 1];
            int compteur = 0;

            foreach(Player player in GamePlayers  ) 
            {
                if(player.Name != shooter)
                {
                    targets[compteur] = player.Name;
                    compteur++;
                }       
            }
            return targets;
        }


        public string[] ActionPlayer(string shooter)
        {
            string[] targets = GetTargetPossibilities(shooter);
            string[] actionList = new string[targets.Length];
            int compteur = 0;

            foreach(string target in targets)
            {
                int[] positionsByPlayer = PlayerShootedBy(shooter, target);
                int scoreByPlayer = CalculScoreTarget(positionsByPlayer);
                string actionByPlayer = target;

                   for(int i=0; i < positionsByPlayer.Length; i++)
                   {
                   actionByPlayer = actionByPlayer + "," + positionsByPlayer[i].ToString();
                   }

                actionByPlayer = actionByPlayer + "," + scoreByPlayer.ToString();
                actionList[compteur] = actionByPlayer;
                compteur++;
            }
            return actionList;
        }


        public int CalculScoreTarget(int[] positions)
        {
            int totalScoreByTarget = 0;
            foreach(int position in positions)
            {
                totalScoreByTarget = totalScoreByTarget + position;
            }
            return totalScoreByTarget;
        }

    }
}
