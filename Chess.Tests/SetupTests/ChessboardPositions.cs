﻿using Chess.Chessboard;
using Chess.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Tests.SetupTests
{
    internal class ChessboardPositions
    {

        public Chessboard.Checkerboard GetDefaultPosition()
        {
            var chessboard = GetEmptyBoard();

            chessboard.Board[0][0] = new Field(true, new Rook(true, 5, "Rook"), 1, 1);
            chessboard.Board[0][1] = new Field(true, new Knight(true, 3, "Knight"), 1, 2);
            chessboard.Board[0][2] = new Field(true, new Bishop(true, 3, "Bishop"), 1, 3);
            chessboard.Board[0][3] = new Field(true, new Queen(true, 10, "Queen"), 1, 4);
            chessboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5);
            chessboard.Board[0][5] = new Field(true, new Bishop(true, 3, "Bishop"), 1, 6);
            chessboard.Board[0][6] = new Field(true, new Knight(true, 3, "Knight"), 1, 7);
            chessboard.Board[0][7] = new Field(true, new Rook(true, 5, "Rook"), 1, 8);

            chessboard.Board[1][0] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 1);
            chessboard.Board[1][1] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 2);
            chessboard.Board[1][2] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 3);
            chessboard.Board[1][3] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 4);
            chessboard.Board[1][4] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 5);
            chessboard.Board[1][5] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 6);
            chessboard.Board[1][6] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 7);
            chessboard.Board[1][7] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 8);

            chessboard.Board[6][0] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 1);
            chessboard.Board[6][1] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 2);
            chessboard.Board[6][2] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 3);
            chessboard.Board[6][3] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 4);
            chessboard.Board[6][4] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 5);
            chessboard.Board[6][5] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 6);
            chessboard.Board[6][6] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 7);
            chessboard.Board[6][7] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 8);

            chessboard.Board[7][0] = new Field(true, new Rook(false, 5, "Rook"), 8, 1);
            chessboard.Board[7][1] = new Field(true, new Knight(false, 3, "Knight"), 8, 2);
            chessboard.Board[7][2] = new Field(true, new Bishop(false, 3, "Bishop"), 8, 3);
            chessboard.Board[7][3] = new Field(true, new Queen(false, 10, "Queen"), 8, 4);
            chessboard.Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5);
            chessboard.Board[7][5] = new Field(true, new Bishop(false, 3, "Bishop"), 8, 6);
            chessboard.Board[7][6] = new Field(true, new Knight(false, 3, "Knight"), 8, 7);
            chessboard.Board[7][7] = new Field(true, new Rook(false, 5, "Rook"), 8, 8);

            return chessboard;
        }

        public Checkerboard SetPawnWithNonPossibleMoves()
        {
            var chessboard = GetDefaultPosition();
            chessboard.Board[5][5].Figure = new Pawn(true, 1, "Pawn");
            chessboard.Board[5][5].IsUsed = true;
            return chessboard;
        }

        public Checkerboard SetPawnWithAllPossibleMoves()
        {
            var chessboard = GetDefaultPosition();
            chessboard.Board[6][5].Figure = new Pawn(true, 1, "Pawn");
            chessboard.Board[6][5].IsUsed= true;
            chessboard.Board[7][5].Figure = null;
            chessboard.Board[7][5].IsUsed = false;
            return chessboard;
        }

        public Checkerboard GetMidGamePosition()
        {
            var chessboard = GetEmptyBoard();

            // White pieces
            chessboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5); // e1
            chessboard.Board[0][7] = new Field(true, new Rook(true, 5, "Rook"), 1, 8); // h1
            chessboard.Board[2][2] = new Field(true, new Bishop(true, 3, "Bishop"), 3, 3); // c3
            chessboard.Board[3][3] = new Field(true, new Knight(true, 3, "Knight"), 4, 4); // d4
            chessboard.Board[4][4] = new Field(true, new Pawn(true, 1, "Pawn"), 5, 5); // e5
            chessboard.Board[1][5] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 6); // f2
            chessboard.Board[1][6] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 7); // g2
            chessboard.Board[1][7] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 8); // h2

            // Black pieces
            chessboard.Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5); // e8
            chessboard.Board[7][0] = new Field(true, new Rook(false, 5, "Rook"), 8, 1); // a8
            chessboard.Board[5][1] = new Field(true, new Bishop(false, 3, "Bishop"), 6, 2); // b6
            chessboard.Board[4][5] = new Field(true, new Knight(false, 3, "Knight"), 5, 6); // f5
            chessboard.Board[3][4] = new Field(true, new Pawn(false, 1, "Pawn"), 4, 5); // e4
            chessboard.Board[6][0] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 1); // a7
            chessboard.Board[6][1] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 2); // b7
            chessboard.Board[6][7] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 8); // h7

            return chessboard;
        }

        public Checkerboard GetTacticalPosition()
        {
            var chessboard = GetEmptyBoard();

            // White pieces
            chessboard.Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5);
            chessboard.Board[0][7] = new Field(true, new Rook(true, 5, "Rook"), 1, 8);
            chessboard.Board[1][3] = new Field(true, new Bishop(true, 3, "Bishop"), 2, 4);
            chessboard.Board[2][4] = new Field(true, new Pawn(true, 1, "Pawn"), 3, 5);
            chessboard.Board[1][5] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 6);
            chessboard.Board[1][6] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 7);

            // Black pieces
            chessboard.Board[7][6] = new Field(true, new King(false, 1000, "King"), 8, 7);
            chessboard.Board[6][6] = new Field(true, new Rook(false, 5, "Rook"), 7, 7);
            chessboard.Board[3][1] = new Field(true, new Bishop(false, 3, "Bishop"), 4, 2);
            chessboard.Board[5][7] = new Field(true, new Queen(false, 10, "Queen"), 6, 8);
            chessboard.Board[6][5] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 6);
            chessboard.Board[6][7] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 8);

            return chessboard;
        }

        public Checkerboard GetCheckmatePosition()
        {
            var chessboard = GetEmptyBoard();

            // White pieces 
            chessboard.Board[0][6] = new Field(true, new King(true, 1000, "King"), 1, 7); 
            chessboard.Board[1][5] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 6);
            chessboard.Board[1][6] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 7);
            chessboard.Board[1][7] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 8);
            chessboard.Board[2][4] = new Field(true, new Rook(true, 5, "Rook"), 3, 5);

            // Black pieces
            chessboard.Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5);
            chessboard.Board[6][3] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 4);
            chessboard.Board[6][4] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 5);
            chessboard.Board[6][5] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 6);
            chessboard.Board[5][4] = new Field(true, new Queen(false, 10, "Queen"), 6, 5);
            chessboard.Board[4][4] = new Field(true, new Rook(false, 5, "Rook"), 5, 5); 

            // Black pieces creating checkmate on white king
            chessboard.Board[0][5] = new Field(true, new Rook(false, 5, "Rook"), 1, 6);
            chessboard.Board[1][4] = new Field(true, new Queen(false, 10, "Queen"), 2, 5);

            return chessboard;
        }

        public Checkerboard GetCheckPosition()
        {
            var chessboard = GetEmptyBoard();

            // White pieces 
            chessboard.Board[0][6] = new Field(true, new King(true, 1000, "King"), 1, 7);
            chessboard.Board[1][5] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 6);
            chessboard.Board[1][6] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 7);
            chessboard.Board[2][4] = new Field(true, new Rook(true, 5, "Rook"), 3, 5);

            // Black pieces
            chessboard.Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5);
            chessboard.Board[6][3] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 4);
            chessboard.Board[6][4] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 5);
            chessboard.Board[6][5] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 6);
            chessboard.Board[5][4] = new Field(true, new Queen(false, 10, "Queen"), 6, 5);
            chessboard.Board[4][4] = new Field(true, new Rook(false, 5, "Rook"), 5, 5);

            // Black pieces creating check on white king
            chessboard.Board[0][5] = new Field(true, new Rook(false, 5, "Rook"), 1, 6);
            chessboard.Board[1][4] = new Field(true, new Queen(false, 10, "Queen"), 2, 5);

            return chessboard;
        }

        public Checkerboard GetBlockingMovesPosition()
        {
            var chessboard = GetEmptyBoard();

            // White pieces 
            chessboard.Board[0][6] = new Field(true, new King(true, 1000, "King"), 1, 7);
            chessboard.Board[1][5] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 6);
            chessboard.Board[1][6] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 7);
            chessboard.Board[2][4] = new Field(true, new Rook(true, 5, "Rook"), 3, 5);

            // Black pieces
            chessboard.Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5);
            chessboard.Board[6][3] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 4);
            chessboard.Board[6][4] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 5);
            chessboard.Board[6][5] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 6);
            chessboard.Board[5][4] = new Field(true, new Queen(false, 10, "Queen"), 6, 5);
            chessboard.Board[4][4] = new Field(true, new Rook(false, 5, "Rook"), 5, 5);

            // Black pieces creating check on white king
            chessboard.Board[0][1] = new Field(true, new Rook(false, 5, "Rook"), 1, 2);

            //Black rook possible to block
            chessboard.Board[2][2] = new Field(true, new Rook(true, 5, "Rook"), 3, 3);

            return chessboard;
        }

        public Checkerboard GetDiagonallPosition()
        {
            var chessboard = GetEmptyBoard();

            chessboard.Board[0][6] = new Field(true, new Bishop(false, 5, "Bishop"), 1, 7);
            chessboard.Board[2][4] = new Field(true, new King(true, 1000, "King"), 3, 5);
            chessboard.Board[4][2] = new Field(true, new Pawn(true, 1, "Pawn"), 5, 3);



            return chessboard;
        }

            public Checkerboard GetEmptyBoard()
        {
            var chessboard = new Checkerboard();
            chessboard.Board = new List<List<Field>>();

            for (int row = 1; row <= 8; row++)
            {
                var boardRow = new List<Field>();
                for (int col = 1; col <= 8; col++)
                {
                    boardRow.Add(new Field(false, row, col));
                }
                chessboard.Board.Add(boardRow);
            }
            return chessboard;
        }
    }
}
