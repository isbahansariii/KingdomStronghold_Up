/*
This setup allows players to convert a flag into a tower-building area in the game. 
 - When the player clicks on the flag, 
 - it spawns a "StoneBuilder" at the flag's location
 - then destroys the flag itself. 
*/

using UnityEngine;

public class FlagController : MonoBehaviour
{
    public GameObject stoneBuilderPrefab; // The StoneBuilder prefab to instantiate

    // Holds the instantiated StoneBuilder object, used to check if one already exists
    private GameObject instantiatedStoneBuilder;

    void OnMouseDown()
    {
        if (instantiatedStoneBuilder == null)
        {
            // Instantiate = function which takes 4 sec
            // Instantiate the StoneBuilder at the flag's position
            instantiatedStoneBuilder = Instantiate(stoneBuilderPrefab, transform.position, Quaternion.identity);
            
            // Destroy the flag
            Destroy(gameObject);
        }
    }
}
