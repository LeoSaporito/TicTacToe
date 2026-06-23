using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class Pieces : MonoBehaviour
{
    public Sprite xs, os;
    public string player;
   public void Activate()
    {
        switch (this.name)
        {
            case "xPiece": this.GetComponent<SpriteRenderer>().sprite = xs; player = "xPlayer"; break;
            case "oPiece": this.GetComponent<SpriteRenderer>().sprite = os; player = "oPlayer"; break;
        }
    }
    public GameObject GetPiece()
    {
        return gameObject;
    }
}
