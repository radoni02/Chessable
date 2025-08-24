using Chess.Chessboard;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures
{
    internal interface IFigure
    {
        string Name { get; }
        bool IsWhite { get; }
        int MoveConut { get; set; }
        int Value { get; set; }
        List<PossibleMove>? PossibleMoves { get; set; }
        List<Field> AttackedFields { get; }
        void CheckPossibleMoves(Checkerboard checkerboard, Field currentField, bool passantEnable = false, PossibleMove? lastMove = null);
        void CalculateAtackedFields(Checkerboard checkerboard, Field currentField);
        void Move(Checkerboard checkerboard, Field currentField, Position targetField, bool increaseMoveCount = false);
        void IncreaseMoveCount();
        bool CheckIfFieldIsOutOfTheBoard(Checkerboard checkerboard, int targetRow, int targetCol);
        Field? GetOppositKing(Checkerboard checkerboard);
        bool CheckIfFigureIsUnderAttack(Checkerboard checkerboard);
        List<Field> GetListOfFieldsAttackingTarget(Checkerboard checkerboard);
        List<Field> GetListOfFieldsThatAreBetweenCurrentAndTarget(Checkerboard checkerboard, Field currentFIeld, Field targetField);
        bool CheckIfFigureIsImmobilized(Checkerboard checkerboard);
        void MakeTempMove(Checkerboard checkerboard, List<Field> fieldsToRecalculate, Action<Checkerboard> calculations);
    }
}
