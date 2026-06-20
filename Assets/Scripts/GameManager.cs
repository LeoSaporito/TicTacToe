using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //GameManager will contain the board positions
    //Spawns the pieces on each click
    //Record which spot on the grid has a piece and which spot is empty

    [SerializeField] GameObject playerPiece;

    GameObject[,] positions = new GameObject[3, 3];

    GameObject[] xPrefabs = new GameObject[5];
    GameObject[] oPrefabs = new GameObject[4];

    //string currentPlayer = "x";

    void Start()
    {
        //When the game starts, clear all positions on the board
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i, i] = null;
        }
    }

    //Create the players piece when they click
    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
           Create();            
        }
    }

    public GameObject Create()
    {
        //Get the position in the grid where the player clicked
        //Spawn the piece in that position
        
        GameObject obj = Instantiate(playerPiece, new Vector2(0, 0), Quaternion.identity);
        Pieces pieces = obj.GetComponent<Pieces>();

        return obj;
    }
}
