using System;
using System.Collections.Generic;

public class Level
{
    private List<Brick> bricks = new List<Brick>();

    public bool IsActive { get; private set; }

    public event Action OnLevelCleared;

    public void StartLevel()
    {
        IsActive = true;
    }

    public void StopLevel()
    {
        IsActive = false;
    }

    public void RegisterBrick(Brick brick)
    {
        if (brick == null) return;

        bricks.Add(brick);
        brick.OnDestroyed += HandleBrickDestroyed;
    }

    private void HandleBrickDestroyed(Brick brick)
    {
        bricks.Remove(brick);

        if (bricks.Count == 0)
        {
            OnLevelCleared?.Invoke();
        }
    }

    public int RemainingBricks => bricks.Count;
}