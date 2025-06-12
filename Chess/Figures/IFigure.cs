using Chess.Chessboard;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures
{
    public interface IFigure
    {
        string Name { get; }
        bool IsWhite { get; }
        int MoveConut { get; set; }
        int Value { get; set; }

        List<Field> AttackedFields { get; }
        HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField);
        void CalculateAtackedFields(Checkerboard checkerboard, Field currentField);
        void Move(Checkerboard checkerboard, Field currentField, Position targetField);
        bool CheckIfFieldIsOutOfTheBoard(Checkerboard checkerboard, int targetRow, int targetCol);
        Field GetOppositKing(Checkerboard checkerboard);
        bool CheckIfFigureIsUnderAttack(Checkerboard checkerboard);
        List<Field> GetListOfFieldsAttackingTarget(Checkerboard checkerboard);
        List<IFigure> GetFiguresThatCanMoveToTheField(Checkerboard checkerboard, Field targetField, bool isWhite);
        List<Field> GetListOfFieldsThatAreBetweenCurrentAndTarget(Checkerboard checkerboard, Field currentFIeld, Field targetField);
    }
}
