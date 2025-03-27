using Chess.Chessboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures
{
    public class Bishop : Figure
    {
        public Bishop(bool isWhite, int value, string name) : base(isWhite, value, name)
        {
        }

        public override void CalculateAtackedFields(Checkerboard checkerboard, Field currentField)
        {
            var hashSet = new HashSet<string>();
            var directions = new List<List<Field>>();
            directions.Add(SelectValidBishopFields(checkerboard, currentField, 1, 1));
            directions.Add(SelectValidBishopFields(checkerboard, currentField, 1, -1));
            directions.Add(SelectValidBishopFields(checkerboard, currentField, -1, 1));
            directions.Add(SelectValidBishopFields(checkerboard, currentField, -1, -1));

            foreach (var direction in directions)
            {
                foreach (var value in direction)
                {
                    AttackedFields.Add(value);
                }
            }
        }

        public override HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField)
        {
            var hashSet = new HashSet<string>();
            var directions = new List<List<string>>();
            directions.Add(AdjustForPossibleMoves(SelectValidBishopFields(checkerboard, currentField,1,1)));
            directions.Add(AdjustForPossibleMoves(SelectValidBishopFields(checkerboard, currentField,1,-1)));
            directions.Add(AdjustForPossibleMoves(SelectValidBishopFields(checkerboard, currentField,-1,1)));
            directions.Add(AdjustForPossibleMoves(SelectValidBishopFields(checkerboard, currentField,-1,-1)));

            foreach (var direction in directions)
            {
                foreach (var value in direction)
                {
                    hashSet.Add(value);
                }
            }
            return hashSet;
        }

        private List<Field> SelectValidBishopFields(Checkerboard checkerboard, Field currentField,int reverserRow,int reverserCol)
        {
            List<Field> selectedFields = new List<Field>();

            for (int i = 1; i < 9; i++)
            {
                if (checkerboard.CheckIfFieldIsOutOfTheBoard((currentField.Row - 1) + (i * reverserRow), (currentField.Col - 1) + (i * reverserCol)))
                    break;
                var targetField = checkerboard.Board[(currentField.Row - 1) + (i * reverserRow)][(currentField.Col - 1) + (i * reverserCol)];
                
                if (!targetField.IsUsed)
                {
                    selectedFields.Add(targetField);
                }
                if (targetField.IsUsed && targetField.Figure.IsWhite != currentField.Figure.IsWhite)
                {
                    selectedFields.Add(targetField);
                    break;
                }
                if (targetField.IsUsed && targetField.Figure.IsWhite == currentField.Figure.IsWhite)
                {
                    break;
                }
            }
            return selectedFields;
        }
        
    }
}
