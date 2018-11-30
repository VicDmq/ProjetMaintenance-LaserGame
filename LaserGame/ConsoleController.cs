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

        public ConsoleController()
        {
            Console.WriteLine("Commencez par rentrer un nom de fichier de partie ou tapez une des commandes suivantes\n");
            Console.WriteLine(this.Help() + "\n");
        }

        private void SetGameScoreboards(IScoreboards scoreboardsImplementation)
        {
            this.gameScoreboards = scoreboardsImplementation;
        }

        private IScoreboards GetGameScoreboards()
        {
            if (IScoreboardsIsInstanciated())
                return this.gameScoreboards;
            else
                throw new Exception("Il faut d'abord lire un fichier avant d'avoir accès à ces commandes");
        }

        private bool IScoreboardsIsInstanciated()
        {
            return this.gameScoreboards != null;
        }

        public string ExecuteCommand(string command)
        {
            string[] args = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
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
                    default:
                        throw new Exception("Le nombre d'arguments (" + args.Length + ") n'est pas correct");
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private string ParseCommandWith1Parameter(string[] args)
        {
            if (args[0] == "exit")
                return args[0];

            if (args[0] == "help")
                return Help();

            if (args[0] == "global")
                return GetGameScoreboards().GetGlobalScore();

            throw new Exception("Le terme \"" + args[0] + "\" n'est pas reconnu");
        }

        private string ParseCommandWith2Parameters(string[] args)
        {
            if (args[0] == "new")
                return ReadNewGameFile(args[1]);

            if (args[0] == "score")
                return GetGameScoreboards().GetScorePlayer(args[1]);

            if (args[0] == "shootBy")
                return GetGameScoreboards().GetShootByPlayers(args[1]);

            if (args[0] == "shootAt")
                return GetGameScoreboards().GetShootAtPlayers(args[1]);

            if (args[0] == "allStat")
                return GetGameScoreboards().GetAllPlayerStatistics(args[1]);

            throw new Exception("Le terme \"" + args[0] + "\" n'est pas reconnu");
        }

        private string ParseCommandWith3Parameters(string[] args)
        {
            if (args[1] == "shootBy")
                return GetGameScoreboards().GetShootByPlayer(args[0], args[2]);

            if (args[1] == "shootAt")
                return GetGameScoreboards().GetShootAtPlayer(args[0], args[2]);

            throw new Exception("Le terme \"" + args[1] + "\" n'est pas reconnu");
        }

        private string ParseCommandWith5Parameters(string[] args)
        {
            if (args[1] == "shootBy" && args[3] == "At")
                return GetGameScoreboards().GetShootByPlayerAtPosition(args[0], args[2], args[4]);

            if (args[1] == "shootAt" && args[3] == "At")
                return GetGameScoreboards().GetShootAtPlayerAtPosition(args[0], args[2], args[4]);

            throw new Exception(GetErrorMessageForCommandWith5Parameters(args));
        }

        private string GetErrorMessageForCommandWith5Parameters(string[] args)
        {
            string[] validArgs = new string[] { "shootBy", "shootAt", "At" };

            if (!validArgs.Contains<string>(args[1]) && !validArgs.Contains<string>(args[3]))
                return "Les termes \"" + args[1] + "\" et \"" + args[3] + "\" ne sont pas reconnus";
            else
            {
                string invalidArgument = validArgs.Contains<string>(args[1]) ? args[3] : args[1];
                return "Le terme \"" + invalidArgument + "\" n'est pas reconnu";
            }
        }

        private string ReadNewGameFile(string fileName)
        {
            try
            {
                Scoreboards newScoreboards = GameInitializer.Initialize(fileName);
                this.SetGameScoreboards(newScoreboards);
            }
            catch (System.IO.IOException)
            {
                return "Le fichier \"" + fileName + ".txt\" n'a pas pu être trouvé";
            }

            return "Votre nouvelle partie a bien été lue";
        }

        private string Help()
        {
            string help;

            help = "Voici la liste des commandes possibles :";
            help += "\nexit : Quitte l'application";
            help += "\nhelp : Affiche l'aide";
            help += "\nnew <fileName> : Récupère un fichier de données d'une partie";

            if (this.IScoreboardsIsInstanciated())
            {
                help += "\nglobal : Donne le score de tous les joueurs";
                help += "\nscore <playerName> : Donne le score de <playerName>";
                help += "\nshootBy <playerName> : Donne la liste des joueurs qui ont tiré sur <playerName> à chacune des positions";
                help += "\nshootAt <playerName> : Donne la liste des joueurs sur qui <playerName> a tiré à chacune des positions";
                help += "\nallStat <playerName> : Donne toutes les informations de <playerName>";
                help += "\n<playerName1> shootBy <playerName2> : Donne les positions ou <playerName1> s'est fait tiré dessus par <playerName2>";
                help += "\n<playerName1> shootAt <playerName2> : Donne les positions ou <playerName1> a tiré sur <playerName2>";
                help += "\n<playerName1> shootBy <playerName2> At <position> : Donne le nombre de fois ou <playerName1> s'est fait tiré dessus par <playerName2> à <position>";
                help += "\n<playerName1> shootAt <playerName2> At <position> : Donne le nombre de fois ou <playerName1> a tiré sur <playerName2> à <position>";
            }

            return help;
        }
    }
}