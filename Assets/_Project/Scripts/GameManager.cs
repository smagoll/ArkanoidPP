using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private BallSpawner ballSpawner;
    [SerializeField] private Transform levelRoot;

    private Level level;

    private void Start()
    {
        level = new Level();
        
        foreach (var brick in levelRoot.GetComponentsInChildren<Brick>())
        {
            level.RegisterBrick(brick);
        }

        level.OnLevelCleared += HandleLevelCleared;
    }

    private void Update()
    {
        if (level.IsActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            level.StartLevel();
            ball.Launch(new Vector2(1, 1));
        }
    }

    private void HandleLevelCleared()
    {
        Debug.Log("Уровень пройден!");
        level.StopLevel();
    }
}