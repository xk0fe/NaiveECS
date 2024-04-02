﻿using NaiveECS.Example.Interfaces;

namespace NaiveECS.Example;

public class ConsoleGame
{
    public ConsoleGame(IBootable game)
    {
        game.Boot();

        var lastFrameTime = DateTime.Now;
        var isRunning = true;
        Console.CursorVisible = false;

        while (isRunning)
        {
            Console.SetCursorPosition(0, 0);

            var currentFrameTime = DateTime.Now;
            var elapsedTime = currentFrameTime - lastFrameTime;
            var deltaTime = (float)elapsedTime.TotalSeconds;
            lastFrameTime = currentFrameTime;
        
            game.Run(deltaTime);

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Escape)
                {
                    isRunning = false;
                }
            }

            Thread.Sleep(500);
        }

        Console.WriteLine("Update loop stopped. Press any key to exit...");
        Console.ReadKey();
    }
}