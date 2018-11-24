using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Interaction
    {
        private Player shooter;

        private Player target;

        private Position position;

        public Interaction(Player shooter, Player target, string positionName)
        {
            this.shooter = shooter;
            this.target = target;
            this.position = Positions.GetPositionByString(positionName);
        }
    }
}
