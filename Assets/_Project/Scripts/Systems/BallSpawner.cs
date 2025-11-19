using UnityEngine;
using UnityEngine.Pool;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Transform ballRoot;

    private ObjectPool<Ball> _ballPool;

    private void Awake()
    {
        _ballPool = new ObjectPool<Ball>(
            CreatePooledBall,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledObject
        );
    }

    private Ball CreatePooledBall()
    {
        Ball ball = Instantiate(ballPrefab, ballRoot);
        ball.SetPool(_ballPool);
        return ball;
    }

    private void OnGetFromPool(Ball ball)
    {
        ball.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Ball ball)
    {
        ball.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Ball ball)
    {
        Destroy(ball.gameObject);
    }

    public Ball Spawn(Vector2 position)
    {
        Ball ball = _ballPool.Get();
        ball.transform.position = position;
        return ball;
    }
}