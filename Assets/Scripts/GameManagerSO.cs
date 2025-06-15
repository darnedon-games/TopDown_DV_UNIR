using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName="Scriptable Objects/GameManager")]

public class GameManagerSO : ScriptableObject
{

    private Player player;
    private InventorySystem inventory;

    public InventorySystem Inventory { get => inventory; }

    [System.Obsolete]
    private void OnEnable() //Llamadas por EVENTO
    {
        SceneManager.sceneLoaded += NewSceneLoaded;
    }

    [System.Obsolete]
    private void NewSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        player = FindObjectOfType<Player>();
        inventory = FindObjectOfType<InventorySystem>();
    }

    public void ChangePlayerState(bool state)
    {
        player.Interacting = !state;
    }
}
