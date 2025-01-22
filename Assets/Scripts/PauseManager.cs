using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;
    private bool axisInUse = false;  // Prevent rapid toggling when key is held

    void Start()
    {
        Time.timeScale = GameManager.Instance.GameSpeed;
    }
    void Update()
    {
        float pauseInput = Input.GetAxis("Pause");

        if (pauseInput > 0 && !axisInUse)
        {

            axisInUse = true;  // Mark axis as in use to prevent spam

            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }

            StartCoroutine(ResetAxisUsage());  // Cooldown before detecting again
        }
        else if (pauseInput == 0)
        {
            axisInUse = false;  // Reset when key is released

        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0.01f;  // Completely pause the game
        pausePanel.SetActive(true);  // Show pause menu
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = GameManager.Instance.GameSpeed; ;  // Resume normal game speed
        pausePanel.SetActive(false);  // Hide pause menu
        isPaused = false;
    }

    public void newGame()
    {
        Time.timeScale = GameManager.Instance.GameSpeed; ;  // Resume normal game speed

        SceneManager.LoadScene(0);
    }

    // Cooldown to avoid rapid key presses
    private IEnumerator ResetAxisUsage()
    {
        yield return new WaitForSecondsRealtime(0.3f);  // Adjust delay to your liking
        axisInUse = false;
    }
}
