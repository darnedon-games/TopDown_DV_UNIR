using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName="Scriptable Objects/GameManager")]

public class GameManagerSO : ScriptableObject
{

    private Player player;

    [System.Obsolete]
    private void OnEnable() //Llamadas por EVENTO
    {
        SceneManager.sceneLoaded += NewSceneLoaded;
    }

    [System.Obsolete]
    private void NewSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        player = FindObjectOfType<Player>();
    }

    public void ChangePlayerState(bool state)
    {
        player.Interacting = !state;
    }
}
