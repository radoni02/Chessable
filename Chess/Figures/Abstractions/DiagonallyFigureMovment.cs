using Chess.Chessboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures.Abstractions
{
    public class DiagonallyFigureMovment
    {
        public List<Field> SelectValidFieldsOnTheDiagonals(Checkerboard checkerboard, Field currentField, int reverserRow, int reverserCol)
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
