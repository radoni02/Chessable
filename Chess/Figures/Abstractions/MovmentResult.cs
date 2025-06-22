using Chess.Chessboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures.Abstractions
{
    public class MovmentResult
    {
        public List<Field> AtackedFields { get; set; } = new List<Field>();
        public List<Field> PossibleMoves { get; set; } = new List<Field>();
    }
}
