using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour
{

    public GameObject panel;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void openOptions()
    {

        panel.gameObject.SetActive(true);


    }
    public void closeOptions()
    {

        panel.gameObject.SetActive(false);


    }
}
