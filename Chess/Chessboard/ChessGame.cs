﻿using Chess.Utils;
using Chess.Utils.ChessPlayer;
using Chess.Utils.Notations.FEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chess.Chessboard
{
    internal sealed class ChessGame
    {
        public uint FullMoveCounter { get; set; }
        public Player CurrentPlayer {  get; set; }
        public Checkerboard Board { get; set; }
        public CheckmateAnalysisResult CheckmateAnalysisResult { get; set; }
        public Dictionary<Player, Player> ChangePlayer { get; set; }
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

            var possibleMoves = CheckPossibleMoves(currentField);

            foreach (var possibleMove in possibleMoves)
            {
                if (possibleMove.TargetPosition.Equals(parsedInput.TargetPosition))
                {
                    var convertedTargetPositionForMatrixNotation = parsedInput.TargetPosition.SwitchFormat();
                    currentField.Figure.Move(Board, currentField, convertedTargetPositionForMatrixNotation);

                    var oppKingField = currentField.Figure.GetOppositKing(Board);
                    CheckmateAnalysisResult = GameStateAnalyzer.AnalizeGameState(Board, oppKingField);

                    if(CheckmateAnalysisResult.IsInStalemate)
                    {
                        gameState.SetIsInStalemate();
                        return gameState;
                    }

                    if(CheckmateAnalysisResult.IsCheckmate)
                    {
                        gameState.SetIsInCheckmate();
                        return gameState;
                    }

                    SwitchPlayer();
                    break;
                }
            }
            gameState.SetMoveIsValid();
            return gameState;
        }

        private void SwitchPlayer()
        {
            if(CurrentPlayer.Color == PlayerColor.Black)
            {
                FullMoveCounter++;
            }
            ChangePlayer.TryGetValue(CurrentPlayer, out var nextPlayer);
            CurrentPlayer = nextPlayer;
        }

        private IEnumerable<PossibleMove> CheckPossibleMoves(Field currentField)
        {
            var possibleMoves = new List<PossibleMove>();
            if (CheckmateAnalysisResult.IsInCheck)
            {
                possibleMoves.AddRange(CheckmateAnalysisResult.PossibleCaptureRescues);
                possibleMoves.AddRange(CheckmateAnalysisResult.PossibleBlockingMoves);
            }
            currentField.Figure.CheckPossibleMoves(Board, currentField);
            possibleMoves.AddRange(currentField.Figure.PossibleMoves);
            return possibleMoves;
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
