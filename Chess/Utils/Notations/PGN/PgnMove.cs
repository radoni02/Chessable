using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils.Notations.PGN
{
    internal class PgnMove
    {
        private char? FigureAbbreviation;
        private char? ColumnName;
        private int? RowNumber;
        private char? IsCapture;
        private string TargetField;
        private PgnPromotionMove? PgnPromotionMove;
        private char? SpecialSymbol;

        private const char CaptureSign = 'x';
        private const char CheckSign = '+';
        private const char CheckmateSign = '#';

        public PgnMove(char figureAbbreviation, string targetField, bool isCapture)
        {
            if (isCapture)
                this.IsCapture = CaptureSign;
            this.FigureAbbreviation = figureAbbreviation;
            this.TargetField = targetField;
        }
        public PgnMove(string targetField, bool isCapture)
        {
            if (isCapture)
                this.IsCapture = CaptureSign;
            this.TargetField = targetField;
        }

        public void SetPgnPromotionMove(char figureAbbreviation)
        {
            this.PgnPromotionMove = new PgnPromotionMove(figureAbbreviation);
        }

        public void SetColumnName(char columnName)
        {
            this.ColumnName = columnName;
        }

        public void SetRowNumber(int rowNumber)
        {
            this.RowNumber = rowNumber;
        }

        public void SetSpecialSymbolCheck()
        {
            this.SpecialSymbol = CheckSign;
        }

        public void SetSpecialSymbolCheckmateOrStalemate()
        {
            this.SpecialSymbol = CheckmateSign;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(this.FigureAbbreviation);
            builder.Append(this.ColumnName);
            builder.Append(this.RowNumber.ToString());
            builder.Append(this.IsCapture);
            builder.Append(this.TargetField);
            builder.Append(this.PgnPromotionMove);
            builder.Append(this.SpecialSymbol);
            return builder.ToString();
        }
    }
}
