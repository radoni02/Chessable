using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils.Notations
{
    internal static class FigureAbbreviation
    {
        public const char King = 'K'; 
        public const char Queen = 'Q'; 
        public const char Bishop = 'B'; 
        public const char Knight = 'N'; 
        public const char Rook = 'R';
        public const char Pawn = 'P';

        public static IDictionary<string, char> FigureAbbreviationDict = new Dictionary<string, char>()
        {
            {"King",King},
            {"Queen",Queen},
            {"Bishop",Bishop},
            {"Knight",Knight},
            {"Rook",Rook},
            {"Pawn",Pawn}
        };
    }
}
