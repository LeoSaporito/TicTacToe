using UnityEngine;

public class Pieces : MonoBehaviour
{
    //Contains the sprites and the piece type
    public Sprite x;
    public Sprite o;

    void Start()
    {               
        gameObject.GetComponent<SpriteRenderer>().sprite = x;        
    }
}
