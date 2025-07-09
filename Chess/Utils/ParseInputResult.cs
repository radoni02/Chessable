using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils
{
    public class ParseInputResult
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

        public Position? CurrentPosition { get; set; }
        public Position? TargetPosition { get; set; }
        public bool Valid { get; set; }
    }
}
