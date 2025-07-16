using Chess.Chessboard;
using Chess.Utils;
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

        protected override void CalculatePossibleMoves(Checkerboard checkerboard, Field currentField)
        {
            var possibleTargets = ValidKnightFields(checkerboard, currentField);
            PossibleMoves = possibleTargets
                .Select(target => new PossibleMove(new Position(currentField.Row, currentField.Col), new Position(target.Row, target.Col)))
                .ToList();

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
