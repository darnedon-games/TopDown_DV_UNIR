using UnityEngine;

public class Item : MonoBehaviour, Interactable
{
    [SerializeField] private ItemSO myData;
    [SerializeField] private GameManagerSO gameManager;
    public void Interact()
    {
        gameManager.Inventory.NewItem(myData);
        Destroy(this.gameObject);
    }
}
