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
        public abstract HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField);
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

        public virtual List<IFigure> GetListOfFiguresAttackingTarget(Checkerboard checkerboard)
        {
            var allOppFields = checkerboard.Board.SelectMany(ff => ff)
                .Where(field => field.Figure is not null && field.Figure.IsWhite != this.IsWhite)
                .ToList();

            return allOppFields.Where(field => field.Figure.AttackedFields.Any(f => f.Figure != null && f.Figure.Equals(this)))
                .Select(field => field.Figure)
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

        public virtual List<Field> GetListOfFieldsThatAreBetweenCurrentAndTarget(Checkerboard checkerboard,Field currentFIeld,Field targetField)
        {
            targetField.Figure.CalculateAtackedFields(checkerboard,targetField);
            return targetField.Figure.AttackedFields.Where(field => field.Row < Math.Max(currentFIeld.Row,targetField.Row) 
                                                        && field.Row > Math.Min(currentFIeld.Row,targetField.Row)
                                                        && field.Col < Math.Max(currentFIeld.Col, targetField.Col)
                                                        && field.Col > Math.Min(currentFIeld.Col, targetField.Col))
                                                        .ToList();
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
