using UnityEngine;

public class Pickaxe : MonoBehaviour, Interactable
{
    public void Interact()
    {
        Destroy(this.gameObject);
    }
}
