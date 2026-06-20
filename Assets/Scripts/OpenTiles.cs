using UnityEngine;

public class OpenTiles : MonoBehaviour
{
    BoxCollider2D bc;

    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    public BoxCollider2D GetBoxCollider()
    {
        return bc;
    }

    public void DestroyThisTile()
    {
        Destroy(gameObject);
    }
}
