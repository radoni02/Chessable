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
            var results = pawn.PossibleMoves(chessboard, currentField);

            // Assert
            Assert.NotNull(pawn);
            Assert.IsType<Pawn>(pawn);
            Assert.True(pawn.IsWhite);
            Assert.NotNull(chessboard);
            Assert.NotNull(results);
            Assert.Equal(new HashSet<string>() { "64", "66" }, results);
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
            var results = pawn.PossibleMoves(chessboard, currentField);
            pawn.Move(chessboard,currentField,new Utils.Position(7,5));

            // Assert
            Assert.NotNull(pawn);
            Assert.IsType<Pawn>(pawn);
            Assert.True(pawn.IsWhite);
            Assert.NotNull(chessboard);
            Assert.NotNull(results);
            Assert.Equal(new HashSet<string>() { "74", "75","76" }, results);
            
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
            var results = whitePawn.PossibleMoves(chessboard, currentField);

            // Assert
            Assert.NotNull(results);
            Assert.Contains("52", results);
            Assert.Contains("54", results);
            Assert.Contains("53", results);
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
            var results = pawn.PossibleMoves(chessboard, currentField);

            // Assert
            Assert.NotNull(results);
            Assert.Contains("23", results);
            Assert.Contains("33", results);
            Assert.Equal(2, results.Count);
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
            var results = pawn.PossibleMoves(chessboard, currentField);
            var originalPawnType = pawn.GetType();

            pawn.Move(chessboard, currentField, new Position(7, 5));
            var promotedPiece = chessboard.Board[7][5].Figure;

            // Assert
            Assert.NotNull(results);
            Assert.Equal(new HashSet<string>() { "74", "75", "76" }, results);
            Assert.IsType<Queen>(promotedPiece);
            Assert.True(promotedPiece.IsWhite);
            Assert.Equal("Queen", promotedPiece.Name);
            Assert.Equal(9, promotedPiece.Value);
        }
    }
}
