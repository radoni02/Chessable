using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Chessboard;
using Chess.Utils;

namespace Chess.Figures
{
    public abstract class Figure : IFigure
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
        public int MoveConut { get; set; } = 0;
        public abstract HashSet<string> CalculatePossibleMoves(Checkerboard checkerboard, Field currentField);
        public virtual void Move(Checkerboard checkerboard, Field currentField, Position targetField)
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

                checkerboard.Board[currentField.Row - 1][currentField.Col - 1] = new Field(false, currentField.Row, currentField.Col);
            }
            currentField.Figure.MoveConut++;
        }
        public Field GetOppositKing(Checkerboard checkerboard)
        {
            var oppositeKing = checkerboard.Board
                .SelectMany(ff => ff)
                .FirstOrDefault(field => field.Figure is not null
                                && field.Figure.IsWhite != this.IsWhite
                                && field.Figure.Name.Equals("King"));
            return oppositeKing;
        }

        public virtual List<Field> GetListOfFieldsAttackingTarget(Checkerboard checkerboard)
        {
            var allOppFields = checkerboard.Board.SelectMany(ff => ff)
                .Where(field => field.Figure is not null && field.Figure.IsWhite != this.IsWhite)
                .ToList();

            return allOppFields.Where(field => field.Figure.AttackedFields.Any(f => f.Figure != null && f.Figure.Equals(this)))
                .ToList();
        }

        public virtual List<IFigure> GetFiguresThatCanMoveToTheField(Checkerboard checkerboard,Field targetField, bool isWhite)
        {
            var selectedFigures = new List<IFigure>();
            var allianceFields = checkerboard.Board
                .SelectMany(ff => ff)
                .Where(field => field.Figure is not null 
                    && field.Figure.IsWhite == isWhite
                    && field.Figure.Name != "King")
                .ToList();

            foreach (var field in allianceFields)
            {
                field.Figure.CalculateAtackedFields(checkerboard,field);
                if (field.Figure.AttackedFields.Any(field => field.Equals(targetField)))
                    selectedFigures.Add(field.Figure);
            }
            return selectedFigures;
        }

        public virtual List<Field> GetListOfFieldsThatAreBetweenCurrentAndTarget(Checkerboard checkerboard,Field currentField, Field targetField)
        {
            var fieldsBetween = new List<Field>();

            int rowDiff = targetField.Row - currentField.Row;
            int colDiff = targetField.Col - currentField.Col;

            if (rowDiff != 0 && colDiff != 0 && Math.Abs(rowDiff) != Math.Abs(colDiff))
            {
                return fieldsBetween;
            }

            int rowStep = rowDiff == 0 ? 0 : (rowDiff > 0 ? 1 : -1);
            int colStep = colDiff == 0 ? 0 : (colDiff > 0 ? 1 : -1);

            int currentRow = currentField.Row + rowStep;
            int currentCol = currentField.Col + colStep;

            while (currentRow != targetField.Row || currentCol != targetField.Col)
            {
                if (!CheckIfFieldIsOutOfTheBoard(checkerboard, currentRow - 1, currentCol - 1))
                {
                    fieldsBetween.Add(checkerboard.Board[currentRow - 1][currentCol - 1]);
                }

                currentRow += rowStep;
                currentCol += colStep;
            }

            return fieldsBetween;
        }

        public virtual bool CheckIfFigureIsImmobilized(Checkerboard checkerboard)
        {
            if (this == null) return false;

            if (this is King) return false;

            var king = checkerboard.Board
               .SelectMany(ff => ff)
               .FirstOrDefault(field => field.Figure is not null
                       && field.Figure.IsWhite == this.IsWhite
                       && field.Figure.Name.Equals("King"));

            if (king == null) return false;

            var fieldsThatAttackCurrentFigure = this.GetListOfFieldsAttackingTarget(checkerboard);
            var fieldsThatAttackCurrentFigureButNotOppKing = fieldsThatAttackCurrentFigure
                                                                .Where(field => field.Figure != null
                                                                                && !field.Figure.AttackedFields
                                                                                                    .Any(attackField => attackField.Equals(king)))
                                                                .ToList();
            if (fieldsThatAttackCurrentFigureButNotOppKing.Count == 0)
                return false;

            bool kingWouldBeAttacked = false;

            MakeTempMove(checkerboard, fieldsThatAttackCurrentFigureButNotOppKing, (tempBoard) =>
            {
                foreach (var attackingField in fieldsThatAttackCurrentFigureButNotOppKing)
                {
                    if (attackingField.Figure.AttackedFields.Any(field =>
                        field.Row == king.Row && field.Col == king.Col))
                    {
                        kingWouldBeAttacked = true;
                        break;
                    }
                }
            });

            return kingWouldBeAttacked;
        }

        public void MakeTempMove(Checkerboard checkerboard,List<Field> fieldsToRecalculate,Action<Checkerboard> calculations)
        {
            var currentField = checkerboard.Board
                .SelectMany(ff => ff)
                .FirstOrDefault(field => field.Figure != null && field.Figure.Equals(this));
            var originalFigure = currentField.Figure;
            var originalIsUsed = currentField.IsUsed;
            try
            {
                currentField.Figure = null;
                currentField.IsUsed = false;
                foreach (var field in fieldsToRecalculate)
                {
                    field.Figure.CalculateAtackedFields(checkerboard, field);
                }
                calculations.Invoke(checkerboard);
            }
            finally
            {
                currentField.Figure = originalFigure;
                currentField.IsUsed = originalIsUsed;
                foreach (var field in fieldsToRecalculate)
                {
                    field.Figure.CalculateAtackedFields(checkerboard, field);
                }
            }
        }

        public virtual bool CheckIfFigureIsUnderAttack(Checkerboard checkerboard)
        {
            var oppFields = checkerboard.Board.SelectMany(ff => ff)
                .Where(field => field.Figure is not null && field.Figure.IsWhite != this.IsWhite)
                .ToList();

            foreach (var relevantField in oppFields)
            {
                relevantField.Figure.CalculateAtackedFields(checkerboard, relevantField);
            }

            var uniqueAttackedFields = oppFields.SelectMany(f => f.Figure.AttackedFields).Distinct().ToList();
            return uniqueAttackedFields.Any(field => field.Figure is not null && field.Figure.Equals(this));
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
        protected List<string> AdjustForPossibleMoves(List<Field> fields)
        {
            return fields.Select(field => $"{field.Row - 1}{field.Col - 1}")
                                .ToList();
        }


    }
}
