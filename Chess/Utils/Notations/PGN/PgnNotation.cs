using Chess.Chessboard;
using Chess.Figures;
using Chess.Utils.Notations.Maps;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils.Notations.PGN
{
    internal class PgnNotation
    {
        public List<PgnMove> PgnMoveHistory { get; set; } = new List<PgnMove>();
        
        public Checkerboard LoadFromPgn(List<string> moves)
        {
            var tempCheckerboard = new Checkerboard();
            foreach (var (move,index) in moves.Select((val,idx) => (val, idx)))
            {
                var isWhiteMove = index % 2 == 0 ? true : false;
                var possibleMove = ConvertPgnSingleMove(move, isWhiteMove, tempCheckerboard);
                //here need to call real Move from ChessGame
            }
            return tempCheckerboard;

        }

        private (Position, Field) ConvertPgnSingleMove(string pgnMove, bool isWhiteMove, Checkerboard checkerboard)
        {
            var specialSymbolsToDelete = new char[] {'#','+'};
            var lastPgnMovePart = pgnMove.Last();
            if(specialSymbolsToDelete.Any(symbol => symbol.Equals(lastPgnMovePart)))
                pgnMove = pgnMove.Substring(0, pgnMove.Length - 1);

            if(pgnMove.Contains('='))
            {
                //promotion and castling
                //return;
            }
            var targetPostion = GetTargetPosition(pgnMove);
            var baseField = GetBaseField(pgnMove, checkerboard, targetPostion, isWhiteMove);
            return (targetPostion, baseField);
        }

        private Field? GetBaseField(string pgnMove, Checkerboard checkerboard, Position targetPostion, bool isWhiteMove)
        {
            var baseFieldValues = pgnMove.Substring(0, pgnMove.Length - 2);
            var targetField = GetTargetField(checkerboard, targetPostion);
            var candidateFields = targetField.GetFiguresThatCanMoveToTheField(checkerboard, isWhiteMove);

            if (string.IsNullOrEmpty(baseFieldValues))
            {
                return candidateFields.FirstOrDefault(field => field.Figure.Name.Equals("Pawn"));
            }
            if(baseFieldValues.Last().Equals('x'))
            {
                baseFieldValues = baseFieldValues.Substring(0, baseFieldValues.Length - 1);
            }
            char potentialAbbreviation = ' ';
            if (char.IsUpper(baseFieldValues[0]))
            {
                potentialAbbreviation = baseFieldValues[0];
                baseFieldValues = baseFieldValues.Substring(1);
            }
            if(baseFieldValues.Length == 2)
            if(baseFieldValues.Length == 1 && char.IsLetter(baseFieldValues[0]) && potentialAbbreviation.Equals(' '))
            {
                var col = ColumnNameDict.ColumnNames.FirstOrDefault(c => c.Value.Equals(baseFieldValues[0])).Key;
                int.TryParse(baseFieldValues[1].ToString(), out var row);
                return candidateFields.FirstOrDefault(field => field.Col == col && field.Row == row);
            }
            if(baseFieldValues.Length != 1)
            {
                return null;
            }

            if (char.IsLetter(baseFieldValues[0]) && potentialAbbreviation.Equals(' '))
            {
                return FilterByColumn(candidateFields, "Pawn", baseFieldValues[0]);
            }
            else if(char.IsLetter(baseFieldValues[0]) && !potentialAbbreviation.Equals(' '))
            {
                var figure = FigureAbbreviation.FigureAbbreviationDict.FirstOrDefault(f => f.Value.Equals(potentialAbbreviation)).Key;
                return FilterByColumn(candidateFields, figure, baseFieldValues[0]);
            }
            else if(char.IsDigit(baseFieldValues[0]) && potentialAbbreviation.Equals(' '))
            {
                return FilterByRow(candidateFields, "Pawn", baseFieldValues[1]);
            }
            else if (char.IsDigit(baseFieldValues[0]) && !potentialAbbreviation.Equals(' '))
            {
                int.TryParse(baseFieldValues[1].ToString(), out var row);
                var figure = FigureAbbreviation.FigureAbbreviationDict.FirstOrDefault(f => f.Value.Equals(potentialAbbreviation)).Key;
                return FilterByRow(candidateFields, figure, baseFieldValues[1]);
            }
            return null;
        }

        private Field GetTargetField(Checkerboard checkerboard, Position targetPosition)
        {
            return checkerboard.Board
                .SelectMany(row => row)
                .FirstOrDefault(field => field.Row == targetPosition.Row && field.Col == targetPosition.Col)
                ?? throw new Exception("Target position not found");
        }

        private Field? FilterByColumn(IEnumerable<Field> candidateFields,string figureName ,char columnChar)
        {
            var column = ColumnNameDict.ColumnNames
                .FirstOrDefault(c => c.Value == columnChar).Key;

            return candidateFields.FirstOrDefault(field => field.Figure.Name.Equals(figureName)
               && field.Col == column);
        }

        private Field? FilterByRow(IEnumerable<Field> candidateFields,string figureName, char rowChar)
        {
            if (int.TryParse(rowChar.ToString(), out var row))
            {
                return candidateFields.FirstOrDefault(field => field.Figure.Name.Equals(figureName)
               && field.Row == row);
                return potentialBaseFields.FirstOrDefault(field => field.Col == col && field.Row == row);
            }
            return null;
        }

        private Position GetTargetPosition(string pgnMove)
        {
            var targetFieldValues = pgnMove.Substring(pgnMove.Length - 2);

            if (targetFieldValues.Length != 2)
                throw new Exception();//need to implement Result pattern

            var targetColumnName = ColumnNameDict.ColumnNames.FirstOrDefault(x => x.Value == targetFieldValues[0]).Key;
            return new Position(targetFieldValues[1], targetColumnName, Formatter.ChessFormat);
        }
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
