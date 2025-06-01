using Chess.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Chessboard;

public class Checkerboard
{
    public Checkerboard()
    {
        Board = new List<List<Field>>() {
            new List<Field>() {
                new Field(true, new Rook(true, 5, "Rook"), 1, 1),
                new Field(true, new Knight(true, 3, "Knight"), 1, 2),
                new Field(true, new Bishop(true, 3, "Bishop"), 1, 3),
                new Field(true, new Queen(true, 10, "Queen"), 1, 4),
                new Field(true, new King(true, 1000, "King"), 1, 5),
                new Field(true, new Bishop(true, 3, "Bishop"), 1, 6),
                new Field(true, new Knight(true, 3, "Knight"), 1, 7),
                new Field(true, new Rook(true, 5, "Rook"), 1, 8),
            },
            new List<Field>() {
                new Field(true, new Pawn(true, 1, "Pawn"), 2, 1),
                new Field(true, new Pawn(true, 1, "Pawn"), 2, 2),
                new Field(true, new Pawn(true, 1, "Pawn"), 2, 3),
                new Field(true, new Pawn(true, 1, "Pawn"), 2, 4),
                new Field(true, new Pawn(true, 1, "Pawn"), 2, 5),
                new Field(true, new Pawn(true, 1, "Pawn"), 2, 6),
                new Field(true, new Pawn(true, 1, "Pawn"), 2, 7),
                new Field(true, new Pawn(true, 1, "Pawn"), 2, 8),
            },
            new List<Field>() {
                new Field(false, 3, 1),
                new Field(false, 3, 2),
                new Field(false, 3, 3),
                new Field(false, 3, 4),
                new Field(false, 3, 5),
                new Field(false, 3, 6),
                new Field(false, 3, 7),
                new Field(false, 3, 8),

            },
            new List<Field>() {
                new Field(false, 4, 1),
                new Field(false, 4, 2),
                new Field(false, 4, 3),
                new Field(false, 4, 4),
                new Field(false, 4, 5),
                new Field(false, 4, 6),
                new Field(false, 4, 7),
                new Field(false, 4, 8),

            },
            new List<Field>() {
                new Field(false, 5, 1),
                new Field(false, 5, 2),
                new Field(false, 5, 3),
                new Field(false, 5, 4),
                new Field(false, 5, 5),
                new Field(false, 5, 6),
                new Field(false, 5, 7),
                new Field(false, 5, 8),
            },                      
            new List<Field>() {     
                new Field(false, 6, 1),
                new Field(false, 6, 2),
                new Field(false, 6, 3),
                new Field(false, 6, 4),
                new Field(false, 6, 5),
                new Field(false, 6, 6),
                new Field(false, 6, 7),
                new Field(false, 6, 8),
            },
            new List<Field>() {
                new Field(true, new Pawn(false, 1, "Pawn"), 7, 1),
                new Field(true, new Pawn(false, 1, "Pawn"), 7, 2),
                new Field(true, new Pawn(false, 1, "Pawn"), 7, 3),
                new Field(true, new Pawn(false, 1, "Pawn"), 7, 4),
                new Field(true, new Pawn(false, 1, "Pawn"), 7, 5),
                new Field(true, new Pawn(false, 1, "Pawn"), 7, 6),
                new Field(true, new Pawn(false, 1, "Pawn"), 7, 7),
                new Field(true, new Pawn(false, 1, "Pawn"), 7, 8),
            },
            new List<Field>() {
                new Field(true, new Rook(false, 5, "Rook"), 8, 1),
                new Field(true, new Knight(false, 3, "Knight"), 8, 2),
                new Field(true, new Bishop(false, 3, "Bishop"), 8, 3),
                new Field(true, new Queen(false, 10, "Queen"), 8, 4),
                new Field(true, new King(false, 1000, "King"), 8, 5),
                new Field(true, new Bishop(false, 3, "Bishop"), 8, 6),
                new Field(true, new Knight(false, 3, "Knight"), 8, 7),
                new Field(true, new Rook(false, 5, "Rook"), 8, 8),
            }

        };
    }
    public bool CheckIfFieldIsOutOfTheBoard(int targetRow, int targetCol)
    {
        try
        {
            var exists = Board[targetRow][targetCol].IsUsed;
            return false;
        }
        catch(Exception ex)
        {
            return true;
        }
        
    }

    public List<List<Field>> Board { get; set; }
}
