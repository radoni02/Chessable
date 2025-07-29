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
    public class KnightTests
    {
        private readonly ChessboardPositions _chessboardPositions;

        public KnightTests()
        {
            _chessboardPositions = new ChessboardPositions();
        }

        [Fact]
        public void Knight_NormalMove_ShouldMoveToTargetPosition()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new Knight(true, 3, "Knight"), 4, 4);
            var knightField = chessboard.Board[3][3];
            var targetPosition = new Position(5, 4, Formatter.MatrixFormat);

            // Act
            knightField.Figure.Move(chessboard, knightField, targetPosition);

            // Assert
            Assert.Null(chessboard.Board[3][3].Figure);
            Assert.False(chessboard.Board[3][3].IsUsed);
            Assert.NotNull(chessboard.Board[5][4].Figure);
            Assert.True(chessboard.Board[5][4].IsUsed);
            Assert.Equal("Knight", chessboard.Board[5][4].Figure.Name);
            Assert.Equal(1, chessboard.Board[5][4].Figure.MoveConut);
        }

        [Fact]
        public void Knight_CenterPosition_ShouldHaveEightPossibleMoves()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new Knight(true, 3, "Knight"), 4, 4);
            var knightField = chessboard.Board[3][3];
            knightField.Figure.CalculateAtackedFields(chessboard, knightField);

            // Act
            knightField.Figure.CheckPossibleMoves(chessboard, knightField);
            var results = knightField.Figure.PossibleMoves;
            // Assert
            Assert.NotNull(results);
            Assert.Equal(8, results.Count);

            var expectedMoves = new HashSet<string> { "23", "25", "32", "36", "52", "56", "63", "65" };
            Assert.Equal(expectedMoves, results.Select(r => r.TargetPosition.ToString()));
        }

        [Fact]
        public void Knight_CornerPosition_ShouldHaveLimitedMoves()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[0][0] = new Field(true, new Knight(true, 3, "Knight"), 1, 1);
            var knightField = chessboard.Board[0][0];
            knightField.Figure.CalculateAtackedFields(chessboard, knightField);

            // Act
            knightField.Figure.CheckPossibleMoves(chessboard, knightField);
            var results = knightField.Figure.PossibleMoves;

            // Assert
            Assert.NotNull(results);
            Assert.Equal(2, results.Count);

            var expectedMoves = new HashSet<string> { "23", "32" };
            Assert.Equal(expectedMoves, results.Select(r => r.TargetPosition.ToString()));
        }

        [Fact]
        public void Knight_CanJumpOverPieces()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new Knight(true, 3, "Knight"), 4, 4);

            chessboard.Board[2][3] = new Field(true, new Pawn(true, 1, "Pawn"), 3, 4);
            chessboard.Board[3][2] = new Field(true, new Pawn(true, 1, "Pawn"), 4, 3);
            chessboard.Board[3][4] = new Field(true, new Pawn(true, 1, "Pawn"), 4, 5);
            chessboard.Board[4][3] = new Field(true, new Pawn(true, 1, "Pawn"), 5, 4);

            var knightField = chessboard.Board[3][3];
            knightField.Figure.CalculateAtackedFields(chessboard, knightField);

            // Act
            knightField.Figure.CheckPossibleMoves(chessboard, knightField);
            var results = knightField.Figure.PossibleMoves;

            // Assert
            Assert.NotNull(results);
            Assert.Equal(8, results.Count);
        }

        [Fact]
        public void Knight_CannotCaptureOwnPieces()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new Knight(true, 3, "Knight"), 4, 4);

            chessboard.Board[1][2] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 3);
            chessboard.Board[1][4] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 5);

            var knightField = chessboard.Board[3][3];
            knightField.Figure.CalculateAtackedFields(chessboard, knightField);

            // Act
            knightField.Figure.CheckPossibleMoves(chessboard, knightField);
            var results = knightField.Figure.PossibleMoves;
            var targets = results.Select(r => r.TargetPosition.ToString());

            // Assert
            Assert.NotNull(results);
            Assert.Equal(6, results.Count);

            Assert.DoesNotContain("24", targets);
            Assert.DoesNotContain("25", targets);
        }

        [Fact]
        public void Knight_CanCaptureEnemyPieces()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new Knight(true, 3, "Knight"), 4, 4);

            chessboard.Board[1][2] = new Field(true, new Pawn(false, 1, "Pawn"), 2, 3);
            chessboard.Board[1][4] = new Field(true, new Pawn(false, 1, "Pawn"), 2, 5);
            chessboard.Board[5][2] = new Field(true, new Pawn(false, 1, "Pawn"), 6, 3);

            var knightField = chessboard.Board[3][3];
            knightField.Figure.CalculateAtackedFields(chessboard, knightField);

            // Act
            knightField.Figure.CheckPossibleMoves(chessboard, knightField);
            var results = knightField.Figure.PossibleMoves;
            var targets = results.Select(r => r.TargetPosition.ToString());

            // Assert
            Assert.NotNull(results);
            Assert.Equal(8, results.Count);
            Assert.Contains("23", targets);
            Assert.Contains("25", targets);
            Assert.Contains("63", targets);
        }

        [Fact]
        public void Knight_AtEdgeOfBoard_ShouldHaveCorrectMoves()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[0][3] = new Field(true, new Knight(true, 3, "Knight"), 1, 4);
            var knightField = chessboard.Board[0][3];
            knightField.Figure.CalculateAtackedFields(chessboard, knightField);

            // Act
            knightField.Figure.CheckPossibleMoves(chessboard, knightField);
            var results = knightField.Figure.PossibleMoves;

            // Assert
            Assert.NotNull(results);
            Assert.Equal(4, results.Count);

            var expectedMoves = new HashSet<string> { "22", "26", "33", "35" };
            Assert.Equal(expectedMoves, results.Select(r => r.TargetPosition.ToString()));
        }
    }
}
