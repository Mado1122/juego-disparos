using Raylib_cs;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(800, 600, "Space Shooter");
        Raylib.InitAudioDevice();
        Raylib.SetTargetFPS(60);

        Game game = new Game();
        game.Run();

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }
}
