using System;
using System.Collections.Generic;
using System.Text;
using FileStream;

namespace Domain
{
    public static class GameInitializer
    {
        private static string relativeFolderPath = "../../Games/";

        public static Scoreboards Initialize(string fileName)
        {
            Scoreboards gameScoreboards = new Scoreboards();

            List<Array> interactions = FileReader.ReadInteractions(relativeFolderPath + fileName + ".txt");
            for (int lineNumber = 0; lineNumber < interactions.Count; lineNumber++)
            {
                CreateInteraction(gameScoreboards, (string[])interactions[lineNumber], lineNumber);
            }

            return gameScoreboards;
        }

        //args contient tout les arguments nécessaires à la création d'une interaction
        //Ces arguments sont stockés sous la forme d'une string
        private static void CreateInteraction(Scoreboards gameScoreboards, string[] args, int lineNumber)
        {
            try
            {
                AddInteraction(gameScoreboards, args);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur à la ligne {0} : {1}", lineNumber + 1, e.Message);
            }
        }

        private static void AddInteraction(Scoreboards gameScoreboards, string[] args)
        {
            ArgumentsValidation(args);
            Player shooter = FindPlayerByName(gameScoreboards, args[0]);
            Player target = FindPlayerByName(gameScoreboards, args[1]);
            Position position = Positions.GetPositionByString(args[2]);

            shooter.ShootAt(position);
            target.IsShootedAt(position);

            Interaction newInteraction = new Interaction(shooter, target, position);

            gameScoreboards.GameInteractions.Add(newInteraction);
        }

        private static void ArgumentsValidation(string[] args)
        {
            if (args.Length != 3)
                throw new Exception("Nombre d'arguments incorrect");
        }

        private static Player FindPlayerByName(Scoreboards gameScoreboards, string name)
        {
            try
            {
                return gameScoreboards.FindPlayerByName(name);
            }
            catch (Exception)
            {
                //Si le joueur n'existe pas encore
                AddPlayer(gameScoreboards.GamePlayers, name);
            }

            //Appel récursif sauf que cette fois le joueur a été crée
            return FindPlayerByName(gameScoreboards, name);
        }

        private static void AddPlayer(List<Player> players, string name)
        {
            Player newPlayer = new Player(name);
            players.Add(newPlayer);
        }
    }
}
