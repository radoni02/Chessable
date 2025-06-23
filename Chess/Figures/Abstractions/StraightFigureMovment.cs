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
        public MovmentResult GetRowFields(Checkerboard checkerboard, Field currentField, bool left)
        {
            var result = new MovmentResult();
            var fieldsInSameRow = checkerboard.Board[currentField.Row - 1];

            if (left)
            {
                var orderedFields = fieldsInSameRow
                                .Where(field => field.Col < currentField.Col)
                                .OrderByDescending(f => f.Col);
                result.AtackedFields = orderedFields
                            .TakeWhile(field => !field.IsUsed 
                                    || (field.Figure is not null 
                                            && field.Figure is King 
                                            && field.Figure.IsWhite != currentField.Figure.IsWhite))
                            .ToList();
                result.PossibleMoves = orderedFields
                            .TakeWhile(field => !field.IsUsed)
                            .ToList();

                CheckFirstOmittedField(fieldsInSameRow.Where(f => f.Col < currentField.Col).OrderByDescending(f => f.Col).ToList());
            }

            if (!left)
            {
                var orderedFields = fieldsInSameRow
                                .Where(field => field.Col > currentField.Col)
                                .OrderBy(f => f.Col);
                result.AtackedFields = orderedFields
                            .TakeWhile(field => !field.IsUsed
                                    || (field.Figure is not null
                                            && field.Figure is King
                                            && field.Figure.IsWhite != currentField.Figure.IsWhite))
                            .ToList();
                result.PossibleMoves = orderedFields
                            .TakeWhile(field => !field.IsUsed)
                            .ToList();

                CheckFirstOmittedField(fieldsInSameRow.Where(f => f.Col > currentField.Col).ToList());
            }

            return result;

            void CheckFirstOmittedField(List<Field> fieldsInSameColumnInValidDirection)
            {
                var firstOmittedField = fieldsInSameColumnInValidDirection
                                           .Skip(result.PossibleMoves.Count)
                                           .FirstOrDefault();
                if (firstOmittedField != null && firstOmittedField.IsUsed && firstOmittedField.Figure.IsWhite != currentField.Figure.IsWhite)
                {
                    result.PossibleMoves.Add(firstOmittedField);
                    result.AtackedFields.Add(firstOmittedField);
                    result.AtackedFields = result.AtackedFields.DistinctBy(field => (field.Row,field.Col)).ToList();
                }
            }
        }
        public MovmentResult GetColFields(Checkerboard checkerboard, Field currentField, bool down)
        {
            var result = new MovmentResult();
            var fieldsInSameColumn = checkerboard.Board
                .SelectMany(row => row)
                .Where(field => field.Col == currentField.Col && field.Row != currentField.Row);

            if (down)
            {
                var orderedFields = fieldsInSameColumn
                                .Where(field => field.Row < currentField.Row)
                                .OrderByDescending(f => f.Row);

                result.AtackedFields = orderedFields
                            .TakeWhile(field => !field.IsUsed
                                    || (field.Figure is not null
                                            && field.Figure is King
                                            && field.Figure.IsWhite != currentField.Figure.IsWhite))
                            .ToList();
                result.PossibleMoves = orderedFields
                            .TakeWhile(field => !field.IsUsed)
                            .ToList();

                CheckFirstOmittedField(fieldsInSameColumn.Where(f => f.Row < currentField.Row).OrderByDescending(f => f.Row).ToList());
            }
            if (!down)
            {
                var orderedFields = fieldsInSameColumn
                                .Where(field => field.Row > currentField.Row)
                                .OrderBy(f => f.Row);

                result.AtackedFields = orderedFields
                            .TakeWhile(field => !field.IsUsed
                                    || (field.Figure is not null
                                            && field.Figure is King
                                            && field.Figure.IsWhite != currentField.Figure.IsWhite))
                            .ToList();
                result.PossibleMoves = orderedFields
                            .TakeWhile(field => !field.IsUsed)
                            .ToList();

                CheckFirstOmittedField(fieldsInSameColumn.Where(f => f.Row > currentField.Row).ToList());
            }

            return result;

            void CheckFirstOmittedField(List<Field> fieldsInSameColumnInValidDirection)
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
}
