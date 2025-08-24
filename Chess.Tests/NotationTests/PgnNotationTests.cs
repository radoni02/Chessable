using Chess.Tests.SetupTests;
using Chess.Utils.Notations.PGN;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Chessboard;
using Chess.Figures;

namespace Chess.Tests.NotationTests
{
    public class PgnNotationTests
    {
        private readonly ChessboardPositions _chessboardPositions;

        public PgnNotationTests()
        {
            _chessboardPositions = new ChessboardPositions();
        }

        [Fact]
        public void ConvertToPgn_ShouldReturnCorrectNotation_ForPawnMove()
        {
            // Arrange
            var pgnNotation = new PgnNotation();
            var moveHistory = new List<PossibleMove>
            {
                new PossibleMove(new Position(2, 5), new Position(4, 5))
            };

            // Act
            pgnNotation.ConvertToPgn(moveHistory);

            // Assert
            Assert.Single(pgnNotation.PgnMoveHistory);
            Assert.Equal("e4", pgnNotation.PgnMoveHistory[0].ToString());
        }

        [Fact]
        public void ConvertSingleMoveToPgn_ShouldReturnCorrectNotation_ForPawnCapture()
        {
            // Arrange
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            checkerboard.Board[4][3] = new Field(true, new Pawn(true, 1, "Pawn"), 5, 4);
            checkerboard.Board[5][4] = new Field(true, new Pawn(false, 1, "Pawn"), 6, 5);
            checkerboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5);
            checkerboard.Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5);

            var pgnNotation = new PgnNotation();
            var move = new PossibleMove(new Position(5, 4), new Position(6, 5));

            // Act
            pgnNotation.ConvertSingleMoveToPgn(checkerboard, move);

