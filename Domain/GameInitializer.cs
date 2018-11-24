using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public static class GameInitializer
    {
        static public Scoreboards Initialize()
        {
            Scoreboards gameScoreboards = new Scoreboards();


            //DO SOME STUFF


            return gameScoreboards;
        }

        //args contient tout les arguments nécessaires à la création d'une interaction
        //Ces arguments sont stockés sous la forme d'une string
        static private void AddInteraction(List<Interaction> interactions, string[] args)
        {

        }

        static private void AddPlayer(List<Player> players, string name)
        {
            Player newPlayer = new Player(name);
            players.Add(newPlayer);
        }

        static private Player FindPlayerByName(List<Player> players, string name)
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
    }
}
