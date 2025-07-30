using Chess.Chessboard;
using Chess.Utils.ChessPlayer;
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
        public FenNotation()
        {
            for (int i = 0; i < 8; i++)
            {
                Rows.Add(new StringBuilder());
            }
        }
        private List<StringBuilder> Rows = new List<StringBuilder>(8);
        private char NextMove;
        private string PossibleCastling;
        private string PossiblePassant;
        private UInt16 HalfMoveClock;
        private uint FullMoveNumber;

        public string GetCurrentPosition(Checkerboard checkerboard, Player nextPlayer)
        {
            CalculatePiecePlacement(checkerboard);
            CalculateNextMove(nextPlayer);
            return "";
        }

        private void CalculateNextMove(Player nextPlayer)
        {
            NextMove = nextPlayer.Color == PlayerColor.White ? 'w' : 'b';
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
                        Rows[matrixNorationRow].Append(field.Figure.Name[0]);
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
