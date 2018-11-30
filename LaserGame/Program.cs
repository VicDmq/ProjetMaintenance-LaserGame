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
            ConsoleController consoleController = new ConsoleController();

            string consoleReturn = "";

            while (consoleReturn != "exit")
            {
                string command = Console.ReadLine();

                consoleReturn = consoleController.ExecuteCommand(command);

                Console.WriteLine(consoleReturn + "\n");
            }
        }
    }
}
