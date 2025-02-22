using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils
{
    public class Player
    {
        public Player(int score, bool isWhite, bool hasNextMove)
        {
            Score = score;
            IsWhite = isWhite;
            HasNextMove = hasNextMove;
        }

        public int Score { get; set; }
        public bool IsWhite { get; set; }

        public bool HasNextMove { get; set; }
    }
}
