/*
This script is designed to handle interactions with a "fire stone" object in a tower defense game. 
When the player clicks on the fire stone, the game checks if they have enough gold to place a tower. 
If they can afford it, the stone is destroyed, and a tower is placed at that position. 
If the player can’t afford the tower, a warning message is displayed in the console.
*/

using UnityEngine;

public class FireStoneController : MonoBehaviour
{
    public GameObject stoneBuilder; //Reference to the stone builder object
    public GameObject towerPrefab;  // Assign the appropriate tower prefab
    public int towerCost = 50;      // The cost of the tower

    void OnMouseDown()
    {
        if (GameManager.instance.CanAfford(towerCost))
        {
            // Spend gold and destroy the StoneBuilder and all remaining stones
            GameManager.instance.SpendGold(towerCost);
            Destroy(stoneBuilder);

            // Placing the selected tower
            Instantiate(towerPrefab, transform.position, Quaternion.identity);

            // Destroying stone object after placing tower
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Not enough gold to place the tower.");
        }
    }
}

