using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        if (!GameLogic.ValidateArgs(args))
        {
            Console.WriteLine("Ошибка: Пожалуйста, укажите нечётное количество (≥ 3) уникальных ходов.");
            return;
        }

        var game = new GameLogic(args);
        var uiManager = new UIManager(game);
        uiManager.RunGameLoop();
    }
}
