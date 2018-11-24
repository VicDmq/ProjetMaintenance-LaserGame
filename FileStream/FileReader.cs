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

            List<Array> interactions = new List<Array>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] args = SplitLineIntoStringArgs(lines[i]);
                interactions.Add(args);
            }

            return interactions;
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
