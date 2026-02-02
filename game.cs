using Raylib_cs;
using System.Numerics;

public class Game
{
    private Player player = new Player();
    private List<Bullet> bullets = new List<Bullet>();
    private List<Enemy> enemies = new List<Enemy>();
    private Random random = new Random();

    private float spawnTimer = 0f;
    private const float SpawnInterval = 1.2f;
    private const int ScreenWidth = 800;
    private const int ScreenHeight = 600;

    private int score = 0;
    private int lives = 3;
    private bool gameOver = false;

    private const string SoundsPath = "assets/sounds";
    private Sound? sndShoot;
    private Sound? sndExplosion;
    private Sound? sndPlayerHit;
    private Sound? sndGameOver;
    private Music? musBg;
    private bool soundsLoaded = false;

    public void Run()
    {
        LoadSounds();

        while (!Raylib.WindowShouldClose())
        {
            Update();
            Draw();
        }
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
                ResetGame();
            return;
        }

        player.Update();

        // Disparo
        var newBullet = player.TryShoot();
        if (newBullet != null)
        {
            bullets.Add(newBullet);
            PlaySound(ref sndShoot);
        }

        // Actualizar balas
        foreach (var b in bullets)
            b.Update();
        bullets.RemoveAll(b => b.IsOffScreen());

        // Spawn enemigos
        spawnTimer -= Raylib.GetFrameTime();
        if (spawnTimer <= 0f)
        {
            spawnTimer = SpawnInterval;
            float x = 50 + random.NextSingle() * (ScreenWidth - 100);
            enemies.Add(new Enemy(x, -20));
        }

        // Actualizar enemigos
        foreach (var e in enemies)
            e.Update();
        enemies.RemoveAll(e => e.IsOffScreen());

        // Colisiones: bala jugador vs enemigo
        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            if (!bullets[i].IsPlayerBullet) continue;
            var br = bullets[i].GetBounds();
            for (int j = enemies.Count - 1; j >= 0; j--)
            {
                if (Raylib.CheckCollisionRecs(br, enemies[j].GetBounds()))
                {
                    bullets.RemoveAt(i);
                    enemies.RemoveAt(j);
                    score += 10;
                    PlaySound(ref sndExplosion);
                    break;
                }
            }
        }

        // Colisión: nave vs enemigo
        var playerBounds = player.GetBounds();
        for (int j = enemies.Count - 1; j >= 0; j--)
        {
            if (Raylib.CheckCollisionRecs(playerBounds, enemies[j].GetBounds()))
            {
                enemies.RemoveAt(j);
                lives--;
                PlaySound(ref sndPlayerHit);
                if (lives <= 0)
                {
                    gameOver = true;
                    PlaySound(ref sndGameOver);
                }
                break;
            }
        }
    }

    private void LoadSounds()
    {
        if (soundsLoaded) return;
        soundsLoaded = true;

        if (File.Exists($"{SoundsPath}/shoot.wav"))
            sndShoot = Raylib.LoadSound($"{SoundsPath}/shoot.wav");
        if (File.Exists($"{SoundsPath}/explosion.wav"))
            sndExplosion = Raylib.LoadSound($"{SoundsPath}/explosion.wav");
        if (File.Exists($"{SoundsPath}/player_hit.wav"))
            sndPlayerHit = Raylib.LoadSound($"{SoundsPath}/player_hit.wav");
        if (File.Exists($"{SoundsPath}/game_over.wav"))
            sndGameOver = Raylib.LoadSound($"{SoundsPath}/game_over.wav");
        if (File.Exists($"{SoundsPath}/music.ogg"))
        {
            musBg = Raylib.LoadMusicStream($"{SoundsPath}/music.ogg");
            Raylib.PlayMusicStream(musBg.Value);
        }
    }

    private void PlaySound(ref Sound? snd)
    {
        if (snd.HasValue)
            Raylib.PlaySound(snd.Value);
    }

    private void Draw()
    {
        if (musBg.HasValue)
            Raylib.UpdateMusicStream(musBg.Value);

        Raylib.BeginDrawing();
        Raylib.ClearBackground(new Color(15, 15, 35, 255));

        player.Draw();
        foreach (var b in bullets)
            b.Draw();
        foreach (var e in enemies)
            e.Draw();

        // UI
        Raylib.DrawText($"Score: {score}", 12, 12, 20, Color.WHITE);
        Raylib.DrawText($"Lives: {lives}", 12, 36, 20, Color.WHITE);

        if (gameOver)
        {
            Raylib.DrawRectangle(0, 0, ScreenWidth, ScreenHeight, new Color(0, 0, 0, 180));
            Raylib.DrawText("GAME OVER", ScreenWidth / 2 - 120, ScreenHeight / 2 - 40, 40, Color.RED);
            Raylib.DrawText($"Final Score: {score}", ScreenWidth / 2 - 80, ScreenHeight / 2 + 10, 24, Color.WHITE);
            Raylib.DrawText("Press R to restart", ScreenWidth / 2 - 100, ScreenHeight / 2 + 50, 20, Color.LIGHTGRAY);
        }

        Raylib.EndDrawing();
    }

    private void ResetGame()
    {
        player = new Player();
        bullets.Clear();
        enemies.Clear();
        score = 0;
        lives = 3;
        gameOver = false;
        spawnTimer = 0f;
    }
}
