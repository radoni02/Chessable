using Chess.Chessboard;
using Chess.Figures.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures
{
    public class Rook : Figure
    {
        private StraightFigureMovment StraightFigureMovment { get; } = new StraightFigureMovment();
        public Rook(bool isWhite, int value, string name) : base(isWhite, value, name)
        {
        }

        public override void CalculateAtackedFields(Checkerboard checkerboard, Field currentField)
        {
            var selectedFields = new List<Field>();

            selectedFields = StraightFigureMovment.GetRowFields(checkerboard, currentField, true);
            selectedFields.AddRange(StraightFigureMovment.GetRowFields(checkerboard, currentField, false));
            selectedFields.AddRange(StraightFigureMovment.GetColFields(checkerboard, currentField, true));
            selectedFields.AddRange(StraightFigureMovment.GetColFields(checkerboard, currentField, false));

            AttackedFields = selectedFields;
        }

        public override HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField)
        {
            var selectedFields = new List<Field>();

            selectedFields = StraightFigureMovment.GetRowFields(checkerboard, currentField, true);
            selectedFields.AddRange(StraightFigureMovment.GetRowFields(checkerboard, currentField, false));
            selectedFields.AddRange(StraightFigureMovment.GetColFields(checkerboard, currentField, true));
            selectedFields.AddRange(StraightFigureMovment.GetColFields(checkerboard, currentField, false));

            return selectedFields
                        .Select(field => $"{field.Row - 1}{field.Col - 1}")
                        .ToHashSet();
        }
    }
}
