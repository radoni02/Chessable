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
            var result = new MovmentResult();
            List<Field> selectedFields = new List<Field>();
            var possibleMovesLock = false;

            for (int i = 1; i < 9; i++)
            {
                if (checkerboard.CheckIfFieldIsOutOfTheBoard((currentField.Row - 1) + (i * reverserRow), (currentField.Col - 1) + (i * reverserCol)))
                    break;
                var targetField = checkerboard.Board[(currentField.Row - 1) + (i * reverserRow)][(currentField.Col - 1) + (i * reverserCol)];

                if(targetField.Figure is not null
                                            && targetField.Figure is King
                                            && targetField.Figure.IsWhite != currentField.Figure.IsWhite)
                {
                    result.AtackedFields.Add(targetField);
                    result.PossibleMoves.Add(targetField);
                    possibleMovesLock = true;
                    continue;
                }
                if (!targetField.IsUsed)
                {
                    result.AtackedFields.Add(targetField);
                    if (!possibleMovesLock)
                        result.PossibleMoves.Add(targetField);
                    continue;
                }
                if (targetField.IsUsed && targetField.Figure.IsWhite != currentField.Figure.IsWhite && !possibleMovesLock)
                {
                    result.AtackedFields.Add(targetField);
                    result.PossibleMoves.Add(targetField);
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
