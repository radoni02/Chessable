using Chess.Chessboard;
using Chess.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils
{
    public class GameStateAnalyzer
    {
        public static CheckmateAnalysisResult AnalyzeForCheckmate(Checkerboard board, Field kingField)
        {
            var result = new CheckmateAnalysisResult();

            if (kingField.Figure is null || kingField.Figure is not King)
            {
                result.WrongFigureSelected = true;
                return result;
            }

            if (!kingField.Figure.CheckIfFigureIsUnderAttack(board))
            {
                result.IsInCheck = false;
                return result;
            }

            result.IsInCheck = true;
            result.PossibleKingMoves = kingField.Figure.CalculatePossibleMoves(board, kingField);
            var figuresThatAttackKing = kingField.Figure.GetListOfFieldsAttackingTarget(board);

            foreach (var attackingField in figuresThatAttackKing)
            {
                if (attackingField.Figure is not Pawn || attackingField.Figure is not Knight)
                {
                    var blockingOptions = GetBlockingOptions(board, kingField, attackingField);
                    result.PossibleBlockingMoves.AddRange(blockingOptions);
                }

                var captureOptions = GetCaptureOptions(board, attackingField, kingField);
                result.PossibleCaptureRescues.AddRange(captureOptions);
                
            }

            result.IsCheckmate = result.PossibleKingMoves.Count == 0 &&
                                result.PossibleCaptureRescues.Count == 0 &&
                                result.PossibleBlockingMoves.Count == 0;

            return result;
        }

        private static List<string> GetCaptureOptions(Checkerboard board, Field attackingField, Field kingField)
        {
            var captureOptions = new List<string>();
            var alliedPiecesThatCanCaptureAttacker = attackingField.Figure.GetListOfFieldsAttackingTarget(board);

            foreach (var alliedPiece in alliedPiecesThatCanCaptureAttacker)
            {
                if(alliedPiece.Figure is King 
                    && !board.Board
                                .SelectMany(ff => ff)
                                .Where(field => field.Figure != null && field.Figure.IsWhite != alliedPiece.Figure.IsWhite)
                                .Any(field => field.Figure.AttackedFields
                                                            .Any(at => at == attackingField)))
                {
                    captureOptions.Add($"{alliedPiece.Row}{alliedPiece.Col}-{attackingField.Row}{attackingField.Col}");
                    continue;
                }
                if (!alliedPiece.Figure.CheckIfFigureIsImmobilized(board) && alliedPiece.Figure is not King)
                {
                    captureOptions.Add($"{alliedPiece.Row}{alliedPiece.Col}-{attackingField.Row}{attackingField.Col}");
                }
            }
            return captureOptions;
        }

        private static List<string> GetBlockingOptions(Checkerboard board, Field kingField, Field attackingField)
        {
            var blockingOptions = new List<string>();
            var possibleTargetsToBlockAttack = kingField.Figure.GetListOfFieldsThatAreBetweenCurrentAndTarget(board, kingField, attackingField);

            var alliedPiecesThatCanBlock = board.Board.SelectMany(ff => ff)
                .Where(field => field.Figure != null &&
                         field.Figure.IsWhite == kingField.Figure.IsWhite &&
                         field.Figure is not King)
                .Where(field => !field.Figure.CheckIfFigureIsImmobilized(board))
                .Where(field =>
                {
                    return possibleTargetsToBlockAttack.Any(blockingField =>
                        field.Figure.AttackedFields.Any(attackedField =>
                            attackedField.Row == blockingField.Row &&
                            attackedField.Col == blockingField.Col));
                })
                .ToList();

            foreach (var piece in alliedPiecesThatCanBlock)
            {
                foreach (var target in possibleTargetsToBlockAttack)
                {
                    if (piece.Figure.AttackedFields.Any(f => f.Row == target.Row && f.Col == target.Col))
                    {
                        blockingOptions.Add($"{piece.Row}{piece.Col}-{target.Row}{target.Col}");
                    }
                }
            }

            return blockingOptions;
        }
    }

    public class CheckmateAnalysisResult
    {
        public bool WrongFigureSelected { get; set; } = false;
        public bool IsInCheck { get; set; }
        public bool IsCheckmate { get; set; }
        public HashSet<string> PossibleKingMoves { get; set; } = new HashSet<string>();
        public List<string> PossibleCaptureRescues { get; set; } = new List<string>();
        public List<string> PossibleBlockingMoves { get; set; } = new List<string>();
    }
}
