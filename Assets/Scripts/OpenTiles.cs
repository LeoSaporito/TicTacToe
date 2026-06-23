using UnityEngine;
using UnityEngine.InputSystem;

public class OpenTiles : MonoBehaviour
{
    public int xBoard;
    public int yBoard;

    public float xOffset;
    public float yOffset;
    public float spacing;

    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Start()
    {
        SetCoords();        
    }

    public int GetXBoard()
    {
        return xBoard;
    }
    public int GetYBoard()
    {
        return yBoard;
    }
    public void SetXBoard(int x)
    {
        xBoard = x;
    }
    public void SetYBoard(int y)
    {
        yBoard = y;
    }
    void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= xOffset;
        y *= yOffset;

        x += spacing;
        y += spacing;

        this.transform.position = new Vector2(x, y);
    }
}
