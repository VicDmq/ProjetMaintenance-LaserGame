using System;
using System.Collections.Generic;
using System.Text;

namespace StringFormatter
{
    public static class ScoreboardsFormatter
    {
        static private string nbSpacesBetweenColumns = "  ";

        static public string FormatPositionsNamesToRawString(string [] positions)
        {
            string rawFormatted = "";

            for (int i = 0; i < positions.Length; i++)
            {
                rawFormatted += positions[i] + nbSpacesBetweenColumns;
            }

            return rawFormatted;
        }

        static public string FormatIntArrayToRawString(string[] positions, int[] array)
        {
            string rawformatted = "";

            for (int i = 0; i < positions.Length; i++)
            {
                int nbLettersOfPositionName = positions[i].Length;

                rawformatted += GetCell(nbLettersOfPositionName, array[i].ToString());

                rawformatted += nbSpacesBetweenColumns;
            }

            return rawformatted;
        }

        private static string GetCell(int cellLength, string cellContent)
        {
            int nbSpacesToAddAtBeginning = cellLength / 2;
            nbSpacesToAddAtBeginning += cellLength % 2 == 0 ? -1 : 0;

            int nbSpacesToAddAtEnding = cellLength - cellLength / 2 - cellContent.Length;
            nbSpacesToAddAtEnding += cellLength % 2 == 0 ? 1 : 0;

            string cell = GetStringWithNSpaces(nbSpacesToAddAtBeginning);
            cell += cellContent;
            cell += GetStringWithNSpaces(nbSpacesToAddAtEnding);

            return cell;
        }

        public static string GetStringWithNSpaces(int nbSpaces)
        {
            string stringWithNSpaces = "";

            for (int i = 0; i < nbSpaces; i++)
                stringWithNSpaces += " ";

            return stringWithNSpaces;
        }
    }
}
