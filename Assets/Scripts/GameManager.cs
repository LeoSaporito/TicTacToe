using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    GameObject[,] positions = new GameObject[3, 3];
    GameObject[] openTiles = new GameObject[9];

    public GameObject openTilePrefab;
    public GameObject piece;

    public string currentPlayer = "xPlayer";

    private bool gameOver;
    private void Start()
    {
        gameOver = false;
        openTiles = new GameObject[]
        {
            SpawnOpenTilePrefab(0, 0), SpawnOpenTilePrefab(0, 1), SpawnOpenTilePrefab(0, 2),
            SpawnOpenTilePrefab(1, 0), SpawnOpenTilePrefab(1, 1), SpawnOpenTilePrefab(1, 2),
            SpawnOpenTilePrefab(2, 0), SpawnOpenTilePrefab(2, 1), SpawnOpenTilePrefab(2, 2),
        };

        for (int i = 0; i < openTiles.Length; i++)
        {
            SetPosition(openTiles[i]);
        }
    }
    private GameObject SpawnOpenTilePrefab(int x, int y)
    {
        GameObject obj = Instantiate(openTilePrefab, new Vector2(x, y), Quaternion.identity);
        OpenTiles ot = obj.GetComponent<OpenTiles>();

        ot.SetXBoard(x);
        ot.SetYBoard(y);

        return obj;
    }
    public void SetPosition(GameObject obj)
    {
        OpenTiles ot = obj.GetComponent<OpenTiles>();

        positions[ot.GetXBoard(), ot.GetYBoard()] = obj;
    }
    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (currentPlayer == "xPlayer")
                {
                    CreatePiece("xPiece", clickedObject.transform.position);
                }
                else if (currentPlayer != "xPlayer")
                {
                    CreatePiece("oPiece", clickedObject.transform.position);                    
                }

                Destroy(clickedObject);
                NextTurn();
            }
        }
    }
    public GameObject CreatePiece(string name, Vector2 position)
    {
        GameObject obj = Instantiate(piece, position, Quaternion.identity);

        Pieces p = obj.GetComponent<Pieces>();
        p.name = name;
        p.Activate();

        return obj;
    }
    void NextTurn()
    {
        if (currentPlayer == "xPlayer")
        {
            currentPlayer = "oPlayer";
        }
        else
        {
            currentPlayer = "xPlayer";
        }
    }
    void Winner()
    {
        if (positions[0, 2] && positions[1, 2] && positions[2, 2])
        {
            gameOver = true;
            print("Game Over");
        }
        else
        {
            gameOver = false;
        }
    }
    private void Update()
    {
        Winner();
    }
}
