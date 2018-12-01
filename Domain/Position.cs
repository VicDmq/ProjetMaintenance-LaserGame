using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public struct Position
    {
        private readonly string name;

        private readonly int bonus;

        private readonly int malus;

        public Position(string name, int bonus, int malus)
        {
            this.name = name;
            this.bonus = bonus;
            this.malus = malus;
        }

        public string Name
        {
            get { return this.name; }
        }

        public int Bonus
        {
            get { return this.bonus; }
        }

        public int Malus
        {
            get { return this.malus; }
        }

        public bool Equals(Position position)
        {
            return this.Name == position.Name;
        }

        public override bool Equals(Object obj)
        {
            if (!(obj is Position position))
                return false;

            return this.Equals(position);
        }

        //Pour éviter un warning
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    static public class Positions
    {
        static private Position[] positions = new Position[]
        {
            new Position("Épaule", 15, 5),
            new Position("Arme", 20, 8),
            new Position("Torse", 10, 3),
            new Position("Dos", 8, 2)
        };

        static public Position[] GetPositions()
        {
            return positions;
        }

        static public int GetPositionsLength()
        {
            return positions.Length;
        }

        static public Position GetPositionByString(string positionName)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                if (positions[i].Name == positionName)
                    return positions[i];
            }

            throw new Exception("Cette position n'existe pas");
        }

        static public string[] GetPositionsNames()
        {
            string[] positionsNames = new string[positions.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                positionsNames[i] = positions[i].Name;
            }

            return positionsNames;
        }
    }
}
