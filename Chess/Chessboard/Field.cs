using Chess.Figures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Chessboard
{
    public class Field        //possible moves zrobic sprawdzanie czy figura nie jest immobilized, dodac sprawdzanie pata
        //w gameStateAnalizer podmienic string na PossibleMoves i sprawidzc Union w Pawn move bo chyba nie działa
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
}
