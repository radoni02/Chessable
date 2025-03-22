using Chess.Chessboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures
{
    public class Rook : Figure
    {
        public Rook(bool isWhite, int value, string name) : base(isWhite, value, name)
        {
        }

        public override void CalculateAtackedFields(Checkerboard checkerboard, Field currentField)
        {
            throw new NotImplementedException();
        }

        public override HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField)
        {
            var leftRow = GetRowFields(checkerboard, currentField, true);
            //here I can foreach in each leftRow rightDown etc, added each fild that is possible to move and stop when taragetField.IsUsed == true
            throw new NotImplementedException();
        }

        private List<Field> GetRowFields(Checkerboard checkerboard, Field currentField,bool left)
        {
            var fieldsInSameRow = checkerboard.Board[currentField.Row - 1];

            return left ? fieldsInSameRow
                                .Where(field => field.Col < currentField.Col)
                                .OrderByDescending(f => f.Col)
                                .ToList() : 
                             fieldsInSameRow
                                .Where(field => field.Col > currentField.Col)
                                .OrderBy(f => f.Col)
                                .ToList();
        }

        private List<Field> GetColFields(Checkerboard checkerboard, Field currentField, bool down)
        {
            var fieldsInSameColumn = checkerboard.Board
                .SelectMany(row => row)
                .Where(field => field.Col == currentField.Col && field.Row != currentField.Row);

            return down ? fieldsInSameColumn
                                .Where(field => field.Row < currentField.Row)
                                .OrderByDescending(f => f.Row)
                                .ToList() :
                          fieldsInSameColumn
                                .Where(field => field.Row > currentField.Row)
                                .OrderBy(f => f.Row)
                                .ToList();
        }
    }
}
