using Chess.Chessboard;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures;

public class King : Figure
{
    public King(bool isWhite, int value, string name) : base(isWhite, value, name)
    {
    }

    public override void CalculateAtackedFields(Checkerboard checkerboard, Field currentField)
    {
        throw new NotImplementedException();
    }

    public override void Move(Checkerboard checkerboard, Field currentField, Position targetField)
    {
        throw new NotImplementedException();
    }

    public override HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField)
    {
        throw new NotImplementedException();
    }
}
