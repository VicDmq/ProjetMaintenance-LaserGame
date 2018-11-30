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
        private IScoreboards gameScoreboards; //TODO remplacer par interface

        public void SetGameScoreboards(IScoreboards scoreboardsImplementation)
        {
            this.gameScoreboards = scoreboardsImplementation;
        }

        public string ExecuteCommand(string command)
        {
            string[] args = command.Split(' ');

            switch (args.Length)
            {
                case 1:
                    return ParseCommandWith1Parameter(args);
                case 2:
                    return ParseCommandWith2Parameters(args);
                case 3:
                    return ParseCommandWith3Parameters(args);
                case 5:
                    return ParseCommandWith5Parameters(args);

            }

            throw new Exception();
        }

        private string ParseCommandWith1Parameter(string[] args)
        {
            if (args[0] == "exit")
                return args[0];

            if (args[0] == "help")
                return Help();

            if (args[0] == "global")
                return gameScoreboards.GetGlobalScore();

            throw new Exception();
        }

        private string ParseCommandWith2Parameters(string[] args)
        {
            if (args[0] == "new")
                return ReadNewGameFile(args[1]);

            if (args[0] == "score")
                return gameScoreboards.GetScorePlayer(args[1]);

            if (args[0] == "shootBy")
                return gameScoreboards.GetShootByPlayers(args[1]);

            if (args[0] == "shootAt")
                return gameScoreboards.GetShootAtPlayers(args[1]);

            if (args[0] == "allStat")
                return gameScoreboards.GetAllPlayerStatistics(args[1]);

            throw new Exception();
        }

        private string ParseCommandWith3Parameters(string[] args)
        {
            if (args[1] == "shootBy")
                return gameScoreboards.GetShootByPlayer(args[0], args[2]);

            if (args[1] == "shootAt")
                return gameScoreboards.GetShootAtPlayer(args[0], args[2]);

            throw new Exception();

        }

        private string ParseCommandWith5Parameters(string[] args)
        {
            if (args[1] == "shootBy" && args[3] == "At")
                return gameScoreboards.GetShootByPlayerAtPosition(args[0], args[2], args[4]);

            if (args[1] == "shootAt" && args[3] == "At")
                return gameScoreboards.GetShootAtPlayerAtPosition(args[0], args[2], args[4]);

            throw new Exception();
        }
            private string ReadNewGameFile(string fileName)
        {

            Scoreboards newScoreboards = GameInitializer.Initialize(fileName);
            this.SetGameScoreboards(newScoreboards);

            return "Votre nouvelle partie a bien été lue";
        }

        private string Help()
        {
            string help;

            help = "Voici la liste des commandes possibles :\n";
            help += "exit : Quitte l'application\n";
            help += "help : Affiche l'aide\n";
            help += "global : Donne le score de tous les joueurs\n";
            help += "new <fileName> : Récupère un fichier de données d'une partie\n";
            help += "score <playerName> : Donne le score de <playerName>\n";
            help += "shootBy <playerName> : Donne la liste des joueurs qui ont tiré sur <playerName> à chacune des positions\n";
            help += "shootAt <playerName> : Donne la liste des joueurs sur qui <playerName> a tiré à chacune des positions\n";
            help += "allStat <playerName> : Donne toutes les informations de <playerName>\n";
            help += "<playerName1> shootBy <playerName2> : Donne les positions ou <playerName1> s'est fait tiré dessus par <playerName2>\n";
            help += "<playerName1> shootAt <playerName2> : Donne les positions ou <playerName1> a tiré sur <playerName2>\n";
            help += "<playerName1> shootBy <playerName2> At <position> : Donne le nombre de fois ou <playerName1> s'est fait tiré dessus par <playerName2> à <position>\n";
            help += "<playerName1> shootAt <playerName2> At <position> : Donne le nombre de fois ou <playerName1> a tiré sur <playerName2> à <position>\n";

            return help;
        }
    }
}