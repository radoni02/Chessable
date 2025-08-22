using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils
{
    internal class ParseInputResult
    {
        public ParseInputResult(bool valid)
        {
            Valid = valid;
        }

        public ParseInputResult(Position currentPosition,Position targetPosition,bool valid)
        {
            CurrentPosition = currentPosition;
            TargetPosition = targetPosition;
            Valid = valid;
        }

        public Position? CurrentPosition { get; private set; }
        public Position? TargetPosition { get; private set; }
        public bool Valid { get; private set; }
    }
}
