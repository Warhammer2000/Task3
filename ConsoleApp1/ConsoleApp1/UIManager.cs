using BetterConsoleTables;
class UIManager
{
    private readonly GameLogic gameLogic;
  
    public UIManager(GameLogic gameLogic)
    {
        this.gameLogic = gameLogic;
    }

    public void RunGameLoop()
    {
        Console.WriteLine($"HMAC: {gameLogic.HMAC}");
        DisplayMoves();

        while (true)
        {
            Console.WriteLine("Введите ваш ход:");
            string? input = Console.ReadLine();
            if (input == "?")
            {
                DisplayHelp();
                DisplayMoves();
                continue;
            }
            if (int.TryParse(input, out int userChoice) && userChoice >= 0 && userChoice <= gameLogic.MovesCount)
            {
                if (userChoice == 0) break;
                PlayRound(userChoice - 1);
            }
            else
            {
                Console.WriteLine("Неверный ввод. Попробуйте ещё раз.");
                DisplayMoves();
            }
        }
    }

    private void DisplayMoves()
    {
        Console.WriteLine("Доступные ходы:");
        for (int i = 0; i < gameLogic.MovesCount; i++)
        {
            Console.WriteLine($"{i + 1} - {gameLogic.GetMove(i)}");
        }
        Console.WriteLine("0 - выход");
        Console.WriteLine("? - помощь");

    }

    private void PlayRound(int userMove)
    {
        int computerMove = gameLogic.ComputerMove;
        Console.WriteLine($"Ваш ход: {gameLogic.GetMove(userMove)}");
        Console.WriteLine($"Ход компьютера: {gameLogic.GetMove(computerMove)}");

        int n = gameLogic.Moves.Length;
        int winRange = n / 2;
        int diff = (computerMove - userMove + n) % n;

        if (diff == 0)
        {
            Console.WriteLine("Ничья!");
        }
        else if (diff <= winRange)
        {
            Console.WriteLine("Компьютер выиграл!");
        }
        else
        {
            Console.WriteLine("Вы выиграли!");
        }

        if (gameLogic.Key != null)
        {
            Console.WriteLine($"HMAC ключ: {BitConverter.ToString(gameLogic.Key).Replace("-", "")}");
        }
        else
        {
            Console.WriteLine("HMAC ключ: не доступен");
        }
    }


    public void DisplayHelp()
    {
        var table = new Table();
        table.Config = TableConfiguration.UnicodeAlt();

        table.AddColumn("v PC\\User >");
        foreach (var move in gameLogic.Moves)
        {
            table.AddColumn(move);
        }

        int n = gameLogic.Moves.Length;
        for (int i = 0; i < n; i++)
        {
            var row = new string[n + 1];
            row[0] = gameLogic.Moves[i];

            for (int j = 0; j < n; j++)
            {
                if (i == j)
                {
                    row[j + 1] = "Draw";
                }
                else
                {
                    int winRange = n / 2;
                    int diff = (j - i + n) % n;
                    row[j + 1] = (diff > 0 && diff <= winRange) ? "Win" : "Lose";
                }
                
            }

            table.AddRow(row);
        }
        Console.WriteLine("Помощь - Условия победы:");
        Console.WriteLine(table.ToString());
    }
}

