using Chess.Utils.ChessPlayer;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Chessboard;

namespace Chess.Public
{
    public class Chessable
    {
        public GameStateModel MakeMove(string move)
        {
            var chessGame = new ChessGame();
            return chessGame.Move(move);
        }
        public GameStateModel MakeMove(Position from, Position to)
        {
            var chessGame = new ChessGame();
            return chessGame.Move(from, to);
        }
        //public List<PossibleMove> GetLegalMoves(Position position);
        //public List<PossibleMove> GetAllLegalMoves();

        //// game state
        //public GameState GetCurrentGameState();
        //public BoardState GetBoardState();
        //public PlayerColor GetCurrentPlayer();

        //// game managament
        //public void ResetToStartingPosition();
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
