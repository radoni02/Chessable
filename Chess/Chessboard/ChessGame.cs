using Chess.Utils;
using Chess.Utils.ChessPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chess.Chessboard
{
    public sealed class ChessGame
    {
        internal Player CurrentPlayer {  get; set; }
        public Checkerboard Board { get; set; }
        public CheckmateAnalysisResult CheckmateAnalysisResult { get; set; }
        internal Dictionary<Player, Player> ChangePlayer { get; set; }
        public ChessGame()
        {
            Board = new Checkerboard();
            CheckmateAnalysisResult = new CheckmateAnalysisResult();
            var whitePlayer = new Player(0, PlayerColor.White, true);
            var blackPlayer = new Player(0, PlayerColor.Black, false);

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

                var gameState = Move(move);
                if(!gameState.IsValidMove)
                    Console.WriteLine(gameState.ErrorMessage);
            }
        }
        public GameStateModel Move(Position from, Position to)
        {
            var gameState = new GameStateModel(Board, CurrentPlayer.Color);
            this.MoveLogic(new ParseInputResult(from,to,true), gameState);
            return gameState;
        }
        public GameStateModel Move(string move)
        {
            var gameState = new GameStateModel(Board, CurrentPlayer.Color);
            var parsedInput = ParseMoveInput(move);

            if (!MoveValidation.ValidateInput(parsedInput, gameState))
                return gameState;

            this.MoveLogic(parsedInput,gameState);
            return gameState;
        }

        private GameStateModel MoveLogic(ParseInputResult parsedInput,GameStateModel gameState)
        {
            var currentField = Board.GetCurrentField(parsedInput.CurrentPosition);

            if (!MoveValidation.ValidateKingInCheck(CheckmateAnalysisResult, currentField, gameState))
                return gameState;

            if (!MoveValidation.ValidateFieldUsage(currentField, gameState))
                return gameState;

            if (!MoveValidation.ValidatePlayerTurn(CurrentPlayer, currentField, gameState))
                return gameState;

            var possibleMoves = new List<PossibleMove>();
            if (CheckmateAnalysisResult.IsInCheck)
            {
                possibleMoves.AddRange(CheckmateAnalysisResult.PossibleCaptureRescues);
                possibleMoves.AddRange(CheckmateAnalysisResult.PossibleBlockingMoves);
            }
            currentField.Figure.CheckPossibleMoves(Board, currentField);
            possibleMoves.AddRange(currentField.Figure.PossibleMoves);
            foreach (var possibleMove in possibleMoves)
            {
                if (possibleMove.TargetPosition.Equals(parsedInput.TargetPosition))
                {
                    var convertedTargetPositionForMatrixNotation = new Position(parsedInput.TargetPosition.Row - 1, parsedInput.TargetPosition.Col - 1);
                    currentField.Figure.Move(Board, currentField, convertedTargetPositionForMatrixNotation);

                    var oppKingField = currentField.Figure.GetOppositKing(Board);
                    CheckmateAnalysisResult = GameStateAnalyzer.AnalizeGameState(Board, oppKingField);

                    if (CheckmateAnalysisResult.IsInStalemate)
                    {
                        gameState.SetIsInStalemate();
                        return gameState;
                    }

                    ChangePlayer.TryGetValue(CurrentPlayer, out var currentPlayer);
                    CurrentPlayer = currentPlayer;
                    break;
                }
            }
            gameState.SetMoveIsValid();
            return gameState;
        }

        public ParseInputResult ParseMoveInput(string input)
        {
            if (!Regex.IsMatch(input, "^[a-h][1-8]-[a-h][1-8]$"))
                return new ParseInputResult(false);
            var positions = input.Split('-');
            var currentPosition = Board.CalculatePosition(positions[0]);
            var TargetPosition = Board.CalculatePosition(positions[1]);
            return new ParseInputResult(currentPosition, TargetPosition, true);
        }
    }
}
