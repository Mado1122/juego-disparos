using Raylib_cs;
using System.Numerics;

public class Player
{
    private Vector2 position = new Vector2(400, 500);
    private float speed = 300f;
    private const float Width = 30f;
    private const float Height = 40f;
    private const int ScreenWidth = 800;
    private const int ScreenHeight = 600;
    private float shootCooldown = 0f;
    private const float ShootInterval = 0.2f;

    public Vector2 Position => position;
    public float WidthSize => Width;
    public float HeightSize => Height;

    public Bullet? TryShoot()
    {
        float dt = Raylib.GetFrameTime();
        shootCooldown -= dt;
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && shootCooldown <= 0f)
        {
            shootCooldown = ShootInterval;
            return new Bullet(new Vector2(position.X, position.Y - Height / 2), true);
        }
        return null;
    }

    public void Update()
    {
        float dt = Raylib.GetFrameTime();

        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            position.X -= speed * dt;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            position.X += speed * dt;

        position.X = Math.Clamp(position.X, Width, ScreenWidth - Width);
        position.Y = Math.Clamp(position.Y, Height, ScreenHeight - Height);
    }

    public void Draw()
    {
        Raylib.DrawTriangle(
            new Vector2(position.X, position.Y - 20),
            new Vector2(position.X - 15, position.Y + 20),
            new Vector2(position.X + 15, position.Y + 20),
            Color.WHITE
        );
    }

    public Rectangle GetBounds()
    {
        return new Rectangle(position.X - Width / 2, position.Y - Height / 2, Width, Height);
    }
}

public class Bullet
{
    private Vector2 position;
    private float speed = 400f;
    private bool playerBullet;

    public Vector2 Position => position;
    public bool IsPlayerBullet => playerBullet;

    public Bullet(Vector2 start, bool fromPlayer)
    {
        position = start;
        playerBullet = fromPlayer;
    }

    public void Update()
    {
        float dt = Raylib.GetFrameTime();
        if (playerBullet)
            position.Y -= speed * dt;
        else
            position.Y += speed * dt;
    }

    public void Draw()
    {
        Color c = playerBullet ? Color.GREEN : Color.RED;
        Raylib.DrawRectangle((int)(position.X - 2), (int)(position.Y - 4), 4, 8, c);
    }

    public bool IsOffScreen()
    {
        return position.Y < -10 || position.Y > 610;
    }

    public Rectangle GetBounds() => new Rectangle(position.X - 2, position.Y - 4, 4, 8);
}

public class Enemy
{
    private Vector2 position;
    private float speed = 120f;
    private const float Size = 24f;

    public Vector2 Position => position;

    public Enemy(float x, float y)
    {
        position = new Vector2(x, y);
    }

    public void Update()
    {
        float dt = Raylib.GetFrameTime();
        position.Y += speed * dt;
    }

    public void Draw()
    {
        Raylib.DrawRectangle((int)(position.X - Size / 2), (int)(position.Y - Size / 2), (int)Size, (int)Size, Color.RED);
        Raylib.DrawRectangleLines((int)(position.X - Size / 2), (int)(position.Y - Size / 2), (int)Size, (int)Size, Color.MAROON);
    }

    public bool IsOffScreen() => position.Y > 620;

    public Rectangle GetBounds() => new Rectangle(position.X - Size / 2, position.Y - Size / 2, Size, Size);
}
