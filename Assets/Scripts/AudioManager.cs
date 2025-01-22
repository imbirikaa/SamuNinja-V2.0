using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class VolumeManager : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer audioMixer;
    public TMP_Text volumeText;
    private const string volumePrefKey = "MasterVolume";

    void Start()
    {

        float savedVolume = PlayerPrefs.GetFloat(volumePrefKey, 0.5f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);


        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);


        PlayerPrefs.SetFloat(volumePrefKey, volume);
        PlayerPrefs.Save();

        volumeText.text = Mathf.RoundToInt(volume * 100) + "%";
    }
}
