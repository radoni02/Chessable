using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils;

internal class Player
{
    public Player(int score, bool isWhite, bool hasNextMove)
    {
        Score = score;
        IsWhite = isWhite;
        HasNextMove = hasNextMove;
    }

    public int Score { get; private set; }
    public bool IsWhite { get; private set; }

    public bool HasNextMove { get; private set; }

    public void AddScore(int newScore)
    {
        Score += newScore;
    }

}
