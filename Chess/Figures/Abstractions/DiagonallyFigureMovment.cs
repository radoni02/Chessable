using Chess.Chessboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures.Abstractions
{
    internal class DiagonallyFigureMovment
    {
        public MovmentResult GetFieldsFromDiagonalFigureMovment(Checkerboard checkerboard, Field currentField)
        {
            var result = new MovmentResult();

            var upperLeft = SelectValidFieldsOnTheDiagonals(checkerboard, currentField, -1, -1);
            result.AtackedFields.AddRange(upperLeft.AtackedFields);
            result.PossibleMoves.AddRange(upperLeft.PossibleMoves);

            var upperRight = SelectValidFieldsOnTheDiagonals(checkerboard, currentField, -1, 1);
            result.AtackedFields.AddRange(upperRight.AtackedFields);
            result.PossibleMoves.AddRange(upperRight.PossibleMoves);

            var lowerLeft = SelectValidFieldsOnTheDiagonals(checkerboard, currentField, 1, -1);
            result.AtackedFields.AddRange(lowerLeft.AtackedFields);
            result.PossibleMoves.AddRange(lowerLeft.PossibleMoves);

            var lowerRight = SelectValidFieldsOnTheDiagonals(checkerboard, currentField, 1, 1);
            result.AtackedFields.AddRange(lowerRight.AtackedFields);
            result.PossibleMoves.AddRange(lowerRight.PossibleMoves);

            return result;
        }

        private MovmentResult SelectValidFieldsOnTheDiagonals(Checkerboard checkerboard, Field currentField, int reverserRow, int reverserCol)
        {
            var result = new MovmentResult();
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
                if(targetField.IsUsed && targetField.Figure.IsWhite == currentField.Figure.IsWhite && !possibleMovesLock)
                {
                    result.AtackedFields.Add(targetField);
                    break;
                }
                if ((targetField.IsUsed && targetField.Figure.IsWhite != currentField.Figure.IsWhite && possibleMovesLock)
                        || (targetField.IsUsed && targetField.Figure.IsWhite == currentField.Figure.IsWhite && possibleMovesLock))
                {
                    break;
                }
            }
            return result;
        }
    }
}
