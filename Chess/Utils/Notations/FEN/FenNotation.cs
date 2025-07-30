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
            Rows.Clear();
            for (int i = 0; i < 8; i++)
            {
                Rows.Add(new StringBuilder());
            }

            var rows = checkerboard.Board
                .SelectMany(ff => ff)
                .GroupBy(field => field.Row)
                .OrderBy(fields => fields.Key);
            foreach(var row in rows)
            {
                var orderedRow = row.OrderBy(field => field.Col);
                var emptyFields = 0;

                var fields = orderedRow.ToList();
                fields.Select(field =>
                {
                    if (field.IsUsed == false)
                        emptyFields++;
                    else
                    {
                        if (emptyFields != 0)
                        {
                            Rows[field.Row - 1].Append(char.Parse(emptyFields.ToString()));
                        }
                        Rows[field.Row - 1].Append(field.Figure.Name[0]);
                        emptyFields = 0;
                    }
                    if(field.Col == 8 && emptyFields != 0)
                    {
                        Rows[field.Row - 1].Append(char.Parse(emptyFields.ToString()));
                    }
                    return Rows[field.Row - 1];
                });
            }
        }
    }
}
