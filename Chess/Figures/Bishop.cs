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
    public class Bishop : Figure
    {
        private DiagonallyFigureMovment DiagonallyFigureMovment { get; } = new DiagonallyFigureMovment();
        public Bishop(bool isWhite, int value, string name) : base(isWhite, value, name)
        {
        }

        public override void CalculateAtackedFields(Checkerboard checkerboard, Field currentField)
        {
            AttackedFields = DiagonallyFigureMovment.GetFieldsFromDiagonalFigureMovment(checkerboard, currentField).AtackedFields;
        }

        protected override void CalculatePossibleMoves(Checkerboard checkerboard, Field currentField)
        {
            var result = DiagonallyFigureMovment.GetFieldsFromDiagonalFigureMovment(checkerboard, currentField);
            PossibleMoves = result.PossibleMoves
                .Select(target => new PossibleMove(new Position(currentField.Row, currentField.Col), new Position(target.Row, target.Col)))
                .ToList();
        }
        
    }
}
