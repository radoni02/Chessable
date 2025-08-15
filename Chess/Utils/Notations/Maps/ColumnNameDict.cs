using Chess.Utils.Notations.FEN.Maps;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils.Notations.Maps
{
    internal static class ColumnNameDict
    {
        private const char FirstColumn = 'a';
        private const char SecondColumn = 'b';
        private const char ThirdColumn = 'c';
        private const char ForthColumn = 'd';
        private const char FifthColumn = 'e';
        private const char SixthColumn = 'f';
        private const char SeventhColumn = 'g';
        private const char EightColumn = 'h';

        public static IDictionary<int, char> ColumnNames = new Dictionary<int, char>()
        {
            {1,FirstColumn},
            {2,SecondColumn},
            {3,ThirdColumn},
            {4,ForthColumn},
            {5,FifthColumn},
            {6,SixthColumn},
            {7,SeventhColumn},
            {8,EightColumn}
        }.ToFrozenDictionary();
    }
}
