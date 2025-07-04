using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Chessboard
{
    public sealed class ChessGame
    {
        public Player CurrentPlayer {  get; set; }
        public Checkerboard Board { get; set; }
        public CheckmateAnalysisResult CheckmateAnalysisResult { get; set; }
        public Dictionary<Player, Player> ChangePlayer { get; set; }
        public ChessGame()
        {
            Board = new Checkerboard();
            CheckmateAnalysisResult = new CheckmateAnalysisResult();
            var whitePlayer = new Player(0, true, true);
            var blackPlayer = new Player(0, false, false);

            ChangePlayer = new Dictionary<Player, Player>()
            {
                {whitePlayer,blackPlayer },
                {blackPlayer,whitePlayer }
            };

            CurrentPlayer = whitePlayer;
        }

        public void StartGame()
        {
            while (true)
            {
                Board.ShowNewPosition();
                Board.UsedFields();

                Console.WriteLine("Provide move in format \"a2-a3\":");
                var move = Console.ReadLine();
                var parsedInput = ParseMoveInput(move);

                var currentField = Board.CalculatePositionOnChessboard(parsedInput.Item1);

                if (CheckmateAnalysisResult.IsInCheck && currentField.Figure is not null && !currentField.Figure.Name.Equals("King"))
                {
                    Console.WriteLine("your King is in check");
                    continue;
                }

                if (!currentField.IsUsed)
                {
                    Console.WriteLine("Choosen empty field");
                    continue;
                }

                if ((CurrentPlayer.IsWhite && CurrentPlayer.IsWhite != currentField.Figure.IsWhite) ||
                    (!CurrentPlayer.IsWhite && CurrentPlayer.IsWhite != currentField.Figure.IsWhite))
                {
                    Console.WriteLine("Choosen wrong color figure");
                    continue;
                }

                List<string> possibleMoves = new List<string>();
                if (CheckmateAnalysisResult.IsInCheck)
                {
                    possibleMoves.AddRange(CheckmateAnalysisResult.PossibleCaptureRescues);
                    possibleMoves.AddRange(CheckmateAnalysisResult.PossibleBlockingMoves);
                }
                possibleMoves.AddRange(currentField.Figure.CalculatePossibleMoves(Board, currentField));

                var targetPosition = Board.CalculateTargetPosition(parsedInput.Item2);
                var targetMove = targetPosition.ToString();
                foreach (var possibleMove in possibleMoves)
                {
                    if (possibleMove.Equals(targetMove))
                    {
                        currentField.Figure.Move(Board, currentField, targetPosition);

                        var oppKingField = currentField.Figure.GetOppositKing(Board);
                        CheckmateAnalysisResult = GameStateAnalyzer.AnalyzeForCheckmate(Board, oppKingField);

                        ChangePlayer.TryGetValue(CurrentPlayer, out var currentPlayer);
                        CurrentPlayer = currentPlayer;
                        break;
                    }
                }
            }
        }

        public (string, string) ParseMoveInput(string input)
        {
            var positions = input.Split('-');
            var currentPossition = positions[0];
            var targetPosition = positions[1];
            return (currentPossition, targetPosition);
        }
    }
}
