using System;

namespace Domain
{
    public class Player
    {
        private string name;

        private int score;

        public Player(string playerName)
        {
            this.name = playerName;
            this.score = 0;
        }

        public string Name
        {
            get { return this.name; }
        }

        public int Score
        {
            get { return this.score; }
        }

        public void ShootAt(string positionName)
        {
            Position position = Positions.GetPositionByString(positionName);
            this.score += position.Bonus;
        }

        public void IsShootedAt(string positionName)
        {
            Position position = Positions.GetPositionByString(positionName);
            this.score -= position.Malus;
        }
    }
}
