using Chess.Chessboard;
using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Figures;

internal class King : Figure
{
    public King(bool isWhite, int value, string name) : base(isWhite, value, name)
    {
    }

    public override void Move(Checkerboard checkerboard, Field currentField, Position targetField)
    {
        base.Move(checkerboard, currentField, targetField);
        int moveDistance = Math.Abs(targetField.Col - (currentField.Col - 1));
        
        if (moveDistance == 2)
        {
            HandleCastling(checkerboard, currentField, targetField);
        }
    }

    private void HandleCastling(Checkerboard checkerboard, Field currentField, Position targetField)
    {
        bool isKingsideCastling = targetField.Col > (currentField.Col - 1);

        var rook = checkerboard.Board.SelectMany(ff => ff)
        .FirstOrDefault(field =>
            field.Figure?.Name == "Rook"
            && field.Figure.IsWhite == currentField.Figure.IsWhite
            && field.Row == currentField.Row
            && ((isKingsideCastling && field.Col == 8)    
                || (!isKingsideCastling && field.Col == 1)));

        int rookTargetCol = isKingsideCastling ? 6 : 4;

        base.Move(checkerboard, rook, new Position(currentField.Row - 1, rookTargetCol - 1, Formatter.MatrixFormat));
    }

    public override void CalculateAtackedFields(Checkerboard checkerboard, Field currentField)
    {
        AttackedFields =  checkerboard.Board.SelectMany(fl => fl)
                                        .Where(field => Math.Abs(currentField.Row - field.Row) <= 1 && Math.Abs(currentField.Col - field.Col) <= 1)
                                        .Where(field => !field.IsUsed || field.Figure.IsWhite != currentField.Figure.IsWhite)
                                        .ToList();
    }

    protected override void CalculatePossibleMoves(Checkerboard checkerboard, Field currentField)
    {
        var possibleTargets = KingPossibleMoves(checkerboard, currentField);

        PossibleMoves = possibleTargets
            .Select(target => new PossibleMove(new Position(currentField.Row, currentField.Col), new Position(target.Row, target.Col)))
            .ToList();

    }

    private IEnumerable<Field> KingPossibleMoves(Checkerboard checkerboard, Field currentField)
    {
        var forbiddenFieldsForKing = checkerboard.Board.SelectMany(fl => fl)
                                                        .Where(f => f.IsUsed && f.Figure?.IsWhite != currentField.Figure.IsWhite)
                                                        .Select(field => field.Figure)
                                                        .SelectMany(s => s.AttackedFields)
                                                        .Distinct();

        var possibleMoves = GetBasicKingMoves(checkerboard, currentField, forbiddenFieldsForKing).ToList();

        if (currentField.Figure.MoveConut == 0)
        {
            var castlingMoves = GetCastlingMoves(checkerboard, currentField, forbiddenFieldsForKing);
            possibleMoves.AddRange(castlingMoves);
        }

        return possibleMoves;

        IEnumerable<Field> GetBasicKingMoves(Checkerboard board, Field current, IEnumerable<Field> forbidden)
        {
            return board.Board.SelectMany(fl => fl)
                              .Where(field => PotentialFieldIsInKingMoveRange(field, current)
                                           && ((field.IsUsed && field.Figure?.IsWhite != current.Figure.IsWhite) || !field.IsUsed))
                              .Where(field => !forbidden.Any(ff => ff.Row == field.Row && ff.Col == field.Col));
        }

        IEnumerable<Field> GetCastlingMoves(Checkerboard board, Field current, IEnumerable<Field> forbidden)
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
                    yield return new Field(current.Row,castlingCol);
                }
            }
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
