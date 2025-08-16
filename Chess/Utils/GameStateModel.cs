using Chess.Chessboard;
using Chess.Utils.ChessPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils
{
    public class GameStateModel
    {
        public bool IsValidMove { get; set; }
        public string? ErrorMessage { get; set; }
        internal PlayerColor CurrentPlayer { get; set; }
        internal PlayerColor NextPlayer { get; set; }
        public bool IsInCheck { get; set; }
        public bool IsCheckmate { get; set; }
        public bool IsStalemate { get; set; }
        public bool MoveMade { get; set; }
        public bool EmptyField { get; set; }
        public bool ChoosenWrongColorFigure { get; set; }
        public string BoardState { get; set; }
        public bool HalfMoveClockDraw { get; private set; }

        public GameStateModel(ICheckerboard checkerboard, PlayerColor currentPlayer)
        {
            IsValidMove = false;
            IsInCheck = false;
            IsCheckmate = false;
            IsStalemate = false;
            MoveMade = false;
            EmptyField = false;
            ChoosenWrongColorFigure = false;
            CurrentPlayer = currentPlayer;
            NextPlayer = currentPlayer == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
            BoardState = checkerboard.GetBoard();
            HalfMoveClockDraw = false;
        }

        public void SetMoveIsValid()
        {
            IsValidMove = true;
        }

        public void SetHalfMoveClockDraw()
        {
            HalfMoveClockDraw = true;
        }

        public void SetKingInCheckError()
        {
            IsValidMove = false;
            ErrorMessage = "your King is in check";
            IsInCheck = true;
        }

        public void SetIsInStalemate()
        {
            IsStalemate = true;
            IsValidMove = true;
        }

        public void SetIsInCheckmate()
        {
            IsCheckmate = true;
            IsValidMove = true;
        }

        public void SetTargetFieldIsNotValid()
        {
            IsValidMove = false;
            ErrorMessage = "Target field is not valid";
        }

        public void SetEmptyFieldError()
        {
            IsValidMove = false;
            ErrorMessage = "Choosen empty field";
            EmptyField = true;
        }
        public void SetWrongColorFigureError()
        {
            IsValidMove = false;
            ErrorMessage = "Choosen wrong color figure";
            ChoosenWrongColorFigure = true;
        }
        public void SetInvalidInputError()
        {
            IsValidMove = false;
            ErrorMessage = "Invalid input data";
        }
    }
}
