using UnityEngine;

public class FlagController : MonoBehaviour
{
    public GameObject stoneBuilderPrefab; // The StoneBuilder prefab to instantiate
    private GameObject instantiatedStoneBuilder;

    void OnMouseDown()
    {
        if (instantiatedStoneBuilder == null)
        {
            // Instantiate the StoneBuilder at the flag's position
            instantiatedStoneBuilder = Instantiate(stoneBuilderPrefab, transform.position, Quaternion.identity);
            
            // Destroy the flag
            Destroy(gameObject);
        }
    }
}
