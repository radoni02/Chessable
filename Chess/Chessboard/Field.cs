using Chess.Figures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Chessboard
{
    public class Field
    {
        public Field(bool isUsed, IField figure, int row, int col)
        {
            IsUsed = isUsed;
            Figure = figure;
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

        public IField Figure { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
    }
}
