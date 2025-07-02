using Chess.Chessboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures
{
    public class Knight : Figure
    {
        public Knight(bool isWhite, int value, string name) : base(isWhite, value, name)
        {
        }

        public override void CalculateAtackedFields(Checkerboard checkerboard, Field currentField)
        {

            AttackedFields = ValidKnightFields(checkerboard, currentField);

        }

        public override HashSet<string> CalculatePossibleMoves(Checkerboard checkerboard, Field currentField)
        {

            return ValidKnightFields(checkerboard, currentField)
                                .Select(field => $"{field.Row-1}{field.Col-1}")
                                .ToHashSet();

        }

        private List<Field> ValidKnightFields(Checkerboard checkerboard,Field currentField)
        {
            var selectedFields = checkerboard.Board.SelectMany(f => f)
                                .Where(field => (Math.Abs(field.Col - currentField.Col) == 2 && Math.Abs(field.Row - currentField.Row) == 1)
                                    || (Math.Abs(field.Col - currentField.Col) == 1 && Math.Abs(field.Row - currentField.Row) == 2))
                                .Distinct()
                                .ToList();
            var sameColorFigures = selectedFields.Where(field => field.IsUsed && field.Figure.IsWhite == currentField.Figure.IsWhite).ToList();

            return selectedFields.Except(sameColorFigures).ToList();
        }
    }
}
