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

        public string parseCommand(string actionCommand)
        {
            switch (actionCommand)
            {
                case "global":
                   
                    break;
               // case ":
                    
               //     break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            return  DisplayHelp();
        }

        public string DisplayHelp()
        {
            string help;

            help = "Aide et commandes possibles :\n";
            help += "global : Donne le score de tous les joueurs\n";
            help += "<playerName> shootBy : Donne la liste des joueurs qui ont tiré sur <playerName> à chacune des positions\n";
            help += "<playerName> shootAt : Donne la liste des joueurs sur qui <playerName> a tiré à chacune des positions\n";
            help += "<playerName> score : Donne le score de <playerName>\n";
            help += "<playerName> all : Donne toutes les informations de <playerName>\n";

            return help;
        }
    }
}