using System;
using System.Collections.Generic;
using System.Text;
using FileStream;

namespace Domain
{
    public static class GameInitializer
    {
        public static string relativeFolderPath = "../../Games/";

        public static Scoreboards Initialize(string fileName)
        {
            Scoreboards gameScoreboards = new Scoreboards();

            List<Array> interactions = FileReader.ReadInteractions(relativeFolderPath + fileName + ".txt");
            for (int i = 0; i < interactions.Count; i++)
            {
                CreateInteraction(gameScoreboards, (string[])interactions[i]);
            }

            return gameScoreboards;
        }

        //args contient tout les arguments nécessaires à la création d'une interaction
        //Ces arguments sont stockés sous la forme d'une string
        private static void CreateInteraction(Scoreboards gameScoreboards, string[] args)
        {
            Player shooter = FindPlayerByName(gameScoreboards.GamePlayers, args[0]);
            Player target = FindPlayerByName(gameScoreboards.GamePlayers, args[1]);
            Position position = Positions.GetPositionByString(args[2]);

            shooter.ShootAt(position);
            target.IsShootedAt(position);

            Interaction newInteraction = new Interaction(shooter, target, position);

            gameScoreboards.GameInteractions.Add(newInteraction);
        }

        private static Player FindPlayerByName(List<Player> players, string name)
        {
            foreach (Player player in players)
            {
                if (player.Name == name)
                    return player;
            }

            //Si le joueur n'existe pas encore
            AddPlayer(players, name);

            //Appel récursif sauf que cette fois le joueur a été crée
            return FindPlayerByName(players, name);
        }

        private static void AddPlayer(List<Player> players, string name)
        {
            Player newPlayer = new Player(name);
            players.Add(newPlayer);
        }
    }
}
