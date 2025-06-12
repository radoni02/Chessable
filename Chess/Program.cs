// See https://aka.ms/new-console-template for more information
using Chess.Chessboard;
using Chess.Figures;
using Chess.Utils;
using System.Runtime.CompilerServices;
using System.Text;

var board = new Checkerboard();

var whitePlayer = new Player(0, true,true);
var blackPlayer = new Player(0, false,false);

var changePlayer = new Dictionary<Player, Player>()
{ 
    {whitePlayer,blackPlayer },
    {blackPlayer,whitePlayer } 
};

var currentPlayer = whitePlayer;

while(true)
{
    ShowNewPosition();
    UsedFields();

    Console.WriteLine("Provide move in format \"a2-a3\":");
    var move = Console.ReadLine();
    var parsedInput = ParseMoveInput(move);

    var currPosition = CalculatePositionOnChessboard(parsedInput.Item1);
    var currentField = board.Board.SelectMany(f => f)
        .FirstOrDefault(field => field.Col == currPosition.Col && field.Row == currPosition.Row);

    if(!currentField.IsUsed)
    {
        Console.WriteLine("Choosen empty field");
        continue;
    }

    if (currentPlayer.IsWhite && currentPlayer.IsWhite != currentField.Figure.IsWhite)
    {
        Console.WriteLine("Choosen wrong color figure");
        continue;
    }

    if (!currentPlayer.IsWhite && currentPlayer.IsWhite != currentField.Figure.IsWhite)
    {
        Console.WriteLine("Choosen wrong color figure");
        continue;
    }

    var possibleMoves = currentField.Figure.PossibleMoves(board, currentField);

    var targetPosition = CalculateTargetPosition(parsedInput.Item2);
    var targetMove = targetPosition.ToString();
    foreach (var possibleMove in possibleMoves)
    {
        if (possibleMove.Equals(targetMove))
        {
            currentField.Figure.Move(board, currentField, targetPosition);

            var oppKingField = currentField.Figure.GetOppositKing(board);
            if(oppKingField.Figure.CheckIfFigureIsUnderAttack(board))
            {
                var possibileCheckmateRescueFields = oppKingField.Figure.PossibleMoves(board, oppKingField);
                var figuresThatAttackKing = oppKingField.Figure.GetListOfFieldsAttackingTarget(board);
                foreach(var figure in figuresThatAttackKing)
                {
                    oppKingField.Figure.GetListOfFieldsThatAreBetweenCurrentAndTarget(board, oppKingField, figure);
                }
            }

            changePlayer.TryGetValue(currentPlayer,out currentPlayer);
            break;
        }
    }
}


void ShowNewPosition()
{
    var valueBeetwenFields = 10;
    foreach (var field in board.Board)
    {
        foreach (var inner in field)
        {
            var gap = valueBeetwenFields - (CheckFigure(inner).Length);
            Console.Write($"{CheckFigure(inner)}{ConvertIntoGap(gap)}");
        }
        Console.WriteLine();
    }

    string CheckFigure(Field inner)
        => inner.Figure != null ? inner.Figure.Name : "Empty";

}

string ConvertIntoGap(int length)
{
    StringBuilder sb = new StringBuilder();
    while(length > 0)
    {
        sb.Append(" ");
        length--;
    }
    return sb.ToString();
}

(string,string) ParseMoveInput(string input)
{
    var positions = input.Split('-');
    var currentPossition = positions[0];
    var targetPosition = positions[1];
    return (currentPossition, targetPosition);
}

Position CalculatePositionOnChessboard(string position) // in format "a2" to format enumerable from 1  custom => (col, row)
{
    var dict = new Dictionary<char, int>()
    {
        {'a',1},
        {'b',2},
        {'c',3},
        {'d',4},
        {'e',5},
        {'f',6},
        {'g',7},
        {'h',8}
    };
    dict.TryGetValue(position.First(), out var value);
    var row = (int)char.GetNumericValue(position.Last());
    return new Position(row, value);
}

Position CalculateTargetPosition(string position) // in format "a2" to format enumerable from 0 default table => [col][row]
{
    var dict = new Dictionary<char, int>()
    {
        {'a',0},
        {'b',1},
        {'c',2},
        {'d',3},
        {'e',4},
        {'f',5},
        {'g',6},
        {'h',7}
    };
    dict.TryGetValue(position.First(), out var value);
    var row = (int)char.GetNumericValue(position.Last());
    return new Position(row - 1, value);
}

void UsedFields()
{
    board.Board.SelectMany(fl => fl)// to increase performace shadowProperty = true should be applied only when figure is used and based on that .Where(field => field.figure.wasUsedInCurrentMove)
        .Where(field => field.Figure != null)
        .ToList()
        .ForEach(field => field.Figure.AttackedFields.Clear());
    var usedWhiteFields = board.Board.SelectMany(f => f)
    .Where(field => field.IsUsed && field.Figure.IsWhite);
    foreach(var field in usedWhiteFields)
    {
        field.Figure.CalculateAtackedFields(board, field);
    }
    
    var usedBlackFields = board.Board.SelectMany(f => f)
    .Where(field => field.IsUsed && !field.Figure.IsWhite);
    foreach (var field in usedBlackFields)
    {
        field.Figure.CalculateAtackedFields(board, field);
    }
}
