using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;
using TMPro;

public class GameManager : MonoBehaviour
{
    GameObject[,] positions = new GameObject[3, 3];
    GameObject[] openTiles = new GameObject[9];

    public GameObject openTilePrefab;
    public GameObject piece;

    public string currentPlayer = "xPlayer";

    private bool gameOver;

    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI restartText;

    private void Start()
    {
        gameOver = false;
        openTiles = new GameObject[]
        {
            SpawnOpenTilePrefab(0, 0), SpawnOpenTilePrefab(0, 1), SpawnOpenTilePrefab(0, 2),
            SpawnOpenTilePrefab(1, 0), SpawnOpenTilePrefab(1, 1), SpawnOpenTilePrefab(1, 2),
            SpawnOpenTilePrefab(2, 0), SpawnOpenTilePrefab(2, 1), SpawnOpenTilePrefab(2, 2),
        };

        winnerText.enabled = false;

        restartText.enabled = false;

        //for (int i = 0; i < openTiles.Length; i++)
        //{
        //    SetPosition(openTiles[i]);
        //}
    }
    private GameObject SpawnOpenTilePrefab(int x, int y)
    {
        GameObject obj = Instantiate(openTilePrefab, new Vector2(x, y), Quaternion.identity);
        OpenTiles ot = obj.GetComponent<OpenTiles>();

        ot.SetXBoard(x);
        ot.SetYBoard(y);

        return obj;
    }
    //public void SetPosition(GameObject obj)
    //{
    //    OpenTiles ot = obj.GetComponent<OpenTiles>();

    //    positions[ot.GetXBoard(), ot.GetYBoard()] = obj;
    //}
    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            if (gameOver)
            {
                SceneManager.LoadScene("GameScene"); 
            }

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedObject = hit.collider.gameObject;

                OpenTiles ot = clickedObject.GetComponent<OpenTiles>();

                GameObject newPiece;

                if (currentPlayer == "xPlayer")
                {
                    newPiece = CreatePiece("xPiece", clickedObject.transform.position);
                }
                else
                {
                    newPiece = CreatePiece("oPiece", clickedObject.transform.position);                    
                }

                positions[ot.GetXBoard(), ot.GetYBoard()] = newPiece;

                Destroy(clickedObject);
                if (Winner())
                {
                    print(currentPlayer + " Won");
                }
                else
                {
                    NextTurn();
                }
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
    public bool Winner()
    {
        //HORIZONTAL WINS
        if (IsPlayerAt(0, 0, currentPlayer) && IsPlayerAt(1, 0, currentPlayer) && IsPlayerAt(2, 0, currentPlayer))
        {
            return true;
        }
        if (IsPlayerAt(0, 1, currentPlayer) && IsPlayerAt(1, 1, currentPlayer) && IsPlayerAt(2, 1, currentPlayer))
        {
            return true;
        }
        if (IsPlayerAt(0, 2, currentPlayer) && IsPlayerAt(1, 2, currentPlayer) && IsPlayerAt(2, 2, currentPlayer))
        {
            return true;
        }
        //VERTICAL WINS
        if (IsPlayerAt(0, 0, currentPlayer) && IsPlayerAt(0, 1, currentPlayer) && IsPlayerAt(0, 2, currentPlayer))
        {
            return true;
        }
        if (IsPlayerAt(1, 0, currentPlayer) && IsPlayerAt(1, 1, currentPlayer) && IsPlayerAt(1, 2, currentPlayer))
        {
            return true;
        }
        if (IsPlayerAt(2, 0, currentPlayer) && IsPlayerAt(2, 1, currentPlayer) && IsPlayerAt(2, 2, currentPlayer))
        {
            return true;
        }
        //DIAGONAL WINS
        if (IsPlayerAt(0, 0, currentPlayer) && IsPlayerAt(1, 1, currentPlayer) && IsPlayerAt(2, 2, currentPlayer))
        {
            return true;
        }
        if (IsPlayerAt(0, 2, currentPlayer) && IsPlayerAt(1, 1, currentPlayer) && IsPlayerAt(2, 0, currentPlayer))
        {
            return true;
        }

        return false;
    }
    public bool IsPlayerAt(int x, int y, string player)
    {
        if (positions[x, y] == null)
        {
            return false;
        }

        if (positions[x, y].GetComponent<Pieces>().player == player)
        {
            return true;
        }

        return false;
    }
    private void Update()
    {
        if (Winner())
        {
            gameOver = true;

            winnerText.enabled = true;
            winnerText.text = currentPlayer + " won";

            restartText.enabled = true;
        }
    }
}
