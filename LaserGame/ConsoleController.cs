using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace LaserGame
{
    public class ConsoleController
    {
        private IScoreboards gameScoreboards;

        public ConsoleController(Scoreboards gameScoreboards)
        {
            this.gameScoreboards = gameScoreboards;
        }
    }
}