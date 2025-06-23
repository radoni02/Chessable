using Chess.Chessboard;
using Chess.Figures.Abstractions;
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
            var selectedFields = new List<Field>();

            selectedFields = StraightFigureMovment.GetRowFields(checkerboard, currentField, true).AtackedFields;
            selectedFields.AddRange(StraightFigureMovment.GetRowFields(checkerboard, currentField, false).AtackedFields);
            selectedFields.AddRange(StraightFigureMovment.GetColFields(checkerboard, currentField, true).AtackedFields);
            selectedFields.AddRange(StraightFigureMovment.GetColFields(checkerboard, currentField, false).AtackedFields);

            selectedFields.AddRange(DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, 1, 1).AtackedFields);
            selectedFields.AddRange(DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, 1, -1).AtackedFields);
            selectedFields.AddRange(DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, -1, 1).AtackedFields);
            selectedFields.AddRange(DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, -1, -1).AtackedFields);

            AttackedFields = selectedFields;
        }

        public override HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField)
        {
            var selectedFields = new List<Field>();

            selectedFields = StraightFigureMovment.GetRowFields(checkerboard, currentField, true).PossibleMoves;
            selectedFields.AddRange(StraightFigureMovment.GetRowFields(checkerboard, currentField, false).PossibleMoves);
            selectedFields.AddRange(StraightFigureMovment.GetColFields(checkerboard, currentField, true).PossibleMoves);
            selectedFields.AddRange(StraightFigureMovment.GetColFields(checkerboard, currentField, false).PossibleMoves);

            selectedFields.AddRange( DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, 1, 1).PossibleMoves);
            selectedFields.AddRange(DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, 1, -1).PossibleMoves);
            selectedFields.AddRange(DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, -1, 1).PossibleMoves);
            selectedFields.AddRange(DiagonallyFigureMovment.SelectValidFieldsOnTheDiagonals(checkerboard, currentField, -1, -1).PossibleMoves);

            return selectedFields
                        .Select(field => $"{field.Row - 1}{field.Col - 1}")
                        .ToHashSet();
        }
    }
}
