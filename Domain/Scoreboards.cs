using System;
using System.Collections.Generic;
using System.Text;
using StringFormatter;

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

        public Player FindPlayerByName(string playerName)
        {
            foreach (Player player in this.gamePlayers)
            {
                if (player.Name == playerName)
                    return player;
            }

            throw new Exception("Le joueur \"" + playerName + "\" n'existe pas");
        }

        private int ShootAtPlayerAtPosition(Player player, Player target, Position position)
        {
            int nbTimes = 0;

            foreach (Interaction interaction in GameInteractions)
            {
                if (interaction.Shooter.Equals(player) && interaction.Target.Equals(target) && interaction.Position.Equals(position))
                    nbTimes++;
            }

            return nbTimes;
        }

        private int[] ShootAtPlayer(Player player, Player target)
        {
            Position[] positionsNames = Positions.GetPositions();
            int[] nbTimesPerPosition = new int[positionsNames.Length];

            for (int i = 0; i < positionsNames.Length; i++)
            {
                nbTimesPerPosition[i] = ShootAtPlayerAtPosition(player, target, positionsNames[i]);
            }

            return nbTimesPerPosition;
        }

        private List<int[]> ShootAtOrByPlayers(Player player, bool playerIsShooterInsteadOfTarget)
        {
            List<Player> potentialTargets = GetOtherPlayers(player);
            int positionsLength = Positions.GetPositionsLength();

            List<int[]> nbTimesPerPlayerAndPerPositions = new List<int[]>();

            foreach (Player target in potentialTargets)
            {
                int[] nbTimesPerPosition = playerIsShooterInsteadOfTarget ? ShootAtPlayer(player, target) : ShootAtPlayer(target, player);
                nbTimesPerPlayerAndPerPositions.Add(nbTimesPerPosition);
            }

            return nbTimesPerPlayerAndPerPositions;
        }

        private List<Player> GetOtherPlayers(Player player)
        {
            List<Player> playersWithoutShooter = new List<Player>();
            playersWithoutShooter.AddRange(this.gamePlayers);
            playersWithoutShooter.Remove(player);

            return playersWithoutShooter;
        }

        private int GetScoreOfAllPositions(int[] nbTimesPerPosition, bool isBonusInsteadOfMalus)
        {
            Position[] positions = Positions.GetPositions();
            int score = 0;

            for (int i = 0; i < Positions.GetPositionsLength(); i++)
            {
                int bonusOrMalus = isBonusInsteadOfMalus ? positions[i].Bonus : -positions[i].Malus;
                score += bonusOrMalus * nbTimesPerPosition[i];
            }

            return score;
        }

        private string GetTabForShootByOrAtPlayers(string playerName, bool playerIsShooterInsteadOfTarget)
        {
            Player player = this.FindPlayerByName(playerName);
            List<int[]> nbTimesPerPlayerAndPerPosition = this.ShootAtOrByPlayers(player, playerIsShooterInsteadOfTarget);
            List<Player> otherPlayers = this.GetOtherPlayers(player);
            string[] positionsNames = Positions.GetPositionsNames();

            int finalBonusOrMalusScore = 0;
            string returnedString = ScoreboardsFormatter.FormatPositionsNamesToRawString(positionsNames);

            for (int i = 0; i < nbTimesPerPlayerAndPerPosition.Count; i++)
            {
                int[] nbTimesPerPosition = nbTimesPerPlayerAndPerPosition[i];
                returnedString += "\n" + ScoreboardsFormatter.FormatIntArrayToRawString(positionsNames, nbTimesPerPosition);

                int bonusOrMalusScore = GetScoreOfAllPositions(nbTimesPerPosition, playerIsShooterInsteadOfTarget);
                finalBonusOrMalusScore += bonusOrMalusScore;
                returnedString += bonusOrMalusScore + ScoreboardsFormatter.GetStringWithNSpaces(5 - bonusOrMalusScore.ToString().Length)+ otherPlayers[i].Name;
            }

            string bonusOrMalus = playerIsShooterInsteadOfTarget ? "Bonus" : "Malus";
            returnedString += "\n" + bonusOrMalus + " total : " + finalBonusOrMalusScore;

            return returnedString;
        }

        private string GetRawForShootByOrAtPlayer(int[] nbTimesPerPosition, int score)
        {
            string[] positionsNames = Positions.GetPositionsNames();

            string returnedText = ScoreboardsFormatter.FormatPositionsNamesToRawString(positionsNames);
            returnedText += "\n" + ScoreboardsFormatter.FormatIntArrayToRawString(positionsNames, nbTimesPerPosition);
            returnedText += score;

            return returnedText;
        }

        //#######################
        // Interface IScoreboards
        //#######################

        public string GetGlobalScore()
        {
            string globalScore = "";

            foreach (Player player in this.gamePlayers)
            {
                globalScore += player.Name + " : " + player.Score;

                bool playerIsLastOne = this.gamePlayers.IndexOf(player) == this.gamePlayers.Count - 1;
                globalScore += playerIsLastOne ? "" : "\n";
            }

            return globalScore;
        }

        public string GetScorePlayer(string playerName)
        {
            Player player = this.FindPlayerByName(playerName);

            return playerName + " : " + player.Score;
        }

        public string GetShootByPlayers(string playerName)
        {
            return this.GetTabForShootByOrAtPlayers(playerName, false);
        }

        public string GetShootAtPlayers(string playerName)
        {
            return this.GetTabForShootByOrAtPlayers(playerName, true);
        }

        public string GetAllPlayerStatistics(string playerName)
        {
            string returnedString = this.GetShootAtPlayers(playerName);
            returnedString += "\n" + this.GetShootByPlayers(playerName);
            returnedString += "\nScore total : " + FindPlayerByName(playerName).Score;

            return returnedString;
        }

        public string GetShootByPlayer(string playerName, string shooterName)
        {
            Player player = this.FindPlayerByName(playerName);
            Player shooter = this.FindPlayerByName(shooterName);

            int[] nbTimesPerPosition = this.ShootAtPlayer(shooter, player);
            int malusScore = this.GetScoreOfAllPositions(nbTimesPerPosition, false);

            return this.GetRawForShootByOrAtPlayer(nbTimesPerPosition, malusScore);
        }

        public string GetShootAtPlayer(string playerName, string targetName)
        {
            Player player = this.FindPlayerByName(playerName);
            Player target = this.FindPlayerByName(targetName);

            int[] nbTimesPerPosition = this.ShootAtPlayer(player, target);
            int bonusScore = this.GetScoreOfAllPositions(nbTimesPerPosition, true);

            return this.GetRawForShootByOrAtPlayer(nbTimesPerPosition, bonusScore);
        }

        public string GetShootByPlayerAtPosition(string playerName, string shooterName, string positionName)
        {
            Player player = this.FindPlayerByName(playerName);
            Player shooter = this.FindPlayerByName(shooterName);
            Position position = Positions.GetPositionByString(positionName);

            int nbTimes = this.ShootAtPlayerAtPosition(shooter, player, position);

            return playerName + " s'est fait tiré dessus " + nbTimes + " fois à la position \"" + position.Name + "\" par " + shooterName;
        }

        public string GetShootAtPlayerAtPosition(string playerName, string targetName, string positionName)
        {
            Player player = this.FindPlayerByName(playerName);
            Player target = this.FindPlayerByName(targetName);
            Position position = Positions.GetPositionByString(positionName);

            int nbTimes = this.ShootAtPlayerAtPosition(player, target, position);

            return playerName + " a tiré " + nbTimes + " fois sur la position \"" + position.Name + "\" de " + targetName;
        }
    }
}
