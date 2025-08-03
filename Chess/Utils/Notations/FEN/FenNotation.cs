using Chess.Chessboard;
using Chess.Figures;
using Chess.Utils.ChessPlayer;
using Chess.Utils.Notations.FEN.Maps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils.Notations.FEN
{
    internal class FenNotation
    {
        private const char WhiteFigure = 'w';
        private const char BlackFigure = 'b';
        public FenNotation()
        {
            for (int i = 0; i < 8; i++)
            {
                Rows.Add(new StringBuilder());
            }
        }
        private List<StringBuilder> Rows = new List<StringBuilder>(8);
        private char NextMove;
        private StringBuilder PossibleCastling = new StringBuilder();
        private string PossiblePassant;
        private UInt16 HalfMoveClock;
        private uint FullMoveNumber;

        public string GetCurrentPosition(Checkerboard checkerboard, Player nextPlayer, uint fullMoveCounter)
        {
            CalculatePiecePlacement(checkerboard);
            CalculateNextMove(nextPlayer);
            CalculatePossibleCastlings(checkerboard);
            FullMoveNumber = fullMoveCounter;
            var builder = new StringBuilder();
            builder.AppendJoin('/', Rows);
            builder.Append(' ');
            builder.Append(NextMove);
            builder.Append(' ');
            builder.Append(PossibleCastling);
            builder.Append(' ');

            builder.Append(' ');

            builder.Append(' ');
            builder.Append(FullMoveNumber);
            return builder.ToString();
        }

        private void CalculatePossibleCastlings(Checkerboard checkerboard)
        {
            var castlingDict = new CastlingDict();
            var rookFields = checkerboard.GetPossibleCastlings()
                .OrderByDescending(field => field.Figure.IsWhite)
                .ThenByDescending(field => field.Col);

            foreach (var rook in rookFields)
            {
                var castKey = new Castling(rook.Figure.IsWhite,rook.Col == 8 ? true : false);
                castlingDict.PossibleCastlings.TryGetValue(castKey, out char value);
                PossibleCastling.Append(value);
            }
            if(PossibleCastling.Length == 0)
                PossibleCastling.Append('-');
        }

        private void CalculateNextMove(Player nextPlayer)
        {
            NextMove = nextPlayer.Color == PlayerColor.White ? WhiteFigure : BlackFigure;
        }

        private void CalculatePiecePlacement(Checkerboard checkerboard)
        {
            var rows = checkerboard.Board
                .SelectMany(ff => ff)
                .GroupBy(field => field.Row)
                .OrderBy(fields => fields.Key);

            foreach(var row in rows)
            {
                var orderedRow = row
                    .OrderBy(field => field.Col);

                var emptyFieldsCounter = 0;

                foreach (var field in orderedRow)
                {
                    var matrixNorationRow = field.Row - 1;
                    if (!field.IsUsed)
                        emptyFieldsCounter++;
                    else
                    {
                        AppendEmptyFields(matrixNorationRow,emptyFieldsCounter);
                        FigureAbbreviation.FigureAbbreviationDict.TryGetValue(field.Figure!.Name, out var nameChar);
                        var figureCharacter = field.Figure.IsWhite ? nameChar : char.ToLower(nameChar);
                        Rows[matrixNorationRow].Append(figureCharacter);
                        emptyFieldsCounter = 0;
                    }
                }
                AppendEmptyFields(row.Last().Row - 1, emptyFieldsCounter);
            }
            void AppendEmptyFields(int row,int emptyFieldsCounter)
            {
                if (emptyFieldsCounter > 0)
                {
                    Rows[row].Append(emptyFieldsCounter.ToString());
                }
            }
        }
    }
}
