using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallManager
{
    private readonly BallSpawner _spawner;
    private readonly List<Ball> _activeBalls = new();

    public BallSpeed Speed { get; private set; }
    
    public int BallCount => _activeBalls.Count;
    public event Action OnAllBallsLost;

    public BallManager(BallSpawner spawner, float speed = 10f)
    {
        _spawner = spawner;

        Speed = new BallSpeed(speed);
    }

    public Ball SpawnBall(Vector3 position)
    {
        Ball ball = _spawner.Spawn(position);
        
        ball.Init(Speed);

        _activeBalls.Add(ball);
        return ball;
    }

    public void RemoveBall(Ball ball)
    {
        if (ball == null || !_activeBalls.Contains(ball)) return;

        _activeBalls.Remove(ball);
        ball.ReturnToPool();

        if (_activeBalls.Count == 0)
            OnAllBallsLost?.Invoke();
    }

    public void LaunchAllBalls(Vector2 direction)
    {
        foreach (var ball in _activeBalls)
            ball.Launch(direction);
    }

    public void Clear()
    {
        foreach (var ball in _activeBalls)
            ball.ReturnToPool();

        _activeBalls.Clear();
    }

    public void MultiplyBalls(int count)
    {
        if (_activeBalls.Count == 0) return;

        List<Ball> originalBalls = new List<Ball>(_activeBalls).Where(b => b.IsLaunched).ToList();

        foreach (var ball in originalBalls)
        {
            Vector2 baseDirection = ball.Direction;

            float spreadAngle = 45f; // угол разлёта
            float step = spreadAngle / (count + 1);

            for (int i = 0; i < count; i++)
            {
                float angle = -spreadAngle/2 + step * (i + 1);

                Vector2 dir = Quaternion.Euler(0, 0, angle) * baseDirection;

                Ball newBall = SpawnBall(ball.transform.position);

                newBall.Launch(dir.normalized);
            }
        }
    }
}