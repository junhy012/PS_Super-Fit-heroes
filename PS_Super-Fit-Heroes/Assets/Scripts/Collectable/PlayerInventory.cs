using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int collectedItems = 0;     
    public int requiredItems = 3;      
    public bool HasAllItems()
    {
        return collectedItems >= requiredItems;
    }

    public void CollectItem()
    {
        collectedItems++;
        Debug.Log("Item collected: " + collectedItems);
    }

    public void ResetItems()
    {
        collectedItems = 0;
    }
}
