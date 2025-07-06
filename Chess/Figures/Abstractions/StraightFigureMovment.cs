using Chess.Chessboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures.Abstractions
{
    public class StraightFigureMovment
    {
        public MovmentResult GetFieldsFromStraightFigureMovment(Checkerboard checkerboard, Field currentField)
        {
            var result = new MovmentResult();

            var fieldsInSameRow = checkerboard.Board[currentField.Row - 1];

            var leftRow = GetRowLeftFields(checkerboard,currentField,fieldsInSameRow);
            result.AtackedFields.AddRange(leftRow.AtackedFields);
            result.PossibleMoves.AddRange(leftRow.PossibleMoves);

            var rightRow = GetRowRightFields(checkerboard,currentField,fieldsInSameRow);
            result.AtackedFields.AddRange(rightRow.AtackedFields);
            result.PossibleMoves.AddRange(rightRow.PossibleMoves);

            var fieldsInSameColumn = checkerboard.Board
                .SelectMany(row => row)
                .Where(field => field.Col == currentField.Col 
                        && field.Row != currentField.Row);

            var downColumn = GetColumnDownFields(checkerboard,currentField,fieldsInSameColumn);
            result.AtackedFields.AddRange(downColumn.AtackedFields);
            result.PossibleMoves.AddRange(downColumn.PossibleMoves);

            var upColumn = GetColumnUpFields(checkerboard, currentField, fieldsInSameColumn);
            result.AtackedFields.AddRange(upColumn.AtackedFields);
            result.PossibleMoves.AddRange(upColumn.PossibleMoves);

            return result;
        }
        public MovmentResult GetRowLeftFields(Checkerboard checkerboard, Field currentField, List<Field> fieldsInSameRow)
        {
            var result = new MovmentResult();
            var orderedFields = fieldsInSameRow
                                    .Where(field => field.Col < currentField.Col)
                                    .OrderByDescending(f => f.Col);

            result.AtackedFields
                        .AddRange(orderedFields
                        .TakeWhile(field => !field.IsUsed
                                || (field.Figure is not null
                                        && field.Figure is King
                                        && field.Figure.IsWhite != currentField.Figure.IsWhite))
                        .ToList());

            result.PossibleMoves
                        .AddRange(orderedFields
                        .TakeWhile(field => !field.IsUsed)
                        .ToList());

            CheckFirstOmittedField(fieldsInSameRow.Where(f => f.Col < currentField.Col).OrderByDescending(f => f.Col).ToList(),currentField,result);

            return result;
        }

        public MovmentResult GetRowRightFields(Checkerboard checkerboard, Field currentField, List<Field> fieldsInSameRow)
        {
            var result = new MovmentResult();
            var orderedFields = fieldsInSameRow
                                    .Where(field => field.Col > currentField.Col)
                                    .OrderBy(f => f.Col);
            result.AtackedFields
                        .AddRange(orderedFields
                        .TakeWhile(field => !field.IsUsed
                                || (field.Figure is not null
                                        && field.Figure is King
                                        && field.Figure.IsWhite != currentField.Figure.IsWhite))
                        .ToList());

            result.PossibleMoves
                        .AddRange(orderedFields
                        .TakeWhile(field => !field.IsUsed)
                        .ToList());

            CheckFirstOmittedField(fieldsInSameRow.Where(f => f.Col > currentField.Col).ToList(),currentField,result);
            return result;  
        }

        public MovmentResult GetColumnDownFields(Checkerboard checkerboard, Field currentField, IEnumerable<Field> fieldsInSameColumn)
        {
            var result = new MovmentResult();
            var orderedFields = fieldsInSameColumn
                                    .Where(field => field.Row < currentField.Row)
                                    .OrderByDescending(f => f.Row);

            result.AtackedFields
                        .AddRange(orderedFields
                        .TakeWhile(field => !field.IsUsed
                                || (field.Figure is not null
                                        && field.Figure is King
                                        && field.Figure.IsWhite != currentField.Figure.IsWhite))
                        .ToList());

            result.PossibleMoves
                        .AddRange(orderedFields
                        .TakeWhile(field => !field.IsUsed)
                        .ToList());

            CheckFirstOmittedField(fieldsInSameColumn.Where(f => f.Row < currentField.Row).OrderByDescending(f => f.Row).ToList(),currentField,result);
            return result;
        }

        public MovmentResult GetColumnUpFields(Checkerboard checkerboard, Field currentField, IEnumerable<Field> fieldsInSameColumn)
        {
            var result = new MovmentResult();
            var orderedFields = fieldsInSameColumn
                                    .Where(field => field.Row > currentField.Row)
                                    .OrderBy(f => f.Row);

            result.AtackedFields
                        .AddRange(orderedFields
                        .TakeWhile(field => !field.IsUsed
                                || (field.Figure is not null
                                        && field.Figure is King
                                        && field.Figure.IsWhite != currentField.Figure.IsWhite))
                        .ToList());

            result.PossibleMoves
                        .AddRange(orderedFields
                        .TakeWhile(field => !field.IsUsed)
                        .ToList());

            CheckFirstOmittedField(fieldsInSameColumn.Where(f => f.Row > currentField.Row).ToList(),currentField,result);
            return result;
        }

        private void CheckFirstOmittedField(List<Field> fieldsInSameColumnInValidDirection,Field currentField, MovmentResult result)
        {
            var firstOmittedField = fieldsInSameColumnInValidDirection
                                       .Skip(result.PossibleMoves.Count)
                                       .FirstOrDefault();
            if (firstOmittedField != null && firstOmittedField.IsUsed && firstOmittedField.Figure.IsWhite != currentField.Figure.IsWhite)
            {
                result.PossibleMoves.Add(firstOmittedField);
                result.AtackedFields.Add(firstOmittedField);
                result.AtackedFields = result.AtackedFields.DistinctBy(field => (field.Row, field.Col)).ToList();
            }
        }
    }
}
