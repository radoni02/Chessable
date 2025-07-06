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
            var selectedFields = new List<Field>();

            selectedFields = DiagonallyFigureMovment.GetFieldsFromDiagonalFigureMovment(checkerboard, currentField).AtackedFields;

            AttackedFields = selectedFields;
        }

        public override HashSet<string> CalculatePossibleMoves(Checkerboard checkerboard, Field currentField)
        {
            //PossibleMoves
            //    .AddRange(DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, 1, 1).PossibleMoves.Select(x => new PossibleMove(new Position(currentField.Row, currentField.Col), new Position(x.Row, x.Col))));
            var selectedFields = new List<Field>();

            selectedFields = DiagonallyFigureMovment.GetFieldsFromDiagonalFigureMovment(checkerboard, currentField).PossibleMoves;

            return selectedFields
                            .Select(field => $"{field.Row - 1}{field.Col - 1}")
                            .ToHashSet();
        }
        
    }
}
