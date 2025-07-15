using Chess.Chessboard;
using Chess.Figures;
using Chess.Tests.SetupTests;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Tests.FigureTests
{
    public class GameStateAnalyzerTests
    {
        private readonly ChessboardPositions _chessboardPositions;

        public GameStateAnalyzerTests()
        {
            _chessboardPositions = new ChessboardPositions();
        }

        [Fact]
        public void AnalyzeForCheckmate_ShouldReturns_IsNeitherInCheckNeitherInChekmate()
        {
            //Arange
            var board = _chessboardPositions.GetMidGamePosition();
            var kingField = board.Board[0][4];
            foreach (var field in board.Board.SelectMany(f => f).Where(f => f.Figure != null))
            {
                field.Figure.CalculateAtackedFields(board, field);
            }

            //Act
            var result = GameStateAnalyzer.AnalizeGameState(board, kingField);

            //Assert
            Assert.False(result.IsInCheck);
            Assert.False(result.IsCheckmate);
            Assert.False(result.WrongFigureSelected);
            Assert.Empty(result.PossibleBlockingMoves);
            Assert.Empty(result.PossibleKingMoves);
            Assert.Empty(result.PossibleCaptureRescues);
        }

        [Fact]
        public void AnalyzeForCheckmate_ShouldReturns_TrueThatWrongFigureIsSelected()
        {
            //Arange
            var board = _chessboardPositions.GetCheckmatePosition();
            var wrongKingField = board.Board[1][5];
            foreach (var field in board.Board.SelectMany(f => f).Where(f => f.Figure != null))
            {
                field.Figure.CalculateAtackedFields(board, field);
            }

            //Act
            var result = GameStateAnalyzer.AnalizeGameState(board, wrongKingField);

            //Assert
            Assert.False(result.IsInCheck);
            Assert.False(result.IsCheckmate);
            Assert.True(result.WrongFigureSelected);
            Assert.Empty(result.PossibleBlockingMoves);
            Assert.Empty(result.PossibleKingMoves);
            Assert.Empty(result.PossibleCaptureRescues);
        }

        [Fact]
        public void AnalyzeForCheckmate_ShouldReturns_CheckmateIsTrue()
        {
            //Arange
            var board = _chessboardPositions.GetCheckmatePosition();
            var kingField = board.Board[0][6];
            foreach (var field in board.Board.SelectMany(f => f).Where(f => f.Figure != null))
            {
                field.Figure.CalculateAtackedFields(board, field);
            }
            //Act
            var result = GameStateAnalyzer.AnalizeGameState(board, kingField);

            //Assert
            Assert.True(result.IsInCheck);
            Assert.True(result.IsCheckmate);
            Assert.False(result.WrongFigureSelected);
            Assert.Empty(result.PossibleBlockingMoves);
            Assert.Empty(result.PossibleKingMoves);
            Assert.Empty(result.PossibleCaptureRescues);
        }

        [Fact]
        public void AnalyzeForCheckmate_ShouldReturns_CheckmateIsFalse()
        {
            //Arange
            var board = _chessboardPositions.GetCheckmatePosition();
            board.Board[0][4] = new Field(true, new Queen(true, 10, "Queen"), 1, 5);
            var kingField = board.Board[0][6];
            foreach (var field in board.Board.SelectMany(f => f).Where(f => f.Figure != null))
            {
                field.Figure.CalculateAtackedFields(board, field);
            }
            var expectedResult = new PossibleMove(new Position(1, 5), new Position(1, 6));

            //Act
            var result = GameStateAnalyzer.AnalizeGameState(board, kingField);

            //Assert
            Assert.True(result.IsInCheck);
            Assert.False(result.IsCheckmate);
            Assert.False(result.WrongFigureSelected);
            Assert.Empty(result.PossibleBlockingMoves);
            Assert.Empty(result.PossibleKingMoves);
            Assert.Contains<PossibleMove>(expectedResult, result.PossibleCaptureRescues);
        }

        [Fact]
        public void AnalyzeForCheckmate_ShouldReturns_KingHasValidMoves()
        {
            //Arange
            var board = _chessboardPositions.GetCheckPosition();
            var kingField = board.Board[0][6];
            foreach (var field in board.Board.SelectMany(f => f).Where(f => f.Figure != null))
            {
                field.Figure.CalculateAtackedFields(board, field);
            }
            var expectedResult = new PossibleMove(new Position(1,7), new Position(2, 8));

            //Act
            var result = GameStateAnalyzer.AnalizeGameState(board, kingField);

            //Assert
            Assert.True(result.IsInCheck);
            Assert.False(result.IsCheckmate);
            Assert.False(result.WrongFigureSelected);
            Assert.Empty(result.PossibleBlockingMoves);
            Assert.Empty(result.PossibleCaptureRescues);
            Assert.Contains<PossibleMove>(expectedResult, result.PossibleKingMoves);
        }

        [Fact]
        public void AnalyzeForCheckmate_ShouldReturns_BlockingMovesAreAvaiable()
        {
            //Arange
            var board = _chessboardPositions.GetBlockingMovesPosition();
            var kingField = board.Board[0][6];
            foreach (var field in board.Board.SelectMany(f => f).Where(f => f.Figure != null))
            {
                field.Figure.CalculateAtackedFields(board, field);
            }
            var firstExpectedResult = new PossibleMove(new Position(3, 3), new Position(1, 3));
            var secondExpectedResult = new PossibleMove(new Position(3, 5), new Position(1, 5));

            //Act
            var result = GameStateAnalyzer.AnalizeGameState(board, kingField);

            //Assert
            Assert.True(result.IsInCheck);
            Assert.False(result.IsCheckmate);
            Assert.False(result.WrongFigureSelected);
            Assert.Empty(result.PossibleCaptureRescues);
            Assert.Contains<PossibleMove>(firstExpectedResult, result.PossibleBlockingMoves);
            Assert.Contains<PossibleMove>(secondExpectedResult, result.PossibleBlockingMoves);
        }
    }
}
