using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace LaserGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Scoreboards sc = GameInitializer.Initialize("Game1");
            ConsoleController console = new ConsoleController(sc);

            Console.Write(console.DisplayHelp());

            string actionCommande;

            do {
                actionCommande = Console.ReadLine();

                Console.WriteLine(console.parseCommand(actionCommande));

            } while (actionCommande != "exit");
        }
    }
}
