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
            List<List<Field>> directions = new List<List<Field>>();
            directions.Add(GetRowFields(checkerboard, currentField, true));
            directions.Add(GetRowFields(checkerboard, currentField, false));
            directions.Add(GetColFields(checkerboard, currentField, true));
            directions.Add(GetColFields(checkerboard, currentField, false));

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
            List<List<string>> directions = new List<List<string>>();
            directions.Add(AdjustForPossibleMoves(GetRowFields(checkerboard, currentField, true),currentField));
            directions.Add(AdjustForPossibleMoves(GetRowFields(checkerboard, currentField, false), currentField));
            directions.Add(AdjustForPossibleMoves(GetColFields(checkerboard, currentField, true),currentField));
            directions.Add(AdjustForPossibleMoves(GetColFields(checkerboard, currentField, false), currentField));

            var hashSet = new HashSet<string>();
            foreach (var direction in directions) 
            {
                foreach(var value in direction)
                {
                    hashSet.Add(value);
                }
            }
            return hashSet;
        }

        private List<string> AdjustForPossibleMoves(List<Field> fields, Field currentField)
        {
            return fields.Select(field => $"{field.Row - 1}{field.Col - 1}")
                                .ToList();
        }

        private List<Field> GetRowFields(Checkerboard checkerboard, Field currentField,bool left)
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
                CheckFirstOmittedField(fieldsInSameRow.Where(f => f.Col < currentField.Col).ToList());
            }

            if(!left)
            {
                selectedFields = fieldsInSameRow
                                .Where(field => field.Col > currentField.Col)
                                .OrderBy(f => f.Col)
                                .TakeWhile(field => !field.IsUsed)
                                .ToList();
                CheckFirstOmittedField(fieldsInSameRow.Where(f => f.Col > currentField.Col).ToList());
            }

            return selectedFields;

            void CheckFirstOmittedField(List<Field> selectedFields)
            {
                var firstOmittedField = fieldsInSameRow
                                           .Skip(selectedFields.Count)
                                           .FirstOrDefault();
                if (firstOmittedField != null && firstOmittedField.IsUsed && firstOmittedField.Figure.IsWhite != currentField.Figure.IsWhite)
                    selectedFields.Add(firstOmittedField);
            }
        }

        private List<Field> GetColFields(Checkerboard checkerboard, Field currentField, bool down)
        {
            var fieldsInSameColumn = checkerboard.Board
                .SelectMany(row => row)
                .Where(field => field.Col == currentField.Col && field.Row != currentField.Row);
            List<Field> selectedFields = new List<Field>();

            if(down)
            {
                selectedFields = fieldsInSameColumn
                                .Where(field => field.Row < currentField.Row)
                                .OrderByDescending(f => f.Row)
                                .TakeWhile(field => !field.IsUsed)
                                .ToList();
                CheckFirstOmittedField(fieldsInSameColumn.Where(f => f.Row < currentField.Row).ToList());
            }
            if(!down)
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
