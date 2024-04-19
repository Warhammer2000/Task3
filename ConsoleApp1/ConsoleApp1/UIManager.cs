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
        int pageSize = 5;
        int totalPages = (int)Math.Ceiling(gameLogic.Moves.Length / (double)pageSize);
        int currentPage = 0;

        Console.Clear();
        Console.WriteLine("Помощь - Условия победы:");
        Console.WriteLine("+-------------+" + string.Concat(Enumerable.Repeat("--------+", pageSize)));


        Console.Write("| v PC\\User > |");
        for (int j = currentPage * pageSize; j < Math.Min((currentPage + 1) * pageSize, gameLogic.Moves.Length); j++)
        {
            Console.Write($" {gameLogic.Moves[j],-6} |");
        }
        Console.WriteLine();
        Console.WriteLine("+-------------+" + string.Concat(Enumerable.Repeat("--------+", pageSize)));

        for (int i = 0; i < gameLogic.Moves.Length; i++)
        {
            Console.Write($"| {gameLogic.Moves[i],-11} |");
            for (int j = currentPage * pageSize; j < Math.Min((currentPage + 1) * pageSize, gameLogic.Moves.Length); j++)
            {
                if (i == j)
                {
                    Console.Write(" Draw  |");
                }
                else
                {
                    int n = gameLogic.Moves.Length;
                    int winRange = n / 2;
                    int diff = (j - i + n) % n;
                    Console.Write((diff <= winRange && diff != 0) ? " Win   |" : " Lose  |");
                }
            }
            Console.WriteLine();
            Console.WriteLine("+-------------+" + string.Concat(Enumerable.Repeat("--------+", pageSize)));
        }

    }


}