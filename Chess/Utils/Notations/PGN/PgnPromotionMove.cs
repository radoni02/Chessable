using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils.Notations.PGN
{
    internal class PgnPromotionMove
    {
        private char FigureAbbreviation;
        
        private const char PromotionSign = '=';

        public PgnPromotionMove(char figureAbbreviation)
        {
            this.FigureAbbreviation = figureAbbreviation;
        }

        public override string ToString()
        {
            return $"{PromotionSign}{FigureAbbreviation}";
        }
    }
}
