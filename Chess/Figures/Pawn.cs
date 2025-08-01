﻿using Chess.Chessboard;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures
{
    internal class Pawn : Figure
    {
        public Pawn(bool isWhite, int value, string name) : base(isWhite, value, name)
        {
        }

        public override void Move(Checkerboard checkerboard, Field currentField, Position targetField)
        {
            base.Move(checkerboard, currentField, targetField);
            var newCurrent = checkerboard.Board[targetField.Row][targetField.Col];
            CheckAndPromote(checkerboard, newCurrent);

        }

        public override void CalculateAtackedFields(Checkerboard checkerboard,Field currentField)
        {
            if (currentField.Figure is null)
                return;

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
            int targetRow = currentField.Row + adjustValueRow;
            int targetCol = currentField.Col + adjustValueCol;

            if (CheckIfFieldIsOutOfTheBoard(checkerboard, targetRow, targetCol))
                return;

            var targetField = checkerboard.Board[targetRow][targetCol];
            var figure = targetField.Figure;

            if (IsWhite && !targetField.IsUsed)
            {
                AttackedFields.Add(targetField);
            }
            if (IsWhite && targetField.IsUsed && !figure!.IsWhite)
            {
                AttackedFields.Add(targetField);
            }

            if (!IsWhite && !targetField.IsUsed)
            {
                AttackedFields.Add(targetField);
            }
            if (!IsWhite && targetField.IsUsed && figure!.IsWhite)
            {
                AttackedFields.Add(targetField);
            }
        }


        protected override void CalculatePossibleMoves(Checkerboard checkerboard, Field currentField)
        {
            if (this.IsWhite)
            {
                var possibleWhiteMoves = new List<Field>();
                var forwardMove = ForwardMoveWhite(checkerboard, currentField);
                if (forwardMove is not null)
                {
                    possibleWhiteMoves.Add(forwardMove);
                }
                var moveByTwo = ForwardMoveByTwoWhite(checkerboard, currentField);
                if (moveByTwo is not null)
                {
                    possibleWhiteMoves.Add(moveByTwo);
                }
                possibleWhiteMoves = possibleWhiteMoves
                    .Union(this.AttackedFields, new FieldComparer())
                    .ToList();
                PossibleMoves = possibleWhiteMoves
                    .Select(target => new PossibleMove(new Position(currentField.Row, currentField.Col), new Position(target.Row, target.Col)))
                    .ToList();

            }
            if (!this.IsWhite)
            {
                var possibleBlackMoves = new List<Field>(); ;
                var forwardMove = ForwardMoveBlack(checkerboard, currentField);
                if (forwardMove is not null)
                {
                    possibleBlackMoves.Add(forwardMove);
                }
                var moveByTwo = ForwardMoveByTwoBlack(checkerboard, currentField);
                if (moveByTwo is not null)
                {
                    possibleBlackMoves.Add(moveByTwo);
                }
                possibleBlackMoves = possibleBlackMoves
                    .Union(this.AttackedFields, new FieldComparer())
                    .ToList();
                PossibleMoves = possibleBlackMoves
                    .Select(target => new PossibleMove(new Position(currentField.Row, currentField.Col), new Position(target.Row, target.Col)))
                    .ToList();
            }
        }

        private Field? ForwardMoveByTwoWhite(Checkerboard checkerboard, Field currentField)
        {
            var tempField = new Field(currentField.Row + 1, currentField.Col);
            if ( currentField.Row == 2 &&
                ForwardMoveWhite(checkerboard, tempField) is Field moveResult && 
                moveResult is not null)
            {
                return moveResult;
            }
            return null;
        }

        private Field? ForwardMoveByTwoBlack(Checkerboard checkerboard, Field currentField)
        {
            var tempField = new Field(currentField.Row - 1, currentField.Col);
            if (currentField.Row == 7 &&
                ForwardMoveBlack(checkerboard, tempField) is Field moveResult &&
                moveResult is not null)
            {
                return moveResult;
            }
            return null;
        }


        private Field? ForwardMoveWhite(Checkerboard checkerboard, Field currentField)
        {
            return ForwardMove(checkerboard, currentField,0);
        }

        private Field? ForwardMoveBlack(Checkerboard checkerboard, Field currentField)
        {
            return ForwardMove(checkerboard, currentField, -2);
        }

        private Field? ForwardMove(Checkerboard checkerboard, Field currentField, int moveByValue)
        {
            return !checkerboard.Board[currentField.Row + moveByValue][currentField.Col - 1].IsUsed ? checkerboard.Board[currentField.Row + moveByValue][currentField.Col - 1] : null;
        }

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
