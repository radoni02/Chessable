using Chess.Chessboard;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils.Notations.FEN.Maps
{
    internal class CastlingDict
    {
        private const char WHITE_KINGSIDE_CASTLING = 'K';
        private const char WHITE_QUEENSIDE_CASTLING = 'Q';
        private const char BLACK_KINGSIDE_CASTLING = 'k';
        private const char BLACK_QUEENSIDE_CASTLING = 'q';

        public IDictionary<Castling, char> PossibleCastlings = new Dictionary<Castling, char>()
        {
            {new Castling(true,true),WHITE_KINGSIDE_CASTLING},
            {new Castling(true,false),WHITE_QUEENSIDE_CASTLING},
            {new Castling(false,true),BLACK_KINGSIDE_CASTLING},
            {new Castling(false,false),BLACK_QUEENSIDE_CASTLING}
        }.ToFrozenDictionary();
    }
    internal class Castling
    {
        public Castling(bool isWhite, bool isKingSide)
        {
            IsWhite = isWhite;
            IsKingSide = isKingSide;
        }

        public bool IsWhite { get; set; }
        public bool IsKingSide { get; set; }
    }

}
