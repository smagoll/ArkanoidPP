using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DeathZone deathZone;
    [SerializeField] private BallSpawner ballSpawner;
    [SerializeField] private Transform levelRoot;
    [SerializeField] private Platform platform;

    private LevelController levelController;
    private BallManager ballManager;
    private BrickManager brickManager;

    private void Start()
    {
        InitializeManagers();
        SetupLevel();
        SpawnInitialBall();
    }

    private void InitializeManagers()
    {
        brickManager = new BrickManager();
        ballManager = new BallManager(ballSpawner, platform.transform);
        levelController = new LevelController(brickManager, ballManager);

        // Подключаем UI
        levelController.OnLevelCompleted += Complete;
        levelController.OnLevelFailed += Fail;

        // Подключаем зону смерти
        deathZone.OnBallEntered += ballManager.RemoveBall;
    }

    private void SetupLevel()
    {
        foreach (var brick in levelRoot.GetComponentsInChildren<Brick>())
            brickManager.AddBrick(brick);
    }

    private void SpawnInitialBall()
    {
        Vector3 spawnPos = platform.transform.position + Vector3.up * 2f;
        ballManager.SpawnBall(spawnPos, attachToPlatform: true);
    }

    private void Complete()
    {
        ClearLevel();
        UIManager.instance.ShowComplete();
    }

    private void Fail()
    {
        ClearLevel();
        UIManager.instance.ShowFailed();
    }

    private void ClearLevel()
    {
        brickManager.Clear();
        ballManager.Clear();
        platform.IsActive = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !levelController.IsGameActive)
        {
            levelController.StartGame();
            ballManager.LaunchAllBalls(Vector2.up);
        }
    }

    private void OnDestroy()
    {
        if (levelController != null)
        {
            levelController.OnLevelCompleted -= UIManager.instance.ShowComplete;
            levelController.OnLevelFailed -= UIManager.instance.ShowFailed;
        }

        if (deathZone != null)
            deathZone.OnBallEntered -= ballManager.RemoveBall;
    }
}