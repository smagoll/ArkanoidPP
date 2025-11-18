using UnityEngine;

public class AutoBorders : MonoBehaviour
{
    public BoxCollider2D leftWall;
    public BoxCollider2D rightWall;
    public BoxCollider2D topWall;
    public BoxCollider2D bottomWall;

    public float thickness = 1f; // толщина стен

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        SetupBorders();
    }

    private void SetupBorders()
    {
        // мировые координаты видимой области камеры
        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float worldWidth = topRight.x - bottomLeft.x;
        float worldHeight = topRight.y - bottomLeft.y;

        // =======================
        //  ЛЕВАЯ СТЕНКА
        // =======================
        leftWall.size = new Vector2(thickness, worldHeight + thickness * 2);
        leftWall.transform.position = new Vector3(bottomLeft.x - thickness / 2, 0, 0);

        // =======================
        //  ПРАВАЯ СТЕНКА
        // =======================
        rightWall.size = new Vector2(thickness, worldHeight + thickness * 2);
        rightWall.transform.position = new Vector3(topRight.x + thickness / 2, 0, 0);

        // =======================
        //  ВЕРХНЯЯ СТЕНКА
        // =======================
        topWall.size = new Vector2(worldWidth + thickness * 2, thickness);
        topWall.transform.position = new Vector3(0, topRight.y + thickness / 2, 0);

        // =======================
        //  НИЖНЯЯ СТЕНКА
        // =======================
        bottomWall.size = new Vector2(worldWidth + thickness * 2, thickness);
        bottomWall.transform.position = new Vector3(0, bottomLeft.y - thickness / 2, 0);
    }
}