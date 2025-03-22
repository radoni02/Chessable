using Chess.Chessboard;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures
{
    public class Empty : IField
    {
        public Empty(int value, string name)
        {
            Value = value;
            Name = name;
        }


        public int Value { get; set; }
        public string Name { get; set; }
        public bool IsWhite { get; }

        List<Field> IField.AttackedFields => new List<Field>();

        public void CalculateAtackedFields(Checkerboard checkerboard, Field currentField)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfFieldIsOutOfTheBoard(Checkerboard checkerboard, int targetRow, int targetCol)
        {
            throw new NotImplementedException();
        }

        public void Move(Checkerboard checkerboard, Field currentField, Position targetField)
        {
            throw new NotImplementedException();
        }

        public HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField)
        {
            return new HashSet<string>();
        }
    }
}
