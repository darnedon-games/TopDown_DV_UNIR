using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Patrón Singleton: 1. "Single" es ÚNICO -- 2. El acceso es global desde cualquier parte del programa

    public static GameManager Instance { get; private set; }

    public Vector3 lastSavedPosition { get; private set; } = new Vector3(0.5f, 1.5f, 0);
    public Vector3 lastSavedRotation { get; private set; } = new Vector3(0,-1,0);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNewScene(int newSceneIndex, Vector3 newPlayerPosition, Vector2 newPlayerRotation)
    {
        lastSavedPosition = newPlayerPosition;
        lastSavedRotation = newPlayerRotation;
        SceneManager.LoadScene(newSceneIndex);
    }
}
