using System;
using System.Collections.Generic;

public interface ICommand
{
    void Execute();
}

public class CreateGameCommand : ICommand
{
    private ChessGame game;

    public CreateGameCommand(ChessGame game)
    {
        this.game = game;
    }

    public void Execute()
    {
        game.CreateNewGame();
    }
}

public class MakeMoveCommand : ICommand
{
    private ChessGame game;
    private int startX;
    private int startY;
    private int endX;
    private int endY;

    public MakeMoveCommand(ChessGame game, int startX, int startY, int endX, int endY)
    {
        this.game = game;
        this.startX = startX;
        this.startY = startY;
        this.endX = endX;
        this.endY = endY;
    }

    public void Execute()
    {
        game.MakeMove(startX, startY, endX, endY);
    }
}

public class UndoMoveCommand : ICommand
{
    private ChessGame game;

    public UndoMoveCommand(ChessGame game)
    {
        this.game = game;
    }

    public void Execute()
    {
        game.UndoLastMove();
    }
}

public class ChessGame
{

    public void CreateNewGame()
    {
        Console.WriteLine("New game created.");
    }

    public void MakeMove(int startX, int startY, int endX, int endY)
    {
        Console.WriteLine($"Move from ({startX}, {startY}) to ({endX}, {endY}) executed.");
    }

    public void UndoLastMove()
    {
        Console.WriteLine("Last move undone.");
    }
}

public class Client
{
    private List<ICommand> commands = new List<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        commands.Add(command);
        command.Execute();
    }

    public void UndoLastCommand()
    {
        if (commands.Count > 0)
        {
            ICommand lastCommand = commands[commands.Count - 1];
            commands.RemoveAt(commands.Count - 1);
            Console.WriteLine("Last command undone.");
        }
        else
        {
            Console.WriteLine("No commands to undo.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ChessGame chessGame = new ChessGame();
        Client client = new Client();

        ICommand createGameCommand = new CreateGameCommand(chessGame);
        client.ExecuteCommand(createGameCommand);

        ICommand makeMoveCommand = new MakeMoveCommand(chessGame, 1, 2, 3, 4);
        client.ExecuteCommand(makeMoveCommand);

        client.UndoLastCommand();
    }
}
