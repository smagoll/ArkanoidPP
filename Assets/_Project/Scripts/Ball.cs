using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float minYAngle = 0.25f; 
    [SerializeField] private float minXAngle = 0.25f; 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float damage = 1f;

    public float Speed => _ballSpeed.CurrentSpeed;

    private BallSpeed _ballSpeed;
    
    private Transform platform;
    private Vector3 offset; 
    
    private Vector2 direction;
    private bool launched = false;

    public void Init(BallSpeed ballSpeed)
    {
        _ballSpeed = ballSpeed;
    }
    
    public void Launch(Vector2 startDirection)
    {
        if (launched) return;

        launched = true;
        direction = startDirection.normalized;
    }
    
    public void AttachToPlatform(Transform platformTransform)
    {
        platform = platformTransform;
        launched = false;
        offset = transform.position - platform.position;
        rb.linearVelocity = Vector2.zero;
    }

    private void Update()
    {
        if (!launched && platform != null)
        {
            transform.position = platform.position + offset;
        }
    }
    
    private void FixedUpdate()
    {
        if (!launched) return;
        
        rb.MovePosition(rb.position + direction * Speed * Time.fixedDeltaTime);
    }

    public void ResetBall(Transform platform)
    {
        launched = false;

        rb.linearVelocity = Vector2.zero;

        transform.position = platform.position + new Vector3(0, 0.5f, 0);
    }
    
    private void FixMinAngle()
    {
        if (Mathf.Abs(direction.y) < minYAngle)
        {
            direction.y = Mathf.Sign(direction.y) * minYAngle;
            direction = direction.normalized;
        }
        
        if (Mathf.Abs(direction.x) < minXAngle)
        {
            direction.x = Mathf.Sign(direction.x) * minXAngle;
            direction = direction.normalized;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Platform"))
        {
            BounceFromPlatform(other);
        }
        else
        {
            Bounce(other.contacts[0].normal);
        }
        
        FixMinAngle();
        
        var damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
    
    private void Bounce(Vector2 normal)
    {
        direction = Vector2.Reflect(direction, normal).normalized;
    }

    private void BounceFromPlatform(Collision2D collision)
    {
        float hitPoint = transform.position.x - collision.transform.position.x;
        float halfWidth = collision.collider.bounds.size.x / 2f;
        float factor = hitPoint / halfWidth;

        direction = new Vector2(factor, 1).normalized;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}