using Chess.Chessboard;
using Chess.Figures;
using Chess.Tests.SetupTests;
using Chess.Utils.ChessPlayer;
using Chess.Utils.Notations.FEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Tests.NotationTests
{
    public class FenNotationTests
    {
        private readonly ChessboardPositions _chessboardPositions;
        public FenNotationTests()
        {
            _chessboardPositions = new ChessboardPositions(); 
        }
        [Fact]
        public void GetCurrentPosition_ShouldCalculateNextMoveCorrectly_ForWhitePlayer()
        {
            // Arrange
            var fenNotation = new FenNotation();
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            var whitePlayer = new Player(0,PlayerColor.White,true);

            // Act
            var result = fenNotation.GetCurrentPosition(checkerboard, whitePlayer, 3);

            // Assert
            var fenString = result.ToString();
            Assert.Contains(" w ", fenString);
        }

        [Fact]
        public void GetCurrentPosition_ShouldCalculateNextMoveCorrectly_ForBlackPlayer()
        {
            // Arrange
            var fenNotation = new FenNotation();
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            var blackPlayer = new Player(0,PlayerColor.Black,true);

            // Act
            var result = fenNotation.GetCurrentPosition(checkerboard, blackPlayer,3);

            // Assert
            var fenString = result.ToString();
            Assert.Contains(" b ", fenString);
        }

    }
}
