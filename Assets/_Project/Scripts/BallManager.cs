using System;
using System.Collections.Generic;
using UnityEngine;

public class BallManager
{
    private readonly BallSpawner _spawner;
    private readonly Transform _platform;
    private readonly List<Ball> _activeBalls = new();

    public int BallCount => _activeBalls.Count;
    public event Action OnAllBallsLost;

    public BallManager(BallSpawner spawner, Transform platform)
    {
        _spawner = spawner;
        _platform = platform;
    }

    public Ball SpawnBall(Vector3 position, bool attachToPlatform = false)
    {
        Ball ball = _spawner.Spawn(position);
        ball.ResetBall(_platform);

        if (attachToPlatform)
            ball.AttachToPlatform(_platform);

        _activeBalls.Add(ball);
        return ball;
    }

    public void RemoveBall(Ball ball)
    {
        if (ball == null || !_activeBalls.Contains(ball)) return;

        _activeBalls.Remove(ball);
        ball.Destroy();

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
            ball.Destroy();

        _activeBalls.Clear();
    }
}