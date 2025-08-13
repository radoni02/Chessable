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

        [Fact]
        public void Pawn_CanPerformEnPassant_WhenConditionsAreMet()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();

            var whitePawn = new Pawn(true, 1, "Pawn");
            chessboard.Board[4][4] = new Field(true, whitePawn, 5, 5); // e5

            var blackPawn = new Pawn(false, 1, "Pawn");
            chessboard.Board[4][5] = new Field(true, blackPawn, 5, 6); // f5

            var currentField = chessboard.Board[4][4];
            var lastMove = new PossibleMove(
                new Position(7, 6), // f7
                new Position(5, 6)  // f5
            );

            whitePawn.CalculateAtackedFields(chessboard, currentField);

            // Act
            whitePawn.CheckPossibleMoves(chessboard, currentField, true, lastMove);
            var results = whitePawn.PossibleMoves;
            var targets = results.Select(r => r.TargetPosition.ToString()).ToList();

            // Assert
            Assert.NotNull(results);
            Assert.Contains("66", targets);
            Assert.True(results.Any(m => m.TargetPosition.Row == 6 && m.TargetPosition.Col == 6));
        }

        [Fact]
        public void Pawn_CannotPerformEnPassant_WhenPassantDisabled()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();

            var whitePawn = new Pawn(true, 1, "Pawn");
            chessboard.Board[4][4] = new Field(true, whitePawn, 5, 5); // e5

            var blackPawn = new Pawn(false, 1, "Pawn");
            chessboard.Board[4][5] = new Field(true, blackPawn, 5, 6); // f5

            var currentField = chessboard.Board[4][4];
            var lastMove = new PossibleMove(
                new Position(7, 6), // f7
                new Position(5, 6)  // f5
            );

            whitePawn.CalculateAtackedFields(chessboard, currentField);

            // Act
            whitePawn.CheckPossibleMoves(chessboard, currentField, false, lastMove); 
            var results = whitePawn.PossibleMoves;
            var targets = results.Select(r => r.TargetPosition.ToString()).ToList();

            // Assert
            Assert.NotNull(results);
            Assert.DoesNotContain("66", targets);
        }

        [Fact]
        public void Pawn_CannotPerformEnPassant_WhenOpponentPawnNotAdjacentHorizontally()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();

            var whitePawn = new Pawn(true, 1, "Pawn");
            chessboard.Board[4][4] = new Field(true, whitePawn, 5, 5); // e5

            var blackPawn = new Pawn(false, 1, "Pawn");
            chessboard.Board[4][6] = new Field(true, blackPawn, 5, 7); // g5 (not adjacent)

            var currentField = chessboard.Board[4][4];
            var lastMove = new PossibleMove(
                new Position(7, 7), // g7
                new Position(5, 7)  // g5
            );

            whitePawn.CalculateAtackedFields(chessboard, currentField);

            // Act
            whitePawn.CheckPossibleMoves(chessboard, currentField, true, lastMove);
            var results = whitePawn.PossibleMoves;
            var targets = results.Select(r => r.TargetPosition.ToString()).ToList();

            // Assert
            Assert.NotNull(results);
            Assert.DoesNotContain("67", targets);
        }

        [Fact]
        public void Pawn_CannotPerformEnPassant_WhenPawnsNotOnSameRow()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();

            var whitePawn = new Pawn(true, 1, "Pawn");
            chessboard.Board[3][4] = new Field(true, whitePawn, 4, 5); // e4

            var blackPawn = new Pawn(false, 1, "Pawn");
            chessboard.Board[4][5] = new Field(true, blackPawn, 5, 6); // f5 (different row)

            var currentField = chessboard.Board[3][4];
            var lastMove = new PossibleMove(
                new Position(7, 6), // f7
                new Position(5, 6)  // f5
            );

            whitePawn.CalculateAtackedFields(chessboard, currentField);

            // Act
            whitePawn.CheckPossibleMoves(chessboard, currentField, true, lastMove);
            var results = whitePawn.PossibleMoves;
            var targets = results.Select(r => r.TargetPosition.ToString()).ToList();

            // Assert
            Assert.NotNull(results);
            Assert.DoesNotContain("66", targets);
        }

        [Theory]
        [InlineData(5, 5, 5, 6, 6, 6)] // White pawn e5, black f5 -> en passant to f6
        [InlineData(5, 5, 5, 4, 6, 4)] // White pawn e5, black d5 -> en passant to d6
        [InlineData(4, 4, 4, 5, 3, 5)] // Black pawn d4, white e4 -> en passant to e3
        [InlineData(4, 6, 4, 5, 3, 5)] // Black pawn f4, white e4 -> en passant to e3
        public void Pawn_EnPassantTargetSquare_CalculatedCorrectly(
        int pawnRow, int pawnCol,
        int opponentRow, int opponentCol,
        int expectedTargetRow, int expectedTargetCol)
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();

            bool isWhitePawn = pawnRow == 5; 
            var pawn = new Pawn(isWhitePawn, 1, "Pawn");
            var opponentPawn = new Pawn(!isWhitePawn, 1, "Pawn");

            chessboard.Board[pawnRow - 1][pawnCol - 1] = new Field(true, pawn, pawnRow, pawnCol);
            chessboard.Board[opponentRow - 1][opponentCol - 1] = new Field(true, opponentPawn, opponentRow, opponentCol);

            var currentField = chessboard.Board[pawnRow - 1][pawnCol - 1];

            var fromRow =  isWhitePawn ? opponentRow + 2 : opponentRow - 2;
            var lastMove = new PossibleMove(
                new Position(fromRow, opponentCol),
                new Position(opponentRow, opponentCol)
            );

            pawn.CalculateAtackedFields(chessboard, currentField);

            // Act
            pawn.CheckPossibleMoves(chessboard, currentField, true, lastMove);
            var results = pawn.PossibleMoves;
            var enPassantMove = results.FirstOrDefault(m =>
                m.TargetPosition.Row == expectedTargetRow &&
                m.TargetPosition.Col == expectedTargetCol);

            // Assert
            Assert.NotNull(enPassantMove);
            Assert.Equal(expectedTargetRow, enPassantMove.TargetPosition.Row);
            Assert.Equal(expectedTargetCol, enPassantMove.TargetPosition.Col);
        }

        [Fact]
        public void Pawn_ExecutesEnPassantMove_AndRemovesOpponentPawn()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();

            var whitePawn = new Pawn(true, 1, "Pawn");
            chessboard.Board[4][4] = new Field(true, whitePawn, 5, 5); // e5

            var blackPawn = new Pawn(false, 1, "Pawn");
            chessboard.Board[4][5] = new Field(true, blackPawn, 5, 6); // f5

            var currentField = chessboard.Board[4][4];
            var lastMove = new PossibleMove(
                new Position(7, 6), // f7
                new Position(5, 6)  // f5
            );

            whitePawn.CalculateAtackedFields(chessboard, currentField);
            whitePawn.CheckPossibleMoves(chessboard, currentField, true, lastMove);

            // Act
            whitePawn.Move(chessboard, currentField, new Position(5, 5, Formatter.MatrixFormat)); // f6

            // Assert
            Assert.Equal(whitePawn, chessboard.Board[5][5].Figure); // White pawn moved to f6
            Assert.True(chessboard.Board[5][5].IsUsed);
            Assert.False(chessboard.Board[4][4].IsUsed); // Original position is empty
            Assert.Null(chessboard.Board[4][4].Figure);
            Assert.Null(chessboard.Board[4][5].Figure); // Opponent pawn removed
            Assert.False(chessboard.Board[4][5].IsUsed);
        }
    }
}
