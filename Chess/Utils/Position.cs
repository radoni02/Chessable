using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils;

public class Position
{
    public Position(int row, int col)
    {
        Col = col;
        Row = row;
    }

    public int Col { get; set; }
    public int Row { get; set; }

    public override string ToString()
    {
        return $"{Row}{Col}";
    }
}
