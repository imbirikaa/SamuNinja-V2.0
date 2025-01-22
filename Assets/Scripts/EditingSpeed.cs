using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EditingSpeed : MonoBehaviour
{
    [SerializeField] private Slider speedSlider;

    public TMP_Text volumeText;


    private void Start()
    {
        speedSlider.value = GameManager.Instance.GameSpeed;

        speedSlider.onValueChanged.AddListener(SetGameSpeed);

        volumeText.text = speedSlider.value.ToString("F1");

    }

    private void SetGameSpeed(float value)
    {
        GameManager.Instance.GameSpeed = value;
        Time.timeScale = value;

        volumeText.text = value.ToString("F1");
    }
}
