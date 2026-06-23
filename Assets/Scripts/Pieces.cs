using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class Pieces : MonoBehaviour
{
    public Sprite xs, os;
    private string player;
   public void Activate()
    {
        switch (this.name)
        {
            case "xPiece": this.GetComponent<SpriteRenderer>().sprite = xs; break;
            case "oPiece": this.GetComponent<SpriteRenderer>().sprite = os; break;
        }
    }
    public GameObject GetPiece()
    {
        return gameObject;
    }
}
