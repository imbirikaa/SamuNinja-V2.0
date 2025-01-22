using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float GameSpeed = 1.0f; // Default game speed

    private void Awake()
    {
        // Ensure this GameManager persists across scenes
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
}
