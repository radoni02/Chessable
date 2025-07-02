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
    public class KingTests
    {
        private readonly ChessboardPositions _chessboardPositions;
        public KingTests()
        {
            _chessboardPositions = new ChessboardPositions();
        }

        [Fact]
        public void King_NormalMove_ShouldMoveOneSquare()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new King(true, 1000, "King"), 4, 4);
            var kingField = chessboard.Board[3][3];
            var targetPosition = new Position(3, 4);

            // Act
            kingField.Figure.Move(chessboard, kingField, targetPosition);

            // Assert
            Assert.Null(chessboard.Board[3][3].Figure);
            Assert.False(chessboard.Board[3][3].IsUsed);
            Assert.NotNull(chessboard.Board[3][4].Figure);
            Assert.True(chessboard.Board[3][4].IsUsed);
            Assert.Equal("King", chessboard.Board[3][4].Figure.Name);
            Assert.Equal(1, chessboard.Board[3][4].Figure.MoveConut);
        }

        [Fact]
        public void King_CastlingMove_ShouldMoveBothKingAndRook()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5);
            chessboard.Board[0][7] = new Field(true, new Rook(true, 5, "Rook"), 1, 8);
            var kingField = chessboard.Board[0][4];
            var targetPosition = new Position(0, 6);

            // Act
            kingField.Figure.Move(chessboard, kingField, targetPosition);

            // Assert
            Assert.Null(chessboard.Board[0][4].Figure);
            Assert.NotNull(chessboard.Board[0][6].Figure);
            Assert.Equal("King", chessboard.Board[0][6].Figure.Name);

            Assert.Null(chessboard.Board[0][7].Figure);
            Assert.NotNull(chessboard.Board[0][5].Figure);
            Assert.Equal("Rook", chessboard.Board[0][5].Figure.Name);
        }

        [Fact]
        public void King_CalculateAtackedFields_ShouldNotAttackOwnPieces()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new King(true, 1000, "King"), 4, 4);
            chessboard.Board[3][4] = new Field(true, new Pawn(true, 1, "Pawn"), 4, 5);
            chessboard.Board[4][4] = new Field(true, new Pawn(false, 1, "Pawn"), 5, 5);
            var kingField = chessboard.Board[3][3];

            // Act
            kingField.Figure.CalculateAtackedFields(chessboard, kingField);

            // Assert
            var attackedFields = kingField.Figure.AttackedFields;

            Assert.DoesNotContain(attackedFields, f => f.Row == 4 && f.Col == 5);
            Assert.Contains(attackedFields, f => f.Row == 5 && f.Col == 5);
        }

        [Fact]
        public void King_PossibleMoves_ShouldIncludeCastling()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5);
            chessboard.Board[0][0] = new Field(true, new Rook(true, 5, "Rook"), 1, 1);
            chessboard.Board[0][7] = new Field(true, new Rook(true, 5, "Rook"), 1, 8);
            var kingField = chessboard.Board[0][4];

            foreach (var field in chessboard.Board.SelectMany(f => f).Where(f => f.Figure != null))
            {
                field.Figure.CalculateAtackedFields(chessboard, field);
            }

            // Act
            var possibleMoves = kingField.Figure.CalculatePossibleMoves(chessboard, kingField);

            // Assert
            Assert.Contains("02", possibleMoves);
            Assert.Contains("06", possibleMoves);
        }

        [Fact]
        public void King_PossibleMoves_ShouldNotIncludeCastlingAfterKingMoved()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            var king = new King(true, 1000, "King");
            king.MoveConut = 1;
            chessboard.Board[0][4] = new Field(true, king, 1, 5);
            chessboard.Board[0][0] = new Field(true, new Rook(true, 5, "Rook"), 1, 1);
            chessboard.Board[0][7] = new Field(true, new Rook(true, 5, "Rook"), 1, 8);
            var kingField = chessboard.Board[0][4];

            foreach (var field in chessboard.Board.SelectMany(f => f).Where(f => f.Figure != null))
            {
                field.Figure.CalculateAtackedFields(chessboard, field);
            }

            // Act
            var possibleMoves = kingField.Figure.CalculatePossibleMoves(chessboard, kingField);

            // Assert
            Assert.DoesNotContain("02", possibleMoves);
            Assert.DoesNotContain("06", possibleMoves);
        }

        [Fact]
        public void King_PossibleMoves_ShouldExcludeForbiddenFields()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new King(true, 1000, "King"), 4, 4);
            chessboard.Board[1][3] = new Field(true, new Rook(false, 5, "Rook"), 2, 4);
            var kingField = chessboard.Board[3][3];

            foreach (var field in chessboard.Board.SelectMany(f => f).Where(f => f.Figure != null))
            {
                field.Figure.CalculateAtackedFields(chessboard, field);
            }

            // Act
            var possibleMoves = kingField.Figure.CalculatePossibleMoves(chessboard, kingField);

            // Assert
            Assert.DoesNotContain("23", possibleMoves); 
            Assert.DoesNotContain("43", possibleMoves);
        }
    }
}
