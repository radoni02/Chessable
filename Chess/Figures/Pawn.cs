using Chess.Chessboard;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures
{
    public class Pawn : Figure
    {
        public Pawn(bool isWhite, int value, string name) : base(isWhite, value, name)
        {
        }

        public override async void Move(Checkerboard checkerboard, Field currentField, Position targetField)
        {
            base.Move(checkerboard, currentField, targetField);
            var newCurrent = checkerboard.Board[targetField.Row][targetField.Col];
            CheckAndPromote(checkerboard, newCurrent);

        }

        public override void CalculateAtackedFields(Checkerboard checkerboard,Field currentField)
        {

            if(currentField.Figure.IsWhite)
            {
                CheckIfShloudAddToAttackedFields(checkerboard, currentField, -2, 0);
                CheckIfShloudAddToAttackedFields(checkerboard, currentField, 0, 0);
            }
            
            if(!currentField.Figure.IsWhite)
            {
                CheckIfShloudAddToAttackedFields(checkerboard, currentField, -2, -2);
                CheckIfShloudAddToAttackedFields(checkerboard, currentField, 0, -2);
            }
        }


        private void CheckIfShloudAddToAttackedFields(Checkerboard checkerboard, Field currentField,int adjustValueCol, int adjustValueRow)
        {
            if (CheckIfFieldIsOutOfTheBoard(checkerboard, currentField.Row + adjustValueRow, currentField.Col + adjustValueCol))
                return;
            if (IsWhite && !checkerboard.Board[currentField.Row][currentField.Col + adjustValueCol].IsUsed)
            {
                AttackedFields.Add(checkerboard.Board[currentField.Row][currentField.Col + adjustValueCol]);
            }
            if(IsWhite &&
                checkerboard.Board[currentField.Row][currentField.Col + adjustValueCol].IsUsed &&
                !checkerboard.Board[currentField.Row][currentField.Col + adjustValueCol].Figure.IsWhite)
            {
                AttackedFields.Add(checkerboard.Board[currentField.Row][currentField.Col + adjustValueCol]);
            }

            if(!IsWhite && !checkerboard.Board[currentField.Row + adjustValueRow][currentField.Col+ adjustValueCol].IsUsed)
            {
                AttackedFields.Add(checkerboard.Board[currentField.Row + adjustValueRow][currentField.Col + adjustValueCol]);
            }
            if (!IsWhite &&
                checkerboard.Board[currentField.Row + adjustValueRow][currentField.Col + adjustValueCol].IsUsed &&
                checkerboard.Board[currentField.Row + adjustValueRow][currentField.Col + adjustValueCol].Figure.IsWhite)
            {
                AttackedFields.Add(checkerboard.Board[currentField.Row + adjustValueRow][currentField.Col + adjustValueCol]);
            }
        }


        public override HashSet<string> CalculatePossibleMoves(Checkerboard checkerboard, Field currentField)
        {
            var additionalPosiibleMoves = currentField.Figure.AttackedFields.Where(f => f.IsUsed)
                .Select(filed =>
                {
                    return $"{filed.Row - 1}{filed.Col - 1}";
                })
                .ToHashSet();
            if (currentField.Figure.IsWhite)
            {
                var possibleWhiteMoves = new HashSet<string>();
                var forwardMove = ForwardMoveWhite(checkerboard, currentField);
                if (forwardMove is not "")
                {
                    possibleWhiteMoves.Add(forwardMove);
                }
                var moveByTwo = ForwardMoveByTwoWhite(checkerboard, currentField);
                if (moveByTwo is not "")
                {
                    possibleWhiteMoves.Add(moveByTwo);
                }
                return possibleWhiteMoves.Union(additionalPosiibleMoves)
                    .ToHashSet();
                
            }

            if(!currentField.Figure.IsWhite)
            {
                var possibleBlackMoves = new HashSet<string>();
                var forwardMove = ForwardMoveBlack(checkerboard, currentField);
                if (forwardMove is not "")
                {
                    possibleBlackMoves.Add(forwardMove);
                }
                var moveByTwo = ForwardMoveByTwoBlack(checkerboard, currentField);
                if (moveByTwo is not "")
                {
                    possibleBlackMoves.Add(moveByTwo);
                }
                return possibleBlackMoves.Union(additionalPosiibleMoves)
                    .ToHashSet();
            }

            return new HashSet<string>();
        }

        private string ForwardMoveByTwoWhite(Checkerboard checkerboard, Field currentField)
        {
            var tempField = new Field(currentField.Row + 1, currentField.Col);
            if ( currentField.Row == 2 &&
                ForwardMoveWhite(checkerboard, tempField) is string moveResult && 
                moveResult is not "")
            {
                return moveResult;
            }
            return "";
        }

        private string ForwardMoveByTwoBlack(Checkerboard checkerboard, Field currentField)
        {
            var tempField = new Field(currentField.Row - 1, currentField.Col);
            if (currentField.Row == 7 &&
                ForwardMoveBlack(checkerboard, tempField) is string moveResult &&
                moveResult is not "")
            {
                return moveResult;
            }
            return "";
        }


        private string ForwardMoveWhite(Checkerboard checkerboard, Field currentField)
        {
            return ForwardMove(checkerboard, currentField,0);
        }

        private string ForwardMoveBlack(Checkerboard checkerboard, Field currentField)
        {
            return ForwardMove(checkerboard, currentField, -2);
        }

        private string ForwardMove(Checkerboard checkerboard, Field currentField,int moveByValue)
        {
            return !checkerboard.Board[currentField.Row + moveByValue][currentField.Col - 1].IsUsed ? $"{currentField.Row + moveByValue}{currentField.Col - 1}" : string.Empty;
        }//there is some problem when pawn comes to last row, need to implement test case for that

        public void CheckAndPromote(Checkerboard checkerboard, Field currentField)
        {
            if (CanPromote(currentField))
            {
                PromotePawn(checkerboard, currentField, "Queen");
            }
        }

        private bool CanPromote(Field currentField)
        {
            return (IsWhite && currentField.Row == 8) || (!IsWhite && currentField.Row == 1);
        }

        public void PromotePawn(Checkerboard checkerboard, Field currentField, string figureType)
        {
            Figure newFigure = figureType.ToLower() switch
            {
                "queen" => new Queen(IsWhite, 9, "Queen"),
                "rook" => new Rook(IsWhite, 5, "Rook"),
                "bishop" => new Bishop(IsWhite, 3, "Bishop"),
                "knight" => new Knight(IsWhite, 3, "Knight"),
                _ => new Queen(IsWhite, 9, "Queen") // Default to Queen
            };

            newFigure.MoveConut = this.MoveConut;
            currentField.Figure = newFigure;
        }
    }
}
