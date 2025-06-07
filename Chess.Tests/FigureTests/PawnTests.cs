using Chess.Figures;
using Chess.Tests.SetupTests;
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
            //Assert.IsType<Pawn>(chessboard.Board[7][5].Figure);//need to implement possibility to choose figure when promoting and check this choosen type here
            
        }
    }
}
