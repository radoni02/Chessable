using Chess.Chessboard;
using Chess.Figures;
using Chess.Tests.SetupTests;
using Chess.Utils;
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
            var result = fenNotation.GetCurrentPosition(checkerboard, whitePlayer, 3, default, default);

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
            var result = fenNotation.GetCurrentPosition(checkerboard, blackPlayer,3,default, default);

            // Assert
            var fenString = result.ToString();
            Assert.Contains(" b ", fenString);
        }

        [Fact]
        public void GetCurrentPosition_ShouldReturnCorrectFenForStartingPosition()
        {
            // Arrange
            var fenNotation = new FenNotation();
            var checkerboard = _chessboardPositions.GetDefaultPosition();
            var whitePlayer = new Player(0, PlayerColor.White, true);

            // Act
            var result = fenNotation.GetCurrentPosition(checkerboard, whitePlayer, 1, 0, null);

            // Assert
            Assert.Equal("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - - 0 1", result);
        }

        [Fact]
        public void GetCurrentPosition_ShouldReturnCorrectFenForMidGamePosition()
        {
            // Arrange
            var fenNotation = new FenNotation();
            var checkerboard = _chessboardPositions.GetMidGamePosition();
            var blackPlayer = new Player(0, PlayerColor.Black, true);

            // Act
            var result = fenNotation.GetCurrentPosition(checkerboard, blackPlayer, 15, 2, null);

            // Assert
            var parts = result.Split(' ');
            Assert.Equal(6, parts.Length);
            Assert.Contains("b", parts[1]);
            Assert.Equal("15", parts[5]);
            Assert.Equal("2", parts[4]);
        }

        [Fact]
        public void GetCurrentPosition_ShouldCalculatePiecePlacementCorrectly_EmptyBoard()
        {
            // Arrange
            var fenNotation = new FenNotation();
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            var whitePlayer = new Player(0, PlayerColor.White, true);

            // Act
            var result = fenNotation.GetCurrentPosition(checkerboard, whitePlayer, 1, 0, null);

            // Assert
            var piecePlacement = result.Split(' ')[0];
            Assert.Equal("8/8/8/8/8/8/8/8", piecePlacement);
        }

    }
}
