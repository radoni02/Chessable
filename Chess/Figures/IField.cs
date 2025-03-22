using Chess.Chessboard;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures
{
    public interface IField
    {
        string Name { get; }
        bool IsWhite { get; }

        List<Field> AttackedFields { get; }
        HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField);
        void Move(Checkerboard checkerboard, Field currentField, Position targetField);
        void CalculateAtackedFields(Checkerboard checkerboard, Field currentField);
        bool CheckIfFieldIsOutOfTheBoard(Checkerboard checkerboard, int targetRow, int targetCol);
    }
}
