using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    public Transform player;
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip deathSound;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure the player is tagged 'Player'");
        }
    }

    public void PlayAttackSound()
    {
        if (attackSound != null && audioSource != null)
        {
            AdjustAudioPan();
            audioSource.PlayOneShot(attackSound);
        }
        else
        {
            Debug.LogWarning("Missing audio source or attack sound clip!");
        }
    }

    public void PlayDeathSound()
    {
        if (deathSound != null && audioSource != null)
        {
            AdjustAudioPan();
            audioSource.PlayOneShot(deathSound);
        }
        else
        {
            Debug.LogWarning("Missing audio source or attack sound clip!");
        }
    }

    private void AdjustAudioPan()
    {
        if (player == null)
        {
            Debug.LogWarning("Player reference not set on EnemySounds!");
            return;
        }

        float direction = transform.position.x - player.position.x;

        // Stereo panning (-1 left, 1 right)
        float panStereo = Mathf.Clamp(direction / 10f, -1f, 1f);
        audioSource.panStereo = panStereo;

        // Adjust volume based on distance
        float distance = Vector3.Distance(transform.position, player.position);
        float maxDistance = 50f;  // Set your preferred max distance
        float minDistance = 5f;   // Minimum distance for full volume
        float volume = Mathf.Clamp01(1 - (distance - minDistance) / (maxDistance - minDistance));

        audioSource.volume = volume;
    }
}
