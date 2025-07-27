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

        //// game state
        //public GameState GetCurrentGameState();
        //public BoardState GetBoardState();
        //public PlayerColor GetCurrentPlayer();

        //// game managament
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
