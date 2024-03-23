using NaiveECS.Example;

var gameExample = new GameExample();
gameExample.Initialize();

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
    
    gameExample.Run(deltaTime);

    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo key = Console.ReadKey(intercept: true);
        if (key.Key == ConsoleKey.Escape)
        {
            isRunning = false;
        }
    }

    Thread.Sleep(500);
}

Console.WriteLine("Update loop stopped. Press any key to exit...");
Console.ReadKey();