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

            if (baseField is null || baseField.Figure is null || targetField is null || targetField.Figure is null)
            {
                throw new Exception("Unnable to find postion fields");
            }
            if(baseField.Figure is Pawn)
            {
                ProcessPawnMove();
            }
            else
            {
                ProcessPieceMove(checkerboard, baseField, targetField);
            }
        }

        private void ProcessPawnMove()
        {

        }

        private void ProcessPieceMove(Checkerboard checkerboard, Field baseField, Field targetField)
        {
            var fieldsFigureCanMoveToTargetField = targetField.Figure!.GetFiguresThatCanMoveToTheField(checkerboard, targetField, baseField.Figure!.IsWhite);
            var sameFigureCanMoveToTargetField = fieldsFigureCanMoveToTargetField
               .Where(field => field != baseField)
               .FirstOrDefault(field => field.Figure.Name == baseField.Figure.Name);
            if (sameFigureCanMoveToTargetField == null)
                PgnMoveHistory.Add(CreateBasicPgnMove(baseField, targetField));


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
    }

    public class PgnMove
    {
        private char? FigureAbbreviation;
        private char? ColumnName;
        private char? RowNumber;
        private char? IsCapture;
        private string TargetField;
        private char? SpecialSymbol;

        private const char CaptureSign = 'x';

        public PgnMove(char figureAbbreviation, string targetField, bool isCapture)
        {
            if (isCapture)
                this.IsCapture = CaptureSign;
            this.FigureAbbreviation = figureAbbreviation;
            this.TargetField = targetField;
        }
        public PgnMove( string targetField, bool isCapture)
        {
            if(isCapture)
                this.IsCapture= CaptureSign;
            this.TargetField = targetField;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(this.FigureAbbreviation);
            builder.Append(this.ColumnName);
            builder.Append(this.RowNumber);
            builder.Append(this.IsCapture);
            builder.Append(this.TargetField);
            builder.Append(this.SpecialSymbol);
            return builder.ToString();
        }
    }
}
