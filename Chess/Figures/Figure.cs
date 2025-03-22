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
            AttackedFields = new List<Field>();
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
        public void Move(Checkerboard checkerboard, Field currentField, Position targetField)
        {
            if (!checkerboard.Board[targetField.Row][targetField.Col].IsUsed)
            {
                var newField = new Field()
                {
                    Row = targetField.Row + 1,
                    Col = targetField.Col + 1,
                    Figure = currentField.Figure,
                    IsUsed = currentField.IsUsed
                };

                var temp = checkerboard.Board[targetField.Row][targetField.Col];

                checkerboard.Board[targetField.Row][targetField.Col] = newField;

                checkerboard.Board[currentField.Row - 1][currentField.Col - 1] = temp;
                checkerboard.Board[currentField.Row - 1][currentField.Col - 1].Row = currentField.Row;
                checkerboard.Board[currentField.Row - 1][currentField.Col - 1].Col = currentField.Col;
            }

            if (checkerboard.Board[targetField.Row][targetField.Col].IsUsed)
            {
                var newField = new Field()
                {
                    Row = targetField.Row + 1,
                    Col = targetField.Col + 1,
                    Figure = currentField.Figure,
                    IsUsed = currentField.IsUsed
                };

                checkerboard.Board[targetField.Row][targetField.Col] = newField;

                checkerboard.Board[currentField.Row - 1][currentField.Col - 1] = new Field(false, new Empty(0, "Empty"), currentField.Row, currentField.Col);
            }
        }

        public abstract void CalculateAtackedFields(Checkerboard checkerboard, Field currentField);

        public bool CheckIfFieldIsOutOfTheBoard(Checkerboard checkerboard, int targetRow, int targetCol)
        {
            try
            {
                var exists = checkerboard.Board[targetRow][targetCol].IsUsed;
                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }


    }
}
