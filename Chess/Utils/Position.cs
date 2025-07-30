using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils;

/// <summary>
/// Represents a position on a chessboard with support for different coordinate formats.
/// </summary>
/// <remarks>
/// This class supports two coordinate systems:
/// <list type="bullet">
/// <item><description><see cref="Formatter.ChessFormat"/>: Standard chess notation (1-8 for both row and column)</description></item>
/// <item><description><see cref="Formatter.MatrixFormat"/>: Zero-based array indexing (0-7 for both row and column)</description></item>
/// </list>
/// If no format is specified in the constructor, <see cref="Formatter.ChessFormat"/> is used by default.
/// </remarks>
/// <example>
/// <code>
/// // Chess format (default) - positions from 1 to 8
/// var chessPos = new Position(1, 1); // Bottom-left corner in chess notation
/// var explicitChess = new Position(8, 8, Formatter.ChessFormat); // Top-right corner
/// 
/// // Matrix format - positions from 0 to 7
/// var matrixPos = new Position(0, 0, Formatter.MatrixFormat); // Top-left in array indexing
/// var matrixBottomRight = new Position(7, 7, Formatter.MatrixFormat); // Bottom-right
/// </code>
/// </example>
public class Position
{
    public Position(int row, int col, Formatter? format = null)
    {
        Col = col;
        Row = row;
        Format = format ?? Formatter.ChessFormat;
        Validate();
    }

    /// <summary>
    /// Gets or sets the column coordinate of the position.
    /// </summary>
    /// <value>
    /// For <see cref="Formatter.ChessFormat"/>: 1-8 (a-h in chess notation).
    /// For <see cref="Formatter.MatrixFormat"/>: 0-7 (array column index).
    /// </value>
    public int Col { get;private set; }

    /// <summary>
    /// Gets or sets the row coordinate of the position.
    /// </summary>
    /// <value>
    /// For <see cref="Formatter.ChessFormat"/>: 1-8 (chess board ranks).
    /// For <see cref="Formatter.MatrixFormat"/>: 0-7 (array row index).
    /// </value>
    public int Row { get;private set; }

    /// <summary>
    /// Gets or sets the coordinate format used by this position.
    /// </summary>
    /// <value>The format determining the valid coordinate ranges.</value>
    public Formatter? Format { get;private set; }

    public Position SwitchFormat()
    {
        if(Format == Formatter.ChessFormat)
            return new Position(Row - 1, Col - 1, Formatter.MatrixFormat);

        return new Position(Row + 1, Col + 1);
    }

    private void Validate()
    {
        if (Format == Formatter.MatrixFormat)
        {
            if (Row < 0 || Row > 7)
                throw new ArgumentOutOfRangeException(nameof(Row), "Row must be between 0 and 7 for MatrixFormat");
            if (Col < 0 || Col > 7)
                throw new ArgumentOutOfRangeException(nameof(Col), "Col must be between 0 and 7 for MatrixFormat");
        }
        else
        {
            if (Row < 1 || Row > 8)
                throw new ArgumentOutOfRangeException(nameof(Row), "Row must be between 1 and 8 for ChessFormat");
            if (Col < 1 || Col > 8)
                throw new ArgumentOutOfRangeException(nameof(Col), "Col must be between 1 and 8 for ChessFormat");
        }
    }

    public override bool Equals(object? obj)
    {
        var pos = obj as Position;
        if (this.Row.Equals(pos.Row) && this.Col.Equals(pos.Col))
            return true;
        return false;
    }

    public override string ToString()
    {
        return $"{Row}{Col}";
    }
}

/// <summary>
/// Specifies the coordinate format for chess positions.
/// </summary>
public enum Formatter
{
    /// <summary>
    /// Standard chess coordinate format with 1-based indexing (1-8 for both rows and columns).
    /// This corresponds to traditional chess notation where positions range from a1 to h8.
    /// </summary>
    ChessFormat = 0,
    /// <summary>
    /// Matrix coordinate format with 0-based indexing (0-7 for both rows and columns).
    /// This corresponds to array indexing commonly used in programming.
    /// </summary>
    MatrixFormat = 1
}
