using Chess.Figures;
using Chess.Utils;
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
        Board = new List<List<Field>>();

        for (int row = 1; row <= 8; row++)
        {
            var boardRow = new List<Field>();
            for (int col = 1; col <= 8; col++)
            {
                boardRow.Add(new Field(false, row, col));
            }
            Board.Add(boardRow);
        }
        Board[0][0] = new Field(true, new Rook(true, 5, "Rook"), 1, 1);
        Board[0][1] = new Field(true, new Knight(true, 3, "Knight"), 1, 2);
        Board[0][2] = new Field(true, new Bishop(true, 3, "Bishop"), 1, 3);
        Board[0][3] = new Field(true, new Queen(true, 10, "Queen"), 1, 4);
        Board[0][4] = new Field(true, new King(true, 1000, "King"), 1, 5);
        Board[0][5] = new Field(true, new Bishop(true, 3, "Bishop"), 1, 6);
        Board[0][6] = new Field(true, new Knight(true, 3, "Knight"), 1, 7);
        Board[0][7] = new Field(true, new Rook(true, 5, "Rook"), 1, 8);

        Board[1][0] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 1);
        Board[1][1] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 2);
        Board[1][2] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 3);
        Board[1][3] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 4);
        Board[1][4] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 5);
        Board[1][5] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 6);
        Board[1][6] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 7);
        Board[1][7] = new Field(true, new Pawn(true, 1, "Pawn"), 2, 8);

        Board[6][0] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 1);
        Board[6][1] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 2);
        Board[6][2] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 3);
        Board[6][3] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 4);
        Board[6][4] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 5);
        Board[6][5] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 6);
        Board[6][6] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 7);
        Board[6][7] = new Field(true, new Pawn(false, 1, "Pawn"), 7, 8);

        Board[7][0] = new Field(true, new Rook(false, 5, "Rook"), 8, 1);
        Board[7][1] = new Field(true, new Knight(false, 3, "Knight"), 8, 2);
        Board[7][2] = new Field(true, new Bishop(false, 3, "Bishop"), 8, 3);
        Board[7][3] = new Field(true, new Queen(false, 10, "Queen"), 8, 4);
        Board[7][4] = new Field(true, new King(false, 1000, "King"), 8, 5);
        Board[7][5] = new Field(true, new Bishop(false, 3, "Bishop"), 8, 6);
        Board[7][6] = new Field(true, new Knight(false, 3, "Knight"), 8, 7);
        Board[7][7] = new Field(true, new Rook(false, 5, "Rook"), 8, 8);
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

    public void UsedFields()
    {
        this.Board.SelectMany(fl => fl)
            .Where(field => field.Figure != null)
            .ToList()
            .ForEach(field => field.Figure.AttackedFields.Clear());
        var usedWhiteFields = this.Board.SelectMany(f => f)
        .Where(field => field.IsUsed && field.Figure.IsWhite);
        foreach (var field in usedWhiteFields)
        {
            field.Figure.CalculateAtackedFields(this, field);
        }

        var usedBlackFields = this.Board.SelectMany(f => f)
        .Where(field => field.IsUsed && !field.Figure.IsWhite);
        foreach (var field in usedBlackFields)
        {
            field.Figure.CalculateAtackedFields(this, field);
        }
    }

    public void ShowNewPosition()
    {
        var valueBeetwenFields = 10;
        foreach (var field in this.Board)
        {
            foreach (var inner in field)
            {
                var gap = valueBeetwenFields - (CheckFigure(inner).Length);
                Console.Write($"{CheckFigure(inner)}{ConvertIntoGap(gap)}");
            }
            Console.WriteLine();
        }

        string CheckFigure(Field inner)
            => inner.Figure != null ? inner.Figure.Name : "Empty";
    }
    public string ConvertIntoGap(int length)
    {
        StringBuilder sb = new StringBuilder();
        while (length > 0)
        {
            sb.Append(" ");
            length--;
        }
        return sb.ToString();
    }

    public Field CalculatePositionOnChessboard(string position) // in format "a2" to format enumerable from 1  custom => (col, row)
    {
        var dict = new Dictionary<char, int>()
            {
            {'a',1},
            {'b',2},
            {'c',3},
            {'d',4},
            {'e',5},
            {'f',6},
            {'g',7},
            {'h',8}
            };
        dict.TryGetValue(position.First(), out var col);
        var row = (int)char.GetNumericValue(position.Last());
        return this.Board.SelectMany(f => f)
               .FirstOrDefault(field => field.Col == col && field.Row == row);
    }

    public Position CalculateTargetPosition(string position) // in format "a2" to format enumerable from 0 default table => [col][row]
    {
        var dict = new Dictionary<char, int>()
            {
            {'a',0},
            {'b',1},
            {'c',2},
            {'d',3},
            {'e',4},
            {'f',5},
            {'g',6},
            {'h',7}
            };
        dict.TryGetValue(position.First(), out var value);
        var row = (int)char.GetNumericValue(position.Last());
        return new Position(row - 1, value);
    }

    public List<List<Field>> Board { get; set; }
}
