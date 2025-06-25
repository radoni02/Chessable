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
    }
}
