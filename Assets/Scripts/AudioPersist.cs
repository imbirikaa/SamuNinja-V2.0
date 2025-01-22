using UnityEngine;
using UnityEngine.Audio;

public class AudioPersist : MonoBehaviour
{
    public static AudioPersist instance;
    public AudioMixer audioMixer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVolume();
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void LoadVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        float adjustedVolume = Mathf.Log10(Mathf.Clamp(savedVolume, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("MasterVolume", adjustedVolume);
    }
}
