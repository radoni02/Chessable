using Chess.Chessboard;
using Chess.Figures.Abstractions;
using Chess.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Tests.SetupTests;

namespace Chess.Tests.FigureTests.AbstractionsFigureTests
{
    public class StraightFigureMovmentTests
    {
        private readonly ChessboardPositions _chessboardPositions;

        public StraightFigureMovmentTests()
        {
            _chessboardPositions = new ChessboardPositions();
        }

        [Fact]
        public void GetRowRightFields_ShouldAttackThroughEnemyKing()
        {
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            var rook = new Rook(false,5,"Rook");
            var a2 = new Field(true, rook, 2, 1);

            var king = new King(true,1000,"King");
            var c2 = new Field(true, king, 2, 3);

            var d2 = new Field(false, null, 2, 4);

            checkerboard.Board[1][0] = a2;
            checkerboard.Board[1][2] = c2;
            checkerboard.Board[1][3] = d2;

            var row = new List<Field> { a2, new Field(false, null, 2, 2), c2, d2 };

            // Act
            var movement = new StraightFigureMovment();
            var result = movement.GetRowRightFields(checkerboard, a2, checkerboard.Board[1]);

            // Assert
            Assert.Contains(d2, result.AtackedFields);
        }

        [Fact]
        public void GetRowRightFields_ShouldNotAttackThroughOwnKing()
        {
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            var rook = new Rook(false, 5, "Rook");
            var a2 = new Field(true, rook, 2, 1);

            var king = new King(false, 1000, "King");
            var c2 = new Field(true, king, 2, 3);

            var d2 = new Field(false, null, 2, 4);

            checkerboard.Board[1][0] = a2;
            checkerboard.Board[1][2] = c2;
            checkerboard.Board[1][3] = d2;

            var row = new List<Field> { a2, new Field(false, null, 2, 2), c2, d2 };

            // Act
            var movement = new StraightFigureMovment();
            var result = movement.GetRowRightFields(checkerboard, a2, checkerboard.Board[1]);

            // Assert
            Assert.DoesNotContain(d2, result.AtackedFields);
        }
    }
}
