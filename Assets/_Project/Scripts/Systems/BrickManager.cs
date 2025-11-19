using System;
using System.Collections.Generic;

public class BrickManager
{
    private readonly List<Brick> _activeBricks = new();

    public int BrickCount => _activeBricks.Count;
    public event Action OnAllBricksDestroyed;
    public event Action<Brick> OnBricksDestroyed;

    public void AddBrick(Brick brick)
    {
        if (brick == null) return;

        _activeBricks.Add(brick);
        brick.OnDestroyed += HandleBrickDestroyed;
    }

    private void HandleBrickDestroyed(Brick brick)
    {
        brick.OnDestroyed -= HandleBrickDestroyed;
        _activeBricks.Remove(brick);
        
        OnBricksDestroyed?.Invoke(brick);

        if (_activeBricks.Count == 0)
            OnAllBricksDestroyed?.Invoke();
    }

    public void Clear()
    {
        foreach (var brick in _activeBricks)
            brick.OnDestroyed -= HandleBrickDestroyed;

        _activeBricks.Clear();
    }
}