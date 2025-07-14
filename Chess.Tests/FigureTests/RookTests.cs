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
    public class RookTests
    {
        private readonly ChessboardPositions _chessboardPositions;

        public RookTests()
        {
            _chessboardPositions = new ChessboardPositions();
        }

        [Fact]
        public void Rook_NormalMove_ShouldMoveToTargetPosition()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new Rook(true, 5, "Rook"), 4, 4);
            var rookField = chessboard.Board[3][3];
            var targetPosition = new Position(3, 6);

            // Act
            rookField.Figure.Move(chessboard, rookField, targetPosition);

            // Assert
            Assert.Null(chessboard.Board[3][3].Figure);
            Assert.False(chessboard.Board[3][3].IsUsed);
            Assert.NotNull(chessboard.Board[3][6].Figure);
            Assert.True(chessboard.Board[3][6].IsUsed);
            Assert.Equal("Rook", chessboard.Board[3][6].Figure.Name);
            Assert.Equal(1, chessboard.Board[3][6].Figure.MoveConut);
        }

        [Fact]
        public void Rook_CalculateAtackedFields_ShouldNotAttackBeyondObstacles()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new Rook(true, 5, "Rook"), 4, 4);
            chessboard.Board[3][5] = new Field(true, new Pawn(true, 1, "Pawn"), 4, 6);
            chessboard.Board[5][3] = new Field(true, new Pawn(false, 1, "Pawn"), 6, 4);
            var rookField = chessboard.Board[3][3];

            // Act
            rookField.Figure.CalculateAtackedFields(chessboard, rookField);

            // Assert
            var attackedFields = rookField.Figure.AttackedFields;

            Assert.DoesNotContain(attackedFields, f => f.Row == 4 && f.Col == 6);
            Assert.DoesNotContain(attackedFields, f => f.Row == 4 && f.Col == 7);
            Assert.DoesNotContain(attackedFields, f => f.Row == 4 && f.Col == 8);

            // Should attack enemy piece but not beyond it vertically
            Assert.Contains(attackedFields, f => f.Row == 6 && f.Col == 4);
            Assert.DoesNotContain(attackedFields, f => f.Row == 7 && f.Col == 4);
            Assert.DoesNotContain(attackedFields, f => f.Row == 8 && f.Col == 4);
        }

        [Fact]
        public void Rook_PossibleMoves_ShouldReturnValidMoves()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new Rook(true, 5, "Rook"), 4, 4);
            var rookField = chessboard.Board[3][3];

            // Act
            rookField.Figure.CalculatePossibleMoves(chessboard, rookField);
            var results = rookField.Figure.PossibleMoves;
            var targets = results.Select(r => r.TargetPosition.ToString());

            // Assert
            Assert.Contains("41", targets);
            Assert.Contains("47", targets);
            Assert.Contains("24", targets);
            Assert.Contains("64", targets);

            // Should not contain diagonal moves
            Assert.DoesNotContain("33", targets);
            Assert.DoesNotContain("44", targets);
        }
    }
}
