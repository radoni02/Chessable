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
    /// <summary>
    /// Represents the current state of a chess game after a move attempt.
    /// This model encapsulates all relevant information about the game's status,
    /// including move validation results, check conditions, and game termination states.
    /// </summary>
    /// <remarks>
    /// This class is returned by move operations to inform the caller about:
    /// - Whether the attempted move was valid and executed
    /// - Current game status (check, checkmate, stalemate)
    /// - Error messages for invalid moves
    /// - Current board representation
    /// 
    /// Example usage:
    /// <code>
    /// var gameState = chessable.MakeMove("e2-e4");
    /// if (!gameState.IsValidMove)
    ///     Console.WriteLine(gameState.ErrorMessage);
    /// 
    /// if (gameState.IsCheckmate)
    ///     Console.WriteLine("Game over - Checkmate!");
    /// </code>
    /// </remarks>
    public class GameStateModel
    {
        /// <summary>
        /// Gets a value indicating whether the attempted move was valid and successfully executed.
        /// </summary>
        /// <value>
        /// <c>true</c> if the move was valid and executed; otherwise, <c>false</c>.
        /// When <c>false</c>, check <see cref="ErrorMessage"/> for details about why the move failed.
        /// </value>
        public bool IsValidMove { get; private set; }
        /// <summary>
        /// Gets the error message describing why a move was invalid.
        /// </summary>
        public string? ErrorMessage { get; private set; }
        /// <summary>
        /// Gets the color of the player whose turn it currently is.
        /// </summary>
        internal PlayerColor CurrentPlayer { get; private set; }
        /// <summary>
        /// Gets the color of the player whose turn will be next.
        /// </summary>
        internal PlayerColor NextPlayer { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the current player's king is in check.
        /// </summary>
        /// <value>
        /// <c>true</c> if the king is under attack and must be moved to safety; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// When this is <c>true</c>, the player must make a move that removes the check condition.
        /// This could be by moving the king, blocking the attack, or capturing the attacking piece.
        /// </remarks>
        public bool IsInCheck { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the current position is checkmate.
        /// </summary>
        public bool IsCheckmate { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the current position is stalemate.
        /// </summary>
        /// <value>
        /// <c>true</c> if the current player has no legal moves but is not in check; 
        /// otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Stalemate results in a draw. The game ends when this condition is met.
        /// Unlike checkmate, the king is not under attack in stalemate.
        /// </remarks>
        public bool IsStalemate { get; private set; }
        /// <summary>
        /// Gets a value indicating whether a move was successfully made in this turn.
        /// </summary>
        public bool MoveMade { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the attempted move was on an empty field.
        /// </summary>
        /// <value>
        /// <c>true</c> if the source position of the move contained no piece; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// This flag helps identify the specific reason for move failure when debugging or
        /// providing detailed feedback to the user.
        /// </remarks>
        public bool EmptyField { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the player attempted to move an opponent's piece.
        /// </summary>
        /// <value>
        /// <c>true</c> if the piece at the source position belongs to the opponent; 
        /// otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// This flag helps identify when a player tries to move a piece that doesn't belong to them,
        /// which is an invalid move in chess.
        /// </remarks>
        public bool ChoosenWrongColorFigure { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the game should end in a draw due to the 50-move rule.
        /// </summary>
        /// <value>
        /// <c>true</c> if 50 moves have been made without a pawn move or capture; 
        /// otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// According to chess rules, if 50 consecutive moves are made without any pawn movement
        /// or piece capture, the game can be declared a draw.
        /// </remarks>
        public bool HalfMoveClockDraw { get; private set; }

        internal GameStateModel(ICheckerboard checkerboard, PlayerColor currentPlayer)
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
            HalfMoveClockDraw = false;
        }

        internal GameStateModel SetMoveIsValid()
        {
            IsValidMove = true;
            MoveMade = true;
            return this;
        }

        internal void SetHalfMoveClockDraw()
        {
            HalfMoveClockDraw = true;
        }

        internal void SetKingInCheckError()
        {
            IsValidMove = false;
            ErrorMessage = "your King is in check";
            IsInCheck = true;
        }

        internal void SetIsInStalemate()
        {
            IsStalemate = true;
            IsValidMove = true;
        }

        internal void SetIsInCheckmate()
        {
            IsCheckmate = true;
            IsValidMove = true;
        }

        internal void SetTargetFieldIsNotValid()
        {
            IsValidMove = false;
            ErrorMessage = "Target field is not valid";
        }

        internal void SetEmptyFieldError()
        {
            IsValidMove = false;
            ErrorMessage = "Choosen empty field";
            EmptyField = true;
        }
        internal void SetWrongColorFigureError()
        {
            IsValidMove = false;
            ErrorMessage = "Choosen wrong color figure";
            ChoosenWrongColorFigure = true;
        }
        internal void SetInvalidInputError()
        {
            IsValidMove = false;
            ErrorMessage = "Invalid input data";
        }
    }
}
