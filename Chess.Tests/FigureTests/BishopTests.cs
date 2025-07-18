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
    public class BishopTests
    {
        private readonly ChessboardPositions _chessboardPositions;

        public BishopTests()
        {
            _chessboardPositions = new ChessboardPositions();
        }

        [Fact]
        public void Bishop_NormalMove_ShouldMoveToTargetPosition()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new Bishop(true, 3, "Bishop"), 4, 4);
            var bishopField = chessboard.Board[3][3];
            var targetPosition = new Position(5, 5);

            // Act
            bishopField.Figure.Move(chessboard, bishopField, targetPosition);

            // Assert
            Assert.Null(chessboard.Board[3][3].Figure);
            Assert.False(chessboard.Board[3][3].IsUsed);
            Assert.NotNull(chessboard.Board[5][5].Figure);
            Assert.True(chessboard.Board[5][5].IsUsed);
            Assert.Equal("Bishop", chessboard.Board[5][5].Figure.Name);
            Assert.Equal(1, chessboard.Board[5][5].Figure.MoveConut);
        }

        [Fact]
        public void Bishop_CalculateAtackedFields_ShouldNotAttackBeyondObstacles()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new Bishop(true, 3, "Bishop"), 4, 4);
            chessboard.Board[5][5] = new Field(true, new Pawn(true, 1, "Pawn"), 6, 6);
            chessboard.Board[1][5] = new Field(true, new Pawn(false, 1, "Pawn"), 2, 6);
            var bishopField = chessboard.Board[3][3];

            // Act
            bishopField.Figure.CalculateAtackedFields(chessboard, bishopField);

            // Assert
            var attackedFields = bishopField.Figure.AttackedFields;

            Assert.Contains(attackedFields, f => f.Row == 5 && f.Col == 5);
            Assert.Contains(attackedFields, f => f.Row == 6 && f.Col == 6);//corrent implementation
            Assert.DoesNotContain(attackedFields, f => f.Row == 7 && f.Col == 7);
            Assert.DoesNotContain(attackedFields, f => f.Row == 8 && f.Col == 8);

            Assert.Contains(attackedFields, f => f.Row == 2 && f.Col == 6);
            Assert.DoesNotContain(attackedFields, f => f.Row == 1 && f.Col == 7);
        }

        [Fact]
        public void Bishop_CalculatePossibleMoves_CenterOfBoard_ShouldHaveMaximumMobility()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new Bishop(true, 3, "Bishop"), 4, 4);
            var bishopField = chessboard.Board[3][3];

            // Act
            bishopField.Figure.CheckPossibleMoves(chessboard, bishopField);

            // Assert
            var possibleMoves = bishopField.Figure.PossibleMoves;

            Assert.Equal(13, possibleMoves.Count);

            Assert.Contains(possibleMoves, m => m.TargetPosition.Row == 1 && m.TargetPosition.Col == 1);
            Assert.Contains(possibleMoves, m => m.TargetPosition.Row == 8 && m.TargetPosition.Col == 8);
            Assert.Contains(possibleMoves, m => m.TargetPosition.Row == 1 && m.TargetPosition.Col == 7);
            Assert.Contains(possibleMoves, m => m.TargetPosition.Row == 7 && m.TargetPosition.Col == 1);
        }

        [Fact]
        public void Bishop_CalculatePossibleMoves_CornerPosition_ShouldHaveLimitedMobility()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[0][0] = new Field(true, new Bishop(true, 3, "Bishop"), 1, 1);
            var bishopField = chessboard.Board[0][0];

            // Act
            bishopField.Figure.CheckPossibleMoves(chessboard, bishopField);

            // Assert
            var possibleMoves = bishopField.Figure.PossibleMoves;

            Assert.Equal(7, possibleMoves.Count);

            Assert.All(possibleMoves, move =>
                Assert.Equal(move.TargetPosition.Row, move.TargetPosition.Col));
        }

        [Fact]
        public void Bishop_CalculatePossibleMoves_WithObstacles_ShouldBeBlocked()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new Bishop(true, 3, "Bishop"), 4, 4);
            chessboard.Board[5][5] = new Field(true, new Pawn(true, 1, "Pawn"), 6, 6);
            chessboard.Board[1][1] = new Field(true, new Pawn(false, 1, "Pawn"), 2, 2);
            var bishopField = chessboard.Board[3][3];

            // Act
            bishopField.Figure.CheckPossibleMoves(chessboard, bishopField);

            // Assert
            var possibleMoves = bishopField.Figure.PossibleMoves;

            Assert.DoesNotContain(possibleMoves, m => m.TargetPosition.Row == 6 && m.TargetPosition.Col == 6);
            Assert.DoesNotContain(possibleMoves, m => m.TargetPosition.Row == 7 && m.TargetPosition.Col == 7);

            Assert.Contains(possibleMoves, m => m.TargetPosition.Row == 2 && m.TargetPosition.Col == 2);

            Assert.DoesNotContain(possibleMoves, m => m.TargetPosition.Row == 1 && m.TargetPosition.Col == 1);
        }
    }
}
