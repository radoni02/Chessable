using Chess.Figures;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Chessboard
{
    internal class Field
    {
        public Field(bool isUsed, IFigure figure, int row, int col)
        {
            IsUsed = isUsed;
            Figure = figure;
            Row = row;
            Col = col;
        }
        public Field(bool isUsed, int row, int col)
        {
            IsUsed = isUsed;
            Figure = null;
            Row = row;
            Col = col;
        }

        public Field(int row, int col)
        {
            IsUsed = false;
            Row = row;
            Col = col;
        }

        public Field()
        {
            
        }

        public bool IsUsed { get; set; } = false;

        public IFigure? Figure { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public bool CheckIfFieldIsNotEmpty()
        {
            if (Figure is Figure)
                return true;
            return false;
        }
    }

    internal class FieldComparer : EqualityComparer<Field>
    {
        public override bool Equals(Field? x, Field? y)
        {
            if (x.Row == y.Row && x.Col == y.Col)
                return true;
            return false;
        }

        public override int GetHashCode([DisallowNull] Field obj)
        {
            return HashCode.Combine(obj.Col, obj.Row);
        }
    }
}
