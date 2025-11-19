using TMPro;
using UnityEngine;

public class UpgradePickup : MonoBehaviour
{
    private UpgradeEffect _effect;
    private GameManager _gameManager;
    [SerializeField] private float fallSpeed = 2f;
    [SerializeField] private TextMeshProUGUI titleText;

    public void Init(UpgradeEffect effect, GameManager gameManager)
    {
        _effect = effect;
        _gameManager = gameManager;
        
        titleText.text = _effect.effectName;
    }

    private void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            _effect.Apply(_gameManager);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("DeathZone"))
        {
            Destroy(gameObject);
        }
    }
}