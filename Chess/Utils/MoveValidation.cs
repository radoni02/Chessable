using Chess.Chessboard;
using Chess.Utils.ChessPlayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils
{
    internal static class MoveValidation
    {
        public static bool ValidateInput(ParseInputResult parsedInput, GameStateModel gameState)
        {
            return MoveValidation.ValidationTemplate(!parsedInput.Valid, gameState.SetInvalidInputError);
        }

        public static bool ValidateKingInCheck(CheckmateAnalysisResult checkmateResult, Field currentField, GameStateModel gameState)
        {
            return MoveValidation.ValidationTemplate(
                checkmateResult.IsInCheck &&
                currentField.Figure is not null &&
                !currentField.Figure.Name.Equals("King"), gameState.SetKingInCheckError);
        }

        public static bool ValidateFieldUsage(Field currentField, GameStateModel gameState)
        {
            return MoveValidation.ValidationTemplate(!currentField.IsUsed, gameState.SetEmptyFieldError);
        }

        internal static bool ValidatePlayerTurn(Player currentPlayer, Field currentField, GameStateModel gameState)
        {
            return MoveValidation.ValidationTemplate(currentPlayer.IsWhite != currentField.Figure.IsWhite, gameState.SetWrongColorFigureError);
        }
        private static bool ValidationTemplate(bool check,Action errorAction)
        {
            if(check)
            {
                errorAction();
                return false;
            }
            return true;
        }
    }
}
