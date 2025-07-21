using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chess.Utils.ChessPlayer;

internal class Player
{
    public Player(int score, PlayerColor color, bool hasNextMove)
    {
        Score = score;
        IsWhite = color.Equals(PlayerColor.White) ? true : false;
        Color = color;
        HasNextMove = hasNextMove;
    }

    public int Score { get; private set; }
    public bool IsWhite { get; private set; }
    public PlayerColor Color { get; private set; }

    public bool HasNextMove { get; private set; }

    public void AddScore(int newScore)
    {
        Score += newScore;
    }
}
