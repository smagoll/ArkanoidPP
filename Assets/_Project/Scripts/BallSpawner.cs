using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Transform ballRoot;

    public Ball Spawn(Vector2 position)
    {
        return Instantiate(ballPrefab, position, Quaternion.identity, ballRoot);
    }
}