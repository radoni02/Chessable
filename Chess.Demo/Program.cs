using Chess.Public;
using Chess.Utils;

Chessable chessable = new Chessable();
GameResult? gameResult = null;
while (gameResult is null)
{
    chessable.ShowPosition();

    Console.WriteLine("Provide move in format \"a2-a3\":");
    var move = Console.ReadLine();

    var gameState = chessable.MakeMove(move);
    if (!gameState.IsValidMove)
        Console.WriteLine(gameState.ErrorMessage);
    gameResult = chessable.GameResult(gameState);
}
