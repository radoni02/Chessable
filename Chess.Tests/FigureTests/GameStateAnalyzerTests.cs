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
            var result = GameStateAnalyzer.AnalyzeForCheckmate(board, kingField);

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
            var result = GameStateAnalyzer.AnalyzeForCheckmate(board, wrongKingField);

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
            var KingField = board.Board[0][6];
            foreach (var field in board.Board.SelectMany(f => f).Where(f => f.Figure != null))
            {
                field.Figure.CalculateAtackedFields(board, field);
            }
            //Act
            var result = GameStateAnalyzer.AnalyzeForCheckmate(board, KingField);

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
            var KingField = board.Board[0][6];
            foreach (var field in board.Board.SelectMany(f => f).Where(f => f.Figure != null))
            {
                field.Figure.CalculateAtackedFields(board, field);
            }
            //Act
            var result = GameStateAnalyzer.AnalyzeForCheckmate(board, KingField);

            //Assert
            Assert.True(result.IsInCheck);
            Assert.False(result.IsCheckmate);
            Assert.False(result.WrongFigureSelected);
            Assert.Empty(result.PossibleBlockingMoves);
            Assert.Empty(result.PossibleKingMoves);
            Assert.Contains<string>("15-16", result.PossibleCaptureRescues);
        }
    }
}
