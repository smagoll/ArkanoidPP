using System;

public class LevelController
{
    private readonly BrickManager _brickManager;
    private readonly BallManager _ballManager;

    public bool IsGameActive { get; private set; }

    public event Action OnLevelCompleted;
    public event Action OnLevelFailed;

    public LevelController(BrickManager brickManager, BallManager ballManager)
    {
        _brickManager = brickManager;
        _ballManager = ballManager;

        brickManager.OnAllBricksDestroyed += HandleLevelCompleted;
        ballManager.OnAllBallsLost += HandleLevelFailed;
    }

    public void StartGame()
    {
        IsGameActive = true;
    }

    private void HandleLevelCompleted()
    {
        IsGameActive = false;
        OnLevelCompleted?.Invoke();
    }

    private void HandleLevelFailed()
    {
        IsGameActive = false;
        OnLevelFailed?.Invoke();
    }

    ~LevelController()
    {
        _brickManager.OnAllBricksDestroyed -= HandleLevelCompleted;
        _ballManager.OnAllBallsLost -= HandleLevelFailed;
    }
}