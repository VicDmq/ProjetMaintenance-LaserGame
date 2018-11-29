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

        public bool ShooterNameIs(string name)
        {
            return shooter.Name == name;
        }
    }
}
