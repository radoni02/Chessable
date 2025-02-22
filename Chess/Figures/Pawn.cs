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

        public override void Move(Checkerboard checkerboard,Field currentField,Position targetField)
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

            checkerboard.Board[currentField.Row-1][currentField.Col - 1] = temp;
            checkerboard.Board[currentField.Row-1][currentField.Col - 1].Row = currentField.Row;
            checkerboard.Board[currentField.Row-1][currentField.Col - 1].Col = currentField.Col;
        }


        public override HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField)
        {
            
            if(currentField.Figure.IsWhite)
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
                return possibleWhiteMoves;  
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
                return possibleBlackMoves;
            }

            return new HashSet<string>();
        }

        private string ForwardMoveByTwoWhite(Checkerboard checkerboard, Field currentField)
        {
            var tempField = new Field(currentField.Row + 1, currentField.Col);
            if ((currentField.Row == 7 || currentField.Row == 2) &&
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
            if ((currentField.Row == 7 || currentField.Row == 2) &&
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
        }
    }
}
