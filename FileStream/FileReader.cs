using System;
using System.Collections.Generic;
using System.IO;

namespace FileStream
{
    static public class FileReader
    {
        public static List<Array> ReadInteractions(string relativeFilePath)
        {
            string[] lines = File.ReadAllLines(relativeFilePath);

            List<Array> interactionsArgs = new List<Array>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] args = SplitLineIntoStringArgs(lines[i]);

                if (args.Length == 3)
                    interactionsArgs.Add(args);

                else
                    Console.WriteLine("Erreur lors de la lecture du fichier à la ligne {0} : Nombre d'argument trop important"
                        , i + 1);
            }

            return interactionsArgs;
        }

        private static string[] SplitLineIntoStringArgs(string line)
        {
            string[] args;

            char[] separatingChars = new char[] { ' ', ':' };

            //L'option fournit en second parametre permet de gérer les erreurs minimes de frappe (Ex : un espace en trop)
            args = line.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries);

            return args;
        }
    }
}
