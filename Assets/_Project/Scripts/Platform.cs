using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float edgeOffset = 0.3f;
    [SerializeField] private float moveSpeed = 10f;

    private Camera cam;
    private float minX;
    private float maxX;

    private float halfWidth;

    private void Start()
    {
        cam = Camera.main;
        
        halfWidth = GetComponent<BoxCollider2D>().size.x * transform.localScale.x / 2f;

        UpdateBounds();
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
}