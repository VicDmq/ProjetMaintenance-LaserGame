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

        public void incrementScore()
        {
            this.score += 5;
        }

        public int getScore()
        {
            return this.score;
        }
    }
}
