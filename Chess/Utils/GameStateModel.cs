using Chess.Chessboard;
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
        public Player CurrentPlayer { get; set; }
        public Player NextPlayer { get; set; }
        public bool IsInCheck { get; set; }
        public bool IsCheckmate { get; set; }
        public bool IsStalemate { get; set; }
        public bool MoveMade { get; set; }
        public bool EmptyField { get; set; }
        public bool ChoosenWrongColorFigure { get; set; }
        public Checkerboard BoardState { get; set; }

        public GameStateModel(Checkerboard checkerboard)
        {
            IsValidMove = false;
            IsInCheck = false;
            IsCheckmate = false;
            IsStalemate = false;
            MoveMade = false;
            EmptyField = false;
            ChoosenWrongColorFigure = false;
            BoardState = checkerboard;
        }

        public void SetKingInCheckError()
        {
            IsValidMove = false;
            ErrorMessage = "your King is in check";
            IsInCheck = true;
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
