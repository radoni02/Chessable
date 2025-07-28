using Chess.Utils.ChessPlayer;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Chessboard;
using Chess.Figures;

namespace Chess.Public
{
    public class Chessable
    {
        private ChessGame game;
        public Chessable()
        {
            game = new ChessGame();
        }

        /// <summary>
        /// Makes a move in the game using a string-based representation of the move.
        /// </summary>
        /// <param name="move">A string representing the move (e.g., "e2-e4").</param>
        /// <returns>The updated game state after the move.</returns>
        public GameStateModel MakeMove(string move)
        {
            game.Board.UsedFields();
            return game.Move(move);
        }

        /// <summary>
        /// Makes a move in the game using source and destination positions.
        /// </summary>
        /// <param name="from">The starting position of the piece.</param>
        /// <param name="to">The target position to move the piece to.</param>
        /// <returns>The updated game state after the move.</returns>
        public GameStateModel MakeMove(Position from, Position to)
        {
            game.Board.UsedFields();
            return game.Move(from, to);
        }

        /// <summary>
        /// Returns all legal moves for the piece located at the specified position,
        /// or <c>null</c> if the field is empty or invalid.
        /// </summary>
        /// <param name="position">The position of the piece to analyze for legal moves.</param>
        /// <returns>
        /// A list of <see cref="PossibleMove"/> objects representing all legal moves for the piece at the given position,
        /// or <c>null</c> if the field is empty, unused, or has no figure.
        /// </returns>
        public List<PossibleMove>? GetLegalMoves(Position position)
        {
            var currentField = game.Board.Board[position.Row - 1][position.Col - 1];
            if(currentField is null || currentField.Figure is null || currentField.IsUsed == false)
            {
                return null;
            }
            currentField.Figure.CheckPossibleMoves(game.Board, currentField);
            return currentField.Figure.PossibleMoves;
        }

        /// <summary>
        /// Returns all legal moves for the specified player color, including possible check or checkmate escape moves.
        /// </summary>
        /// <param name="playerColor">The color of the player to get legal moves for.</param>
        /// <returns>A list of all legal <see cref="PossibleMove"/> objects available for the specified player.</returns>
        public List<PossibleMove> GetAllLegalMoves(PlayerColor playerColor)
        {
            var possibleMoves = new List<PossibleMove>();
            var isWhite = playerColor == PlayerColor.White ? true : false;
            var fields = game.Board.Board
                .SelectMany(ff => ff)
                .Where(field => field.Figure is not null
                        && field.Figure.IsWhite.Equals(isWhite));

            foreach(var field in fields)
            {
                field.Figure!.CheckPossibleMoves(game.Board,field);
                possibleMoves.AddRange(field.Figure.PossibleMoves!);
            }

            var kingField = fields.FirstOrDefault(field => field.Figure is King);
            game.CheckmateAnalysisResult = GameStateAnalyzer.AnalizeGameState(game.Board, kingField);
            if (game.CheckmateAnalysisResult.IsInCheck)
            {
                possibleMoves.AddRange(game.CheckmateAnalysisResult.PossibleCaptureRescues);
                possibleMoves.AddRange(game.CheckmateAnalysisResult.PossibleBlockingMoves);
            }

            return possibleMoves;
        }

        public GameResult? GameResult(GameStateModel gameState)
        {
            if (gameState.IsCheckmate)
                return new GameResult(GameResultType.Win, game.CurrentPlayer.Color);

            if (gameState.IsStalemate)
                return new GameResult(GameResultType.Draw);
            return null;
        }

        /// <summary>
        /// Displays the current game board state in the console.
        /// </summary>
        /// <remarks>
        /// This is a helper method mainly intended for debugging or testing purposes.
        /// It prints the current layout of the pieces on the board to the console.
        /// </remarks>
        public void ShowPosition()
        {
            game.Board.ShowNewPosition();
        }

        /// <summary>
        /// Returns the current check status of the game, including check, checkmate, or stalemate (pat).
        /// </summary>
        /// <returns>
        /// A <see cref="CheckmateAnalysisResult"/> representing the current game's state analysis.
        /// </returns>
        public CheckmateAnalysisResult EvaluatePosition()
        {
            return game.CheckmateAnalysisResult;
        }

        /// <summary>
        /// Evaluates the game state (e.g., check, checkmate, stalemate) from the perspective of the piece
        /// located at the specified board position.
        /// </summary>
        /// <param name="position">
        /// The position on the board to analyze (1-based indexing). Must point to the king's current position.
        /// </param>
        /// <returns>
        /// A <see cref="CheckmateAnalysisResult"/> representing the game state based on the specified position.
        /// </returns>
        public CheckmateAnalysisResult EvaluatePosition(Position position)
        {
            var checkedField = game.Board.Board[position.Row - 1][position.Col - 1];
            return GameStateAnalyzer.AnalizeGameState(game.Board, checkedField);
        }

        /// <summary>
        /// Gets the color of the player whose turn it currently is.
        /// </summary>
        /// <returns>
        /// The <see cref="PlayerColor"/> of the current player (White or Black).
        /// </returns>
        public PlayerColor GetCurrentPlayer()
        {
            return game.CurrentPlayer.Color;
        }

        /// <summary>
        /// Resets the game to the initial starting position, clearing all progress.
        /// </summary>
        public void ResetToStartingPosition()
        {
            game = new ChessGame();
        }
        //public Result LoadFromFEN(string fenString);
        //public string ExportToFEN();
        //public Result LoadFromPGN(string pgnString);
        //public string ExportToPGN();

        //// move history
        //public List<Move> GetMoveHistory();
        //public Result UndoLastMove();
        //public Result RedoMove();
    }
}
