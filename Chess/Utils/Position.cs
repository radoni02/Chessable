using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [Range(1,8)]

    public int Col { get; set; }
    [Range(1, 8)]
    public int Row { get; set; }

    public override bool Equals(object? obj)
    {
        var pos = obj as Position;
        if (this.Row.Equals(pos.Row) && this.Col.Equals(pos.Col))
            return true;
        return false;
    }

    public override string ToString()
    {
        return $"{Row}{Col}";
    }
}
