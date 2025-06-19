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
    public class FigureTests
    {
        private readonly ChessboardPositions _chessboardPositions;

        public FigureTests()
        {
            _chessboardPositions = new ChessboardPositions();
        }

        [Fact]
        public void GetOppositKing_ShouldReturnFieldWithKing()
        { 
            // Arrange
            var board = _chessboardPositions.GetDefaultPosition();
            var whitePawn = board.Board[1][0].Figure;
            var blackPawn = board.Board[6][0].Figure;

            // Act
            var oppositeKingFromWhitePiece = whitePawn.GetOppositKing(board);
            var oppositeKingFromBlackPiece = blackPawn.GetOppositKing(board);

            // Assert
            Assert.NotNull(oppositeKingFromWhitePiece);
            Assert.Equal("King", oppositeKingFromWhitePiece.Figure.Name);
            Assert.False(oppositeKingFromWhitePiece.Figure.IsWhite);
            Assert.Equal(8, oppositeKingFromWhitePiece.Row);
            Assert.Equal(5, oppositeKingFromWhitePiece.Col);

            Assert.NotNull(oppositeKingFromBlackPiece);
            Assert.Equal("King", oppositeKingFromBlackPiece.Figure.Name);
            Assert.True(oppositeKingFromBlackPiece.Figure.IsWhite);
            Assert.Equal(1, oppositeKingFromBlackPiece.Row);
            Assert.Equal(5, oppositeKingFromBlackPiece.Col);

            Assert.NotEqual(oppositeKingFromWhitePiece.Figure.IsWhite, oppositeKingFromBlackPiece.Figure.IsWhite);
        }

        [Fact]
        public void GetListOfFieldsAttackingTarget_ShouldNotReturnAnyField()
        {
            // Arrange
            var board = _chessboardPositions.GetDefaultPosition();
            var whitePawn = board.Board[1][4].Figure; 

            foreach (var field in board.Board.SelectMany(f => f).Where(f => f.Figure != null))
            {
                field.Figure.CalculateAtackedFields(board, field);
            }

            // Act
            var attackingFields = whitePawn.GetListOfFieldsAttackingTarget(board);

            // Assert
            Assert.Empty(attackingFields);
        }

        [Fact]
        public void GetListOfFieldsAttackingTarget_ShouldReturn_MultipleFields()
        {
            //Arange
            var board = _chessboardPositions.GetDefaultPosition();
            board.Board[2][2] = new Field(true, new Pawn(false, 1, "Pawn"), 3, 3);
            var targetField = board.Board[2][2];
            var expectedFields = new List<Field> { new Field(1,2), new Field(2,2),new Field(2,4) };
            foreach (var field in board.Board.SelectMany(f => f).Where(f => f.Figure != null))
            {
                field.Figure.CalculateAtackedFields(board, field);
            }

            //Act
            var attackingFields = targetField.Figure.GetListOfFieldsAttackingTarget(board);

            // Assert
            Assert.NotEmpty(attackingFields);
            Assert.Equal(expectedFields.Count, attackingFields.Count);
            Assert.All(expectedFields, expectedField =>
                Assert.Contains(attackingFields, field =>
                    field.Row == expectedField.Row && field.Col == expectedField.Col));



        }
    }
}
