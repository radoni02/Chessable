using Chess.Chessboard;
using Chess.Figures.Abstractions;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures
{
    public class Queen : Figure
    {
        private StraightFigureMovment StraightFigureMovment { get; } = new StraightFigureMovment();
        private DiagonallyFigureMovment DiagonallyFigureMovment { get; } = new DiagonallyFigureMovment();
        public Queen(bool isWhite, int value, string name) : base(isWhite, value, name)
        {
        }

        public override void CalculateAtackedFields(Checkerboard checkerboard, Field currentField)
        {
            AttackedFields = StraightFigureMovment.GetFieldsFromStraightFigureMovment(checkerboard, currentField).AtackedFields;

            AttackedFields.AddRange(DiagonallyFigureMovment.GetFieldsFromDiagonalFigureMovment(checkerboard, currentField).AtackedFields);
        }

        public override void CalculatePossibleMoves(Checkerboard checkerboard, Field currentField)
        {
            var selectedFields = StraightFigureMovment.GetFieldsFromStraightFigureMovment(checkerboard, currentField).PossibleMoves;
            selectedFields.AddRange(DiagonallyFigureMovment.GetFieldsFromDiagonalFigureMovment(checkerboard, currentField).PossibleMoves);

            PossibleMoves = selectedFields
                .Select(target => new PossibleMove(new Position(currentField.Row, currentField.Col), new Position(target.Row, target.Col)))
                .ToList();
        }
    }
}
