using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float edgeOffset = 0.3f;
    [SerializeField] private float bottomOffset = 0.5f;

    private Camera cam;
    private float minX;
    private float maxX;

    private float halfWidth;
    private float halfHeight;

    private void Start()
    {
        cam = Camera.main;
        
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        halfWidth = col.size.x * transform.localScale.x / 2f;
        halfHeight = col.size.y * transform.localScale.y / 2f;

        UpdateBounds();
        SetStartVerticalPosition();
    }

    private void Update()
    {
        UpdateBounds();

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        float targetX = Mathf.Clamp(mousePos.x, minX, maxX);

        transform.position = new Vector3(
            targetX,
            transform.position.y,
            transform.position.z
        );
    }

    private void UpdateBounds()
    {
        Vector3 left = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 right = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        minX = left.x + halfWidth + edgeOffset;
        maxX = right.x - halfWidth - edgeOffset;
    }
    
    private void SetStartVerticalPosition()
    {
        Vector3 bottom = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));

        float newY = bottom.y + halfHeight + bottomOffset;

        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );
    }
}