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

        public void ShootAt(Position position)
        {
            this.score += position.Bonus;
        }

        public void IsShootedAt(Position position)
        {
            this.score -= position.Malus;
        }

        public bool Equals(Player player)
        {
            return this.Name == player.Name;
        }

        public override bool Equals(Object obj)
        {
            if (!(obj is Player player))
                return false;

            return this.Equals(player);
        }

        //Pour éviter un warning
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
