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
        public List<Field> GetRowFields(Checkerboard checkerboard, Field currentField, bool left)
        {
            var fieldsInSameRow = checkerboard.Board[currentField.Row - 1];
            List<Field> selectedFields = new List<Field>();

            if (left)
            {
                selectedFields = fieldsInSameRow
                                .Where(field => field.Col < currentField.Col)
                                .OrderByDescending(f => f.Col)
                                .TakeWhile(field => !field.IsUsed)
                                .ToList();
                CheckFirstOmittedField(fieldsInSameRow.Where(f => f.Col < currentField.Col).OrderByDescending(f => f.Col).ToList());
            }

            if (!left)
            {
                selectedFields = fieldsInSameRow
                                .Where(field => field.Col > currentField.Col)
                                .OrderBy(f => f.Col)
                                .TakeWhile(field => !field.IsUsed)
                                .ToList();
                CheckFirstOmittedField(fieldsInSameRow.Where(f => f.Col > currentField.Col).ToList());
            }

            return selectedFields;

            void CheckFirstOmittedField(List<Field> fieldsInSameColumnInValidDirection)
            {
                var firstOmittedField = fieldsInSameColumnInValidDirection
                                           .Skip(selectedFields.Count)
                                           .FirstOrDefault();
                if (firstOmittedField != null && firstOmittedField.IsUsed && firstOmittedField.Figure.IsWhite != currentField.Figure.IsWhite)
                    selectedFields.Add(firstOmittedField);
            }
        }
        public List<Field> GetColFields(Checkerboard checkerboard, Field currentField, bool down)
        {
            var fieldsInSameColumn = checkerboard.Board
                .SelectMany(row => row)
                .Where(field => field.Col == currentField.Col && field.Row != currentField.Row);
            List<Field> selectedFields = new List<Field>();

            if (down)
            {
                selectedFields = fieldsInSameColumn
                                .Where(field => field.Row < currentField.Row)
                                .OrderByDescending(f => f.Row)
                                .TakeWhile(field => !field.IsUsed)
                                .ToList();
                CheckFirstOmittedField(fieldsInSameColumn.Where(f => f.Row < currentField.Row).OrderByDescending(f => f.Row).ToList());
            }
            if (!down)
            {
                selectedFields = fieldsInSameColumn
                                .Where(field => field.Row > currentField.Row)
                                .OrderBy(f => f.Row)
                                .TakeWhile(field => !field.IsUsed)
                                .ToList();
                CheckFirstOmittedField(fieldsInSameColumn.Where(f => f.Row > currentField.Row).ToList());
            }

            return selectedFields;

            void CheckFirstOmittedField(List<Field> fieldsInSameColumnInValidDirection)
            {
                var firstOmittedField = fieldsInSameColumnInValidDirection
                                           .Skip(selectedFields.Count)
                                           .FirstOrDefault();
                if (firstOmittedField != null && firstOmittedField.IsUsed && firstOmittedField.Figure.IsWhite != currentField.Figure.IsWhite)
                    selectedFields.Add(firstOmittedField);
            }
        }
    }
}
