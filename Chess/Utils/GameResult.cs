using Chess.Utils.ChessPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils
{
    public enum GameResultType
    {
        Draw = 0,
        Win = 1,
    }

    public class GameResult
    {
        public GameResult(GameResultType type, PlayerColor? color)
        {
            Type = type;
            Color = color;
        }
        public GameResult(GameResultType type)
        {
            Type = type;
        }

        public GameResultType Type { get; init; }
        public PlayerColor? Color { get; init; }
    }

}