            // Assert
            Assert.Single(pgnNotation.PgnMoveHistory);
            var result = pgnNotation.PgnMoveHistory[0].ToString();
            Assert.Contains("dx", result);
            Assert.Contains("e6", result);
        }

        [Fact]
        public void ConvertSingleMoveToPgn_ShouldReturnCorrectNotation_ForKnightMove()
        {
            // Arrange
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            checkerboard.Board[0][1] = new Field(true, new Knight(true, 3, "Knight"), 1, 2);
            checkerboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5);
            checkerboard.Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5);

            var pgnNotation = new PgnNotation();
            var move = new PossibleMove(new Position(1, 2), new Position(3, 3));

            // Act
            pgnNotation.ConvertSingleMoveToPgn(checkerboard, move);

            // Assert
            Assert.Single(pgnNotation.PgnMoveHistory);
            Assert.Equal("Nc3", pgnNotation.PgnMoveHistory[0].ToString());
        }

        [Fact]
        public void ConvertSingleMoveToPgn_ShouldReturnCorrectNotation_ForKnightCapture()
        {
            // Arrange
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            checkerboard.Board[2][2] = new Field(true, new Knight(true, 3, "Knight"), 3, 3);
            checkerboard.Board[4][3] = new Field(true, new Pawn(false, 1, "Pawn"), 5, 4);
            checkerboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5);
            checkerboard.Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5);

            var pgnNotation = new PgnNotation();
            var move = new PossibleMove(new Position(3, 3), new Position(5, 4));

            // Act
            pgnNotation.ConvertSingleMoveToPgn(checkerboard, move);

            // Assert
            Assert.Single(pgnNotation.PgnMoveHistory);
            var result = pgnNotation.PgnMoveHistory[0].ToString();
            Assert.StartsWith("N", result);
            Assert.Contains("x", result);
            Assert.Contains("d5", result);
        }

        [Fact]
        public void ConvertSingleMoveToPgn_ShouldReturnCorrectNotation_ForBishopMove()
        {
            // Arrange
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            checkerboard.Board[0][2] = new Field(true, new Bishop(true, 3, "Bishop"), 1, 3);
            checkerboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5);
            checkerboard.Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5);

            var pgnNotation = new PgnNotation();
            var move = new PossibleMove(new Position(1, 3), new Position(4, 6));
            // Act
            pgnNotation.ConvertSingleMoveToPgn(checkerboard, move);

            // Assert
            Assert.Single(pgnNotation.PgnMoveHistory);
            Assert.Equal("Bf4", pgnNotation.PgnMoveHistory[0].ToString());
        }

        [Fact]
        public void ConvertSingleMoveToPgn_ShouldReturnCorrectNotation_ForKingMove()
        {
            // Arrange
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            checkerboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5);
            checkerboard.Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5);

            var pgnNotation = new PgnNotation();
            var move = new PossibleMove(new Position(1, 5), new Position(1, 6));

            // Act
            pgnNotation.ConvertSingleMoveToPgn(checkerboard, move);

            // Assert
            Assert.Single(pgnNotation.PgnMoveHistory);
            Assert.Equal("Kf1", pgnNotation.PgnMoveHistory[0].ToString());
        }

        [Fact]
        public void ConvertSingleMoveToPgn_ShouldReturnCorrectNotation_ForKingCapture()
        {
            // Arrange
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            checkerboard.Board[4][4] = new Field(true, new King(true, 1000, "King"), 5, 5);
            checkerboard.Board[5][5] = new Field(true, new Pawn(false, 1, "Pawn"), 6, 6);
            checkerboard.Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5);

            var pgnNotation = new PgnNotation();
            var move = new PossibleMove(new Position(5, 5), new Position(6, 6));

            // Act
            pgnNotation.ConvertSingleMoveToPgn(checkerboard, move);

            // Assert
            Assert.Single(pgnNotation.PgnMoveHistory);
            var result = pgnNotation.PgnMoveHistory[0].ToString();
            Assert.StartsWith("K", result);
            Assert.Contains("x", result);
            Assert.Contains("f6", result);
        }

        [Fact]
        public void ConvertSingleMoveToPgn_ShouldReturnCorrectNotation_ForAmbiguousKnightMove_DifferentColumns()
        {
            // Arrange
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            checkerboard.Board[2][1] = new Field(true, new Knight(true, 3, "Knight"), 3, 2);
            checkerboard.Board[2][5] = new Field(true, new Knight(true, 3, "Knight"), 3, 6);
            checkerboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5); 
            checkerboard.Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5);

            var pgnNotation = new PgnNotation();
            var move = new PossibleMove(new Position(3, 2), new Position(4, 4));

            // Act
            pgnNotation.ConvertSingleMoveToPgn(checkerboard, move);

            // Assert
            Assert.Single(pgnNotation.PgnMoveHistory);
            var result = pgnNotation.PgnMoveHistory[0].ToString();
            Assert.StartsWith("N", result);
            Assert.Contains("b", result);
            Assert.Contains("d4", result);
        }

        [Fact]
        public void ConvertSingleMoveToPgn_ShouldReturnCorrectNotation_ForAmbiguousRookMove_SameColumn()
        {
            // Arrange
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            checkerboard.Board[0][0] = new Field(true, new Rook(true, 5, "Rook"), 1, 1);
            checkerboard.Board[7][0] = new Field(true, new Rook(true, 5, "Rook"), 8, 1);
            checkerboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5);
            checkerboard.Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5);

            var pgnNotation = new PgnNotation();
            var move = new PossibleMove(new Position(1, 1), new Position(5, 1));

            // Act
            pgnNotation.ConvertSingleMoveToPgn(checkerboard, move);

            // Assert
            Assert.Single(pgnNotation.PgnMoveHistory);
            var result = pgnNotation.PgnMoveHistory[0].ToString();
            Assert.StartsWith("R", result);
            Assert.Contains("1", result); 
            Assert.Contains("a5", result);
        }

        [Fact]
        public void ConvertSingleMoveToPgn_ShouldReturnCorrectNotation_ForCheckMove()
        {
            // Arrange
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            checkerboard.Board[0][3] = new Field(true, new Queen(true, 10, "Queen"), 1, 4); 
            checkerboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5);
            checkerboard.Board[7][7] = new Field(true, new King(false, 1000, "King"), 8, 8);

            var pgnNotation = new PgnNotation();
            var move = new PossibleMove(new Position(1, 4), new Position(5, 8));

            // Act
            pgnNotation.ConvertSingleMoveToPgn(checkerboard, move);

            // Assert
            Assert.Single(pgnNotation.PgnMoveHistory);
            var result = pgnNotation.PgnMoveHistory[0].ToString();
            Assert.Contains("Qh5", result);
            Assert.EndsWith("+", result);
        }

        [Fact]
        public void ConvertSingleMoveToPgn_ShouldReturnCorrectNotation_ForCheckmateMove()
        {
            // Arrange
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            checkerboard.Board[0][3] = new Field(true, new Queen(true, 10, "Queen"), 1, 4);
            checkerboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5);
            checkerboard.Board[7][6] = new Field(true, new King(false, 1000, "King"), 8, 7);
            checkerboard.Board[7][7] = new Field(true, new Rook(true, 5, "Rook"), 7, 1); 

            var pgnNotation = new PgnNotation();
            var move = new PossibleMove(new Position(1, 4), new Position(8, 4));

            // Act
            pgnNotation.ConvertSingleMoveToPgn(checkerboard, move);

            // Assert
            Assert.Single(pgnNotation.PgnMoveHistory);
            var result = pgnNotation.PgnMoveHistory[0].ToString();
            Assert.Contains("Qd8", result);
            Assert.EndsWith("#", result);
        }

        [Fact]
        public void ConvertSingleMoveToPgn_ShouldThrowException_WhenSourceFieldEmpty()
        {
            // Arrange
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            var pgnNotation = new PgnNotation();
            var invalidMove = new PossibleMove(new Position(2, 5), new Position(4, 5));

            // Act & Assert
            Assert.Throws<Exception>(() => pgnNotation.ConvertSingleMoveToPgn(checkerboard, invalidMove));
        }

        [Fact]
        public void ConvertSingleMoveToPgn_ShouldReturnCorrectNotation_ForComplexAmbiguity()
        {
            // Arrange
            var checkerboard = _chessboardPositions.GetEmptyBoard();
            checkerboard.Board[1][1] = new Field(true, new Knight(true, 3, "Knight"), 2, 2);
            checkerboard.Board[1][5] = new Field(true, new Knight(true, 3, "Knight"), 2, 6);
            checkerboard.Board[4][1] = new Field(true, new Knight(true, 3, "Knight"), 4, 2);
            checkerboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5);
            checkerboard.Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5);

            var pgnNotation = new PgnNotation();
            var move = new PossibleMove(new Position(2, 2), new Position(3, 4));

            // Act
            pgnNotation.ConvertSingleMoveToPgn(checkerboard, move);

            // Assert
            Assert.Single(pgnNotation.PgnMoveHistory);
            var result = pgnNotation.PgnMoveHistory[0].ToString();
            Assert.StartsWith("N", result);
            Assert.Contains("b2", result);
            Assert.Contains("d3", result);
        }

    }
}
