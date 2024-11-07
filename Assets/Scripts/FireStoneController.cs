using UnityEngine;

public class FireStoneController : MonoBehaviour
{
    public GameObject stoneBuilder;
    public GameObject towerPrefab; // Assign the appropriate tower prefab
    public int towerCost = 50; // The cost of the tower

    void OnMouseDown()
    {
        if (GameManager.instance.CanAfford(towerCost))
        {
            // Spend gold and destroy the StoneBuilder and all remaining stones
            GameManager.instance.SpendGold(towerCost);
            Destroy(stoneBuilder);

            // Instantiate the selected tower
            Instantiate(towerPrefab, transform.position, Quaternion.identity);

            // Optionally, you can also destroy the stone itself if needed
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Not enough gold to place the tower.");
        }
    }
}

