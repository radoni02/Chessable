using Chess.Chessboard;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures;

public class King : Figure
{
    public King(bool isWhite, int value, string name) : base(isWhite, value, name)
    {
    }

    public override void CalculateAtackedFields(Checkerboard checkerboard, Field currentField)
    {
        AttackedFields =  checkerboard.Board.SelectMany(fl => fl)
                                        .Where(field => Math.Abs(currentField.Row - field.Row) <= 1 && Math.Abs(currentField.Col - field.Col) <= 1)
                                        .Where(field => !field.IsUsed || field.Figure.IsWhite != currentField.Figure.IsWhite)
                                        .ToList();
    }

    public override HashSet<string> PossibleMoves(Checkerboard checkerboard, Field currentField)
    {

        var possibleKingMoves = new HashSet<string>();
        possibleKingMoves = KingPossibleMoves(checkerboard,currentField);
        return possibleKingMoves;

    }

    private HashSet<string> KingPossibleMoves(Checkerboard checkerboard, Field currentField)
    {
        var forbiddenFieldsForKing = checkerboard.Board.SelectMany(fl => fl)
                                                        .Where(f => f.IsUsed && f.Figure?.IsWhite != currentField.Figure.IsWhite)
                                                            .Select(field => field.Figure)
                                                            .SelectMany(s => s.AttackedFields)
                                                            .Distinct();

        var possibleMoves = GetBasicKingMoves(checkerboard, currentField, forbiddenFieldsForKing);

        if (currentField.Figure.MoveConut == 0)
        {
            var castlingMoves = GetCastlingMoves(checkerboard, currentField, forbiddenFieldsForKing);
            foreach (var move in castlingMoves)
            {
                possibleMoves.Add(move);
        }
        }

        return possibleMoves;
        
        HashSet<string> GetBasicKingMoves(Checkerboard board, Field current, IEnumerable<Field> forbidden)
        {
            return board.Board.SelectMany(fl => fl)
                              .Where(field => PotentialFieldIsInKingMoveRange(field, current)
                                           && ((field.IsUsed && field.Figure?.IsWhite != current.Figure.IsWhite) || !field.IsUsed))
                              .Where(field => !forbidden.Any(ff => ff.Row == field.Row && ff.Col == field.Col))
                              .Select(field => $"{field.Row - 1}{field.Col - 1}")
                              .ToHashSet();
        }

        HashSet<string> GetCastlingMoves(Checkerboard board, Field current, IEnumerable<Field> forbidden)
                                    {
            var castlingMoves = new HashSet<string>();
            var possibleCastling = board.Board.SelectMany(fl => fl)
                                             .Where(field => field.Figure?.Name == "Rook"
                                                          && field.Figure.IsWhite == current.Figure.IsWhite
                                                          && field.Figure.MoveConut == 0);

            foreach (var rook in possibleCastling)
                                        {
                if (IsCastlingValid(board, current, rook, forbidden))
                {
                    int castlingCol = rook.Col < current.Col ? current.Col - 2 : current.Col + 2;
                    castlingMoves.Add($"{current.Row - 1}{castlingCol - 1}");
                                        }
            }
            return castlingMoves;
        }

        bool IsCastlingValid(Checkerboard board, Field king, Field rook, IEnumerable<Field> forbidden)
                                    {
            var minCol = Math.Min(king.Col, rook.Col);
            var maxCol = Math.Max(king.Col, rook.Col);

            var fieldsInBetween = board.Board.SelectMany(fl => fl)
                                            .Where(field => field.Row == king.Row
                                                         && field.Col > minCol
                                                         && field.Col < maxCol)
                                            .ToList();

            bool allFieldsEmpty = fieldsInBetween.All(field => !field.IsUsed);

            var kingPath = new[] { king.Col, king.Col + (rook.Col > king.Col ? 1 : -1), king.Col + (rook.Col > king.Col ? 2 : -2) };
            bool pathUnderAttack = kingPath.Any(col =>
                forbidden.Any(ff => ff.Row == king.Row && ff.Col == col));

            return allFieldsEmpty && !pathUnderAttack;
                                        }
    }


    private bool PotentialFieldIsInKingMoveRange(Field potentialField, Field currentField)
    {
        if (Math.Abs(currentField.Row - potentialField.Row) <= 1 && Math.Abs(currentField.Col - potentialField.Col) <= 1
                   && !(potentialField.Row == currentField.Row && potentialField.Col == currentField.Col))
            return true;
        return false;
    }
}
