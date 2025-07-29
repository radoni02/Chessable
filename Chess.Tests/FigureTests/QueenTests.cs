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
    public class QueenTests
    {
        private readonly ChessboardPositions _chessboardPositions;

        public QueenTests()
        {
            _chessboardPositions = new ChessboardPositions();
        }

        [Fact]
        public void Queen_NormalMove_ShouldMoveToTargetPosition()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new Queen(true, 10, "Queen"), 4, 4);
            var queenField = chessboard.Board[3][3];
            var targetPosition = new Position(3, 6, Formatter.MatrixFormat);

            // Act
            queenField.Figure.Move(chessboard, queenField, targetPosition);

            // Assert
            Assert.Null(chessboard.Board[3][3].Figure);
            Assert.False(chessboard.Board[3][3].IsUsed);
            Assert.NotNull(chessboard.Board[3][6].Figure);
            Assert.True(chessboard.Board[3][6].IsUsed);
            Assert.Equal("Queen", chessboard.Board[3][6].Figure.Name);
            Assert.Equal(1, chessboard.Board[3][6].Figure.MoveConut);
        }

        [Fact]
        public void Queen_CalculateAtackedFields_ShouldNotAttackBeyondObstacles()
        {
            // Arrange
            var chessboard = _chessboardPositions.GetEmptyBoard();
            chessboard.Board[3][3] = new Field(true, new Queen(true, 10, "Queen"), 4, 4);
            chessboard.Board[3][5] = new Field(true, new Pawn(true, 1, "Pawn"), 4, 6);
            chessboard.Board[5][5] = new Field(true, new Pawn(false, 1, "Pawn"), 6, 6);
            var queenField = chessboard.Board[3][3];

            // Act
            queenField.Figure.CalculateAtackedFields(chessboard, queenField);

            // Assert
            var attackedFields = queenField.Figure.AttackedFields;

            Assert.DoesNotContain(attackedFields, f => f.Row == 4 && f.Col == 7);
            Assert.DoesNotContain(attackedFields, f => f.Row == 4 && f.Col == 8);

            Assert.Contains(attackedFields, f => f.Row == 6 && f.Col == 6);
            Assert.DoesNotContain(attackedFields, f => f.Row == 7 && f.Col == 7);
            Assert.DoesNotContain(attackedFields, f => f.Row == 8 && f.Col == 8);
        }
    }
}
