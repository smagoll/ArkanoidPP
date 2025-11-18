using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DeathZone deathZone;
    [SerializeField] private BallSpawner ballSpawner;
    [SerializeField] private Transform platformTransform;
    [SerializeField] private Transform levelRoot;

    private Level level;
    private List<Ball> balls = new(); // список всех шаров на сцене

    private void Start()
    {
        deathZone.OnBallEntered += BallExit;
        level = new Level();
        
        foreach (var brick in levelRoot.GetComponentsInChildren<Brick>())
        {
            level.RegisterBrick(brick);
        }

        level.OnLevelCleared += HandleLevelCleared;

        var ball = SpawnBall(platformTransform.position + new Vector3(0, 2f, 0));
        ball.AttachToPlatform(platformTransform);
    }

    private void Update()
    {
        if (level.IsActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            level.StartLevel();
            LaunchAllBalls(Vector2.up);
        }
    }
    
    private Ball SpawnBall(Vector3 position)
    {
        Ball ball = ballSpawner.Spawn(position);
        ball.ResetBall(platformTransform);
        balls.Add(ball);
        
        return ball;
    }
    
    private void LaunchAllBalls(Vector2 direction)
    {
        foreach (var ball in balls)
            ball.Launch(direction);
    }

    private void BallExit(Ball ball)
    {
        ball.Destroy();
        balls.Remove(ball);
        
        if(balls.Count <= 0)
        {
            UIManager.instance.ShowFailed();
        }
    }

    private void HandleLevelCleared()
    {
        level.StopLevel();
        
        foreach (var ball in balls)
        {
            ball.Destroy();
        }
        balls.Clear();
        
        UIManager.instance.ShowComplete();
    }
}