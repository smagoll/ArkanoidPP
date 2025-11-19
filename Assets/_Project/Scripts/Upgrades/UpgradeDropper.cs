using UnityEngine;

public class UpgradeDropper : MonoBehaviour
{
    [SerializeField] private UpgradePickup pickupPrefab;
    [SerializeField] private UpgradeEffect[] effects;

    [Range(0f, 1f)]
    [SerializeField] private float dropChance = 0.25f;
    
    private GameManager _gameManager;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;

        _gameManager.BrickManager.OnBricksDestroyed += HandleBrickDestroyed;
    }

    private void HandleBrickDestroyed(Brick brick)
    {
        TryDrop(brick.transform.position);
    }

    public void TryDrop(Vector3 position)
    {
        if (Random.value > dropChance)
            return;

        UpgradeEffect effect = effects[Random.Range(0, effects.Length)];

        UpgradePickup pickup = Instantiate(pickupPrefab, position, Quaternion.identity);
        pickup.Init(effect, _gameManager);
    }
}