using UnityEngine;

public class PlayerAttackSound : MonoBehaviour
{
    public AudioSource audioSource;  
    public AudioClip attackSound;  
    public AudioClip consumeSound;  
    public AudioClip healSound;  

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlayAttackSound()
    {
        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound); 
        }
        else
        {
            Debug.LogWarning("Missing audio source or attack sound clip!");
        }
    }
    public void PlayConsumeSound()
    {
        if (consumeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(consumeSound); 
        }
        else
        {
            Debug.LogWarning("Missing audio source or attack sound clip!");
        }
    }
    public void PlayHealSound()
    {
        if (healSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(healSound); 
        }
        else
        {
            Debug.LogWarning("Missing audio source or attack sound clip!");
        }
    }
}
