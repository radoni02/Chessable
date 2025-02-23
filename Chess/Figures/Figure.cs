using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Chessboard;
using Chess.Utils;

namespace Chess.Figures
{
    public abstract class Figure : IField
    {
        protected Figure(bool isWhite, int value, string name)
        {
            IsWhite = isWhite;
            Value = value;
            Name = name;
        }

        private Figure(int value, string name)
        {
            Value = value;
            Name = name;
        }

        public List<Field> AttackedFields { get; set; }
        public bool IsWhite { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public abstract HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField);
        public abstract void Move(Checkerboard checkerboard, Field currentField, Position targetField);

        public abstract void CalculateAtackedFields(Checkerboard checkerboard, Field currentField);
    }
}
