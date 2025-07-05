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

        public ParseInputResult(string currentPosition,string targetPosition,bool valid)
        {
            CurrentPosition = currentPosition;
            TargetPosition = targetPosition;
            Valid = valid;
        }

        public string? CurrentPosition { get; set; }
        public string? TargetPosition { get; set; }
        public bool Valid { get; set; }
    }
}
