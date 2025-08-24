using Chess.Chessboard;
using Chess.Figures;
using Chess.Utils.Notations.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils.Notations.PGN
{
    internal class PgnNotation
    {
        public List<PgnMove> PgnMoveHistory { get; set; } = new List<PgnMove>();
        
        public void ConvertToPgn(List<PossibleMove> moveHistory)
        {
            var tempBoard = new Checkerboard();
            foreach (var move in moveHistory)
            {
                ProcessSingleMoveToPgn(tempBoard, move);
            }
        }

        public void ConvertSingleMoveToPgn(Checkerboard checkerboard, PossibleMove possibleMove)
        {
            ProcessSingleMoveToPgn(checkerboard, possibleMove);
        }

        private void ProcessSingleMoveToPgn(Checkerboard checkerboard, PossibleMove move)
        {
            var baseField = checkerboard.Board
                .SelectMany(row => row)
                .FirstOrDefault(field => field.Row == move.BasePosition.Row
                && field.Col == move.BasePosition.Col);

            var targetField = checkerboard.Board
                .SelectMany(row => row)
                .FirstOrDefault(field => field.Row == move.TargetPosition.Row
                && field.Col == move.TargetPosition.Col);

            if (baseField is null || baseField.Figure is null || targetField is null)
            {
                throw new Exception("Unnable to find postion fields");
            }
            PgnMove? pgnMove = null;
            if(baseField.Figure is Pawn)
            {
                pgnMove = ProcessPawnMove(baseField, targetField);
            }
            else
            {
                pgnMove = ProcessPieceMove(checkerboard, baseField, targetField);
            }
            SpecialSymbolCheck(checkerboard,baseField, targetField, pgnMove);
            PgnMoveHistory.Add(pgnMove);
        }

        private PgnMove ProcessPawnMove(Field baseField, Field targetField)
        {
            var pgnMove = CreateBasicPgnMove(baseField, targetField);
            if(targetField.IsUsed)
            {
                ColumnNameDict.ColumnNames.TryGetValue(baseField.Col, out var columnNameResult);
                pgnMove.SetColumnName(columnNameResult);
            }
            return pgnMove;
        }

        private PgnMove ProcessPieceMove(Checkerboard checkerboard, Field baseField, Field targetField)
        {
            var fieldsFigureCanMoveToTargetField = targetField.GetFiguresThatCanMoveToTheField(checkerboard, baseField.Figure.IsWhite);
            var sameFigureCanMoveToTargetField = fieldsFigureCanMoveToTargetField
               .Where(field => field != baseField)
               .Where(field => field.Figure.Name == baseField.Figure.Name);
            if (!sameFigureCanMoveToTargetField.Any())
            {
                return CreateBasicPgnMove(baseField, targetField);
            }

            if(sameFigureCanMoveToTargetField.Any(field => field.Col == baseField.Col) 
                && sameFigureCanMoveToTargetField.Any(field => field.Row == baseField.Row))
            {
                return CreatePgnMoveForSameFigureAttactTarget(baseField, targetField, true, true);
            }

            if(!sameFigureCanMoveToTargetField.Any(field => field.Col == baseField.Col))
                return CreatePgnMoveForSameFigureAttactTarget(baseField, targetField);
            else
                return CreatePgnMoveForSameFigureAttactTarget(baseField, targetField, true);
        }

        private PgnMove CreatePgnMoveForSameFigureAttactTarget(Field baseField, Field targetField, bool sameColumn = false, bool sameRow = false)
        {
            var pgnMove = CreateBasicPgnMove(baseField, targetField);
            if(sameColumn && sameRow)
            {
                ColumnNameDict.ColumnNames.TryGetValue(baseField.Col, out var columnNameResult);
                pgnMove.SetColumnName(columnNameResult);
                pgnMove.SetRowNumber(baseField.Row);
                return pgnMove;
            }
            if(!sameColumn)
            {
                ColumnNameDict.ColumnNames.TryGetValue(baseField.Col, out var columnNameResult);
                pgnMove.SetColumnName(columnNameResult);
                return pgnMove;
            }
            if(sameColumn)
            {
                pgnMove.SetRowNumber(baseField.Row);
                return pgnMove;
            }
            return pgnMove;
        }

        private PgnMove CreateBasicPgnMove(Field baseField, Field targetField)
        {
            ColumnNameDict.ColumnNames.TryGetValue(targetField.Col, out var columnNameResult);
            var targetFieldNotationResult = $"{columnNameResult}{targetField.Row}";
            if (baseField.Figure is not Pawn)
            {
                FigureAbbreviation.FigureAbbreviationDict.TryGetValue(baseField.Figure.Name, out var abbreviationResult);
                return new PgnMove(abbreviationResult, targetFieldNotationResult, targetField.IsUsed);
            }
            return new PgnMove(targetFieldNotationResult, targetField.IsUsed);
        }

        private void SpecialSymbolCheck(Checkerboard checkerboard,Field baseField, Field targetField, PgnMove move)
        {
            var matrixPosition = new Position(targetField.Row - 1, targetField.Col - 1, Formatter.MatrixFormat);
            baseField.Figure.Move(checkerboard, baseField, matrixPosition);

            var oppKing = baseField.Figure.GetOppositKing(checkerboard);
            if(oppKing is null)
            {
                throw new Exception("Unnable to create Pgn");
            }
            var result = GameStateAnalyzer.AnalizeGameState(checkerboard, oppKing);
            if (result.IsCheckmate || result.IsInStalemate)//checkmate need to be first cuz when King is in checkmate also is in check
                move.SetSpecialSymbolCheckmateOrStalemate();
            else if (result.IsInCheck)
                move.SetSpecialSymbolCheck();
            
        }
    }
}
