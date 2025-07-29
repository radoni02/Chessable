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
    public class PawnTests
    {
        private readonly ChessboardPositions _chessboardPositions;
        public PawnTests()
        {
            _chessboardPositions = new ChessboardPositions();
        }

        [Fact]
        public void Pawn_WithLimitedMoves_ReturnsExpectedMoves()
        {
            // Arrange
            var chessboard = _chessboardPositions.SetPawnWithNonPossibleMoves();
            var pawn = chessboard.Board[5][5].Figure;
            var currentField = chessboard.Board[5][5];
            pawn.CalculateAtackedFields(chessboard, currentField);

            // Act
            pawn.CheckPossibleMoves(chessboard, currentField);
            var results = pawn.PossibleMoves;
            var targets = results.Select(r => r.TargetPosition.ToString());

            // Assert
            Assert.NotNull(pawn);
            Assert.IsType<Pawn>(pawn);
            Assert.True(pawn.IsWhite);
            Assert.NotNull(chessboard);
            Assert.NotNull(results);
            Assert.Equal(new HashSet<string>() { "75", "77" }, targets);
        }

        [Fact]
        public void Pawn_WithAllMovesPossible_MovesAsExpected()
        {
            // Arrange
            var chessboard = _chessboardPositions.SetPawnWithAllPossibleMoves();
            var pawn = chessboard.Board[6][5].Figure;
            var currentField = chessboard.Board[6][5];
            pawn.CalculateAtackedFields(chessboard, currentField);

            // Act
            pawn.CheckPossibleMoves(chessboard, currentField);
            var results = pawn.PossibleMoves;
            var targets = results.Select(r => r.TargetPosition.ToString());
            pawn.Move(chessboard, currentField, new Utils.Position(7, 5,Formatter.MatrixFormat));

            // Assert
            Assert.NotNull(pawn);
            Assert.IsType<Pawn>(pawn);
            Assert.True(pawn.IsWhite);
            Assert.NotNull(chessboard);
            Assert.NotNull(results);
            Assert.Equal(new HashSet<string>() { "86", "85", "87" }, targets);

        }

        [Fact]
        public void Pawn_CanCaptureEnemyPieces()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetDefaultPosition();
            var whitePawn = new Pawn(true, 1, "Pawn");
            chessboard.Board[4][3] = new Field(true, whitePawn, 5, 4);
            var blackPawn1 = new Pawn(false, 1, "Pawn");
            var blackPawn2 = new Pawn(false, 1, "Pawn");

            chessboard.Board[5][2] = new Field(true, blackPawn1, 6, 3);
            chessboard.Board[5][4] = new Field(true, blackPawn2, 6, 5);

            var currentField = chessboard.Board[4][3];
            whitePawn.CalculateAtackedFields(chessboard, currentField);

            // Act
            whitePawn.CheckPossibleMoves(chessboard, currentField);
            var results = whitePawn.PossibleMoves;
            var targets = results.Select(r => r.TargetPosition.ToString());

            // Assert
            Assert.NotNull(results);
            Assert.Contains("63", targets);
            Assert.Contains("65", targets);
            Assert.Contains("64", targets);
        }

        [Fact]
        public void Pawn_AtStartingPosition_CanMoveTwoSquares()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetDefaultPosition();
            var pawn = chessboard.Board[1][3].Figure;
            var currentField = chessboard.Board[1][3];
            pawn.CalculateAtackedFields(chessboard, currentField);

            // Act
            pawn.CheckPossibleMoves(chessboard, currentField);
            var results = pawn.PossibleMoves;
            var targets = results.Select(r => r.TargetPosition.ToString());

            // Assert
            Assert.NotNull(results);
            Assert.Contains("34", targets);
            Assert.Contains("44", targets);
            Assert.Equal(4, results.Count);
        }

        [Fact]
        public void Pawn_PromotesToQueen_WhenReachingEndRank()
        {
            // Arrange
            var chessboard = _chessboardPositions.SetPawnWithAllPossibleMoves();
            var pawn = chessboard.Board[6][5].Figure;
            var currentField = chessboard.Board[6][5];
            pawn.CalculateAtackedFields(chessboard, currentField);

            // Act
            pawn.CheckPossibleMoves(chessboard, currentField);
            var results = pawn.PossibleMoves;
            var targets = results.Select(r => r.TargetPosition.ToString());

            pawn.Move(chessboard, currentField, new Position(7, 5, Formatter.MatrixFormat));
            var promotedPiece = chessboard.Board[7][5].Figure;

            // Assert
            Assert.NotNull(results);
            Assert.Equal(new HashSet<string>() {"86","85", "87" }, targets);
            Assert.IsType<Queen>(promotedPiece);
            Assert.True(promotedPiece.IsWhite);
            Assert.Equal("Queen", promotedPiece.Name);
            Assert.Equal(9, promotedPiece.Value);
        }
    }
}
