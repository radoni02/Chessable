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
                new Field(true, new Pawn(true, 1, "Field"), 1, 1),
                new Field(true, new Pawn(true, 1, "Field"), 1, 2),
                new Field(true, new Pawn(true, 1, "Field"), 1, 3),
                new Field(true, new King(true, 1, "King"), 1, 4),
                new Field(true, new Pawn(true, 1, "Field"), 1, 5),
                new Field(true, new Pawn(true, 1, "Field"), 1, 6),
                new Field(true, new Pawn(true, 1, "Field"), 1, 7),
                new Field(true, new Pawn(true, 1, "Field"), 1, 8),
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
                new Field(false, new Empty(0, "Empty"), 3, 1),
                new Field(false, new Empty(0, "Empty"), 3, 2),
                new Field(false, new Empty(0, "Empty"), 3, 3),
                new Field(false, new Empty(0, "Empty"), 3, 4),
                new Field(false, new Empty(0, "Empty"), 3, 5),
                new Field(false, new Empty(0, "Empty"), 3, 6),
                new Field(false, new Empty(0, "Empty"), 3, 7),
                new Field(false, new Empty(0, "Empty"), 3, 8),

            },
            new List<Field>() {
                new Field(false, new Empty(0, "Empty"), 4, 1),
                new Field(false, new Empty(0, "Empty"), 4, 2),
                new Field(false, new Empty(0, "Empty"), 4, 3),
                new Field(false, new Empty(0, "Empty"), 4, 4),
                new Field(false, new Empty(0, "Empty"), 4, 5),
                new Field(false, new Empty(0, "Empty"), 4, 6),
                new Field(false, new Empty(0, "Empty"), 4, 7),
                new Field(false, new Empty(0, "Empty"), 4, 8),

            },
            new List<Field>() {
                new Field(false, new Empty(0, "Empty"), 5, 1),
                new Field(false, new Empty(0, "Empty"), 5, 2),
                new Field(false, new Empty(0, "Empty"), 5, 3),
                new Field(false, new Empty(0, "Empty"), 5, 4),
                new Field(false, new Empty(0, "Empty"), 5, 5),
                new Field(false, new Empty(0, "Empty"), 5, 6),
                new Field(false, new Empty(0, "Empty"), 5, 7),
                new Field(false, new Empty(0, "Empty"), 5, 8),
            },                      
            new List<Field>() {     
                new Field(false, new Empty( 0, "Empty"), 6, 1),
                new Field(false, new Empty( 0, "Empty"), 6, 2),
                new Field(false, new Empty( 0, "Empty"), 6, 3),
                new Field(false, new Empty( 0, "Empty"), 6, 4),
                new Field(false, new Empty( 0, "Empty"), 6, 5),
                new Field(false, new Empty( 0, "Empty"), 6, 6),
                new Field(false, new Empty( 0, "Empty"), 6, 7),
                new Field(false, new Empty( 0, "Empty"), 6, 8),
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
                new Field(true, new Pawn(false, 1, "Field"), 8, 1),
                new Field(true, new Pawn(false, 1, "Field"), 8, 2),
                new Field(true, new Pawn(false, 1, "Field"), 8, 3),
                new Field(true, new Pawn(false, 1, "Field"), 8, 4),
                new Field(true, new Pawn(false, 1, "Field"), 8, 5),
                new Field(true, new Pawn(false, 1, "Field"), 8, 6),
                new Field(true, new Pawn(false, 1, "Field"), 8, 7),
                new Field(true, new Pawn(false, 1, "Field"), 8, 8),
            }

        };
    }

    public List<List<Field>> Board { get; set; }
}
