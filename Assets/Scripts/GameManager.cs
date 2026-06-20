using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{    
    [SerializeField] GameObject openTilePrefab;
    [SerializeField] float xIncrement;
    [SerializeField] float yIncrement;
    [SerializeField] float xStart;
    [SerializeField] float yStart;

    GameObject openTilesScript;
    private void Start()
    {
        openTilesScript = GameObject.FindGameObjectWithTag("OpenTile");

        //Spawn all the open tile prefabs in the grid

        for (int i = 0; i < 3; i++)
        { 
            for (int j = 0; j < 3; j++)
            {
                //Increment the space between each prefab to fit into the grid
                Instantiate(openTilePrefab, new Vector2(xIncrement * i - xStart, yIncrement * j - yStart), Quaternion.identity);
            }
        }
    }

    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            //Check the position of the mouse
            //if the mouse position is in a open tile box collider
            //spawn a piece and destroy the open tile prefab

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            OpenTiles ot = openTilesScript.GetComponent<OpenTiles>();

            if (ot.GetBoxCollider().bounds.Contains(mousePosition))
            {
                ot.DestroyThisTile();
            }
        }
    }
}
