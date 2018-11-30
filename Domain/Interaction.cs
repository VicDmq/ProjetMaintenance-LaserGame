using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Interaction
    {
        private readonly Player shooter;

        private readonly Player target;

        private readonly Position position;

        public Interaction(Player shooter, Player target, Position position)
        {
            this.shooter = shooter;
            this.target = target;
            this.position = position;
        }

        public Player Shooter
        {
            get { return this.shooter; }
        }
        public Player Target
        {
            get { return this.target; }

        }
        public Position Position
        {
            get { return this.position; }
        }
    }
}
